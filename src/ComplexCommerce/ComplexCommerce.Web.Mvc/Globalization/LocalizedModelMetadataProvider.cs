using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ComplexCommerce.Web.Mvc.Globalization
{
    /// <summary>
    /// Metadata provider used to localize models and their meta data.
    /// </summary>
    /// <remarks>
    /// <para>Check the namespace documentation for an example on how to use the provider.</para>
    /// </remarks>
    public class LocalizedModelMetadataProvider 
        : DataAnnotationsModelMetadataProvider
    {
        public LocalizedModelMetadataProvider(ILocalizedStringProvider localizedStringProvider)
        {
            if (localizedStringProvider == null) 
                throw new ArgumentNullException("localizedStringProvider");
            this.localizedStringProvider = localizedStringProvider;
        }

        private readonly ILocalizedStringProvider localizedStringProvider;

        /// <summary>
        /// Gets the metadata for the specified property.
        /// </summary>
        /// <param name="attributes">The attributes.</param>
        /// <param name="containerType">The type of the container.</param>
        /// <param name="modelAccessor">The model accessor.</param>
        /// <param name="modelType">The type of the model.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>
        /// The metadata for the property.
        /// </returns>
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType,
                                                        Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var metadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
            if (containerType == null || propertyName == null)
                return metadata;

            if (metadata.DisplayName == null)
            {
                metadata.DisplayName = Translate(containerType, propertyName);

                if (metadata.DisplayName == null)
                    metadata.DisplayName = string.Format("[{0}: {1}]", CultureInfo.CurrentUICulture.Name, propertyName);
            }

            if (metadata.Watermark == null)
                metadata.Watermark = Translate(containerType, propertyName, "Watermark");

            if (metadata.Description == null)
                metadata.Description = Translate(containerType, propertyName, "Description");

            if (metadata.NullDisplayText == null)
                metadata.NullDisplayText = Translate(containerType, propertyName, "NullDisplayText");

            if (metadata.ShortDisplayName == null)
                metadata.ShortDisplayName = Translate(containerType, propertyName, "ShortDisplayName");

            return metadata;
        }

        /// <summary>
        /// Translate a string
        /// </summary>
        /// <param name="type">mode type</param>
        /// <param name="propertyName">Property name to translate</param>
        /// <returns>Translated string</returns>
        protected virtual string Translate(Type type, string propertyName)
        {
            return localizedStringProvider.GetModelString(type, propertyName);
        }

        /// <summary>
        /// Translate a string
        /// </summary>
        /// <param name="type">Model type</param>
        /// <param name="propertyName">Property name</param>
        /// <param name="metadataName">Meta data name</param>
        /// <returns>Translated string</returns>
        protected virtual string Translate(Type type, string propertyName, string metadataName)
        {
            return localizedStringProvider.GetModelString(type, propertyName, metadataName);
        }
    }
}
