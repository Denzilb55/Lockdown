using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;

namespace Lockdown.Game
{
    /// <summary>
    /// The base management module, which manages entities of type TEntity.
    /// </summary>
    /// <typeparam name="TModule">This module type, needed for singleton instance access</typeparam>
    /// <typeparam name="TObject">The type of the managed object</typeparam>
    public class ManagementModule<TModule, TObject> : SingletonModule<TModule>, IEnumerable<TObject> 
        where TModule : ManagementModule<TModule, TObject>
    {
        protected List<TObject> _managedObjects = new List<TObject>();

        public int Count => _managedObjects.Count;
        
        public TObject CreateManagedObject()
        {
            TObject obj = Activator.CreateInstance<TObject>();
            _managedObjects.Add(obj);
            
            return obj;
        }
        

        public TObject GetObject(int index)
        {
            return _managedObjects[index];
        }

        public IEnumerator<TObject> GetEnumerator()
        {
            return _managedObjects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
    }
}