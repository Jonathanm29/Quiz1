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

    public class OrdersController : Controller
    {

        Common.Listas Listas = new Common.Listas();

        string baseurl = "http://localhost:61186/";


        // GET: Orders
        public async Task<IActionResult> Index()
        {
            List<data.Orders> aux = new List<data.Orders>();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cl.GetAsync("api/Orders");

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<data.Orders>>(auxres);
                }
            }
            return View(aux);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = GetById(id);


            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(Listas.getAllCustomers(), "CustomerId", "Email");
            ViewData["StaffId"] = new SelectList(Listas.getAllStaffs(), "StaffId", "Email");
            ViewData["StoreId"] = new SelectList(Listas.getAllStores(), "StoreId", "StoreName");
            return View();
        }

        //POST: Orders/Create
        //To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,CustomerId,OrderStatus,OrderDate,RequiredDate,ShippedDate,StoreId,StaffId")] data.Orders orders)
        {
            if (ModelState.IsValid)
            {
                using (var cl = new HttpClient())
                {
                    cl.BaseAddress = new Uri(baseurl);
                    var content = JsonConvert.SerializeObject(orders);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var postTask = cl.PostAsync("api/Orders", byteContent).Result;

                    if (postTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            ViewData["CustomerId"] = new SelectList(Listas.getAllCustomers(), "CustomerId", "Email", orders.CustomerId);
            ViewData["StaffId"] = new SelectList(Listas.getAllStaffs(), "StaffId", "Email", orders.StaffId);
            ViewData["StoreId"] = new SelectList(Listas.getAllStores(), "StoreId", "StoreName", orders.StoreId);
            return View(orders);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var orders = GetById(id);
            if (orders == null)
            {
                return NotFound();
            }

            ViewData["CustomerId"] = new SelectList(Listas.getAllCustomers(), "CustomerId", "Email", orders.CustomerId);
            ViewData["StaffId"] = new SelectList(Listas.getAllStaffs(), "StaffId", "Email", orders.StaffId);
            ViewData["StoreId"] = new SelectList(Listas.getAllStores(), "StoreId", "StoreName", orders.StoreId);
            return View(orders);
        }

        //// POST: Orders/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,CustomerId,OrderStatus,OrderDate,RequiredDate,ShippedDate,StoreId,StaffId")] data.Orders orders)
        {
            if (id != orders.OrderId)
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
                        var content = JsonConvert.SerializeObject(orders);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                        var postTask = cl.PutAsync("api/Orders/" + id, byteContent).Result;

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
            ViewData["CustomerId"] = new SelectList(Listas.getAllCustomers(), "CustomerId", "Email", orders.CustomerId);
            ViewData["StaffId"] = new SelectList(Listas.getAllStaffs(), "StaffId", "Email", orders.StaffId);
            ViewData["StoreId"] = new SelectList(Listas.getAllStores(), "StoreId", "StoreName", orders.StoreId);
            return View(orders);
        }

        //// GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = GetById(id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        //// POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cl.DeleteAsync("api/Orders/" + id);

                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction(nameof(Index));
        }


        private bool OrdersExists(int id)
        {
            return (GetById(id)!=null);
        }
        private data.Orders GetById(int? id)
        {
            data.Orders aux = new data.Orders();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = cl.GetAsync("api/Orders/" + id).Result;

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<data.Orders>(auxres);
                }
            }
            return aux;
        }
     
    }
}