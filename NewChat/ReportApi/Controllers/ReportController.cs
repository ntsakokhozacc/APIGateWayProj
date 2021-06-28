using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ReportApi.Modals;
using ReportApi.Modals.Commands;

namespace ReportApi.Controllers
{
    [ApiController]
    [Route("Report")]
    public class ReportController :ControllerBase
    {
        private ReportSQLCommands reportSQLCommands;

        public ReportController(IConfiguration config)
        {
            reportSQLCommands = new ReportSQLCommands(config);
        }

        [HttpPost]
        [Route("createCase")]
        public async Task<HttpStatusCode> CreateCase(ReportModals report)
        {
            //int CaseId = Convert.ToInt16(report.CaseId);
            string Status = report.Status;
            string CaseTitle = report.CaseTitle;
            string Subject = report.Subject;
            string Priority = report.Priority;
            string Origin = report.Origin;
            string Customer = report.Customer;
            string Contact = report.Contact;
            string Product = report.Product;
            string CaseDescription =report.CaseDescription;
            string Stages = report.Stages;
            
            HttpStatusCode StatusCode = HttpStatusCode.Created;
            DbDataReader ReportReader = null;
            
            try
            {
                ReportReader = await reportSQLCommands.CreateReport(Status,CaseTitle,Subject, Priority, Origin,Customer,Contact,Product,CaseDescription,Stages);

            }catch(Exception ex)
            {
                StatusCode = HttpStatusCode.ExpectationFailed;
                Console.WriteLine(ex);
            }

            await reportSQLCommands.CloseConnection();
            
            return StatusCode;
        }

        [HttpGet]
        [Route("getAll/Reports")]
        public async Task<List<ReportModals>> DisplayAllReports()
        {
            List<ReportModals> Reports = new List<ReportModals>();
            DbDataReader ReportReader = null;
            ReportReader = await reportSQLCommands.DisplayAllReports();
            while(await ReportReader.ReadAsync())
            {
                Reports.Add
                (
                   new ReportModals()
                   {
                       CaseId = Convert.ToInt16(ReportReader.GetValue("CaseId")),
                       Status = ReportReader.GetValue("Status").ToString(),
                       CaseTitle = ReportReader.GetValue("CaseTitle").ToString(),
                       Subject = ReportReader.GetValue("Subject").ToString(),
                       Priority = ReportReader.GetValue("Priority").ToString(),
                       Origin = ReportReader.GetValue("Origin").ToString(),
                       Customer = ReportReader.GetValue("Customer").ToString(),
                       Contact = ReportReader.GetValue("Contact").ToString(),
                       Product = ReportReader.GetValue("Product").ToString(),
                       CaseDescription = ReportReader.GetValue("CaseDescription").ToString(),
                       Stages = ReportReader.GetValue("Stages").ToString()
                   }
                 );
            }
            await reportSQLCommands.CloseConnection();
            /*  var Result = JsonConvert.SerializeObject(Reports);
            List<ReportModals> deserializedList = JsonConvert.DeserializeObject<List<ReportModals>>(Result);*/
            return Reports;
        }

        [HttpGet]
        [Route("getReport/{CaseId}")]
        public async Task<ReportModals> getReport(string CaseId)
        {
            DbDataReader ReportReader = null;

            ReportReader = await reportSQLCommands.DisplayReport(Convert.ToInt32(CaseId));
            ReportModals Report = null;

            while(await ReportReader.ReadAsync())
            {
                Report = (new ReportModals()
                {
                    CaseId = Convert.ToInt16(ReportReader.GetValue("CaseId")),
                    Status = ReportReader.GetValue("Status").ToString(),
                    CaseTitle = ReportReader.GetValue("CaseTitle").ToString(),
                    Subject = ReportReader.GetValue("Subject").ToString(),
                    Priority = ReportReader.GetValue("Priority").ToString(),
                    Origin = ReportReader.GetValue("Origin").ToString(),
                    Customer = ReportReader.GetValue("Customer").ToString(),
                    Contact = ReportReader.GetValue("Contact").ToString(),
                    Product = ReportReader.GetValue("Product").ToString(),
                    CaseDescription = ReportReader.GetValue("CaseDescription").ToString(),
                    Stages = ReportReader.GetValue("Stages").ToString()
                });
            }
            await reportSQLCommands.CloseConnection();
            /*var Result =JsonConvert.SerializeObject(Report);
            ReportModals DeserializationResult = JsonConvert.DeserializeObject<ReportModals>(Result)*/
            return Report;
        }

        [HttpGet]
        [Route("getReports/{Customer}")]
        public async Task<List<ReportModals>> DisplayAllCompanyReports(string Customer)
        {
            List<ReportModals> Reports = new List<ReportModals>();
            DbDataReader ReportReader = null;
            ReportReader = await reportSQLCommands.DisplayAllCompanyReports(Customer);
            while (await ReportReader.ReadAsync())
            {
                Reports.Add
                (
                   new ReportModals()
                   {
                       CaseId = Convert.ToInt16(ReportReader.GetValue("CaseId")),
                       Status = ReportReader.GetValue("Status").ToString(),
                       CaseTitle = ReportReader.GetValue("CaseTitle").ToString(),
                       Subject = ReportReader.GetValue("Subject").ToString(),
                       Priority = ReportReader.GetValue("Priority").ToString(),
                       Origin = ReportReader.GetValue("Origin").ToString(),
                       Customer = ReportReader.GetValue("Customer").ToString(),
                       Contact = ReportReader.GetValue("Contact").ToString(),
                       Product = ReportReader.GetValue("Product").ToString(),
                       CaseDescription = ReportReader.GetValue("CaseDescription").ToString(),
                       Stages = ReportReader.GetValue("Stages").ToString()
                   }
                 );
            }
            await reportSQLCommands.CloseConnection();
            /*  var Result = JsonConvert.SerializeObject(Reports);
            List<ReportModals> deserializedList = JsonConvert.DeserializeObject<List<ReportModals>>(Result);*/
            return Reports;
        }

        [HttpPost]
        [Route("updateStatus/{CaseId}")]
        public async Task<HttpStatusCode>  UpdateStatus(string CaseId,string Status)
        {
            DbDataReader ReportReader = null;
            HttpStatusCode httpStatusCode = HttpStatusCode.Created;
            try
            {
                ReportReader = await reportSQLCommands.UpdateStatus(Convert.ToInt16(CaseId), Status);
            }catch(Exception ex)
            {
                httpStatusCode = HttpStatusCode.ExpectationFailed;
                Console.WriteLine(ex);
            }
            await reportSQLCommands.CloseConnection();
            return httpStatusCode;
        }

        [HttpPost]
        [Route("updateStages/{CaseId}")]
        public async Task<HttpStatusCode> UpdateStage(string CaseId,string Stages)
        {
            DbDataReader ReportReader = null;
            HttpStatusCode httpStatusCode = HttpStatusCode.Created;
            try
            {
                ReportReader = await reportSQLCommands.UpdateStage(Convert.ToInt16(CaseId), Stages);
            }catch(Exception ex)
            {
                httpStatusCode = HttpStatusCode.ExpectationFailed;
                Console.WriteLine(ex);
            }
            await reportSQLCommands.CloseConnection();
            return httpStatusCode;
        }
   
        [HttpGet]
        [Route("Company/DisplayByStages")]
        public async Task<List<ReportModals>> SearchCompanyStage(string Customer,string Stages)
        {
            List<ReportModals> Reports = new List<ReportModals>();
            DbDataReader ReportReader = null;
            ReportReader = await reportSQLCommands.SearchCompanyStage(Customer, Stages);
            while (await ReportReader.ReadAsync())
            {
                Reports.Add
                (
                   new ReportModals()
                   {
                       CaseId = Convert.ToInt16(ReportReader.GetValue("CaseId")),
                       Status = ReportReader.GetValue("Status").ToString(),
                       CaseTitle = ReportReader.GetValue("CaseTitle").ToString(),
                       Subject = ReportReader.GetValue("Subject").ToString(),
                       Priority = ReportReader.GetValue("Priority").ToString(),
                       Origin = ReportReader.GetValue("Origin").ToString(),
                       Customer = ReportReader.GetValue("Customer").ToString(),
                       Contact = ReportReader.GetValue("Contact").ToString(),
                       Product = ReportReader.GetValue("Product").ToString(),
                       CaseDescription = ReportReader.GetValue("CaseDescription").ToString(),
                       Stages = ReportReader.GetValue("Stages").ToString()
                   }
                 );
            }
            await reportSQLCommands.CloseConnection();
          /*  var Result = JsonConvert.SerializeObject(Reports);
            List<ReportModals> deserializedList = JsonConvert.DeserializeObject<List<ReportModals>>(Result);*/
            return Reports;

        }

        //Display Resolved or unResolved cases
        [HttpGet]
        [Route("DisplayByCustomerResolution")]
        public async Task<List<ReportModals>> DisplayByResolution(string Customer,string Resolution)
        {

            List<ReportModals> Reports = new List<ReportModals>();
            DbDataReader ReportReader = null;
            if(Resolution.CompareTo("Resolved") == 0)
            {
                ReportReader = await reportSQLCommands.ResolvedCase(Customer, Resolution);
            }
            else if(Resolution.CompareTo("Resolved") !=0)
            {
                ReportReader = await reportSQLCommands.Unresolved(Customer);
            }
            
            while (await ReportReader.ReadAsync())
            {
                Reports.Add
                (
                   new ReportModals()
                   {
                       CaseId = Convert.ToInt16(ReportReader.GetValue("CaseId")),
                       Status = ReportReader.GetValue("Status").ToString(),
                       CaseTitle = ReportReader.GetValue("CaseTitle").ToString(),
                       Subject = ReportReader.GetValue("Subject").ToString(),
                       Priority = ReportReader.GetValue("Priority").ToString(),
                       Origin = ReportReader.GetValue("Origin").ToString(),
                       Customer = ReportReader.GetValue("Customer").ToString(),
                       Contact = ReportReader.GetValue("Contact").ToString(),
                       Product = ReportReader.GetValue("Product").ToString(),
                       CaseDescription = ReportReader.GetValue("CaseDescription").ToString(),
                       Stages = ReportReader.GetValue("Stages").ToString()
                   }
                 );
            }
            await reportSQLCommands.CloseConnection();

            return Reports;
            
        }


    }
}