using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PGameManager : MonoBehaviourPunCallbacks
{
    public GameObject PlayerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector3(0,0),Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Leave()
    {
        PhotonNetwork.LeaveRoom(); 
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("LobbyPhoton");
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        
    }
}
