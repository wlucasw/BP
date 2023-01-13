using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Data;
using Npgsql;

public class AddPlayer : MonoBehaviour
{
    public TMP_InputField Nom;
    public TMP_Dropdown Liste;
    ConnectBDD BDD;
    public GameObject emptyBDD;

    public void Start()
    {
        BDD = emptyBDD.GetComponent<ConnectBDD>();
    }
    public void NewPlayer()
    {
        string name = Nom.text;
        BDD.Addp(name);
        Liste.options.Add(new TMP_Dropdown.OptionData() { text = name });
    }
    
}
