using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.RequestForQuotations.Dto
{
    public class UpdateRequestForQuotationDto : CreateRequestForQuotationDto, IEntityDto<long>
    {
        public long Id { get; set; }

        public void AddValidationErrors(CustomValidationContext context)
        {

            if (RequestForQuotationContacts.Where(x => x.RequestForQuotationContactType == RequestForQuotationContactType.Source).Count() < 1)
                context.Results.Add(new ValidationResult("You Need To Add Contact For Source "));
            //if (RequestForQuotationContacts.Where(x => x.RequestForQuotationContactType == RequestForQuotationContactType.Destination).Count() < 1)
            //    context.Results.Add(new ValidationResult("You Need To Add Contact For Destination "));
        }
    }


}
