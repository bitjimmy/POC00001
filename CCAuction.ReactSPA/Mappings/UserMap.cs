using AutoMapper;
using CCAuction.Domain.Models;
using CCAuction.ReactSPA.Models;

namespace CCAuction.ReactSPA.Mappings
{
    public class UserMap : Profile
    {
        public UserMap()
        {
            CreateMap<UserViewModel, User>();
        }
    }
}
