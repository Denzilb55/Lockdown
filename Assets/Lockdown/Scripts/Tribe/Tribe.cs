using Lockdown.Game.Entities;
using UnityEngine;

namespace Lockdown.Game.Tribes
{
    public class Tribe
    {
        public int tribeId;

        public string tribeName;
        
        public bool IsMainTribe = false;

        public int foodCount
        {
            get;
            private set;
        }

        public Color color
        {
            get;
            private set;
        }

        public Tribe()
        {
            foodCount = 30;
        }

        public void CollectFood(int count = 1)
        {
            foodCount += count;
        }

        public void ConsumeFood(int count = 1)
        {
            foodCount -= count;
        }

        public void Initialise(int id, string name, Color color)
        {
            tribeId = id;
            tribeName = name;
            this.color = color;
        }

        public Tribesman SpawnTribesman(Vector2 pos)
        {
            var tribesman = TribesmanManagerModule.Instance.CreateManagedObject(pos);
            tribesman.SetTribe(this);
            return tribesman;
        }

        public BaseBuilding SpawnBuilding(Vector2 pos)
        {
            var tribesman = BaseModule.Instance.CreateManagedObject(pos);
            tribesman.SetTribe(this);
            return tribesman;
        }
        
    }
}