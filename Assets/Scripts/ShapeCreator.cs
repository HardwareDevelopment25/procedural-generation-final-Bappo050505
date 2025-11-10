using Unity.VisualScripting;
using UnityEngine;

public class ShapeCreator : MonoBehaviour
{

    public int sizeOfGrid = 64;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MeshFilter MF = this.AddComponent<MeshFilter>();
        MeshRenderer MR =  this.AddComponent<MeshRenderer>();

        Material material = new Material(Shader.Find("Unlit/Texture"));
        MR.material = material;

        float[,] noisemap = PerlinNoiseGenerator.GenerateNoiseMap(sizeOfGrid, sizeOfGrid, 5, 10, 1, 5, 0, Vector2.zero);
        MeshData MD = MeshGenerator.GenerateTerrain(noisemap, 10f);

        MF.mesh = MD.CreateMesh();

        //then texture

    }

  

    
    
}
