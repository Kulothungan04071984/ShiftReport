﻿//using Fuji_I.Data;
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

        public IActionResult ProdData()
        {
            // var customerlist=_dataAccess.getCustomerDetails();
            // string curdate = DateTime.Today.ToString();
            string curdate = string.Empty;
            var linedata = _dataAccess.getLineDetails(curdate);
            return View(linedata);
        }

        [HttpPost]
        public IActionResult ViewGrid()
        {
            var linedata = _dataAccess.getLineDetails(string.Empty);
            List<HourlyData> hoursData = new List<HourlyData>();
            HourlyData objHour;
            if (linedata != null && linedata.Count > 0)
            {
                foreach (var line in linedata)
                {
                    objHour = new HourlyData();
                    objHour.Date = Convert.ToDateTime(line.CurrentDate);
                    objHour.Hour = Convert.ToInt32(line.Hour);
                    objHour.Target = Convert.ToInt32(line.Target);
                    objHour.Actual = Convert.ToInt32(line.Actual);
                    hoursData.Add(objHour);

                }
            }

        //    var hourlyData = new List<HourlyData>
        //{
        //    new HourlyData { Date = DateTime.Today, Hour = 0, Actual = 5, Target = 10 },
        //    new HourlyData { Date = DateTime.Today, Hour = 1, Actual = 15, Target = 20 },
        //    new HourlyData { Date = DateTime.Today, Hour = 2, Actual = 10, Target = 30 },
        //    new HourlyData { Date = DateTime.Today, Hour = 3, Actual = 20, Target = 15 },
        //    // Add more data as needed
        //};

            return View(hoursData);
        }

      


    }

}
