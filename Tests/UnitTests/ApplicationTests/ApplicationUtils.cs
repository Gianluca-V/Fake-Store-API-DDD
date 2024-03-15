using Domain.CategoryAggregate;
using Domain.ProductAggregate;
using Domain.UserAggregate;
using Infrastructure.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.UnitTests.ApplicationTests
{
    public static class ApplicationUtils
    {
        //Product Test Utils
        public static readonly string ProductName = "ProductName";
        public static readonly string ProductDescription = "ProductDescription";
        public static readonly float ProductPrice = 100;
        public static readonly List<string> ProductImages = ["http://example.net"];

        //Category Test Utils
        public static readonly string CategoryName = "CategoryName";
        public static readonly string CategoryImage = "http://example.net/";

        //User Test Utils
        public static readonly string UserName = "UserName";
        public static readonly string UserEmail = "UserEmail@email.com";
        public static readonly string UserPassword = "UserPassword";

        //JWT Test Utils
        public static readonly JWTSettings JwtSettings = new JWTSettings
        {
            Audience = "JWTAudience",
            ExpiryDays = 5,
            Issuer = "JWTIssuer",
            Secret = "JWT_Secret_Key_Used_For_Unit_Testing"
        };

        //Instances Test Utils
        public static readonly Category Category = Category.Create(CategoryName, CategoryImage);
        public static readonly Product Product = Product.Create(ProductName, ProductDescription, ProductPrice, Category, ProductImages);
        public static readonly User User = User.Create(UserName, UserEmail, UserPassword);

        //Functions Test Utils
        public static Product GetRandomProduct()
        {
            var productSample = Product;
            productSample.Name = Domain.ProductAggregate.ValueObjects.ProductName.CreateName(ProductName + new Random().Next());

            return productSample;
        }
        public static Category GetRandomCategory()
        {
            var categorySample = Category;
            categorySample.Name = Domain.CategoryAggregate.ValueObjects.CategoryName.CreateName(CategoryName + new Random().Next());

            return categorySample;
        }
    }
}
