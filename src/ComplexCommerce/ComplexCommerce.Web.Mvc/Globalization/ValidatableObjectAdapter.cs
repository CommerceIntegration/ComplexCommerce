using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ComplexCommerce.Web.Mvc.Globalization
{
    /// <summary>
    /// Adapter which convers the result from <see cref="IValidatableObject"/> to <see cref="ModelValidationResult"/>
    /// </summary>
    /// <remarks>Client side validation will only work if the rules from <see cref="IValidatableObject.Validate"/> 
    /// implements <see cref="IClientValidationRule"/></remarks>
    public class ValidatableObjectAdapter 
        : ModelValidator
    {
        public ValidatableObjectAdapter(ModelMetadata metadata, ControllerContext controllerContext)
            : base(metadata, controllerContext)
        {
            this.metadata = metadata;
        }

        private readonly ModelMetadata metadata;

        /// <summary>
        /// Gets or sets a value that indicates whether a model property is required.
        /// </summary>
        /// <returns>true if the model property is required; otherwise, false.</returns>
        public override bool IsRequired
        {
            get { return true; }
        }

        /// <summary>
        /// When implemented in a derived class, returns metadata for client validation.
        /// </summary>
        /// <returns>
        /// The metadata for client validation.
        /// </returns>
        /// <remarks>Will only work if the rules from <see cref="IValidatableObject.Validate"/> implements <see cref="IClientValidationRule"/></remarks>
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            var validator = (IValidatableObject)metadata.Model;
            var validationResults = validator.Validate(new ValidationContext(metadata.Model, null, null));
            foreach (var validationResult in validationResults)
            {
                var clientRule = validationResult as IClientValidationRule;
                if (clientRule == null)
                    continue;

                var rule = new ModelClientValidationRule
                {
                    ErrorMessage = clientRule.ErrorMessage,
                    ValidationType = clientRule.ValidationType
                };

                foreach (var kvp in clientRule.ValidationParameters)
                {
                    rule.ValidationParameters.Add(kvp);
                }

                yield return rule;
            }
        }

        /// <summary>
        /// When implemented in a derived class, validates the object.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <returns>
        /// A list of validation results.
        /// </returns>
        public override IEnumerable<ModelValidationResult> Validate(object container)
        {
            var validator = (IValidatableObject)metadata.Model;
            var validationResults = validator.Validate(new ValidationContext(metadata.Model, null, null));
            foreach (var validationResult in validationResults)
            {
                bool hasMemberNames = false;
                foreach (var memberName in validationResult.MemberNames)
                {
                    hasMemberNames = true;
                    var item = new ModelValidationResult
                    {
                        MemberName = memberName,
                        Message = validationResult.ErrorMessage
                    };
                    yield return item;
                }

                if (!hasMemberNames)
                    yield return new ModelValidationResult
                    {
                        MemberName = string.Empty,
                        Message = validationResult.ErrorMessage
                    };
            }
        }
    }
}
