﻿using System;
using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;
// ReSharper disable PossibleNullReferenceException

namespace Facility
{
    class Program
    {
        static void Main(string[] args)
        {
            RunBus().GetAwaiter().GetResult();
        }

        static async Task RunBus()
        {
            IEndpointInstance endpoint = null;
            try
            {
                DefaultFactory defaultFactory = LogManager.Use<DefaultFactory>();
                defaultFactory.Level(LogLevel.Error);

                var configuration = new EndpointConfiguration();
                configuration.EndpointName("Chocolate.Facility");

                configuration.UseTransport<MsmqTransport>();
                configuration.UsePersistence<InMemoryPersistence>();

                endpoint = await Endpoint.Start(configuration);

                Console.ReadLine();
            }
            finally
            {
                await endpoint.Stop();
            }
        }
    }
}
