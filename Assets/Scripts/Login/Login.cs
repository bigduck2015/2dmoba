using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour 
{


	// Use this for initialization
	void Start () 
    {
        init();
	}
	
    void OnConnectedToPhoton()               //连接到Photon服务器  
    {  
        Debug.Log("This client has connected to a server");

    } 

    void OnDisconnectedFromPhoton()         //从Photon服务器断开  
    {  
        Debug.Log("This client has disconnected from the server"); 
        PhotonNetwork.ConnectUsingSettings("v1.0");
    }  

    void OnFailedToConnectToPhoton() //连接Photon服务器失败  
    {  
        Debug.Log("Failed to connect to Photon"); 

    }

    void OnConnectedToMaster()
    {
        Debug.Log("This client has connected to a Master");
        SceneManager.LoadScene(1);
    }

    void init()
    {
        PhotonNetwork.ConnectUsingSettings("v1.0");

    }
}
