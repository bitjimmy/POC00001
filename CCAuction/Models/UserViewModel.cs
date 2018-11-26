using CCAuction.Domain.Enums;
using System;

namespace CCAuction.Models
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get => $"{FirstName} {LastName}"; }
        public GenderType GenderType { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public RoleType RoleType { get; set; }
    }
}
