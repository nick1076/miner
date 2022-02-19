using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    void Start()
    {
        Entity player = new GameObject().AddComponent<Entity>();
        Camera cam = new GameObject().AddComponent<Camera>();

        cam.orthographic = true;

        player.transform.parent = cam.gameObject.transform;
        player.transform.position = new Vector3(0, 0, 1);

        cam.transform.position = new Vector3(0, 0, 0);
        cam.orthographicSize = 2.5f;

        player.InitializeAs("Player", true);
    }
}
