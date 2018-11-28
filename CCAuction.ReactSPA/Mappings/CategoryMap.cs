using AutoMapper;
using CCAuction.Domain.Models;
using CCAuction.ReactSPA.Models;

namespace CCAuction.ReactSPA.Mappings
{
    public class CategoryMap : Profile
    {
        public CategoryMap()
        {
            CreateMap<CategoryViewModel, Category>();
        }
    }
}
