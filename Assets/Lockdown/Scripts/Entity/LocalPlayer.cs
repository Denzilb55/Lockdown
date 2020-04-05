using System;
using Lockdown.Game.Tribes;
using Photon.Pun;
using UnityEngine;

namespace Lockdown.Game.Entities
{
    /// <summary>
    /// The local player is "You" or any people sitting next to you if this turns out to have
    /// couch co-op support.
    /// </summary>
    public class LocalPlayer : Player
    {
        private Vector2 _moveDir;
        private Rigidbody2D _body;

        private void Start()
        {
            _body = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _moveDir = Vector2.zero;
            if (Input.GetKey(KeyCode.W))
            {
                _moveDir += Vector2.up;
            }
            
            if (Input.GetKey(KeyCode.A))
            {
                _moveDir += Vector2.left;
            }
            
            if (Input.GetKey(KeyCode.S))
            {
                _moveDir += Vector2.down;
            }
            
            if (Input.GetKey(KeyCode.D))
            {
                _moveDir += Vector2.right;
            }
        }

        private void FixedUpdate()
        {
            _body.velocity = _moveDir;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Food food = other.transform.GetComponent<Food>();
            // check if collided object is food, and consume
            Debug.Log("Collide: " + other);
            if (food != null)
            {
                // consume food
                food.NetworkDestroy();
                PhotonView photonView = PhotonView.Get(this);
                photonView.RPC(nameof(ConsumeFood), RpcTarget.All);
            }
            else
            {
                           
                Tribesman enemy = other.transform.GetComponent<Tribesman>();
                // check if collided object is enemy, and destroy
                if (enemy != null && enemy.tribe != TribeManagerModule.Instance.MainTribe)
                {
                    enemy.NetworkDestroy();
                } 
            }

        }
    }
}