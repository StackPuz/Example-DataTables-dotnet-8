using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using App.Models;

namespace App.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("api/products")]
        public async Task<IActionResult> Index()
        {
            int size = Request.Query["length"].Any() ? Convert.ToInt32(Request.Query["length"]) : 10;
            int start = Convert.ToInt32(Request.Query["start"]);
            string order = Request.Query["order[0][column]"].Any() ? Request.Query["columns[" + Request.Query["order[0][column]"] + "][data]"] : "Id";
            string direction = Request.Query["order[0][dir]"].Any() ? Request.Query["order[0][dir]"] : "asc";
            var query = _context.Product.Select(e => new {
                Id = e.Id,
                Name = e.Name,
                Price = e.Price
            });
            if (!String.IsNullOrEmpty(Request.Query["search[value]"])) {
                query = query.Where(e => e.Name.Contains(Request.Query["search[value]"]));
            }
            query = query.OrderBy(order, direction);
            int recordsTotal = await _context.Product.CountAsync();
            int recordsFiltered = await query.CountAsync();
            var data = await query.Skip(start).Take(size).ToListAsync();
            return Ok(new { draw = Request.Query["draw"].First(), recordsTotal, recordsFiltered, data });
        }
    }
}