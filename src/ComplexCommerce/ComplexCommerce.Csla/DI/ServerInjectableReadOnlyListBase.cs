using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Csla;

namespace ComplexCommerce.Csla.DI
{
    [Serializable]
    public abstract class ServerInjectableReadOnlyListBase<T, C> :
        ReadOnlyListBase<T, C>
        where T : ReadOnlyListBase<T, C>
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

        private void InjectDependencies()
        {
            if (!IsServerInjected)
            {
                Inject();
                IsServerInjected = true;
            }
        }

        //protected abstract void Inject();

        protected virtual void Inject()
        {
            if (DI.IoC.Container != null)
                DI.IoC.Container.Inject(this);
            // TODO: Throw sensible exception here explaining how to configure the container
            // for use in dependency injection.
        }
    }
}
