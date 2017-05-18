using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

        UI.Instance.CreateBtnSkill1();
    }

    public void OnBtnSkill1()
    {
        player.GetComponent<skill>().StartRollBoomCo();
    }

    void OnDestroy()
    {
        Debug.LogError("OnDestroy");
        PhotonNetwork.LeaveRoom();
    }
}
