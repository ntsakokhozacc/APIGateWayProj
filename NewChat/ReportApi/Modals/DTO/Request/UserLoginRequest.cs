using System.ComponentModel.DataAnnotations;

namespace ReportApi.Modals.DTO.Request
{
    public class UserLoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email{get;set;}
        [Required]
        public string Password{get;set;}

        //trying to add roles
       // [Required]
       // public string Roles{get;set;}

       // public UserRole UserRole { get; set; }

    }
}