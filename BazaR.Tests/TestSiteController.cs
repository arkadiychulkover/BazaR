using BazaR.Controllers;
using BazaR.Data;
using BazaR.Interfaces;
using BazaR.Models;
using BazaR.Models.BazaR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Security.Claims;
using Xunit;

namespace BazaR.Tests.Controllers
{
    public class SiteControllerTests : IDisposable
    {
        private readonly Mock<IUserDb> _mockUserDb;
        private readonly Mock<IItemRepository> _mockItemRepo;
        private readonly AppDbContext _dbContext;
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly Mock<ILogDb> _mockLogDb;
        private readonly SiteController _controller;
        private readonly List<Category> _testCategories;
        private readonly List<Item> _testItems;
        private readonly List<User> _testUsers;
        private readonly List<CartItem> _testCartItems;
        private readonly List<WishlistItem> _testWishlistItems;

        public SiteControllerTests()
        {
            _mockUserDb = new Mock<IUserDb>();
            _mockItemRepo = new Mock<IItemRepository>();
            _mockLogDb = new Mock<ILogDb>();

            // Используем InMemory database вместо мокинга DbContext
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new AppDbContext(options);

            // Setup UserManager mock
            var userStoreMock = new Mock<IUserStore<User>>();
            _mockUserManager = new Mock<UserManager<User>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            _testCategories = GetTestCategories();
            _testItems = GetTestItems();
            _testUsers = GetTestUsers();
            _testCartItems = GetTestCartItems();
            _testWishlistItems = GetTestWishlistItems();

            // Добавляем тестовые данные в InMemory database
            _dbContext.Categories.AddRange(_testCategories);
            _dbContext.Items.AddRange(_testItems);
            _dbContext.SaveChanges();

            SetupMocks();

            _controller = new SiteController(
                _mockUserDb.Object,
                _mockItemRepo.Object,
                _dbContext,
                _mockUserManager.Object,
                _mockLogDb.Object);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        private void SetupMocks()
        {
            // Настраиваем моки для корзины - возвращаем только 2 товара для пользователя 1
            _mockUserDb.Setup(x => x.GetCartItems(It.IsAny<int>()))
                .Returns<int>(userId =>
                {
                    var cartItemIds = _testCartItems
                        .Where(ci => ci.UserId == userId)
                        .Select(ci => ci.ItemId)
                        .ToList();
                    return _testItems
                        .Where(i => cartItemIds.Contains(i.Id))
                        .AsQueryable();
                });

            _mockUserDb.Setup(x => x.GetCartItemsWithQuantity(It.IsAny<int>()))
                .Returns<int>(userId =>
                    _testCartItems
                        .Where(ci => ci.UserId == userId)
                        .Select(ci => new CartItem
                        {
                            ItemId = ci.ItemId,
                            Item = _testItems.FirstOrDefault(i => i.Id == ci.ItemId),
                            Quantity = ci.Quantity
                        })
                        .ToList());

            _mockUserDb.Setup(x => x.GetWishList(It.IsAny<int>()))
                .Returns<int>(userId =>
                {
                    var wishlistItemIds = _testWishlistItems
                        .Where(wi => wi.UserId == userId)
                        .Select(wi => wi.ItemId)
                        .ToList();
                    return _testItems
                        .Where(i => wishlistItemIds.Contains(i.Id))
                        .AsQueryable();
                });

            _mockUserDb.Setup(x => x.AddToCart(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);

            _mockUserDb.Setup(x => x.RemoveFromCart(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);

            _mockUserDb.Setup(x => x.SetCartQuantity(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);

            _mockUserDb.Setup(x => x.ClearCart(It.IsAny<int>()))
                .Returns(true);

            _mockUserDb.Setup(x => x.AddToWishList(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);

            _mockUserDb.Setup(x => x.RemoveFromWishList(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);

            _mockUserDb.Setup(x => x.CreateOrder(It.IsAny<int>(), It.IsAny<Order>()))
                .Returns<int, Order>((userId, order) =>
                {
                    order.Id = 1;
                    order.Number = "ORDER-001";
                    return true;
                });

            _mockUserDb.Setup(x => x.GetOrderById(It.IsAny<int>()))
                .Returns<int>(id => new Order
                {
                    Id = id,
                    Number = $"ORDER-{id}",
                    UserId = 1,
                    TotalAmount = 1000,
                    Status = "New"
                });

            _mockUserDb.Setup(x => x.GetUserOrders(It.IsAny<int>()))
                .Returns<int>(userId => new List<Order>
                {
                    new Order { Id = 1, Number = "ORDER-001", UserId = userId, TotalAmount = 1000 },
                    new Order { Id = 2, Number = "ORDER-002", UserId = userId, TotalAmount = 500 }
                }.AsQueryable());

            _mockUserDb.Setup(x => x.CancelOrder(It.IsAny<int>()))
                .Returns(true);

            _mockItemRepo.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns<int>(id => _testItems.FirstOrDefault(i => i.Id == id));

            _mockItemRepo.Setup(x => x.GetAll())
                .Returns(_testItems);

            _mockItemRepo.Setup(x => x.GetByCategory(It.IsAny<int>()))
                .Returns<int>(categoryId => _testItems.Where(i => i.CategoryId == categoryId).ToList());

            _mockItemRepo.Setup(x => x.GetByBrand(It.IsAny<int>()))
                .Returns<int>(brandId => _testItems.Where(i => i.BrandId == brandId).ToList());

            _mockItemRepo.Setup(x => x.Search(It.IsAny<string>()))
                .Returns<string>(query => _testItems.Where(i => i.Name.Contains(query)).ToList());

            _mockItemRepo.Setup(x => x.GetCategoryById(It.IsAny<int>()))
                .Returns<int>(id => _testCategories.FirstOrDefault(c => c.Id == id));

            _mockItemRepo.Setup(x => x.Create(It.IsAny<Item>()))
                .Returns<Item>(item =>
                {
                    item.Id = _testItems.Count + 1;
                    _testItems.Add(item);
                    return true;
                });

            _mockItemRepo.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Item>()))
                .Returns<int, Item>((id, item) =>
                {
                    var existing = _testItems.FirstOrDefault(i => i.Id == id);
                    if (existing != null)
                    {
                        existing.Name = item.Name;
                        existing.Price = item.Price;
                        return existing;
                    }
                    return null;
                });

            _mockItemRepo.Setup(x => x.Delete(It.IsAny<int>()))
                .Returns<int>(id =>
                {
                    var item = _testItems.FirstOrDefault(i => i.Id == id);
                    if (item != null)
                    {
                        _testItems.Remove(item);
                        return true;
                    }
                    return false;
                });

            _mockItemRepo.Setup(x => x.AddReview(It.IsAny<int>(), It.IsAny<Review>()))
                .Returns(true);

            _mockItemRepo.Setup(x => x.RemoveReview(It.IsAny<int>()))
                .Returns(true);

            _mockLogDb.Setup(x => x.Log(It.IsAny<string>()))
                .Verifiable();

            _mockLogDb.Setup(x => x.LogPageVisitAsync(
                    It.IsAny<int>(),
                    It.IsAny<UserAction>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<int?>(),
                    It.IsAny<int?>(),
                    It.IsAny<SearchFilters>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var currentUser = _testUsers[0];
            _mockUserManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(currentUser);

            _mockUserManager.Setup(x => x.UpdateAsync(It.IsAny<User>()))
                .ReturnsAsync(IdentityResult.Success);
        }

        private List<Category> GetTestCategories()
        {
            return new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Name = "Electronics",
                    ParentCategoryId = null,
                    DisplayOrder = 1,
                    Filters = new List<CategoryFilter>
                    {
                        new CategoryFilter { Id = 1, Key = "brand", DisplayName = "Brand", ValueType = FilterValueType.String }
                    }
                },
                new Category
                {
                    Id = 2,
                    Name = "Laptops",
                    ParentCategoryId = 1,
                    DisplayOrder = 1,
                    CategoryBrands = new List<CategoryBrand>
                    {
                        new CategoryBrand { CategoryId = 2, BrandId = 1, Brand = new Brand { Id = 1, Name = "Apple" } },
                        new CategoryBrand { CategoryId = 2, BrandId = 2, Brand = new Brand { Id = 2, Name = "Dell" } }
                    }
                },
                new Category
                {
                    Id = 3,
                    Name = "Smartphones",
                    ParentCategoryId = 1,
                    DisplayOrder = 2
                }
            };
        }

        private List<Item> GetTestItems()
        {
            return new List<Item>
            {
                new Item
                {
                    Id = 1,
                    Name = "MacBook Pro",
                    Desc = "Powerful laptop with M1 chip",
                    Price = 2000,
                    CategoryId = 2,
                    BrandId = 1,
                    UserId = 1,
                    IsAvailable = true,
                    Garantia = 12,
                    ImageUrl = "/images/test.jpg",
                    Reviews = new List<Review>
                    {
                        new Review { Id = 1, Rating = 5, Comment = "Great!" },
                        new Review { Id = 2, Rating = 4, Comment = "Good" }
                    },
                    Characteristics = new List<ItemCharacteristic>
                    {
                        new ItemCharacteristic { Id = 1, Key = "processor", Value = "M1" },
                        new ItemCharacteristic { Id = 2, Key = "ram", Value = "16GB" }
                    },
                    Colors = new List<ItemColor>
                    {
                        new ItemColor { Id = 1, Color = "Silver" }
                    }
                },
                new Item
                {
                    Id = 2,
                    Name = "Dell XPS",
                    Desc = "High-end Windows laptop",
                    Price = 1500,
                    CategoryId = 2,
                    BrandId = 2,
                    UserId = 1,
                    IsAvailable = true,
                    Garantia = 24,
                    Reviews = new List<Review>(),
                    Characteristics = new List<ItemCharacteristic>
                    {
                        new ItemCharacteristic { Id = 3, Key = "processor", Value = "i7" },
                        new ItemCharacteristic { Id = 4, Key = "ram", Value = "32GB" }
                    }
                },
                new Item
                {
                    Id = 3,
                    Name = "iPhone 13",
                    Desc = "Latest iPhone model",
                    Price = 800,
                    CategoryId = 3,
                    BrandId = 1,
                    UserId = 1,
                    IsAvailable = false,
                    Garantia = 12,
                    Reviews = new List<Review>(),
                    Characteristics = new List<ItemCharacteristic>()
                }
            };
        }

        private List<CartItem> GetTestCartItems()
        {
            return new List<CartItem>
            {
                new CartItem
                {
                    Id = 1,
                    UserId = 1,
                    ItemId = 1,
                    Quantity = 1
                },
                new CartItem
                {
                    Id = 2,
                    UserId = 1,
                    ItemId = 2,
                    Quantity = 1
                }
                // Товар с Id = 3 НЕ добавляем в корзину, чтобы количество было 2
            };
        }

        private List<WishlistItem> GetTestWishlistItems()
        {
            return new List<WishlistItem>
            {
                new WishlistItem
                {
                    Id = 1,
                    UserId = 1,
                    ItemId = 1
                },
                new WishlistItem
                {
                    Id = 2,
                    UserId = 1,
                    ItemId = 2
                }
                // Товар с Id = 3 НЕ добавляем в избранное, чтобы количество было 2
            };
        }

        private List<User> GetTestUsers()
        {
            return new List<User>
            {
                new User
                {
                    Id = 1,
                    Name = "Test User",
                    Email = "test@example.com",
                    UserName = "test@example.com",
                    IsAdmin = false
                },
                new User
                {
                    Id = 2,
                    Name = "Admin User",
                    Email = "admin@example.com",
                    UserName = "admin@example.com",
                    IsAdmin = true
                }
            };
        }

        private void SetupAuthenticatedUser(int userId = 1)
        {
            var user = _testUsers.FirstOrDefault(u => u.Id == userId);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, user?.Name ?? "Test User"),
                new Claim(ClaimTypes.Email, user?.Email ?? "test@example.com")
            };

            var identity = new ClaimsIdentity(claims, "TestAuth");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext { User = claimsPrincipal };
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }

        private void SetupUnauthenticatedUser()
        {
            var identity = new ClaimsIdentity();
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext { User = claimsPrincipal };
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }

        [Fact]
        public void Index_ReturnsViewResult_WithLayoutData()
        {
            SetupAuthenticatedUser();
            var result = _controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult.ViewData["Categories"]);
            Assert.NotNull(viewResult.ViewData["FeaturedItems"]);
            Assert.NotNull(viewResult.ViewData["TrendingItems"]);
            Assert.NotNull(viewResult.ViewData["RecommendedItems"]);
            Assert.NotNull(viewResult.ViewData["PopularItems"]);
        }

        [Fact]
        public void Index_WhenUserAuthenticated_SetsCartAndWishlistCount()
        {
            SetupAuthenticatedUser(1);
            var result = _controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);

            // Ожидаем 2 товара в корзине (только товары 1 и 2)
            Assert.Equal(2, viewResult.ViewData["CartCount"]);
            // Ожидаем 2 товара в избранном (только товары 1 и 2)
            Assert.Equal(2, viewResult.ViewData["WishlistCount"]);
        }

        [Fact]
        public void Browse_ReturnsViewResult_WithItems()
        {
            SetupAuthenticatedUser();
            var result = _controller.Browse(query: null, categoryIds: null, page: 1, sort: "default", minPrice: null, maxPrice: null, brandIds: null);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<List<Item>>(viewResult.Model);
            Assert.NotNull(model);
        }

        [Fact]
        public void Browse_WithQuery_FiltersItems()
        {
            SetupAuthenticatedUser();
            var query = "MacBook";
            var result = _controller.Browse(query: query, categoryIds: null, page: 1, sort: "default", minPrice: null, maxPrice: null, brandIds: null);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<List<Item>>(viewResult.Model);
            Assert.Single(model);
            Assert.Contains("MacBook", model[0].Name);
        }

        [Fact]
        public void Browse_WithCategoryIds_FiltersByCategory()
        {
            SetupAuthenticatedUser();
            var categoryIds = new List<int> { 2 };
            var result = _controller.Browse(query: null, categoryIds: categoryIds, page: 1, sort: "default", minPrice: null, maxPrice: null, brandIds: null);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<List<Item>>(viewResult.Model);
            Assert.Equal(2, model.Count);
            Assert.All(model, i => Assert.Equal(2, i.CategoryId));
        }

        [Fact]
        public void Browse_WithPriceRange_FiltersItems()
        {
            SetupAuthenticatedUser();
            decimal minPrice = 1000;
            decimal maxPrice = 1800;
            var result = _controller.Browse(query: null, categoryIds: null, page: 1, sort: "default", minPrice: minPrice, maxPrice: maxPrice, brandIds: null);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<List<Item>>(viewResult.Model);
            Assert.Single(model);
            Assert.True(model[0].Price >= minPrice && model[0].Price <= maxPrice);
        }

        [Fact]
        public void Browse_WithBrandIds_FiltersByBrand()
        {
            SetupAuthenticatedUser();
            var brandIds = new List<int> { 1 };
            var result = _controller.Browse(query: null, categoryIds: null, page: 1, sort: "default", minPrice: null, maxPrice: null, brandIds: brandIds);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<List<Item>>(viewResult.Model);
            Assert.Equal(2, model.Count);
            Assert.All(model, i => Assert.Equal(1, i.BrandId));
        }

        [Fact]
        public void Browse_WithSortPriceAsc_SortsItems()
        {
            SetupAuthenticatedUser();
            var sort = "price_asc";
            var result = _controller.Browse(query: null, categoryIds: null, page: 1, sort: sort, minPrice: null, maxPrice: null, brandIds: null);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<List<Item>>(viewResult.Model);
            Assert.Equal(3, model.Count);
            Assert.True(model[0].Price <= model[1].Price);
        }

        [Fact]
        public void Browse_WithSortPriceDesc_SortsItems()
        {
            SetupAuthenticatedUser();
            var sort = "price_desc";
            var result = _controller.Browse(query: null, categoryIds: null, page: 1, sort: sort, minPrice: null, maxPrice: null, brandIds: null);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<List<Item>>(viewResult.Model);
            Assert.Equal(3, model.Count);
            Assert.True(model[0].Price >= model[1].Price);
        }

        [Fact]
        public void Browse_WithPagination_ReturnsCorrectPage()
        {
            SetupAuthenticatedUser();
            int page = 1;
            var result = _controller.Browse(query: null, categoryIds: null, page: page, sort: "default", minPrice: null, maxPrice: null, brandIds: null);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(page, viewResult.ViewData["Page"]);
            Assert.Equal(12, viewResult.ViewData["PageSize"]);
        }
    }
}