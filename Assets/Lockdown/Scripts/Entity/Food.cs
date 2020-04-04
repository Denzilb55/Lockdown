using System;
using UnityEngine;

namespace Lockdown.Game.Entity
{
    public class Food : MonoBehaviour
    {
        private void OnDestroy()
        {
            FoodModule.Instance.DestroyFood(this);
        }
    }
}