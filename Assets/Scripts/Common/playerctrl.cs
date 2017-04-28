using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerctrl : MonoBehaviour 
{
    public Transform player;
    public Transform enemy;

    private Coroutine mTouchCtrlCo;

	// Use this for initialization
	void Start () 
    {
        StartTouchCtrlCo();
	}

	// Update is called once per frame
	void FixedUpdate () 
    {
        //Debug.LogError(gameObject.name);
        float offset_h = Input.GetAxis("Horizontal");
        float target = offset_h + player.position.x;
        float move = Mathf.MoveTowards(player.position.x, target, 0.1f);
        //Debug.LogError("target = "+ target);
        player.position = new Vector3(move, 0.5f, 0);

        if (enemy != null)
        {
            player.LookAt(enemy.position);
        }

        if (Input.GetKeyUp(KeyCode.U))
        {
            player.GetComponent<skill>().StartRollBoomCo();
        }
	}

    public void StartTouchCtrlCo()
    {
        mTouchCtrlCo = StartCoroutine(TouchCtrlCo());
    }

    IEnumerator TouchCtrlCo()
    {
        float speed = 100f;

        while (true)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                // Get movement of the finger since last frame
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

                // Move object across XY plane
                transform.Translate(0, 0, touchDeltaPosition.x * speed);
            }

            yield return new WaitForSeconds(0.02f);
        }
    }
}
