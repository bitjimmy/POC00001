using AutoMapper;
using CCAuction.Domain.Models;
using CCAuction.Models;

namespace CCAuction.Mappings
{
    public class UserMap : Profile
    {
        public UserMap()
        {
            CreateMap<UserViewModel, User>();
        }
    }
}
