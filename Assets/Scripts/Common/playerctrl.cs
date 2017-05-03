using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerctrl : MonoBehaviour 
{
    public Transform player;
    public Transform enemy;
    private bool isMoveTouch;

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
        float offset_h = Input.GetAxis("Horizontal");
        //Debug.LogError(offset_h);

        if (offset_h != 0 && !m_movelock)
        {
            this.GetComponent<PhotonView>().RPC("OnMove", PhotonTargets.Others, offset_h);
            m_movelock = true;
            //Debug.LogError("move");
        }

        if(offset_h == 0 && m_movelock)
        {
            this.GetComponent<PhotonView>().RPC("OnMove", PhotonTargets.Others, offset_h);
            m_movelock = false;
            //Debug.LogError("stop");
        }

        float target = offset_h + player.position.x;
        float move = Mathf.MoveTowards(player.position.x, target, 0.1f);

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

    void OnBtnMove()
    {
        //Debug.LogError("playerctrl.OnBtnMove");
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
        phase curstate;

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
                        m_movelock = false;
                        curstate = phase.idel;
                    }
                }
            }

            if (move)
            {
                // Get movement of the finger since last frame
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

                uiTest.text = touchDeltaPosition.x.ToString();

                if (touchDeltaPosition.x > 2 && !m_movelock)
                {
                    curstate = phase.right;
                    speed = 5f;
                    this.GetComponent<PhotonView>().RPC("OnMove", PhotonTargets.Others, speed);
                    m_movelock = true;
                }
                else if (touchDeltaPosition.x < -2 && !m_movelock)
                {
                    curstate = phase.left;
                    speed = -5f;
                    this.GetComponent<PhotonView>().RPC("OnMove", PhotonTargets.Others, speed);
                    m_movelock = true;
                }

                // Move object across XY plane
                transform.Translate(0, 0, speed * Time.deltaTime);
            }

            if (curstate != state)
            {
                m_movelock = false;
                state = curstate;
            }

            yield return null;
        }
    }
}
