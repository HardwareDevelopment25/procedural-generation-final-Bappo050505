using UnityEngine;

public static class PerlinNoiseGenerator
{

    public static float[,] GenerateFalloffMap(int size, AnimationCurve curve)
    {
        float[,] falloffMap = new float[size, size];

        for (int i = 0; i < size; i++) 
        { 
            for (int j = 0; j < size; j++)
            {
                float x = i / (float)size * 2 - 1;
                float y = j / (float)size * 2 - 1;

                float value = Mathf.Max(Mathf.Abs(x) , Mathf.Abs(y));

                falloffMap[i, j] = curve.Evaluate(value);
            }
        }

        

        return falloffMap;
    }

    public static float[,] GenerateNoiseMap (int mapHeight, int mapWidth ,int octave,  float scale , float lacunarity , float persistance , int seed , Vector2 offset)
    {
        float[,] noiseMap = new float[mapHeight, mapWidth];
        if (scale == 0) scale = 0.0001f;

        float maxPossibleHeight = float.MinValue;
        float minPossibleHeight = float.MaxValue;

        System.Random rand = new System.Random(seed);

        Vector2[] octaveOffset = new Vector2[octave]; // try have less than 5 offset



            //creates x amount of samples of offsets for variation and randomness
            for (int i = 0; i < octave; i++)
            {
                float offsetx = rand.Next(-100000, 100000) + offset.x;
                float offsety = rand.Next(-100000, 100000) + offset.y;

                octaveOffset[i] = new Vector2 (offsetx, offsety);
            }

            

            //creates the perlin map
            for (int y = 0; y < mapHeight; y++)
            {
                for(int x =0; x< mapWidth; x++)
                {
                    float amplitude = 1, frequencey = 1, noiseheight = 1;

                    for (int i = 0; i < octave; i++) //creates individual perlin noise maps
                    {

                        float sampleX = ((float)(x - mapWidth / 2)) / scale * frequencey + octaveOffset[i].x;
                        float sampleY = ((float)(y - mapHeight / 2)) / scale * frequencey + octaveOffset[i].y;

                        float perlinResult = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                        noiseheight += perlinResult * amplitude;
                        amplitude *= persistance;
                        frequencey *= lacunarity;

                    }
                    //we are after the highest peak and the lowest peak for our future lerp
                    if (noiseheight > maxPossibleHeight) maxPossibleHeight = noiseheight;
                    else if (noiseheight < minPossibleHeight) minPossibleHeight = noiseheight;

                    noiseMap[x, y] = Mathf.InverseLerp(minPossibleHeight, maxPossibleHeight, noiseheight);



                }

            }
        

        return noiseMap;
    }



}
