using System.Collections.Generic;
using Lockdown.Game.Entity;
using Photon.Pun;
using UnityEngine;

namespace Lockdown.Game
{
    public class BaseModule : MonoBehaviour
    {
        private static BaseModule _instance;
        public static BaseModule Instance
        {
            get
            {
                if (_instance == null)
                {
                    // Instantiate a game object in the scene for the network module
                    GameObject networkModuleObj = new GameObject();
                    _instance = networkModuleObj.AddComponent<BaseModule>();
                }

                return _instance;
            }
        }

        public BaseBuilding Base
        {
            get;
            private set;
        }

        private BaseBuilding _basePrefab;
        
        

        public void Init(BaseBuilding prefab)
        {
            _basePrefab = prefab;
            // Randomly spawn food every 2 seconds

            if (PhotonNetwork.IsMasterClient)
            {
                SpawnBase();
            }
        }

        public void DestroyBase(BaseBuilding baseBuilding)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                //
            }
        }
        
        void SpawnBase()
        {
            GameObject obj = PhotonNetwork.Instantiate(_basePrefab.name, new Vector3(0, 0), Quaternion.identity, 0);
            Base = obj.GetComponent<BaseBuilding>();
        }
    }
}