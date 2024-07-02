﻿using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Threading.BackgroundWorkers;
using AutoMapper;
using Mofleet.Authorization;
using Mofleet.BackGroundJobs;
using Mofleet.Cities.Dto;
using Mofleet.ContactUsService.Dto;
using Mofleet.Countries;
using Mofleet.Countries.Dto;
using Mofleet.Domain.AttributeChoices;
using Mofleet.Domain.AttributeChoices.Dto;
using Mofleet.Domain.AttributesForSourceType;
using Mofleet.Domain.AttributesForSourceType.Dto;
using Mofleet.Domain.Cities;
using Mofleet.Domain.Companies;
using Mofleet.Domain.Companies.Dto;
using Mofleet.Domain.CompanyBranches;
using Mofleet.Domain.CompanyBranches.Dto;
using Mofleet.Domain.ContactUses;
using Mofleet.Domain.Countries;
using Mofleet.Domain.FrequentlyQuestions;
using Mofleet.Domain.FrequentlyQuestions.Dto;
using Mofleet.Domain.Points;
using Mofleet.Domain.PrivacyPolicies;
using Mofleet.Domain.PushNotifications;
using Mofleet.Domain.Regions;
using Mofleet.Domain.Regions.Dto;
using Mofleet.Domain.RejectReasons;
using Mofleet.Domain.RejectReasons.Dto;
using Mofleet.Domain.services;
using Mofleet.Domain.services.Dto;
using Mofleet.Domain.SourceTypes;
using Mofleet.Domain.SourceTypes.Dto;
using Mofleet.Domain.SubServices;
using Mofleet.Domain.SubServices.Dto;
using Mofleet.Domain.Terms;
using Mofleet.Domain.Toolss;
using Mofleet.Domain.Toolss.Dto;
using Mofleet.Points.Dto;
using Mofleet.PrivacyPolicyService.Dto;
using Mofleet.PushNotifications.Dto;
using Mofleet.TermService.Dto;

namespace Mofleet
{
    [DependsOn(
        typeof(MofleetCoreModule),
        typeof(AbpAutoMapperModule))]
    public class MofleetApplicationModule : AbpModule
    {
        public override void PostInitialize()
        {
            var workManager = IocManager.Resolve<IBackgroundWorkerManager>();
            workManager.Add(IocManager.Resolve<DeleteDraftBackgroundJob>());
            workManager.Add(IocManager.Resolve<ConfirmRequestIfUserDidnotConfirmedBGJ>());
            workManager.Add(IocManager.Resolve<RejectAllOffersForUserIfHeDidnotMakeAnyChangeBGJ>());
            workManager.Add(IocManager.Resolve<MakeAllPossibleRequestOutOfPossibleBGJ>());
            workManager.Add(IocManager.Resolve<FinishSubscribtionInFeatureServiceBGJ>());
        }
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<MofleetAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(MofleetApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            ); Configuration.Modules.AbpAutoMapper().Configurators.Add(configuration =>
            {
                CustomDtoMapper.CreateMappings(configuration, new MultiLingualMapContext(
                    IocManager.Resolve<ISettingManager>()
                ));
            });
        }
        public static class CustomDtoMapper
        {
            public static void CreateMappings(IMapperConfigurationExpression configuration, MultiLingualMapContext context)
            {

                #region PushNotification
                configuration.CreateMultiLingualMap<PushNotification, PushNotificationTranslation, PushNotificationDetailsDto>(context).TranslationMap
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message));
                configuration.CreateMultiLingualMap<PushNotification, PushNotificationTranslation, PushNotificationDto>(context).TranslationMap
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message));
                #endregion




                #region Country
                // Country Translation Configuration
                configuration.CreateMultiLingualMap<Country, CountryTranslation, CountryDetailsDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                configuration.CreateMultiLingualMap<Country, CountryTranslation, CountryDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                #endregion

                #region City
                // City Translation Configuration
                configuration.CreateMultiLingualMap<City, CityTranslation, LiteCityDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                configuration.CreateMultiLingualMap<City, CityTranslation, CityDetailsDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                configuration.CreateMultiLingualMap<City, CityTranslation, CityDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                configuration.CreateMultiLingualMap<City, CityTranslation, LiteCity>(context).TranslationMap
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                #endregion

                #region Point
                // Point Translation Configuration
                configuration.CreateMultiLingualMap<Point, PointTranslation, LitePointDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                configuration.CreateMultiLingualMap<Point, PointTranslation, PointDetailsDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                configuration.CreateMultiLingualMap<Point, PointTranslation, PointDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                #endregion

                #region Region
                // Region Translation Configuration
                configuration.CreateMultiLingualMap<Region, RegionTranslation, LiteRegionDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                configuration.CreateMultiLingualMap<Region, RegionTranslation, LiteRegionCityDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                configuration.CreateMultiLingualMap<Region, RegionTranslation, RegionDetailsDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                configuration.CreateMultiLingualMap<Region, RegionTranslation, RegionDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                #endregion

                #region SourceTyppe
                configuration.CreateMultiLingualMap<SourceType, SourceTypeTranslation, LiteSourceTypeDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                configuration.CreateMultiLingualMap<SourceType, SourceTypeTranslation, SourceTypeDetailsDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                configuration.CreateMultiLingualMap<SourceType, SourceTypeTranslation, SourceTypeDto>(context).TranslationMap
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                #endregion


                #region AttributeForSourceType
                configuration.CreateMultiLingualMap<AttributeForSourceType, AttributeForSourceTypeTranslation, LiteAttributeForSourceTypeDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                configuration.CreateMultiLingualMap<AttributeForSourceType, AttributeForSourceTypeTranslation, AttributeForSourceTypeDetailsDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                configuration.CreateMultiLingualMap<AttributeForSourceType, AttributeForSourceTypeTranslation, AttributeForSourceTypeDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                #endregion

                #region AttributeChoices
                configuration.CreateMultiLingualMap<AttributeChoice, AttributeChoiceTranslation, AttributeChoiceDto>(context).TranslationMap
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                configuration.CreateMultiLingualMap<AttributeChoice, AttributeChoiceTranslation, LiteAttributeChoiceDto>(context).TranslationMap
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

                configuration.CreateMultiLingualMap<AttributeChoice, AttributeChoiceTranslation, SuperLiteAttributeChoiceDto>(context).TranslationMap
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                configuration.CreateMultiLingualMap<AttributeChoice, AttributeChoiceTranslation, AttributeChoiceDetailsDto>(context).TranslationMap
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

                #endregion

                #region ContactUs
                configuration.CreateMultiLingualMap<ContactUs, ContactUsTranslation, ContactUsDetailsDto>(context).TranslationMap
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                 .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                 .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
                #endregion

                #region PrivacyPolicy
                configuration.CreateMultiLingualMap<PrivacyPolicy, PrivacyPolicyTranslation, PrivacyPolicyDetailsDto>(context).TranslationMap
                 .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                 .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
                configuration.CreateMultiLingualMap<PrivacyPolicy, PrivacyPolicyTranslation, LitePrivacyPolicyDto>(context).TranslationMap
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                 .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
                #endregion
                #region Company
                configuration.CreateMultiLingualMap<Company, CompanyTranslation, CompanyDetailsDto>(context).TranslationMap
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                 .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Bio))
                 .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
                configuration.CreateMultiLingualMap<Company, CompanyTranslation, LiteCompanyDto>(context).TranslationMap
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                 .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Bio))
                 .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
                configuration.CreateMultiLingualMap<Company, CompanyTranslation, CompanyDto>(context).TranslationMap
                  .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                  .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Bio))
                  .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));


                #endregion
                #region CompanyBranch
                configuration.CreateMultiLingualMap<CompanyBranch, CompanyBranchTranslation, CompanyBranchDetailsDto>(context).TranslationMap
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                 .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Bio))
                 .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
                configuration.CreateMultiLingualMap<CompanyBranch, CompanyBranchTranslation, LiteCompanyBranchDto>(context).TranslationMap
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                 .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Bio))
                 .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
                configuration.CreateMultiLingualMap<CompanyBranch, CompanyBranchTranslation, CompanyBranchDto>(context).TranslationMap
                  .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                  .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Bio))
                  .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));





                #endregion

                #region Term
                configuration.CreateMultiLingualMap<Term, TermTranslation, TermDetailsDto>(context).TranslationMap
                 .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                 .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
                configuration.CreateMultiLingualMap<Term, TermTranslation, LiteTermDto>(context).TranslationMap
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                 .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
                #endregion

                #region Service
                configuration.CreateMultiLingualMap<Service, ServiceTranslation, ServiceDetailsDto>(context).TranslationMap
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                configuration.CreateMultiLingualMap<Service, ServiceTranslation, LiteServiceDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                configuration.CreateMultiLingualMap<Service, ServiceTranslation, ServiceDto>(context).TranslationMap
                  .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

                #endregion

                #region SubService
                configuration.CreateMultiLingualMap<SubService, SubServiceTranslation, SubServiceDetailsDto>(context).TranslationMap
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                configuration.CreateMultiLingualMap<SubService, SubServiceTranslation, LiteSubServiceDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                configuration.CreateMultiLingualMap<SubService, SubServiceTranslation, SubServiceDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                #endregion

                #region Tool
                configuration.CreateMultiLingualMap<Tool, ToolTranslation, ToolDetailsDto>(context).TranslationMap
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                configuration.CreateMultiLingualMap<Tool, ToolTranslation, LiteToolDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                #endregion

                #region FrequentlyQuestion
                configuration.CreateMultiLingualMap<FrequentlyQuestion, FrequentlyQuestionTranslation, FrequentlyQuestionDetailsDto>(context).TranslationMap
                 .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.Question))
                 .ForMember(dest => dest.Answer, opt => opt.MapFrom(src => src.Answer));
                configuration.CreateMultiLingualMap<FrequentlyQuestion, FrequentlyQuestionTranslation, LiteFrequentlyQuestionDto>(context).TranslationMap
                .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.Question))
                 .ForMember(dest => dest.Answer, opt => opt.MapFrom(src => src.Answer));
                #endregion

                #region RejectReason
                configuration.CreateMultiLingualMap<RejectReason, RejectReasonTranslation, RejectReasonDetailsDto>(context).TranslationMap
                 .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
                configuration.CreateMultiLingualMap<RejectReason, RejectReasonTranslation, LiteRejectReasonDto>(context).TranslationMap
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
                #endregion


            }
        }

    }
}
