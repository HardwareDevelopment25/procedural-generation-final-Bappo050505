using System.Collections.Generic;
//using System.Drawing;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    System.Random m_rand;
    public int size = 10, seed = 0;
    public GameObject wall, floor;
    Texture2D texture;

    private bool[,] maze;

    [SerializeField] bool set2D = true;
    [SerializeField] bool set3D = true;
    

    private void Start()
    {
        
        m_rand = new System.Random(seed);

        texture = new Texture2D(size, size);

        maze = new bool[size, size]; // creates a maze of false values

        GenerateMaze();

        if (set2D)
        {
            
            GetComponent<MeshRenderer>().material.mainTexture = proGenTools.Draw2D(maze);
        }
        if (set3D)
        {
            Draw3D();
            GetComponent<MeshRenderer>().material.mainTexture = texture;
        }

    }

    public void imageOfMaze()
    {
        set3D = false;
        Start();
    }

    private void Draw3D()
    {
        for (int x = 0; x < size; x++)
        {
            for(int y = 0; y <size; y++)
            {
                if (maze[x, y])
                {
                    GameObject.Instantiate(floor, new Vector3(x, 0, y), Quaternion.identity);
                }
                else 
                {
                    GameObject.Instantiate(wall, new Vector3(x, 0, y), Quaternion.identity); ;
                }
            }
        }
    }


    private void GenerateMaze()
    {
        //need a stack to store all unvisited locations.

        Stack<Vector2Int> stack = new Stack<Vector2Int>();

        //start maze generation from top left.

        Vector2Int current = new Vector2Int(0, 0);

        //marks the starting cell as true

        maze[current.x, current.y] = true;

        //add start position to stack.

        stack.Push(current);

        while(stack.Count > 0)// while stack is not empty
        {
            current = stack.Pop(); //takes the top position to be checked.

            List<Vector2Int> neighbours = new List<Vector2Int>();

            if(current.x > 1 && !maze[current.x - 2 , current.y] ) // is left available
            {
                neighbours.Add(new Vector2Int(current.x -2, current.y));
            }

            if(current.x < size - 2 && !maze[current.x + 2, current.y]) // is right available
            {
                neighbours.Add(new Vector2Int(current.x + 2, current.y));
            }

            if(current.y > 1 && !maze[current.x, current.y - 2]) // is up available
            {
                neighbours.Add(new Vector2Int(current.x, current.y - 2));
            }

            if(current.y < size - 2 && !maze[current.x, current.y + 2]) // is down available
            {
                neighbours.Add(new Vector2Int(current.x, current.y + 2));
            }

            //choose a neighbour , and add it to the stack
            if(neighbours.Count > 0) //did we add anything to the list
            {
                stack.Push(current);//puts back what we popped of the stack

                //choose a neighbour and make sure to check amount of neighbours as may differ.
                Vector2Int chosenOne = neighbours[m_rand.Next(0,neighbours.Count)];

                if (chosenOne.x == current.x)
                {
                    maze[chosenOne.x, chosenOne.y + 1] = true; // mark the vertical gap as true.

                }
                else
                {
                    maze[chosenOne.x + 1, chosenOne.y] = true;// bridge the gap horizontally
                }
                maze[chosenOne.x, chosenOne.y] = true;

                stack.Push(chosenOne);
            }
        }
    }
}
