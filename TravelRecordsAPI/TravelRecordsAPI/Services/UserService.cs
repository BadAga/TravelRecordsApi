using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using TravelRecordsAPI.Models;

namespace TravelRecordsAPI.Services
{
    public class UserService : IUserService
    {
        #region Dependency Injection / Constructor
        private readonly CoreDbContext _context;

        public UserService(CoreDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<User>> Add(User user)
        {
            if (_context.Users.Count() == 0)
            {
                user.UserId = 1;
            }
            else
            {
                int maxUserId = _context.Users.Max(x => x.UserId);
                user.UserId = maxUserId + 1;
            }
            //username must be unique
            if (UsernameExists(user.Username))
            {
                return null;
            }
            if (EmailExists(user.Email))
            {
                return null;
            }

            PasswordConverter passCov = new PasswordConverter(user.Password);
            user.Password = passCov.GetHashedPassword();

            _context.Users.Add(user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.UserId))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return user;
        }


        public async Task<ActionResult<IEnumerable<User>>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            return user;
        }

        public async Task<ActionResult<User>> GetUser(string username, string password)
        {
            if (_context.Users.Any(e => e.Username == username))
            {
                var user = await _context.Users.Where(e => e.Username == username).FirstOrDefaultAsync();
                if (user.Password == password)
                {

                    return user;
                }
            }
            return null;
        }

        public async Task<ActionResult<User>> Update(int id, User user)
        {
            if (id != user.UserId)
            {
                return null;
            }

            if (ChangedUsername(user.UserId, user.Username))
            {
                if (UsernameExists(user.Username))
                {
                    return null;
                }
            }

            if (ChangedEmail(user.UserId, user.Email))
            {
                if (EmailExists(user.Email))
                {
                    return null;
                }
            }

            PasswordConverter passCov = new PasswordConverter(user.Password);
            user.Password = passCov.GetHashedPassword();

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return null; 
                }
                else
                {
                    throw;
                }
            }
            return user;
        }

        /////////////////
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }

        private bool UsernameExists(string username)
        {
            return _context.Users.Any(e => e.Username == username);
        }

        private bool EmailExists(string email)
        {
            return _context.Users.Any(e => e.Email == email);
        }

        private bool ChangedUsername(int id, string username)
        {
            if (UserExists(id))
            {
                return _context.Users.Any(e => (e.UserId == id) && (e.Username != username));
            }
            return false;
        }

        private bool ChangedEmail(int id, string email)
        {
            if (UserExists(id))
            {
                return _context.Users.Any(e => (e.UserId == id) && (e.Email != email));
            }
            return false;
        }
        #endregion
    }
}
