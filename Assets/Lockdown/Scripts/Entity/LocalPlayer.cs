using System;
using Photon.Pun;
using UnityEngine;

namespace Lockdown.Game.Entity
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
                PhotonNetwork.Destroy(food.gameObject);
                PhotonView photonView = PhotonView.Get(this);
                photonView.RPC(nameof(ConsumeFood), RpcTarget.All);
            }
        }
    }
}