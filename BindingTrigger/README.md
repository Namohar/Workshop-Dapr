# Introduction 

This POC demonstrates the working model for Azure Function with Dapr Extension deployed on to AKS. The function gets triggered when the messages get posted on the Service Bus Queue. The Dapr Component binds to the service bus and triggers the Azure function to consume the message. 

# Getting Started

    * Create a Service Bus and Message Queue on the Azure Portal.
    * Update the components/servicebus_bindings.yaml (local testing) with the connection string.
    * Update the deployment.yaml file for AKS deployment with the connection string.
    * For local testing, run the below command:

# Build and Test

dotnet build -o bin/ extensions.csproj

## Dapr CLI command
    * dapr run --app-id pythonapp --app-port 3001 --dapr-http-port 3500  --components-path ./components/ -- func host start --no-build

## K8S deployment

    * kubectl apply -f deployment.yaml

## Docker Image Build
    * az acr login -n alegeus
    * docker build . -t alegeus.azurecr.io/pythonapp:1.2
    * docker push alegeus.azurecr.io/pythonapp:1.2    

## Cosmos Emulator
    * For testing purpose we are using cosmos emulator. Please refer [emulator setup](https://docs.microsoft.com/en-us/azure/cosmos-db/linux-emulator?tabs=ssl-netstd21)

## Testing

    * Produce a message on the service bus using the explorer and verify the logs for function execution.

## NVM 
    export NVM_DIR=~/.nvm
    source $(brew --prefix nvm)/nvm.sh