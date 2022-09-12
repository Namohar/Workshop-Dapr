# Start Order-Processor 
dapr run --app-id order-processor --components-path ../components/ --app-port 7001 -- dotnet run --project .

# Run Checkout

dapr run --app-id checkout --components-path ../components/ -- dotnet run --project .

# Output

## Token

== APP == TokenString in middleware = 65044efa-fa39-47ac-a149-8b2e727bd629

## Order

== APP == Subscriber token received in controller : Order { OrderId = 56612585 }

## Publish using Dapr
dapr publish --publish-app-id order-processor --pubsub order_pub_sub --topic orders --data '{"orderId": "100"}'