using Fuji_I.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fuji_I.Controllers
{
    public class ProdDataController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProdDataController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Prod_data()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetWorkOrderDetails(string workOrder)
        {
            var workOrderDetails = _context.work_order_mapping
                .Where(wo => wo.Work_order == workOrder)
                .FirstOrDefault();
            if (workOrderDetails == null)
            {
                Console.WriteLine($"Work Order: {workOrderDetails.Work_order}");
                Console.WriteLine($"Product No: {workOrderDetails.product_no}");
                Console.WriteLine($"Quantity: {workOrderDetails.qty}");
                Console.WriteLine($"UPH: {workOrderDetails.uph}");
                //Console.WriteLine($"Created At: {workOrderDetails.created_at}");
            }
            else
            {
                Console.WriteLine("No work order found with the specified ID.");
            }

            return Json(workOrderDetails);
        }

    }

}
