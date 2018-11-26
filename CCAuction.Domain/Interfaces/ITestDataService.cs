using CCAuction.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CCAuction.Domain.Interfaces
{
    public interface ITestDataService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<IEnumerable<User>> GetMembers();
        Task<IEnumerable<Category>> GetCategories();
        Task<IEnumerable<Item>> GetItems();
        Task<IEnumerable<Item>> GetItemsByCategoryName(string categoryName);
    }
}
