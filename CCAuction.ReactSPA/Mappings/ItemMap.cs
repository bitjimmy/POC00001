using AutoMapper;
using CCAuction.Domain.Models;
using CCAuction.ReactSPA.Models;

namespace CCAuction.ReactSPA.Mappings
{
    public class ItemMap : Profile
    {
        public ItemMap()
        {
            CreateMap<ItemViewModel, Item>();
        }
    }
}
