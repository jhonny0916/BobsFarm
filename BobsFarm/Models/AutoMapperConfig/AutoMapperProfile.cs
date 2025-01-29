using AutoMapper;
using BobsFarm.Models.Error;
using BobsFarm_BO;

namespace BobsFarm.Models.AutoMapperConfig
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BOError, ErrorResponse>();
        }
    }
}
