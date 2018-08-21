using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MovieMessenger.Models;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Http;
using System.Threading;

namespace MovieMessenger
{
    public class Startup
    {
        public static IApplicationBuilder staticApp = null;
        public static Dictionary<string, WebSocket> chatSockets = new Dictionary<string, WebSocket>() {};
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<MovieMessengerContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("MovieMessengerContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            
            app.UseWebSockets();
            app.Use(async (context, next) =>
            {
                if (context.Request.Path.ToString().Contains("/ws"))
                {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                        
                        chatSockets.Add(context.Request.Path.ToString().Split('/').Last(), webSocket);
                    await Echo(context, webSocket);
                }
                else
                {
                    context.Response.StatusCode = 400;
                }
                }
                else
                {
                    await next();
                }

            });

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                
            });
        }

        private async Task Echo(HttpContext context, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            string[] names = context.Request.Path.ToString().Split('/').Last().Split(new char[] {'%','7','C' });
            while (!result.CloseStatus.HasValue)
            {
                
                try
                {
                    string chatTo = names.Last() + "%7C"+ names.First();
                    await chatSockets[chatTo].SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);
                }
                catch
                {

                }
            
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }

    }
}
