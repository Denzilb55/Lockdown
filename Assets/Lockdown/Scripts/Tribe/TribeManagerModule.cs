using System;

namespace Lockdown.Game.Tribe
{
    public class TribeManagerModule : ManagementModule<TribeManagerModule, Tribe>
    {
        private int tribeCount;
        
        public new Tribe CreateManagedObject()
        {
            Tribe obj = new Tribe(tribeCount, "default");
            tribeCount++;
            _managedObjects.Add(obj);
            return obj;
        }
    }
}