using UnityEngine;

namespace Lockdown.Game.Entity
{
    /// <summary>
    /// The local player is "You" or any people sitting next to you if this turns out to have
    /// couch co-op support.
    /// </summary>
    public class LocalPlayer : Player
    {
        private void Update()
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(0, Time.deltaTime, 0);
            }
            
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(-Time.deltaTime, 0, 0);
            }
            
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(0, -Time.deltaTime, 0);
            }
            
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Time.deltaTime, 0, 0);
            }
        }
    }
}