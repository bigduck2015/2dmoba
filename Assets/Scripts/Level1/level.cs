using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class level : MonoBehaviour 
{
    private GameObject player;


    void Awake()
    {


    }

	// Use this for initialization
	void Start () 
    {
        init();
	}
	
    void init()
    {
        player = PhotonNetwork.Instantiate("player", new Vector3(0, 0.5f, 0), Quaternion.Euler(0, 90, 0), 0);
        player.name = "player";

        playerctrl ctrl = player.AddComponent<playerctrl>();
        ctrl.player = player.transform;

        player.AddComponent<player>();
        player.AddComponent<skill>().init();

        GameObject.Find("TouchMove").GetComponent<UIButtonMessage>().target = player;
        GameObject.Find("TouchMove").GetComponent<UIButtonMessage>().functionName = "OnBtnMove";

        UI.Instance.CreateBtnSkill();
    }

    void OnLeftRoom()                   //离开房间  
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(1);
    }

    public void OnBtnSkill1()
    {
        player.GetComponent<skill>().StartRollBoomCo();
    }

    public void  OnBtnSkill2()
    {
        player.GetComponent<skill>().StartHideCo();
    }

    void OnDestroy()
    {
        Debug.LogError("OnDestroy");
        PhotonNetwork.LeaveRoom();
    }
}
