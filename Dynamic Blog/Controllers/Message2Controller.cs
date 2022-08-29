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
        Message2Manager message2Manager = new Message2Manager(new EFMessage2Repository());
        WriterManager writerManager = new WriterManager(new EFWriterRepository());

        public IActionResult Inbox()
        {
            var values = message2Manager.TGetReceivingMessageListByWriter(GetByWriterId());
            return View(values);
        }

        public IActionResult MessageDetails(int id)
        {
            var value = message2Manager.TGetReceivingMessageListByWriter(GetByWriterId())
                .Where(x => x.MessageId == id).FirstOrDefault();
            return View(value);
        }

        public int GetByWriterId()
        {
            return writerManager.TGetByFilter(x => x.WriterId == int.Parse(User.Identity.Name)).WriterId;
        }
    }
}