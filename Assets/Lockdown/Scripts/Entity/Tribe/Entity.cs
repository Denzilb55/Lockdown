using System;
using Photon.Pun;
using UnityEngine;


namespace Lockdown.Game.Entity
{
    /// <summary>
    /// A basic entity class, which will represent AI, player-controlled and networked agents.
    /// </summary>
    public abstract class Entity : MonoBehaviour
    {
        public void NetworkDestroy()
        {
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC(nameof(_Destroy), RpcTarget.All);
        }
        
        [PunRPC]
        public void _Destroy()
        {
            Destroy(gameObject);
        }
    }
}