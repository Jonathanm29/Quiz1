using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using data = FrontEnd.API.Models;


namespace FrontEnd.API.Controllers
{
    public class StocksController : Controller
    {

        Common.Listas Listas = new Common.Listas();

        string baseurl = "http://localhost:61186/";


        // GET: Stocks
        public async Task<IActionResult> Index()
        {
            List<data.Stocks> aux = new List<data.Stocks>();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cl.GetAsync("api/Stocks");

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<data.Stocks>>(auxres);
                }
            }
            return View(aux);
        }

        // GET: Stocks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stocks = GetById(id);


            if (stocks == null)
            {
                return NotFound();
            }

            return View(stocks);
        }

        // GET: Stocks/Create
        public IActionResult Create()
        {
            //ViewData["CustomerId"] = new SelectList(Listas.getAllCustomers(), "CustomerId", "Email");
            //ViewData["StaffId"] = new SelectList(Listas.getAllStocks(), "StaffId", "Email");
            //ViewData["StoreId"] = new SelectList(Listas.getAllStores(), "StoreId", "StoreName");
            return View();
        }

        //POST: Stocks/Create
        //To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,CustomerId,OrderStatus,OrderDate,RequiredDate,ShippedDate,StoreId,StaffId")] data.Stocks stocks)
        {
            if (ModelState.IsValid)
            {
                using (var cl = new HttpClient())
                {
                    cl.BaseAddress = new Uri(baseurl);
                    var content = JsonConvert.SerializeObject(stocks);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var postTask = cl.PostAsync("api/Stocks", byteContent).Result;

                    if (postTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            //ViewData["CustomerId"] = new SelectList(Listas.getAllCustomers(), "CustomerId", "Email", stocks.CustomerId);
            //ViewData["StaffId"] = new SelectList(Listas.getAllStocks(), "StaffId", "Email", stocks.StaffId);
            //ViewData["StoreId"] = new SelectList(Listas.getAllStores(), "StoreId", "StoreName", stocks.StoreId);
            return View(stocks);
        }

        // GET: Stocks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var stocks = GetById(id);
            if (stocks == null)
            {
                return NotFound();
            }

            //ViewData["CustomerId"] = new SelectList(Listas.getAllCustomers(), "CustomerId", "Email", stocks.CustomerId);
            //ViewData["StaffId"] = new SelectList(Listas.getAllStocks(), "StaffId", "Email", stocks.StaffId);
            //ViewData["StoreId"] = new SelectList(Listas.getAllStores(), "StoreId", "StoreName", stocks.StoreId);
            return View(stocks);
        }

        //// POST: Stocks/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,CustomerId,OrderStatus,OrderDate,RequiredDate,ShippedDate,StoreId,StaffId")] data.Stocks stocks)
        {
            if (id != stocks.StoreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    using (var cl = new HttpClient())
                    {
                        cl.BaseAddress = new Uri(baseurl);
                        var content = JsonConvert.SerializeObject(stocks);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                        var postTask = cl.PutAsync("api/Stocks/" + id, byteContent).Result;

                        if (postTask.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
                catch (Exception)
                {
                    var aux2 = GetById(id);
                    if (aux2 == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CustomerId"] = new SelectList(Listas.getAllCustomers(), "CustomerId", "Email", stocks.CustomerId);
            //ViewData["StaffId"] = new SelectList(Listas.getAllStocks(), "StaffId", "Email", stocks.StaffId);
            //ViewData["StoreId"] = new SelectList(Listas.getAllStores(), "StoreId", "StoreName", stocks.StoreId);
            return View(stocks);
        }

        //// GET: Stocks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stocks = GetById(id);
            if (stocks == null)
            {
                return NotFound();
            }

            return View(stocks);
        }

        //// POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cl.DeleteAsync("api/Stocks/" + id);

                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction(nameof(Index));
        }


        private bool StocksExists(int id)
        {
            return (GetById(id) != null);
        }
        private data.Stocks GetById(int? id)
        {
            data.Stocks aux = new data.Stocks();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = cl.GetAsync("api/Stocks/" + id).Result;

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<data.Stocks>(auxres);
                }
            }
            return aux;
        }

    }
}