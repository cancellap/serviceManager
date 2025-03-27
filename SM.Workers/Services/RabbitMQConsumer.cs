using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using SM.Domaiin.Entities;


namespace SM.Workers.Services
{
    public class RabbitMQConsumer
    {
        private readonly string _hostname = "localhost";
        private readonly string _queueName = "servico_queue";

        public void Consuming()
        {
            var factory = new ConnectionFactory() { HostName = _hostname };
            using var connection = factory.CreateConnection();

            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var mensagemJson = Encoding.UTF8.GetString(body);
                    var mensagem = JsonConvert.DeserializeObject<ServicoMessageRabbitMQ>(mensagemJson);

                    //EnviarEmail(mensagem.EmailCliente, "Serviço " + mensagem.ServicoId + " criado", $"Olá, {mensagem.NomeCliente} seu serviço com ID {mensagem.ServicoId} foi criado: {mensagem.DescricaoServico}. \nAgradecemos pela confiança!");
                    //foreach (var emailTecnico in mensagem.EmailsTecnicos)
                    //{
                    //    EnviarEmail(emailTecnico, "Novo serviço atribuído: " + mensagem.ServicoId, $"Novo serviço atribuído a você, serviço com ID {mensagem.ServicoId}: {mensagem.DescricaoServico}. \nPor favor, entre em contato com o cliente.");
                    //}
                };
                channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
            }
        }
        static void EnviarEmail(string destinatario, string assunto, string corpo)
        {
            try
            {
                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("pedro.cancella@t2mlab.com");
                mailMessage.To.Add(destinatario);
                mailMessage.Subject = assunto;
                mailMessage.Body = corpo;
                mailMessage.IsBodyHtml = false;

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("pedro.cancella@t2mlab.com", "sua_senha"),
                    EnableSsl = true,
                };

                smtpClient.Send(mailMessage);
                Console.WriteLine($"Email enviado para {destinatario}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao enviar e-mail: " + ex.Message);
            }
        }
    }
}
