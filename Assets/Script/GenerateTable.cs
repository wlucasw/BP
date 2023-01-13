using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class GenerateTable : MonoBehaviour
{
    public GameObject Case;
    public Transform SpawnPoint;
    public RectTransform content;

    //int i = 0, j = 0;

    List<int> test = new List<int>();

    int[] j1 = { 0, 1, 1 }, j2 = { 0, 0, 1 };
    
    int MaxL(List<int> L)
    {
        int max = L[0];
        foreach (int x in L)
        {
            if (x > max) { max = x; }
        }
        return max;
    }

    int IsInL(List<int> L,int e)
    {
        int k = 0;
        foreach (int x in L)
        {
            if (x == e) { k++; }
        }
        return k;
    }

    void AddCase(Vector3 pos,int score)
    {
        //instantiate item
        GameObject SpawnedItem1 = Instantiate(Case, pos, SpawnPoint.rotation);

        //setParent
        SpawnedItem1.transform.SetParent(SpawnPoint, false);

        //get ItemDetails Component
        CaseDet BDetails1 = SpawnedItem1.GetComponent<CaseDet>();

        //set name
        if (score > 0)
        {
            BDetails1.text.text = "" + score;
            BDetails1.im.color = new Vector4(46f / 255f, 183f / 255f, 46f / 255f, 255f / 255f);
        }
    }
    public void Generate(List<int> tirsJ1, List<int> tirsJ2)
    {
        int numberOfx =  MaxL(tirsJ1)+1;

        float w = 120f, h = 105f;
        content.sizeDelta = new Vector2( h, (numberOfx - 1) * w);
        for (int j = 0; j < numberOfx; j++)
        {
            // 60 width of item
            float spawnY = h;
            float spawnx = j * w;

            //newSpawn Position
            Vector3 pos1 = new Vector3(spawnx, -spawnY, SpawnPoint.position.z);
            Vector3 pos2 = new Vector3(spawnx, -2*spawnY, SpawnPoint.position.z);

            AddCase(pos1, IsInL(tirsJ1, j));
            AddCase(pos2, IsInL(tirsJ2, j));
        }
        if (numberOfx == MaxL(tirsJ1))
        {
            for (int j = numberOfx + 1; j < MaxL(tirsJ2); j++)
            {
                // 60 width of item
                float spawnY = h;
                float spawnx = j * w;

                //newSpawn Position
                Vector3 pos1 = new Vector3(spawnx, -spawnY, SpawnPoint.position.z);
                Vector3 pos2 = new Vector3(spawnx, -2 * spawnY, SpawnPoint.position.z);

                AddCase(pos1, 0);
                AddCase(pos2, IsInL(tirsJ2, j));
            }
        }
        else
        {
            for (int j = numberOfx + 1; j < MaxL(tirsJ1); j++)
            {
                // 60 width of item
                float spawnY = h;
                float spawnx = j * w;

                //newSpawn Position
                Vector3 pos1 = new Vector3(spawnx, -spawnY, SpawnPoint.position.z);
                Vector3 pos2 = new Vector3(spawnx, -2 * spawnY, SpawnPoint.position.z);

                AddCase(pos2, 0);
                AddCase(pos1, IsInL(tirsJ1, j));
            }
        }
    }
}
