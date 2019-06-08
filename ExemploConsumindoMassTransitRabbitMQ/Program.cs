using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExemploConsumindoMassTransitRabbitMQ
{
    using ExemploWebApiMassTransitRabbitMQ.Models;

    using MassTransit;

    using Newtonsoft.Json;

    class Program
    {
        static void Main(string[] args)
        {
            //Acessa o barramento e efetua a conexão
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc => {
                var host = sbc.Host(new Uri("rabbitmq://localhost/"), h => {
                    h.Username("guest");
                    h.Password("guest");
                });

                //Conecta no barramento e aguarda o evento de recpção
                sbc.ReceiveEndpoint(host, "InputCreateTransactionRequestQueue", endpoint => {
                    endpoint.Handler<CreateTransactionRequest>(async context => {
                        var data = JsonConvert.SerializeObject(context.Message);
                        await Console.Out.WriteLineAsync($"Received: {data}");
                    });
                });
            });
            bus.Start(); //Incia o barramento
            Console.ReadKey(); //Aguarda uma tecla ser pressionada
            bus.Stop(); //Para o barramento
        }
    }
}
