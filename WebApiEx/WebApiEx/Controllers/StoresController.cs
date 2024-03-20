using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Xml.Linq;
using WebApiEx.Models;

namespace WebApiEx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        public static readonly List<Store> _stores = new List<Store>
        {
            new Store
            {
                Id = Guid.NewGuid(),
                Name = "Store 1",
                Country = " Country 1 ",
                City = "City 1",
                MonthlyIncome = 1,
                OwnerName = "Owner 1",
                ActiveSince = DateTime.UtcNow,
            },
            new Store
            {
                Id = Guid.NewGuid(),
                Name = "Store 2",
                Country = " Country 2 ",
                City = "Coty 2",
                MonthlyIncome = 2,
                OwnerName = "Owner 2",
                ActiveSince = DateTime.UtcNow,
            }
        };

        [HttpGet]
        public Store[] GetAllStores()
        {
            return _stores.ToArray();
        }

        [HttpGet("get-stores-keyword/{keyword}")]
        public List<Store> GetStoresKeyword( string keyword)
        {
            List<Store> keywordsList = new();
            foreach (var existingStore in _stores)
            {
                if (keyword.Equals(existingStore.Name) || keyword.Equals(existingStore.Country) || keyword.Equals(existingStore.City) || keyword.Equals(existingStore.OwnerName)   )
                {
                    keywordsList.Add(existingStore);
                }
            }
            return keywordsList;
        }

        [HttpGet("get-stores-country/{kcountry}")]
        public List<Store> GetStoresCountry( string kcountry)
        {
            List<Store> countrylist = new();

            foreach (var existingStore in _stores)
            {
                if (kcountry.Equals(existingStore.Country))
                {
                  countrylist.Add(existingStore);
                }
            }
            return countrylist;
        }

        [HttpGet("get-stores-city/{kcity}")]
        public List<Store> GetStoresCity(string kcity)
        {
            List<Store> citylist = new();
            foreach (var existingStore in _stores)
            {
                if (kcity.Equals(existingStore.City))
                {
                    citylist.Add(existingStore);
                }
            }
            return citylist;
        }

        [HttpGet("get-stores-income")]
        public List<Store> GetStoresIncome()
        {
          
          List<Store> list = new List < Store > (_stores);
          List<Store> lista = new();

          for (int i = 0; i < list.Count; i++)
          {
              for (int j = 0; j < list.Count; j++)
              {
                    if (list[i].MonthlyIncome > list[j].MonthlyIncome)
                    {
                        Store temp = list[i];
                        list[j] = list[i];
                        list[i] = temp;
                    }
              }
          }
            return list;
        }

        [HttpGet("get-oldest-store")]
        public List<Store> GetOldestStore()
        {
            List<Store> oldest = new();
            DateTime max = DateTime.UtcNow;
            foreach (var existingStore in _stores)
            {
                if (existingStore.ActiveSince < max)
                {
                    max = existingStore.ActiveSince;
                }
            }
            foreach (var existingStore in _stores)
            {
                if(existingStore.ActiveSince == max)
                    oldest.Add(existingStore);
            }
             return oldest;
            
        }

        [HttpPost]
        public IActionResult CreateStore([FromBody]Store store)
        {
            if(store == null) 
            {
                return BadRequest("Store is null");
            }
            foreach (var existingStore in _stores)
            {
                if(existingStore.Id == store.Id) 
                {
                    return BadRequest("Store with same Id already existing!");
                }
            }
            _stores.Add(store);
            return Ok(store);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStore(Guid id)
        {
            
            foreach (var existingStore in _stores)
            {
                if (existingStore.Id == id)
                {
                    _stores.Remove(existingStore);
                    return Ok();
                }
            }
            return NotFound("Store not found!");
        }


        [HttpPut("transfer-ownership/{storeId}")]
        public IActionResult TransferOwnership(Guid storeId,[FromBody] string name)
        {

            foreach (var existingStore in _stores)
            {
                if (existingStore.Id == storeId)
                {
                    existingStore.OwnerName = name;
                    return Ok();
                }
            }
            return NotFound("Store not found!");
        }

    }
}
