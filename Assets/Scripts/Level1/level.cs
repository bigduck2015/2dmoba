﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level : MonoBehaviour 
{
    private GameObject player;

	// Use this for initialization
	void Start () 
    {
        init();
	}
	
    void init()
    {
        player = PhotonNetwork.Instantiate("player", new Vector3(0, 0.5f, 0), Quaternion.identity, 0);
        playerctrl ctrl = player.AddComponent<playerctrl>();
        ctrl.player = player.transform;
    }


}
