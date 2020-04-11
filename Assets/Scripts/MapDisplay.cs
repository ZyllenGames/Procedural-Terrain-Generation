using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer TextureRenderer;

    public void DisplayMap(float[,] noiseMap)
    {
        int mapWidth = noiseMap.GetLength(1);
        int mapHeight = noiseMap.GetLength(0);

        Texture2D texture = new Texture2D(mapWidth, mapHeight);

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                Color color = Color.Lerp(Color.black, Color.white, noiseMap[y, x]);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();

        TextureRenderer.sharedMaterial.mainTexture = texture;
    }
}
