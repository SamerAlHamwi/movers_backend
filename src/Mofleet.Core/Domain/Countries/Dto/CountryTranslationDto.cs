using Abp.AutoMapper;
using Mofleet.Domain.Countries;
using System.ComponentModel.DataAnnotations;


namespace Mofleet.Countries.Dto
{
    /// <summary>
    /// Post Category Translation Dto
    /// </summary>
    [AutoMap(typeof(CountryTranslation))]
    public class CountryTranslationDto
    {
        /// <summary>
        /// Name
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Language
        /// </summary>
        [Required]
        public string Language { get; set; }
    }
}
