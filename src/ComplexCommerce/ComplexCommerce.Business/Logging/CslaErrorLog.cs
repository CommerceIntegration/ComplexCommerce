using System;
using System.Collections;
using Elmah;

namespace ComplexCommerce.Business.Logging
{
    public class CslaErrorLog
        : global::Elmah.ErrorLog
    {
        public CslaErrorLog(IDictionary config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            //
            // Set the application name as this implementation provides
            // per-application isolation over a single store.
            //
            var appName = config.Find("applicationName", string.Empty);

            if (appName.Length > maxAppNameLength)
            {
                throw new Elmah.ApplicationException(string.Format(
                    "Application name is too long. Maximum length allowed is {0} characters.",
                    maxAppNameLength.ToString("N0")));
            }

            ApplicationName = appName;
        }

        private const int maxAppNameLength = 60;

        /// <summary>
        /// Gets the name of this error log implementation.
        /// </summary>
        public override string Name
        {
            get { return "CSLA Error Log"; }
        }

        /// <summary>
        /// Logs an error to the database.
        /// </summary>
        /// <remarks>
        /// Use the stored procedure called by this implementation to set a
        /// policy on how long errors are kept in the log. The default
        /// implementation stores all errors for an indefinite time.
        /// </remarks>
        public override string Log(Elmah.Error error)
        {
            if (error == null)
                throw new ArgumentNullException("error");

            var errorXml = ErrorXml.EncodeString(error);
            var id = Guid.NewGuid();

            id = Error.LogError(
                id, 
                ApplicationName, 
                error.HostName, 
                error.Type, 
                error.Source, 
                error.Message, 
                error.User, 
                error.StatusCode,
                error.Time, 
                errorXml);

            return id.ToString();
        }

        /// <summary>
        /// Returns a page of errors from the databse in descending order 
        /// of logged time.
        /// </summary>
        public override int GetErrors(int pageIndex, int pageSize, IList errorEntryList)
        {
            if (pageIndex < 0)
                throw new ArgumentOutOfRangeException("pageIndex", pageIndex, null);
            if (pageSize < 0)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, null);

            var list = ErrorList.GetErrorList(ApplicationName, pageIndex, pageSize);
            ErrorListToErrorLogEntryList(list, errorEntryList);

            return list.TotalRowCount;
        }

        private void ErrorListToErrorLogEntryList(ErrorList list, IList errorEntryList)
        {
            foreach (var item in list)
            {
                var error = new Elmah.Error()
                {
                    ApplicationName = item.Application,
                    HostName = item.Host,
                    Type = item.Type,
                    Message = item.Message,
                    Source = item.Source,
                    User = item.User,
                    Time = item.UtcTime.ToLocalTime(),
                    StatusCode = item.StatusCode,
                };

                errorEntryList.Add(new ErrorLogEntry(this, item.Id.ToString(), error));
            }
        }

        /// <summary>
        /// Returns the specified error from the database, or null 
        /// if it does not exist.
        /// </summary>
        public override ErrorLogEntry GetError(string id)
        {
            if (id == null) 
                throw new ArgumentNullException("id");
            if (id.Length == 0) 
                throw new ArgumentException(null, "id");

            Guid errorGuid;

            try
            {
                errorGuid = new Guid(id);
            }
            catch (FormatException e)
            {
                throw new ArgumentException(e.Message, "id", e);
            }

            string errorXml = Error.GetErrorXml(errorGuid, this.ApplicationName);

            if (errorXml == null)
                return null;

            var error = ErrorXml.DecodeString(errorXml);
            return new ErrorLogEntry(this, id, error);
        }
    }
}
