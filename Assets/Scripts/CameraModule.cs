using System.Collections;
using UnityEngine;

public class CameraModule : MonoBehaviour
{
    [SerializeField] private Transform m_Transform;
    [SerializeField] private Camera m_MainCamera;

    // 카메라 렉트 만들기
    public Rect GetCameraRect
    {
        get
        {
            if (m_MainCamera == null)
                m_MainCamera = Camera.main;

            // Bottom-left  -> 0, 0
            // Top-right    -> 1, 1
            Vector3 BL = m_MainCamera.ViewportToWorldPoint(new Vector3(0, 0));
            // Vector3 TL = m_MainCamera.ViewportToWorldPoint(new Vector3(0, 1));
            Vector3 TR = m_MainCamera.ViewportToWorldPoint(new Vector3(1, 1));
            // Vector3 BR = m_MainCamera.ViewportToWorldPoint(new Vector3(1, 0));

            float width = TR.x - BL.x;
            float height = TR.y - BL.y;

            return new Rect(
                BL.x, BL.y,
                width, height
                );
        }
    }

    // 카메라가 플레이어 따라가도록 하기
    public void MoveCamera(Rect bound)
    {
        transform.position = m_Transform.position;
        if (bound == Rect.zero) return;

        var rect = GetCameraRect;
        if (rect.x < bound.x)
        {
            transform.position = new Vector3(
                bound.x + rect.width * 0.5f,
                transform.position.y,
                0);
        }
        else if (rect.xMax > bound.xMax)
        {
            transform.position = new Vector3(
                bound.xMax - rect.width * 0.5f,
                transform.position.y,
                0);
        }

        if (rect.yMax > bound.yMax)
        {
            transform.position = new Vector3(
                transform.position.x,
                bound.yMax - rect.height * 0.5f,
                0);
        }
        else if (rect.y < bound.y)
        {
            transform.position = new Vector3(
                transform.position.x,
                bound.y + rect.height * 0.5f,
                0);
        }
    }

    // 카메라 렉트 화면에 그리기
    private void OnDrawGizmos()
    {
        var rect = GetCameraRect;

        Gizmos.DrawWireCube(rect.center, rect.size);
    }
}