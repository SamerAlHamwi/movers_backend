using Abp.AutoMapper;
using Mofleet.Domain.Points;
using System.ComponentModel.DataAnnotations;

namespace Mofleet.Points.Dto

{
    /// <summary>
    /// Post Category Translation Dto
    /// </summary>
    [AutoMap(typeof(PointTranslation))]
    public class PointTranslationDto
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
