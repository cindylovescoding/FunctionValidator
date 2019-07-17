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
using System.Collections;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

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
            // https://cindydedicatedfn.scm.azurewebsites.net/functionValidator/api/home
            string siteFolder = string.Empty;
            IDictionary appsettings = Environment.GetEnvironmentVariables();
            
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



            return View(model: appsettings);
        }

        private void validateStorageAccount(object sender, EventArgs e)
        {
            try
            {
                //string accountName = tbAccountName.Text;
                //string accountKey = tbAccountKey.Text;

                var storageCredentialsAccountAndKey = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
                CloudStorageAccount csa = CloudStorageAccount.Parse(storageCredentialsAccountAndKey);
                var cloudTableClient = csa.CreateCloudTableClient();

               CloudBlobClient blobClient = csa.CreateCloudBlobClient();
                //   blobClient.ListContainersSegmentedAsync();
                blobClient.ListContainers().Count();


                IEnumerable<string> tableNames = cloudTableClient.ListTables();
                int totaTables = tableNames.Count();
                MessageBox.Show("All is well");
            }
            catch (Exception excep)
            {
                MessageBox.Show("Something is wrong");
            }
        }

    }
}