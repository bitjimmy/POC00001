using CCAuction.Domain.Enums;
using CCAuction.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCAuction.Domain.Models
{
    public class Item : IId, IDateCreated
    {
        public Item() => ItemBidHistories = new List<ItemBidHistory>();

        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
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
        public User Seller { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? DateModified { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public List<ItemBidHistory> ItemBidHistories { get; set; }
    }
}
