using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunCall : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
		
	}
	
    void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        if (info.sender.IsLocal == false)
        {
            Debug.LogError("On Enemy PhotonInstantiate");
            this.gameObject.AddComponent<enemy>();
            this.gameObject.AddComponent<enemyskill>();
            GameObject.Find("player").GetComponent<player>().SendPlayerInfo();
        }
    }
}
