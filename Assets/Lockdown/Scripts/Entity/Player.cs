using Photon.Pun;
using UnityEngine;

namespace Lockdown.Game.Entity
{
    /// <summary>
    /// The Player class represents a base Entity with player related functionality. It is
    /// extended by LocalPlayer and NetworkPlayer for local and network instances of the same
    /// object respectively.
    /// </summary>
    public abstract class Player : Entity
    {
        public int foodConsumed;
        
        [PunRPC]
        public void ConsumeFood()
        {
            Debug.Log("PLAYER CONSUMED FOOD");
            
            // Not synced properly for new players?
            foodConsumed++;
        }
    }
}