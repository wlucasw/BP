using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateStats : MonoBehaviour
{
    public GameObject BDDm;
    ConnectBDD BDD;
    public TMP_Text Mj, BOj, BOWr, MWr,Pre;

    public void UpdateS(string J)
    {
        BDD = BDDm.GetComponent<ConnectBDD>();
        int id = BDD.GetJ_id(J);
        int boj = BDD.RintSQl("SELECT COUNT(\"ID\") From \"BO\" WHERE \"J2\" ="+id) + BDD.RintSQl("SELECT COUNT(\"ID\") From \"BO\" WHERE \"J1\" =" + id);
        BOj.text = boj + "";
        int mj = BDD.RintSQl("SELECT COUNT(\"BO\".\"ID\") From \"BO\" Join \"Matchs\" ON \"BO\"=\"BO\".\"ID\" WHERE \"J2\" =" + id) + BDD.RintSQl("SELECT COUNT(\"BO\".\"ID\") From \"BO\" Join \"Matchs\" ON \"BO\"=\"BO\".\"ID\" WHERE \"J1\" =" + id);
        Mj.text = mj + "";
        float mwr = (float) BDD.RintSQl("SELECT COUNT(\"BO\".\"ID\") From \"BO\" Join \"Matchs\" ON \"BO\"=\"BO\".\"ID\" WHERE \"Matchs\".\"Winner\" =" + id)/ (float) mj;
        MWr.text = ((mwr * 100)+"    ").Substring(0, 4) + " %";
        float bowr = (float)BDD.RintSQl("SELECT COUNT(\"BO\".\"ID\") From \"BO\" WHERE \"Winner\" =" + id)/ (float) boj;
        BOWr.text = ((bowr * 100)+"    ").Substring(0, 4) + " %";
        Pre.text = Precision(id);
    }

    string Precision(int id)
    {
        int pre = 0;
        string CondID1 = BDD.GenerateCondIDJ1(id);
        string CondID2 = BDD.GenerateCondIDJ2(id);
        pre = BDD.Somme(BDD.Get_scoreJ1(CondID1))+BDD.Somme(BDD.Get_scoreJ2(CondID2));
        float pr = (float) (pre)/
            (float)(BDD.Somme(BDD.RLintSQl("Select \"J1nbB\"  From \"BO\" Join \"Matchs\" ON \"BO\"=\"BO\".\"ID\" Where \"J1\" = " + id))
            + BDD.Somme(BDD.RLintSQl("Select \"J2nbB\"  From \"BO\" Join \"Matchs\" ON \"BO\"=\"BO\".\"ID\"  Where \"J2\" = " + id)));
        return (((pr*100)+ "    ").Substring(0, 4) + " %");
    }
}
