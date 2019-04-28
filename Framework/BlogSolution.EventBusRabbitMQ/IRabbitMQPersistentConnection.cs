using RabbitMQ.Client;
using System;

namespace BlogSolution.EventBusRabbitMQ
{
    public interface IRabbitMQPersistentConnection
          : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}
