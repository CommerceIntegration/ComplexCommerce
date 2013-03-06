using System;
using CslaLibrary = Csla;
using Csla;
using Csla.Core;

namespace ComplexCommerce.Csla.DI
{
    [Serializable]
    public abstract class ServerInjectableBusinessListBase<T, C> :
        CslaLibrary.BusinessListBase<T, C>
        where T : CslaLibrary.BusinessListBase<T, C>
        where C : IEditableBusinessObject
    {
        [NonSerialized]
        private bool mIsServerInjected = false;

        private bool IsServerInjected
        {
            get { return mIsServerInjected; }
            set { mIsServerInjected = value; }
        }


        protected override void DataPortal_OnDataPortalInvoke(DataPortalEventArgs e)
        {
            //inject dependencies into instance 
            InjectDependencies();

            //call base class
            base.DataPortal_OnDataPortalInvoke(e);
        }

        protected override void Child_OnDataPortalInvoke(DataPortalEventArgs e)
        {
            //inject dependencies into instance 
            InjectDependencies();

            //call base class
            base.Child_OnDataPortalInvoke(e);
        }

        protected override void OnDeserialized()
        {
            //inject dependencies into instance 
            InjectDependencies();

            // call base class
            base.OnDeserialized();
        }

        protected void InjectDependencies()
        {
            if (!IsServerInjected)
            {
                Inject();
                IsServerInjected = true;
            }
        }

        private void Inject()
        {
            if (DI.IoC.Container != null)
                DI.IoC.Container.Inject(this);
            // TODO: Throw sensible exception here explaining how to configure the container
            // for use in dependency injection.
        }
    }
}
