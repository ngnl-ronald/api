using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public interface IAuthUserService
    {
		AuthUser Authenticate(string username, string password);

        Task<IEnumerable<AuthUser>> GetAll();
        Task<AuthUser> GetById(int id);
        Task<AuthUser> Create(AuthUser user, string password);
        void Update(AuthUser user, string password = null);
        void Delete(int id);
    }

    public class AuthUserService : IAuthUserService
    {
        private DataContext _context;

        public AuthUserService(DataContext context)
        {
            _context = context;
        }

        public AuthUser Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;
            try
            {
                var user = _context.AuthUsers.SingleOrDefault(x => x.Username == username);
                // check if username exists
                if (user == null)
                    return null;

                // check if password is correct
                if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                    return null;

                // authentication successful
                return user;

            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }



        }

        public async Task<IEnumerable<AuthUser>> GetAll()
        {
            return await _context.AuthUsers.ToListAsync();
        }

        public async Task<AuthUser> GetById(int id)
        {
            return await _context.AuthUsers.FindAsync(id);
        }

        public async Task<AuthUser> Create(AuthUser user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_context.AuthUsers.Any(x => x.Username == user.Username))
                throw new AppException("Username '" + user.Username + "' is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.AuthUsers.AddAsync(user);
            _context.SaveChanges();

            return user;
        }

        public void Update(AuthUser userParam, string password = null)
        {
            var user = _context.AuthUsers.Find(userParam.Id);

            if (user == null)
                throw new AppException("User not found");

            if (userParam.Username != user.Username)
            {
                // username has changed so check if the new username is already taken
                if (_context.AuthUsers.Any(x => x.Username == userParam.Username))
                    throw new AppException("Username " + userParam.Username + " is already taken");
            }

            // update user properties
            //user.GivenName = userParam.GivenName;
            //user.Surname = userParam.Surname;
            user.Username = userParam.Username;

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _context.AuthUsers.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.AuthUsers.Find(id);
            if (user != null)
            {
                _context.AuthUsers.Remove(user);
                _context.SaveChanges();
            }
        }

        // private helper methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}