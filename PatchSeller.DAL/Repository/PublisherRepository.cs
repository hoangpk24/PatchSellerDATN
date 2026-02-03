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

        public async Task<List<Publisher>> GetAll(string? keyword)
        {
            try
            {
                if(keyword != null)
                {
                    return await _context.Publishers
                        .Where(x => x.Delete != true && x.Name.ToLower().Contains(keyword.ToLower()))
                        .ToListAsync();
                }

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
                var duplicateName = await _context.Publishers.AnyAsync(c => c.Name.ToLower() == entity.Name.ToLower() && c.Delete != true);

                if (duplicateName)
                {
                    throw new InvalidOperationException("DUPLICATE_NAME");
                }

                entity.Delete = false;
                entity.CreatedAt = DateTime.Now;
                var added = _context.Publishers.Add(entity).Entity;
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

        public async Task<Publisher> Update(Publisher entity)
        {
            try
            {
                var existingPublisher = await _context.Publishers.FindAsync(entity.PublisherId);

                if (existingPublisher == null || (existingPublisher != null && existingPublisher.Delete == true))
                {
                    throw new InvalidOperationException("NOT_FOUND");
                }

                var duplicateName = await _context.Publishers.AnyAsync(c => c.PublisherId != entity.PublisherId && c.Name.ToLower() == entity.Name.ToLower() && c.Delete != true);

                if (duplicateName)
                {
                    throw new InvalidOperationException("DUPLICATE_NAME");
                }

                existingPublisher.Name = entity.Name;
                existingPublisher.Description = entity.Description;
                existingPublisher.Status = entity.Status;
                existingPublisher.Delete = entity.Delete;

                _context.Publishers.Update(existingPublisher);
                await _context.SaveChangesAsync();
                return entity;
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
                var entity = await _context.Publishers.FindAsync(id);

                if (entity == null || (entity != null && entity.Delete == true))
                {
                    throw new InvalidOperationException("NOT_FOUND");
                }

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

