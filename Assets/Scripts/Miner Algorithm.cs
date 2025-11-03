using System.Diagnostics.Contracts;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class MinerAlgorithm : MonoBehaviour
{
    [SerializeField] Vector3 miner;
    [SerializeField] int size = 32;
    [SerializeField] GameObject floor;

    public bool[,] gamegrid;
    public int seed = 0;
    public int movesLeft = 10;

    private Vector3 pathplacer;
    private System.Random random;
    private int randnumber = 0;
    private string up, down, left, right , playerMoveDirection;
    private bool canmove;
    private string[] moveDirection;
    private int sizemin;

  

    Quaternion quaternion = Quaternion.identity;




    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {

      
        random = new System.Random(seed);
        sizemin = size - size;
        gamegrid = new bool[size, size];


    }
    void Start()
    {

        moveDirection = new string[4] { "up", "down", "left", "right" };

        canmove = true;

        Vector3 minerpos = miner;
       
        do
        {
            randnumber = random.Next(0, 4);

            playerMoveDirection = moveDirection[randnumber];
            if (playerMoveDirection == "up" && canmove)
            {
                minerpos.x += 1;
                miner = minerpos;
                movesLeft -= 1;
            }
            if (playerMoveDirection == "down" && canmove)
            {
                minerpos.x -= 1;
                miner = minerpos;
                movesLeft -= 1;
            }
            if (playerMoveDirection == "left" && canmove)
            {
                minerpos.z += 1;
                miner = minerpos;
                movesLeft -= 1;
            }
            if (playerMoveDirection == "right" && canmove)
            {
                minerpos.z -= 1;
                miner = minerpos;
                movesLeft -= 1;
            }
            else canmove = false;

            if (minerpos.x < size || minerpos.z < size || minerpos.x > 0 || minerpos.z > 0)
            {
                canmove = true;
                gamegrid[(int)minerpos.x, (int)minerpos.z] = true;

            }
            else continue;

            Instantiate(floor, miner, quaternion);

          
        }
        while(movesLeft > 0);

        for (int x = 0; x< size; x++)
        {
            for(int y =0; y <size; y++)
            {
                if (gamegrid[x, y])
                {
                    pathplacer = new Vector3(miner.x, 0, miner.z);
                    GameObject.Instantiate(floor, new Vector3(x, 0, y), Quaternion.identity);
                }
                else
                {
                    pathplacer = new Vector3(miner.x, 0, miner.z);
                    GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = new Vector3(x,0,y);
                }
            }
        }
        

    }

    

    
}
