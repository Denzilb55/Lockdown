using System;
using System.Collections;
using System.Collections.Generic;
using Lockdown.Game.Tribes;
using UnityEngine;

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

        protected override void OnSetTribe()
        {
            if (tribe == TribeManagerModule.Instance.MainTribe)
            {
                InvokeRepeating(nameof(SpawnMyTribesman), 2, 2);
            }
        }
    }
}
