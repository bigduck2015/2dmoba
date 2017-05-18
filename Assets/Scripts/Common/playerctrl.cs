using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerctrl : MonoBehaviour 
{
    public Transform player;
    public Transform enemy;
    private bool isMoveTouch;
	private float m_pc_speed;

    enum phase
    {
        left,
        right,
        idel
    }

    private Coroutine mTouchCtrlCo;

	// Use this for initialization
	void Start () 
    {
        StartTouchCtrlCo();
	}

    private bool m_movelock;

	// Update is called once per frame
	void Update () 
    {

		if (Input.GetKeyDown (KeyCode.A)) 
        {
            m_pc_speed = -5;
            this.GetComponent<PhotonView> ().RPC ("OnMove", PhotonTargets.Others, m_pc_speed);
		}
        else if (Input.GetKeyDown (KeyCode.D)) 
        {
            m_pc_speed = 5;
            this.GetComponent<PhotonView> ().RPC ("OnMove", PhotonTargets.Others, m_pc_speed);
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            m_pc_speed = 0;
            this.GetComponent<PhotonView> ().RPC ("OnMove", PhotonTargets.Others, m_pc_speed);
        }
            
        transform.Translate(transform.right * -m_pc_speed * Time.deltaTime);

        if (enemy != null)
        {
            player.LookAt(enemy.position);
        }

        if (Input.GetKeyUp(KeyCode.U))
        {
            player.GetComponent<skill>().StartRollBoomCo();
        }
	}

    void OnBtnMove()
    {
        isMoveTouch = true;
    }

    public void StartTouchCtrlCo()
    {
        mTouchCtrlCo = StartCoroutine(TouchCtrlCo());
    }

    IEnumerator TouchCtrlCo()
    {
        float speed = 0;
        bool move = false;
        var uiTest = GameObject.Find("UITest").GetComponent<Text>();
        uiTest.text = "";
        phase state = phase.idel;
        phase curstate = phase.idel;

        while (true)
        {
            //Debug.LogError(Time.deltaTime);

            if (isMoveTouch)
            {
                if (Input.touchCount > 0)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        move = true;
                    }
                    else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        isMoveTouch = false;
                        move = false;
                        curstate = phase.idel;
                        speed = 0;

                        if (curstate != state)
                        {
                            state = curstate;
                            this.GetComponent<PhotonView>().RPC("OnMove", PhotonTargets.Others, speed);
                            Debug.LogError("speed = " + speed);
                        }
                    }
                }
            }

            if (move)
            {
                // Get movement of the finger since last frame
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

                //uiTest.text = touchDeltaPosition.x.ToString();

                if (touchDeltaPosition.x > 2)
                {
                    Debug.LogError("curstate = phase.right");
                    curstate = phase.right;
                    speed = 5f;
                }
                else if (touchDeltaPosition.x < -2)
                {
                    curstate = phase.left;
                    speed = -5f;
                }

                if (curstate != state)
                {
                    state = curstate;
                    this.GetComponent<PhotonView>().RPC("OnMove", PhotonTargets.Others, speed);
                    Debug.LogError("speed = " + speed);
                }

                // Move object across XY plane
                transform.Translate(transform.right * -speed * Time.deltaTime);
            }


            yield return null;
        }
    }
}
