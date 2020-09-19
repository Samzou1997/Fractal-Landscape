using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondSquareTerrain : MonoBehaviour
{
    public int mDivisions;
    public float mSize;
    public float mHeight;

    public Shader shader;
    public Texture texture;

    private float height;

    Vector3[] mVerts;
    int mVertCount;
    // Start is called before the first frame update
    void Start()
    {
        CreateTerrain();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)) {
            CreateTerrain();
        }
    }

    void CreateTerrain() {
        height = mHeight;
        mVertCount = (mDivisions + 1) * (mDivisions + 1);
        mVerts = new Vector3[mVertCount];
        Vector2[] uvs = new Vector2[mVertCount];
        Color[] colors = new Color[mVertCount];
        int[] tris = new int[mDivisions * mDivisions * 6];

        float halfSize = mSize * 0.5f;
        float divisionSize = mSize/mDivisions;

        Mesh mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        GetComponent<MeshFilter>().mesh = mesh;

        int triOffset = 0;

        for(int i = 0; i <= mDivisions; i++) {
            for(int j = 0; j <= mDivisions; j++) {
                mVerts[i * (mDivisions + 1) + j] = new Vector3(-halfSize + j * divisionSize, 0.0f, halfSize - i * divisionSize);
                uvs[i * (mDivisions + 1) + j] = new Vector2((float)i/mDivisions, (float)j/mDivisions);

                if(i < mDivisions && j < mDivisions) {
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

        mVerts[0].y = Random.Range((float)(-height * 0.5), height);
        mVerts[mDivisions].y = Random.Range((float)(-height * 0.5), height);
        mVerts[mVerts.Length - 1].y = Random.Range((float)(-height * 0.5), height);
        mVerts[mVerts.Length - 1 - mDivisions].y = Random.Range((float)(-height * 0.8), height);

        int iterations = (int)Mathf.Log(mDivisions, 2);
        int numSquares = 1;
        int squareSize = mDivisions;

        for (int i = 0; i < iterations; i++) {
            int row = 0;
            for (int j = 0; j < numSquares; j++) {
                int col = 0;
                for (int k = 0; k < numSquares; k++) {
                    DiamondSquare(row, col, squareSize, height);
                    col += squareSize;
                }
                row += squareSize;
            }
            numSquares *= 2;
            squareSize /= 2;
            height *= 0.5f;
        }

        for (int i = 0; i < mVertCount; i++) {
            // height with larger than 18
            // Snow
            if (mVerts[i].y > mHeight * 0.9) {
                colors[i] = new Color32(255, 255, 255, 255);
            }
            // height with larger than 15 and lower than 18
            // Rocks
            if (mVerts[i].y > mHeight * 0.55 & mVerts[i].y <= mHeight * 0.9)
            {
                colors[i] = new Color32(105, 105, 105, 255);
            }
            // height with larger than 0 and lower than 15
            // Forest
            if (mVerts[i].y > mHeight * 0.05 & mVerts[i].y <= mHeight * 0.55)
            {
                colors[i] = new Color32(85, 107, 47, 255);
            }
            // height with lower than 0
            // Beach
            if (mVerts[i].y <= mHeight * 0.05)
            {
                colors[i] = new Color32(244, 164, 96, 255);
            }
        }

        mesh.vertices = mVerts;
        mesh.uv = uvs;
        mesh.colors = colors;
        mesh.triangles = tris;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();
        /*renderer.material.shader = this.shader;*/
        /*renderer.material.mainTexture = texture;*/

        MeshCollider collider = this.gameObject.GetComponent<MeshCollider>();
        collider.sharedMesh = mesh;
    }

    void DiamondSquare(int row, int col, int size, float offset) {

        int halfSize = (int)(size * 0.5f);
        int topLeft = row * (mDivisions + 1) + col;
        int botLeft = (row + size) * (mDivisions + 1) + col;

        int mid = (int)(row + halfSize) * (mDivisions + 1) + (int)(col + halfSize);
        mVerts[mid].y = (mVerts[topLeft].y + mVerts[topLeft + size].y + mVerts[botLeft].y + mVerts[botLeft + size].y) * 0.25f + Random.Range(-offset, offset);

        mVerts[topLeft + halfSize].y = (mVerts[topLeft].y + mVerts[topLeft + size].y + mVerts[mid].y) / 3 + Random.Range(-offset, offset);
        mVerts[mid - halfSize].y = (mVerts[topLeft].y + mVerts[botLeft].y + mVerts[mid].y) / 3 + Random.Range(-offset, offset);
        mVerts[mid + halfSize].y = (mVerts[topLeft + size].y + mVerts[botLeft + size].y + mVerts[mid].y) / 3 + Random.Range(-offset, offset);
        mVerts[botLeft + halfSize].y = (mVerts[botLeft].y + mVerts[botLeft + size].y + mVerts[mid].y) / 3 + Random.Range(-offset, offset);
    }
}
