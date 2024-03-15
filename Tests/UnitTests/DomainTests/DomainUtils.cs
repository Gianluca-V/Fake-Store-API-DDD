using System;
using System.Collections.Generic;
using Domain.CategoryAggregate;
using Domain.ProductAggregate;

namespace Tests.UnitTests.DomainTest
{
    public static class DomainUtils
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

        //Instances Test Utils
        public static readonly Category Category = Category.Create(CategoryName, CategoryImage);
        public static readonly Product Product = Product.Create(ProductName, ProductDescription, ProductPrice, Category, ProductImages);
    }
}

