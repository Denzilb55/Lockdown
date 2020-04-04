using System.Collections.Generic;
using Lockdown.Game.Entity;
using Photon.Pun;
using UnityEngine;

namespace Lockdown.Game
{
    public class FoodModule : MonoBehaviour
    {
        private static FoodModule _instance;
        public static FoodModule Instance
        {
            get
            {
                if (_instance == null)
                {
                    // Instantiate a game object in the scene for the network module
                    GameObject networkModuleObj = new GameObject();
                    _instance = networkModuleObj.AddComponent<FoodModule>();
                }

                return _instance;
            }
        }
        
        private List<Food> _foods = new List<Food>();

        private Food _foodPrefab;

        public void Init(Food prefab)
        {
            _foodPrefab = prefab;
            // Randomly spawn food every 2 seconds

            if (PhotonNetwork.IsMasterClient)
            {
                InvokeRepeating(nameof(SpawnFood), 0.5f, 2);
            }
        }

        public void DestroyFood(Food food)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                _foods.Remove(food);
            }
        }
        
        void SpawnFood()
        {
            GameObject foodObj = PhotonNetwork.Instantiate(_foodPrefab.name, new Vector3(Random.Range(-6f, 6f),Random.Range(-6f, 6f),0f), Quaternion.identity, 0);
            _foods.Add(foodObj.GetComponent<Food>());
        }
    }
}