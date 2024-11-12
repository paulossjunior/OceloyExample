using Service2.Model;

namespace Service2.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly List<Product> _products;

    public ProductRepository()
    {
        _products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Description = "High performance laptop", Price = 1299.99m, Stock = 10 },
            new Product { Id = 2, Name = "Smartphone", Description = "Latest model smartphone", Price = 799.99m, Stock = 15 },
            new Product { Id = 3, Name = "Headphones", Description = "Wireless noise-canceling headphones", Price = 199.99m, Stock = 20 }
        };
    }

    public IEnumerable<Product> GetAll()
    {
        return _products;
    }

    public Product GetById(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public void Add(Product product)
    {
        product.Id = _products.Max(p => p.Id) + 1;
        _products.Add(product);
    }

    public void Update(Product product)
    {
        var index = _products.FindIndex(p => p.Id == product.Id);
        if (index != -1)
        {
            _products[index] = product;
        }
    }

    public void Delete(int id)
    {
        var product = GetById(id);
        if (product != null)
        {
            _products.Remove(product);
        }
    }
}