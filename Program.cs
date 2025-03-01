using System;
using RabbitMQ.Client;

using RabbitMQ.Client.Exceptions;

using System.Text;
using RabbitMQDemo;


class Program
{
    static void Main()
    {

        var factory = new ConnectionFactory()
        {
            HostName = "localhost",  
            VirtualHost = "/",     
            UserName = "guest",
            Password = "guest"
        };

       
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        
        channel.QueueDeclare(queue: "testQueue",
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        // 🔹 4. Publicar un mensaje de prueba
        string message = "¡Hola, RabbitMQ!";
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "",
                             routingKey: "testQueue",
                             basicProperties: null,
                             body: body);

        Console.WriteLine($"[✔] Enviado: {message}");
    }
}