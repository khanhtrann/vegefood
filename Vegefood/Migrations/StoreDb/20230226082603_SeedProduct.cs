using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vegefood.Migrations.StoreDb
{
    public partial class SeedProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ID", "Description", "Image", "IsFeatured", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, "A small river named Duden flows by their place and supplies it with the necessary regelialia. It is a paradisematic country, in which roasted parts of sentences fly into your mouth. Text should turn around and return to its own, safe country. But nothing the copy said could convince her and so it didn’t take long until.", "images/product-1.png", true, "Bell Pepper", 12m, 100 },
                    { 2, "A small river named Duden flows by their place and supplies it with the necessary regelialia. It is a paradisematic country, in which roasted parts of sentences fly into your mouth. Text should turn around and return to its own, safe country. But nothing the copy said could convince her and so it didn’t take long until.", "images/product-2.jpg", true, "Strawberry", 9m, 15 },
                    { 3, "A small river named Duden flows by their place and supplies it with the necessary regelialia. It is a paradisematic country, in which roasted parts of sentences fly into your mouth. Text should turn around and return to its own, safe country. But nothing the copy said could convince her and so it didn’t take long until.", "images/product-3.jpg", false, "Green Beans", 7m, 200 },
                    { 4, "A small river named Duden flows by their place and supplies it with the necessary regelialia. It is a paradisematic country, in which roasted parts of sentences fly into your mouth. Text should turn around and return to its own, safe country. But nothing the copy said could convince her and so it didn’t take long until.", "images/product-4.jpg", true, "Purple Cabbage", 10m, 30 },
                    { 5, "The big red tomato is one of the nutritions that you must have.", "images/product-5.jpg", false, "Tomato", 18m, 10 },
                    { 6, "This spiky and layered looking vegetable is defenitely a good choice.", "images/product-6.jpg", false, "Broccoli", 17m, 150 },
                    { 7, "A orange veggie is one of the best things to have in your warm soup.", "images/product-7.jpg", false, "Carrots", 24m, 20 },
                    { 8, "Drink more juice to have a healthier body and digestion.", "images/product-8.jpg", false, "Fruit Juice", 29m, 50 },
                    { 9, "Onion, something that your curry soup cannot live without.", "images/product-9.jpg", false, "Onion", 26m, 30 },
                    { 10, "An apple a day keeps the doctor away.", "images/product-10.jpg", false, "Apple", 22m, 300 },
                    { 11, "Keep accompany of your casual garlic bread breakfast!", "images/product-11.jpg", false, "Garlic", 22m, 70 },
                    { 12, "Something dangerous for your mouth, but still you want to bite into it.", "images/product-12.jpg", false, "Chili", 22m, 50 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
