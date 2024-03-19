
using DemoApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DemoApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(
                options => options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
  $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"))
            );
            builder.Services.AddSqlServer<DemoContext>(builder.Configuration.GetConnectionString("MyConn"));
            //builder.Services.AddDbContext<DemoContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyConn")));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
