using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] private CameraFollow _camera;
    [SerializeField] private TeleportScript _teleport;
    [SerializeField] private TeleportScript _teleport1;
    [SerializeField] private TeleportScript _teleport2;
    [SerializeField] private TeleportScript _teleport3;
    [SerializeField] private TeleportScript _teleport4;
    public GameObject[] playerPrefabs;
    public Transform[] spawnPoints;

    private void Start()
    {
        int randomNumber = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomNumber];
        GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
        var go = PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint.position, Quaternion.identity);

        if(go.GetComponent<PhotonView>().IsMine)
        {
            _teleport.player = go;
            _teleport1.player = go;
            _teleport2.player = go;
            _teleport3.player = go;
            _teleport4.player = go;
            _camera.target = go.GetComponent<Controller2D>();
        }
    }
}
