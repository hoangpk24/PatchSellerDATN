using Microsoft.EntityFrameworkCore;
using PatchSeller.DAL.Context;
using PatchSeller.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchSeller.DAL.Repository
{
    public class UserRepository
    {
        private readonly PatchSellerDbContext _context;

        public UserRepository()
        {
            _context = new PatchSellerDbContext();
        }

        public  Task<User> GetByKeyAndPassword(string userName, string passwordHash)
        {
               var u =  _context.Users.Where(x=>x.UserName == userName && passwordHash == x.PasswordHash || x.Email == userName && passwordHash == x.PasswordHash).FirstOrDefaultAsync();
            return u;
        }

        public async Task<List<User>> GetAll(string? keyword)
        {
            try
            {
                if(!string.IsNullOrEmpty(keyword))
                {
                    return await _context.Users
                        .Where(x => (x.FullName.Contains(keyword) || x.Email.Contains(keyword) || x.PhoneNumber.Contains(keyword)) && x.Delete != true)
                        .ToListAsync();
                }
                return await _context.Users
                    .Where(x => x.Delete != true)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<User> GetById(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user != null && user.Delete == true)
                    return null;
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<User> FindUserExistByKeyWord(string key)
        {
            try
            {
                var c = _context.Users.FirstOrDefault(x => (x.Email == key || x.PhoneNumber == key) && x.Delete != true);

                if (c == null)
                    return null;
                return c;


            }
            catch
            {
                return null;
            }
        }

        public async Task<User> FindUserByEmailAndPhoneAndUserName(string email, string phoneNumber, string username)
        {
            try
            {
                var customer = await _context.Users.FirstOrDefaultAsync(x => (x.Email == email || x.PhoneNumber == phoneNumber || x.UserName == username) && x.Delete != true);
                return customer;
            }
            catch
            {
                return null;
            }
        }

        public async Task<User> Create(User user)
        {
            try
            {
                var exitingUser = await _context.Users
                    .AnyAsync(u => u.UserName.ToLower() == user.UserName.ToLower() || u.Email.ToLower() == user.Email.ToLower());

                if (exitingUser)
                {
                    throw new InvalidOperationException("DUPLICATE_NAME_OR_EMAIL");
                }

                user.Delete = false;
                user.CreatedAt = DateTime.Now;
                var addedUser = _context.Users.Add(user).Entity;
                await _context.SaveChangesAsync();
                return addedUser;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<User> Update(User user)
        {
            try
            {
                var exitingUser = await _context.Users.FindAsync(user.UserId);

                if (exitingUser == null || (exitingUser != null && exitingUser.Delete == true))
                {
                    throw new InvalidOperationException("NOT_FOUND");
                }

                var duplicateUser = await _context.Users
                    .AnyAsync(u => (u.UserName.ToLower() == user.UserName.ToLower() || u.Email.ToLower() == user.Email.ToLower()) && u.UserId != user.UserId);

                if (duplicateUser)
                {
                    throw new InvalidOperationException("DUPLICATE_NAME_OR_EMAIL");
                }

                exitingUser.FullName = user.FullName;
                exitingUser.UserName = user.UserName;
                exitingUser.Email = user.Email;
                exitingUser.PhoneNumber = user.PhoneNumber;
                exitingUser.PasswordHash = user.PasswordHash;
                exitingUser.RewardPoint = user.RewardPoint;
                exitingUser.Status = user.Status;
                exitingUser.LastLogin = user.LastLogin;
                exitingUser.RankId = user.RankId;

                _context.Users.Update(exitingUser);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);

                if (user == null || (user != null && user.Delete == true)) { 
                    throw new InvalidOperationException("NOT_FOUND");
                }

                user.Delete = true;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

