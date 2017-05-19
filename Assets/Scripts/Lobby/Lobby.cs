using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour 
{
    public GameObject Btn_Match;
    private bool isCreater=false;

    void OnJoinedLobby()                //加入大厅  
    {  
        Debug.Log("We joined the lobby.");  
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
            SceneManager.LoadScene(2);
        }
    }

    void OnLeftRoom()                   //离开房间  
    {  
        Debug.Log("This client has left a game room.");
        Btn_Match.SetActive(true);
    } 

    void OnPhotonPlayerConnected(PhotonPlayer player)  //玩家连接  
    {  
        Debug.Log("Player connected: " + player);


        SceneManager.LoadScene(2);
    } 

    public void OnBtnMatch()
    {
        RoomOptions roomOptions = new RoomOptions() { isVisible = false, maxPlayers = 2 };
        PhotonNetwork.JoinOrCreateRoom("123", roomOptions, TypedLobby.Default);
        Btn_Match.SetActive(false);
    }

    void OnApplicationPause( bool pauseStatus )
    {
        if (pauseStatus) 
        {
            Debug.Log("app moved to background");

            if (PhotonNetwork.inRoom)
            {
                PhotonNetwork.LeaveRoom();
            }
        } 
        else
        {
            Debug.Log("app is foreground again");
        }
    }

	// Use this for initialization
	void Start () 
    {
        init();
	}
	
    void init()
    {

    }
}
