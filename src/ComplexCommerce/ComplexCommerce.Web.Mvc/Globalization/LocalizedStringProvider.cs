using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplexCommerce.Business.Context;
using ComplexCommerce.Business.Globalization;

namespace ComplexCommerce.Web.Mvc.Globalization
{
    public class LocalizedStringProvider
        : ILocalizedStringProvider
    {
        public LocalizedStringProvider(
            IApplicationContext appContext
            )
        {
            if (appContext == null)
                throw new ArgumentNullException("appContext");
            this.appContext = appContext;
        }

        private readonly IApplicationContext appContext;

        #region ILocalizedStringProvider Members

        /// <summary>
        /// Gets a enum string
        /// </summary>
        /// <param name="enumType">Type of enum</param>
        /// <param name="name">Name of the value to translation for</param>
        /// <returns>Translated name if found; otherwise null.</returns>
        public string GetEnumString(Type enumType, string name)
        {
            return Translate(enumType, name);
        }

        /// <summary>
        /// Get a localized metadata for a model property
        /// </summary>
        /// <param name="model">Model being localized</param>
        /// <param name="propertyName">Property to get string for</param>
        /// <param name="metadataName">Valid names are: Watermark, Description, NullDisplayText, ShortDisplayText.</param>
        /// <returns>Translated string if found; otherwise null.</returns>
        /// <remarks>
        /// Look at <see cref="ModelMetadata"/> to know more about the meta data
        /// </remarks>
        public string GetModelString(Type model, string propertyName, string metadataName)
        {
            return Translate(model, propertyName + "_" + metadataName);
        }

        /// <summary>
        /// Get a localized string for a model property
        /// </summary>
        /// <param name="model">Model being localized</param>
        /// <param name="propertyName">Property to get string for</param>
        /// <returns>Translated string if found; otherwise null.</returns>
        public string GetModelString(Type model, string propertyName)
        {
            return Translate(model, propertyName);
        }

        /// <summary>
        /// Get a translated string for a validation attribute
        /// </summary>
        /// <param name="attributeType">Type of attribute</param>
        /// <param name="modelType">Your view model</param>
        /// <param name="propertyName">Property in your view model</param>
        /// <returns>
        /// Translated validation message if found; otherwise null.
        /// </returns>
        public string GetValidationString(Type attributeType, Type modelType, string propertyName)
        {
            return Translate(modelType, propertyName + "_" + attributeType.Name);
        }

        /// <summary>
        /// Get a translated string for a validation attribute
        /// </summary>
        /// <param name="attributeType">Type of attribute</param>
        /// <returns>Translated validtion message if found; otherwise null.</returns>
        /// <remarks>
        /// Used to get localized error messages for the DataAnnotation attributes. The returned string 
        /// should have the same format as the built in messages, such as "{0} is required.".
        /// </remarks>
        public string GetValidationString(Type attributeType)
        {
            return Translate(attributeType, "class");
        }

        #endregion

        /// <summary>
        /// Translate a string
        /// </summary>
        /// <param name="type">Model being translated</param>
        /// <param name="name">Property name (or <c>propertyName_metadataName</c>)</param>
        /// <returns></returns>
        protected virtual string Translate(Type type, string name)
        {
            if (type == null) 
                throw new ArgumentNullException("type");
            if (name == null) 
                throw new ArgumentNullException("name");


            if (!string.IsNullOrEmpty(type.Namespace) && 
                (type.Namespace.StartsWith("Griffin.MvcContrib") && 
                !type.Namespace.Contains("TestProject")))
            {
                return null;
            }

            // To get data we need:
            // 1. TenantId
            // 2. LocaleId
            // 3. Key based on type (for data lookup and cache of entire type's data) [type.FullName + type.FullName.GetHashCode()]
            var list = AssemblyTypeTextLocaleList.GetCachedAssemblyTypeTextLocaleList(
                appContext.CurrentTenant.Id, appContext.CurrentLocaleId, type.FullName);

            // Then we need to locally match the correct item in the collection based on name
            return list.GetLocalizedText(name);


            //var key = new TypePromptKey(type.FullName, name);
            //var prompt = _repository.GetPrompt(CultureInfo.CurrentUICulture, key) ??
            //             _repository.GetPrompt(CultureInfo.CurrentUICulture,
            //                                   new TypePromptKey(typeof(CommonPrompts).FullName, name));


            //if (prompt == null)
            //{
            //    _repository.Save(CultureInfo.CurrentUICulture, type.FullName, name, "");
            //}
            //else
            //{
            //    if (!string.IsNullOrEmpty(prompt.TranslatedText))
            //        return prompt.TranslatedText;
            //}


            //return name.EndsWith("NullDisplayText") ? "" : null;
        }
    }
}
