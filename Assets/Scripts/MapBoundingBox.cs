using System.Collections;
using UnityEngine;

public class MapBoundingBox : MonoBehaviour
{
    [SerializeField] private Transform m_BottomLeft;
    [SerializeField] private Transform m_TopRight;

    public Rect GetBoundingBox
    {
        get
        {
            if (m_TopRight == null || m_BottomLeft == null)
                return Rect.zero;

            float width = m_TopRight.position.x - m_BottomLeft.position.x;
            float height = m_TopRight.position.y - m_BottomLeft.position.y;

            return new Rect(
                m_BottomLeft.position.x,
                m_BottomLeft.position.y,
                width, height
                );
        }
    }

    private void OnDrawGizmos()
    {
        var rect = GetBoundingBox;
        if (rect == Rect.zero) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(rect.center, rect.size);
    }
}