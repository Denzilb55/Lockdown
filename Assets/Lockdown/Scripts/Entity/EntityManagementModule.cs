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
    public class EntityManagementModule<TModule, TEntity> : ManagementModule<TModule, TEntity>
        where TEntity : Entity.Entity where TModule : EntityManagementModule<TModule, TEntity>
    {
        /// <summary>
        /// The prefab that will be duplicated when new entities are instantiated.
        /// </summary>
        protected TEntity _blueprintPrefab;


        public void Init(TEntity prefab)
        {
            _blueprintPrefab = prefab;
            // Randomly spawn food every 2 seconds
            
            OnInit();
        }
        
        public new TEntity CreateManagedObject()
        {
            return CreateManagedObject(Vector2.zero);
        }
        
        public TEntity CreateManagedObject(Vector2 pos)
        {
            GameObject obj = PhotonNetwork.Instantiate(_blueprintPrefab.name, pos, Quaternion.identity, 0);
            var entity = obj.GetComponent<TEntity>();
            _managedObjects.Add(entity);
            return entity;
        }

        protected virtual void OnInit()
        {
            
        }
        

        protected void StartSpawnRepeating(float delay, Func<Vector2> nextPosition)
        {
            StartCoroutine(SpawnRepeating(delay, nextPosition));
        }
      
        private IEnumerator SpawnRepeating(float delay, Func<Vector2> nextPosition)
        {
            while (true)
            {
                CreateManagedObject(nextPosition());
                yield return new WaitForSeconds(delay);
            }
        }
    }
}