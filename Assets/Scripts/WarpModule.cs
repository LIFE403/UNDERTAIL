using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpModule : MonoBehaviour
{
    public Transform TeleportPosition;
    public MapController CurrentController;
    public MapController NextController;
    public AudioClip BackGroundMusic;
    public bool isLoop;

    void Start()
    {
        NextController.audioSource.clip = BackGroundMusic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("TileMaps"))
            return;

        collision.transform.position = TeleportPosition.position;

        if (isLoop == false)
        {
            CurrentController.audioSource.Stop();
            NextController.audioSource.Play();
        }

        CurrentController.enabled = false;
        NextController.enabled = true;
    }
}
