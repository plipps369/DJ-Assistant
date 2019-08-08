using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuddyBux.DAO;
using BuddyBux.Models;
using BuddyBux.Models.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuddyBuxWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreFrontController : ControllerBase
    {
        private IBuddyBuxDAO _db;

        /// <summary>
        /// Creates a new account controller.
        /// </summary>
        /// <param name="tokenGenerator">A token generator used when creating auth tokens.</param>
        /// <param name="db">Access to the BuddyBux database.</param>
        public StoreFrontController(IBuddyBuxDAO db)
        {
            _db = db;
        }

        [HttpPost("addproduct")]
        [Authorize]
        public IActionResult AddProduct(ProductModel model)
        {
            // Assume the user is not authorized
            IActionResult result = Unauthorized();

            try
            {
                
                ProductItem product = new ProductItem();
                product.CreatorId = _db.GetUserItemByEmail(User.Identity.Name).Id;
                product.IsCharitable = model.IsCharitable;
                product.Name = model.Name;
                product.Id = _db.AddProduct(product);

                result = Ok(product);
            }
            catch (Exception)
            {
                result = BadRequest(new { Message = "Add product failed." });
            }

            return result;
        }

        [HttpPost("add-product-to-store")]
        [Authorize]
        public IActionResult AddProductToStore(NewProductModel newProduct)
        {
            // Assume the user is not authorized
            IActionResult result = Unauthorized();

            try
            {
                StoreProductItem item = new StoreProductItem();
                item.ProductId = newProduct.Id;
                item.StoreId = _db.GetStoreFront(_db.GetUserItemByEmail(User.Identity.Name).Id).Store.Id;
                item.Cost = newProduct.Cost;
                item.Qty = newProduct.Qty;
                item.Id = _db.AddStoreProductItem(item);
                result = Ok(item);

            }
            catch (Exception)
            {
                result = BadRequest(new { Message = "Add product to store failed." });
            }

            return result;
        }


        [HttpGet("products")]
        //[Authorize]
        public IActionResult GetProducts()
        {
            // Assume the user is not authorized
            IActionResult result = Unauthorized();

            try
            {
                var products = _db.GetProducts();
                string t = User.Identity.Name;
                Console.WriteLine(t);
                result = Ok(products);
            }
            catch (Exception)
            {
                result = BadRequest(new { Message = "Get products failed." });
            }

            return result;
        }

        [HttpGet("mystore")]
        [Authorize]
        public IActionResult GetMyStoreFront()
        {
            // Assume the user is not authorized
            IActionResult result = Unauthorized();

            try
            {
                var stroreFront = _db.GetStoreFront(_db.GetUserItemByEmail(User.Identity.Name).Id);
                string t = User.Identity.Name;
                Console.WriteLine(t);
                result = Ok(stroreFront);
            }
            catch (Exception)
            {
                result = BadRequest(new { Message = "Get Store Front failed." });
            }

            return result;
        }

        [HttpGet("{email}")]
        [Authorize]
        public IActionResult GetStoreFront(string email)
        {
            // Assume the user is not authorized
            IActionResult result = Unauthorized();

            try
            {
                var stroreFront = _db.GetStoreFront(_db.GetUserItemByEmail(email).Id);
                string t = User.Identity.Name;
                Console.WriteLine(t);
                result = Ok(stroreFront);
            }
            catch (Exception)
            {
                result = BadRequest(new { Message = "Get Store Front failed." });
            }

            return result;
        }

    }
}