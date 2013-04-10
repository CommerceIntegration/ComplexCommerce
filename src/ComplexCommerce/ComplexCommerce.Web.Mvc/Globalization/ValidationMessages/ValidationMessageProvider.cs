using System;

namespace ComplexCommerce.Web.Mvc.Globalization.ValidationMessages
{
    /// <summary>
    /// Uses <see cref="ILocalizedStringProvider"/> to find attribute translations.
    /// </summary>
    /// <remarks>Uses the <see cref="DependencyResolver"/> to find the localized string provider.</remarks>
    public class ValidationMessageProvider 
        : IValidationMessageProvider
    {
        public ValidationMessageProvider(ILocalizedStringProvider stringProvider)
        {
            if (stringProvider == null) 
                throw new ArgumentNullException("stringProvider");

            this.stringProvider = stringProvider;
        }

        private readonly ILocalizedStringProvider stringProvider;

        /// <summary>
        /// Gets the string provider.
        /// </summary>
        /// <returns></returns>
        protected virtual ILocalizedStringProvider GetStringProvider()
        {
            return stringProvider;
        }

        /// <summary>
        /// Get a validation message
        /// </summary>
        /// <param name="context"></param>
        /// <returns>
        /// String if found; otherwise <c>null</c>.
        /// </returns>
        public string GetMessage(IMessageContext context)
        {
            var provider = GetStringProvider();
            return provider.GetValidationString(context.Attribute.GetType(), context.ContainerType, context.PropertyName) ??
                        provider.GetValidationString(context.Attribute.GetType());
        }
    }
}
