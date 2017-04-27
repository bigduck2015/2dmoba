using System.Collections;
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
	
    public void SetPlayerHP(float hp)
    {
        player.text = hp.ToString();
    }

    public void SetEnemyHP(float hp)
    {
        enemy.text = hp.ToString();
    }

}
