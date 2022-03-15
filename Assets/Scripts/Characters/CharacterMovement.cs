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

        // ���͸� ������ �� ��ĵ�� ������Ʈ�� null�� �ƴϰ�, �ؽ�Ʈ�� ��µǰ� ���� ���� ��
        if (Input.GetKeyDown(KeyCode.Return) && m_ScanObj != null && gameManager.isTexting != true)
        {
            //Debug.Log("TEST2");
            gameManager.Action(m_ScanObj);
        }
    }

    private void FixedUpdate()
    {
        m_PlayerMoving = false;

        // ������ X -> GetAxisRaw
        // �ؽ�Ʈâ�� �������� ��� 0���� ����
        var horizon = gameManager.isAction ? 0 : Input.GetAxisRaw("Horizontal");  // ����
        var vertical = gameManager.isAction ? 0 : Input.GetAxisRaw("Vertical");   // ����

        if (horizon != 0f || vertical != 0f)
        {
            m_PlayerMoving = true;
            m_LastMove = new Vector2(horizon, vertical);
        }

        // ĳ���Ͱ� ���� �ִ� ������ m_DirVec�� ����
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

        // �ִϸ��̼�
        m_Anim.SetFloat("MoveX", horizon);
        m_Anim.SetFloat("MoveY", vertical);
        m_Anim.SetFloat("LastMoveX", m_LastMove.x);
        m_Anim.SetFloat("LastMoveY", m_LastMove.y);
        m_Anim.SetBool("PlayerMoving", m_PlayerMoving);

        // �÷��̾ ���� �ִ� �������� ray �׸��� ray�� ���� ��ü�� ���̾ "Object"�� ���� �����ؼ� m_ScanObj�� ����
        Debug.DrawRay(transform.position, m_DirVec * 0.2f, new Color(0, 1, 0));
        RaycastHit2D ray = Physics2D.Raycast(transform.position, m_DirVec, 0.7f, LayerMask.GetMask("Object"));

        if (ray.collider != null)
            m_ScanObj = ray.collider.gameObject;
        else
            m_ScanObj = null;
    }
}
