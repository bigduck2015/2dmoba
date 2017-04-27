using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skill : MonoBehaviour 
{
	private Coroutine mRollBoomCo;
    private Coroutine mRollBoomCDCo;

//    public delegate void damagedel(GameObject boom, data.skill damage);
//    public damagedel del_damage;

	// Use this for initialization
	void Start () 
	{

	}

    public void init()
    {
        data.skill boom = new data.skill();
        boom.name = "Boom";
        boom.damage = 10f;

        data.Instance.Dic_Skill.Add("Boom", boom);
    }

    public void StartRollBoomCo()
    {
        if (mRollBoomCDCo == null)
        {
            mRollBoomCo = StartCoroutine(RollBoomCo());
            mRollBoomCDCo = StartCoroutine(RollBoomCDCo());
        }
    }

    IEnumerator RollBoomCDCo()
    {
        yield return new WaitForSeconds(1);
        mRollBoomCDCo = null;
    }

	IEnumerator RollBoomCo()
	{
        var enemy = GameObject.Find("enemy");
        var Boom = PhotonNetwork.Instantiate("Boom", Vector3.zero, Quaternion.identity, 0).transform;
        Boom.position = new Vector3(transform.position.x, Boom.position.y, Boom.position.z);
        var Target = Boom.position + new Vector3(5, 0, 0);

		while (true) 
        {
            Boom.position = Vector3.MoveTowards (Boom.position, Target, 0.1f);

            if (Boom.position == Target)
            {
                Debug.LogError("RollBoomCo break");

                enemy.GetComponent<player>().CheckDamage(Boom.gameObject, data.Instance.Dic_Skill["Boom"]);

                PhotonNetwork.Destroy(Boom.gameObject);

                break;
            }

            yield return new WaitForSeconds(0.02f);
		}
	}
}
