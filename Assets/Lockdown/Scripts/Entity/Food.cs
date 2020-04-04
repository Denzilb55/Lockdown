using System;
using Photon.Pun;
using UnityEngine;

namespace Lockdown.Game.Entity
{
    public class Food : MonoBehaviour
    {
        private void OnDestroy()
        {
            FoodModule.Instance.DestroyFood(this);
        }

        [PunRPC]
        public void NetworkDestroy()
        {
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC(nameof(_Destroy), RpcTarget.All);
        }
        
        public void _Destroy()
        {
            Destroy(gameObject);
        }
    }
}