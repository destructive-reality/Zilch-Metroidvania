using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LevelConnector : MonoBehaviour
{
    public int sceneToLoad;

    private void Awake()
    {
        //Safety valve to always set the collider to trigger
        GetComponent<Collider2D>().isTrigger = true;
    }
}
