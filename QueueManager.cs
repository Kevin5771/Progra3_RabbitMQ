using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQDemo
{
    public class QueueManager
    {
        private readonly IQueueService _queueService;

        public QueueManager(IQueueService queueService)
        {
            _queueService = queueService;
        }


        public void SendMessage(string message)
        {
            _queueService.Enqueue(message);
        }


        public void ReceiveMessage()
        {
            string message = _queueService.Dequeue();
            if (message == null)
            {
                Console.WriteLine("No messages in queue.");
            }
        }
    }
}
