using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateHmap : MonoBehaviour
{
    public GameObject BDDm,Hmap,Verre,J1,J2;
    ConnectBDD BDD;
    // Start is called before the first frame update
    void Start()
    {
        BDD = BDDm.GetComponent<ConnectBDD>();
        Generate();
    }

    void AddVerre(Vector3 pos, int score,GameObject J)
    {
        //instantiate item
        GameObject SpawnedItem1 = Instantiate(Verre, pos, Hmap.transform.rotation);

        //setParent
        SpawnedItem1.transform.SetParent(J.transform, false);

        //get ItemDetails Component
        CaseDet BDetails1 = SpawnedItem1.GetComponent<CaseDet>();

        //set name
        if (score >= 0)
        {
            BDetails1.text.text = "" + score;
            score++;
            BDetails1.im.color = new Vector4(255f / 255f, score * 40f / 255f, 0f / 255f, 255f / 255f);
        }
        else
        {
            BDetails1.text.text = "X";
            BDetails1.im.color = new Vector4(0f / 255f, 0f / 255f, 0f / 255f, 0f / 255f);
        }
    }

    int Tri(int x)
    {
        if (x == 0)
        {
            return x;
        }
        else
        {
            return (x + Tri(x - 1));
        }
    }

    void Generate()
    {
        List<int> placement = new List<int>();
        placement.Add(0);
        placement.Add(1);
        placement.Add(2);
        placement.Add(3);
        float h = 70f, w = 100f;
        int CJ1 = -1,CJ2 = -1,TC1=-1,TC2=-1;
        List<string> C1 = BDD.RLcontrestrSQl("J1","="+BDD.Game)[0];
        List<string> C2 = BDD.RLcontrestrSQl("J2", "=" + BDD.Game)[0];
        if (C1.Count > 2)
        {
            CJ1 = int.Parse(C1[0]);
            TC1 = int.Parse(C1[1]);
        }
        if (C2.Count > 2)
        {
            CJ2 = int.Parse(C2[0]);
            TC2 = int.Parse(C2[1]);
        }
        foreach (int j in placement)
        {
            for (int i = 1; i <= j+1; i++)
            {
                // 60 width of item
                float spawnx = ((4-j)+2*i)*w/2;
                float spawnY = (j+1) * h;

                //newSpawn Position
                Vector3 pos1 = new Vector3(spawnx, spawnY, Hmap.transform.position.z);
                Vector3 pos2 = new Vector3(spawnx, spawnY, Hmap.transform.position.z);
                int k = i + Tri(j);
                if (k == CJ1)
                {
                    if (k == CJ2)
                    {
                        AddVerre(pos1,TC1, J1);
                        AddVerre(pos2, TC2, J2);
                    }
                    else
                    {
                        AddVerre(pos1, TC1, J1);
                        AddVerre(pos2, BDD.RintSQl("Select \"J2_" + k + "\" From \"Matchs\" Where \"ID\" = " + BDD.Game), J2);
                    }
                }
                else
                {
                    if (k == CJ2)
                    {
                        AddVerre(pos1, BDD.RintSQl("Select \"J1_" + k + "\" From \"Matchs\" Where \"ID\" = " + BDD.Game), J1);
                        AddVerre(pos2, TC2, J2);
                    }
                    else
                    {
                        AddVerre(pos1, BDD.RintSQl("Select \"J1_" + k + "\" From \"Matchs\" Where \"ID\" = " + BDD.Game), J1);
                        AddVerre(pos2, BDD.RintSQl("Select \"J2_" + k + "\" From \"Matchs\" Where \"ID\" = " + BDD.Game), J2);
                    }
                }
            }
        }
    }
}

