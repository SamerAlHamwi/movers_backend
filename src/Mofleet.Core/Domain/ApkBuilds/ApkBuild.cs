using Abp.Domain.Entities.Auditing;
using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.ApkBuilds
{
    public class ApkBuild : FullAuditedEntity
    {
        public AppType AppType { get; set; }
        public SystemType SystemType { get; set; }
        public int VersionCode { get; set; }
        public string VersionNumber { get; set; }
        public string Description { get; set; }
        public UpdateOptions UpdateOptions { get; set; }

    }
}
