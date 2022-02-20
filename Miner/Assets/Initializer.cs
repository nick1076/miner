using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    void Start()
    {
        Entity player = new GameObject().AddComponent<Entity>();
        Entity pot = new GameObject().AddComponent<Entity>();
        Entity itemRed = new GameObject().AddComponent<Entity>();
        Camera cam = new GameObject().AddComponent<Camera>();

        cam.orthographic = true;

        cam.transform.parent = player.gameObject.transform;
        cam.backgroundColor = new Color(.1f, .1f, .1f);
        player.transform.position = new Vector3(0, 0, 1);
        pot.transform.position = new Vector3(0, 1, 1);
        itemRed.transform.position = new Vector3(0, -1, 1);

        cam.transform.position = new Vector3(0, 0, 0);
        cam.orthographicSize = 2.5f;

        player.InitializeAs("Player", true);
        pot.InitializeAs("Pot", false);
        itemRed.InitializeAs("Item_Red", false);
    }
}
