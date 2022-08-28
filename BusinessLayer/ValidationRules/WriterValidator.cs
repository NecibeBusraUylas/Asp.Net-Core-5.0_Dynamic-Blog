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
            RuleFor(x => x.WriterMail).EmailAddress().WithMessage("Lütfen geçerli bir e-mail giriniz.");
            RuleFor(x => x.WriterAbout).NotEmpty().WithMessage("Hakkınızda kısmı boş Bırakılamaz.");
            RuleFor(x => x.WriterPassword).NotEmpty().WithMessage("Parola bos gecilemez");
            RuleFor(p => p.WriterPassword).Must(IsPasswordValid).WithMessage("Parola en az bir büyük harf, bir küçük harf ve rakam içermelidir");
            RuleFor(x => x.WriterPassword).MinimumLength(6).WithMessage("Parolanız en az 6 karakter içermelidir.");
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