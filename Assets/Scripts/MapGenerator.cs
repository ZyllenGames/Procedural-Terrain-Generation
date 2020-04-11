using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int Width;
    public int Height;
    public int seed;

    public float NoiseScale;
    public int NumOctave;
    public float Lacunarity;
    [Range(0.1f, 1f)]
    public float Persistance;

    MapDisplay m_MapDisplay;

    private void Awake()
    {
        m_MapDisplay = GetComponent<MapDisplay>();
    }

    private void Update()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(Width, Height, seed, NoiseScale, NumOctave, Lacunarity, Persistance);

        m_MapDisplay.DisplayMap(noiseMap);
    }

    private void OnValidate()
    {
        Width = Width < 1 ? 1 : Width;
        Height = Height < 1 ? 1 : Height;
        NumOctave = NumOctave < 1 ? 1 : NumOctave;
        Lacunarity = Lacunarity < 1 ? 1 : Lacunarity;
    }
}
