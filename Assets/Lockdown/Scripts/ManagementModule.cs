using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Lockdown.Game
{
    /// <summary>
    /// The base management module, which manages entities of type TEntity.
    /// </summary>
    /// <typeparam name="TModule"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public class ManagementModule<TModule, TEntity> : SingletonModule<TModule>, IEnumerable<TEntity> 
        where TEntity : Entity.Entity where TModule : ManagementModule<TModule, TEntity>
    {
        protected List<TEntity> _managedEntities = new List<TEntity>();

        /// <summary>
        /// The prefab that will be duplicated when new entities are instantiated.
        /// </summary>
        protected TEntity _blueprintPrefab;

        private int _count;
        private int _count1;

        public void Init(TEntity prefab)
        {
            _blueprintPrefab = prefab;
            // Randomly spawn food every 2 seconds
            

            OnInit();
        }
        
        public TEntity SpawnEntity()
        {
            return SpawnEntity(Vector2.zero);
        }

        public TEntity SpawnEntity(Vector2 pos)
        {
            GameObject obj = PhotonNetwork.Instantiate(_blueprintPrefab.name, pos, Quaternion.identity, 0);
            var entity = obj.GetComponent<TEntity>();
            _managedEntities.Add(entity);
            return entity;
        }

        protected virtual void OnInit()
        {
            
        }

        public int EntityCount => _managedEntities.Count;

        public TEntity GetEntity(int index)
        {
            return _managedEntities[index];
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return _managedEntities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected void StartSpawnRepeating(float delay, Func<Vector2> nextPosition)
        {
            StartCoroutine(SpawnRepeating(delay, nextPosition));
        }
      
        private IEnumerator SpawnRepeating(float delay, Func<Vector2> nextPosition)
        {
            while (true)
            {
                SpawnEntity(nextPosition());
                yield return new WaitForSeconds(delay);
            }
        }
    }
}