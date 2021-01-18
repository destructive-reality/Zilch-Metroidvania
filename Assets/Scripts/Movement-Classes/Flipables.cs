﻿using UnityEngine;

public enum FLIP_DIRECTION
{
  LEFT = 0, RIGHT = 1, OTHER = 2
}

public class Flipables : MonoBehaviour
{
  public bool isFacingRight = true;

  public void flip(FLIP_DIRECTION _direction = FLIP_DIRECTION.OTHER)
  {
    Vector3 localScale = transform.localScale;
    if (_direction == FLIP_DIRECTION.OTHER)
    {
      isFacingRight = !isFacingRight;
      //TODO kann man das nicht ohne zwischenspeichern direkt flippen?
      localScale.x *= -1;
    }

    else if (_direction == FLIP_DIRECTION.RIGHT)
    {
      isFacingRight = true;
      if (localScale.x < 0)
      {
        localScale.x *= -1;
      }
    }

    else if (_direction == FLIP_DIRECTION.LEFT)
    {
      isFacingRight = false;
      if (localScale.x > 0)
      {
        localScale.x *= -1;
      }
    }

    transform.localScale = localScale;
  }
}