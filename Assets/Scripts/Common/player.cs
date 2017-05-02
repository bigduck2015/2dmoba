using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour 
{
    public struct playerInfo
    {
        public string name;
        public float hp;
    }

    playerInfo mInfo;

    private List<int> mEnters = new List<int>();

    public delegate void damageDel(int type, float damage);
    public damageDel del_damage;


    void Awake()
    {
        
    }

	// Use this for initialization
	void Start () 
    {
        init();
	}

    void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.LogError("On Player TriggerEnter");

        var view = collider.gameObject.GetComponent<PhotonView>();

        if (view != null)
        {
            var id = view.viewID;
            mEnters.Add(id);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        Debug.LogError("On Player TriggerExit");

        var view = collider.gameObject.GetComponent<PhotonView>();

        if (view != null)
        {
            var id = view.viewID;
            mEnters.Remove(id);
        }
    }

    [PunRPC]//受击对象
    void OnRPCDamage(int id, float damage)
    {
        Debug.LogError("OnRPCDamage = " + id);

        StartCoroutine(AttackDetectionCo(id, damage));
    }

    [PunRPC]
    void OnRPCDamageConfirm(float damage)
    {
        Debug.LogError("OnRPCDamageConfirm = " + damage);

        var enemy = GameObject.Find("enemy").GetComponent<enemy>();
        enemy.mInfo.hp -= damage;

        UI.Instance.SetEnemyHP(enemy.mInfo.hp);
    }

    //攻击判定
    IEnumerator AttackDetectionCo(int id, float damage)
    {
        var enemy = GameObject.Find("enemy");
        float total_time = 0;
        Debug.LogError("AttackDetectionCo.mEnemyEnters.count = " + mEnters.Count);

        while (true)
        {
            if (mEnters.Contains(id))
            {
                mInfo.hp -= damage;
                enemy.GetComponent<PhotonView>().RPC("OnRPCDamageConfirm", PhotonTargets.Others, damage);
                mEnters.Remove(id);

                UI.Instance.SetPlayerHP(mInfo.hp);

                break;
            }

            yield return new WaitForSeconds(0.02f);

            total_time += Time.deltaTime + 0.02f;

            if (total_time >= 0.5f)
            {
                mEnters.Remove(id);
                break;
            }
        }
    }

    void init()
    {
        mInfo = new playerInfo();
        mInfo.name = "";
        mInfo.hp = 100;


    }

    public void SendPlayerInfo()
    {
        this.GetComponent<PhotonView>().RPC("OnPlayerInfo", PhotonTargets.Others, mInfo.name, mInfo.hp);
    }

    void OnDestroy()
    {
        
    }
}
