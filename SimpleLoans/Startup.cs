using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Grpc.Auth;
using Grpc.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SimpleLoans.DataAccess.BogusRepository;

namespace SimpleLoans
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var projectRootFolder = Path.Combine(env.ContentRootPath, env.ApplicationName);
             var builder = new ConfigurationBuilder()
                    .SetBasePath(projectRootFolder)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                    .AddEnvironmentVariables();

            Configuration = builder.Build();       
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.Configure<ConfigSettings>(Configuration.GetSection("Settings"));

            var provider = services.BuildServiceProvider();
            var options =  provider.GetService<IOptions<ConfigSettings>>();

            GoogleCredential cred = GoogleCredential.FromFile(options.Value.CredentialsFilePath);
            Channel channel = new Channel(FirestoreClient.DefaultEndpoint.Host,
                  FirestoreClient.DefaultEndpoint.Port,
                  cred.ToChannelCredentials());
            FirestoreClient client = FirestoreClient.Create(channel);
            FirestoreDb d = FirestoreDb.Create("myfriendlyloaner", client);
            services.AddSingleton<FirestoreDb>(d);
            services.AddTransient<IBorrowerRepository, BorrowerRepository>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

           

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
