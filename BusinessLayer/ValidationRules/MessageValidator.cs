using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
           RuleFor(x => x.ReceiverMail).NotEmpty().WithMessage("Lütfen Alıcı adresini boş bırakmayınız!");
            RuleFor(c => c.ReceiverMail).EmailAddress().WithMessage("Lütfen Alıcı adresini mail adresi türünde yazınız!");
            //RuleFor(c => c.SenderMail).EmailAddress().WithMessage("Lütfen Gönderen adresini mail adresi türünde yazınız!");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Lütfen Konuyu boş bırakmayınız!");
            //RuleFor(x => x.SenderMail).NotEmpty().WithMessage("Lütfen Gönderici adresini boş bırakmayınız!");
            RuleFor(x => x.MessageContent).NotEmpty().WithMessage("Lütfen Mesajı boş bırakmayınız!");
            RuleFor(x => x.Subject).MinimumLength(3).WithMessage("Lütfen en az 3 karakter girişi yapınız!");
            RuleFor(x => x.Subject).MaximumLength(100).WithMessage("Lütfen 100 karakterden fazla değer girişi yapmayınız!");
        }
    }
}
