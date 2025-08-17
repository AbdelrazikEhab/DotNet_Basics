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
    private readonly IMapper _mapper;

    public UserControllerEF(IConfiguration config, IMapper mapper)
    {
        _entityFramework = new DataContextEF(config);
_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));    }
    private static string[] repostryArray = new[] { "ali", "sami", "salem" };

    // [HttpGet("testConnection")]
    // public DateTime testConnection()
    // {
    //     return _entityFramework.GetService();

    // }


    [HttpPost("CreateUser")]
    public IActionResult CreateUser(UserToAddDto user)
    {
        User? userDb = _mapper.Map<User>(user);

        _entityFramework.Users.Add(userDb);

        if (_entityFramework.SaveChanges() > 0)
        { return Ok(); }
        throw new Exception("Failed to Create user");

    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        User? userDb = _entityFramework.Users.Where(u => u.UserId == user.UserId).FirstOrDefault<User>();
        if (user != null)
        {
            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.Email = user.Email;
            userDb.Active = user.Active;
            userDb.Gender = user.Gender;


            if (_entityFramework.SaveChanges() > 0)
            { return Ok(); }
            throw new Exception("Failed to Update user");
        }
        throw new Exception("Failed to Main Update user");
    }




    [HttpGet("Users")]
    public IEnumerable<User> getUsers()
    {
        // string sql = @"
        // SELECT [FirstName],[LastName],[Email],[Gender],[Active] FROM TutorialAppSchema.Users;
        // ";
        IEnumerable<User> users = _entityFramework.Users.ToList<User>();
        return users;

    }

    [HttpGet("getUser/{userId}")]
    public User GetUser(int userId)
    {
        User? user = _entityFramework.Users.Where(u => u.UserId == userId).FirstOrDefault<User>();
        if (user != null)
        {
            return user;
        }
        throw new Exception("Failed to Get user");

    }


    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        User? user = _entityFramework.Users.Where(u => u.UserId == userId).FirstOrDefault<User>();
        if (user != null)
        {
            _entityFramework.Users.Remove(user);
            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok("Deleted User Called '" + user.FirstName + "'");
            }
            throw new Exception("Failed to  delete user");
        }
        throw new Exception("Failed to Main delete user");
    }


    //UserSallery///////
    [HttpPost("CreateUserSalary")]
    public IActionResult CreateUserSalary(UserSalaryDto user)
    {
        UserSalary? userDb = _mapper.Map<UserSalary>(user);

        _entityFramework.UserSalary.Add(userDb);

        if (_entityFramework.SaveChanges() > 0)
        { return Ok(); }
        throw new Exception("Failed to Create user");

    }

    [HttpPut("EditUserSalary")]
    public IActionResult EditUserSalary(UserSalaryDto user)
    {
        UserSalary? userDb = _entityFramework.UserSalary.Where(u => u.UserId == user.UserId).FirstOrDefault<UserSalary>();
        if (user != null)
        {
            userDb.Salary = user.Salary;


            if (_entityFramework.SaveChanges() > 0)
            { return Ok(); }
            throw new Exception("Failed to Update user");
        }
        throw new Exception("Failed to Main Update user");
    }




    [HttpGet("UserSalaries")]
    public IEnumerable<UserSalary> getUserSalaries()
    {
        // string sql = @"
        // SELECT [FirstName],[LastName],[Email],[Gender],[Active] FROM TutorialAppSchema.Users;
        // ";
        IEnumerable<UserSalary> users = _entityFramework.UserSalary.ToList<UserSalary>();
        return users;

    }

    [HttpGet("getUserSalary/{userId}")]
    public IActionResult GetUserSalary(int userId)
    {
        UserSalary? user = _entityFramework.UserSalary.Where(u => u.UserId == userId).FirstOrDefault<UserSalary>();
        if (user != null)
        {
            return Ok(user);
        }
        throw new Exception("Failed to Get user");

    }


    [HttpDelete("DeleteUserSalary/{userId}")]
    public IActionResult DeleteUserSalary(int userId)
    {
        UserSalary? user = _entityFramework.UserSalary.Where(u => u.UserId == userId).FirstOrDefault<UserSalary>();
        if (user != null)
        {
            _entityFramework.UserSalary.Remove(user);
            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok("Deleted User");
            }
            throw new Exception("Failed to  delete user");
        }
        throw new Exception("Failed to Main delete user");
    }


    //UserInfo/////////
    [HttpPost("CreateUserJobInfo")]
    public IActionResult CreateUserJobInfo(UserJobInfoDto user)
    {
        UserJobInfo? userDb = _mapper.Map<UserJobInfo>(user);

        _entityFramework.UserJobInfo.Add(userDb);

        if (_entityFramework.SaveChanges() > 0)
        { return Ok(); }
        throw new Exception("Failed to Create user");

    }

    [HttpPut("EditUserJobInfo")]
    public IActionResult EditUserJobInfo(UserJobInfoDto user)
    {
        UserJobInfo? userDb = _entityFramework.UserJobInfo.Where(u => u.UserId == user.UserId).FirstOrDefault<UserJobInfo>();
        if (user != null)
        {
            userDb.JobTitle = user.JobTitle;
            userDb.Department = user.Department;


            if (_entityFramework.SaveChanges() > 0)
            { return Ok(); }
            throw new Exception("Failed to Update user");
        }
        throw new Exception("Failed to Main Update user");
    }




    [HttpGet("UserJobInfos")]
    public IEnumerable<UserJobInfo> getUserJobInfos()
    {
        // string sql = @"
        // SELECT [FirstName],[LastName],[Email],[Gender],[Active] FROM TutorialAppSchema.Users;
        // ";
        IEnumerable<UserJobInfo> users = _entityFramework.UserJobInfo.ToList<UserJobInfo>();
        return users;

    }

    [HttpGet("getUserJobInfo/{userId}")]
    public IActionResult GetUserJobInfo(int userId)
    {
        UserJobInfo? user = _entityFramework.UserJobInfo.Where(u => u.UserId == userId).FirstOrDefault<UserJobInfo>();
        if (user != null)
        {
            return Ok(user);
        }
        throw new Exception("Failed to Get user");

    }


    [HttpDelete("DeleteUserJobInfo/{userId}")]
    public IActionResult DeleteUserJobInfo(int userId)
    {
        UserJobInfo? user = _entityFramework.UserJobInfo.Where(u => u.UserId == userId).FirstOrDefault<UserJobInfo>();
        if (user != null)
        {
            _entityFramework.UserJobInfo.Remove(user);
            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok("Deleted User");
            }
            throw new Exception("Failed to  delete user");
        }
        throw new Exception("Failed to Main delete user");
    }

}

