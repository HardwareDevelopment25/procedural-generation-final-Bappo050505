using TMPro;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public ShapeCreator SCs;
    public TextMeshProUGUI SeedText;
    public TextMeshProUGUI SizeText;

    private void Awake()
    {
        SCs = GameObject.FindWithTag("Logic holder").GetComponent<ShapeCreator>();

    }

    public void ChangeSeed(string Seed)
    {
        SCs.seed= int.Parse(Seed);
        
    }
    public void ChangeSize(string Size)
    {
        SCs.sizeOfGrid = int.Parse(Size);

        if (SCs.sizeOfGrid > 1024)
        {
            SCs.sizeOfGrid = 1024;
        }

    }
        

}
