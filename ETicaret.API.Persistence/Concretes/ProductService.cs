using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.API.Persistence.Concretes
{
    public class ProductService : IProductService
    {
        public List<Product> GetProducts()
        => new() { new() { Id = Guid.NewGuid(), CreatedDate = DateTime.UtcNow, Name = "Elma", Price = 5, Stock = 10 } };

    }
}