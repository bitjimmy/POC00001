using AutoMapper;
using CCAuction.Domain.Models;
using CCAuction.Models;

namespace CCAuction.Mappings
{
    public class ItemMap : Profile
    {
        public ItemMap()
        {
            CreateMap<ItemViewModel, Item>();
        }
    }
}
