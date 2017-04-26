using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour 
{
    private float mHP;

    void Awake()
    {
        //this.GetComponent<skill>().del_damage += OnDamage;
    }

	// Use this for initialization
	void Start () 
    {
        init();
	}

    void OnTriggerStay(Collider collider)
    {
        Debug.LogError("OnTriggerStay");
    }

//    void OnDamage(float damage)
//    {
//        mHP -= damage;
//    }

    void init()
    {
        mHP = 100;
    }

    void OnDestroy()
    {
        //this.GetComponent<skill>().del_damage -= OnDamage;
    }
}
