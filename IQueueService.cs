using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RabbitMQ.Client;
using System.Threading.Tasks;

namespace RabbitMQDemo
{
    public interface IQueueService : IDisposable
    {
        void Enqueue(string message);
        string Dequeue();
    }
}
