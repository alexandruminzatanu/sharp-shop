using SharpShop.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpShop.Data.Repositories.ProductRepository
{
    internal interface IProductReposity
    {
        Task Delete(int productId);
        Task<Product> Update(Product product);
        Task<Product> Save(Product product);
        Task<Product> Get(int productId);
    }
}
