using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieMessenger.Models;

namespace MovieMessenger.Controllers
{
    public class LoginsController : Controller
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

        [HttpPost]
        public async Task<IActionResult> Login(string username, string pass)
        {
            DbSet<Account> x = _context.Account;
            foreach (var account in x)
            {
                if (account.Username == username & account.Password == pass)
                {
                    ViewData["user"] = username;
                    return View("/Views/ChatSessions/ChatsList.cshtml", await _context.ChatSession.ToListAsync());
                }
            }
            ViewData["account status"] = "account does not exist";
            return View();


        }
    }
}