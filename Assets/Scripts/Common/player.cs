using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour 
{
    public float HP;
	private bool mIsEnemy;

    private List<int> mEnters = new List<int>();

    public delegate void damageDel(int type, float damage);
    public damageDel del_damage;


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
        if (info.sender.IsLocal == false)
        {
            Debug.LogError("On Enemy PhotonInstantiate");
            mIsEnemy = true;
            this.gameObject.name = "enemy";
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (mIsEnemy)
        {
            Debug.LogError("On Enemy TriggerEnter");
        }
        else
        {
            Debug.LogError("On Player TriggerEnter");
        }

        var view = collider.gameObject.GetComponent<PhotonView>();

        if (view != null)
        {
            var id = view.viewID;
            mEnters.Add(id);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (mIsEnemy)
        {
            Debug.LogError("On Enemy TriggerExit");
        }
        else
        {
            Debug.LogError("On Player TriggerExit");
        }

        var view = collider.gameObject.GetComponent<PhotonView>();
        if (view != null)
        {
            var id = view.viewID;
            mEnters.Remove(id);
        }
    }

    public void CheckDamage(GameObject ammo, data.skill skill)
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

        var enemy = GameObject.Find("enemy").GetComponent<player>();
        enemy.HP -= damage;

        UI.Instance.SetEnemyHP(enemy.HP);
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
                HP -= damage;
                enemy.GetComponent<PhotonView>().RPC("OnRPCDamageConfirm", PhotonTargets.Others, damage);
                mEnters.Remove(id);

                UI.Instance.SetPlayerHP(HP);

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
        HP = 100;

    }

    void OnDestroy()
    {
        //this.GetComponent<skill>().del_damage -= OnDamage;
    }
}
