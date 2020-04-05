using System;
using Lockdown.Game.Entities;
using Lockdown.Game.Tribes;
using Photon.Pun;
using UnityEngine;
using NetworkPlayer = Lockdown.Game.Entities.NetworkPlayer;
using Random = UnityEngine.Random;

namespace Lockdown.Game
{

    public enum GameState
    {
        PlaceBase,
        Playing
    }
    
    /// <summary>
    /// The Game Manager is where it all happens.
    /// </summary>
    public class GameManager : MonoBehaviourPunCallbacks
    {
        [Header("Managers")] 
        [SerializeField] private UiManager _uiManager;
        
        [Header("Prefabs")]
        
        [SerializeField]
        private NetworkPlayer _playerPrefab;
        
        [SerializeField]
        private Food _foodPrefab;
        
        [SerializeField]
        private BaseBuilding _basePrefab;
        
        [SerializeField]
        private Tribesman _enemyPrefab;
        
        

        private PhotonView _photonView;

        private GameState _gameState = GameState.PlaceBase;

        
        
        private void Start()
        {

            
            
            NetworkManager.Instance.OnReady += () =>
            {
                
                Debug.Log(PhotonNetwork.LocalPlayer.ActorNumber);
               // localPlayer = PhotonNetwork.Instantiate(_playerPrefab.name, new Vector3(Random.Range(-3, 3),Random.Range(-3, 3),0f), Quaternion.identity, 0);
                Debug.Log("Instantiated Player (You)");
                
                FoodModule.Instance.Init(_foodPrefab);
                BaseModule.Instance.Init(_basePrefab);
                TribesmanManagerModule.Instance.Init(_enemyPrefab);
                
                _photonView = PhotonView.Get(this);
                _photonView.RPC(nameof(CreateOpposingTribe), RpcTarget.AllBufferedViaServer, PhotonNetwork.LocalPlayer.ActorNumber-1);
                
                
                _uiManager.ShowText("Place your base!");
            };

        }

        private void Update()
        {
            if (_gameState == GameState.PlaceBase)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    TribeManagerModule.Instance.MainTribe.SpawnBuilding(pos);
                    _gameState = GameState.Playing;
                    _uiManager.HideText();
                }
                
            }
        }



        [PunRPC]
        public void CreateOpposingTribe(int tribeId)
        {
            TribeManagerModule.Instance.CreateManagedObject(tribeId);
        }
        
    }
}