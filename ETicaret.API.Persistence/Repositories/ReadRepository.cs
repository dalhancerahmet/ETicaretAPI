using ETicaret.API.Persistence.Contexts;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

        public IQueryable<T> GetAll(bool tracking = true)
        //=> Table; Direkt tablosunun kendisini döndürüyoruz.(Bu tracking olmayan kullanımı)
        {
            //Tracking = Yapılan requestleri(istekleri) takip edip veritabanını da takip eder. Böylelikle update insert ve delete işlemlerinden sonra güncel veriyi takip ettiği için döndürür.
            //Tracking işleminin olmasına gerek olmayan metotlar query metotlardır. Yani get işlemleridir.
            //Bundan dolayı aşağıdaki işlem ile Tracking'i devre dışı bırakarak verileri dönderiyoruz ki performans kaybı olmasın.
            var query= Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }

        public async Task<T> GetByIdAsync(string id, bool tracking = true)
        //=> await Table.FirstOrDefaultAsync(p => p.Id == Guid.Parse(id)); // 1.kullanım
        //=> await Table.FindAsync(Guid.Parse(id));2.yöntem(daha kullanışlı)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
        //=> await Table.FirstOrDefaultAsync(method);// Tracking olmadan kulanım
        {
            var query = Table.AsQueryable();
            if(!tracking)
                query= query.AsNoTracking();
            return await query.FirstOrDefaultAsync(method);
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
        //=> Table.Where(method); // Tracking olmadan kulanım
        {
            var query = Table.AsQueryable();
            if(!tracking)
                query= query.AsNoTracking();
            return query.Where(method);
        }
    }
}
