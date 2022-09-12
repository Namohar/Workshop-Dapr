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

## Bindings

### Introduction 

This POC demonstrates the working model for Azure Function with Dapr Extension deployed on to AKS. The function gets triggered when the messages get posted on the Service Bus Queue. The Dapr Component binds to the service bus and triggers the Azure function to consume the message. 

### Getting Started

    * Create a Service Bus and Message Queue on the Azure Portal.
    * Update the components/servicebus_bindings.yaml (local testing) with the connection string.
    * Update the deployment.yaml file for AKS deployment with the connection string.
    * For local testing, run the below command:

### Build and Test

dotnet build -o bin/ extensions.csproj

### Dapr CLI command
    * dapr run --app-id pythonapp --app-port 3001 --dapr-http-port 3500  --components-path ./components/ -- func host start --no-build

### K8S deployment

    * kubectl apply -f deployment.yaml

### Docker Image Build
    * az acr login -n alegeus
    * docker build . -t alegeus.azurecr.io/pythonapp:1.2
    * docker push alegeus.azurecr.io/pythonapp:1.2    

### Cosmos Emulator
    * For testing purpose we are using cosmos emulator. Please refer [emulator setup](https://docs.microsoft.com/en-us/azure/cosmos-db/linux-emulator?tabs=ssl-netstd21)

### Testing

    * Produce a message on the service bus using the explorer and verify the logs for function execution.

### NVM 
    export NVM_DIR=~/.nvm
    source $(brew --prefix nvm)/nvm.sh

## K8S

    docker build . -t sshrav/azfunc:1.0
    docker push sshrav/azfunc:1.0
    kubectl apply -f deployment.yaml

