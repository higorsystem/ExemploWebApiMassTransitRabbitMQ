namespace ExemploWebApiMassTransitRabbitMQ.Models
{
    using System;

    public class CreateTransactionResponse
    {
        public Guid TransactionKey { get; set; }
        public dynamic Errors { get; set; }
    }
}