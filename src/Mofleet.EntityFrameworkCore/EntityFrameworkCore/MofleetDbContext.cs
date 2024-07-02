using Abp.Zero.EntityFrameworkCore;
using KeyFinder.Domain.ChangedPhoneNumber;
using Microsoft.EntityFrameworkCore;
using Mofleet.Authorization.Roles;
using Mofleet.Authorization.Users;
using Mofleet.Countries;
using Mofleet.Domain.Advertisiments;
using Mofleet.Domain.ApkBuilds;
using Mofleet.Domain.AskForHelps;
using Mofleet.Domain.Attachments;
using Mofleet.Domain.AttributeAndAttachments;
using Mofleet.Domain.AttributeAndAttachmentsForDrafts;
using Mofleet.Domain.AttributeChoices;
using Mofleet.Domain.AttributeForSourceTypeValuesForDrafts;
using Mofleet.Domain.AttributeForSourcTypeValues;
using Mofleet.Domain.AttributesForSourceType;
using Mofleet.Domain.Cities;
using Mofleet.Domain.Codes;
using Mofleet.Domain.CommissionGroups;
using Mofleet.Domain.Companies;
using Mofleet.Domain.CompanyBranches;
using Mofleet.Domain.ContactUses;
using Mofleet.Domain.Countries;
using Mofleet.Domain.Drafts;
using Mofleet.Domain.Drivers;
using Mofleet.Domain.FrequentlyQuestions;
using Mofleet.Domain.Mediators;
using Mofleet.Domain.MoneyTransfers;
using Mofleet.Domain.Offers;
using Mofleet.Domain.PaidRequestPossibles;
using Mofleet.Domain.Partners;
using Mofleet.Domain.Points;
using Mofleet.Domain.PointsValues;
using Mofleet.Domain.PrivacyPolicies;
using Mofleet.Domain.PushNotifications;
using Mofleet.Domain.Regions;
using Mofleet.Domain.RegisterdPhoneNumbers;
using Mofleet.Domain.RejectReasons;
using Mofleet.Domain.RequestForQuotationContacts;
using Mofleet.Domain.RequestForQuotationContactsForDrafts;
using Mofleet.Domain.RequestForQuotations;
using Mofleet.Domain.Reviews;
using Mofleet.Domain.SearchedPlacesByUsers;
using Mofleet.Domain.SelectedCompaniesByUsers;
using Mofleet.Domain.services;
using Mofleet.Domain.ServiceValueForOffers;
using Mofleet.Domain.ServiceValues;
using Mofleet.Domain.ServiceValuesForDrafts;
using Mofleet.Domain.SourceTypes;
using Mofleet.Domain.SubServices;
using Mofleet.Domain.Terms;
using Mofleet.Domain.TimeWorks;
using Mofleet.Domain.Toolss;
using Mofleet.Domain.Trucks;
using Mofleet.Domains.UserVerficationCodes;
using Mofleet.MultiTenancy;

namespace Mofleet.EntityFrameworkCore
{
    public class MofleetDbContext : AbpZeroDbContext<Tenant, Role, User, MofleetDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<CountryTranslation> CountryTranslations { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<CityTranslation> CityTranslations { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<RegionTranslation> RegionTranslations { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<PushNotification> PushNotifications { get; set; }
        public virtual DbSet<UserVerficationCode> UserVerficationCodes { get; set; }
        public virtual DbSet<RegisterdPhoneNumber> RegisterdPhoneNumbers { get; set; }
        public virtual DbSet<RequestForQuotation> RequestForQuotations { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<CompanyTranslation> CompanyTranslations { get; set; }
        public virtual DbSet<AttributeForSourceType> AttributeForSourcTypes { get; set; }
        public virtual DbSet<AttributeForSourceTypeValue> AttributeForSourceTypeValues { get; set; }
        public virtual DbSet<RequestForQuotationContact> RequestForQuotationContacts { get; set; }
        public virtual DbSet<Advertisiment> Advertisiments { get; set; }
        public virtual DbSet<AdvertisimentPosition> AdvertisimentPositions { get; set; }
        public virtual DbSet<AttributeForSourceTypeTranslation> AttributeForSourceTypeTranslations { get; set; }
        public virtual DbSet<SourceType> SourceTypes { get; set; }
        public virtual DbSet<SourceTypeTranslation> SourceTypeTranslations { get; set; }
        public virtual DbSet<AttributeChoice> AttributeChoices { get; set; }
        public virtual DbSet<AttributeChoiceTranslation> AttributeChoiceTranslations { get; set; }
        public virtual DbSet<ServiceTranslation> ServiceTranslations { get; set; }
        public virtual DbSet<SubService> SubServices { get; set; }
        public virtual DbSet<SubServiceTranslation> SubServiceTranslations { get; set; }
        public virtual DbSet<ContactUs> ContactUs { get; set; }
        public virtual DbSet<PrivacyPolicy> PrivacyPolicies { get; set; }
        public virtual DbSet<PrivacyPolicyTranslation> PrivacyPolicyTranslations { get; set; }
        public virtual DbSet<ServiceValue> ServiceValues { get; set; }
        public virtual DbSet<AttributeChoiceAndAttachment> AttributeChoiceAndAttachments { get; set; }
        public virtual DbSet<CompanyBranch> CompanyBranches { get; set; }
        public virtual DbSet<CompanyContact> CompanyContacts { get; set; }
        public virtual DbSet<Tool> Tools { get; set; }
        public virtual DbSet<ToolTranslation> ToolTranslations { get; set; }
        public virtual DbSet<Term> Terms { get; set; }
        public virtual DbSet<TermTranslation> TermTranslations { get; set; }
        public virtual DbSet<Mediator> Mediator { get; set; }
        public virtual DbSet<Partner> Partner { get; set; }
        public virtual DbSet<SelectedCompaniesBySystemForRequest> SelectedCompaniesBySystemForRequests { get; set; }
        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<ServiceValueForOffer> ServiceValueForOffers { get; set; }
        public virtual DbSet<CompanyBranchTranslation> CompanyBranchTranslation { get; set; }
        public virtual DbSet<AskForHelp> AskForHelps { get; set; }
        public virtual DbSet<FrequentlyQuestion> FrequentlyQuestions { get; set; }
        public virtual DbSet<FrequentlyQuestionTranslation> FrequentlyQuestionTranslations { get; set; }
        public virtual DbSet<RejectReason> RejectReasons { get; set; }
        public virtual DbSet<RejectReasonTranslation> RejectReasonTranslations { get; set; }
        public virtual DbSet<Draft> Drafts { get; set; }
        public virtual DbSet<AttributeForSourceTypeValuesForDraft> AttributeForSourceTypeValuesForDrafts { get; set; }
        public virtual DbSet<ServiceValuesForDraft> ServiceValuesForDrafts { get; set; }
        public virtual DbSet<RequestForQuotationContactsForDraft> RequestForQuotationContactsForDrafts { get; set; }
        public virtual DbSet<AttributeAndAttachmentsForDraft> AttributeAndAttachmentsForDrafts { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<ChangedPhoneNumberForUser> ChangedPhoneNumberForUsers { get; set; }
        public virtual DbSet<Code> Codes { get; set; }
        public virtual DbSet<Point> Points { get; set; }
        public virtual DbSet<PointTranslation> PointTranslations { get; set; }
        public virtual DbSet<PaidRequestPossible> PaidRequestPossibles { get; set; }
        public virtual DbSet<PointsValue> PointsValue { get; set; }
        public virtual DbSet<SearchedPlacesByUser> SearchedPlacesByUsers { get; set; }
        public virtual DbSet<CommissionGroup> CommissionGroups { get; set; }
        public virtual DbSet<TimeWork> TimeWork { get; set; }
        public virtual DbSet<ApkBuild> ApkBuilds { get; set; }
        public virtual DbSet<MoneyTransfer> MoneyTransfers { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Truck> Trucks { get; set; }


        public MofleetDbContext(DbContextOptions<MofleetDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // 2. Decimal Property Issues
            modelBuilder.Entity<Code>()
                .Property(c => c.DiscountPercentage)
                .HasColumnType("decimal(18,2)");
            // Other configurations...

            base.OnModelCreating(modelBuilder);
        }
    }
}
