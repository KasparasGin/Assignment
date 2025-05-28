
using System.Text.Json.Serialization;
using Assingment.Interfaces;
using Assingment.PaymentGateways;
using Assingment.Services;

namespace Assingment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            builder.Services.AddScoped<IPaymentGatewayFactory, PaymentGatewayFactory>();
            builder.Services.AddScoped<IBillingService, BillingService>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
