﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory
{
    HostName = "localhost",
    UserName = "rabbit.tiagope",
    Password = "Ponta1375@*15"
};

var connection = factory.CreateConnection();

using var channel = connection.CreateModel();

channel.QueueDeclare("product", exclusive: false);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, eventArgs) =>
{
    try
    {
        var body = eventArgs.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine($"Product message received: {message}");

        channel.BasicAck(eventArgs.DeliveryTag, false);
    }
    catch
    {
        channel.BasicNack(eventArgs.DeliveryTag, false, false);
    }
};

channel.BasicConsume(queue: "product", autoAck: true, consumer: consumer);

Console.ReadKey();