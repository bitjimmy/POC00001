using CCAuction.Domain.Enums;
using CCAuction.Domain.Interfaces;
using System;

namespace CCAuction.Domain.Models
{
    public class User : IId, IDateCreated
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderType GenderType { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public RoleType RoleType { get; set; }
        public bool Active { get; set; }
        public DateTimeOffset? LastLoginDateTime { get; set; }
        public DateTimeOffset DateCreated { get; set; }
    }
}

