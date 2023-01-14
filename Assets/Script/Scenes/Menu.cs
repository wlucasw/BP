using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{
    public TMP_Text J1;
    public TMP_Text J2;
    public TMP_InputField nbBalle;
    public TMP_Text engage;
    public GameObject emptyBDD;
    ConnectBDD BDD;
    public GameObject BDDG;
    public GameObject UIg;

    private void Start()
    {
        BDD = emptyBDD.GetComponent<ConnectBDD>();
    }
    public void PlayGame()
    {
        BDD.BO = BDD.AddBO(J1.text, J2.text, engage.text, int.Parse(nbBalle.text));
        BDD.Game = BDD.AddMa(BDD.BO);
        SceneManager.LoadScene("Game");
        BDDG.GetComponent<NoteScore>().BO = BDD.BO;
        BDDG.GetComponent<NoteScore>().Game = BDD.Game;
        BDDG.GetComponent<NoteScore>().balleT = int.Parse(nbBalle.text);
        BDDG.GetComponent<ConnectBDD>().BO = BDD.BO;
        BDDG.GetComponent<ConnectBDD>().Game = BDD.Game;
        UIg.GetComponent<Afficheplayer>().affiche(BDD.GetJ1(BDD.BO),BDD.GetJ2(BDD.BO));
        //BDDG.GetComponent<NoteScore>().Tj1 = BDDG.GetComponent<NoteScore>().J1engagt();
        BDDG.GetComponent<NoteScore>().Init();
    }

    public void PlayNewGame()
    {
        BDD.Game = BDD.AddMa(BDD.BO);
        SceneManager.LoadScene("Game");
        BDDG.GetComponent<NoteScore>().BO = BDD.BO;
        BDDG.GetComponent<NoteScore>().Game = BDD.Game;
        BDDG.GetComponent<NoteScore>().balleT = BDD.RintSQl("Select \"nbBtour\" From \"BO\" Where \"ID\" = "+ BDD.BO);
        BDDG.GetComponent<ConnectBDD>().BO = BDD.BO;
        BDDG.GetComponent<ConnectBDD>().Game = BDD.Game;
        UIg.GetComponent<Afficheplayer>().affiche(BDD.GetJ1(BDD.BO), BDD.GetJ2(BDD.BO));
        //BDDG.GetComponent<NoteScore>().Tj1 = BDDG.GetComponent<NoteScore>().J1engagt();
        BDDG.GetComponent<NoteScore>().Init();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

   
}
