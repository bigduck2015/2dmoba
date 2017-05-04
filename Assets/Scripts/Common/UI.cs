﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour 
{
    public static UI Instance;

    public Text player;
    public Text enemy;

	// Use this for initialization
	void Awake () 
    {
        Instance = this;
	}

    public void OnBtnMove()
    {
        Debug.LogError("UI.OnBtnMove");
    }

    public void SetPlayerHP(float hp)
    {
        player.text = hp.ToString();
    }

    public void SetEnemyHP(float hp)
    {
        enemy.text = hp.ToString();
    }

    public void CreateBtnSkill1()
    {
        var father = GameObject.Find("Canvas/Skills").transform;
        var BtnSkill1 = logic.Instantiate("BtnSkill1", father);

        level Level = GameObject.Find("Level").GetComponent<level>();

        BtnSkill1.GetComponent<Button>().onClick.AddListener( delegate() 
        {
            Level.OnBtnSkill1();  
        });
    }
}
