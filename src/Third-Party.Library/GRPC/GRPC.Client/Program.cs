// See https://aka.ms/new-console-template for more information
// The port number must match the port of the gRPC server.
using AspNetCore.GRPC.Services;
using Grpc.Net.Client;

using var channel = GrpcChannel.ForAddress("https://localhost:7153");
var client = new Greeter.GreeterClient(channel);
var reply = await client.SayHelloAsync(
                  new HelloRequest { Name = "GreeterClient" });
Console.WriteLine("Greeting: " + reply.Message);
Console.WriteLine("Press any key to exit...");
Console.ReadKey();


