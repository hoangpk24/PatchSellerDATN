using Microsoft.EntityFrameworkCore;
using PatchSeller.DAL.Context;
using PatchSeller.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchSeller.DAL.Repository
{
    public class StaffPagePermissionRepository
    {
        private readonly PatchSellerDbContext _context;

        public StaffPagePermissionRepository()
        {
            _context = new PatchSellerDbContext();
        }

        public async Task<List<StaffPagePermission>> GetAll()
        {
            try
            {
                return await _context.StaffPagePermissions
                    .AsNoTracking()
                    .Select(x => new StaffPagePermission
                    {
                        Id = x.Id,
                        StaffId = x.StaffId,
                        PagePermissionId = x.PagePermissionId,
                        PermissionCode = x.PermissionCode
                    })
                    .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<StaffPagePermission>> GetAllByStaffId(int staffId)
        {
            try
            {
                return await _context.StaffPagePermissions
                    .AsNoTracking()
                    .Where(x => x.StaffId == staffId)
                    .Select(x => new StaffPagePermission
                    {
                        Id = x.Id,
                        StaffId = x.StaffId,
                        PagePermissionId = x.PagePermissionId,
                        PermissionCode = x.PermissionCode
                    })
                    .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<StaffPagePermission> GetById(int id)
        {
            try
            {
                return await _context.StaffPagePermissions
                    .AsNoTracking()
                    .Where(x => x.Id == id)
                    .Select(x => new StaffPagePermission
                    {
                        Id = x.Id,
                        StaffId = x.StaffId,
                        PagePermissionId = x.PagePermissionId,
                        PermissionCode = x.PermissionCode
                    })
                    .FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<StaffPagePermission> Create(int staffId, string pageCode, string permissionCode)
        {
            try
            {
                var pagePermission = await _context.PagePermissions
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.PageCode != null && x.PageCode.ToLower() == pageCode.ToLower());
                if (pagePermission == null)
                {
                    throw new InvalidOperationException("PAGE_CODE_NOT_FOUND");
                }

                var staffExists = await _context.Staffs.AnyAsync(x => x.StaffId == staffId);
                if (!staffExists)
                {
                    throw new InvalidOperationException("STAFF_NOT_FOUND");
                }

                var staffPagePermission = new StaffPagePermission
                {
                    StaffId = staffId,
                    PagePermissionId = pagePermission.Id,
                    PermissionCode = permissionCode
                };

                var duplicateData = await _context.StaffPagePermissions.AnyAsync(x =>
                    x.StaffId == staffPagePermission.StaffId
                    && x.PagePermissionId == staffPagePermission.PagePermissionId
                    && x.PermissionCode == staffPagePermission.PermissionCode);

                if (duplicateData)
                {
                    throw new InvalidOperationException("DUPLICATE_DATA");
                }

                var added = _context.StaffPagePermissions.Add(staffPagePermission).Entity;
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

        public async Task<StaffPagePermission> Update(int id, int staffId, string pageCode, string permissionCode)
        {
            try
            {
                var existingStaffPagePermission = await _context.StaffPagePermissions.FindAsync(id);
                if (existingStaffPagePermission == null)
                {
                    throw new InvalidOperationException("NOT_FOUND");
                }

                var pagePermission = await _context.PagePermissions
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.PageCode != null && x.PageCode.ToLower() == pageCode.ToLower());
                if (pagePermission == null)
                {
                    throw new InvalidOperationException("PAGE_CODE_NOT_FOUND");
                }

                var staffExists = await _context.Staffs.AnyAsync(x => x.StaffId == staffId);
                if (!staffExists)
                {
                    throw new InvalidOperationException("STAFF_NOT_FOUND");
                }

                var duplicateData = await _context.StaffPagePermissions.AnyAsync(x =>
                    x.Id != id
                    && x.StaffId == staffId
                    && x.PagePermissionId == pagePermission.Id
                    && x.PermissionCode == permissionCode);

                if (duplicateData)
                {
                    throw new InvalidOperationException("DUPLICATE_DATA");
                }

                existingStaffPagePermission.StaffId = staffId;
                existingStaffPagePermission.PagePermissionId = pagePermission.Id;
                existingStaffPagePermission.PermissionCode = permissionCode;

                _context.StaffPagePermissions.Update(existingStaffPagePermission);
                await _context.SaveChangesAsync();
                return await GetById(existingStaffPagePermission.Id);
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
                var staffPagePermission = await _context.StaffPagePermissions.FindAsync(id);
                if (staffPagePermission == null)
                {
                    throw new InvalidOperationException("NOT_FOUND");
                }

                _context.StaffPagePermissions.Remove(staffPagePermission);
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
