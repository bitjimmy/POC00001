using CCAuction.Domain.Enums;
using System;
using System.Collections.Generic;

namespace CCAuction.Models
{
    public class ItemViewModel
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public CategoryViewModel Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ConditionType ConditionType { get; set; }
        public byte[] Image { get; set; }
        public CurrencyType CurrencyType { get; set; }
        public decimal StartPrice { get; set; }

        /// <summary>
        /// Highest bid price is also the current bid price
        /// </summary>
        public decimal? HighestBidPrice { get; set; }
        public decimal? GST { get; set; }

        public DateTimeOffset BidStartDate { get; set; }
        public DateTimeOffset BidEndDate { get; set; }
        public Guid SellerId { get; set; }
        public UserViewModel Seller { get; set; }
        public int DurationInDay
        {
            get
            {
                if (BidEndDate > BidStartDate) return (int)BidEndDate.Subtract(BidStartDate).TotalDays;
                return 0;
            }
        }
        public int BidCount { get => ItemBidHistories.Count; }

        public List<ItemBidHistoryViewModel> ItemBidHistories { get; set; }

    }
}
