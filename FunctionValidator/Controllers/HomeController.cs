using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Authorization;
using System.Data.SqlClient;
using System.Dynamic;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace FunctionValidator.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment _env;

        public HomeController(IHostingEnvironment env)
        {
            _env = env;
        }

        [Route("api/home")]
        public IActionResult Index()
        {
            string siteFolder = string.Empty;
            int fileCount;

            if (Environment.GetEnvironmentVariable("home") != null)
            {
                // Maps to the physical path of your site in Azure
                siteFolder =
                    Environment.ExpandEnvironmentVariables(@"%HOME%\site\wwwroot");
            }
            else
            {
                // Maps to the current sites root physical path.
                // Allows us to run locally.
            //    siteFolder = Server.MapPath("/");
             siteFolder =  _env.ContentRootPath;
            }

            fileCount =
                Directory.GetFiles(
                    siteFolder,
                    "*.*",
                    SearchOption.AllDirectories).Length;

            return View(model: fileCount);
        }
       
        
    }
}