using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportApi.Modals
{
    public class ReportModals
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CaseId{get;set;}
        [Required]
        public string Status{get;set;}
        [Required]
        public string CaseTitle{get;set;}
        [Required]
        public string Subject{get;set;}
        [Required]
        public string Priority { get;set;}
        [Required]
        public string Origin{get;set;}
        [Required]
        public string Customer{get;set;}
        [Required]
        public string Contact{get;set;}
        [Required]
        public string Product{get;set;}
        [Required]
        public string CaseDescription{get;set;}
        [Required]
        public string Stages{get;set;}
        

    }
}