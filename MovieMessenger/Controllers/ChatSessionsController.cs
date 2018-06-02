using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Create([Bind("ID,From,To,Chat")] ChatSession chatSession)
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

        // POST: ChatSessions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: ChatSessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: ChatSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chatSession = await _context.ChatSession.SingleOrDefaultAsync(m => m.ID == id);
            _context.ChatSession.Remove(chatSession);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChatSessionExists(int id)
        {
            return _context.ChatSession.Any(e => e.ID == id);
        }

        [HttpGet]
        public IActionResult OpenChat(string From)
        {
            if (From == null)
            {
                IEnumerable<ChatSession> chatFrom = new ChatSession[] {};
                return View("/Views/ChatSessions/Chat.cshtml", chatFrom);
            }   
            else
            {
                DbSet<ChatSession> chats = _context.ChatSession;
                foreach (ChatSession chat in chats)
                {
                    if (chat.From == From)
                    {
                        IEnumerable<ChatSession> chatFrom = new ChatSession[] { chat };
                        return View("/Views/ChatSessions/Chat.cshtml", chatFrom);
                    }
                }
                return View("/Views/ChatSessions/Chat.cshtml");
            }
        }
 
    }   
}
