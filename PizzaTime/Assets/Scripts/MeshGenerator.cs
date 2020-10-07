using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[ExecuteInEditMode]
public class MeshGenerator : MonoBehaviour
{

    public float scale = 27.0f;
    public int octaves = 4;
    public float persistence = 0.5f;
    public float lacunarity = 2.0f;

    Mesh mesh;
    Vector3[] vertices;
    Vector2[] uv;
    Vector4[] tangents;
    int[] triangles;
    int[] roadTriangles;

    public int xSize = 20;
    public int zSize = 20;
    public LevelControl levelControl;
    public int[,] levelArray;

    // Start is called before the first frame update
    void Start()
    {
        levelArray = levelControl.GetLevelArray();
        xSize = levelControl.arraySize * (int)levelControl.blockSize + 100;
        zSize = levelControl.arraySize * (int)levelControl.blockSize + 100;

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        CreateShape();
        FlattenRoad();

        UpdateMesh();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.F))
        {
            CreateShape();
            UpdateMesh();
        }
        */
    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        uv = new Vector2[vertices.Length];
        tangents = new Vector4[vertices.Length];

        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {

                float y = 0;
                float amplitude = 1;
                float frequency = 1;

                for (int j = 0; j < octaves; j++)
                {
                    float perlinVal = Mathf.PerlinNoise(x / scale * frequency, z / scale * frequency) * 0.5f - 0.25f;
                    y += perlinVal * amplitude;

                    amplitude *= persistence;
                    frequency *= lacunarity;
                }

                if (y > maxNoiseHeight)
                {
                    maxNoiseHeight = y;
                }
                else if (y < minNoiseHeight)
                {
                    minNoiseHeight = y;
                }

                int arrayY = (int)((float)(z - 50) / levelControl.blockSize);
                int arrayX = (int)((float)(x - 50) / levelControl.blockSize);

                if (arrayX < levelControl.arraySize && arrayY < levelControl.arraySize && arrayX >= 0 && arrayY >= 0)
                {
                    if (levelArray[arrayX, arrayY] == 1)
                    {
                        //((float)(z - 25) / levelControl.blockSize) / (float)arrayY)

                        //float yMult = Mathf.Abs((float)(z - 25) / levelControl.blockSize - (float)arrayY - 0.5f);

                        //float xMult = Mathf.Abs((float)(x - 25) / levelControl.blockSize - (float)arrayX - 0.5f);

                        y *= 0.1f;
                        //y -= 0.2f;
                    }
                    y *= 0.1f;
                }
                
                vertices[i] = new Vector3(x, y, z);

                uv[i] = new Vector2((float)x / xSize, (float)z / zSize);
                tangents[i] = tangent;
                i++;
            }
        }

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, vertices[i].y);
            vertices[i].y *= 8f;
        }

        /*
        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        Vector2[] uv = new Vector2[vertices.Length];
        Vector4[] tangents = new Vector4[vertices.Length];
        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x, y);
                uv[i] = new Vector2((float)x / xSize, (float)y / ySize);
                tangents[i] = tangent;
            }
        }
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.tangents = tangents;
        */

        //int roadTriangleCount = roadVerticesCount*6;
        //Debug.Log(roadTriangleCount);
        roadTriangles = new int[(xSize * zSize * 6)];

        //Debug.Log("verticies: " + vertices.Length);
        //Debug.Log(xSize * zSize);
        triangles = new int[(xSize * zSize * 6)];

        int vert = 0;
        int tris = 0;
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                int arrayY = (int)((float)(z - 50) / levelControl.blockSize);
                int arrayX = (int)((float)(x - 50) / levelControl.blockSize);
                
                if (arrayX < levelControl.arraySize && arrayY < levelControl.arraySize && arrayX >= 0 && arrayY >= 0)
                {
                    if (levelArray[arrayX, arrayY] == 1)
                    {
                        
                        roadTriangles[0 + tris] = vert + 0;
                        roadTriangles[1 + tris] = vert + xSize + 1;
                        roadTriangles[2 + tris] = vert + 1;
                        roadTriangles[3 + tris] = vert + 1;
                        roadTriangles[4 + tris] = vert + xSize + 1;
                        roadTriangles[5 + tris] = vert + xSize + 2;

                        vert++;
                        tris += 6;
                        continue;
                    }
                }

                triangles[0 + tris] = vert + 0;
                triangles[1 + tris] = vert + xSize + 1;
                triangles[2 + tris] = vert + 1;
                triangles[3 + tris] = vert + 1;
                triangles[4 + tris] = vert + xSize + 1;
                triangles[5 + tris] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;

        }
        /*
        for(int i = 0; i < vertices.Length; i++)
        {
            vertices[i].x += transform.position.x;
            vertices[i].y += transform.position.y;
            vertices[i].z += transform.position.z;
        }
        */
    }

    void FlattenRoad() { 
    
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.subMeshCount = 2;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.tangents = tangents;
        //mesh.triangles = triangles;
        mesh.SetTriangles(triangles, 0);
        mesh.SetTriangles(roadTriangles, 1);
        //mesh.SetTriangles(triDex[1], 1);
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;

    }
}