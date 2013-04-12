using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Dto;

namespace ComplexCommerce.Business.Logging
{
    [Serializable]
    public class ErrorInfo
        : CslaReadOnlyBase<ErrorInfo>
    {
        public static PropertyInfo<Guid> IdProperty = RegisterProperty<Guid>(c => c.Id);
        public Guid Id
        {
            get { return GetProperty(IdProperty); }
            private set { LoadProperty(IdProperty, value); }
        }

        // TODO: Add ChainId to the log so we can later retrieve errors by Chain

        public static PropertyInfo<int> TenantIdProperty = RegisterProperty<int>(c => c.TenantId);
        public int TenantId
        {
            get { return GetProperty(TenantIdProperty); }
            private set { LoadProperty(TenantIdProperty, value); }
        }

        public static PropertyInfo<string> ApplicationProperty = RegisterProperty<string>(c => c.Application);
        public string Application
        {
            get { return GetProperty(ApplicationProperty); }
            private set { LoadProperty(ApplicationProperty, value); }
        }

        public static PropertyInfo<string> HostProperty = RegisterProperty<string>(c => c.Host);
        public string Host
        {
            get { return GetProperty(HostProperty); }
            private set { LoadProperty(HostProperty, value); }
        }

        public static PropertyInfo<string> TypeProperty = RegisterProperty<string>(c => c.Type);
        public string Type
        {
            get { return GetProperty(TypeProperty); }
            private set { LoadProperty(TypeProperty, value); }
        }

        public static PropertyInfo<string> SourceProperty = RegisterProperty<string>(c => c.Source);
        public string Source
        {
            get { return GetProperty(SourceProperty); }
            private set { LoadProperty(SourceProperty, value); }
        }

        public static PropertyInfo<string> MessageProperty = RegisterProperty<string>(c => c.Message);
        public string Message
        {
            get { return GetProperty(MessageProperty); }
            private set { LoadProperty(MessageProperty, value); }
        }

        public static PropertyInfo<string> UserProperty = RegisterProperty<string>(c => c.User);
        public string User
        {
            get { return GetProperty(UserProperty); }
            private set { LoadProperty(UserProperty, value); }
        }

        public static PropertyInfo<int> StatusCodeProperty = RegisterProperty<int>(c => c.StatusCode);
        public int StatusCode
        {
            get { return GetProperty(StatusCodeProperty); }
            private set { LoadProperty(StatusCodeProperty, value); }
        }

        public static PropertyInfo<DateTime> TimeProperty = RegisterProperty<DateTime>(c => c.Time);
        public DateTime Time
        {
            get { return GetProperty(TimeProperty); }
            private set { LoadProperty(TimeProperty, value); }
        }

        private void Child_Fetch(ErrorDto item)
        {
            Id = item.Id;
            TenantId = item.TenantId;
            Application = item.Application;
            Host = item.Host;
            Type = item.Type;
            Source = item.Source;
            Message = item.Message;
            User = item.User;
            StatusCode = item.StatusCode;
            Time = item.UtcTime;
        }

    }
}
