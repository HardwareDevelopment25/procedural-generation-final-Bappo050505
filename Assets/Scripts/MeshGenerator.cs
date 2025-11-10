using UnityEngine;

public static class MeshGenerator
{
    public static MeshData GenerateTerrain(float[,] heightMap, float hightMulti )
    {
        int height = heightMap.GetLength( 0 );
        int width = heightMap.GetLength( 1 );

        float topLeftX = (width - 1) / -2f;
        float topLeftZ = (height - 1) / 2f;

        MeshData meshData = new MeshData(width, height);
        int vertexIndex = 0;

        for(int y = 0; y< height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                meshData.vertcies[vertexIndex] = new Vector3((topLeftX + x), (heightMap[x, y] * hightMulti), (topLeftZ - y));

                meshData.uvs[vertexIndex] = new Vector2(x / (float)width, y / (float)height);


                //create triangle faces

                if ((x < width - 1)&&(y < height - 1))//making square of 2 triangles
                {
                    meshData.AddTriangle(vertexIndex, vertexIndex + width + 1 , vertexIndex + width);
                    meshData.AddTriangle(vertexIndex + width + 1, vertexIndex, vertexIndex + 1);

                    
                }
                vertexIndex++;
            }
        }

        return meshData;
    }
}

public class MeshData
{
    public Vector3[] vertcies;
    public int[] triangles;

    int triIndex = 0;
    public Vector2[] uvs;

    public MeshData(int meshWidth, int meshHeight)
    {
        vertcies = new Vector3[meshWidth * meshHeight];
        triangles = new int[(meshWidth -1) * (meshHeight-1) * 6 ]; // dont want to fall off the grid.
        uvs = new Vector2[meshWidth * meshHeight];
    }

    public void AddTriangle(int a, int b, int c)
    {
        triangles[triIndex] = a;
        triangles[triIndex + 1] = b;
        triangles[triIndex + 2] = c;
        triIndex += 3;
    }

    public Mesh CreateMesh() // make mesh
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertcies;
        mesh.triangles = triangles; 
        mesh.uv = uvs;
        mesh.RecalculateNormals();

        return mesh;
    }
}

