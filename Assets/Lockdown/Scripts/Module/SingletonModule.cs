using Photon.Pun;
using UnityEngine;

namespace Lockdown.Game
{
    /// <summary>
    /// The base singleton module type which provides an instance
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonModule<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    // Instantiate a game object in the scene for the network module
                    GameObject networkModuleObj = new GameObject();
                    _instance = networkModuleObj.AddComponent<T>();
                }

                return _instance;
            }
        }
    }
}