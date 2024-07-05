using Abp.Application.Services.Dto;
using System;

namespace Mofleet.Domain.Offers.Dto
{
    public class UpdateOfferDto : CreateOfferDto, IEntityDto<Guid>
    {
        public Guid Id { get; set; }


    }
}
