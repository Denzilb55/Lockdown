using Lockdown.Game.Entities;
using Lockdown.Game.Tribes;
using Photon.Pun;
using UnityEngine;
using NetworkPlayer = Lockdown.Game.Entities.NetworkPlayer;

namespace Lockdown.Game
{
    /// <summary>
    /// The Game Manager is where it all happens.
    /// </summary>
    public class GameManager : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private NetworkPlayer _playerPrefab;
        
        [SerializeField]
        private Food _foodPrefab;
        
        [SerializeField]
        private BaseBuilding _basePrefab;
        
        [SerializeField]
        private Tribesman _enemyPrefab;

        private PhotonView _photonView;
        
        public GameObject localPlayer;

        
        
        private void Start()
        {

            NetworkManager.Instance.OnReady += () =>
            {
                localPlayer = PhotonNetwork.Instantiate(_playerPrefab.name, new Vector3(Random.Range(-3, 3),Random.Range(-3, 3),0f), Quaternion.identity, 0);
                Debug.Log("Instantiated Player (You)");
                
                // Remove the network player component, these are for network player instances
                Destroy(localPlayer.GetComponent<NetworkPlayer>());
                
                // Add the local player component so that you can control it
                localPlayer.AddComponent<LocalPlayer>();
                
                FoodModule.Instance.Init(_foodPrefab);
                BaseModule.Instance.Init(_basePrefab);
                TribesmanManagerModule.Instance.Init(_enemyPrefab);

                TribeManagerModule.Instance.CreateMainTribe();
                
                _photonView = PhotonView.Get(this);
                _photonView.RPC(nameof(CreateOpposingTribe), RpcTarget.OthersBuffered);
                
                
                InvokeRepeating(nameof(SpawnMyTribesman), 2, 2);
                
            };

        }

        void SpawnMyTribesman()
        {
            TribeManagerModule.Instance.MainTribe.SpawnTribesman(localPlayer.transform.position);
        }

        [PunRPC]
        public void CreateOpposingTribe()
        {
            TribeManagerModule.Instance.CreateManagedObject();
        }
        
    }
}