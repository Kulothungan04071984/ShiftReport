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

        public IActionResult cycletime()
        {
            var data = new List<lane>
            {
                new lane { lanename="AIMEX-llC_1", label="M1",AIMEX_llC_1_M1=20 ,AIMEX_llC_1_M2=30,AIMEX_llC_2_M1=40,AIMEX_llC_2_M2=50,AIMEX_llC_3_M1=60,AIMEX_llC_3_M2=70,linevalue=20},
                new lane { lanename="AIMEX-llC_1", label="M2", AIMEX_llC_1_M1=20 ,AIMEX_llC_1_M2=30,AIMEX_llC_2_M1=40,AIMEX_llC_2_M2=50,AIMEX_llC_3_M1=60,AIMEX_llC_3_M2=70,linevalue=60},
                new lane { lanename="AIMEX-llC_2", label="M1", AIMEX_llC_1_M1=20 ,AIMEX_llC_1_M2=30,AIMEX_llC_2_M1=40,AIMEX_llC_2_M2=50,AIMEX_llC_3_M1=60,AIMEX_llC_3_M2=70,linevalue=40},
                new lane { lanename="AIMEX-llC_2", label="M2", AIMEX_llC_1_M1=20 ,AIMEX_llC_1_M2=30,AIMEX_llC_2_M1=40,AIMEX_llC_2_M2=50,AIMEX_llC_3_M1=60,AIMEX_llC_3_M2=70, linevalue = 70},
                new lane {lanename="AIMEX-llC_3",label="M1", AIMEX_llC_1_M1=20 ,AIMEX_llC_1_M2=30,AIMEX_llC_2_M1=40,AIMEX_llC_2_M2=50,AIMEX_llC_3_M1=60,AIMEX_llC_3_M2=70, linevalue = 30},
                new lane {lanename="AIMEX-llC_3", label="M2", AIMEX_llC_1_M1=20 ,AIMEX_llC_1_M2=30,AIMEX_llC_2_M1=40,AIMEX_llC_2_M2=50,AIMEX_llC_3_M1=60,AIMEX_llC_3_M2=70, linevalue = 20}

            };
            return View(data);
        }

        public IActionResult linewithbar()
        {
            var salesData = new List<SalesData>
            {
                //new SalesData { Month = "January", Sales = 2000, Revenue = 2000 },
                //new SalesData { Month = "February", Sales = 3000, Revenue = 3000 },
                //new SalesData { Month = "March", Sales = 1500, Revenue = 1500 },
                //new SalesData { Month = "April", Sales = 5000, Revenue = 5000 },
                //new SalesData { Month = "May", Sales = 3500, Revenue = 3500 },
                //new SalesData { Month = "June", Sales = 500, Revenue = 500 }

                new SalesData { Category = "Electronics", Year = 2020, Sales = 100 },
                new SalesData { Category = "Electronics", Year = 2021, Sales = 120 },
                new SalesData { Category = "Electronics", Year = 2022, Sales = 150 },
                new SalesData { Category = "Furniture", Year = 2020, Sales = 80 },
                new SalesData { Category = "Furniture", Year = 2021, Sales = 90 },
                new SalesData { Category = "Furniture", Year = 2022, Sales = 100 },
                new SalesData { Category = "Clothing", Year = 2020, Sales = 150 },
                new SalesData { Category = "Clothing", Year = 2021, Sales = 170 },
                new SalesData { Category = "Clothing", Year = 2022, Sales = 200 }

            };

            var categories = salesData.Select(d => d.Category).Distinct().ToList();
            var years = salesData.Select(d => d.Year).Distinct().ToList();

            var dataGroupedByYear = years.Select(year => new SalesDataGroup
            {
                Year = year,
                SalesByCategory = categories.Select(category => salesData.FirstOrDefault(d => d.Category == category && d.Year == year)?.Sales ?? 0).ToList()
            }).ToList();

            var viewModel = new StackedBarChartViewModel
            {
                Categories = categories,
                Years = years,
                DataGroupedByYear = dataGroupedByYear
            };


            return View(viewModel);
        }

    }
}
