using System;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Data.Dto;

namespace ComplexCommerce.Business.Logging
{
    [Serializable]
    public class Error
    {
        public static string GetErrorXml(Guid id, string application)
        {
            var cmd = new GetErrorXmlCommand(id, application);
            cmd = DataPortal.Execute<GetErrorXmlCommand>(cmd);
            return cmd.Xml;
        }

        public static Guid LogError(Guid id, string application, string host, string type, string source, string message, string user, int statusCode, DateTime time, string xml)
        {
            var cmd = new LogErrorCommand(id, application, host, type, source, message, user, statusCode, time, xml);
            cmd = DataPortal.Execute<LogErrorCommand>(cmd);
            return cmd.Id;
        }


        #region GetErrorXmlCommand

        [Serializable]
        private class GetErrorXmlCommand
            : CslaCommandBase<GetErrorXmlCommand>
        {
            public GetErrorXmlCommand(Guid id, string application)
            {
                if (id == null)
                    throw new ArgumentNullException("id");
                if (string.IsNullOrEmpty(application))
                    throw new ArgumentNullException("application");

                this.Id = id;
                this.Application = application;
            }


            public static PropertyInfo<Guid> IdProperty = RegisterProperty<Guid>(c => c.Id);
            public Guid Id
            {
                get { return ReadProperty(IdProperty); }
                private set { LoadProperty(IdProperty, value); }
            }

            public static PropertyInfo<string> ApplicationProperty = RegisterProperty<string>(c => c.Application);
            public string Application
            {
                get { return ReadProperty(ApplicationProperty); }
                private set { LoadProperty(ApplicationProperty, value); }
            }

            public static PropertyInfo<string> XmlProperty = RegisterProperty<string>(c => c.Xml);
            public string Xml
            {
                get { return ReadProperty(XmlProperty); }
                private set { LoadProperty(XmlProperty, value); }
            }

            /// <summary>
            /// We work with the cache on the server side of the DataPortal
            /// </summary>
            protected override void DataPortal_Execute()
            {
                using (var ctx = ContextFactory.GetContext())
                {
                    var data = repository.Fetch(this.Application, this.Id);
                    if (data != null)
                    {
                        this.Xml = data.Xml;
                    }
                }
            }

            #region Dependency Injection

            [NonSerialized]
            [NotUndoable]
            private IErrorRepository repository;
            public IErrorRepository Repository
            {
                set
                {
                    // Don't allow the value to be set to null
                    if (value == null)
                    {
                        throw new ArgumentNullException("value");
                    }
                    // Don't allow the value to be set more than once
                    if (this.repository != null)
                    {
                        throw new InvalidOperationException();
                    }
                    this.repository = value;
                }
            }

            #endregion
        }

        #endregion

        #region LogErrorCommand

        [Serializable]
        private class LogErrorCommand
            : CslaCommandBase<LogErrorCommand>
        {
            public LogErrorCommand(Guid id, string application, string host, string type, string source, string message, string user, int statusCode, DateTime time, string xml)
            {
                if (id == null)
                    throw new ArgumentNullException("id");
                if (string.IsNullOrEmpty(application))
                    throw new ArgumentNullException("application");
                if (string.IsNullOrEmpty(message))
                    throw new ArgumentNullException("message");
                if (string.IsNullOrEmpty(xml))
                    throw new ArgumentNullException("xml");
                
                this.Id = id;
                this.Application = application;
                this.Host = host;
                this.Type = type;
                this.Source = source;
                this.Message = message;
                this.User = user;
                this.StatusCode = statusCode;
                this.Time = time;
                this.Xml = xml;
            }


            public static PropertyInfo<Guid> IdProperty = RegisterProperty<Guid>(c => c.Id);
            public Guid Id
            {
                get { return ReadProperty(IdProperty); }
                private set { LoadProperty(IdProperty, value); }
            }

            public static PropertyInfo<string> ApplicationProperty = RegisterProperty<string>(c => c.Application);
            public string Application
            {
                get { return ReadProperty(ApplicationProperty); }
                private set { LoadProperty(ApplicationProperty, value); }
            }

            public static PropertyInfo<string> HostProperty = RegisterProperty<string>(c => c.Host);
            public string Host
            {
                get { return ReadProperty(HostProperty); }
                private set { LoadProperty(HostProperty, value); }
            }

            public static PropertyInfo<string> TypeProperty = RegisterProperty<string>(c => c.Type);
            public string Type
            {
                get { return ReadProperty(TypeProperty); }
                private set { LoadProperty(TypeProperty, value); }
            }

            public static PropertyInfo<string> SourceProperty = RegisterProperty<string>(c => c.Source);
            public string Source
            {
                get { return ReadProperty(SourceProperty); }
                private set { LoadProperty(SourceProperty, value); }
            }

            public static PropertyInfo<string> MessageProperty = RegisterProperty<string>(c => c.Message);
            public string Message
            {
                get { return ReadProperty(MessageProperty); }
                private set { LoadProperty(MessageProperty, value); }
            }

            public static PropertyInfo<string> UserProperty = RegisterProperty<string>(c => c.User);
            public string User
            {
                get { return ReadProperty(UserProperty); }
                private set { LoadProperty(UserProperty, value); }
            }

            public static PropertyInfo<int> StatusCodeProperty = RegisterProperty<int>(c => c.StatusCode);
            public int StatusCode
            {
                get { return ReadProperty(StatusCodeProperty); }
                private set { LoadProperty(StatusCodeProperty, value); }
            }

            public static PropertyInfo<DateTime> TimeProperty = RegisterProperty<DateTime>(c => c.Time);
            public DateTime Time
            {
                get { return ReadProperty(TimeProperty); }
                private set { LoadProperty(TimeProperty, value); }
            }

            public static PropertyInfo<string> XmlProperty = RegisterProperty<string>(c => c.Xml);
            public string Xml
            {
                get { return ReadProperty(XmlProperty); }
                private set { LoadProperty(XmlProperty, value); }
            }

            /// <summary>
            /// We work with the cache on the server side of the DataPortal
            /// </summary>
            protected override void DataPortal_Execute()
            {
                using (var ctx = ContextFactory.GetContext())
                {
                    var item = new ErrorDto()
                    {
                        Id = this.Id,
                        Application = this.Application,
                        ChainId = this.GetChainId(),
                        TenantId = this.GetTenantId(),
                        Host = this.Host,
                        Type = this.Type,
                        Source = this.Source,
                        Message = this.Message,
                        User = this.User,
                        StatusCode = this.StatusCode,
                        UtcTime = this.Time.ToUniversalTime(),
                        Xml = this.Xml
                    };
                    repository.Insert(item);
                }
            }

            private int GetTenantId()
            {
                var tenant = appContext.CurrentTenant;
                if (tenant != null)
                    return tenant.Id;
                return 0;
            }

            private int GetChainId()
            {
                var tenant = appContext.CurrentTenant;
                if (tenant != null)
                    return tenant.ChainId;
                return 0;
            }

            #region Dependency Injection

            [NonSerialized]
            [NotUndoable]
            private IErrorRepository repository;
            public IErrorRepository Repository
            {
                set
                {
                    // Don't allow the value to be set to null
                    if (value == null)
                    {
                        throw new ArgumentNullException("value");
                    }
                    // Don't allow the value to be set more than once
                    if (this.repository != null)
                    {
                        throw new InvalidOperationException();
                    }
                    this.repository = value;
                }
            }

            [NonSerialized]
            [NotUndoable]
            private Context.IApplicationContext appContext;
            public Context.IApplicationContext AppContext
            {
                set
                {
                    // Don't allow the value to be set to null
                    if (value == null)
                    {
                        throw new ArgumentNullException("value");
                    }
                    // Don't allow the value to be set more than once
                    if (this.appContext != null)
                    {
                        throw new InvalidOperationException();
                    }
                    this.appContext = value;
                }
            }

            #endregion
        }

        #endregion
    }
}
