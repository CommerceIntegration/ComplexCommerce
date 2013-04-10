using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ComplexCommerce.Web.Mvc.Globalization
{
    /// <summary>
    /// Uses a delegate to create the client validation rules.
    /// </summary>
    public class DelegateValidationAttributeAdaptorFactory 
        : IValidationAttributeAdaptorFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateValidationAttributeAdapterFactory"/> class.
        /// </summary>
        /// <param name="factory">Takes attribute + error Message and returns client rules.</param>
        public DelegateValidationAttributeAdaptorFactory(Func<ValidationAttribute, string, IEnumerable<ModelClientValidationRule>> factory)
        {
            this.factory = factory;
        }

        private readonly Func<ValidationAttribute, string, IEnumerable<ModelClientValidationRule>> factory;

        /// <summary>
        /// Generate client rules for a validation attribute
        /// </summary>
        /// <param name="attribute">Attribute to get rules for</param>
        /// <param name="errorMessage">Message to display</param>
        /// <returns>Validation rules</returns>
        public IEnumerable<ModelClientValidationRule> Create(ValidationAttribute attribute, string errorMessage)
        {
            return factory(attribute, errorMessage);
        }
    }
}
