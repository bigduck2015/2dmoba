using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour 
{
    private float mHP;
	private bool mIsEnemy;

    public delegate void EnemyTriggerEnter(Collider collider);
    public EnemyTriggerEnter del_EnemyTriggerEnter;

    public delegate void EnemyTriggerExit(Collider collider);
    public EnemyTriggerExit del_EnemyTriggerExit;

    void Awake()
    {
        //this.GetComponent<skill>().del_damage += OnDamage;
    }

	// Use this for initialization
	void Start () 
    {
        init();
	}

    void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        Debug.LogError("OnPhotonInstantiate");
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.LogError("OnTriggerEnter");

        if (mIsEnemy)
        {
            if (del_EnemyTriggerEnter != null)
            {
                del_EnemyTriggerEnter(collider);
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        Debug.LogError("OnTriggerExit");

        if (mIsEnemy)
        {
            if (del_EnemyTriggerExit != null)
            {
                del_EnemyTriggerExit(collider);
            }
        }
    }

//    void OnDamage(float damage)
//    {
//        mHP -= damage;
//    }

    void init()
    {
        mHP = 100;

		if (this.GetComponent<PhotonView> ().isMine == false) 
        {
            mIsEnemy = true;
		}
    }

    void OnDestroy()
    {
        //this.GetComponent<skill>().del_damage -= OnDamage;
    }
}
