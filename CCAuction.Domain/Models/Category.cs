using CCAuction.Domain.Interfaces;
using System;

namespace CCAuction.Domain.Models
{
    public class Category : IId, IDateCreated
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset DateCreated { get; set; }
    }
}
