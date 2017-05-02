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

        var offset = transform.forward * 5;
        var Target = Boom.position + new Vector3(offset.x, 0, 0);

        while (true) 
        {
            Boom.position = Vector3.MoveTowards (Boom.position, Target, 0.1f);

            if (Boom.position == Target)
            {
                Debug.LogError("enemy RollBoomCo break");

                Destroy(Boom.gameObject);

                break;
            }

            yield return new WaitForSeconds(0.02f);
        }
    }
}
