using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敌人技能动画映射
public class enemyskill : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
		
	}
	
    public void StartRollBoomCo()
    {
        StartCoroutine(RollBoomCo());
    }

    IEnumerator RollBoomCo()
    {
        var Boom = logic.Instantiate("Boom", null).transform;
        Boom.position = new Vector3(transform.position.x, Boom.position.y, Boom.position.z);

        var Target = transform.forward * 5 + Boom.transform.position;

        while (true) 
        {
            Boom.position = Vector3.MoveTowards (Boom.position, Target, 0.2f);

            Boom.RotateAround(Boom.position, transform.right, 20);

            if (Boom.position == Target)
            {
                Debug.LogError("enemy RollBoomCo break");

                Destroy(Boom.gameObject);

                break;
            }

            yield return new WaitForSeconds(0.02f);
        }
    }

    public void StartHideCo(float duration)
    {
        StartCoroutine(HideCo(duration));
    }

    IEnumerator HideCo(float duration)
    {
        this.gameObject.SetActive(false);
        yield return new WaitForSeconds(duration);
        this.gameObject.SetActive(true);
    }
}
