using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = ETicaretAPI.Domain.Entities.File;

namespace ETicaret.API.Persistence.Contexts
{
    public class ETicaretAPIDbContext : DbContext
    {
        public ETicaretAPIDbContext(DbContextOptions options) : base(options)
        {}
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }

        //saveChangesAsync sınıfı ile midleware uyguluyoruz. Swithc case ile Ekleme mi, yoksa güncelleme işlemi mi 
        // yapıldığını tespit edip, ilgili sınıftaki updateDate ve CreatedDate propertilerine o anki zamanı işliyoruz.
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //changeTracker ile yapılan işlemi yakalayıp foreach ile dönüp switc case ile duruma göre ilgili propertileri eşliyoruz.
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Modified=> data.Entity.UpdatedDate=DateTime.UtcNow,
                    EntityState.Added=>data.Entity.CreatedDate=DateTime.UtcNow,
                };
                    
            }

            //yapılan işlemi savechanges(değişikliği kaydet) yapıyoruz.
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
