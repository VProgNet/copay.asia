using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using WebApplication1.Currencies;
using WebApplication1.Currencies.Ethereum;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddHttpClient<IEthereumAPIClient, EthereumAPIClient>();
            services.AddHttpClient("ganacheClient", client =>
            {
                client.BaseAddress = new Uri("http://localhost:8545");
                client.Timeout = TimeSpan.FromSeconds(20);                
            });
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<IEthereumAPIClient, EthereumAPIClient>();
            
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Ganache API client",
                    Description = "A simple Ganache API client. \n " +
                                  "You need download and install ganache-cli\n" +
                                  "and launch it on localhost:8545 \n" +
                                  "On Ubuntu: \n" +
                                  "sudo apt-get install nodejs \n" +
                                  "sudo npm install -g ganache-cli \n" +
                                  "/usr/local/bin/ganache-cli \n" +
                                  "This will create private blockchain with 1 wallet, \n" +
                                  "that contains 10 addreses with 100eth on each address.\n" +
                                  "\n" +
                                  "Now you can work with this client\n" +
                                  "",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Krilovskiy Vladislav",
                        Email = "vlad.cv.mail@gmail.com",
                        Url = "http://t.me/@little0big"
                    },
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            
            app.UseSwagger();
            app.UseSwaggerUI(options => 
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Ganache API client v1");
                options.RoutePrefix = string.Empty;
            });
            
            
            
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}