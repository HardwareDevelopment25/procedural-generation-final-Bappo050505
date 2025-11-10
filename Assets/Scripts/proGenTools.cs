using UnityEngine;

public static class proGenTools
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static Texture2D Draw2D(bool[,] maze)
    {

        Texture2D texture = new Texture2D(maze.GetLength(0), maze.GetLength(1));

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                if (maze[x, y])
                {
                    texture.SetPixel(x, y, Color.white);
                }
                else
                {
                    texture.SetPixel(x, y, Color.black);
                }
            }
        }

        texture.Apply();
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;

        return texture;

    }

    public static Mesh Triangle(float size)
    {
        Mesh triangle = new Mesh();

        Vector3[] vertecies = new Vector3[]
        {
            new Vector3 (0 ,0, 0),
            new Vector3 (size, 0, 0),
            new Vector3 (size/2 ,size, 0)
        };

        Vector2[] uvs = new Vector2[]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0.5f, 1)
        };

        int[] triangles = new int[]
        { 
            0,1,2
        };

        triangle.vertices = vertecies;
        triangle.uv = uvs;
        triangle.triangles = triangles;


        return triangle;
    }

    public static Mesh Square(float size)
    {
        Mesh square = new Mesh();

        Vector3[] vertecies = new Vector3[]
        {
            new Vector3 (0 ,0, 0),//0
            new Vector3 (size, 0, 0),//1
            new Vector3 (size ,size, 0),//2
            new Vector3 (0 , size , 0),//3

            new Vector3 (0, 0, size),//4
            new Vector3 (size, 0, size),//5
            new Vector3 (size, size, size),//6
            new Vector3 (0, size, size)//7
        };

        Vector2[] uvs = new Vector2[]
        {
        //front
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0,1),
        //back
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0,1)
        };

        int[] triangles = new int[]
        {//front
            0,2,1,
            0,3,2,
        //bottom
            1,4,0,
            5,4,1,
        //back
            7,4,5,
            6,7,5,
        //top
            2,3,7,
            2,7,6,
        //left    
            0,4,3,
            4,7,3,
        //right
            1,2,5,
            5,2,6
        };

        square.vertices = vertecies;
        square.uv = uvs;
        square.triangles = triangles;
        square.RecalculateNormals();


        return square;
    }
}
