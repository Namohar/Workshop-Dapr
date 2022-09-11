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
