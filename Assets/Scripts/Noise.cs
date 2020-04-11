using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int width, int height, int seed, float noiseScale, int numOctave, float lacunarity, float persistance)
    {
        float[,] noiseMap = new float[height, width];

        Random.InitState(seed);
        float xNoiseOffset = Random.Range(-1000f, 1000f);
        float yNoiseOffset = Random.Range(-1000f, 1000f);

        float minNoiseValue = float.MaxValue;
        float maxNoiseValue = float.MinValue;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float frequency = 1f;
                float amplitude = 1f;
                float noiseValue = 0;

                for (int io = 0; io < numOctave; io++)
                {
                    float xCoordinate = (float)x / width * noiseScale * frequency + xNoiseOffset;
                    float yCoordinate = (float)y / height * noiseScale * frequency + yNoiseOffset;

                    float perlinValue = Mathf.PerlinNoise(xCoordinate, yCoordinate) * 2 - 1;

                    frequency *= lacunarity;
                    amplitude *= persistance;
                    noiseValue += perlinValue * amplitude;
                }

                noiseMap[y, x] = noiseValue;

                minNoiseValue = noiseValue < minNoiseValue ? noiseValue : minNoiseValue;
                maxNoiseValue = noiseValue > maxNoiseValue ? noiseValue : maxNoiseValue;
            }
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                noiseMap[y, x] = Mathf.InverseLerp(minNoiseValue, maxNoiseValue, noiseMap[y, x]);
            }
        }
        
        return noiseMap;
    }
}
