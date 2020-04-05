using System.Collections;
using System.Collections.Generic;

namespace Lockdown.Game
{
    /// <summary>
    /// The base management module, which manages entities of type TEntity.
    /// </summary>
    /// <typeparam name="TModule"></typeparam>
    /// <typeparam name="T"></typeparam>
    public class ManagementModule<TModule, T> : SingletonModule<TModule>, IEnumerable<T> 
        where TModule : ManagementModule<TModule, T>
    {
        protected List<T> _managedObjects = new List<T>();

        public int Count => _managedObjects.Count;

        public T GetObject(int index)
        {
            return _managedObjects[index];
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _managedObjects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
    }
}