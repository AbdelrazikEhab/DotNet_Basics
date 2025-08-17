using DotNetAPI.Data;
using DotNetAPI.Dots;
using DotNetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAPI.Controllers; // Note: Corrected namespace to plural "Controllers" (convention)

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase // Note: Renamed class to follow convention
{
    DataContextDapper _dapper;
    public UserController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);

    }

    [HttpGet("testConnection")]
    public DateTime testConnection()
    {
        return _dapper.loadSingleData<DateTime>("SELECT GETDATE()");

    }


    [HttpPost("CreateUser")]
    public IActionResult CreateUser(UserToAddDto user)
    {
        string sql = @"
        INSERT INTO TutorialAppSchema.USERS ([FirstName], [LastName], [Email], [Gender], [Active])
        VALUES(" +
        "'" + user.FirstName +
        "','" + user.LastName +
        "','" + user.Email +
        "','" + user.Active +
        "','" + user.Active +
        "')";
        Console.WriteLine(sql);
        if (_dapper.ExcuteSql(sql))
        { return Ok(); }
        throw new Exception("Failed to create user");
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        string sql = @"
        UPDATE TutorialAppSchema.USERS 
        SET
        [FirstName]='" + user.FirstName + @"', 
        [LastName]='" + user.LastName + @"',
        [Email]='" + user.Email + @"', 
        [Gender]='" + user.Active + @"',
        [Active]='" + user.Active + @"'
        WHERE UserId = '" + user.UserId + @"'";

        if (_dapper.ExcuteSql(sql))
        { return Ok(); }
        throw new Exception("Failed to Update user");
    }




    [HttpGet("Users")]
    public IEnumerable<User> getUsers()
    {
        string sql = @"
        SELECT [FirstName],[LastName],[Email],[Gender],[Active] FROM TutorialAppSchema.Users;
        ";
        IEnumerable<User> users = _dapper.loadData<User>(sql);
        return users;

    }

    [HttpGet("getUser/{userId}")]
    public User GetUser(int userId)
    {
        string sql = @"
        SELECT [FirstName],[LastName],[Email],[Gender],[Active] FROM TutorialAppSchema.Users WHERE UserId =" + userId;
        User user = _dapper.loadSingleData<User>(sql);
        return user;
    }


    [HttpDelete("deleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        string sql = @"DELETE FROM TutorialAppSchema.Users WHERE UserId = '" + userId + "'";
        if (_dapper.ExcuteSql(sql)) { return Ok(); }
        throw new Exception("Failed to delete user");
    }


    //UserSallery///////////////
    [HttpPost("CreateUserSalary")]
    public IActionResult CreateUserSalary(UserSalaryDto user)
    {
        string sql = @"
        INSERT INTO TutorialAppSchema.UserSalary ([Salary])
        VALUES(" +
        "'" + user.Salary +
        "')";
        Console.WriteLine(sql);
        if (_dapper.ExcuteSql(sql))
        { return Ok(); }
        throw new Exception("Failed to create user");
    }

    [HttpPut("EditUserSalary")]
    public IActionResult EditUserSalary(UserSalaryDto user)
    {
        string sql = @"
        UPDATE TutorialAppSchema.UserSalary 
        SET
        [Salary]='" + user.Salary + @"'
        WHERE UserId = '" + user.UserId + @"'";

        if (_dapper.ExcuteSql(sql))
        { return Ok(); }
        throw new Exception("Failed to Update user");
    }




    [HttpGet("UserSalaries")]
    public IEnumerable<User> getUserSalaries()
    {
        string sql = @"
        SELECT [UserId],[Salary] FROM TutorialAppSchema.UserSalary;
        ";
        IEnumerable<User> users = _dapper.loadData<User>(sql);
        return users;

    }

    [HttpGet("getUserSalary/{userId}")]
    public User GetUserSalary(int userId)
    {
        string sql = @"
        SELECT [Salary] FROM TutorialAppSchema.UserSalary WHERE UserId =" + userId;
        User user = _dapper.loadSingleData<User>(sql);
        return user;
    }


    [HttpDelete("deleteUserSalary/{userId}")]
    public IActionResult DeleteUserSalary(int userId)
    {
        string sql = @"DELETE FROM TutorialAppSchema.UserSalary WHERE UserId = '" + userId + "'";
        if (_dapper.ExcuteSql(sql)) { return Ok(); }
        throw new Exception("Failed to delete user");
    }


    //UserInfo///////////////

    [HttpPost("CreateUserJobInfo")]
    public IActionResult CreateUserJobInfo(UserJobInfoDto user)
    {
        string sql = @"
        INSERT INTO TutorialAppSchema.UserJobInfo ([JopTitle],[Department])
        VALUES(" +
        "'" + user.JobTitle +
        "','" + user.Department +
        "')";
        Console.WriteLine(sql);
        if (_dapper.ExcuteSql(sql))
        { return Ok(); }
        throw new Exception("Failed to create user");
    }

    [HttpPut("EditUserJobInfo")]
    public IActionResult EditUserJobInfo(UserJobInfoDto user)
    {
        string sql = @"
        UPDATE TutorialAppSchema.UserJobInfo 
        SET
        [JobTitle]='" + user.JobTitle + @"'
        [Department]='" + user.Department + @"'
        WHERE UserId = '" + user.UserId + @"'";

        if (_dapper.ExcuteSql(sql))
        { return Ok(); }
        throw new Exception("Failed to Update user");
    }




    [HttpGet("UserJobInfos")]
    public IEnumerable<User> getUserJobInfos()
    {
        string sql = @"
        SELECT [JobTitle],[Department] FROM TutorialAppSchema.UserJobInfo;
        ";
        IEnumerable<User> users = _dapper.loadData<User>(sql);
        return users;

    }

    [HttpGet("getUserJobInfo/{userId}")]
    public User GetUserJobInfo(int userId)
    {
        string sql = @"
        SELECT [JobTitle],[Department] FROM TutorialAppSchema.UserJobInfo WHERE UserId =" + userId;
        User user = _dapper.loadSingleData<User>(sql);
        return user;
    }


    [HttpDelete("deleteUserJobInfo/{userId}")]
    public IActionResult DeleteUserJobInfo(int userId)
    {
        string sql = @"DELETE FROM TutorialAppSchema.UserJobInfo WHERE UserId = '" + userId + "'";
        if (_dapper.ExcuteSql(sql)) { return Ok(); }
        throw new Exception("Failed to delete user");
    }

}

