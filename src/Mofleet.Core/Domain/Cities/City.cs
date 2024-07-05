using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Mofleet.Countries;
using Mofleet.Domain.Companies;
using Mofleet.Domain.CompanyBranches;
using Mofleet.Domain.Partners;
using Mofleet.Domain.Regions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mofleet.Domain.Cities
{
    //city model
    public class City : FullAuditedEntity, IActiveEntity, IMultiLingualEntity<CityTranslation>
    {
        public City()
        {
            Translations = new HashSet<CityTranslation>();
        }
        public int CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public virtual Country Country { get; set; }
        public ICollection<CityTranslation> Translations { get; set; }
        public virtual ICollection<Region> Regions { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<CompanyBranch> CompanyBranches { get; set; }
        public virtual ICollection<Partner> Partners { get; set; }
        public bool IsActive { get; set; }
    }
}
