using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Lockdown.Game.Entity
{
    public class Enemy : Entity
    {
        [SerializeField]
        private Rigidbody2D _body;
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                Vector2 delta = BaseModule.Instance.Base.transform.position - transform.position;
                _body.velocity = delta.normalized * 0.8f;
            }
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
