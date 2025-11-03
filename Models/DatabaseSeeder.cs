using GradDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GradDemo.Data
{
    public static class DatabaseSeeder
    {
        // Static dates for consistent seeding
        private static readonly DateTime BaseDate = new DateTime(2024, 1, 15, 12, 0, 0);
        
        public static void Seed(this ModelBuilder modelBuilder)
        {
            SeedRoles(modelBuilder);
            SeedUsers(modelBuilder);
            SeedSizes(modelBuilder);
            SeedTables(modelBuilder);
            SeedItems(modelBuilder);
            SeedItemSizes(modelBuilder);
            SeedOrders(modelBuilder);
            SeedOrderItems(modelBuilder);
            SeedBills(modelBuilder);
            SeedCancelledOrders(modelBuilder);
        }

        private static void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "Cashier" },
                new Role { Id = 3, Name = "Captain" },
                new Role { Id = 4, Name = "Waiter" },
                new Role { Id = 5, Name = "Customer" }
            );
        }

        private static void SeedUsers(ModelBuilder modelBuilder)
        {
            // Password hash for '12345678' generated using ASP.NET Core Identity's PasswordHasher
            var passwordHash = "AQAAAAIAAYagAAAAEEdWWKzOyYcpm02EZvXQvtSWGoNo/kFwiYgsJZL5APdqOcx/6ZGytrFrgyb88l0mzA==";
            
            modelBuilder.Entity<User>().HasData(
                // Admin
                new User { Id = 1, Name = "Cafe Manager", Email = "manager@mochacafe.com", PasswordHash = passwordHash, Phone = "+201000000001", RoleId = 1 },
                
                // Cashiers
                new User { Id = 2, Name = "Ahmed Cashier", Email = "ahmed.cashier@mochacafe.com", PasswordHash = passwordHash, Phone = "+201000000002", RoleId = 2 },
                new User { Id = 3, Name = "Mona Cashier", Email = "mona.cashier@mochacafe.com", PasswordHash = passwordHash, Phone = "+201000000003", RoleId = 2 },
                
                // Captains (Shift Supervisors)
                new User { Id = 4, Name = "Captain Samir", Email = "samir.captain@mochacafe.com", PasswordHash = passwordHash, Phone = "+201000000004", RoleId = 3 },
                new User { Id = 5, Name = "Captain Rania", Email = "rania.captain@mochacafe.com", PasswordHash = passwordHash, Phone = "+201000000005", RoleId = 3 },
                
                // Waiters (Baristas)
                new User { Id = 6, Name = "Barista Tarek", Email = "tarek.barista@mochacafe.com", PasswordHash = passwordHash, Phone = "+201000000006", RoleId = 4 },
                new User { Id = 7, Name = "Barista Nadia", Email = "nadia.barista@mochacafe.com", PasswordHash = passwordHash, Phone = "+201000000007", RoleId = 4 },
                new User { Id = 8, Name = "Barista Karim", Email = "karim.barista@mochacafe.com", PasswordHash = passwordHash, Phone = "+201000000008", RoleId = 4 },
                
                // Customers
                new User { Id = 9, Name = "Regular Customer Ali", Email = "ali.customer@email.com", PasswordHash = passwordHash, Phone = "+201000000009", RoleId = 5 },
                new User { Id = 10, Name = "Customer Sara", Email = "sara.customer@email.com", PasswordHash = passwordHash, Phone = "+201000000010", RoleId = 5 },
                new User { Id = 11, Name = "Customer Omar", Email = "omar.customer@email.com", PasswordHash = passwordHash, Phone = "+201000000011", RoleId = 5 }
            );
        }

        private static void SeedSizes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Size>().HasData(
                new Size { Id = 1, Code = 'S', Description = "Small (8oz)" },
                new Size { Id = 2, Code = 'M', Description = "Medium (12oz)" },
                new Size { Id = 3, Code = 'L', Description = "Large (16oz)" },
                new Size { Id = 4, Code = 'X', Description = "Extra Large (20oz)" }
            );
        }

        private static void SeedTables(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Table>().HasData(
                new Table { Id = 1, Area = "Main Hall - Window View" },
                new Table { Id = 2, Area = "Main Hall - Center" },
                new Table { Id = 3, Area = "Main Hall - Quiet Corner" },
                new Table { Id = 4, Area = "Outdoor Terrace - Garden" },
                new Table { Id = 5, Area = "Outdoor Terrace - Street View" },
                new Table { Id = 6, Area = "VIP Lounge - Private" },
                new Table { Id = 7, Area = "VIP Lounge - Family" },
                new Table { Id = 8, Area = "Smoking Area - Outdoor" },
                new Table { Id = 9, Area = "Reading Corner" },
                new Table { Id = 10, Area = "Business Meeting Area" }
            );
        }

        private static void SeedItems(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasData(
                // Hot Coffees
                new Item { Id = 1, Name = "Mocha Flavor", Category = "Hot Coffee", Description = "Rich chocolate flavor with espresso and steamed milk", Price = 35.00, IsAvailable = true, ImageUrl = "https://cat-price.pockethost.io/api/files/i7flkxeto2m2woc/cdce0tucwn7xsy9/4K1jXswHxGI_Q6E9hQY7Hy.png?token=" },
                new Item { Id = 2, Name = "Vanilla Latte", Category = "Hot Coffee", Description = "Smooth espresso with vanilla syrup and steamed milk", Price = 32.00, IsAvailable = true, ImageUrl = "https://cat-price.pockethost.io/api/files/i7flkxeto2m2woc/cdce0tucwn7xsy9/5ZxfcWYH844_DS0p54Eqvm.png" },
                new Item { Id = 3, Name = "Hazelnut Latte", Category = "Hot Coffee", Description = "Creamy latte with rich hazelnut flavor", Price = 34.00, IsAvailable = true, ImageUrl = "https://cat-price.pockethost.io/api/files/i7flkxeto2m2woc/cdce0tucwn7xsy9/3fSbMGcJ23O_jXYMsEQvIP.png?token=" },
                new Item { Id = 4, Name = "Caramel Macchiato", Category = "Hot Coffee", Description = "Espresso with vanilla syrup, milk, and caramel drizzle", Price = 38.00, IsAvailable = true, ImageUrl = "https://cat-price.pockethost.io/api/files/i7flkxeto2m2woc/cdce0tucwn7xsy9/6lQMyC4OXqu_OzhkAwC0QO.png?token=" },

                // Cold Coffees
                new Item { Id = 5, Name = "Cold Coffee", Category = "Cold Coffee", Description = "Chilled brewed coffee served over ice", Price = 25.00, IsAvailable = true, ImageUrl = "https://cat-price.pockethost.io/api/files/i7flkxeto2m2woc/cdce0tucwn7xsy9/2bRiHb1mqXg_9Qy7IRUrcn.png?token=" },
                new Item { Id = 6, Name = "Strawberry Latte", Category = "Cold Coffee", Description = "Refreshing cold latte with strawberry syrup", Price = 36.00, IsAvailable = true, ImageUrl = "https://cat-price.pockethost.io/api/files/i7flkxeto2m2woc/cdce0tucwn7xsy9/5ZxfcWYH844_DS0p54Eqvm.png" },
                new Item { Id = 7, Name = "Iced Americano", Category = "Cold Coffee", Description = "Espresso shots chilled and served over ice", Price = 28.00, IsAvailable = true, ImageUrl = "https://cat-price.pockethost.io/api/files/i7flkxeto2m2woc/cdce0tucwn7xsy9/5ZxfcWYH844_DS0p54Eqvm.png?token=" },
                new Item { Id = 8, Name = "Cold Brew", Category = "Cold Coffee", Description = "Smooth cold brewed coffee served chilled", Price = 30.00, IsAvailable = true, ImageUrl = "https://cat-price.pockethost.io/api/files/i7flkxeto2m2woc/cdce0tucwn7xsy9/6lQMyC4OXqu_OzhkAwC0QO.png?token=" },

                // Tea & Infusions
                new Item { Id = 9, Name = "Moroccan Mint Tea", Category = "Tea", Description = "Traditional green tea with fresh mint leaves", Price = 20.00, IsAvailable = true, ImageUrl = "https://cat-price.pockethost.io/api/files/i7flkxeto2m2woc/cdce0tucwn7xsy9/5ZxfcWYH844_DS0p54Eqvm.png?token=" },
                new Item { Id = 10, Name = "Earl Grey", Category = "Tea", Description = "Classic black tea with bergamot orange flavor", Price = 18.00, IsAvailable = true, ImageUrl = "https://cat-price.pockethost.io/api/files/i7flkxeto2m2woc/cdce0tucwn7xsy9/3fSbMGcJ23O_jXYMsEQvIP.png?token=" },
                new Item { Id = 11, Name = "Chai Latte", Category = "Tea", Description = "Spiced tea with steamed milk and honey", Price = 26.00, IsAvailable = true, ImageUrl = "https://cat-price.pockethost.io/api/files/i7flkxeto2m2woc/cdce0tucwn7xsy9/5ZxfcWYH844_DS0p54Eqvm.png?token=" },

                // Pastries & Snacks
                new Item { Id = 12, Name = "Croissant", Category = "Pastries", Description = "Freshly baked butter croissant", Price = 15.00, IsAvailable = true, ImageUrl = "https://cat-price.pockethost.io/api/files/i7flkxeto2m2woc/cdce0tucwn7xsy9/3fSbMGcJ23O_jXYMsEQvIP.png?token=" },
                new Item { Id = 13, Name = "Chocolate Muffin", Category = "Pastries", Description = "Rich chocolate muffin with chocolate chips", Price = 18.00, IsAvailable = true, ImageUrl = "https://cat-price.pockethost.io/api/files/i7flkxeto2m2woc/cdce0tucwn7xsy9/5ZxfcWYH844_DS0p54Eqvm.png?token=" },
                new Item { Id = 14, Name = "Blueberry Scone", Category = "Pastries", Description = "Traditional scone with fresh blueberries", Price = 16.00, IsAvailable = true, ImageUrl = "https://cat-price.pockethost.io/api/files/i7flkxeto2m2woc/cdce0tucwn7xsy9/14g9qpFHWtQ_Fb1kchHuS1.png?token=" },
                new Item { Id = 15, Name = "Cinnamon Roll", Category = "Pastries", Description = "Soft cinnamon roll with cream cheese frosting", Price = 22.00, IsAvailable = true, ImageUrl = "https://cat-price.pockethost.io/api/files/i7flkxeto2m2woc/cdce0tucwn7xsy9/6lQMyC4OXqu_OzhkAwC0QO.png?token=" },

                // Special Drinks
                new Item { Id = 16, Name = "Hot Chocolate", Category = "Special Drinks", Description = "Rich and creamy hot chocolate", Price = 24.00, IsAvailable = true, ImageUrl = "https://cat-price.pockethost.io/api/files/i7flkxeto2m2woc/cdce0tucwn7xsy9/14g9qpFHWtQ_Fb1kchHuS1.png?token=" },
                new Item { Id = 17, Name = "Matcha Latte", Category = "Special Drinks", Description = "Green tea powder with steamed milk", Price = 32.00, IsAvailable = true, ImageUrl = "https://cat-price.pockethost.io/api/files/i7flkxeto2m2woc/cdce0tucwn7xsy9/5ZxfcWYH844_DS0p54Eqvm.png?token=" },
                new Item { Id = 18, Name = "Turkish Coffee", Category = "Special Drinks", Description = "Traditional strong Turkish coffee", Price = 20.00, IsAvailable = true, ImageUrl = "https://cat-price.pockethost.io/api/files/i7flkxeto2m2woc/cdce0tucwn7xsy9/2bRiHb1mqXg_9Qy7IRUrcn.png?token=" }
            );
        }

        private static void SeedItemSizes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemSize>().HasData(
                // Hot Coffees - All sizes available
                new ItemSize { Id = 1, ItemId = 1, SizeId = 1, Multiplier = 0.8 },  // Mocha Small
                new ItemSize { Id = 2, ItemId = 1, SizeId = 2, Multiplier = 1.0 },  // Mocha Medium
                new ItemSize { Id = 3, ItemId = 1, SizeId = 3, Multiplier = 1.2 },  // Mocha Large
                new ItemSize { Id = 4, ItemId = 1, SizeId = 4, Multiplier = 1.5 },  // Mocha XL

                new ItemSize { Id = 5, ItemId = 2, SizeId = 1, Multiplier = 0.8 },  // Vanilla Latte Small
                new ItemSize { Id = 6, ItemId = 2, SizeId = 2, Multiplier = 1.0 },  // Vanilla Latte Medium
                new ItemSize { Id = 7, ItemId = 2, SizeId = 3, Multiplier = 1.2 },  // Vanilla Latte Large
                new ItemSize { Id = 8, ItemId = 2, SizeId = 4, Multiplier = 1.5 },  // Vanilla Latte XL

                new ItemSize { Id = 9, ItemId = 3, SizeId = 1, Multiplier = 0.8 },  // Hazelnut Latte Small
                new ItemSize { Id = 10, ItemId = 3, SizeId = 2, Multiplier = 1.0 }, // Hazelnut Latte Medium
                new ItemSize { Id = 11, ItemId = 3, SizeId = 3, Multiplier = 1.2 }, // Hazelnut Latte Large
                new ItemSize { Id = 12, ItemId = 3, SizeId = 4, Multiplier = 1.5 }, // Hazelnut Latte XL

                new ItemSize { Id = 13, ItemId = 4, SizeId = 1, Multiplier = 0.8 }, // Caramel Macchiato Small
                new ItemSize { Id = 14, ItemId = 4, SizeId = 2, Multiplier = 1.0 }, // Caramel Macchiato Medium
                new ItemSize { Id = 15, ItemId = 4, SizeId = 3, Multiplier = 1.2 }, // Caramel Macchiato Large
                new ItemSize { Id = 16, ItemId = 4, SizeId = 4, Multiplier = 1.5 }, // Caramel Macchiato XL

                // Cold Coffees - All sizes available
                new ItemSize { Id = 17, ItemId = 5, SizeId = 1, Multiplier = 0.8 }, // Cold Coffee Small
                new ItemSize { Id = 18, ItemId = 5, SizeId = 2, Multiplier = 1.0 }, // Cold Coffee Medium
                new ItemSize { Id = 19, ItemId = 5, SizeId = 3, Multiplier = 1.2 }, // Cold Coffee Large
                new ItemSize { Id = 20, ItemId = 5, SizeId = 4, Multiplier = 1.5 }, // Cold Coffee XL

                new ItemSize { Id = 21, ItemId = 6, SizeId = 1, Multiplier = 0.8 }, // Strawberry Latte Small
                new ItemSize { Id = 22, ItemId = 6, SizeId = 2, Multiplier = 1.0 }, // Strawberry Latte Medium
                new ItemSize { Id = 23, ItemId = 6, SizeId = 3, Multiplier = 1.2 }, // Strawberry Latte Large
                new ItemSize { Id = 24, ItemId = 6, SizeId = 4, Multiplier = 1.5 }, // Strawberry Latte XL

                // Pastries - Single size only (Medium)
                new ItemSize { Id = 25, ItemId = 12, SizeId = 2, Multiplier = 1.0 }, // Croissant
                new ItemSize { Id = 26, ItemId = 13, SizeId = 2, Multiplier = 1.0 }, // Chocolate Muffin
                new ItemSize { Id = 27, ItemId = 14, SizeId = 2, Multiplier = 1.0 }, // Blueberry Scone
                new ItemSize { Id = 28, ItemId = 15, SizeId = 2, Multiplier = 1.0 }  // Cinnamon Roll
            );
        }

        private static void SeedOrders(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, OrderDate = BaseDate, Status = "Completed", Total = 142.00, CustomerId = 9, CashierId = 2, CaptainId = 4, WaiterId = 6, TableId = 1 },
                new Order { Id = 2, OrderDate = BaseDate.AddHours(-2), Status = "Paid", Total = 198.00, CustomerId = 10, CashierId = 3, CaptainId = 5, WaiterId = 7, TableId = 3 },
                new Order { Id = 3, OrderDate = BaseDate.AddHours(-1), Status = "Pending", Total = 76.00, CustomerId = 11, CashierId = 2, CaptainId = 4, WaiterId = 8, TableId = 5 },
                new Order { Id = 4, OrderDate = BaseDate.AddDays(-1), Status = "Completed", Total = 124.00, CustomerId = 9, CashierId = 3, CaptainId = 5, WaiterId = 6, TableId = 2 },
                new Order { Id = 5, OrderDate = BaseDate.AddDays(-1), Status = "Cancelled", Total = 45.00, CustomerId = 10, CashierId = 2, CaptainId = 4, WaiterId = 7, TableId = 4 }
            );
        }

        private static void SeedOrderItems(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>().HasData(
                // Order 1 - Morning Coffee Break
                new OrderItem { Id = 1, Quantity = 2, OrderId = 1, ItemId = 1, SizeId = 2, IsPayed = true },  // 2x Mocha Medium
                new OrderItem { Id = 2, Quantity = 1, OrderId = 1, ItemId = 12, SizeId = 2, IsPayed = true }, // 1x Croissant
                new OrderItem { Id = 3, Quantity = 1, OrderId = 1, ItemId = 9, SizeId = 2, IsPayed = true },  // 1x Moroccan Mint Tea

                // Order 2 - Afternoon Meeting
                new OrderItem { Id = 4, Quantity = 1, OrderId = 2, ItemId = 4, SizeId = 3, IsPayed = true },  // 1x Caramel Macchiato Large
                new OrderItem { Id = 5, Quantity = 1, OrderId = 2, ItemId = 6, SizeId = 2, IsPayed = true },  // 1x Strawberry Latte Medium
                new OrderItem { Id = 6, Quantity = 2, OrderId = 2, ItemId = 13, SizeId = 2, IsPayed = true }, // 2x Chocolate Muffin
                new OrderItem { Id = 7, Quantity = 1, OrderId = 2, ItemId = 5, SizeId = 2, IsPayed = true },  // 1x Cold Coffee Medium

                // Order 3 - Evening Study Session
                new OrderItem { Id = 8, Quantity = 1, OrderId = 3, ItemId = 3, SizeId = 2, IsPayed = false }, // 1x Hazelnut Latte Medium
                new OrderItem { Id = 9, Quantity = 1, OrderId = 3, ItemId = 15, SizeId = 2, IsPayed = false }, // 1x Cinnamon Roll

                // Order 4 - Quick Coffee
                new OrderItem { Id = 10, Quantity = 1, OrderId = 4, ItemId = 2, SizeId = 4, IsPayed = true }, // 1x Vanilla Latte XL
                new OrderItem { Id = 11, Quantity = 1, OrderId = 4, ItemId = 14, SizeId = 2, IsPayed = true }, // 1x Blueberry Scone

                // Order 5 (Cancelled) - Late Night Craving
                new OrderItem { Id = 12, Quantity = 1, OrderId = 5, ItemId = 16, SizeId = 2, IsPayed = false }, // 1x Hot Chocolate
                new OrderItem { Id = 13, Quantity = 1, OrderId = 5, ItemId = 12, SizeId = 2, IsPayed = false }  // 1x Croissant
            );
        }

        private static void SeedBills(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bill>().HasData(
                new Bill { Id = 1, PaymentMethod = "Cash", BillDate = BaseDate, OrderId = 1, CashierId = 2 },
                new Bill { Id = 2, PaymentMethod = "Credit Card", BillDate = BaseDate.AddHours(-2), OrderId = 2, CashierId = 3 },
                new Bill { Id = 3, PaymentMethod = "Cash", BillDate = BaseDate.AddDays(-1), OrderId = 4, CashierId = 3 }
            );
        }

        private static void SeedCancelledOrders(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CancelledOrder>().HasData(
                new CancelledOrder { Id = 1, Reason = "Customer had to leave suddenly", CancelledDate = BaseDate.AddDays(-1), OrderId = 5, CancelledById = 2 }
            );
        }
    }
}