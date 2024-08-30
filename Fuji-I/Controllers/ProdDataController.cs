//using Fuji_I.Data;
using Fuji_I.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fuji_I.Controllers
{
    public class ProdDataController : Controller
    {
      //  private readonly ApplicationDbContext _context;

        //public ProdDataController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        //[HttpGet]
        //public IActionResult Prod_data()
        //{
        //    return View();
        //}

        //[HttpGet]
        //public JsonResult GetWorkOrderDetails(string workOrder)
        //{
        //    var workOrderDetails = _context.work_order_mapping
        //        .Where(wo => wo.Workorder_no == workOrder)
        //        .FirstOrDefault();
        //    if (workOrderDetails == null)
        //    {
        //        Console.WriteLine($"Work Order: {workOrderDetails.Workorder_no}");
        //        Console.WriteLine($"Product No: {workOrderDetails.Workorder_no}");
        //        Console.WriteLine($"Quantity: {workOrderDetails.WO_Quantity}");
        //       // Console.WriteLine($"UPH: {workOrderDetails.uph}");
        //        //Console.WriteLine($"Created At: {workOrderDetails.created_at}");
        //    }
        //    else
        //    {
        //        Console.WriteLine("No work order found with the specified ID.");
        //    }

        //    return Json(workOrderDetails);
        //}

        private readonly DataAccess _dataAccess;
        public ProdDataController(DataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public IActionResult Prod_data()
        {
            var customerlist=_dataAccess.getCustomerDetails();
            return View(customerlist);
        }

    }

}
