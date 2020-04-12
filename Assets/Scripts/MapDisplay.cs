using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer TextureRenderer;

    public MeshFilter MeshFilter;
    public Renderer MeshRenderer;

    public void DisplayTexture(Texture2D texture)
    {
        TextureRenderer.sharedMaterial.mainTexture = texture;
    }

    public void DisplayMesh(MeshData meshData, Texture2D texture)
    {
        MeshFilter.mesh = meshData.GenerateMesh();
        MeshRenderer.material.mainTexture = texture;
    }
}
