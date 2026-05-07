using Microsoft.EntityFrameworkCore;
using PatchSeller.DAL.Context;
using PatchSeller.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchSeller.DAL.Repository
{
    public class RoleRepository
    {
        private readonly PatchSellerDbContext _context;

        public RoleRepository()
        {
            _context = new PatchSellerDbContext();
        }

        public async Task<List<Role>> GetAll()
        {
            try
            {
                return await _context.Roles
                    .Where(x => x.Status == true)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Role> GetById(int id)
        {
            try
            {
                var role = await _context.Roles.FindAsync(id);
                if (role != null && role.Status == false)
                    return null;
                return role;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Role> GetByIdIncludeInactive(int id)
        {
            try
            {
                return await _context.Roles.FindAsync(id);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<Role> Create(Role role)
        {
            try
            {
                role.Status = true;
                var addedRole = _context.Roles.Add(role).Entity;
                await _context.SaveChangesAsync();
                return addedRole;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<Role> Update(Role role)
        {
            try
            {
                var currentRole = _context.Roles.Find(role.Id);
                if (currentRole == null)
                    return null;

                currentRole.Status = role.Status;
                currentRole.PagesPermission = role.PagesPermission;
                currentRole.RoleName = role.RoleName;
                _context.Roles.Update(currentRole);
                await _context.SaveChangesAsync();
                return currentRole;
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
                var role = await _context.Roles.FindAsync(id);
                if (role == null)
                    return false;

                role.Status = false;
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