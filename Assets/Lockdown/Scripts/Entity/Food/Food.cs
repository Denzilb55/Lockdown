using System;
using Photon.Pun;
using UnityEngine;

namespace Lockdown.Game.Entities
{
    public class Food : Entity
    {
        private void OnDestroy()
        {
            FoodModule.Instance.DeRegisterEntity(this);
        }


        private void Awake()
        {
            FoodModule.Instance.RegisterEntity(this);
        }
    }
}