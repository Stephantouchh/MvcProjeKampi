using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class WriterValidator : AbstractValidator<Writer>
    {
        public WriterValidator()
        {
            RuleFor(x => x.WriterName).NotEmpty().WithMessage("Lütfen Yazar Adını Boş Bırakmayınız!");
            RuleFor(x => x.WriterSurName).NotEmpty().WithMessage("Lütfen Yazar Soyadını Boş Bırakmayınız!");
            RuleFor(x => x.WriterAbout).NotEmpty().WithMessage("Lütfen Hakkımda kısmını Boş Bırakmayınız!");
            RuleFor(x => x.WriterTitle).NotEmpty().WithMessage("Ünvan Kısmını Boş Geçemezsiniz.");
            RuleFor(x => x.WriterName).MinimumLength(2).WithMessage("Lütfen en az 2 karakterlik bir Ad yazınız!");
            RuleFor(x => x.WriterSurName).MinimumLength(2).WithMessage("Lütfen en az 2 karakterlik bir Soyad yazınız!");  
            RuleFor(x => x.WriterPassword).NotEmpty().WithMessage("Şifre Boş Geçilemez.");
            RuleFor(x => x.WriterMail).EmailAddress().WithMessage("Mail Adresiniz example@example.com Şeklinde Olmalıdır.");
            //RuleFor(x => x.WriterPassword).MinimumLength(3).WithMessage("Şifre En Az 3 Karakter Olmalıdır.");
            RuleFor(x => x.WriterAbout).Must(MustBea).WithMessage("Hakkında Kısmında Mutlaka 'a' Harfi Bulunmalıdır.");
        }
        private bool MustBea(string arg)
        {
            return arg.Contains("a");
        }
    }
}
