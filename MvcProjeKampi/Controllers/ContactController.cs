﻿using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        Context _context = new Context();
        ContactManager cm = new ContactManager(new EfContactDal());
        ContactValidator cv = new ContactValidator();
        MessageManager mm = new MessageManager(new EfMessageDal());
        DraftManager draftManager = new DraftManager(new EfDraftDal());

      
        public ActionResult Index()
        {
            var contactvalues = cm.GetList();
            return View(contactvalues);
        }
        public ActionResult GetContactDetails(int id)
        {
            var contactvalues = cm.GetByID(id);
            return View(contactvalues);
        }
        public PartialViewResult ContactSideBarPartial()
        {
            var receiverMail = _context.Messages.Count(m => m.ReceiverMail == "gizem@hotmail.com").ToString();
            ViewBag.receiverMail = receiverMail;

            var senderMail = _context.Messages.Count(m => m.SenderMail == "gizem@hotmail.com").ToString();
            ViewBag.senderMail = senderMail;

            var contact = _context.Contacts.Count().ToString();
            ViewBag.contact = contact;

            var draft = _context.Drafts.Count().ToString();
            ViewBag.draft = draft;

            var readMessage = mm.GetList().Count();
            ViewBag.readMessage = readMessage;

            var unReadMessage = mm.GetListUnRead().Count();
            ViewBag.unReadMessage = unReadMessage;

            return PartialView();
        }
    }
}