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
    
    //public int levelOfDetail = 1;

    public int sizeOfGrid = 64;
   
    public int seed = 0;
    public float scale = 10f;
    public int octave = 5;
    public float lacunarity = 2;
    public float persistance = 1;
    public Vector2 offset;
    public AnimationCurve AC;
    

    public Texture2D newTexture;
    float currentHeight;
    public TerrainHeights[] Region;

    

   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        newTexture = new Texture2D(sizeOfGrid, sizeOfGrid);

        MeshFilter MF = this.AddComponent<MeshFilter>();
        MeshRenderer MR =  this.AddComponent<MeshRenderer>();

        Material material = new Material(Shader.Find("Unlit/Texture"));

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

        MeshData MD = MeshGenerator.GenerateTerrain(CombinedMap, 5f);
        MF.mesh = MD.CreateMesh();

        MR.material = material;



        //then texture

        ColourTheMap(CombinedMap,newTexture);
        material.mainTexture = newTexture;


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
