using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class WriterValidator : AbstractValidator<Writer>
    {
        public WriterValidator()
        {
            RuleFor(x => x.WriterName).NotEmpty().WithMessage("Yazar adı boş geçilemez.");
            RuleFor(x => x.WriterName).MinimumLength(2).WithMessage("Lütfen en az 2 karakter girişi yapın.");
            RuleFor(x => x.WriterName).MaximumLength(50).WithMessage("En fazla 50 karakter girilebilir");
            RuleFor(x => x.WriterMail).NotEmpty().WithMessage("E-mail alanı boş geçilemez.");
            RuleFor(x => x.WriterPassword).NotEmpty().WithMessage("Sifre bos gecilemez");
            RuleFor(p => p.WriterPassword).Matches(@"[A-Z]+").WithMessage("Sifre en az bir büyük harf içermelidir");
            RuleFor(p => p.WriterPassword).Matches(@"[a-z]+").WithMessage("Sifre en az bir küçük harf içermelidir");
            RuleFor(p => p.WriterPassword).Matches(@"[0-9]+").WithMessage("Sifre en az bir rakam içermelidir");
        }

        private bool IsPasswordValid(string arg)
        {
            try
            {
                Regex regex = new Regex(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[0-9])[A-Za-z\d]");
                return regex.IsMatch(arg);
            }
            catch
            {
                return false;
            }
        }
    }
}