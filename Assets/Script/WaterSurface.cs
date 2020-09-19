using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSurface : MonoBehaviour
{
    // Start is called before the first frame update
    public int mDivisions = 64;
    public float mSize = 500;
    public Octave[] octaves;

    Vector3[] mVerts;
    int mVertCount;

    protected Mesh mesh;

    [System.Serializable]
    public struct Octave
    {
        public Vector2 speed;
        public Vector2 scale;
        public float height;
        public bool alternate;
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateTerrain();
    }

    // Update is called once per frame
    void Update()
    {
        WaveMotion();

    }

    void WaveMotion() {
        var verts = mesh.vertices;
        float halfSize = mSize * 0.5f;
        float divisionSize = mSize / mDivisions;

        for (int x = 0; x <= mDivisions; x++) 
        {
            for (int z = 0; z <= mDivisions; z++)
            {
                var y = 0f;
                for (int o = 0; o < octaves.Length; o++)
                {
                    if (octaves[o].alternate)
                    {
                        var perl = Mathf.PerlinNoise((x * octaves[o].scale.x) / mDivisions, (z * octaves[o].scale.y) / mDivisions) * Mathf.PI * 2f;
                        y += Mathf.Cos(perl + octaves[o].speed.magnitude * Time.time) * octaves[o].height;
                    }
                    else 
                    {
                        var perl = Mathf.PerlinNoise((x * octaves[o].scale.x + Time.time * octaves[o].speed.x) / mDivisions, (z * octaves[o].scale.y + Time.time * octaves[o].speed.y) / mDivisions) - 0.5f;
                        y += perl * octaves[o].height;
                    }
                }
                verts[index(x, z)] = new Vector3(-halfSize + x * divisionSize, y, -halfSize + z * divisionSize);
            }
        }
        mesh.vertices = verts;
        mesh.RecalculateNormals();
    }

    private int index(int x, int z)
    {
        return x * (mDivisions + 1) + z;
    }

    void CreateTerrain()
    {
        mVertCount = (mDivisions + 1) * (mDivisions + 1);
        mVerts = new Vector3[mVertCount];
        Vector2[] uvs = new Vector2[mVertCount];
        Color[] colors = new Color[mVertCount];
        int[] tris = new int[mDivisions * mDivisions * 6];

        float halfSize = mSize * 0.5f;
        float divisionSize = mSize / mDivisions;

        this.mesh = new Mesh();
        

        int triOffset = 0;

        for (int i = 0; i <= mDivisions; i++)
        {
            for (int j = 0; j <= mDivisions; j++)
            {
                mVerts[i * (mDivisions + 1) + j] = new Vector3(-halfSize + j * divisionSize, 0.0f, halfSize - i * divisionSize);
                uvs[i * (mDivisions + 1) + j] = new Vector2((float)i / mDivisions, (float)j / mDivisions);

                if (i < mDivisions && j < mDivisions)
                {
                    int topLeft = i * (mDivisions + 1) + j;
                    int botLeft = (i + 1) * (mDivisions + 1) + j;
                    tris[triOffset] = topLeft;
                    tris[triOffset + 1] = topLeft + 1;
                    tris[triOffset + 2] = botLeft + 1;

                    tris[triOffset + 3] = topLeft;
                    tris[triOffset + 4] = botLeft + 1;
                    tris[triOffset + 5] = botLeft;

                    triOffset += 6;
                }
            }
        }

        for (int i = 0; i < mVertCount; i++)
        {
            colors[i] = Color.blue;
        }

        mesh.vertices = mVerts;
        mesh.uv = uvs;
        mesh.colors = colors;
        mesh.triangles = tris;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;

        MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();
        /*renderer.material.shader = this.shader;
        renderer.material.mainTexture = texture;*/
    }
}
