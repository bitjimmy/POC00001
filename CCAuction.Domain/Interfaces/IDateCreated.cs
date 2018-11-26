using System;

namespace CCAuction.Domain.Interfaces
{
    public interface IDateCreated
    {
        DateTimeOffset DateCreated { get; set; }
    }
}
