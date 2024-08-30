using Fuji_I.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fuji_I.Controllers
{
    public class LoginController : Controller
    {
        private readonly DataAccess _dataAccess;
        public LoginController(DataAccess dataAccess)
        {
            _dataAccess = dataAccess;
            
        }
        public IActionResult Login()
        {
            return View();
        }

        public JsonResult loginValidation(string userid,string password)
        {
            string res = string.Empty;
            var result = _dataAccess.getLoginDetails(userid, password);
            if (result != null)
            {
                if (result.Rows.Count > 0)
                {
                    if (result.Rows[0][0].ToString() == userid && result.Rows[0][1].ToString() == password)
                    {
                        res = "valid";
                    }
                    else
                        res = "invalid";
                }
                else
                    res = "invalid";
                
            }
            else if (result == null)
                res = "invalid";

            return Json(res);
        }
    }
}
