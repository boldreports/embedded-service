using BoldReports.Web.ReportViewer;
using BoldReports.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace boldreportsservice
{
    [Route("api/[controller]/[action]")]
    [Microsoft.AspNetCore.Cors.EnableCors("AllowAllOrigins")]
    public class ReportViewerController : ControllerBase, IReportController, IReportLogger
    {
        // Report viewer requires a memory cache to store the information of consecutive client request and
        // have the rendered report viewer information in server.
        private Microsoft.Extensions.Caching.Memory.IMemoryCache _cache;

        // IHostingEnvironment used with sample to get the application data from wwwroot.
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        // Post action to process the report from server based json parameters and send the result back to the client.
        public ReportViewerController(Microsoft.Extensions.Caching.Memory.IMemoryCache memoryCache,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _cache = memoryCache;
            _hostingEnvironment = hostingEnvironment;
        }

        // Post action to process the report from server based json parameters and send the result back to the client.
        [HttpPost]
        public object PostReportAction([FromBody] Dictionary<string, object> jsonArray)
        {
            return ReportHelper.ProcessReport(jsonArray, this, this._cache);
        }

        // Method will be called to initialize the report information to load the report with ReportHelper for processing.
        [NonAction]
        public void OnInitReportOptions(ReportViewerOptions reportOption)
        {
            string basePath = _hostingEnvironment.WebRootPath;
            // Here, we have loaded the sales-order-detail.rdl report from application the folder wwwroot\Resources. sales-order-detail.rdl should be there in wwwroot\Resources application folder.
            System.IO.FileStream reportStream = new System.IO.FileStream(basePath + @"\Reports\" + reportOption.ReportModel.ReportPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            reportOption.ReportModel.Stream = reportStream;
        }

        // Method will be called when reported is loaded with internally to start to layout process with ReportHelper.
        [NonAction]
        public void OnReportLoaded(ReportViewerOptions reportOption)
        {
        }

        //Get action for getting resources from the report
        [ActionName("GetResource")]
        [AcceptVerbs("GET")]
        // Method will be called from Report Viewer client to get the image src for Image report item.
        public object GetResource(ReportResource resource)
        {
            return ReportHelper.GetResource(resource, this, _cache);
        }

        [HttpPost]
        public object PostFormReportAction()
        {
            return ReportHelper.ProcessReport(null, this, _cache);
        }

        public void LogError(string message, Exception exception, MethodBase methodType, ErrorType errorType)
        {
            WriteLogs(string.Format("Error Message: {0} \n Stack Trace: {1}", message, exception.StackTrace));
        }

        public void LogError(string errorCode, string message, Exception exception, string errorDetail, string methodName, string className)
        {
            WriteLogs(string.Format("Class Name: {0} \n Method Name: {1} \n Error Message: {2} \n Stack Trace: {3}", className, methodName, errorDetail, exception.StackTrace));
        }

        internal void WriteLogs(string errorMessage)
        {
            // Error details text file path location
            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "ErrorDetails.txt");
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.AutoFlush = true;
                writer.WriteLine(errorMessage);
            }
        }
    }
}
