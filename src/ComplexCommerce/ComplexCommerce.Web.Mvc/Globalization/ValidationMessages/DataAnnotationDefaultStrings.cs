using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;

namespace ComplexCommerce.Web.Mvc.Globalization.ValidationMessages
{
    /// <summary>
    /// Loads the default DataAnnotation strings from the resource file System.ComponentModel.DataAnnotations.Resources.DataAnnotationsResources
    /// </summary>
    /// <remarks>Do note that resource files can fallback to default culture (and therefore return the incorrect language)</remarks>
    public class DataAnnotationDefaultStrings 
        : IValidationMessageProvider
    {
        private readonly ResourceManager resourceManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationAttributesStringProvider"/> class.
        /// </summary>
        public DataAnnotationDefaultStrings()
        {
            var resourceStringType =
                typeof(RequiredAttribute).Assembly.GetType(
                    "System.ComponentModel.DataAnnotations.Resources.DataAnnotationsResources");

            if (resourceStringType == null)
                return;

            resourceManager = new ResourceManager(resourceStringType);
        }

        /// <summary>
        /// Get the localized text
        /// </summary>
        /// <param name="type">Validation attribute type.</param>
        /// <param name="culture">Culture to get for </param>
        /// <returns>Text if found; otherwise null</returns>
        public virtual string GetString(Type type, CultureInfo culture)
        {
            var resourceName = string.Format("{0}_ValidationError", type.Name);

            if (culture.Name.StartsWith("en"))
                return resourceManager.GetString(resourceName, culture);

            var rs = resourceManager.GetResourceSet(culture, false, true);
            return rs == null ? null : rs.GetString(resourceName);
        }

        public string GetMessage(IMessageContext context)
        {

            return GetString(context.Attribute.GetType(), context.CultureInfo);
        }
    }
}
