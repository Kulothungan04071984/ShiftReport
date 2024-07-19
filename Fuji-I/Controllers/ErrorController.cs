using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

public class ErrorController : Controller
{
    private readonly ILogger<ErrorController> _logger;

    public ErrorController(ILogger<ErrorController> logger)
    {
        _logger = logger;
    }

    [Route("/Error")]
    public IActionResult Error()
    {
        var exceptionHandlerPathFeature =
            HttpContext.Features.Get<IExceptionHandlerPathFeature>();

        _logger.LogError($"The path {exceptionHandlerPathFeature?.Path} threw an exception " +
            $"{exceptionHandlerPathFeature?.Error}");

        // Check if exceptionHandlerPathFeature is null
        if (exceptionHandlerPathFeature?.Path == null || exceptionHandlerPathFeature?.Error == null)
        {
            // Log the issue
            _logger.LogError("ExceptionHandlerPathFeature or its properties are null.");

            // Redirect or return a generic error view
            return RedirectToAction("Index", "Home"); // Example redirect to Home/Index
        }

        return View("Error"); // Assuming you have an Error.cshtml view
    }
}


//using Microsoft.AspNetCore.Diagnostics;
//using Microsoft.AspNetCore.Mvc;
//using Serilog;

//public class ErrorController : Controller
//{
//    private readonly Serilog.ILogger _logger;

//    public ErrorController()
//    {
//        _logger = Log.ForContext<ErrorController>();
//    }

//    [Route("/Error")]
//    public IActionResult Error()
//    {
//        var exceptionHandlerPathFeature =
//            HttpContext.Features.Get<IExceptionHandlerPathFeature>();

//        _logger.Error($"The path {exceptionHandlerPathFeature?.Path} threw an exception: " +
//            $"{exceptionHandlerPathFeature?.Error}");

//        // Check if exceptionHandlerPathFeature is null
//        if (exceptionHandlerPathFeature?.Path == null || exceptionHandlerPathFeature?.Error == null)
//        {
//            // Log the issue
//            _logger.Error("ExceptionHandlerPathFeature or its properties are null.");

//            // Redirect or return a generic error view
//            return RedirectToAction("Index", "Home"); // Example redirect to Home/Index
//        }

//        return View("Error"); // Assuming you have an Error.cshtml view
//    }
//}





