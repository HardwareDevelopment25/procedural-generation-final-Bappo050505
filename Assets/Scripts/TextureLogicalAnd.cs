using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class TextureLogicalAnd : MonoBehaviour
{
    public enum DrawMode {noisemap, colourmap }
    public DrawMode drawMode = DrawMode.noisemap;

    [System.Serializable]
    public struct TerrainHeights
    {
        public string name;
        public float height;
        public Color color;
    }

    public TerrainHeights[] Region;


    float currentHeight;
    public int ImageSize = 64;
    public int seed = 0;
    public float scale = 10f;
    public int octave = 5;
    public float lacunarity = 2;
    public float persistance = 1;
    public Vector2 offset;

    public AnimationCurve curve;

    Color[,] colourmap;

    private Color[] pix;
    Texture2D texture;
    System.Random random;

    

    private void Start()
    {
        

        random = new System.Random();
        texture = new Texture2D(ImageSize, ImageSize);
        pix = new Color[texture.width * texture.height];

        colourmap = new Color[ImageSize, ImageSize];

        //createPattern();
        createNoiseMap();



        //generatePattern();
        
        
        GetComponent<MeshRenderer>().material.mainTexture = texture;
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //generatePattern();
        }
    }

    //creating a fractal
    public void createPattern()
    {
        //go through each pixle in each texture

        for(int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                Color pixleColour = ((x & y) !=0?Color.white:Color.black);
                texture.SetPixel(x, y, pixleColour);
            }
        }
       
        texture.Apply();
    }

    //creating a noise map
    public void createNoiseMap()
    {
        if (scale == 0) scale = 0.00001f;

        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                float xCoord = (float)x/texture.width * scale;
                float ycoord = (float)y/texture.height * scale;
                float Noise = Mathf.PerlinNoise(xCoord, ycoord);
                pix[(int)y * texture.width + (int)x] = new Color(Noise,Noise,Noise);

                
            }
        }

        float[,] noisemap = PerlinNoiseGenerator.GenerateNoiseMap(ImageSize, ImageSize, octave, scale, lacunarity, persistance, random.Next(), offset);

        for (int i = 0; i < noisemap.GetLength(0); i++)
        {
            for (int j = 0; j < noisemap.GetLength(1); j++)
            {
                texture.SetPixel(i, j, new Color(noisemap[i, j], noisemap[i, j], noisemap[i, j]));
            }
        }
        texture.Apply();

        texture.SetPixels(pix);
        texture.Apply();

        Colour(noisemap);
        
    }

    //generating a multi layered perlin noise map
    /*public void generatePattern()
    {
        float[,] noisemap = PerlinNoiseGenerator.GenerateNoiseMap(ImageSize, ImageSize, octave, scale, lacunarity, persistance, random.Next(), offset);

        for (int i = 0; i < noisemap.GetLength(0);  i++)
        {
            for(int j = 0; j < noisemap.GetLength(1); j++)
            {
                texture.SetPixel(i, j, new Color(noisemap[i, j], noisemap[i, j], noisemap[i, j]));
            }
        }
        texture.Apply();

       Colour(noisemap);

    }*/

    public void Colour(float[,] noisemap)
    {

        
        for(int i = 0; i < texture.width; i++)
        {
            for (int j = 0 ; j < texture.height; j++)
            {
                currentHeight = noisemap[i, j];

                for(int k = 0; k < Region.Length; k++)
                {
                    if(currentHeight <= Region[k].height)
                    {
                        colourmap[i,j] = Region[k].color;
                        break;
                    }
                }
            }
        }
        for (int i = 0; i < colourmap.GetLength(0); i++)
        {
            for (int j = 0; j < colourmap.GetLength(1); j++)
            {
                texture.SetPixel(i, j, colourmap[i, j]);
            }
        }
        texture.Apply();

        drawMode = DrawMode.colourmap;

        
    }   


    
   
}
