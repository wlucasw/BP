using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AfficheGames : MonoBehaviour
{
    ConnectBDD BDD;
    public GameObject BDDmenu;
    [SerializeField]
    private Transform SpawnPoint = null;

    [SerializeField]
    private GameObject item = null;

    [SerializeField]
    private RectTransform content = null;

    // Use this for initialization
    void Start()
    {
        BDD = BDDmenu.GetComponent<ConnectBDD>();
        Debug.Log("ici");


        int numberOfM = BDD.RintSQl("Select Count(\"ID\") From \"Matchs\"");
        List<int> IDs = BDD.RLintSQl("Select \"ID\" From \"Matchs\" WHERE \"Matchs\".\"ID\">0 ORDER BY \"ID\"");
        List<int> J1s = BDD.RLintSQl("Select \"J1\" From \"Matchs\" JOIN \"BO\" ON \"BO\"=\"BO\".\"ID\" WHERE \"Matchs\".\"ID\">0 ORDER BY \"Matchs\".\"ID\"");
        List<int> J2s = BDD.RLintSQl("Select \"J2\" From \"Matchs\"JOIN \"BO\" ON \"BO\"=\"BO\".\"ID\" WHERE \"Matchs\".\"ID\">0 ORDER BY \"Matchs\".\"ID\"");
        //setContent Holder Height;
        content.sizeDelta = new Vector2(0, (numberOfM-1) * 32);
        //SpawnPoint.position = new Vector3(80f,10f,0f);

        for (int i = 0; i < numberOfM -1; i++)
        {
            // 60 width of item
            float spawnY = i * 30;
            
            //newSpawn Position
            Vector3 pos = new Vector3(SpawnPoint.position.x, -spawnY , SpawnPoint.position.z);

            //instantiate item
            GameObject SpawnedItem = Instantiate(item, pos, SpawnPoint.rotation);

            //setParent
            SpawnedItem.transform.SetParent(SpawnPoint, false);

            //get ItemDetails Component
            BoutonMdet BDetails = SpawnedItem.GetComponent<BoutonMdet>();

            //set name
            BDetails.id = IDs[i];
            BDetails.ID.text = "Matche " + IDs[i] + " :";
            BDetails.J1.text =  BDD.GetJ_name(J1s[i]);
            BDetails.J2.text = BDD.GetJ_name(J2s[i]);
            BDetails.Score.text = BDD.Get_scoreJ1(">0")[i] + " - " + BDD.Get_scoreJ2(">0")[i];

        }
    }
}