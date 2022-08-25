using ETicaret.API.Persistence.Contexts;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.API.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly ETicaretAPIDbContext _context;

        public ReadRepository(ETicaretAPIDbContext context)
        {
            _context = context;
        }

        public Microsoft.EntityFrameworkCore.DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll()
        => Table;// Direkt tablosunun kendisini döndürüyoruz.

        public async Task<T> GetByIdAsync(string id)
        => await Table.FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));

        public async Task<T> GetSingleAsync(System.Linq.Expressions.Expression<Func<T, bool>> method)
        => await Table.FirstOrDefaultAsync(method);

        public IQueryable<T> GetWhere(System.Linq.Expressions.Expression<Func<T, bool>> method)
        => Table.Where(method);
    }
}
