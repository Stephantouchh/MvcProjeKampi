using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class MessageManager : IMessageService
    {
        IMessageDal _messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public Message GetByID(int id)
        {
            return _messageDal.Get(x => x.MessageID == id);
        }

        public List<Message> GetList()
        {
            return _messageDal.List(x => x.ReceiverMail == "gizem@hotmail.com").Where(x => x.IsRead == true).ToList();
        }

        public List<Message> GetListInbox(string p)
        {
            return _messageDal.List(x => x.ReceiverMail == p);
        }
        public List<Message> GetListSendBox(string p)
        {
            return _messageDal.List(x => x.SenderMail == p);
        }

        public List<Message> GetListUnRead()
        {
            return _messageDal.List(x => x.ReceiverMail == "gizem@hotmail.com").Where(x => x.IsRead == false).ToList();
        }

        public void MessageAdd(Message message)
        {
            _messageDal.Insert(message);
        }

        public void MessageDelete(Message message)
        {
            _messageDal.Delete(message);
        }

        public void MessageUpdate(Message message)
        {
            _messageDal.Update(message);
        }
    }
}
