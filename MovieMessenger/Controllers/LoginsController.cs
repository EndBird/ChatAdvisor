using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieMessenger.Models;
using System.Web.Mvc;
using Microsoft.AspNetCore.Session;
namespace MovieMessenger.Controllers
{
    public class LoginsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly MovieMessengerContext _context;
        public LoginsController(MovieMessengerContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View("/Views/Logins/Login.cshtml");
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("ChatSessions")]
        public async Task<IActionResult> Login(string username, string pass)
        {
            DbSet<Account> accounts = _context.Account;
            foreach (var account in accounts)
            {
                if (account.Username == username & account.Password == pass)
                {

                    ViewData["user"] = username;
                    
                    return View("/Views/ChatSessions/ChatsList.cshtml", 
                        await _context.ChatSession.Where(x => (x.From == username || x.To == username)).ToListAsync());

                }
            }
            ViewData["account status"] = "account does not exist";
            return View();


        }
    }
}