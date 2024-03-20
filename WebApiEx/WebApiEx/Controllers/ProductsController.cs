using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiEx.Models;

namespace WebApiEx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public static readonly List<Products> _products = new List<Products>
        {
            new Products
            {
                Id = Guid.NewGuid(),
                Name = "Prod 1",
                Description = " New prod 1 ",
                Ratings = [1,2,3 ],
                CreatedOn = DateTime.UtcNow
            },
            new Products
            {
                Id = Guid.NewGuid(),
                Name = "Prod 2",
                Description = " New prod 2 ",
                Ratings = [1,1,1 ],
                CreatedOn = DateTime.UtcNow
            }
        };

        [HttpGet]
        public Products[] GetAllProducts()
        {
            return _products.ToArray();
        }

        [HttpGet("get-products-keyword/{keyword}")]
        public List<Products> GetProductsKeyword(string keyword)
        {
            List<Products> keywordsList = new();
            foreach (var existingProducts in _products)
            {
                if (keyword.Equals(existingProducts.Name) || keyword.Equals(existingProducts.Description) || keyword.Equals(existingProducts.Id) || keyword.Equals(existingProducts.CreatedOn))
                {
                    keywordsList.Add(existingProducts);
                }
                else
                {
                    int ok = 0;
                    bool successfullParse = int.TryParse(keyword, out ok);
                    if (successfullParse == true)
                    {
                        if (existingProducts.Ratings.Contains(int.Parse(keyword)))
                            keywordsList.Add(existingProducts);

                    }

                }
            }
            return keywordsList;
        }

        [HttpGet("get-products-rating-asc")]
         public List<Products> GetProductsRatingAsc()
         {
             List<Products> orderedlist = _products;
             int avr = 0, count = 0;
             int avr2 = 0, count2 = 0;
             //int avrr=0, avre=0;
             for (int i = 0; i < orderedlist.Count; i++)
             {
        
                 for (int k = 0; k < orderedlist[i].Ratings.Length; k++)
                 {
                     avr = avr + orderedlist[i].Ratings[k];
                     count++;
                 }
                 avr = avr / count;
        
                 for (int j = 0; j < orderedlist.Count; j++)
                 {
                     for (int l = 0; l < orderedlist[j].Ratings.Length; l++)
                     {
                         avr2 = avr2 + orderedlist[j].Ratings[l];
                         count2++;
                     }
                     avr2 = avr2 / count2;
        
                     if (avr2 > avr)
                     {
                         Products temp = orderedlist[i];
                         orderedlist[i] = orderedlist[j];
                         orderedlist[j] = temp;
                     }
                     avr2 = 0; count2 = 0;
                 }
                 avr = 0; count = 0;
             }
             return orderedlist;
         }

        [HttpGet("get-products-rating-desc")]
        public List<Products> GetProductsRatingDesc()
        {
            List<Products> orderedlist = _products;
            int avr = 0, count = 0;
            int avr2 = 0, count2 = 0;
            
                for (int i = 0; i < orderedlist.Count; i++)
                {
                    
                    for (int k = 0; k < orderedlist[i].Ratings.Length; k++)
                    {
                        avr = avr + orderedlist[i].Ratings[k];
                        count++;
                    }
                    avr = avr / count;
        
                    for (int j = 0 ; j < orderedlist.Count; j++)
                    {
                        for (int l = 0; l < orderedlist[j].Ratings.Length; l++)
                        {
                            avr2 = avr2 + orderedlist[j].Ratings[l];
                            count2++;
                        }
                        avr2 = avr2 / count2;
                       
                        if (avr2 < avr)
                        {
                            Products temp = orderedlist[i];
                            orderedlist[i] = orderedlist[j];
                            orderedlist[j] = temp;
                        }
                        avr2 = 0; count2 = 0;
                    }
                    avr = 0; count = 0;
                }
            return orderedlist;
        }

        [HttpGet("get-oldest-product")]
        public List<Products> GetOldestProduct()
        {
            List<Products> oldest = new();
            DateTime max = DateTime.UtcNow;
            foreach (var existingProduct in _products)
            {
                if (existingProduct.CreatedOn < max)
                {
                    max = existingProduct.CreatedOn;
                }
            }
            foreach (var existingProduct in _products)
            {
                if (existingProduct.CreatedOn == max)
                    oldest.Add(existingProduct);
            }
            return oldest;
        }

        [HttpGet("get-newest-product")]
        public List<Products> GetNewestProduct()
        {
            List<Products> newest = new();
            DateTime max = _products[0].CreatedOn;
            bool ok = false;

            foreach (var existingProduct in _products)
            {
                if (ok == false)
                {
                    ok = true;
                    max = existingProduct.CreatedOn;
                }
                else
                {
                    if (existingProduct.CreatedOn > max)
                    {
                        max = existingProduct.CreatedOn;
                    }
                }
            }
            foreach (var existingProduct in _products)
            {
                if (existingProduct.CreatedOn == max)
                    newest.Add(existingProduct);
            }
            return newest;
        }

        [HttpPost]
        public IActionResult CreateProducts([FromBody] Products product)
        {
            if (product == null)
            {
                return BadRequest("Product is null");
            }
            foreach (var existingProduct in _products)
            {
                if (existingProduct.Id == product.Id)
                {
                    return BadRequest("Product with same Id already existing!");
                }
            }
            _products.Add(product);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(Guid id)
        {
            foreach (var existingProduct in _products)
            {
                if (existingProduct.Id == id)
                {
                    _products.Remove(existingProduct);
                    return Ok();
                }
            }
            return NotFound("Product not found!");
        }

        [HttpPut("change-name/{productId}")]
        public IActionResult ChangeName(Guid productId, [FromBody] string name)
        {
            foreach (var existingProduct in _products)
            {
                if (existingProduct.Id == productId)
                {
                    existingProduct.Name = name;
                    return Ok();
                }
            }
            return NotFound("Product not found!");
        }
    }
}

