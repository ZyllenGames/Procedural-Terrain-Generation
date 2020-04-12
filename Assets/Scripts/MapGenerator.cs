using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum MapDisplayType
    {
        NOISE,
        COLOR,
        MESH
    }

    [Header("Map Info")]
    public MapDisplayType EnMapDisplayType;
    int m_ChunkSize = 241;
    public int seed;

    [Header("Noise Info")]
    public float NoiseScale;
    public int NumOctave;
    public float Lacunarity;
    [Range(0.1f, 1f)]
    public float Persistance;

    [Header("Mesh Info")]
    public float GridLength;
    public float MeshHeightMultipler;
    public AnimationCurve HeightCurve;
    [Range(0, 6)]
    public int LevelOfDetail;

    [Header("Region Info")]
    public Region[] MapRegions;

    MapDisplay m_MapDisplay;

    private void Awake()
    {
        m_MapDisplay = GetComponent<MapDisplay>();
    }

    private void Update()
    {
        float[,] heightMap = Noise.GenerateNoiseMap(m_ChunkSize, m_ChunkSize, seed, NoiseScale, NumOctave, Lacunarity, Persistance);
        Color[,] colorMap = new Color[heightMap.GetLength(0), heightMap.GetLength(1)];
        for (int y = 0; y < heightMap.GetLength(0); y++)
        {
            for (int x = 0; x < heightMap.GetLength(1); x++)
            {
                for (int i = 0; i < MapRegions.Length; i++)
                {
                    float height = heightMap[y, x];
                    Region curRegion = MapRegions[i];
                    if (height <= curRegion.MaxHeight)
                    {
                        colorMap[y, x] = curRegion.Color;
                        break;
                    }
                }
            }
        }

        if (EnMapDisplayType == MapDisplayType.NOISE)
            m_MapDisplay.DisplayTexture(TextureGenerator.GenTextureByHeightMap(heightMap));
        else if (EnMapDisplayType == MapDisplayType.COLOR)
            m_MapDisplay.DisplayTexture(TextureGenerator.GenTextureByColorMap(colorMap));
        else if (EnMapDisplayType == MapDisplayType.MESH)
            m_MapDisplay.DisplayMesh(MeshGenerator.GenerateMeshData(heightMap, GridLength, MeshHeightMultipler, HeightCurve, LevelOfDetail), TextureGenerator.GenTextureByColorMap(colorMap));

    }

    private void OnValidate()
    {
        NumOctave = NumOctave < 1 ? 1 : NumOctave;
        Lacunarity = Lacunarity < 1 ? 1 : Lacunarity;
        MeshHeightMultipler = MeshHeightMultipler < 1 ? 1 : MeshHeightMultipler;
    }
}

[System.Serializable]
public class Region
{
    public string Name;
    public float MaxHeight;
    public Color Color;
}

