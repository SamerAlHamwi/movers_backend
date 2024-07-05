using AutoMapper;
using Mofleet.Domain.Points;
using Mofleet.Points.Dto;

namespace Mofleet.Points.Mapper
{
    public class PointMapProfile : Profile
    {
        public PointMapProfile()
        {
            CreateMap<Point, CreatePointDto>();
            CreateMap<CreatePointDto, Point>();
            CreateMap<Point, UpdatePointDto>();
            CreateMap<UpdatePointDto, Point>();

        }

    }
}
