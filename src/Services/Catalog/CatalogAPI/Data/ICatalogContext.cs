using CaltalogAPI.Entities;
using MongoDB.Driver;

namespace CaltalogAPI.Data;
public interface ICatalogContext
{
    IMongoCollection<Product> Products {get; }
}
