using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lockdown.Game.Tribes
{
    public class TribeManagerModule : ManagementModule<TribeManagerModule, Tribe>
    {
        private int tribeCount;

        public Tribe MainTribe;
        
        
        private Stack<Color> _colors = new Stack<Color>(new List<Color>
        {
            Color.red,
            Color.blue,
            Color.yellow,
            Color.cyan,
            Color.magenta
        });

        private Color GetNextColor()
        {
            return _colors.Pop();
        }
        
        public new Tribe CreateManagedObject()
        {
            Tribe obj = new Tribe(tribeCount, "default", GetNextColor());
            tribeCount++;
            obj.IsMainTribe = false;
            _managedObjects.Add(obj);
            return obj;
        }
        
        public Tribe CreateMainTribe()
        {
            MainTribe = CreateManagedObject();
            MainTribe.IsMainTribe = true;
            return MainTribe;
        }
    }
}