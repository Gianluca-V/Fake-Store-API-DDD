using System;
using System.Collections.Generic;
using Domain.CategoryAggregate;
using Domain.ProductAggregate;
using Domain.UserAggregate;
using Infrastructure.Authentication;

namespace Tests.UnitTests.InfrastructureTest
{
    public static class InfrastructureUtils
    {
        //Product Test Utils
        public static string ProductName = "ProductName";
        public static string ProductDescription = "ProductDescription";
        public static float ProductPrice = 100;
        public static List<string> ProductImages = ["http://example.net"];

        //Category Test Utils
        public static string CategoryName = "CategoryName";
        public static string CategoryImage = "http://example.net/";

        //User Test Utils
        public static string UserName = "UserName";
        public static string UserEmail = "UserEmail@email.com";
        public static string UserPassword = "UserPassword";

        //JWT Test Utils
        public static JWTSettings JwtSettings = new JWTSettings
        {
            Audience = "JWTAudience",
            ExpiryDays = 5,
            Issuer = "JWTIssuer",
            Secret = "JWT_Secret_Key_Used_For_Unit_Testing"
        };

        //Instances Test Utils
        public static Category Category = Category.Create(CategoryName, CategoryImage);
        public static Product Product = Product.Create(ProductName, ProductDescription, ProductPrice, Category, ProductImages);
        public static User User = User.Create(UserName,UserEmail,UserPassword);
    }
}

