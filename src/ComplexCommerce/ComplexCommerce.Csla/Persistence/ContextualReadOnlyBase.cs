﻿using System;
using CslaLibrary = Csla;
using Csla;
using ComplexCommerce.Data.Context;

namespace ComplexCommerce.Csla.Persistence
{
    [Serializable]
    public class ContextualReadOnlyBase<T> :
        DI.ServerInjectableReadOnlyBase<T>
        where T : CslaLibrary.ReadOnlyBase<T>
    {
        // Injection of context factory
        [NonSerialized]
        [NotUndoable]
        private IPersistenceContextFactory contextFactory;
        public IPersistenceContextFactory ContextFactory
        {
            get
            {
                return this.contextFactory;
            }
            set
            {
                // Don't allow the value to be set to null
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                // Don't allow the value to be set more than once
                if (this.contextFactory != null)
                {
                    throw new InvalidOperationException("The contextFactory has already been instantiated.");
                }
                this.contextFactory = value;
            }
        }
    }
}
