using ETicaretAPI.Application.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Validators.Product
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .NotNull().WithMessage("Lütfen ürün adını boş geçmeyiniz.")
                .MinimumLength(3)
                .MaximumLength(150).WithMessage("Lütfen ürün adını 3 ile 150 karekter arasında giriniz.");

            RuleFor(s => s.Stock)
                .NotEmpty()
                .NotEmpty()
                .NotNull()
                .WithMessage("Lütfen Stok bilgisi giriniz.")
                .Must(s => s >= 0).WithMessage("Stok Bilgisi negatif olamaz.");

            RuleFor(s => s.Price)
               .NotEmpty()
               .NotEmpty()
               .NotNull()
               .WithMessage("Lütfen Price bilgisi giriniz.")
               .Must(s => s >= 0).WithMessage("Price Bilgisi negatif olamaz.");

        }
    }
}
