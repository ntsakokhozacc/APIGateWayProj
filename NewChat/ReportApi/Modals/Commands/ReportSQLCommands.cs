using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace ReportApi.Modals.Commands
{
    public class ReportSQLCommands
    {
        private SqlConnection connection =null;
        private SqlCommand SqlCommand = null;

        public ReportSQLCommands(IConfiguration configuration)
        {
             connection = new SqlConnection(configuration.GetSection("ConnectionStrings").GetSection("Default").Value);
        }

        public async Task<DbDataReader> CreateReport(string Status,string CaseTitle,string Subject,string Priority, string Origin,string Customer,string Contact,string Product,string CaseDescription,string Stages)
        {
            await connection.OpenAsync();
            SqlCommand = new SqlCommand("INSERT INTO reports(Status,CaseTitle,Subject,Priority,Origin,Customer,Contact,Product,CaseDescription,Stages)VALUES('" + Status+"','"+CaseTitle+"','"+Subject+"','"+Priority+"','"+Origin+"','"+Customer+"','"+Contact+"','"+Product+"','"+CaseDescription+"','"+Stages+"')",connection);
            var Result = await SqlCommand.ExecuteReaderAsync();
            return Result;
        }
        //Shows all reports
        public async Task<DbDataReader> DisplayAllReports()
        {
            await connection.OpenAsync();
            List<ReportModals> reports = new List<ReportModals>();
            SqlCommand = new SqlCommand("SELECT * FROM reports", connection);
            var Result = await SqlCommand.ExecuteReaderAsync();
            return Result;
        }
        // Show single report
        public async Task<DbDataReader> DisplayReport(int CaseId)
        {
            await connection.OpenAsync();
            SqlCommand = new SqlCommand("SELECT * FROM reports WHERE CaseId='" + CaseId + "'", connection);
            var Result = await SqlCommand.ExecuteReaderAsync();
            return Result;
        }
        //Show reports based on company
        public async Task<DbDataReader> DisplayAllCompanyReports(string Customer)
        {
            await connection.OpenAsync();
            List<ReportModals> CompanyReports = new List<ReportModals>();
            SqlCommand = new SqlCommand("SELECT * FROM reports WHERE Customer ='" + Customer + "'", connection);
            var Result = await SqlCommand.ExecuteReaderAsync();
            return Result;
        }
        //Update status of case
        public async Task<DbDataReader> UpdateStatus(int CaseId,string Status)
        {
            await connection.OpenAsync();
            SqlCommand = new SqlCommand("UPDATE reports SET Status='" + Status + "'WHERE CaseId='" + CaseId + "'", connection);
            var Result = await SqlCommand.ExecuteReaderAsync();
            return Result;
        }
        //Update stages
        public async Task<DbDataReader> UpdateStage(int CaseId,string Stages)
        {
            await connection.OpenAsync();
            SqlCommand = new SqlCommand("UPDATE reports SET Stages='" + Stages + "'WHERE CaseId='" + CaseId + "'", connection);
            var Result = await SqlCommand.ExecuteReaderAsync();
            return Result;
        }
      public async Task<DbDataReader> SearchCompanyStage(string Customer,string Stage)
        {
            await connection.OpenAsync();
            List<ReportModals> Reports = new List<ReportModals>();
            SqlCommand = new SqlCommand("SELECT * FROM reports WHERE Customer='" + Customer + "'AND Stages='" + Stage + "'", connection);
            var Result = await SqlCommand.ExecuteReaderAsync();
            return Result;
        }

        public async Task<DbDataReader> ResolvedCase(string Customer,string Stages)
        {
            await connection.OpenAsync();
            List<ReportModals> Reports = new List<ReportModals>();
            SqlCommand = new SqlCommand("SELECT * FROM reports WHERE Customer ='"+Customer+"'AND Stages = '" + Stages + "'", connection);
            var Result = await SqlCommand.ExecuteReaderAsync();
            return Result;
        }

        public async Task<DbDataReader> Unresolved(string Customer)
        {
            await connection.OpenAsync();
            List<ReportModals> Reports = new List<ReportModals>();
            SqlCommand = new SqlCommand("SELECT * FROM reports WHERE Customer = '"+Customer+"'AND Stages NOT LIKE  'Resolved' ", connection);
            var Result = await SqlCommand.ExecuteReaderAsync();
            return Result;
        }
        public async Task CloseConnection()
        {
            await connection.CloseAsync();
        }

       
    }
}