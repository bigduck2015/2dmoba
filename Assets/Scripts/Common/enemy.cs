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
        transform.Translate(transform.right * -m_offset_h * Time.deltaTime);
    }

    [PunRPC]
    void OnPlayerInfo(string name, float hp)
    {
        Debug.LogError("OnPlayerInfo hp = " + hp);
        mInfo.hp = hp;
        UI.Instance.SetEnemyHP(mInfo.hp);
    }

    [PunRPC]
    void OnMove(float speed)
    {
        //Debug.LogError("OnMove = " + offset_h);
        //GameObject.Find("UITest").GetComponent<Text>().text = "OnMove = " + offset_h;

        m_offset_h = speed;
    }

    [PunRPC]
    void OnSkill(byte id, object value)
    {
        switch (id)
        {
            case 1:
                this.GetComponent<enemyskill>().StartRollBoomCo();
                break;
            case 2:
                this.GetComponent<enemyskill>().StartHideCo((float)value);
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

        if (mEnters.Contains(id))
        {
            mEnters.Remove(id);
        }
        else
        {
            return;
        }

        if (skill.name == "Boom")
        {
            Debug.LogError("Boom");
            this.GetComponent<PhotonView>().RPC("OnRPCDamage", PhotonTargets.Others, id, skill.damage);
        }
        else if (skill.name == "Hide")
        {
            Debug.LogError("Hide");
            this.GetComponent<PhotonView>().RPC("OnRPCDamage", PhotonTargets.Others, id, skill.damage);
        }
    }


}
