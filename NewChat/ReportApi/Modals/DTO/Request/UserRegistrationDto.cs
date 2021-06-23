using System.ComponentModel.DataAnnotations;

namespace ReportApi.Modals.DTO.Request
{
    public class UserRegistrationDto
    {
        [Required]
        public string userName{get;set;}

        [Required]
        [EmailAddress]
        public string Email {get;set;}

        [Required]
        public string Password{get;set;}

        [Required]
        public string Roles{get;set;}
    }
}