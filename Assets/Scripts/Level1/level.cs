using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level : MonoBehaviour 
{
    private GameObject player;
    private List<Collider> mEnemyEnters;

    void Awake()
    {
        this.GetComponent<skill>().del_damage += OnDamage;

    }

	// Use this for initialization
	void Start () 
    {
        init();
	}
	
    void init()
    {
        player = PhotonNetwork.Instantiate("player", new Vector3(0, 0.5f, 0), Quaternion.identity, 0);
        player.name = "player";

        playerctrl ctrl = player.AddComponent<playerctrl>();
        ctrl.player = player.transform;
    }


    void OnEnemyTriggerEnter(Collider collider)
    {
        mEnemyEnters.Add(collider);
    }

    void OnEnemyTriggerExit(Collider collider)
    {
        mEnemyEnters.Remove(collider);
    }

    void OnDamage(GameObject ammo, data.skill skill)
    {
        if (skill.name == "Boom")
        {
            foreach (var item in mEnemyEnters)
            {
                if (item.gameObject == ammo)
                {
                    Debug.LogError("Boom");
                    break;
                }
            }
        }
    }
}
