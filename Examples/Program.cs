
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;
{ 
    Console.WriteLine("Hello, World!");
    NorthwindContext context = new();
    #region Add
    //Product product1 = new() { ProductName="Ürün 1" };
    //Product product2 = new() { ProductName = "Ürün 2" };
    //Product product3 = new() { ProductName = "Ürün 3" };
    //Product product4 = new() { ProductName = "Ürün 4" };
    //List<Product> products = new() { product1, product2, product3, product4 };
    //await context.Products.AddRangeAsync(products);
    //await context.Products.AddAsync(product1);
    #endregion
    #region Delete
    //IEnumerable<Product> products= await context.Products.Where(p=>p.ProductId>=105).ToListAsync();
    ////Console.WriteLine(products);
    //context.Products.RemoveRange(products);
    //context.SaveChanges();
    #endregion
    #region Update
    ////Product product =await context.Products.FirstOrDefaultAsync(p => p.ProductId == 77);
    ////product.ProductName = "Original Frankfurter";
    //Product product = new() {ProductId=77,ProductName= "Original" };
    //context.Products.Update(product);
    //context.SaveChanges();
    #endregion
    #region List
    //List<Product> products= await context.Products.ToListAsync();
    //Console.WriteLine(products);
    //IEnumerable<Product> products=await context.Products.Where(p => p.ProductName.Contains("as")).ToListAsync();
    //foreach (var product in products)
    //{
    //    Console.WriteLine(product.ProductName);
    //}
    //IQueryable<Product> products= context.Products.Where(p => p.ProductName.StartsWith("A"));
    // foreach (var product in products)
    // {
    //     Console.WriteLine(product.ProductName);
    // }
    //IQueryable<Product> products = from product in context.Products
    //                               where product.ProductId==77
    //                               select product;
    //foreach (var product in products)
    //{
    //    Console.WriteLine(product.ProductName);
    //}
    #endregion
    #region OrderBy
    //var products= await context.Products.OrderByDescending(p => p.ProductId).ToListAsync();
    //foreach (var product in products)
    //{
    //    Console.WriteLine(product.ProductId);
    //}
    //var products = from product in context.Products
    //               orderby product.ProductId descending
    //               select product;
    //foreach (var product in products)
    //{

    //    Console.WriteLine(product.ProductId);
    //}
    #endregion
    #region SingleAsync and SingleOrDefault 
    //sadece tek bir verinin gelmesi isteniyorsa Single veya singleOrDefault kullanılır
    //var product= await context.Products.SingleAsync(p => p.ProductId == 77);
    //Console.WriteLine(product.ProductName);
    #endregion


}

class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
}
class NorthwindContext:DbContext
{
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=AHMET\\SQLEXPRESS;Database=NORTHWND; Trusted_Connection=True;");
    }
}
