using System;

namespace ComplexCommerce.Web.Mvc.Globalization.ValidationMessages
{
    /// <summary>
    /// Uses <see cref="ILocalizedStringProvider"/> to find attribute translations.
    /// </summary>
    /// <remarks>Uses the <see cref="DependencyResolver"/> to find the localized string provider.</remarks>
    public class ValidationMessageDataSource : IValidationMessageDataSource
    {
        private readonly ILocalizedStringProvider _stringProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="GriffinStringsProvider"/> class.
        /// </summary>
        /// <param name="stringProvider">The string provider.</param>
        public ValidationMessageDataSource(ILocalizedStringProvider stringProvider)
        {
            if (stringProvider == null) throw new ArgumentNullException("stringProvider");
            _stringProvider = stringProvider;
        }

        /// <summary>
        /// Gets the string provider.
        /// </summary>
        /// <returns></returns>
        protected virtual ILocalizedStringProvider GetStringProvider()
        {
            return _stringProvider;
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
