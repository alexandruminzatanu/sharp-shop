using SharpShop.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpShop.Data.Repositories.ProductRepository
{
    public interface IProductRepository
    {
        Task Delete(int productId);
        Task<ProductModel> Update(ProductModel product);
        Task<ProductModel> Save(ProductModel product);
        Task<ProductModel> Get(int productId);
    }
}
