using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Xunit;

namespace ProductsApp.Tests {

    public class ProductsAppShould {
        // Add your test here
        [Fact]
        public void Return_argument_null_exception_when_product_is_null()
        {
            //arrange
            var sut = new Products();
            Product product = null;

            //act
            var exception = Assert.Throws<ArgumentNullException>(() => sut.AddNew(product));

            //assert
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void Add_product_should_store_the_item_in_the_list()
        {
            //arrange
            var sut = new Products();
            var product01 = new Product() { Name = "Test 101", IsSold = false };
            var product02 = new Product() { Name = "Test 102", IsSold = false };

            //act
            sut.AddNew(product01);
            sut.AddNew(product02);

            //assert
            Assert.NotNull(sut.Items);
            Assert.Equal(2, sut.Items.Count());
        }

        [Fact]
        public void Sold_product_should_update_the_item_status_in_the_list()
        {
            //arrange
            var sut = new Products();
            var product01 = new Product() { Name = "Test 101", IsSold = false };
            var product02 = new Product() { Name = "Test 102", IsSold = false };
            var product03 = new Product() { Name = "Test 103", IsSold = false };


            //act
            sut.AddNew(product01);
            sut.AddNew(product02);
            sut.AddNew(product03);

            sut.Sold(product01);

            //assert
            Assert.True(product01.IsSold);
            Assert.NotNull(sut.Items);
            Assert.Equal(2, sut.Items.Count());
        }

        [Fact]
        public void Return_namerequiredexception_when_product_name_is_null()
        {
            //arrange
            var product = new Product() { Name = null, IsSold = false };

            //act
            var exception = Assert.Throws<NameRequiredException>(() => product.Validate());

            //assert
            Assert.NotNull(exception);
            Assert.IsType<NameRequiredException>(exception);
        }
    }

    internal class Products {
        private readonly List<Product> _products = new List<Product> ();

        public IEnumerable<Product> Items => _products.Where (t => !t.IsSold);

        public void AddNew (Product product) {
            product = product ??
                throw new ArgumentNullException ();
            product.Validate ();
            _products.Add (product);
        }

        public void Sold (Product product) {
            product.IsSold = true;
        }

    }

    internal class Product {
        public bool IsSold { get; set; }
        public string Name { get; set; }

        internal void Validate () {
            Name = Name ??
                throw new NameRequiredException ();
        }

    }

    [Serializable]
    internal class NameRequiredException : Exception {
        public NameRequiredException () { /* ... */ }

        public NameRequiredException (string message) : base (message) { /* ... */ }

        public NameRequiredException (string message, Exception innerException) : base (message, innerException) { /* ... */ }

        protected NameRequiredException (SerializationInfo info, StreamingContext context) : base (info, context) { /* ... */ }
    }
}