using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    public void DefensePowerStart(GameObject player)
    {
        Debug.Log("Increase Player Health");
        player.GetComponent<PlayerHealth>().maxHealth += 10;
    }
}
