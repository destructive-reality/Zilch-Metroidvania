using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LevelConnector : MonoBehaviour
{
    public int sceneToLoad;
    public int doorId;

    private void Awake()
    {
        //Safety valve to always set the collider to trigger
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LevelLoader.Instance.LoadLevelWithID(sceneToLoad);
        }
    }
}
