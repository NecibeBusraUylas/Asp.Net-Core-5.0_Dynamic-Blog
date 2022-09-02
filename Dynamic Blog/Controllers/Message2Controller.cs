using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IWriterService _writerService;

        public Message2Controller(IMessage2Service message2Service, IWriterService writerService)
        {
            _message2Service = message2Service;
            _writerService = writerService;
        }

        public IActionResult Inbox()
        {
            var values = _message2Service.TGetReceivingMessageListByWriter(GetByWriterId());
            return View(values);
        }

        public IActionResult MessageDetails(int id)
        {
            var value = _message2Service.TGetReceivingMessageListByWriter(GetByWriterId())
                .Where(x => x.MessageId == id).FirstOrDefault();
            return View(value);
        }

        public int GetByWriterId()
        {
            return _writerService.TGetByFilter(x => x.WriterId == int.Parse(User.Identity.Name)).WriterId;
        }
    }
}