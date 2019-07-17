using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;

namespace FunctionValidator.Services
{
    public class AccountKeyValidator
    {
        //https://msazure.visualstudio.com/One/_git/AzureStack-Services-Storage?path=%2Fsrc%2Fsdx%2Fbase%2Fwoss%2Ftest%2Ftests%2FWFE%2FHorizontalBVT%2FAccountKeyTests.cs&version=GBmaster&_a=contents
        /// <summary>
        /// Make a couple of requests to the blob, queue, and table services, expecting success.
        /// </summary>
        /// <param name="account">The account through which the services will be accessed.</param>
        internal void MakeServiceRequestsExpectSuccess(CloudStorageAccount account)
        {
            // Make blob service requests
            CloudBlobClient blobClient = account.CreateCloudBlobClient();
         //   blobClient.ListContainersSegmentedAsync();
            blobClient.ListContainers().Count();
            blobClient.GetServiceProperties();

            // Make queue service requests
            CloudQueueClient queueClient = account.CreateCloudQueueClient();
            queueClient.ListQueues().Count();
            queueClient.GetServiceProperties();

            // Make table service requests
            CloudTableClient tableClient = account.CreateCloudTableClient();
            tableClient.ListTables().Count();
            tableClient.GetServiceProperties();
        }
    }
}
