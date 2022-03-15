using System.Collections;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public MapBoundingBox BoundingBox;
    public WarpModule WarpModule;
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void LateUpdate()
    {
        var camera = Camera.main.GetComponent<CameraModule>();

        camera.MoveCamera(
            BoundingBox != null ?
            BoundingBox.GetBoundingBox :
            Rect.zero);
    }
}