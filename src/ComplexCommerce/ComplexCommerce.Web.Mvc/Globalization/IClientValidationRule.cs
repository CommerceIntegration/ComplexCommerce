using System;
using System.Collections.Generic;

namespace ComplexCommerce.Web.Mvc.Globalization
{
    /// <summary>
    /// Interface for client validation rules
    /// </summary>
    /// <remarks>Created so that we can decorate existing validation classes instead of having to create a lot of adaptors.</remarks>
    public interface IClientValidationRule
    {
        /// <summary>
        /// Gets complete error message (formatted)
        /// </summary>
        string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets parameters required for the client validation rule
        /// </summary>
        IDictionary<string, object> ValidationParameters { get; }

        /// <summary>
        /// Gets client validation rule (name of the jQuery rule)
        /// </summary>
        string ValidationType { get; set; }
    }
}
