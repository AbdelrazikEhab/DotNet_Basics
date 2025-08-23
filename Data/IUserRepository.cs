using DotNetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAPI.Data
{
    public interface IUserReposirory
    {

        public bool SaveChanges();
        public void AddEntity<T>(T entity);
        public void RemoveEntity<T>(T entity);
        public IEnumerable<User> getUsers();
        public User GetUser(int userId);
        public IActionResult DeleteUser(int userId);
        public IEnumerable<UserSalary> getUserSalaries();
        public IActionResult GetUserSalary(int userId);

        public IActionResult DeleteUserSalary(int userId);
        public IEnumerable<UserJobInfo> getUserJobInfos();
        public IActionResult GetUserJobInfo(int userId);
        public IActionResult DeleteUserJobInfo(int userId);





    }
}