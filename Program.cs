using System;
using RabbitMQ.Client;
using System.Text;
using RabbitMQDemo;


class Program
{
    static void Main()
    {
        using (IQueueService queueService = new RabbitMQService())
        {
            QueueManager queueManager = new QueueManager(queueService);


            queueManager.SendMessage("Hello, RabbitMQ!");


            queueManager.ReceiveMessage();
        }
    }
}