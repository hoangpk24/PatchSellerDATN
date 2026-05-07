using Microsoft.EntityFrameworkCore;
using PatchSeller.DAL.Context;
using PatchSeller.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchSeller.DAL.Repository
{
    public class PagePermissionRepository
    {
        private readonly PatchSellerDbContext _context;

        public PagePermissionRepository()
        {
            _context = new PatchSellerDbContext();
        }

        public async Task<List<PagePermission>> GetAll(string? keyword)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    return await _context.PagePermissions
                        .Where(x => (x.PageCode != null && x.PageCode.Contains(keyword))
                            || (x.PageRoute != null && x.PageRoute.Contains(keyword)))
                        .ToListAsync();
                }

                return await _context.PagePermissions.ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PagePermission> GetById(int id)
        {
            try
            {
                return await _context.PagePermissions.FindAsync(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PagePermission> Create(PagePermission pagePermission)
        {
            try
            {
                pagePermission.StaffPagePermissions = null;

                var duplicateCode = !string.IsNullOrWhiteSpace(pagePermission.PageCode)
                    && await _context.PagePermissions.AnyAsync(x => x.PageCode != null && x.PageCode.ToLower() == pagePermission.PageCode.ToLower());

                if (duplicateCode)
                {
                    throw new InvalidOperationException("DUPLICATE_CODE");
                }

                var added = _context.PagePermissions.Add(pagePermission).Entity;
                await _context.SaveChangesAsync();
                return await GetById(added.Id);
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

        public async Task<PagePermission> Update(PagePermission pagePermission)
        {
            try
            {
                pagePermission.StaffPagePermissions = null;

                var existingPagePermission = await _context.PagePermissions.FindAsync(pagePermission.Id);
                if (existingPagePermission == null)
                {
                    throw new InvalidOperationException("NOT_FOUND");
                }

                var duplicateCode = !string.IsNullOrWhiteSpace(pagePermission.PageCode)
                    && await _context.PagePermissions.AnyAsync(x =>
                        x.Id != pagePermission.Id
                        && x.PageCode != null
                        && x.PageCode.ToLower() == pagePermission.PageCode.ToLower());

                if (duplicateCode)
                {
                    throw new InvalidOperationException("DUPLICATE_CODE");
                }

                existingPagePermission.PageCode = pagePermission.PageCode;
                existingPagePermission.PageRoute = pagePermission.PageRoute;
                existingPagePermission.AvailablePermissions = pagePermission.AvailablePermissions;
                existingPagePermission.DefaultPermissions = pagePermission.DefaultPermissions;

                _context.PagePermissions.Update(existingPagePermission);
                await _context.SaveChangesAsync();
                return await GetById(existingPagePermission.Id);
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
                var pagePermission = await _context.PagePermissions.FindAsync(id);
                if (pagePermission == null)
                {
                    throw new InvalidOperationException("NOT_FOUND");
                }

                _context.PagePermissions.Remove(pagePermission);
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
