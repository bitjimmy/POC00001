using CCAuction.Domain.Enums;
using CCAuction.Domain.Interfaces;
using CCAuction.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCAuction.Domain.Services
{
    public class TestDataService : ITestDataService
    {
        const int DELAY_IN_MILLISECOND = 100;
        const int BID_DURATION_IN_DAY = 7;
        const decimal BID_PRICE_FACTOR1 = 1.1M;
        const decimal BID_PRICE_FACTOR2 = BID_PRICE_FACTOR1 + 0.1M;
        const decimal BID_PRICE_FACTOR3 = BID_PRICE_FACTOR2 + 0.1M;
        public TestDataService() { }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await Task.Run(() =>
            {
                // mimic some delays like database request
                Task.Delay(DELAY_IN_MILLISECOND);
                var testUsers = new List<User>
                {
                    new User{ Id = Guid.NewGuid(), UserName = "admin.tester", FirstName = "Admin", LastName = "Tester", GenderType = GenderType.Male, DateOfBirth = DateTimeOffset.Now.Date.AddYears(-30), RoleType = RoleType.Admin, Active = true, DateCreated = DateTimeOffset.Now },
                    new User{ Id = Guid.NewGuid(), UserName = "buyer001.tester", FirstName = "Buyer001", LastName = "Tester", GenderType = GenderType.Male, DateOfBirth = DateTimeOffset.Now.Date.AddYears(-30), RoleType = RoleType.Member, Active = true, DateCreated = DateTimeOffset.Now },
                    new User{ Id = Guid.NewGuid(), UserName = "buyer002.tester", FirstName = "Buyer002", LastName = "Tester", GenderType = GenderType.Female, DateOfBirth = DateTimeOffset.Now.Date.AddYears(-35), RoleType = RoleType.Member, Active = true, DateCreated = DateTimeOffset.Now },
                    new User{ Id = Guid.NewGuid(), UserName = "buyer003.tester", FirstName = "Buyer003", LastName = "Tester", GenderType = GenderType.Male, DateOfBirth = DateTimeOffset.Now.Date.AddYears(-40), RoleType = RoleType.Member, Active = true, DateCreated = DateTimeOffset.Now },
                    new User{ Id = Guid.NewGuid(), UserName = "seller001.tester", FirstName = "Seller001", LastName = "Tester", GenderType = GenderType.Female, DateOfBirth = DateTimeOffset.Now.Date.AddYears(-25), RoleType = RoleType.Member, Active = true, DateCreated = DateTimeOffset.Now }
                };
                return testUsers;
            });
            return users;
        }

        public async Task<IEnumerable<User>> GetMembers()
        {
            var users = await GetUsers();
            var members = users.Where(x => x.Active && x.RoleType == RoleType.Member);
            return members;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var categories = await Task.Run(() =>
            {
                // mimic some delays like database request
                Task.Delay(DELAY_IN_MILLISECOND);
                var testCategories = new List<Category>
                {
                    new Category{ Id = Guid.NewGuid(), Name = "Mobile", Description = "Mobile units", DateCreated = DateTimeOffset.Now  },
                    new Category{ Id = Guid.NewGuid(), Name = "Computer", Description = "Computer units", DateCreated = DateTimeOffset.Now  },
                    new Category{ Id = Guid.NewGuid(), Name = "Camera", Description = "Camera units", DateCreated = DateTimeOffset.Now  }
                };
                return testCategories;
            });
            return categories;
        }

        public async Task<IEnumerable<Item>> GetItems()
        {
            var testMembers = await GetMembers();
            var seller001 = testMembers.Where(x => x.UserName.Equals("seller001.tester", StringComparison.OrdinalIgnoreCase)).Single();
            var bidder001 = testMembers.Where(x => x.UserName.Equals("buyer001.tester", StringComparison.OrdinalIgnoreCase)).Single();
            var bidder002 = testMembers.Where(x => x.UserName.Equals("buyer002.tester", StringComparison.OrdinalIgnoreCase)).Single();
            var bidder003 = testMembers.Where(x => x.UserName.Equals("buyer003.tester", StringComparison.OrdinalIgnoreCase)).Single();
            var testCategories = await GetCategories();
            var mobileCategory = testCategories.Where(x => x.Name.Equals("Mobile", StringComparison.OrdinalIgnoreCase)).Single();
            var items = await Task.Run(() =>
            {
                // mimic some delays like database request
                Task.Delay(DELAY_IN_MILLISECOND);
                var testItems = new List<Item>
                {
                    new Item{ Id = Guid.NewGuid(), CategoryId = mobileCategory.Id, Category = mobileCategory, Name = "iPhone 3GS", Description = "iPhone 3GS - Black - 8GB", ConditionType = ConditionType.Used, CurrencyType = CurrencyType.AUD, StartPrice = 99, GST = 10, BidStartDate = DateTimeOffset.Now, BidEndDate = DateTimeOffset.Now.AddDays(BID_DURATION_IN_DAY), SellerId = seller001.Id, Seller = seller001, CreatedBy = seller001.Id, DateCreated= DateTimeOffset.Now },
                    new Item{ Id = Guid.NewGuid(), CategoryId = mobileCategory.Id, Category = mobileCategory, Name = "iPhone 4", Description = "iPhone 4 - Black - 16GB", ConditionType = ConditionType.Used, CurrencyType = CurrencyType.AUD, StartPrice = 199, GST = 10, BidStartDate = DateTimeOffset.Now, BidEndDate = DateTimeOffset.Now.AddDays(BID_DURATION_IN_DAY), SellerId = seller001.Id, Seller = seller001, CreatedBy = seller001.Id, DateCreated= DateTimeOffset.Now },
                    new Item{ Id = Guid.NewGuid(), CategoryId = mobileCategory.Id, Category = mobileCategory, Name = "iPhone 5", Description = "iPhone 5 - White - 32GB", ConditionType = ConditionType.Used, CurrencyType = CurrencyType.AUD, StartPrice = 299, GST = 10, BidStartDate = DateTimeOffset.Now, BidEndDate = DateTimeOffset.Now.AddDays(BID_DURATION_IN_DAY), SellerId = seller001.Id, Seller = seller001, CreatedBy = seller001.Id, DateCreated= DateTimeOffset.Now },
                    new Item{ Id = Guid.NewGuid(), CategoryId = mobileCategory.Id, Category = mobileCategory, Name = "iPhone 6", Description = "iPhone 6 - Black - 64GB", ConditionType = ConditionType.Used, CurrencyType = CurrencyType.AUD, StartPrice = 399, GST = 10, BidStartDate = DateTimeOffset.Now, BidEndDate = DateTimeOffset.Now.AddDays(BID_DURATION_IN_DAY), SellerId = seller001.Id, Seller = seller001, CreatedBy = seller001.Id, DateCreated= DateTimeOffset.Now },
                    new Item{ Id = Guid.NewGuid(), CategoryId = mobileCategory.Id, Category = mobileCategory, Name = "iPhone 7", Description = "iPhone 7 - White - 128GB", ConditionType = ConditionType.Used, CurrencyType = CurrencyType.AUD, StartPrice = 499, GST = 10, BidStartDate = DateTimeOffset.Now, BidEndDate = DateTimeOffset.Now.AddDays(BID_DURATION_IN_DAY), SellerId = seller001.Id, Seller = seller001, CreatedBy = seller001.Id, DateCreated= DateTimeOffset.Now },
                    new Item{ Id = Guid.NewGuid(), CategoryId = mobileCategory.Id, Category = mobileCategory, Name = "iPhone 8", Description = "iPhone 8 - Gold - 256GB", ConditionType = ConditionType.New, CurrencyType = CurrencyType.AUD, StartPrice = 599, GST = 10, BidStartDate = DateTimeOffset.Now, BidEndDate = DateTimeOffset.Now.AddDays(BID_DURATION_IN_DAY), SellerId = seller001.Id, Seller = seller001, CreatedBy = seller001.Id, DateCreated= DateTimeOffset.Now },
                    new Item{ Id = Guid.NewGuid(), CategoryId = mobileCategory.Id, Category = mobileCategory, Name = "iPhone X", Description = "iPhone X - Gold - 512GB", ConditionType = ConditionType.New, CurrencyType = CurrencyType.AUD, StartPrice = 699, GST = 10, BidStartDate = DateTimeOffset.Now, BidEndDate = DateTimeOffset.Now.AddDays(BID_DURATION_IN_DAY), SellerId = seller001.Id, Seller = seller001, CreatedBy = seller001.Id, DateCreated= DateTimeOffset.Now }
                };

                testItems.ForEach(async x => x.ItemBidHistories.Add(await getItemBidHistory(x, bidder001, BID_PRICE_FACTOR1)));
                testItems.ForEach(async x => x.ItemBidHistories.Add(await getItemBidHistory(x, bidder002, BID_PRICE_FACTOR2)));
                testItems.ForEach(async x => x.ItemBidHistories.Add(await getItemBidHistory(x, bidder003, BID_PRICE_FACTOR3)));

                return testItems;
            });
            return items;
        }

        private async Task<ItemBidHistory> getItemBidHistory(Item item, User bidder, decimal bidPriceFactor)
        {
            var itemBidHistory = await Task.Run(() =>
            {
                // mimic some delays like database request
                Task.Delay(DELAY_IN_MILLISECOND);
                var testItemBidHistory = new ItemBidHistory { Id = Guid.NewGuid(), ItemId = item.Id, Item = item, BidPrice = item.StartPrice * bidPriceFactor, BidDate = DateTimeOffset.Now, BidderId = bidder.Id, Bidder = bidder, DateCreated = DateTimeOffset.Now };
                return testItemBidHistory;
            });
            item.HighestBidPrice = itemBidHistory.BidPrice;
            return itemBidHistory;
        }

        public async Task<IEnumerable<Item>> GetItemsByCategoryName(string categoryName)
        {
            var items = await GetItems();
            var filteredItems = items.Where(x => x.Category.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
            return filteredItems;
        }
    }
}
