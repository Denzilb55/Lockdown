using Lockdown.Game.Entity;
using UnityEngine;

namespace Lockdown.Game
{
    public class EnemyManagerModule : ManagementModule<EnemyManagerModule, Enemy>
    {
        protected override void OnInit()
        {
            base.OnInit();

            StartSpawnRepeating(2, () => Random.insideUnitCircle.normalized * 10);
        }
    }
}