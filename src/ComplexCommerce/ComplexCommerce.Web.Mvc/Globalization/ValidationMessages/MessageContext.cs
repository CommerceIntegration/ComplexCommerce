using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ComplexCommerce.Web.Mvc.Globalization.ValidationMessages
{
    /// <summary>
    /// Context information used to be able to identify and load the correct translation
    /// </summary>
    public class MessageContext : IMessageContext
    {
        public MessageContext(ValidationAttribute attribute, Type containerType, string propertyName,
                                 CultureInfo cultureInfo)
        {
            if (attribute == null) 
                throw new ArgumentNullException("attribute");
            if (containerType == null) 
                throw new ArgumentNullException("containerType");
            if (propertyName == null) 
                throw new ArgumentNullException("propertyName");
            if (cultureInfo == null) 
                throw new ArgumentNullException("cultureInfo");

            this.attribute = attribute;
            this.containerType = containerType;
            this.propertyName = propertyName;
            this.cultureInfo = cultureInfo;
        }

        private readonly ValidationAttribute attribute;
        private readonly Type containerType;
        private readonly CultureInfo cultureInfo;
        private readonly string propertyName;

        #region IMessageContext Members

        /// <summary>
        /// Gets attribute to get message for.
        /// </summary>
        public ValidationAttribute Attribute
        {
            get { return attribute; }
        }

        /// <summary>
        /// Gets model that the property exists in
        /// </summary>
        public Type ContainerType
        {
            get { return containerType; }
        }

        /// <summary>
        /// Gets name of the target property
        /// </summary>
        public string PropertyName
        {
            get { return propertyName; }
        }

        /// <summary>
        /// Gets requested language
        /// </summary>
        public CultureInfo CultureInfo
        {
            get { return cultureInfo; }
        }

        #endregion
    }
}
