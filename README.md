# Workshop

## Service to Service:

dapr run --app-port 5023 --app-id weather2 --app-protocol http --dapr-http-port 3501 -- dotnet run

dapr run --app-port 5051 --app-id weather --app-protocol http --dapr-http-port 3500 -- dotnet run

curl http://localhost:3500/v1.0/invoke/weather2/method/WeatherForecast

curl http://localhost:3500/v1.0/invoke/weather/method/WeatherForecast

### K8S Deploy

kubectl port-forward pod/apidotnet2-587986775d-qtkqt  8080:80

curl http://localhost:8080/WeatherForecast

## State Management

dapr run --app-id order-processor --components-path ../../../components/ -- dotnet run

## Middleware

helm repo add ingress-nginx https://kubernetes.github.io/ingress-nginx

helm install my-release ingress-nginx/ingress-nginx   

c:\windows\system32\drivers\etc\hosts

http://dummy.com/v1.0/invoke/echoapp/method/echo?text=hello


## PubSub

### Start Order-Processor 
dapr run --app-id order-processor --components-path ../components/ --app-port 7001 -- dotnet run --project .

### Run Checkout

dapr run --app-id checkout --components-path ../components/ -- dotnet run --project .

### Output

### Token

== APP == TokenString in middleware = 65044efa-fa39-47ac-a149-8b2e727bd629

### Order

== APP == Subscriber token received in controller : Order { OrderId = 56612585 }

### Publish using Dapr
dapr publish --publish-app-id order-processor --pubsub order_pub_sub --topic orders --data '{"orderId": "100"}'