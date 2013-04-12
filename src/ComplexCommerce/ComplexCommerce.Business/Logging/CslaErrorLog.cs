using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
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
                    Time = item.Time.ToLocalTime(), // TODO: work out how to convert this to local time
                    StatusCode = item.StatusCode,
                };

                errorEntryList.Add(new ErrorLogEntry(this, item.Id.ToString(), error));
            }
        }

        ///// <summary>
        ///// Returns a page of errors from the databse in descending order 
        ///// of logged time.
        ///// </summary>
        //public override int GetErrors(int pageIndex, int pageSize, ICollection<ErrorLogEntry> errorEntryList)
        //{
        //    if (pageIndex < 0) 
        //        throw new ArgumentOutOfRangeException("pageIndex", pageIndex, null);
        //    if (pageSize < 0) 
        //        throw new ArgumentOutOfRangeException("pageSize", pageSize, null);

        //    var list = ErrorList.GetErrorList(ApplicationName, pageIndex, pageSize);

        //    foreach (var item in list)
        //    {
        //        var error = new Elmah.Error()
        //        {
        //            ApplicationName = item.Application,
        //            HostName = item.Host,
        //            Type = item.Type,
        //            Message = item.Message,
        //            Source = item.Source,
        //            //Detail = item. // TODO: figure out where this is coming from
        //            User = item.User,
        //            Time = item.Time, // TODO: work out how to convert this to local time
        //            StatusCode = item.StatusCode,
        //            //WebHostHtmlMessage = item. // TODO: figure out where this is coming from
        //        };

        //        errorEntryList.Add(new ErrorLogEntry(this, item.Id.ToString(), error));
        //    }

        //    return list.TotalRowCount;
        //}

        // TODO: Add Async Methods

        //public override IAsyncResult BeginGetError(string id, AsyncCallback asyncCallback, object asyncState)
        //{
        //    //return base.BeginGetError(id, asyncCallback, asyncState);
        //}

         

        //public override IAsyncResult BeginGetErrors(int pageIndex, int pageSize, IList errorEntryList, AsyncCallback asyncCallback, object asyncState)
        //{
        //    if (pageIndex < 0) 
        //        throw new ArgumentOutOfRangeException("pageIndex", pageIndex, null);
        //    if (pageSize < 0) 
        //        throw new ArgumentOutOfRangeException("pageSize", pageSize, null);


            

        //    //
        //    // Create a closure to handle the ending of the async operation
        //    // and retrieve results.
        //    //
        //    AsyncResultWrapper asyncResult = null;

        //    //var result = new AsyncResult();

        //    ErrorList list = null;
            


        //    Func<IAsyncResult, int> endHandler = delegate
        //    {
        //        //Debug.Assert(asyncResult != null);

        //        if (list != null)
        //        {
        //            this.ErrorListToErrorLogEntryList(list, errorEntryList);
        //            return list.TotalRowCount;
        //        }
        //        else
        //        {
        //            return 0;
        //        }


        //        //using (connection)
        //        //using (command)
        //        //{
        //        //    var xml = ReadSingleXmlStringResult(command.EndExecuteReader(asyncResult.InnerResult));
        //        //    ErrorsXmlToList(xml, errorEntryList);
        //        //    int total;
        //        //    Commands.GetErrorsXmlOutputs(command, out total);
        //        //    return total;
        //        //}
        //    };

        //        //ErrorList list;
        //        //ErrorList.GetErrorList(this.ApplicationName, pageIndex, pageSize, (o, e) =>
        //        //{
        //        //    if (e.Error != null)
        //        //        throw e.Error;
        //        //    else
        //        //        list = e.Object;
        //        //}, asyncState);

        //    //
        //    // Open the connenction and execute the command asynchronously,
        //    // returning an IAsyncResult that wrap the downstream one. This
        //    // is needed to be able to send our own AsyncState object to
        //    // the downstream IAsyncResult object. In order to preserve the
        //    // one sent by caller, we need to maintain and return it from
        //    // our wrapper.
        //    //

        //    //asyncResult = new AsyncResultWrapper(
        //    //    ErrorList.GetErrorList(this.ApplicationName, pageIndex, pageSize, endHandler, asyncState), asyncState
        //    //    );

        //    asyncResult = new AsyncResultWrapper(asyncState);

        //    ErrorList.GetErrorList(this.ApplicationName, pageIndex, pageSize, 
        //        (o, e) =>
        //        {
        //            if (e.Error != null)
        //                throw e.Error;
        //            else
        //            {
        //                list = e.Object;
        //                endHandler(asyncResult);
        //                asyncResult.ActionCompleted();
        //                //endHandler(result);

        //                //this.ErrorListToErrorLogEntryList(list, errorEntryList);
                        
        //                //((ManualResetEvent)result.AsyncWaitHandle).Set();
        //            }
        //        }
        //    , asyncState);


        //    //return result;

        //    return asyncResult;

        //    //try
        //    //{
        //    //    connection.Open();

        //    //    asyncResult = new AsyncResultWrapper(
        //    //        command.BeginExecuteReader(
        //    //            asyncCallback != null ? /* thunk */ delegate { asyncCallback(asyncResult); } : (AsyncCallback)null,
        //    //            endHandler), asyncState);

        //    //    return asyncResult;
        //    //}
        //    //catch (Exception)
        //    //{
        //    //    connection.Dispose();
        //    //    throw;
        //    //}
        //}



        ///// <summary>
        ///// Ends an asynchronous version of <see cref="ErrorLog.GetErrors"/>.
        ///// </summary>
        //public override int EndGetErrors(IAsyncResult asyncResult)
        //{
        //    if (asyncResult == null)
        //        throw new ArgumentNullException("asyncResult");

        //    //var wrapper = asyncResult as AsyncResultWrapper;

        //    //if (wrapper == null)
        //    //    throw new ArgumentException("Unexepcted IAsyncResult type.", "asyncResult");

        //    //var endHandler = (Func<IAsyncResult, int>)wrapper.InnerResult.AsyncState;
        //    //return endHandler(wrapper.InnerResult);

        //    var endHandler = (Func<IAsyncResult, int>)asyncResult.AsyncState;
        //    return endHandler(asyncResult);
        //}


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

            //using (var connection = new SqlConnection(ConnectionString))
            //using (var command = Commands.GetErrorXml(ApplicationName, errorGuid))
            //{
            //    command.Connection = connection;
            //    connection.Open();
            //    errorXml = (string)command.ExecuteScalar();
            //}

            if (errorXml == null)
                return null;

            var error = ErrorXml.DecodeString(errorXml);
            return new ErrorLogEntry(this, id, error);
        }


        /// <summary>
        /// An <see cref="IAsyncResult"/> implementation that wraps another.
        /// </summary>
        private sealed class AsyncResultWrapper 
            : IAsyncResult
        {
            public AsyncResultWrapper(object asyncState)
            {
                AsyncState = asyncState;
            }

            private ManualResetEvent asyncWaitHandle;
            private bool isCompleted = false;

            internal void ActionCompleted()
            {
                this.isCompleted = true;
                this.FaultInWaitHandle();
                this.asyncWaitHandle.Set();
            }

            public bool IsCompleted
            {
                get { return isCompleted; }
            }

            public WaitHandle AsyncWaitHandle
            {
                get 
                {
                    this.FaultInWaitHandle();
                    return this.asyncWaitHandle; 
                }
            }

            public object AsyncState { get; private set; }

            public bool CompletedSynchronously
            {
                get { return false; }
            }

            private void FaultInWaitHandle()
            {
                lock (this)
                {
                    if (this.asyncWaitHandle == null)
                    {
                        this.asyncWaitHandle = new ManualResetEvent(false);
                    }
                }
            }

        }

        ///// <summary>
        ///// An <see cref="IAsyncResult"/> implementation that wraps another.
        ///// </summary>
        //private sealed class AsyncResultWrapper : IAsyncResult
        //{
        //    public AsyncResultWrapper(IAsyncResult inner, object asyncState)
        //    {
        //        InnerResult = inner;
        //        AsyncState = asyncState;
        //    }

        //    public IAsyncResult InnerResult { get; private set; }

        //    public bool IsCompleted
        //    {
        //        get { return InnerResult.IsCompleted; }
        //    }

        //    public WaitHandle AsyncWaitHandle
        //    {
        //        get { return InnerResult.AsyncWaitHandle; }
        //    }

        //    public object AsyncState { get; private set; }

        //    public bool CompletedSynchronously
        //    {
        //        get { return InnerResult.CompletedSynchronously; }
        //    }
        //}
    }
}
