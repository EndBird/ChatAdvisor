using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using MovieMessenger.Models;

namespace MovieMessenger.Controllers
{
    public class ChatSessionsController : Controller
    {
        private readonly MovieMessengerContext _context;

        public ChatSessionsController(MovieMessengerContext context)
        {
            _context = context;
            
        }
        

        // GET: ChatSessions
        public async Task<IActionResult> Index()
        {
            return View(await _context.ChatSession.ToListAsync());
        }

        // GET: ChatSessions/Details/5
        /*
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chatSession = await _context.ChatSession
                .SingleOrDefaultAsync(m => m.ID == id);
            if (chatSession == null)
            {
                return NotFound();
            }

            return View(chatSession);
        }
        */
        // GET: ChatSessions/Create
        public IActionResult Create()
        {
            return View();//override this method to go to chat.cshtml with no chat history. 
        }

        // POST: ChatSessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("From,To,Chat")] ChatSession chatSession)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chatSession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chatSession);
        }

        // GET: ChatSessions/Edit/5
        /*
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chatSession = await _context.ChatSession.SingleOrDefaultAsync(m => m.ID == id);
            if (chatSession == null)
            {
                return NotFound();
            }
            return View(chatSession);
        }
        */
        // POST: ChatSessions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        /*
        public async Task<IActionResult> Edit(int id, [Bind("ID,From,To,Chat")] ChatSession chatSession)
        {
            if (id != chatSession.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chatSession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChatSessionExists(chatSession.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(chatSession);
        }
        */
        // GET: ChatSessions/Delete/5
        public async Task<IActionResult> Delete(string relation)
        {
            if (relation == null)
            {
                return NotFound();
            }

            var chatSession = await _context.ChatSession
                .SingleOrDefaultAsync(m => m.ToString() == relation);
            if (chatSession == null)
            {
                return NotFound();
            }

            return View(chatSession);
        }

        // POST: ChatSessions/Delete/5
        [HttpGet]
        
        public async Task<IActionResult> DeleteConfirmed(string relation)
        {
            var chatSession = await _context.ChatSession.SingleOrDefaultAsync(m => m.ToString() == relation);
            _context.ChatSession.Remove(chatSession);
            await _context.SaveChangesAsync();
            ViewData["user"] = relation.Split(" ")[0];
            
            return View("/Views/ChatSessions/ChatsList.cshtml",
                await _context.ChatSession.Where(x => (x.From == ViewData["user"].ToString() || x.To == ViewData["user"].ToString())).ToListAsync());
        }

        private bool ChatSessionExists(string relation)
        {
            return _context.ChatSession.Any(e => e.ToString() == relation);
        }

        [HttpPost]
        public async Task UpdateChat(string From, string To, string message)
        {
            try
            {
               ChatSession chat = await _context.ChatSession.SingleAsync(m => m.From == From && m.To == To);
               chat.Chat = chat.Chat + "<br>" + message;
                _context.ChatSession.Update(chat);
                await _context.SaveChangesAsync();
                
                
            }
            catch
            {
                return; //add something later here. 
            }
        }

        [HttpGet]
        public async Task<IActionResult> NewChatOptions(string user)
        {
            ViewData["user"] = user;
            return View("/Views/ChatSessions/NewChatOptions.cshtml", await _context.Account.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> OpenChat(string To, string From, string message)
        {
            if (To == null)
            {
                
                IEnumerable<ChatSession> chat = new ChatSession[] {};
                return View("/Views/ChatSessions/Chat.cshtml", chat);
            }   
            else
            {
                ViewData["To"] = To;
                ViewData["From"] = From;
                DbSet<ChatSession> chats = _context.ChatSession;
                foreach (ChatSession chat in chats) //currently chat is empty because its not saving. use updatechat.
                {
                    if (chat.To == To && chat.From==From)
                    {
                        IEnumerable<ChatSession> chatTo = new ChatSession[] { chat };
                        return View("/Views/ChatSessions/Chat.cshtml", chatTo);
                    }
                }
                IEnumerable<ChatSession> newChat = new ChatSession[] {new ChatSession(From, To, "") }; //Assume its alex. what to put for new chat?
                await Create(newChat.First());
                
                return View("/Views/ChatSessions/Chat.cshtml", newChat);
            }
        }
 
    }   
}
