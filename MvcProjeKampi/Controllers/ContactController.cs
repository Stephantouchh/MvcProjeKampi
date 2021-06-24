using BusinessLayer.Concrete;
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
        ContactManager contactmanager = new ContactManager(new EfContactDal());        
        MessageManager messagemanager = new MessageManager(new EfMessageDal());
        DraftManager draftManager = new DraftManager(new EfDraftDal());

      
        public ActionResult Index()
        {
            var contactvalues = contactmanager.GetList();
            return View(contactvalues);
        }
        public ActionResult GetContactDetails(int id)
        {
            var contactvalues = contactmanager.GetByID(id);
            return View(contactvalues);
        }
        public PartialViewResult ContactSideBarPartial()
        {

            string p = (string)Session["WriterMail"];
            var writeridinfo = _context.Writers.Where(x => x.WriterMail == p).Select(y => y.WriterId).FirstOrDefault();

            var receiverMail = _context.Messages.Count(m => m.ReceiverMail == p).ToString();
            ViewBag.receiverMail = receiverMail;

            var senderMail = _context.Messages.Count(m => m.SenderMail == p).ToString();
            ViewBag.senderMail = senderMail;

            var contact = _context.Contacts.Count().ToString();
            ViewBag.contact = contact;

            var draft = _context.Drafts.Count().ToString();
            ViewBag.draft = draft;

            var readMessage = messagemanager.GetList().Count();
            ViewBag.readMessage = readMessage;

            var unReadMessage = messagemanager.GetListUnRead().Count();
            ViewBag.unReadMessage = unReadMessage;

            return PartialView();
        }
    }
}