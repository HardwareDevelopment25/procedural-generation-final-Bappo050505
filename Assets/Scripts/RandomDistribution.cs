using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.VFX;

public class RandomDistrbution : MonoBehaviour
{

    

    List<Vector3> points = new List<Vector3>();

    [SerializeField] GameObject mSphere;

    private int currentAmount = 0;

    System.Random rand;


   public  void GeneratePoints(int sizeOfGrid , int noSpawns , int Uniformity, int Seed )
    {
        rand = new System.Random(Seed);

        int range = sizeOfGrid;
        int amount = noSpawns;
        int numberOfCandidates = Uniformity;
        

        float minDist = float.MaxValue;
        float currentDist = 0;
        float[] distances = new float[numberOfCandidates];

        while (currentAmount < amount)
        {
            currentAmount++;
            Vector3[] candidates = new Vector3[numberOfCandidates];

            for (int i = 0; i < numberOfCandidates; i++)
            {
                candidates[i] = new Vector3((float)rand.Next(-(range/2), (range/2) + 1), 0, (float)rand.Next(-(range / 2), (range / 2) + 1));
            }
            
                
        

            for (int Candidate = 0; Candidate < candidates.Length; Candidate++)
            {
                minDist = float.MaxValue;
                foreach (Vector3 point in points)
                {
                    currentDist = Mathf.Abs(Vector2.Distance(candidates[Candidate], point));
                    if (currentDist < minDist)
                    {
                        minDist = currentDist;
                    }
                    distances[Candidate] = minDist;
                }
            }

            int correctCandidate = distances.ToList().IndexOf(distances.Max());
            points.Add(candidates[correctCandidate]);






            Instantiate(mSphere, candidates[correctCandidate], Quaternion.identity, this.transform);
            
        }

    }
}










     


