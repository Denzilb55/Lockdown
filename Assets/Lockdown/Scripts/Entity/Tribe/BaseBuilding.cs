﻿using System;
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

        private void Start()
        {
            if (tribe == TribeManagerModule.Instance.MainTribe)
            {
                InvokeRepeating(nameof(SpawnMyTribesman), 2, 2);
            }
        }

        void SpawnMyTribesman()
        {
            tribe.SpawnTribesman(transform.position);
        }
    }
}
