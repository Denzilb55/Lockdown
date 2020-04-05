using System;
using Photon.Pun;
using UnityEngine;

namespace Lockdown.Game.Entity
{
    public class Food : Entity
    {
        private void OnDestroy()
        {
            FoodModule.Instance.DestroyFood(this);
        }
        

    }
}