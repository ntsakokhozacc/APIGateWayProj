using System.Collections.Generic;

namespace ReportApi.Configuration
{
    public class AuthResult
    {
       public string Token {get;set;}

        public bool TokenSuccess{get;set;}

        public List<string> Errors {get;set;}
         
    }
}