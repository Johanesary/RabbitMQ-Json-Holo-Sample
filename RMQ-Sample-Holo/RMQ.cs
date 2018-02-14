using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMQ_Sample_Holo
{
    class RMQ
    {
        //default settings
        private const string hostname_local = "169.254.80.80";
        private const int port = 5672;
        private const string username = "username";
        private const string password = "pass";
        private const string virtualhost = "/virtualhost";

        private string data = "";

        public ConnectionFactory connectionFactory;
        public IConnection connection;
        public IModel channel;

        public RMQ()
        {
            InitRMQConnection();
            CreateRMQConnection();
        }

        private void InitRMQConnection(string host = hostname_local, int port = port, string user = username, string pass = password, string vhost = virtualhost)
        {
            connectionFactory = new ConnectionFactory();
            connectionFactory.HostName = host;
            connectionFactory.Port = port;
            connectionFactory.UserName = user;
            connectionFactory.Password = pass;
            connectionFactory.VirtualHost = vhost;
        }

        private void CreateRMQConnection()
        {
            connection = connectionFactory.CreateConnection();
            Debug.WriteLine("Koneksi " + (connection.IsOpen ? "Berhasil!" : "Gagal!"));
        }

        public void createChannel(string queue, string exchange, string routing_key, bool durable = true, string type = "topic")
        {
            if (connection.IsOpen)
            {
                channel = connection.CreateModel();
                Debug.WriteLine("Channel : " + channel.IsOpen);
            }

            if (channel.IsOpen)
            {
                Debug.WriteLine("Declare Exchange");
                channel.ExchangeDeclare(exchange: exchange,
                                        type: type,
                                        durable: true);

                Debug.WriteLine("Declare Queue");
                channel.QueueDeclare(queue: queue,
                                     durable: durable,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                Debug.WriteLine("Bind Queue");
                channel.QueueBind(queue: queue,
                                  exchange: exchange,
                                  routingKey: routing_key);

                Debug.WriteLine("Declare Consuming Queue Process");
                var consumer = new EventingBasicConsumer(channel);

                Debug.WriteLine("Begin Reveiving data");

                consumer.Received += (model, ea) =>
                {
                    Debug.WriteLine("Retriving......");
                    var body = ea.Body;
                    data = Encoding.UTF8.GetString(body);
                    Debug.WriteLine("Data : " + data);
                };

                channel.BasicConsume(queue: queue,
                                    noAck: true,
                                    consumer: consumer);

            }
        }

        public void deleteQueue(string queue_name)
        {
            using (channel = connection.CreateModel())
            {
                if (channel.IsOpen)
                {
                    channel.QueueDelete(queue_name);
                }
            }
        }

        public void deleteExchange(string exchange_name)
        {
            using (channel = connection.CreateModel())
            {
                if (channel.IsOpen)
                {
                    channel.ExchangeDelete(exchange_name);
                }
            }
        }

        public string getData()
        {
            return data;
        }

        ~RMQ()
        {
            Disconnect();
        }

        public void Disconnect()
        {
            channel.Close();
            channel = null;
            Debug.WriteLine("Channel ditutup!");
            if (connection.IsOpen)
            {
                connection.Close();
            }
            Debug.WriteLine("Koneksi diputus!");
            connection.Dispose();
            connection = null;
        }
    }
}
