using System;
using Dapr.Client;
using Dapr;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
//dapr run --app-id checkout --components-path ../../../components/ -- dotnet run --project .
Random random = new Random();
for (int i = 1; i <= 1; i++) {
    var order = new Order(random.Next());
    using var client = new DaprClientBuilder().Build();
    
    var customCloudEvent = new CustomCloudEvent<Order>(order)
    {                
        TokenString = Guid.NewGuid().ToString()
    };

    await client.PublishEventAsync("order_pub_sub", "orders", customCloudEvent);                

    Console.WriteLine("Published data: " + customCloudEvent.Data); 
    await Task.Delay(TimeSpan.FromSeconds(1));
}

 public record Order([property: JsonPropertyName("orderId")] int OrderId);

public class CustomCloudEvent<TData> : CloudEvent<TData> {

    public CustomCloudEvent(TData data) : base(data)
    {			
    }

    [JsonPropertyName("tokenstring")]        
    public string TokenString { get; set;}

//    public T GenericObject(get; set;)
}