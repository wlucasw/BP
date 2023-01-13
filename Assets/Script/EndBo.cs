using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBo : MonoBehaviour
{
    public GameObject BDDUI;
    ConnectBDD BDD;

    private void Start()
    {
        BDD = BDDUI.GetComponent<ConnectBDD>();
    }

    public void EndBO()
    {
        int winner = BDD.RintSQl("Select \"Winner\" From \"Matchs\" Where \"ID\" = "+BDD.Game);
        //Debug.Log("Update \"BO\" Set \"winner\" = " + winner + "Where \"ID\" = " + BDD.BO);
        BDD.WSQl("Update \"BO\" Set \"Winner\" = "+winner+"Where \"ID\" = "+BDD.BO);
    }

}
