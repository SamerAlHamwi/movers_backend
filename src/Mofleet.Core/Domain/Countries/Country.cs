using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Mofleet.Domain;
using Mofleet.Domain.Cities;
using Mofleet.Domain.Countries;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Mofleet.Enums.Enum;

namespace Mofleet.Countries
{
    public class Country : FullAuditedEntity, IActiveEntity, IMultiLingualEntity<CountryTranslation>
    {
        public Country()
        {

            Cities = new HashSet<City>();
            Translations = new HashSet<CountryTranslation>();
        }

        [Required]
        [StringLength(5)]
        public string DialCode { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        public ICollection<CountryTranslation> Translations { get; set; }
        public bool IsActive { get; set; }
        public ServiceType Type { get; set; }
    }
}
