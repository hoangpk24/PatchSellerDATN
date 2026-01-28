using Microsoft.EntityFrameworkCore;
using PatchSeller.DAL.Context;
using PatchSeller.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatchSeller.DAL.Repository
{
    public class StaffRepository
    {
        private readonly PatchSellerDbContext _context;

        public StaffRepository()
        {
            _context = new PatchSellerDbContext();
        }

        public Task<Staff> GetByKeyAndPassword(string userName, string passwordHash)
        {
            var u = _context.Staffs.Where(x => x.UserName == userName && passwordHash == x.PasswordHash || x.Email == userName && passwordHash == x.PasswordHash).FirstOrDefaultAsync();
            return u;
        }

        public async Task<List<Staff>> GetAll()
        {
            try
            {
                return await _context.Staffs.ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Staff> GetById(int id)
        {
            try
            {
                return await _context.Staffs.FindAsync(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Staff> Create(Staff staff)
        {
            try
            {
                var added = _context.Staffs.Add(staff).Entity;
                await _context.SaveChangesAsync();
                return added;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Staff> Update(Staff staff)
        {
            try
            {
                _context.Staffs.Update(staff);
                await _context.SaveChangesAsync();
                return staff;
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
                var staff = await _context.Staffs.FindAsync(id);
                if (staff == null)
                    return false;

                _context.Staffs.Remove(staff);
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

