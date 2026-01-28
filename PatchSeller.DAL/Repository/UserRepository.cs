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

        public async Task<List<User>> GetAll()
        {
            try
            {
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

        public async Task<User> Create(User user)
        {
            try
            {
                user.Delete = false;
                var addedUser = _context.Users.Add(user).Entity;
                await _context.SaveChangesAsync();
                return addedUser;
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
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return user;
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
                if (user == null)
                    return false;

                user.Delete = true;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

