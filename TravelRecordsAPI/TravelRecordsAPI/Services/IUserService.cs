using Microsoft.AspNetCore.Mvc;
using TravelRecordsAPI.Models;

namespace TravelRecordsAPI.Services
{
    public interface IUserService
    {
        Task<ActionResult<IEnumerable<User>>> GetAllAsync();

        Task<ActionResult<User>> GetUser(int id);

        Task<ActionResult<User>> GetUser(string username,string password);

        Task<ActionResult<User>> Update(int id, User user);

        Task<ActionResult<User>> Add(User user);
    }
}
