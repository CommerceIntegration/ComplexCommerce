using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ComplexCommerce.Web.Mvc.Globalization
{
    /// <summary>
    /// Create validation rules for an attribute.
    /// </summary>
    public interface IValidationAttributeAdaptorFactory
    {
        /// <summary>
        /// Generate client rules for a validation attribute
        /// </summary>
        /// <param name="attribute">Attribute to get rules for</param>
        /// <param name="errorMessage">Message to display</param>
        /// <returns>Validation rules</returns>
        IEnumerable<ModelClientValidationRule> Create(ValidationAttribute attribute, string errorMessage);
    }
}
