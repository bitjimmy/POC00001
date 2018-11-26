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
    public class UsersController : ControllerBase
    {
        readonly ITestDataService _testDataService;
        readonly IMapper _mapper;
        public UsersController(ITestDataService testDataService, IMapper mapper)
        {
            _testDataService = testDataService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _testDataService.GetUsers();
            var result = _mapper.Map<IEnumerable<UserViewModel>>(users);
            return Ok(result.OrderBy(x => x.FullName));
        }

        [HttpGet]
        [Route("members")]
        public async Task<IActionResult> GetMembers()
        {
            var users = await _testDataService.GetMembers();
            var result = _mapper.Map<IEnumerable<UserViewModel>>(users);
            return Ok(result.OrderBy(x => x.FullName));
        }
    }
}