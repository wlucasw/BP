using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AfficheGMenu : MonoBehaviour
{
    public TMP_Text Winner,Loser,Score,Date,precisonw,precisonl;
    public GameObject BDDmenu,SsMenu;
    ConnectBDD BDD;
    GenerateTable table;

    private void Start()
    {
        BDD = BDDmenu.GetComponent<ConnectBDD>();
        afficheB(BDD.GetJ1(BDD.BO), BDD.GetJ2(BDD.BO), BDD.Get_scoreJ1("=" + BDD.Game)[0] + " - " + BDD.Get_scoreJ2("=" + BDD.Game)[0],"");
    }

    public void afficheB(string J1,string J2,string score,string date)
    {
        table = SsMenu.GetComponent<GenerateTable>();
        BDD = BDDmenu.GetComponent<ConnectBDD>();
        int x = BDD.Get_Winner(BDD.Game);
        int scoreJ1 = BDD.Get_scoreJ1("="+BDD.Game)[0];
        int scoreJ2 = BDD.Get_scoreJ2("="+BDD.Game)[0];
        Winner.text = BDD.GetJ_name(x);
        if (Winner.text == J1)
        {
            Loser.text = J2;
            precisonw.text = (""+(scoreJ1/BDD.RfloatSQl("Select \"J1nbB\" FROM \"Matchs\" WHERE \"ID\" ="+BDD.Game)*100) + "    ").Substring(0, 4) + " %";
            precisonl.text = ("" + (scoreJ2 / BDD.RfloatSQl("Select \"J2nbB\" FROM \"Matchs\" WHERE \"ID\" =" + BDD.Game) * 100) + "    ").Substring(0, 4) + " %";
            table.Generate(BDD.Get_ScoreTour(BDD.Game, 1), BDD.Get_ScoreTour(BDD.Game, 2));
            Score.text = score;
        }
        else
        {
            Loser.text = J1;
            precisonw.text = ("" + (scoreJ2 / BDD.RfloatSQl("Select \"J2nbB\" FROM \"Matchs\" WHERE \"ID\" =" + BDD.Game) * 100)+ "    ").Substring(0,4) + " %";
            precisonl.text = ("" + (scoreJ1 / BDD.RfloatSQl("Select \"J1nbB\" FROM \"Matchs\" WHERE \"ID\" =" + BDD.Game) * 100)+"    ").Substring(0,4) + " %";
            table.Generate(BDD.Get_ScoreTour(BDD.Game, 2), BDD.Get_ScoreTour(BDD.Game, 1));
            Score.text = BDD.Get_scoreJ2("="+BDD.Game)[0] + " - " + BDD.Get_scoreJ1("=" + BDD.Game)[0];
        }

    }
}
