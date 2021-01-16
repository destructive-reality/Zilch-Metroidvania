using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public ParallaxCamera parallaxCamera;
    List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();

    void Start()
    {
        if (parallaxCamera == null)
            parallaxCamera = Camera.main.GetComponent<ParallaxCamera>();
        if (parallaxCamera != null)
            parallaxCamera.onCameraTranslate += Move;
        InitLayers();
    }

    void InitLayers()
    {
        parallaxLayers.Clear();
        SetLayers(transform);
    }

    void SetLayers(Transform _transform)
    {
        for (int i = 0; i < _transform.childCount; i++)
        {
            GetRecursive(i, _transform);
        }
    }

    void GetRecursive(int _count, Transform _transform)
    {
        ParallaxLayer layer;
        Transform child = _transform.GetChild(_count);

            if((child != null) && (child.GetComponent<ParallaxLayer>() != null)) 
            {
                layer = child.GetComponent<ParallaxLayer>();
                if(layer != null)
                {
                    layer.name = "Layer-" + _count;
                    parallaxLayers.Add(layer);
                }
            } else if ((child != null)) 
            {
                SetLayers(child);
            }
    }

    void Move(float delta)
    {
        foreach (ParallaxLayer layer in parallaxLayers)
        {
            layer.Move(delta);
        }
    }
}