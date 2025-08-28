using NUnit.Framework;
using SharpShop.Models.Base;
using SharpShop.Data.Repositories.ProductsRepository;
using Microsoft.Extensions.Configuration;
using Moq;

namespace SharpShop.TestsApi.Repositories
{
    public class ProductsSQLRepositoryTests
    {
        private Mock<IProductsRepository> _mockRepository;
        private ProductsSQLRepository _repository;
        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IProductsRepository>();

            var mockProducts = new List<ProductModel>
            {
                new ProductModel(1, "Apple", "Fresh apple", 1.99m, 100),
                new ProductModel(2, "Banana", "Yellow banana", 0.99m, 150),
                new ProductModel(3, "Carrot", "Organic carrot", 2.49m, 200)
            };

            _mockRepository.Setup(repo => repo.GetAll(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(mockProducts);
        }


        [Test]
        public void GetAllProducts_ReturnsListOfProducts()
        {
            // Arrange
            var expectedCount = 3;

            // Act
            var products = _mockRepository.Object.GetAll().Result;

            // Assert

            Assert.That(products, Is.InstanceOf<IEnumerable<ProductModel>>());
            Assert.That(products.Count(), Is.EqualTo(expectedCount));
        }

        [Test]
        public void GetAllProducts_ReturnsListOfProducts_Asc()
        {
            // Arrange
            var expectedCount = 3;

            // Act
            var products = _repository.GetAll("asc", "").Result;

            // Assert
            
            Assert.That(products, Is.InstanceOf<IEnumerable<ProductModel>>());
            Assert.That(products.Count(), Is.EqualTo(expectedCount));
            var productList = products.ToList();
            for (int i = 1; i < productList.Count; i++)
            {
                Assert.That(string.Compare(productList[i - 1].Name, productList[i].Name, StringComparison.Ordinal), Is.LessThanOrEqualTo(0));
            }
        }

        [Test]
        public void GetAllProducts_ReturnsListOfProducts_Desc()
        {
            // Arrange
            var expectedCount = 3;

            // Act
            var products = _repository.GetAll("desc", "").Result;

            // Assert
            Assert.That(products, Is.Not.Null);
            Assert.That(products, Is.InstanceOf<IEnumerable<ProductModel>>());
            Assert.That(products.Count(), Is.EqualTo(expectedCount));
            var productList = products.ToList();
            for (int i = 1; i < productList.Count; i++)
            {
                Assert.That(string.Compare(productList[i - 1].Name, productList[i].Name, StringComparison.Ordinal), Is.GreaterThanOrEqualTo(0));
            }
        }
    }
}
