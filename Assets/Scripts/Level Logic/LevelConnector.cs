using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DIRECTION
{
    RightToLeft = 0,
    LeftToRight = 1,
    TopToBottom = 2,
    BottomToTop = 3
}

[RequireComponent(typeof(Collider2D))]
public class LevelConnector : MonoBehaviour
{
    public int sceneToLoad;
    public int doorId;
    public DIRECTION enterDirection;

    private void Awake()
    {
        //Safety valve to always set the collider to trigger
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameMaster.Instance.lastLevelDoorId = doorId;
            LevelLoader.Instance.LoadLevelWithID(sceneToLoad);
        }
    }

    public static Vector2 SpawnOffset(DIRECTION direction)
    {
        float baseOffset = 2.5f;

        switch (direction)
        {
            case DIRECTION.RightToLeft:
                return new Vector2(-baseOffset, 0);
            case DIRECTION.LeftToRight:
                return new Vector2(baseOffset, 0);
            case DIRECTION.TopToBottom:
                return new Vector2(0, -baseOffset);
            case DIRECTION.BottomToTop:
                return new Vector2(0, baseOffset);
            default:
                throw new ArgumentException("Not a valid direction value");
        }
    }
}
