using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Lockdown.Game
{
    
    /// <summary>
    /// The Network Manager class is a singleton which extends Photon Unity Networking callbacks
    /// to handle networking for this game.
    /// </summary>
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        public static NetworkManager Instance;

        public event Action OnReady;

        private void Awake()
        {
            Instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Connecting ...");
            PhotonNetwork.ConnectUsingSettings();
        }
        
        
        public override void OnConnectedToMaster()
        {
            Debug.Log("OnConnectedToMaster() was called by PUN.");
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 0; // no limit
            TypedLobby typedLobby = new TypedLobby("default lobby", LobbyType.Default);
            if (PhotonNetwork.JoinOrCreateRoom("GameRoom", roomOptions, typedLobby))
            {
                Debug.Log("joining room");
            }
        }
        
        /// <summary>
        /// Called when the local user/client left a room, so the game's logic can clean up it's internal state.
        /// </summary>
        /// <remarks>
        /// When leaving a room, the LoadBalancingClient will disconnect the Game Server and connect to the Master Server.
        /// This wraps up multiple internal actions.
        ///
        /// Wait for the callback OnConnectedToMaster, before you use lobbies and join or create rooms.
        /// </remarks>
        public override void OnLeftRoom()
        {
            Debug.Log("Left room");
        }
        
        /// <summary>
        /// Called when the server couldn't create a room (OpCreateRoom failed).
        /// </summary>
        /// <remarks>
        /// The most common cause to fail creating a room, is when a title relies on fixed room-names and the room already exists.
        /// </remarks>
        /// <param name="returnCode">Operation ReturnCode from the server.</param>
        /// <param name="message">Debug message for the error.</param>
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.LogError("Failed Creating Room: " + message);
        }

        /// <summary>
        /// Called when a previous OpJoinRoom call failed on the server.
        /// </summary>
        /// <remarks>
        /// The most common causes are that a room is full or does not exist (due to someone else being faster or closing the room).
        /// </remarks>
        /// <param name="returnCode">Operation ReturnCode from the server.</param>
        /// <param name="message">Debug message for the error.</param>
        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.LogError("Failed Joining Room: " + message);
        }

        /// <summary>
        /// Called when this client created a room and entered it. OnJoinedRoom() will be called as well.
        /// </summary>
        /// <remarks>
        /// This callback is only called on the client which created a room (see OpCreateRoom).
        ///
        /// As any client might close (or drop connection) anytime, there is a chance that the
        /// creator of a room does not execute OnCreatedRoom.
        ///
        /// If you need specific room properties or a "start signal", implement OnMasterClientSwitched()
        /// and make each new MasterClient check the room's state.
        /// </remarks>
        public override void OnCreatedRoom()
        {
            Debug.Log("Created Room");
        }

        /// <summary>
        /// Called on entering a lobby on the Master Server. The actual room-list updates will call OnRoomListUpdate.
        /// </summary>
        /// <remarks>
        /// While in the lobby, the roomlist is automatically updated in fixed intervals (which you can't modify in the public cloud).
        /// The room list gets available via OnRoomListUpdate.
        /// </remarks>
        public override void OnJoinedLobby()
        {
            Debug.Log("Joined Lobby");
        }

        /// <summary>
        /// Called after leaving a lobby.
        /// </summary>
        /// <remarks>
        /// When you leave a lobby, [OpCreateRoom](@ref OpCreateRoom) and [OpJoinRandomRoom](@ref OpJoinRandomRoom)
        /// automatically refer to the default lobby.
        /// </remarks>
        public override void OnLeftLobby()
        {
            Debug.Log("Left Lobby");
        }

        /// <summary>
        /// Called after disconnecting from the Photon server. It could be a failure or intentional
        /// </summary>
        /// <remarks>
        /// The reason for this disconnect is provided as DisconnectCause.
        /// </remarks>
        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.Log("Disconnected");
        }
        

        /// <summary>
        /// Called when the LoadBalancingClient entered a room, no matter if this client created it or simply joined.
        /// </summary>
        /// <remarks>
        /// When this is called, you can access the existing players in Room.Players, their custom properties and Room.CustomProperties.
        ///
        /// In this callback, you could create player objects. For example in Unity, instantiate a prefab for the player.
        ///
        /// If you want a match to be started "actively", enable the user to signal "ready" (using OpRaiseEvent or a Custom Property).
        /// </remarks>
        public override void OnJoinedRoom()
        {
            Debug.Log("Joined Room");
            OnReady?.Invoke();
        }
    }
}

