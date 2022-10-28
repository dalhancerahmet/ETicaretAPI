using ETicaretAPI.Application.Abstractions.Hubs;
using ETicaretAPI.Application.Abstractions.Storage;
using ETicaretAPI.Application.Consts;
using ETicaretAPI.Application.CustomAttributes;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.ViewModels;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO.Pipelines;
using ETicaretAPI.Application.Enums;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes ="Admin")]
    public class ProductsController : ControllerBase
    {
        IProductWriteRepository _productWriteRepository;
        IProductReadRepository _productReadRepository;
        IWebHostEnvironment _webHostEnvironment;
        IStorageService _storageService;
        IProductImageFileWriteRepository _productImageFileWriteRepository;
        IProductHubService _productHubService;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IWebHostEnvironment webHostEnvironment, IStorageService storageService, IProductImageFileWriteRepository productImageFileWriteRepository, IProductHubService productHubService)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _webHostEnvironment = webHostEnvironment;
            _storageService = storageService;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _productHubService = productHubService;
        }
        [HttpPost("create")]
        [AuthorizeDefinition(Menu =AuthorizeDefinitionConsts.Baskets, ActionType = ActionType.Writing,Definition ="Product Create")]
        public async Task<IActionResult> Create(VM_Create_Product model)
        {
            await _productWriteRepository.AddAsync(new()
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock,
            });
            await _productHubService.ProductAddedMessageAsync($"{model.Name} isminde ürün eklenmiştir.");
            await _productWriteRepository.SaveAysnc();
            return Ok(true);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var product = await _productReadRepository.GetByIdAsync(id, false);
            return Ok(product);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = _productReadRepository.GetAll().Select(p => new
            {
                p.Name,
                p.Price,
                p.Stock,

            });
            return Ok(result);
        }
        [HttpPost("upload")]
        public async Task<IActionResult> Upload()
        {
            //dikkat!! -- > dosya yükleme esnasında alınan hata giderilecek, veritabanına yazmıyor.
            var datas = await _storageService.UploadAsync("files", Request.Form.Files);

            await _productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProductImageFile()
            {
                FileName = d.fileName,
                Path = d.pathOrContainerName,
                Storage = _storageService.StorageName

            }).ToList());
            await _productWriteRepository.SaveAysnc();
            return Ok(datas);
            //    Random rand = new Random();//random sayılar oluşturan sınıf
            //    //_webHostEnvironment.WebRootPath ile wwwroot path'ine erişip altındaki resource/product-images pathine ulaşıyoruz.
            //    string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "resource/product-images");

            //    // yukarıdaki resource/product-images path'i var mı yok mu kontrolünü yapıyoruz.
            //    if(!Directory.Exists(uploadPath))
            //        //yoksa oluşturuyoruz.
            //        Directory.CreateDirectory(uploadPath);

            //    //Request.Form.Files ile clientdan gelen dosyaları geziyoruz ve her birine file takma adını veriyoruz.
            //    foreach (var file in Request.Form.Files)
            //    {
            //        //path.combine ile uploadpath i ve geri kalanını tek bir dize şeklinde birleştiriyoruz.
            //       //wwwroot/resource/product-images/randomsayi+gelendosyaninadı
            //        string fullPath = Path.Combine(uploadPath, $"{rand.Next()},{Path.GetExtension(file.FileName)}");

            //        //fileStream sınıfı ile belirlenen dizeye/yola create,write yetkileri veriyoruz. eklenecek dosyanın boyutunu belirtiyoruz.
            //        using FileStream fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);

            //        //gelen dosyayı(file(takma ad verdiğimiz)) fileStream ile belirtilen dizeye copyalıyoruz(CopyToAsync ile )
            //        await file.CopyToAsync(fileStream);

            //        //Son olarak FileStream'i kapatıyoruz.
            //        await fileStream.FlushAsync();
            //    }
            //    return Ok();
        }
    }
}



