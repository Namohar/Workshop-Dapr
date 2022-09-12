
using Dapr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// needed for Dapr pub/sub routing
app.MapSubscribeHandler();


app.Use(async (httpContext, next) =>
{
  string? charSet;
  string ContentType = "application/cloudevents+json";
  var match = true;
  Stream body;
  string? contentType;
  if (httpContext.Request.ContentType == null)
  {
      charSet = null;
      match = false;
  }

  // Handle cases where the content type includes additional parameters like charset.
  // Doing the string comparison up front so we can avoid allocation.
  if (httpContext.Request.ContentType != null && !httpContext.Request.ContentType.StartsWith(ContentType))
  {
      charSet = null;
      match = false;
  }

  if (!MediaTypeHeaderValue.TryParse(httpContext.Request.ContentType, out var p))
  {
      charSet = null;
      match = false;
  }

  if (p?.MediaType != ContentType)
  {
      charSet = null;
      match = false;
  }
  if(match){
    charSet = p?.Charset.Length > 0 ? p?.Charset.Value : "UTF-8";
    if (string.Equals(charSet, Encoding.UTF8.WebName, StringComparison.OrdinalIgnoreCase))
    {    
        var json = await JsonSerializer.DeserializeAsync<JsonElement>(httpContext.Request.Body);
        var isTokenSet = json.TryGetProperty("tokenstring", out var token);
        Console.WriteLine("TokenString in middleware = " + token);
        json.TryGetProperty("data", out var data);
        json.TryGetProperty("datacontenttype", out var dataContentType);
        contentType = dataContentType.GetString();
        body = new MemoryStream();
        await JsonSerializer.SerializeAsync<JsonElement>(body, data);
        body.Seek(0L, SeekOrigin.Begin);
        httpContext.Request.Body = body;
        httpContext.Request.ContentType = contentType;
    }
  }
     await next(httpContext);     
});
//dapr run --app-id order-processor --components-path ../components/ --app-port 7001 -- dotnet run --project .
// Dapr will send serialized event object vs. being raw CloudEvent
// This will strip the cloudevent envelop and present the data to the controller. We need our own middleware to handle this and be able to get the token string
app.UseCloudEvents(); 

//(base) sarvananaidu@AeriesITs-MacBook-Pro checkout % curl --request POST 'http://localhost:53128/v1.0/publish/order_pub_sub/orders' --header 'Content-Type: application/json' --data-raw '{"datacontenttype":"application/cloudevents+json", "token":"0987654321234567890", "data":{ "orderId": "10"}}'


if (app.Environment.IsDevelopment()) {app.UseDeveloperExceptionPage();}

// Dapr subscription in [Topic] routes orders topic to this route 
//app.MapPost("/orders", [Topic("orderpubsub", "orders")] 
/* app.MapPost("/orders", [Topic("order_pub_sub", "orders")] (CustomCloudEvent<Order> cloudEvent) => {  
  Console.WriteLine("Subscriber toekn received : " + cloudEvent.TokenString);
  Console.WriteLine("Subscriber toekn received : " + cloudEvent.Data);
  Console.WriteLine("Subscriber toekn received : " + cloudEvent.DataContentType);
  return Results.Ok(((Order)cloudEvent.Data).OrderId);  
});*/

//app.MapPost("/orders", [Topic("order_pub_sub", "orders")] (Order order) => {  
app.MapPost("/orders", (Order order) => {    
  Console.WriteLine("Subscriber token received in controller : " + order);
  return Results.Ok(order.OrderId);  
});

await app.RunAsync();
public record Order([property: JsonPropertyName("orderId")] int OrderId);

public class CustomCloudEvent<TData> : CloudEvent<TData> {
		public CustomCloudEvent(TData data) : base(data)
		{			
		}

	  [JsonPropertyName("tokenstring")]
		public string? TokenString { get; set;}
}