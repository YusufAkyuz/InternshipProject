using Emp.Service.Concretes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Threading.Tasks;
using AutoMapper;
using Emp.Data.Context;
using Emp.Entity.DTOs;
using Emp.Entity.Entities;
using Emp.Entity.Enums;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Emp.Presentation.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IValidator<User> _validator;
        private readonly IUserService _service;

        public UserController(IUserService userService, 
            IMapper mapper, 
            IValidator<User> validator, 
            IUserService service)
        {
            _userService = userService;
            _mapper = mapper;
            _validator = validator;
            _service = service;
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
                    await _userService.AddAsync(map);
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
                        _mapper.Map(postUser, employee);
                        employee.DateOfEntry = Convert.ToDateTime(postUser.DateOfEntry);
                        await _userService.UpdateAsync(employee);
                        return Ok(employee);
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
        
        // ------- TODO : Searching process will include in the project
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Search([FromQuery] string name, [FromQuery] string lastName)
        {
            try
            {
                var result = await _userService.SearchAsync(name, lastName);
                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound("No user with this name or surname was found.");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the DB.");
            }
        }
        
        
        [HttpDelete("delete-employee")]
        public async Task<IActionResult> DeleteEmployee([FromQuery] Guid userId, [FromBody] Guid employeeId)
        {
            var user = await _userService.GetUserById(userId);
            if (user is null)
            {
                Log.Error("No employer with this id value was found.");
                return NotFound("No employer with this id value was found.");
            }
            if (user.RoleOfEmp == Role.Employer)
            {
                var employee = await _userService.GetUserById(employeeId);
                if (employee is not null)
                {
                    //var employee = _mapper.Map<User>(employeeTask);
                    await _userService.DeleteAsync(employee);
                    return Ok(employee);
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
                    await _userService.DeleteAsync(emp);
                }

                return Ok(employeeList);

            }
            Log.Warning("Unauthorized transaction");
            return Forbid("Unauthorized action: User is not an employer.");
        }
        
        
    }
}