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
using Serilog;

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
        public async Task<IActionResult> GetAllUsers([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var users = await _userService.GetAllUsersAsync(pageNumber, pageSize);
            if (users is null)
            {
                Log.Warning("Failed to retrieve employees from database.");
                return NotFound("Failed to retrieve employees from database.");
            }
            return Ok(users);
        }

        [HttpPost("add-employee")]
        public async Task<IActionResult> AddEmployee([FromQuery] Guid userId, [FromBody] UserAddDto postUser)
        {
            if (postUser is null)
            {
                Log.Error("Post user is null");
                return BadRequest("Post user is null");
            }
            
            var employeeMap = _mapper.Map<User>(postUser);
            var result = await _validator.ValidateAsync(employeeMap);

            if (result.IsValid)
            {
                var user = await _userService.GetUserById(userId);
                if (user is null)
                {
                    Log.Error("No employer found with this id value..");
                    return NotFound("No employer found with this id value..");
                }
                if (user.RoleOfEmp == Role.Employer)
                {
                    var map = _mapper.Map<User>(postUser);
                    
                    //TODO : ***************** Date İşlemi ve parse durumu sorulacak ****************
                    
                    map.DateOfEntry = Convert.ToDateTime(map.DateOfEntry);
                    await _dbContext.AddAsync(map);
                    await _dbContext.SaveChangesAsync();
                    return Ok(postUser);
                }
                else
                {
                    Log.Warning("Unauthorized transaction");
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
            if (user is null)
            {
                Log.Error("No employee with this id value was found.");
                return NotFound("No employee with this id value was found.");
            }
            return Ok(user);
        }
        

        [HttpPost("update-employee")]
        public async Task<IActionResult> UpdateEmployee([FromQuery] Guid userId,
            [FromQuery] Guid employeeId, [FromBody] UserUpdateDto postUser)
        {
            var employeeMap = _mapper.Map<User>(postUser);
            var result = await _validator.ValidateAsync(employeeMap);

            if (result.IsValid)
            {
                var user = await _userService.GetUserById(userId);
                
                if (user is null)
                {
                    Log.Error("No employee with this id value was found.");
                    return NotFound("No employee with this id value was found.");
                }
                
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
                    else
                    {
                        Log.Error("No employee found with this id value");
                        return NotFound("No employee with this id value was found.");
                    }
                }
                Log.Warning("Unauthorized transaction");
                return Forbid("Unauthorized action: User is not an employer.");
            }
            
            result.AddToModelState(this.ModelState);
            return BadRequest(ModelState);
        }
        

        [HttpDelete("delete-employee")]
        public async Task<IActionResult> DeleteEmployee([FromQuery] Guid userId, [FromBody] Guid employeeId)
        {
            var user = await _userService.GetUserById(userId);
            if (user is null)
            {
                Log.Error("No employee with this id value was found.");
                return NotFound("No employee with this id value was found.");
            }
            if (user.RoleOfEmp == Role.Employer)
            {
                var employee = _userService.GetUserById(employeeId);
                if (employee is not null)
                {
                    _dbContext.Remove(employee);
                    await _dbContext.SaveChangesAsync();
                    return Ok(user);
                }
                else
                {
                    Log.Error("No employee found with this id value");
                    return NotFound("No employee with this id value was found.");
                }
            }
            Log.Warning("Unauthorized transaction");
            return Forbid("Unauthorized action: User is not an employer.");

        }
        [HttpDelete("delete-employeeList")]
        public async Task<IActionResult> DeleteEmployeeList([FromQuery] Guid userId, [FromBody] List<Guid> employeeIdList)
        {
            var user = await _userService.GetUserById(userId);
            if (user is null)
            {
                Log.Error("No employer found with this id value.");
                return NotFound("No employer found with this id value.");
            }
            if (user.RoleOfEmp == Role.Employer)
            {
                List<User> employeeList = [];
                foreach (var employeeId in employeeIdList)
                {
                    employeeList.Add(await _userService.GetUserById(employeeId));
                }

                foreach (var emp in employeeList)
                {
                    _dbContext.Remove(emp);
                    await _dbContext.SaveChangesAsync();
                }

                return Ok(employeeList);

            }
            Log.Warning("Unauthorized transaction");
            return Forbid("Unauthorized action: User is not an employer.");
        }
    }
}