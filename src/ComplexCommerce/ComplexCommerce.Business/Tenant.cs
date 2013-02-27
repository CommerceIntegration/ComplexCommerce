using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Repositories;

namespace ComplexCommerce.Business
{

    #region Tenant Factory

    public interface ITenantFactory
    {
        ITenant GetTenant(string host);
        ITenant NewTenant();
    }

    public class TenantFactory
        : ITenantFactory
    {

        #region ITenantFactory Members

        public ITenant GetTenant(string host)
        {
            return Tenant.GetTenant(host);
        }

        public ITenant NewTenant()
        {
            return Tenant.NewTenant();
        }

        #endregion
    }

    #endregion

    public interface ITenant
    {
        int Id { get; }
        int ChainId { get; }
        string Name { get; }
        string LogoUrl { get; }
        CultureInfo DefaultLocale { get; }
    }

    [Serializable]
    public class Tenant
        : CslaReadOnlyBase<Tenant>, ITenant
    {
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            private set { LoadProperty(IdProperty, value); }
        }

        public static readonly PropertyInfo<int> ChainIdProperty = RegisterProperty<int>(p => p.ChainId);
        public int ChainId
        {
            get { return GetProperty(ChainIdProperty); }
            private set { LoadProperty(ChainIdProperty, value); }
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            private set { LoadProperty(NameProperty, value); }
        }

        public static readonly PropertyInfo<string> LogoUrlProperty = RegisterProperty<string>(p => p.LogoUrl);
        public string LogoUrl
        {
            get { return GetProperty(LogoUrlProperty); }
            private set { LoadProperty(LogoUrlProperty, value); }
        }

        public static readonly PropertyInfo<int> DefaultLocaleIdProperty = RegisterProperty<int>(p => p.DefaultLocaleId);
        public int DefaultLocaleId
        {
            get { return GetProperty(DefaultLocaleIdProperty); }
            private set { LoadProperty(DefaultLocaleIdProperty, value); }
        }

        public CultureInfo DefaultLocale
        {
            get 
            {
                var result = new CultureInfo("en");
                if (IsValidLocaleId(this.DefaultLocaleId))
                {
                    result = new CultureInfo(this.DefaultLocaleId);
                }
                return result;
            }
        }

        public static readonly PropertyInfo<TenantTypeEnum> TenantTypeProperty = RegisterProperty<TenantTypeEnum>(p => p.TenantType);
        public TenantTypeEnum TenantType
        {
            get { return GetProperty(TenantTypeProperty); }
            private set { LoadProperty(TenantTypeProperty, value); }
        }


        private bool IsValidLocaleId(int localeId)
        {
            return
                CultureInfo
                .GetCultures(CultureTypes.AllCultures)
                .Any(c => c.LCID == localeId);
        }



        internal static Tenant GetTenant(string host)
        {
            return DataPortal.Fetch<Tenant>(host);
            // TODO: Put in some kind of factory here that can return the
            // correct type of tenant depending on the TenantType property
        }

        internal static Tenant NewTenant()
        {
            return DataPortal.Create<Tenant>();
            // TODO: Put in some kind of factory here that can return the
            // correct type of tenant depending on the TenantType property
        }

        private void DataPortal_Fetch(string domain)
        {
            using (var ctx = ContextFactory.GetContext())
            {
                var data = repository.Fetch(domain);

                if (data != null)
                {
                    Id = data.Id;
                    ChainId = data.ChainId;
                    Name = data.Name;
                    LogoUrl = data.LogoUrl;
                    DefaultLocaleId = data.DefaultLocaleId;
                    TenantType = (TenantTypeEnum)data.TenantType;
                }
            }
        }



        #region Dependency Injection

        private ITenantRepository repository;
        public ITenantRepository Repository
        {
            set
            {
                // Don't allow the value to be set to null
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                // Don't allow the value to be set more than once
                if (this.repository != null)
                {
                    throw new InvalidOperationException();
                }
                this.repository = value;
            }
        }

        #endregion

    }
}
