using Luxury.Domain.Models;
using MediatR;

namespace Luxury.Api.Application.Commands
{
    public class CreateOwnerAndPropertyCommand : IRequest<LuxuryResponse>
    {

        public string  NameOwner { get; set; }
        public string  AddressOwner { get; set; }
        public string  PhotoOwner { get; set; }
        public DateTime  BirthdayOwner { get; set; }
        public string  NameProperty { get; set; }
        public string  AddressProperty { get; set; }
        public long?  PriceProperty { get; set; }
        public int?  CodeInternalProperty { get; set; }
        public int?  YearProperty { get; set; }
        public string  FileImagePropertyImage { get; set; }
        public bool?  EnablePropertyImage { get; set; }

        public CreateOwnerAndPropertyCommand(string nameOwner, string addressOwner, string photoOwner, DateTime birthdayOwner, string nameProperty,
            string addressProperty, long? priceProperty, int? codeInternalProperty, int? yearProperty, string fileImagePropertyImage,
            bool? enablePropertyImage)
        {
            NameOwner = nameOwner;
            AddressOwner = addressOwner;
            PhotoOwner = photoOwner;
            BirthdayOwner = birthdayOwner;
            NameProperty = nameProperty;
            AddressProperty = addressProperty;
            PriceProperty = priceProperty;
            CodeInternalProperty = codeInternalProperty;
            YearProperty = yearProperty;
            FileImagePropertyImage = fileImagePropertyImage;
            EnablePropertyImage = enablePropertyImage;
        }
    }
}
