using NUnit.Framework;
using SharpShop.Models.Base;
using SharpShop.Data.Repositories.ProductsRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace SharpShop.TestsApi.Repositories
{
    public class ProductsSQLRepositoryTests
    {
        private Mock<IProductsRepository> _mockRepository = null!;

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
            var products = _mockRepository.Object.GetAll("asc", "").Result;

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
            var mock = new Mock<IProductsRepository>();
            mock.Setup(r => r.GetAll("desc", It.IsAny<string>())).ReturnsAsync(new List<ProductModel>
            {
                new ProductModel(3, "Carrot", "Organic carrot", 2.49m, 200),
                new ProductModel(2, "Banana", "Yellow banana", 0.99m, 150),
                new ProductModel(1, "Apple", "Fresh apple", 1.99m, 100)
            });

            // Act
            var products = mock.Object.GetAll("desc", "").Result;

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

        [Test]
        public void GetAllProducts_InvalidSortOrder_ThrowsArgumentException()
        {
            // Arrange: Build a minimal configuration; it won't be used for invalid sort order
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    { "SQLPath", "Server=(local);Database=Dummy;Trusted_Connection=True;TrustServerCertificate=True" }
                })
                .Build();
            var logger = new Mock<ILogger<ProductsSQLRepository>>().Object;
            var realRepo = new ProductsSQLRepository(config, logger);

            Assert.ThrowsAsync<ArgumentException>(async () => await realRepo.GetAll("invalid", ""));
        }

        [Test]
        public void Get_ProductNotFound_ThrowsKeyNotFoundException()
        {
            // Arrange
            _mockRepository.Setup(r => r.Get(It.IsAny<int>()))
                .ThrowsAsync(new KeyNotFoundException("Product with id 999 not found"));

            // Act + Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _mockRepository.Object.Get(999));
        }

        [Test]
        public void Update_ProductNotFound_ThrowsKeyNotFoundException()
        {
            // Arrange
            var product = new ProductModel(999, "Ghost", "", 0m, 0);
            _mockRepository.Setup(r => r.Update(product))
                .ThrowsAsync(new KeyNotFoundException("Product with id 999 not found"));

            // Act + Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _mockRepository.Object.Update(product));
        }

        [Test]
        public void Delete_ProductNotFound_ThrowsKeyNotFoundException()
        {
            // Arrange
            _mockRepository.Setup(r => r.Delete(999))
                .ThrowsAsync(new KeyNotFoundException("Product with id 999 not found"));

            // Act + Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _mockRepository.Object.Delete(999));
        }

        [Test]
        public void Save_DbUpdateException_IsPropagated()
        {
            // Arrange
            var toSave = new ProductModel(0, "New", "Desc", 9.99m, 10);
            _mockRepository.Setup(r => r.Save(toSave))
                .ThrowsAsync(new DbUpdateException("Failed to save product."));

            // Act + Assert
            Assert.ThrowsAsync<DbUpdateException>(async () => await _mockRepository.Object.Save(toSave));
        }
    }
}
