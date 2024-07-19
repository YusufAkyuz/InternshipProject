using Emp.Service.Concretes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AutoMapper;
using Emp.Data.Context;
using Emp.Entity.DTOs;
using Emp.Entity.Entities;
using Emp.Entity.Enums;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Emp.Presentation.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IValidator<User> _validator;

        public UserController(IUserService userService, AppDbContext dbContext, IMapper mapper, IValidator<User> validator)
        {
            _userService = userService;
            _dbContext = dbContext;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet("employees")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPost("add-employee")]
        public async Task<IActionResult> AddEmployee([FromQuery] Guid userId, [FromBody] UserAddDto postUser)
        {
            var userMap = _mapper.Map<User>(postUser);
            var result = await _validator.ValidateAsync(userMap);

            if (result.IsValid)
            {
                var user = await _userService.GetUserById(userId);
                if (user.RoleOfEmp == Role.Employer)
                {
                    var map = _mapper.Map<User>(postUser);
                    await _dbContext.AddAsync(map);
                    await _dbContext.SaveChangesAsync();
                    return Ok(postUser);
                }
                else
                {
                    return Forbid("Unauthorized action: User is not an employer.");
                }
            }
            else
            {
                result.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }
        }
    

        [HttpGet("get_employee_info")]
        public async Task<IActionResult> EmployeeInfo([FromQuery] Guid employeeId)
        {
            var user = await _userService.GetUserById(employeeId);
            return Ok(user);
        }
        

        [HttpPost("update-employee")]
        public async Task<IActionResult> UpdateEmployee([FromQuery] Guid userId,
            [FromQuery] Guid employeeId, [FromBody] UserUpdateDto postUser)
        {
            var user = await _userService.GetUserById(userId);
            if (user.RoleOfEmp == Role.Employer)
            {
                var employee = await _userService.GetUserById(employeeId);
                if (employee is not null)
                {
                    var map = _mapper.Map<User>(postUser);
                    _mapper.Map(postUser, employee);
                    await _dbContext.SaveChangesAsync();
                    return Ok(postUser);
                }
            }
            return Forbid("Unauthorized action: User is not an employer.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee([FromBody] Guid userId)
        {
            var user = await _userService.GetUserById(userId);
            if (user.RoleOfEmp == Role.Employer)
            {
                _dbContext.Remove(user);
                await _dbContext.SaveChangesAsync();
                return Ok(user);
            }
            return Forbid("Unauthorized action: User is not an employer.");
        }
    }
}