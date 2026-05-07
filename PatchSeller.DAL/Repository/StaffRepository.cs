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

        public async Task<List<Staff>> GetAll(string? keyword)
        {
            try
            {
                if(keyword != null)
                {
                    return await _context.Staffs
                        .Where(s => s.FullName.Contains(keyword) || s.Email.Contains(keyword) || s.PhoneNumber.Contains(keyword))
                        .ToListAsync();
                }
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

        public async Task<List<Staff>> GetAllByRoleId(int roleId)
        {
            try
            {
                return await _context.Staffs
                    .Where(x => x.RoleId == roleId)
                    .ToListAsync();
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
                var exitingStaff = await _context.Staffs
                    .AnyAsync(s => s.UserName.ToLower() == staff.UserName.ToLower() || s.Email.ToLower() == staff.Email.ToLower());

                if (exitingStaff)
                {
                    throw new InvalidOperationException("DUPLICATE_NAME_OR_EMAIL");
                }

                staff.CreatedAt = DateTime.Now;
                var added = _context.Staffs.Add(staff).Entity;
                await _context.SaveChangesAsync();
                return added;
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

        public async Task<Staff> Update(Staff staff)
        {
            try
            {
                var exitingStaff = await _context.Staffs.FindAsync(staff.StaffId);

                if (exitingStaff == null)
                {
                    throw new InvalidOperationException("NOT_FOUND");
                }

                var duplicateStaff = await _context.Staffs
                    .AnyAsync(s => (s.UserName.ToLower() == staff.UserName.ToLower() || s.Email.ToLower() == staff.Email.ToLower()) && s.StaffId != staff.StaffId);

                if (duplicateStaff)
                {
                    throw new InvalidOperationException("DUPLICATE_NAME_OR_EMAIL");
                }

                exitingStaff.FullName = staff.FullName;
                exitingStaff.UserName = staff.UserName;
                exitingStaff.PasswordHash = staff.PasswordHash;
                exitingStaff.Email = staff.Email;
                exitingStaff.PhoneNumber = staff.PhoneNumber;
                exitingStaff.Role = staff.Role;
                exitingStaff.RoleId = staff.RoleId;

                _context.Staffs.Update(exitingStaff);
                await _context.SaveChangesAsync();
                return staff;
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
                var staff = await _context.Staffs.FindAsync(id);

                if (staff == null)
                    throw new InvalidOperationException("NOT_FOUND");

                _context.Staffs.Remove(staff);
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

