using System.Collections;
using Lockdown.Game.Entity;
using Photon.Pun;
using UnityEngine;

namespace Lockdown.Game
{
    public class FoodModule : ManagementModule<FoodModule, Food>
    {
        public void DestroyFood(Food food)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                _managedEntities.Remove(food);
            }
        }
        

        protected override void OnInit()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                StartSpawnRepeating(2, () => new Vector2(Random.Range(-6f, 6f), Random.Range(-6f, 6f)));
            }
        }
        
    }
}