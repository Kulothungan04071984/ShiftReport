using Fuji_I.Models;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;

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

        //public ActionResult Logout()
        //{
        //    // Clear session data
        //    Session.Clear();      // Clears all session data
        //    Session.Abandon();    // Ends the session

        //    // Optionally, clear server-side output cache for specific page or action
        //    HttpCachePolicy.RemoveOutputCacheItem("/Home/Index");

        //    // Redirect to the login page
        //    return RedirectToAction("Login", "Login");
        //}


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
           // res = "valid"; //Testing
            return Json(res);
        }
        [HttpPost]
        public JsonResult UpdatePassword(string userid, string password)
        {
            try
            {
                var result=_dataAccess.UpdatePassword(userid, password);
                return Json(result);
            }
            catch (Exception ex) { 
                return Json(ex);
            }
        }
    }
}

