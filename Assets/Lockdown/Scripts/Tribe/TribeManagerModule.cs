using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Lockdown.Game.Tribes
{
    public class TribeManagerModule : ManagementModule<TribeManagerModule, Tribe>
    {
        private int tribeCount;

        public Tribe MainTribe;
        
        
        private Queue<Color> _colors = new Queue<Color>(new List<Color>
        {
            Color.red,
            Color.blue,
            Color.yellow,
            Color.cyan,
            Color.magenta
        });

        private Color GetNextColor()
        {
            return _colors.Dequeue();
        }
        
        public Tribe CreateManagedObject(int tribeId)
        {
            Tribe tribe = new Tribe();
            tribe.Initialise(tribeCount, "default", GetNextColor());
            tribeCount++;
            

            if (_managedObjects.Count <= tribeId)
            {
                for (int i = _managedObjects.Count; i <= tribeId; i++)
                {
                    _managedObjects.Add(null);
                }
            }

            _managedObjects[tribeId] = tribe;
            
            if (tribeId + 1 == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                MainTribe = tribe;
                tribe.IsMainTribe = true;
            }
            
            return tribe;
        }
    }
}