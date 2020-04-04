using System.Collections.Generic;
using Lockdown.Game.Entity;
using Photon.Pun;
using UnityEngine;
using Lockdown.Game.Entity;

namespace Lockdown.Game
{
    public class EnemyManagerModule : MonoBehaviour
    {
        private static EnemyManagerModule _instance;
        public static EnemyManagerModule Instance
        {
            get
            {
                if (_instance == null)
                {
                    // Instantiate a game object in the scene for the network module
                    GameObject networkModuleObj = new GameObject();
                    _instance = networkModuleObj.AddComponent<EnemyManagerModule>();
                }

                return _instance;
            }
        }
        
        private List<Enemy> _enemies = new List<Enemy>();

        private Enemy _enemyPrefab;

        public void Init(Enemy prefab)
        {
            _enemyPrefab = prefab;
            // Randomly spawn food every 2 seconds

            if (PhotonNetwork.IsMasterClient)
            {
                InvokeRepeating(nameof(SpawnEnemy), 0.5f, 2);
            }
        }
        
        void SpawnEnemy()
        {
            GameObject enemyObj = PhotonNetwork.Instantiate(_enemyPrefab.name, Random.insideUnitCircle.normalized * 10, Quaternion.identity, 0);
            _enemies.Add(enemyObj.GetComponent<Enemy>());
        }
    }
}