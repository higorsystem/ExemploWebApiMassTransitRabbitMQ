// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SaleController.cs" company="Exemplo de utilização do RabbitMQ">
//   Projeto para ilustrar o comportamento de um projeto WebAPI com MassTransit/RabbitMQ
// </copyright>
// <summary>
//   Controlador dos recursos de venda
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ExemploWebApiMassTransitRabbitMQ.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;

    using MassTransit;

    using Models;

    /// <summary>
    /// Controlador dos recursos de venda
    /// </summary>
    [RoutePrefix("api")]
    public class SaleController : ApiController
    {
        /// <summary>
        /// Cria uma venda
        /// </summary>
        /// <param name="createTransactionRequest">Dados da requisição</param>
        /// <returns>Retorna uma venda</returns>
        [HttpPost]
        [Route("sale/")]
        public IHttpActionResult CreateSale(CreateTransactionRequest createTransactionRequest)
        {
            try
            {
                // Verifica se há erros
                if (!this.ModelState.IsValid)
                {
                    var message = string.Join(
                        " | ", this.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                    
                    // Retorna a lista de erros
                    return this.BadRequest(message);
                }

                // Conecta no barramento de servios
                var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
                    {
                        sbc.Host(
                            new Uri("rabbitmq://localhost/"),
                            h =>
                                {
                                    h.Username("guest");
                                    h.Password("guest");
                                });
                });

                // Inicia o barramento
                bus.Start();
                 var task = bus.Publish(createTransactionRequest);
                 task.Wait();
                bus.Stop();
                

                // Cria a resposta
                var createSaleResponse = new CreateTransactionResponse()
                {
                    TransactionKey = createTransactionRequest.TransactionKey
                };
                return this.Ok(createSaleResponse);
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }
        }
    }
}