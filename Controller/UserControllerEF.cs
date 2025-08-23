using AutoMapper;
using DotNetAPI.Data;
using DotNetAPI.Dots;
using DotNetAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace DotNetAPI.Controllers; // Note: Corrected namespace to plural "Controllers" (convention)

[ApiController]
[Route("[controller]")]
public class UserControllerEF : ControllerBase // Note: Renamed class to follow convention
{
    DataContextEF _entityFramework;
    IUserReposirory _userRepository;
    private readonly IMapper _mapper;

    public UserControllerEF(IConfiguration config, IUserReposirory userRepository, IMapper mapper)
    {
        _entityFramework = new DataContextEF(config);
        _userRepository = userRepository;
        _mapper = mapper;
    }



    [HttpPost("CreateUser")]
    public IActionResult CreateUser(UserToAddDto user)
    {
        User? userDb = _mapper.Map<User>(user);

        _userRepository.AddEntity<User>(userDb);

        if (_userRepository.SaveChanges())
        { return Ok(); }
        throw new Exception("Failed to Create user");

    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        User? userDb = _userRepository.GetUser(user.UserId);
        if (user != null)
        {
            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.Email = user.Email;
            userDb.Active = user.Active;
            userDb.Gender = user.Gender;


            if (_userRepository.SaveChanges())
            { return Ok(); }
            throw new Exception("Failed to Update user");
        }
        throw new Exception("Failed to Main Update user");
    }


    [HttpGet("Users")]
    public IEnumerable<User> getUsers()
    {
        return _userRepository.getUsers();
    }

    [HttpGet("getUser/{userId}")]
    public User GetUser(int userId)
    {
        return _userRepository.GetUser(userId);
    }


    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        return _userRepository.DeleteUser(userId);
    }




    //UserSallery///////
    [HttpPost("CreateUserSalary")]
    public IActionResult CreateUserSalary(UserSalary user)
    {
        UserSalary? userDb = _mapper.Map<UserSalary>(user);

        _userRepository.AddEntity<UserSalary>(userDb);

        if (_userRepository.SaveChanges())
        { return Ok(); }
        throw new Exception("Failed to Create user");

    }

    [HttpPut("EditUserSalary")]
    public IActionResult EditUserSalary(UserSalary user)
    {
        UserSalary? userDb = _userRepository.GetUserSalary(user.UserId) as UserSalary;
        if (user != null)
        {
            userDb.Salary = user.Salary;


            if (_userRepository.SaveChanges())
            { return Ok(); }
            throw new Exception("Failed to Update user");
        }
        throw new Exception("Failed to Main Update user");
    }




    [HttpGet("UserSalaries")]
    public IEnumerable<UserSalary> getUserSalaries()
    {
        return _userRepository.getUserSalaries();

    }

    [HttpGet("getUserSalary/{userId}")]
    public IActionResult GetUserSalary(int userId)
    {
        return _userRepository.GetUserSalary(userId);

    }


    [HttpDelete("DeleteUserSalary/{userId}")]
    public IActionResult DeleteUserSalary(int userId)
    {
        return _userRepository.DeleteUserSalary(userId);
    }


    //UserInfo/////////
    [HttpPost("CreateUserJobInfo")]
    public IActionResult CreateUserJobInfo(UserJobInfo user)
    {
        UserJobInfo? userDb = _mapper.Map<UserJobInfo>(user);

        _userRepository.AddEntity<UserJobInfo>(userDb);

        if (_userRepository.SaveChanges())
        { return Ok(); }
        throw new Exception("Failed to Create user");

    }

    [HttpPut("EditUserJobInfo")]
    public IActionResult EditUserJobInfo(UserJobInfo user)
    {
        UserJobInfo? userDb = _userRepository.GetUserJobInfo(user.UserId) as UserJobInfo;
        if (user != null)
        {
            userDb.JobTitle = user.JobTitle;
            userDb.Department = user.Department;


            if (_userRepository.SaveChanges())
            { return Ok(); }
            throw new Exception("Failed to Update user");
        }
        throw new Exception("Failed to Main Update user");
    }




    [HttpGet("UserJobInfos")]
    public IEnumerable<UserJobInfo> getUserJobInfos()
    {

        return _userRepository.getUserJobInfos();

    }

    [HttpGet("getUserJobInfo/{userId}")]
    public IActionResult GetUserJobInfo(int userId)
    {
        return _userRepository.GetUserJobInfo(userId);

    }


    [HttpDelete("DeleteUserJobInfo/{userId}")]
    public IActionResult DeleteUserJobInfo(int userId)
    {
        return _userRepository.DeleteUserJobInfo(userId);
    }
}

