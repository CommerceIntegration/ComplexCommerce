using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Csla.Web.Mvc;
using ComplexCommerce.Web.Mvc;
using ComplexCommerce.Shared.DI;
using ComplexCommerce.Web.Mvc.Globalization;

namespace ComplexCommerce
{
    public class ModelConfig
    {
        public static void Register(IDependencyInjectionContainer container)
        {
            // CSLA 4 Configuration

            // NOTE: For some reason this doesn't work even though the correct type is configured in the DI container
            //var modelBinder = container.Resolve<IModelBinder>(); 
            var modelBinder = new CslaModelBinder();
            ModelBinders.Binders.DefaultBinder = modelBinder;

            // Localization
            var modelMetadataProvider = container.Resolve<LocalizedModelMetadataProvider>();
            ModelMetadataProviders.Current = modelMetadataProvider;

            var modelValidatorProvider = container.Resolve<LocalizedModelValidatorProvider>();
            ModelValidatorProviders.Providers.Clear();
            ModelValidatorProviders.Providers.Add(modelValidatorProvider);
        }
    }
}