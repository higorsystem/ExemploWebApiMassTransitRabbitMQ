using System;
using System.ComponentModel.DataAnnotations;

namespace ExemploWebApiMassTransitRabbitMQ.Models
{
    // Requisição de criação de transação de cartão de crédito
    public class CreateTransactionRequest
    {

        [Required]
        public string CreditCardNumber { get; set; } // Número do cartão

        [Required]
        public string SecurityCode { get; set; } // Código de segurança

        [Required]
        public string ExpMonth { get; set; } // Mês de expiração

        [Required]
        public string ExpYear { get; set; } // Ano de expiração

        [Required]
        public string HolderName { get; set; } // Nome do comprador

        [Required]
        public long AmountInCents { get; set; } // Valor em centavos

        public Guid TransactionKey { get; set; } // Chave da transação

        public Guid RequestKey { get; set; } // Chave da requisição

        public CreateTransactionRequest()
        {
            this.TransactionKey = Guid.NewGuid();//Cria a chave da transação
            this.RequestKey = Guid.NewGuid(); //Cria a chave de requisição
        }
    }
}