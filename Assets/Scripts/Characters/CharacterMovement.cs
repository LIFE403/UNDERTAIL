using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float m_Speed;
    public GameObject Sprite;

    private Animator m_Anim;
    bool m_PlayerMoving;
    Vector2 m_LastMove;

    public GameManager gameManager;
    Vector3 m_DirVec;
    GameObject m_ScanObj;

    void Start()
    {
        m_Anim = Sprite.GetComponent<Animator>();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Return))
        //    Debug.Log("test1");

        // 엔터를 눌렀을 때 스캔한 오브젝트가 null이 아니고, 텍스트가 출력되고 있지 않을 때
        if (Input.GetKeyDown(KeyCode.Return) && m_ScanObj != null && gameManager.isTexting != true)
        {
            //Debug.Log("TEST2");
            gameManager.Action(m_ScanObj);
        }
    }

    private void FixedUpdate()
    {
        m_PlayerMoving = false;

        // 스무스 X -> GetAxisRaw
        // 텍스트창이 켜져있을 경우 0으로 설정
        var horizon = gameManager.isAction ? 0 : Input.GetAxisRaw("Horizontal");  // 수직
        var vertical = gameManager.isAction ? 0 : Input.GetAxisRaw("Vertical");   // 수평

        if (horizon != 0f || vertical != 0f)
        {
            m_PlayerMoving = true;
            m_LastMove = new Vector2(horizon, vertical);
        }

        // 캐릭터가 보고 있는 방향을 m_DirVec에 저장
        if (m_LastMove.y == 1)
            m_DirVec = Vector3.up;
        else if (m_LastMove.y == -1)
            m_DirVec = Vector3.down;
        else if (m_LastMove.x == 1)
            m_DirVec = Vector3.right;
        else if (m_LastMove.x == -1)
            m_DirVec = Vector3.left;

        float speed = Time.deltaTime * m_Speed;
        transform.Translate(new Vector3(horizon, vertical, 0) * speed);

        // 애니메이션
        m_Anim.SetFloat("MoveX", horizon);
        m_Anim.SetFloat("MoveY", vertical);
        m_Anim.SetFloat("LastMoveX", m_LastMove.x);
        m_Anim.SetFloat("LastMoveY", m_LastMove.y);
        m_Anim.SetBool("PlayerMoving", m_PlayerMoving);

        // 플레이어가 보고 있는 방향으로 ray 그리고 ray에 닿은 객체의 레이어가 "Object"일 때만 감지해서 m_ScanObj에 저장
        Debug.DrawRay(transform.position, m_DirVec * 0.2f, new Color(0, 1, 0));
        RaycastHit2D ray = Physics2D.Raycast(transform.position, m_DirVec, 0.7f, LayerMask.GetMask("Object"));

        if (ray.collider != null)
            m_ScanObj = ray.collider.gameObject;
        else
            m_ScanObj = null;
    }
}
