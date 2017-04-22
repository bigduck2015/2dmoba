using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerctrl : MonoBehaviour 
{
    public Transform player;

	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        float offset_h = Input.GetAxis("Horizontal");
        float target = offset_h + player.position.x;
        float move = Mathf.MoveTowards(player.position.x, target, 0.1f);
        //Debug.LogError("target = "+ target);
        player.position = new Vector3(move, 0.5f, 0);
	}
}
