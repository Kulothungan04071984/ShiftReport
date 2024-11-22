//using Fuji_I.Data;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Vml;
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
            string curdate = DateOnly.FromDateTime(DateTime.Now).ToString("dd-MM-yyyy"); 

            var linedata = _dataAccess.getLineDetails(curdate);
            return View(linedata);
        }

        [HttpPost]
        public IActionResult ViewGrid()
        {
            string curdate = DateOnly.FromDateTime(DateTime.Now).ToString("dd-MM-yyyy");
            var linedata = _dataAccess.getLineDetails(curdate);
            List<HourlyData> hoursData = new List<HourlyData>();
            HourlyData objHour;
            if (linedata != null && linedata.Count > 0)
            {
                foreach (var line in linedata)
                {
                    objHour = new HourlyData();
                    objHour.CurrentDate= line.CurrentDate;
                    objHour.Hour = Convert.ToInt32(line.Hour);
                    objHour.WorkOrder=line.WorkOrder;
                    objHour.FG_Name=line.FG_Name;
                    //objHour.Target = Convert.ToInt32(line.Target);
                    objHour.PCB_COUNT = Convert.ToInt32(line.PCB_COUNT);
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


        public IActionResult WorkOrder()
        {
            // var customerlist=_dataAccess.getCustomerDetails();
            // string curdate = DateTime.Today.ToString();
            string curdate = DateOnly.FromDateTime(DateTime.Now).ToString("dd-MM-yyyy");
            var linedetailsdata = _dataAccess.getWorkOrderDetails(curdate);
            return View(linedetailsdata);
        }

        public IActionResult Dailyreport()
        {
            // var customerlist=_dataAccess.getCustomerDetails();
            // string curdate = DateTime.Today.ToString();
            string curdate = DateOnly.FromDateTime(DateTime.Now).ToString("dd-MM-yyyy");
            var DailyOutputdata = _dataAccess.getDailyOutputDetails(curdate);
            return View(DailyOutputdata);
        }

        [HttpPost]
        public IActionResult ViewGrid2()
        {
            string curdate = DateOnly.FromDateTime(DateTime.Now).ToString("dd-MM-yyyy");
            var DailyOutputdata = _dataAccess.getDailyOutputDetails(curdate);
            List<Dailyreport> Dailydata = new List<Dailyreport>();
            Dailyreport objdaily;
            if (DailyOutputdata != null && DailyOutputdata.Count > 0)
            {
                foreach (var data in DailyOutputdata)
                {
                    objdaily = new Dailyreport();
                    objdaily.FG_Name = (data.FG_Name).ToString();
                    objdaily.CurrentDate = (data.CurrentDate).ToString();
                    objdaily.PCB_COUNT = Convert.ToInt32(data.PCB_COUNT);
                    Dailydata.Add(objdaily);

                }
            }
            return View(Dailydata);

        }

    }
}
