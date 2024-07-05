﻿using Abp.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using Mofleet.Editions;
using Mofleet.MultiTenancy;
using System.Linq;

namespace Mofleet.EntityFrameworkCore.Seed.Tenants
{
    public class DefaultTenantBuilder
    {
        private readonly MofleetDbContext _context;

        public DefaultTenantBuilder(MofleetDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateDefaultTenant();
        }

        private void CreateDefaultTenant()
        {
            // Default tenant

            var defaultTenant = _context.Tenants.IgnoreQueryFilters().FirstOrDefault(t => t.TenancyName == AbpTenantBase.DefaultTenantName);
            if (defaultTenant == null)
            {
                defaultTenant = new Tenant(AbpTenantBase.DefaultTenantName, AbpTenantBase.DefaultTenantName);

                var defaultEdition = _context.Editions.IgnoreQueryFilters().FirstOrDefault(e => e.Name == EditionManager.DefaultEditionName);
                if (defaultEdition != null)
                {
                    defaultTenant.EditionId = defaultEdition.Id;
                }

                _context.Tenants.Add(defaultTenant);
                _context.SaveChanges();
            }
            var companyTenant = _context.Tenants.IgnoreQueryFilters().FirstOrDefault(t => t.TenancyName == "CompanyTenant");
            if (companyTenant == null)
            {
                companyTenant = new Tenant("CompanyTenant", "CompanyTenant");

                var defaultEdition = _context.Editions.IgnoreQueryFilters().FirstOrDefault(e => e.Name == EditionManager.DefaultEditionName);
                if (defaultEdition != null)
                {
                    companyTenant.EditionId = defaultEdition.Id;
                }

                _context.Tenants.Add(companyTenant);
                _context.SaveChanges();
            }
        }
    }
}
