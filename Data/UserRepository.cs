using AutoMapper;
using DotNetAPI.Dots;
using DotNetAPI.Models;
using Microsoft.AspNetCore.Mvc;
using DotNetAPI.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DotNetAPI.Data
{
    public class UserReposirory : IUserReposirory
    {
        DataContextEF _entityFramework;

        public UserReposirory(IConfiguration config)
        {
            _entityFramework = new DataContextEF(config);
        }
        public bool SaveChanges()
        {
            return _entityFramework.SaveChanges() > 0;
        }
        public void AddEntity<T>(T entity)
        {
            if (entity != null)
            {
                _entityFramework.Add(entity);
            }
        }

        public void RemoveEntity<T>(T entity)
        {
            if (entity != null)
            {
                _entityFramework.Remove(entity);
            }
        }

        private IActionResult Ok<T>(T v)
        {
            throw new NotImplementedException();
        }
        private IActionResult Ok()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> getUsers()
        {
            IEnumerable<User> users = _entityFramework.Users.ToList<User>();
            return users;

        }

        public User GetUser(int userId)
        {
            User? user = _entityFramework.Users.Where(u => u.UserId == userId).FirstOrDefault<User>();
            if (user != null)
            {
                return user;
            }
            throw new Exception("Failed to Get user");

        }

        public IActionResult DeleteUser(int userId)
        {
            User? user = _entityFramework.Users.Where(u => u.UserId == userId).FirstOrDefault<User>();
            if (user != null)
            {
                RemoveEntity<User>(user);
                if (SaveChanges())
                {
                    return Ok("Deleted User Called '" + user.FirstName + "'");
                }
                throw new Exception("Failed to  delete user");
            }
            throw new Exception("Failed to Main delete user");
        }


        public IEnumerable<UserSalary> getUserSalaries()
        {
            IEnumerable<UserSalary> users = _entityFramework.UserSalary.ToList<UserSalary>();
            return users;

        }

        public IActionResult GetUserSalary(int userId)
        {
            UserSalary? user = _entityFramework.UserSalary.Where(u => u.UserId == userId).FirstOrDefault<UserSalary>();
            if (user != null)
            {
                return Ok(user);
            }
            throw new Exception("Failed to Get user");

        }

        public IActionResult DeleteUserSalary(int userId)
        {
            UserSalary? user = GetUserSalary(userId) as UserSalary;
            if (user != null)
            {
                RemoveEntity<UserSalary>(user);
                if (SaveChanges())
                {
                    return Ok("Deleted User");
                }
                throw new Exception("Failed to  delete user");
            }
            throw new Exception("Failed to Main delete user");
        }
    

    public IEnumerable<UserJobInfo> getUserJobInfos()
    {
 
        IEnumerable<UserJobInfo> users = _entityFramework.UserJobInfo.ToList<UserJobInfo>();
        return users;

    }

    public IActionResult GetUserJobInfo(int userId)
    {
        UserJobInfo? user = _entityFramework.UserJobInfo.Where(u => u.UserId == userId).FirstOrDefault<UserJobInfo>();
        if (user != null)
        {
            return Ok(user);
        }
        throw new Exception("Failed to Get user");

    }


    public IActionResult DeleteUserJobInfo(int userId)
    {
        UserJobInfo? user = _entityFramework.UserJobInfo.Where(u => u.UserId == userId).FirstOrDefault<UserJobInfo>();
        if (user != null)
        {
            RemoveEntity<UserJobInfo>(user);
            if (SaveChanges())
            {
                return Ok("Deleted User");
            }
            throw new Exception("Failed to  delete user");
        }
        throw new Exception("Failed to Main delete user");
    }



    }
}