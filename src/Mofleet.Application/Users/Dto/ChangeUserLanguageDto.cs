using System.ComponentModel.DataAnnotations;

namespace Mofleet.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}