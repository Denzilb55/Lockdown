using Lockdown.Game.Entity;
using Photon.Pun;
using UnityEngine;
using NetworkPlayer = Lockdown.Game.Entity.NetworkPlayer;

namespace Lockdown.Game
{
    /// <summary>
    /// The Game Manager is where it all happens.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private NetworkPlayer _playerPrefab;
        
        private void Awake()
        {
            NetworkModule.Instance.OnReady += () =>
            {
                GameObject localPlayer = PhotonNetwork.Instantiate(_playerPrefab.name, new Vector3(Random.Range(-3, 3),Random.Range(-3, 3),0f), Quaternion.identity, 0);
                Debug.Log("Instantiated Player (You)");
                
                // Remove the network player component, these are for network player instances
                Destroy(localPlayer.GetComponent<NetworkPlayer>());
                
                // Add the local player component so that you can control it
                localPlayer.AddComponent<LocalPlayer>();
            };
        }
    }
}