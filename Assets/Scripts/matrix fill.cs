using JetBrains.Annotations;
using System;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;



public class matrixfill : MonoBehaviour
{

    System.Random r;
    public int gridSize = 10;
    public int[,] grid;
    public float percent = 0.4f;
    public Texture2D texture;
    GameObject noise;


    void Start()
    {
        grid = new int[gridSize, gridSize];
        texture = new Texture2D(gridSize, gridSize);

       r = new System.Random();

        noise = GameObject.CreatePrimitive(PrimitiveType.Plane);

        PopulateGrid(grid);
        DisplayGrid(grid, texture);

        noise.GetComponent<MeshRenderer>().material.mainTexture = texture;

    }

    public void PopulateGrid(int[,] grid)
    {
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                if (r.NextDouble() < percent)
                {
                    grid[x, y] = 1;
                }
                else if (r.NextDouble() > percent)
                {
                    grid[x, y] = 0;
                }

                Debug.Log(grid[x, y]);
            }
        }
    }

    public void DisplayGrid(int[,] grid , Texture2D texture)
    {
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {

                double randPercent = r.NextDouble();

                if (grid[x,y] == 1)
                {
                    texture.SetPixel(x, y, Color.black);
                }
                else if (grid[x,y] ==0)
                {
                    texture.SetPixel(x, y, Color.white);
                }
            }

        }

        texture.Apply();
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;

    }

    public int GetNeighbours(int gridx, int gridy)
    {
        int totalNeighbours = 0;

        for (int NeighbourX = gridx - 1; NeighbourX <= gridx + 1; NeighbourX++)
        {
            for (int NeighbourY = gridy - 1; NeighbourY <= gridy + 1; NeighbourY++)
            { 
                if (isMapInRange(NeighbourX, NeighbourY))
                {
                    

                    if(gridx == NeighbourX && gridy == NeighbourY) 
                    {
                        continue;
                    }
                    totalNeighbours++;
                }
            }
        }

        return totalNeighbours;
    }

    bool isMapInRange(int x, int y) => x>=0 && x<=gridSize && y>=0 && y<=gridSize;
    
        
    
}







