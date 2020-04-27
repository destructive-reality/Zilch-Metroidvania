using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelLoader : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        LevelConnector levelConnector = other.gameObject.GetComponent<LevelConnector>();
        if (levelConnector){
            LevelLoader.Instance.LoadLevelWithID(levelConnector.sceneToLoad);
        }
    }
}
