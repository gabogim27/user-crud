namespace UserCrud.Infrastructure.Mapper
{
    using AutoMapper;
    using UserCrud.Domain.DTOs;
    using UserCrud.Domain.Entities;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}
