using System;
using System.Collections;
using System.Collections.Generic;
using Lockdown.Game.Tribes;
using UnityEngine;
using Random = System.Random;

namespace Lockdown.Game.Entities
{
    public class BaseBuilding : TribeEntity
    {
        private void OnDestroy()
        {
            BaseModule.Instance.DeRegisterEntity(this);
        }


        private void Awake()
        {
            BaseModule.Instance.RegisterEntity(this);
        }
        

        void SpawnMyTribesman()
        {
            if (tribe.foodCount >= 3)
            {
                Tribesman tribesman = tribe.SpawnTribesman(transform.position);
                tribe.ConsumeFood(3);
            }
        }

        void GrowCrops()
        {
            Vector2 pos = (Vector2) (transform.position) + UnityEngine.Random.insideUnitCircle * 4;
            FoodModule.Instance.CreateManagedObject(pos);
        }

        protected override void OnSetTribe()
        {
            if (tribe == TribeManagerModule.Instance.MainTribe)
            {
                InvokeRepeating(nameof(SpawnMyTribesman), 2, 2);
                InvokeRepeating(nameof(GrowCrops), 15, 15);
            }
        }
    }
}
