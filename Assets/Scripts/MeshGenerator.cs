using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator
{
    public static MeshData GenerateMeshData(float[,] heightMap, float gridLength, float meshHeightMultipler, AnimationCurve heightCurve, int levelOfDetail)
    {
        //LOD
        int meshSampleIncrement = levelOfDetail == 0 ? 1 : levelOfDetail * 2;

        int width = heightMap.GetLength(1);
        int height = heightMap.GetLength(0);
        float mapHalfWidth = (width - 1) * gridLength / 2f;
        float mapHalfHeight = (height - 1) * gridLength / 2f;

        int gridWidth = (width - 1) / meshSampleIncrement;
        int gridHeight = (height - 1) / meshSampleIncrement;

        MeshData meshData = new MeshData(gridWidth, gridHeight);

        for (int y = 0; y < height; y+= meshSampleIncrement)
        {
            for (int x = 0; x < width; x+= meshSampleIncrement)
            {
                //create vertices
                Vector3 vertice = new Vector3(x * gridLength - mapHalfWidth, heightCurve.Evaluate(heightMap[y, x]) * meshHeightMultipler, y * gridLength - mapHalfHeight);
                meshData.AddVertice(vertice);

                //create uvs
                Vector2 uv = new Vector2((float)x / width, (float)y/ height);
                meshData.AddUV(uv);
            }
        }

        //create triangles
        for (int row = 0; row < gridHeight; row++)
        {
            for (int col = 0; col < gridWidth; col++)
            {
                int iVertLB = row * (gridWidth + 1) + col;
                int iVertRB = iVertLB + 1;
                int iVertLT = iVertLB + gridWidth + 1;
                int iVertRT = iVertLT + 1;
                meshData.AddTriangle(iVertLB, iVertLT, iVertRB);
                meshData.AddTriangle(iVertLT, iVertRT, iVertRB);
            }
        }

        return meshData;
    }
}

public class MeshData
{
    Vector3[] m_Vertices;
    int[] m_Triangles;
    Vector2[] m_UVs;

    int m_iVertices = 0;
    int m_iTriangle = 0;
    int m_iUV = 0;

    public MeshData(int gridWidth, int gridHeight)
    {
        m_Vertices = new Vector3[(gridWidth + 1) * (gridHeight + 1)];
        m_Triangles = new int[gridWidth * gridHeight * 2 * 3];
        m_UVs =new Vector2[m_Vertices.Length];
    }

    public void AddVertice(Vector3 vertice)
    {
        m_Vertices[m_iVertices] = vertice;
        m_iVertices++;
    }

    public void AddTriangle(int a, int b, int c)
    {
        m_Triangles[m_iTriangle * 3] = a;
        m_Triangles[m_iTriangle * 3 + 1] = b;
        m_Triangles[m_iTriangle * 3 + 2] = c;
        m_iTriangle++;
    }

    public void AddUV(Vector2 uv)
    {
        m_UVs[m_iUV] = uv;
        m_iUV++;
    }

    public Mesh GenerateMesh()
    {
        Mesh newMesh = new Mesh();
        newMesh.vertices = m_Vertices;
        newMesh.triangles = m_Triangles;
        newMesh.uv = m_UVs;
        newMesh.RecalculateNormals();
        return newMesh;
    }
}