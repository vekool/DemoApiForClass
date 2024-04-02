
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

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Test", builder =>
                {
                    //builder.AllowAnyMethod(); //All- GET, POST PUT, DELETE ALL ARE ALLOWED. 
                    builder.AllowAnyOrigin(); //All origins are allowed
                    builder.WithMethods("GET", "POST");

                    //builder.AllowAnyHeader(); //All headers are allowed
                });
                options.AddPolicy("Secure", builder =>
                {
                    builder.AllowAnyOrigin(); //All origins are allowed
                    builder.WithMethods("GET", "POST");
                    //builder.WithOrigins("https://www.mywebsite.com", "https://someTestingserver.com")
                    //.WithHeaders("Content-Type", "authorization", "Accept")
                    //.WithMethods("GET", "POST");
                });
            });
            builder.Services.AddMemoryCache(); //enable in memory caching
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseCors("Test"); //loose security
            }
            else
            {
                app.UseCors("Secure");
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
