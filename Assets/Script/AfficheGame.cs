using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AfficheGame : MonoBehaviour
{

    public Button Bmatch;
    BoutonMdet det;
    GameObject Menu;
    ConnectBDD BDD;
    public void affiche()
    {
        det = Bmatch.GetComponent<BoutonMdet>();
        Menu = GameObject.Find("UI Menu");
        GameObject GMenu = Menu.transform.GetChild(0).GetChild(2).gameObject;
        GMenu.SetActive(true);
        Menu.transform.GetChild(0).GetChild(5).gameObject.SetActive(false);
        BDD = GameObject.Find("BDD").GetComponent<ConnectBDD>();
        BDD.Game = det.id;
        BDD.BO = BDD.Get_BO(BDD.Game);
        GMenu.GetComponent<AfficheGMenu>().afficheB(det.J1.text,det.J2.text, det.Score.text, det.Date.text);
    }
}
