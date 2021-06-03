using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            RuleFor(x => x.UserMail).NotEmpty().WithMessage("Lütfen Mail adresini boş bırakmayınız!");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Lütfen Konu adını boş bırakmayınız!");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Lütfen Kullanıcı adını Boş Bırakmayınız!");
            RuleFor(x => x.Subject).MinimumLength(3).WithMessage("Lütfen en az 3 karakter girişi yapınız!");
            RuleFor(x => x.UserName).MinimumLength(2).WithMessage("Lütfen en az 2 karakterlik bir Ad yazınız!");
            RuleFor(x => x.Subject).MaximumLength(50).WithMessage("Lütfen 50 karakterden fazla değer girişi yapmayınız!");
        }
    }
}
