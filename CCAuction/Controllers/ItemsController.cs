using AutoMapper;
using CCAuction.Domain.Interfaces;
using CCAuction.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCAuction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        readonly ITestDataService _testDataService;
        readonly IMapper _mapper;
        public ItemsController(ITestDataService testDataService, IMapper mapper)
        {
            _testDataService = testDataService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var items = await _testDataService.GetItems();
            var result = _mapper.Map<IEnumerable<ItemViewModel>>(items.OrderBy(x => x.StartPrice));
            return Ok(result);
        }

        [HttpGet]
        [Route("category/{name}")]
        public async Task<IActionResult> GetByCategory(string name)
        {
            var items = await _testDataService.GetItemsByCategoryName(name);
            var result = _mapper.Map<IEnumerable<ItemViewModel>>(items.OrderBy(x => x.StartPrice));
            return Ok(result);
        }
    }
}