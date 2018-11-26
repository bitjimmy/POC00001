using System;

namespace CCAuction.Models
{
    public class ItemBidHistoryViewModel
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public decimal BidPrice { get; set; }
        public DateTimeOffset BidDate { get; set; }
        public Guid BidderId { get; set; }
        public UserViewModel Bidder { get; set; }
    }
}
