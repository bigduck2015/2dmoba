using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level0 : MonoBehaviour 
{
    public GameObject Btn_Match;
    private bool isCreater=false;

	// Use this for initialization
	void Start () 
    {
        init();
	}
	
    void OnConnectedToPhoton()               //连接到Photon服务器  
    {  
        Debug.Log("This client has connected to a server");

        Btn_Match.SetActive(true);
    } 

    void OnDisconnectedFromPhoton()         //从Photon服务器断开  
    {  
        Debug.Log("This client has disconnected from the server");  
    }  

    void OnFailedToConnectToPhoton() //连接Photon服务器失败  
    {  
        Debug.Log("Failed to connect to Photon");  
    }

    void OnCreatedRoom()                 //创建房间  
    {  
        Debug.Log("We have created a room.");  
        isCreater = true;
    }  

    void OnJoinedRoom()                   //加入房间  
    {  
        Debug.Log("We have joined a room.");

        if(isCreater==false)
        {
            SceneManager.LoadScene(1);
        }
    }  

    void OnLeftRoom()                   //离开房间  
    {  
        Debug.Log("This client has left a game room.");  
    } 

    void OnPhotonPlayerConnected(PhotonPlayer player)  //玩家连接  
    {  
        Debug.Log("Player connected: " + player);

        SceneManager.LoadScene(1);
    } 

    public void OnBtnMatch()
    {
        RoomOptions roomOptions = new RoomOptions() { isVisible = false, maxPlayers = 2 };
        PhotonNetwork.JoinOrCreateRoom("123", roomOptions, TypedLobby.Default);
    }

    void init()
    {
        PhotonNetwork.ConnectUsingSettings("v1.0");

        Btn_Match.SetActive(false);
    }
}
