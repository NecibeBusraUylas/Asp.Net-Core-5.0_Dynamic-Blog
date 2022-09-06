using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminMessageController : Controller
    {

        private readonly IMessage2Service _message2Service;
        private readonly UserManager<AppUser> _userManager;

        public AdminMessageController(IMessage2Service message2Service, UserManager<AppUser> userManager)
        {
            _message2Service = message2Service;
            _userManager = userManager;
        }

        public async Task<IActionResult> Inbox()
        {
            var values = _message2Service.TGetReceivingMessageListByWriter(await GetByUserId());
            if (values.Count() > 3)
            {
                values = values.TakeLast(3).ToList();
            }
            return View(values);
        }

        public async Task<IActionResult> Sendbox()
        {
            var values = _message2Service.TGetSendingMessageListByWriter(await GetByUserId());
            if (values.Count() > 3)
            {
                values = values.TakeLast(3).ToList();
            }
            return View(values);
        }

        public async Task<int> GetByUserId()
        {
            var userName = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            return user.Id;
        }

        [HttpGet]
        public IActionResult ComposeMessageAsync()
        {
            return View(Tuple.Create<Message2, AppUser>(new Message2(), new AppUser()));
        }

        [HttpPost]
        public async Task<IActionResult> ComposeMessageAsync([Bind(Prefix = "message")] Message2 message, [Bind(Prefix = "appuser")] AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                var sender = await _userManager.FindByNameAsync(User.Identity.Name);
                var receiver = await _userManager.FindByEmailAsync(appUser.Email);
                message.SenderId = sender.Id;
                message.ReceiverId = receiver.Id;
                message.MessageStatus = true;
                message.MessageDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()); ;
                _message2Service.TAdd(message);
                return RedirectToAction("Sendbox", "AdminMessage");
            }
            return View();
        }
    }
}