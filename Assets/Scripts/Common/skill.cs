using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skill : MonoBehaviour 
{
    public Dictionary<string, skillInfo> Dic_Skill = new Dictionary<string, skillInfo>();

    public struct skillInfo
    {
        public int id;
        public string name;
        public float damage;
        public float duration;
    }

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
        skillInfo boom = new skillInfo();
        boom.id = 1;
        boom.name = "Boom";
        boom.damage = 10f;

        Dic_Skill.Add("Boom", boom);

        skillInfo hide = new skillInfo();
        hide.id = 2;
        hide.name = "Hide";
        hide.damage = 0f;
        hide.duration = 3f;

        Dic_Skill.Add("Hide", hide);
    }

    public void StartRollBoomCo()
    {
        if (mRollBoomCDCo == null)
        {
            mRollBoomCo = StartCoroutine(RollBoomCo());
            mRollBoomCDCo = StartCoroutine(RollBoomCDCo());

            this.GetComponent<PhotonView>().RPC("OnSkill", PhotonTargets.Others, (byte)1);
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
		var Boom = logic.Instantiate("Boom", null).transform;

        Boom.position = new Vector3(transform.position.x, Boom.position.y, Boom.position.z);

        var Target = transform.forward * 5 + Boom.transform.position;

		while (true) 
        {
            Boom.position = Vector3.MoveTowards (Boom.position, Target, 0.2f);

            Boom.RotateAround(Boom.position, transform.right, 20);

            if (Boom.position == Target)
            {
                Debug.LogError("RollBoomCo break");

                enemy.GetComponent<enemy>().CheckDamage(Boom.gameObject, Dic_Skill["Boom"]);

                Destroy(Boom.gameObject);

                break;
            }

            yield return new WaitForSeconds(0.02f);
		}
	}

    public void StartHideCo()
    {
        StartCoroutine(HideCo());
    }

    IEnumerator HideCo()
    {
        var skill = Dic_Skill["Hide"];
        var player = GameObject.Find("player");
        player.GetComponent<PhotonView>().RPC("OnSkill", PhotonTargets.Others, skill.id, skill.duration);
        player.transform.Find("Cube").GetComponent<MeshRenderer>().material.color = Color.gray;
        yield return new WaitForSeconds(skill.duration);
        player.transform.Find("Cube").GetComponent<MeshRenderer>().material.color = Color.white;
    }
}
