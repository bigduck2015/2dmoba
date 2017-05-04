using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemy : MonoBehaviour 
{
    private List<int> mEnters = new List<int>();
    public player.playerInfo mInfo;

    private float m_offset_h = 0;

	// Use this for initialization
	void Start () 
    {
        init();

	}

    void Update()
    {
        transform.Translate(0, 0, m_offset_h * Time.deltaTime);
    }

    [PunRPC]
    void OnPlayerInfo(string name, float hp)
    {
        mInfo.hp = hp;
    }

    [PunRPC]
    void OnMove(float speed)
    {
        //Debug.LogError("OnMove = " + offset_h);
        //GameObject.Find("UITest").GetComponent<Text>().text = "OnMove = " + offset_h;

        m_offset_h = speed;
    }

    [PunRPC]
    void OnSkill(byte id)
    {
        switch (id)
        {
            case 1:
                this.GetComponent<enemyskill>().StartRollBoomCo();
                break;
            default:
                Debug.LogError("non skill id!");
                break;
        }
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
