using System.ComponentModel.DataAnnotations;

namespace basurapp.api.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string realName { get; set; }
        [Required]
        public string lastName { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage="You must set a password with 8 or more characters")]        
        public string password { get; set; }
        public string phone { get; set; }
        [Required]
        public int role { get; set; }    

    }
}