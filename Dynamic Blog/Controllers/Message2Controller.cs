using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicBlog.Controllers
{
    public class Message2Controller : Controller
    {
        private readonly IMessage2Service _message2Service;
        private readonly UserManager<AppUser> _userManager;

        public Message2Controller(IMessage2Service message2Service, UserManager<AppUser> userManager)
        {
            _message2Service = message2Service;
            _userManager = userManager;
        }

        public async Task<IActionResult> Inbox()
        {
            var userName = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var values = _message2Service.TGetReceivingMessageListByWriter(user.Id);
            if (values.Count() > 3)
            {
                values = values.TakeLast(3).ToList();
            }
            return View(values);
        }

         public async Task<IActionResult> Sendbox()
        {
            var userName = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var values = _message2Service.TGetSendingMessageListByWriter(user.Id);
            if (values.Count() > 3)
            {
                values = values.TakeLast(3).ToList();
            }
            return View(values);
        }
        public async Task<IActionResult> MessageDetails(int id)
        {

            var userName = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var value = _message2Service.TGetReceivingMessageListByWriter(user.Id)
                .Where(x => x.MessageId == id).FirstOrDefault();
            return View(value);
        }

        [HttpGet]
        public IActionResult SendMessage()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> SendMessage(Message2 message, string receiverMail)
        {
            if (ModelState.IsValid)
            {
                var userName = User.Identity.Name;
                var user = await _userManager.FindByNameAsync(userName);
                message.SenderId = user.Id;
                var receiver = await _userManager.FindByEmailAsync(receiverMail);
                if (receiver.Id != 0)
                {
                    message.ReceiverId = receiver.Id;
                }
                else
                {
                    return View();
                }
                message.MessageStatus = true;
                message.MessageDate = DateTime.Now;
                _message2Service.TAdd(message);
                return RedirectToAction("Inbox", "Message2");
            }
            return View();
        }

        public async Task<IActionResult> MessageDetailsSendbox(int id)
        {

            var userName = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var value = _message2Service.TGetSendingMessageListByWriter(user.Id)
                .Where(x => x.MessageId == id).FirstOrDefault();
            return View(value);
        }
    }
}