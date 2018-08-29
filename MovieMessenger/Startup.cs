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
using MovieMessenger.Controllers;

namespace MovieMessenger
{
    public class Startup
    {
        public static IApplicationBuilder staticApp = null;
        public static Dictionary<string, WebSocket> chatSockets = new Dictionary<string, WebSocket>() {};
        public static MovieMessengerContext _context;
        public static ChatSessionsController chatSessionsController;
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
            services.AddDbContext<MovieMessengerContext>(opt => opt.UseInMemoryDatabase("MovieMessengerContext-4cac2f23-28c6-4ac8-b3a0-76736cf137a5"));
            services.AddMvc().AddControllersAsServices();
            //not working... 
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
                    await Echo(app, context, webSocket);
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

        private async Task Echo(IApplicationBuilder app, HttpContext context, WebSocket webSocket)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                chatSessionsController = scope.ServiceProvider.GetRequiredService<ChatSessionsController>();
                _context = scope.ServiceProvider.GetRequiredService<MovieMessengerContext>();
                // rest of your code
                var buffer = new byte[1024 * 4];
                WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                string[] names = context.Request.Path.ToString().Split('/').Last().Split(new char[] { '%', '7', 'C' });
                while (!result.CloseStatus.HasValue)
                {

                    try
                    {
                        string chatTo = names.Last() + "%7C" + names.First();
                        await chatSockets[chatTo].SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);
                    }
                    catch
                    
                    {  
                        
                        //bool g = _context.ChatSession.Any(e => e.ToString() == relation); ;
                        //do work here to automatically save the chat to the receviers account. 
                        if (chatSessionsController.ChatSessionExists(names.Last() + " " + names.First()))
                        {
                            await chatSessionsController.UpdateChat(names.Last(), names.First(), names.First() + ": "+ System.Text.Encoding.Default.GetString(buffer));
                            /*
                            ChatSession chat = _context.ChatSession.Single(e => e.ToString() == names.Last() + " " + names.First());
                            chat.Chat = chat.Chat + "<br>" + names.First() + ": " + System.Text.Encoding.Default.GetString(buffer);
                            _context.ChatSession.Update(chat);
                            await _context.SaveChangesAsync();
                            */

                        }
                        else
                        {
                            ChatSession chat = new ChatSession(names.Last(), names.First(), names.First() + ": " + System.Text.Encoding.Default.GetString(buffer));
                            await chatSessionsController.Create(chat);
                        }
                    }

                    result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                }
                await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
            }
            
            
        }

    }
}
