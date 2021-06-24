using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class MessageController : Controller
    {
        MessageManager messagemanager = new MessageManager(new EfMessageDal());
        MessageValidator messagevalidator = new MessageValidator();
        DraftController draftController = new DraftController();

        // GET: Message
        public ActionResult Inbox()
        {
            string p = (string)Session["WriterMail"];
            var messagelist = messagemanager.GetListInbox(p);
            return View(messagelist);
        }
        public ActionResult SendBox()
        {
            string p = (string)Session["WriterMail"];
            var messagelist = messagemanager.GetListSendBox(p);
            return View(messagelist);
        }
        public ActionResult GetMessageDetails(int id)
        {
            var messagevalues = messagemanager.GetByID(id);
            return View(messagevalues);
        }
        public ActionResult GetInBoxMessageDetails(int id)
        {
            var inboxvalues = messagemanager.GetByID(id);
            return View(inboxvalues);
        }

        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult NewMessage(Message p, string button)
        {
            ValidationResult results = messagevalidator.Validate(p);
            if (button == "draft")
            {
                results = messagevalidator.Validate(p);
                if (results.IsValid)
                {
                    Draft draft = new Draft();
                    draft.ReceiverMail = p.ReceiverMail;
                    draft.Subject = p.Subject;
                    draft.DraftContent = p.MessageContent;
                    draft.DraftDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                    draftController.AddDraft(draft);
                    return RedirectToAction("Draft", "Draft");
                }
                else
                {
                    foreach (var item in results.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
            }
            else if (button == "save")
            {
                string sender = (string)Session["WriterMail"];
                results = messagevalidator.Validate(p);
                if (results.IsValid)
                {
                    p.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                    p.SenderMail = sender;
                    messagemanager.MessageAdd(p);
                    return RedirectToAction("SendBox");
                }
                else
                {
                    foreach (var item in results.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
            }
            return View();
        }
        public ActionResult IsRead(int id)
        {
            var result = messagemanager.GetByID(id);
            if (result.IsRead == false)
            {
                result.IsRead = true;
            }
            messagemanager.MessageUpdate(result);
            return RedirectToAction("ReadMessage");
        }

        public ActionResult ReadMessage()
        {
            var readMessage = messagemanager.GetList().Where(x => x.IsRead == true).ToList();
            return View(readMessage);
        }

        public ActionResult UnReadMessage()
        {
            var unReadMessage = messagemanager.GetListUnRead();
            return View(unReadMessage);
        }
    }
}