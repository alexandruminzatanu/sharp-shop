using SharpShop.Models.Base;
using SharpShop.Data.Repositories.ProductRepository;

namespace SharpShop.Business.Product
{
    internal class ProductService: IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository) {
        
            _productRepository = productRepository;
        }

        public Task<ProductModel> Get(int productId)
        {
            return _productRepository.Get(productId);
        }
        public Task<ProductModel> Save(ProductModel product)
        {
           return _productRepository.Save(product);
        }
        public Task<ProductModel> Update(ProductModel product)
        {
            return _productRepository.Update(product);
        }
        public Task Delete(int productId)
        {
            return _productRepository.Delete(productId);
        }
    }
}
