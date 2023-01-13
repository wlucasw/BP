using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Afficheplayer : MonoBehaviour
{
    public TMP_Text J1;
    public TMP_Text J2;
    ConnectBDD BDD;
    public GameObject BDDgame;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void affiche(string j1,string j2)
    {
        BDD = BDDgame.GetComponent<ConnectBDD>();
        J1.text = j2;
        J2.text = j1;
    }
}
