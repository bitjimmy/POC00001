using AutoMapper;
using CCAuction.Domain.Models;
using CCAuction.Models;

namespace CCAuction.Mappings
{
    public class CategoryMap : Profile
    {
        public CategoryMap()
        {
            CreateMap<CategoryViewModel, Category>();
        }
    }
}
