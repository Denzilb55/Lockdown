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


                StartPlacingBase();
            };

        }

        private void StartPlacingBase()
        {
            _uiManager.ShowText("Place your base!");
            _gameState = GameState.PlaceBase;
        }

        private void Update()
        {

            switch (_gameState)
            {
                case GameState.PlaceBase:
                    if (Input.GetMouseButtonDown(0))
                    {
                        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        TribeManagerModule.Instance.MainTribe.SpawnBuilding(pos);
                        _gameState = GameState.Playing;
                        _uiManager.HideText();
                    }

                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        _gameState = GameState.Playing;
                        _uiManager.HideText();
                    }
                    
                    break;

                case GameState.Playing:
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

                    if (hit.collider != null)
                    {
                        Tribesman tribesman = hit.collider.GetComponent<Tribesman>();

                        if (tribesman != null && !tribesman.tribe.IsMainTribe)
                        {
                            tribesman.Smite();
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.B))
                    {
                        StartPlacingBase();
                    }
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }



        [PunRPC]
        public void CreateOpposingTribe(int tribeId)
        {
            TribeManagerModule.Instance.CreateManagedObject(tribeId);
        }
        
    }
}