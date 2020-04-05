using System.Collections;
using Lockdown.Game.Entities;
using Photon.Pun;
using UnityEngine;

namespace Lockdown.Game
{
    public class FoodModule : EntityManagementModule<FoodModule, Food>
    {
        public void DestroyFood(Food food)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                _managedObjects.Remove(food);
            }
        }
        

        protected override void OnInit()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                StartSpawnRepeating(2, () => new Vector2(Random.Range(-6f, 6f), Random.Range(-6f, 6f)));

                for (int i = 0; i < 50; i++)
                {
                    CreateManagedObject(new Vector2(Random.Range(-6f, 6f), Random.Range(-6f, 6f)));
                }
            }
        }
        
    }
}