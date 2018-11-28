using AutoMapper;
using CCAuction.Domain.Models;
using CCAuction.ReactSPA.Models;

namespace CCAuction.ReactSPA.Mappings
{
    public class ItemBidHistoryMap : Profile
    {
        public ItemBidHistoryMap()
        {
            CreateMap<ItemBidHistoryViewModel, ItemBidHistory>();
        }
    }
}
