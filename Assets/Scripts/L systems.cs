using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class LSystems : MonoBehaviour
{

    public string axion = "F";

    private string currentString;

    public int iterations = 3;

    public string[] laws;

    [SerializeField] private Dictionary<char, string> rules = new Dictionary<char, string>();
    [SerializeField] public float len;
    [SerializeField] public float angle;

    private transformInfo Trans;


    //[SerializeField] private GameObject Tutel;

    public struct transformInfo
    {
        public Vector3 position;
        public Quaternion angle;

        public void SetValues(Vector3 pos, Quaternion ang)
        {
            position = pos;
            angle = ang;
        }
    }

    public Stack<transformInfo> _transform;
    

    private void Awake()
    {
        _transform = new Stack<transformInfo>();
        

        foreach (string law in laws)
        {
            string[] l = law.Split("->");
            rules.Add(l[0][0], l[1]); //grabs what is first in a string then what ever is after

        }

        currentString = axion;

        LineRenderer lr = this.GetComponent<LineRenderer>();
        

        GenerateLSystem();

        DrawLSystem(lr);
    }

    private void GenerateLSystem()
    {
        for (int i = 0; i < iterations; i++)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in currentString)
            {
                sb.Append(rules.ContainsKey(c) ? rules[c] : c.ToString());
            }
            currentString = sb.ToString();
        }
        Debug.Log(currentString);
    }

    

    private void DrawLSystem(LineRenderer lr)
    {
        List<Vector3> posit = new List<Vector3>();    

        foreach(char c in currentString)
        {
            switch (c) 
            {
                case 'F':
                case 'G':
                    transform.position += Vector3.up * len;
                    posit.Add(transform.position);
                    break;

                case '+':
                    transform.Rotate(Vector3.forward * angle);
                    break;
                case '-':
                    transform.Rotate(Vector3.forward * -angle);
                    break;

                case '[':
                    Trans.SetValues(transform.position, transform.rotation);
                    _transform.Push(Trans);
                    break;

                case ']':
                    Trans = _transform.Pop();

                    transform.position = Trans.position;
                    transform.rotation = Trans.angle;
                    break;


                default:
                    break;
            }

            lr.positionCount = posit.Count;
            lr.SetPositions(posit.ToArray());


        }
    }

}
