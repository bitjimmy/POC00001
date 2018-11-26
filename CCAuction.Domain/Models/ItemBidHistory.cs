using CCAuction.Domain.Interfaces;
using System;

namespace CCAuction.Domain.Models
{
    public class ItemBidHistory : IId, IDateCreated
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public Item Item { get; set; }
        public decimal BidPrice { get; set; }
        public DateTimeOffset BidDate { get; set; }
        public Guid BidderId { get; set; }
        public User Bidder { get; set; }
        public DateTimeOffset DateCreated { get; set; }
    }
}
