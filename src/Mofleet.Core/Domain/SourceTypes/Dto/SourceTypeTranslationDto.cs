using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Mofleet.Domain.SourceTypes.Dto
{
    [AutoMap(typeof(SourceTypeTranslation))]
    public class SourceTypeTranslationDto
    {
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Language
        /// </summary>
        [Required]
        public string Language { get; set; }
    }
}
