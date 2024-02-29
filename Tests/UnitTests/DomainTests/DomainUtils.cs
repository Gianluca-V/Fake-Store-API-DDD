using System;
using System.Collections.Generic;
using Domain.CategoryAggregate;
using Domain.ProductAggregate;

namespace Tests.UnitTests.DomainTest
{
    public static class DomainUtils
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

        //Instances Test Utils
        public static Category Category = Category.Create(CategoryName, CategoryImage);
        public static Product Product = Product.Create(ProductName, ProductDescription, ProductPrice, Category, ProductImages);
    }
}

