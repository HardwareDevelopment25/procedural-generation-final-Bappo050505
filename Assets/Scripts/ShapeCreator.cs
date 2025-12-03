using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static TextureLogicalAnd;

public class ShapeCreator : MonoBehaviour
{
    [System.Serializable]
    public struct TerrainHeights
    {
        public string name;
        public float height;
        public Color color;
    }

    [SerializeField] private RandomDistrbution RD;
    
    //public int levelOfDetail = 1;

    public int sizeOfGrid = 64;
   
    public int seed = 0;
    public float scale = 10f;
    public int octave = 5;
    public float lacunarity = 2;
    public float persistance = 1;
    public Vector2 offset;
    public AnimationCurve AC;
    public int levelOfDetail;
    public int heightMult;
    

    public Texture2D newTexture;
    float currentHeight;
    public TerrainHeights[] Region;

    MeshFilter MF;
    MeshRenderer MR;
    Material material;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        MF = this.AddComponent<MeshFilter>();
        MR = this.AddComponent<MeshRenderer>();



        /*newTexture = new Texture2D(sizeOfGrid, sizeOfGrid);

        float[,] noisemap = PerlinNoiseGenerator.GenerateNoiseMap(sizeOfGrid, sizeOfGrid, octave, scale, lacunarity, persistance, seed, offset);
        float[,] FallOffMap = PerlinNoiseGenerator.GenerateFalloffMap(sizeOfGrid, AC);
        float[,] CombinedMap = new float[sizeOfGrid,sizeOfGrid];


        for(int i = 0; i < noisemap.GetLength(0); i++)
        {
            for(int j = 0; j < noisemap.GetLength(1); j++)
            {
                CombinedMap[i,j] = noisemap[i,j] - FallOffMap[i,j];
            }
        }

        MeshData MD = MeshGenerator.GenerateTerrain(CombinedMap, heightMult, levelOfDetail);
        MF.mesh = MD.CreateMesh();

        MR.material = material;





        //then texture

        ColourTheMap(CombinedMap,newTexture);
        material.mainTexture = newTexture;*/

        material = new Material(Shader.Find("Unlit/Texture"));

        RD = GetComponent<RandomDistrbution>();

        GenerateMap();

    }
    public void GenerateMap()
    {

        float[,] noisemap = null;
        float[,] FallOffMap = null;
        float[,] CombinedMap = null;




        newTexture = new Texture2D(sizeOfGrid, sizeOfGrid);

        noisemap = PerlinNoiseGenerator.GenerateNoiseMap(sizeOfGrid, sizeOfGrid, octave, scale, lacunarity, persistance, seed, offset);
        FallOffMap = PerlinNoiseGenerator.GenerateFalloffMap(sizeOfGrid, AC);
        CombinedMap = new float[sizeOfGrid, sizeOfGrid];


        for (int i = 0; i < noisemap.GetLength(0); i++)
        {
            for (int j = 0; j < noisemap.GetLength(1); j++)
            {
                CombinedMap[i, j] = noisemap[i, j] - FallOffMap[i, j];
            }
        }

        MeshData MD = MeshGenerator.GenerateTerrain(CombinedMap, heightMult, levelOfDetail);
        MF.mesh = MD.CreateMesh();

        MR.material = material;



        //then texture

        ColourTheMap(CombinedMap, newTexture);
        material.mainTexture = newTexture;





        RD.GeneratePoints(sizeOfGrid , 50, 4, seed);




        
    }

    public Texture2D ColourTheMap(float[,] noisemap, Texture2D texture)// pass in a noise map and returns the coloured texture.
    {
        //make a new texture.

       

        for (int i = 0; i < texture.width; i++)
        {
            for (int j = 0; j < texture.height; j++)
            {
                currentHeight = noisemap[i, j];

                for (int k = 0; k < Region.Length; k++)
                {
                    if (currentHeight <= Region[k].height)
                    {
                        
                        newTexture.SetPixel(i, j, Region[k].color);

                    }
                }
            }
        }
        
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;

        texture.Apply();

        return texture;
    }

    





}
