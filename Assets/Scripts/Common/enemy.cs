using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour 
{
    private List<int> mEnters = new List<int>();
    public player.playerInfo mInfo;

	// Use this for initialization
	void Start () 
    {
        init();
	}

    [PunRPC]
    void OnPlayerInfo(string name, float hp)
    {
        mInfo.hp = hp;
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.LogError("On Enemy TriggerEnter");

        var view = collider.gameObject.GetComponent<PhotonView>();

        if (view != null)
        {
            var id = view.viewID;
            mEnters.Add(id);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        Debug.LogError("On Enemy TriggerExit");

        var view = collider.gameObject.GetComponent<PhotonView>();
        if (view != null)
        {
            var id = view.viewID;
            mEnters.Remove(id);
        }
    }

    void init()
    {
        this.gameObject.name = "enemy";
        GameObject.Find("player").GetComponent<playerctrl>().enemy = transform;
    }

    public void CheckDamage(GameObject ammo, skill.skillInfo skill)
    {
        var id = ammo.GetComponent<PhotonView>().viewID;

        if (skill.name == "Boom")
        {
            if (mEnters.Contains(id))
            {
                Debug.LogError("Boom");
                mEnters.Remove(id);
                this.GetComponent<PhotonView>().RPC("OnRPCDamage", PhotonTargets.Others, id, skill.damage);
            }
        }
    }


}
