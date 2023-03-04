using Microsoft.EntityFrameworkCore;

namespace Vegefood.Models
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Creating a composite key
            modelBuilder.Entity<CartItems>().HasKey(cartitems => new { cartitems.CartID, cartitems.ProductID });
            modelBuilder.Entity<OrderItems>().HasKey(orderitems => new { orderitems.OrderID, orderitems.ProductID });

            // Seeding Data
            modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Product>()
            .HasData(
                new Product
                {
                    ID = 1,
                    Name = "Bell Pepper",
                    Price = 12,
                    Quantity = 100,
                    Description = "A small river named Duden flows by their place and supplies it with the necessary regelialia. It is a paradisematic country, in which roasted parts of sentences fly into your mouth. Text should turn around and return to its own, safe country. But nothing the copy said could convince her and so it didn’t take long until.",
                    Image = "images/product-1.png",
                    IsFeatured = true
                },
                new Product
                {
                    ID = 2,
                    Name = "Strawberry",
                    Price = 9,
                    Quantity = 15,
                    Description = "A small river named Duden flows by their place and supplies it with the necessary regelialia. It is a paradisematic country, in which roasted parts of sentences fly into your mouth. Text should turn around and return to its own, safe country. But nothing the copy said could convince her and so it didn’t take long until.",
                    Image = "images/product-2.jpg",
                    IsFeatured = true
                },
                new Product
                {
                    ID = 3,
                    Name = "Green Beans",
                    Price = 7,
                    Quantity = 200,
                    Description = "A small river named Duden flows by their place and supplies it with the necessary regelialia. It is a paradisematic country, in which roasted parts of sentences fly into your mouth. Text should turn around and return to its own, safe country. But nothing the copy said could convince her and so it didn’t take long until.",
                    Image = "images/product-3.jpg",
                    IsFeatured = false
                },
                new Product
                {
                    ID = 4,
                    Name = "Purple Cabbage",
                    Price = 10,
                    Quantity = 30,
                    Description = "A small river named Duden flows by their place and supplies it with the necessary regelialia. It is a paradisematic country, in which roasted parts of sentences fly into your mouth. Text should turn around and return to its own, safe country. But nothing the copy said could convince her and so it didn’t take long until.",
                    Image = "images/product-4.jpg",
                    IsFeatured = true
                },
                new Product
                {
                    ID = 5,
                    Name = "Tomato",
                    Price = 18,
                    Quantity = 10,
                    Description = "The big red tomato is one of the nutritions that you must have.",
                    Image = "images/product-5.jpg",
                    IsFeatured = false
                },
                new Product
                {
                    ID = 6,
                    Name = "Broccoli",
                    Price = 17,
                    Quantity = 150,
                    Description = "This spiky and layered looking vegetable is defenitely a good choice.",
                    Image = "images/product-6.jpg",
                    IsFeatured = false
                },
                new Product
                {
                    ID = 7,
                    Name = "Carrots",
                    Price = 24,
                    Quantity = 20,
                    Description = "A orange veggie is one of the best things to have in your warm soup.",
                    Image = "images/product-7.jpg",
                    IsFeatured = false
                },
                new Product
                {
                    ID = 8,
                    Name = "Fruit Juice",
                    Price = 29,
                    Quantity = 50,
                    Description = "Drink more juice to have a healthier body and digestion.",
                    Image = "images/product-8.jpg",
                    IsFeatured = false
                },
                new Product
                {
                    ID = 9,
                    Name = "Onion",
                    Price = 26,
                    Quantity = 30,
                    Description = "Onion, something that your curry soup cannot live without.",
                    Image = "images/product-9.jpg",
                    IsFeatured = false
                },
                new Product
                {
                    ID = 10,
                    Name = "Apple",
                    Price = 22,
                    Quantity = 300,
                    Description = "An apple a day keeps the doctor away.",
                    Image = "images/product-10.jpg",
                    IsFeatured = false
                },
                new Product
                {
                    ID = 11,
                    Name = "Garlic",
                    Price = 22,
                    Quantity = 70,
                    Description = "Keep accompany of your casual garlic bread breakfast!",
                    Image = "images/product-11.jpg",
                    IsFeatured = false
                },
                new Product
                {
                    ID = 12,
                    Name = "Chili",
                    Price = 22,
                    Quantity = 50,
                    Description = "Something dangerous for your mouth, but still you want to bite into it.",
                    Image = "images/product-12.jpg",
                    IsFeatured = false
                }
                );
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItems> CartItems { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
    }
}
