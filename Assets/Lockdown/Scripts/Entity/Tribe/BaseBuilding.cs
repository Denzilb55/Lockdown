using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lockdown.Game.Entities
{
    public class BaseBuilding : Entity
    {
        private void OnDestroy()
        {
            BaseModule.Instance.DeRegisterEntity(this);
        }


        private void Awake()
        {
            BaseModule.Instance.RegisterEntity(this);
        }
    }
}
