using Fuji_I.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fuji_I.Controllers
{
    public class MainPageController : Controller
    {
        private readonly Inter_details _inter_Details;
        public MainPageController(Inter_details inter_Details)
        {
            _inter_Details = inter_Details;
        }
        public IActionResult liveData()
        {
           // var data = new int[] { 30, 20, 50 };
            //ViewBag.Data = data;
            return View();
        }

        public IActionResult shiftReport()
        {
            var detils=_inter_Details.GetLineUtilization();
            Utilization objUtilization=new Utilization ();
            var chartData = new List<LineUtilization>
            {
                new LineUtilization { Label = "Production", Value = 10 },
                new LineUtilization { Label = "Wait Previous", Value = 20 },
                new LineUtilization { Label = "Wait Next", Value = 30 },
                new LineUtilization { Label = "Changeover", Value = 40 },
                new LineUtilization { Label = "Part Supply", Value = 80 },
                new LineUtilization { Label = "Mechine Error", Value = 40 },
                new LineUtilization { Label = "Operator Downtime", Value = 20 },
                new LineUtilization { Label = "Maintenance", Value = 70 },
                new LineUtilization { Label = "Others", Value = 10 },
            };

            var data = new List<ModuleUtilization>
        {
            new ModuleUtilization { Label = "Production", Value = 10 },
            new ModuleUtilization { Label = "Wait Previous", Value = 20 },
            new ModuleUtilization { Label = "Wait Next", Value = 30 },
            new ModuleUtilization { Label = "Changeover", Value = 10 },
            new ModuleUtilization { Label = "Part Supply", Value = 20 },
            new ModuleUtilization { Label = "Mechine Error", Value = 30 },
            new ModuleUtilization { Label = "Operator Downtime", Value = 10 },
            new ModuleUtilization { Label = "Maintenance", Value = 20 },
            new ModuleUtilization { Label = "Others", Value = 30 }
        };

            objUtilization.lstLineUtilization = chartData;
            objUtilization.lstModuleUtilization = data;
            return View(objUtilization);
        }
    }
}
