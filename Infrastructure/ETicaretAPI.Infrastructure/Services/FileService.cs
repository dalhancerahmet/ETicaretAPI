using ETicaretAPI.Application.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services
{
    public class FileService : IFileService
    {
        IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
          await using FileStream fileStream = new FileStream(path, FileMode.Create,FileAccess.Write,FileShare.None,1024*1024,useAsync:false);
          await fileStream.CopyToAsync(fileStream);
          await fileStream.FlushAsync();
            return true;
        }

        public Task<string> FileRenameAsync(string fileName)
        {
            throw new NotImplementedException();
        }

        public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
        {
            List<(string fileName, string path)> datas = new();
            List<bool> results = new();

            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

            if(!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            foreach (IFormFile file in files)
            {
                 string fileNewName=await FileRenameAsync(file.FileName);
                bool result=await CopyFileAsync($"{uploadPath}\\{fileNewName}",file);
                datas.Add((fileNewName, $"{uploadPath}\\{fileNewName}"));
            }
            if (results.TrueForAll(r => r.Equals(true)))
                return datas;

            return null;
        }
    }
}
