using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SpawnManager : NetworkManager {

    public GameObject DronePrefab;
    public Vector3 playerSpawnPos;

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        if(conn.address == "localClient")
        {
            base.OnServerAddPlayer(conn, playerControllerId);
            //var player = (GameObject)GameObject.Instantiate(DronePrefab, playerSpawnPos, Quaternion.identity);
            //NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        }
        else
        {
            //base.OnServerAddPlayer(conn, playerControllerId);
            var player = (GameObject)GameObject.Instantiate(DronePrefab, playerSpawnPos, Quaternion.identity);
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        }


    }

}
