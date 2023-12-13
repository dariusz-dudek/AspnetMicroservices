using CaltalogAPI.Data;
using CaltalogAPI.Entities;
using MongoDB.Driver;

namespace CaltalogAPI.Repositories;
public class ProductRepository : IProductRepository
{
    private readonly ICatalogContext _context;

    public ProductRepository(ICatalogContext context)
    {
        _context = context;
    }

    public async Task CreateProduct(Product product)
        => await _context
                    .Products
                    .InsertOneAsync(product);

    public async Task<bool> DeleteProduct(string id)
    {
        var deleteResult = await _context
                    .Products
                    .DeleteOneAsync(p => p.Id == id);
        
        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }

    public async Task<Product> GetProduct(string id)
        => await _context
                    .Products
                    .Find(p => p.Id == id)
                    .FirstOrDefaultAsync();

    public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        => await _context
                    .Products
                    .Find(p => p.Category == categoryName)
                    .ToListAsync();

    public async Task<IEnumerable<Product>> GetProductByName(string name)
        => await _context
                    .Products
                    .Find(p => p.Name == name)
                    .ToListAsync();

    public async Task<IEnumerable<Product>> GetProducts()
        => await _context
                    .Products
                    .Find(p => true)
                    .ToListAsync();

    public async Task<bool> UpdateProduct(Product product)
    {
        var updateResult = await _context
                                    .Products
                                    .ReplaceOneAsync(p => p.Id == product.Id, product);
        
        return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
    }
}
