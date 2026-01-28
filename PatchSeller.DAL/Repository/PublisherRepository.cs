using Microsoft.EntityFrameworkCore;
using PatchSeller.DAL.Context;
using PatchSeller.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchSeller.DAL.Repository
{
    public class PublisherRepository
    {
        private readonly PatchSellerDbContext _context;

        public PublisherRepository()
        {
            _context = new PatchSellerDbContext();
        }

        public async Task<List<Publisher>> GetAll()
        {
            try
            {
                return await _context.Publishers
                    .Where(x => x.Delete != true)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Publisher> GetById(int id)
        {
            try
            {
                var entity = await _context.Publishers.FindAsync(id);
                if (entity != null && entity.Delete == true)
                    return null;
                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Publisher> Create(Publisher entity)
        {
            try
            {
                entity.Delete = false;
                var added = _context.Publishers.Add(entity).Entity;
                await _context.SaveChangesAsync();
                return added;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Publisher> Update(Publisher entity)
        {
            try
            {
                _context.Publishers.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
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
                var entity = await _context.Publishers.FindAsync(id);
                if (entity == null)
                    return false;

                entity.Delete = true;
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

