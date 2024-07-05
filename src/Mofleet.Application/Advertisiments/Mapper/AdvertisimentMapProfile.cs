using AutoMapper;
using Mofleet.Advertisiments.Dto;
using Mofleet.Domain.Advertisiments;
using Mofleet.Domain.Attachments;

namespace Mofleet.Advertisiments.Mapper
{
    /// <summary>
    /// PostsMapProfile
    /// </summary>
    public class AdvertisimentMapProfile : Profile
    {
        /// <summary>
        ///  Posts Map Profile 
        /// </summary>
        public AdvertisimentMapProfile()
        {
            CreateMap<Advertisiment, UpdateAdvertisimentDto>();
            CreateMap<CreateAdvertisimentDto, Advertisiment>();
            CreateMap<Advertisiment, CreateAdvertisimentDto>();
            CreateMap<Advertisiment, LiteAdvertisimentDto>();
            CreateMap<Advertisiment, AdvertisimentDetailsDto>();
            CreateMap<Attachment, string>().ConvertUsing(source => source.RelativePath ?? string.Empty);
            CreateMap<CreateAdvertisimentPositionDto, AdvertisimentPosition>();
            CreateMap<AdvertisimentPosition, AdvertisimentPositionDto>();
            CreateMap<AddAdvertisimentPositionDto, AdvertisimentPosition>();
        }



    }
}
