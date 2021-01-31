using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelateScreen : MonoBehaviour
{
    public RenderTexture renderTexture;

    public bool startPixelate;
    void Start()
    {
        startPixelate = true;
    }
    void Update()
    {
        if (startPixelate)
        {
            if (renderTexture.width > 0)
            {
                float widthPrev = renderTexture.width;
                widthPrev = widthPrev / Time.deltaTime * 10;
                int newWidth = (int)widthPrev;
                renderTexture.width = newWidth;
                float heightPrev = renderTexture.width;
                heightPrev = heightPrev / Time.deltaTime * 10;
                int newHeight = (int)widthPrev;
                renderTexture.width = newHeight;
                startPixelate = false;
            }
        }
    }

}
