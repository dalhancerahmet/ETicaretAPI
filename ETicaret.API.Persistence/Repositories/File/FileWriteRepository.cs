using ETicaret.API.Persistence.Contexts;
using ETicaretAPI.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F = ETicaretAPI.Domain.Entities.File;

namespace ETicaret.API.Persistence.Repositories.File
{
    internal class FileWriteRepository : WriteRepository<F>, IFileWriteRepository
    {
        public FileWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
