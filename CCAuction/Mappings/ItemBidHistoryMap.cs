using AutoMapper;
using CCAuction.Domain.Models;
using CCAuction.Models;

namespace CCAuction.Mappings
{
    public class ItemBidHistoryMap : Profile
    {
        public ItemBidHistoryMap()
        {
            CreateMap<ItemBidHistoryViewModel, ItemBidHistory>();
        }
    }
}
