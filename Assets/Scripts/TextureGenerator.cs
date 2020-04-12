using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureGenerator 
{
    public static Texture2D GenTextureByHeightMap(float[,] heightMap)
    {
        int mapWidth = heightMap.GetLength(1);
        int mapHeight = heightMap.GetLength(0);
        Color[,] colorMap = new Color[mapHeight, mapWidth];

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                colorMap[y, x] = Color.Lerp(Color.black, Color.white, heightMap[y, x]);
            }
        }

        return GenTextureByColorMap(colorMap);
    }

    public static Texture2D GenTextureByColorMap(Color[,] colorMap)
    {
        int mapWidth = colorMap.GetLength(1);
        int mapHeight = colorMap.GetLength(0);

        Texture2D texture = new Texture2D(mapWidth, mapHeight);

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                Color color = colorMap[y, x];
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        return texture;
    }
}
