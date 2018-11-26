using CCAuction.Domain.Enums;
using CCAuction.Domain.Models;
using CCAuction.Domain.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Shouldly;

namespace CCAuction.Test
{
    public class TestDataServiceTest
    {
        [Fact]
        public async Task GetUsers_ShouldContain_Admin()
        {
            var service = new TestDataService();
            var users = await service.GetUsers();

            Assert.True(users.Any());
            Assert.NotEmpty(users.Where(x => x.RoleType == RoleType.Admin));
        }

        [Fact]
        public async Task GetUsers_ShouldContain_Admin_UseShouldly()
        {
            var service = new TestDataService();
            var users = await service.GetUsers();

            users.ShouldNotBeEmpty();
            users.ShouldContain(users.Where(x => x.RoleType == RoleType.Admin).FirstOrDefault());
        }

        [Fact]
        public async Task GetUsers_ShouldContain_Member()
        {
            var service = new TestDataService();
            var users = await service.GetUsers();

            Assert.True(users.Any());
            Assert.NotEmpty(users.Where(x => x.RoleType == RoleType.Member));
        }

        [Fact]
        public async Task GetMembers_ShouldContain_MemberOnly()
        {
            var service = new TestDataService();
            var members = await service.GetMembers();

            Assert.True(members.Any());
            Assert.NotEmpty(members.Where(x => x.RoleType == RoleType.Member));
            Assert.Empty(members.Where(x => x.RoleType == RoleType.Admin));
        }

        [Fact]
        public async Task GetCategories_ShouldContain_Data()
        {
            var service = new TestDataService();
            var categories = await service.GetCategories();

            Assert.True(categories.Any());
        }

        [Fact]
        public async Task GetItems_ShouldContain_Data()
        {
            var service = new TestDataService();
            var items = await service.GetItems();

            Assert.True(items.Any());
            foreach (var i in items)
            {
                Assert.NotNull(i.Category);
                Assert.NotNull(i.Seller);
                Assert.True(i.ItemBidHistories.Any());
                foreach (var ibh in i.ItemBidHistories)
                {
                    Assert.NotNull(ibh.Bidder);
                }
            }
        }

        [Fact]
        public async Task GetItemsByCategoryName_ShouldHandle_IgnoreCase()
        {
            var service = new TestDataService();

            var categoryName = "MOBILE";
            var items_MOBILE = await service.GetItemsByCategoryName(categoryName);
            Assert.True(items_MOBILE.Any());

            categoryName = "mobile";
            var items_mobile = await service.GetItemsByCategoryName(categoryName);
            Assert.True(items_mobile.Any());

            Assert.Equal(items_MOBILE.Count(), items_mobile.Count());
        }
    }
}
