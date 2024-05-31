using AutoMapper;
using MealBookingAPI.Application.DTOs;
using MealBookingAPI.Data.Models;
using MealBookingAPI.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace MealBookingAPI.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;
        public UserController(IRepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTEntity(UserRequestDTO request)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<UserRequestDTO, User>(request);
                var inserted = await _repository.InsertAsync(user);
                if (inserted <= 0)
                {
                    return BadRequest("User not added");
                }
                else
                {
                    return Ok("User added");
                }
            }
            else
            {
                return BadRequest("User not added");
            }
        }
    }
}
