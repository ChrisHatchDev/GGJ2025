using Normal.Realtime;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private Realtime _realtime;
    [SerializeField] private GameObject _playerPrefab;
    
    public Transform PlayerOneSpawn;
    public Transform PlayerTwoSpawn;

    private void Awake() {
        // Get the Realtime component on this game object
        _realtime = GetComponent<Realtime>();

        // Notify us when Realtime successfully connects to the room
        _realtime.didConnectToRoom += DidConnectToRoom;
    }

    private void DidConnectToRoom(Realtime realtime)
    {
        var playerOne = realtime.clientID == 0;
        var playerPosition = playerOne ? PlayerOneSpawn.position : PlayerTwoSpawn.position;
        var playerRotation = playerOne ? PlayerOneSpawn.rotation : PlayerTwoSpawn.rotation;
        
        // Instantiate the Player for this client once we've successfully connected to the room
        GameObject playerGameObject = Realtime.Instantiate(
            prefabName: _playerPrefab.name,  // Prefab name
            ownedByClient: true,      // Make sure the RealtimeView on this prefab is owned by this client
            preventOwnershipTakeover: true,      // Prevent other clients from calling RequestOwnership() on the root RealtimeView.
            useInstance: realtime,
            position: playerPosition,
            rotation: playerRotation
        ); // Use the instance of Realtime that fired the didConnectToRoom event.

        
        // Get a reference to the player
        var player = playerGameObject.GetComponentInChildren<Player>();
        player.PlayerNumber = playerOne ? Player.Players.One : Player.Players.Two;

        var childrenRealtimeViews = playerGameObject.GetComponentsInChildren<RealtimeTransform>();
        foreach (var childRealtimeTransform in childrenRealtimeViews)
        {
            if (!childRealtimeTransform.isOwnedLocallyInHierarchy)
            {
                continue;
            }
            
            childRealtimeTransform.RequestOwnership();
            childRealtimeTransform.realtimeView.RequestOwnership();
        }

        // var playerTransform = playerGameObject.GetComponentInChildren<RealtimeTransform>();
        // if (playerTransform.realtimeView.isOwnedLocallyInHierarchy)
        // {
        //     playerTransform.realtimeView.RequestOwnershipOfSelfAndChildren();
        //     playerTransform.RequestOwnership();
        // }
    }
}
