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
    private static string[] repostryArray = new[] { "ali", "sami", "salem" };

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
}

