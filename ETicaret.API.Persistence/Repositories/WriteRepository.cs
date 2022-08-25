using ETicaret.API.Persistence.Contexts;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.API.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly ETicaretAPIDbContext _context;

        public WriteRepository(ETicaretAPIDbContext context)
        {
            _context = context;
        }
        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> AddAsync(T model)
        {
           EntityEntry<T> entityEntry= await Table.AddAsync(model);
            return entityEntry.State==EntityState.Added;
            _context.SaveChanges();
        }
        public async Task<bool> AddRangeAsync(List<T> datas)
        {
           await Table.AddRangeAsync(datas);
            _context.SaveChanges();
            return true;
        }
        public async Task<bool> Remove(string id)
        {
           T model= await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
           return Remove(model);
        }
        public bool Remove(T model)
        {
           EntityEntry<T> entityEntry=  Table.Remove(model);
           return entityEntry.State==EntityState.Deleted;
            _context.SaveChanges();

        }

        public bool RemoveRange(List<T> datas)
        {
            Table.RemoveRange(datas);
            _context.SaveChanges();
            return true;
        }

        public bool Update(T model)
        {
            Table.Update(model);
            EntityEntry<T> entityEntry = Table.Update(model);
            return entityEntry.State == EntityState.Modified;
             _context.SaveChanges();

        }
    }
}
