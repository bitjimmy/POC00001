using AutoMapper;
using CCAuction.Domain.Interfaces;
using CCAuction.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCAuction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        readonly ITestDataService _testDataService;
        readonly IMapper _mapper;
        public CategoriesController(ITestDataService testDataService, IMapper mapper)
        {
            _testDataService = testDataService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _testDataService.GetCategories();
            var result = _mapper.Map<IEnumerable<CategoryViewModel>>(categories.OrderBy(x => x.Name));
            return Ok(result); 
        }
    }
}