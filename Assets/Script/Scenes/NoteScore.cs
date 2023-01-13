using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Npgsql;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NoteScore : MonoBehaviour
{
    public int BO = -1;
    public int Game = -1;
    public int balleT = -1, touch1, touch2;
    public Camera cameraJ1,cameraJ2;
    int balleJ1, balleJ2, nbT1, nbT2, contr1, contr2, balleJ1C, balleJ2C,Btot1,Btot2;
    public bool Tj1;
    ConnectBDD BDD,BDDm;
    public GameObject BDDgame;
    Camera cmera;
    public GameObject J1, J2;
    public Canvas UIM;
    public Button Normal, Rebond, Renversé;
    public bool Norm, Reb, Renv;
    int mode,NbReb;
    public Canvas UIG;
    MvmtVerre Interface;

    void DisableMode()
    {
        if (mode == 1)
        {
            Norm = false;
        }
        if (mode == 2)
        {
            Reb = false;
        }
        if (mode == 3)
        {
            Renv = false;
        }
    }
    public void ChangeNorm()
    {
        Norm = true;
        DisableMode();
        mode = 1;
    }

    public void ChangeReb()
    {
        Reb = true;
        DisableMode();
        mode = 2;
    }

    public void ChangeRenv()
    {
        Renv = true;
        DisableMode();
        mode = 3;
    }
    //Notation score
    public void AddVerre(string verre,int balle, string mise)
    {
        string connectionString =
           "Server=localhost;" +
           "Database=BeerPong;" +
           "User ID=postgres;" +
           "Password=cheval;";

        NpgsqlConnection dbcon;
        dbcon = new NpgsqlConnection(connectionString);
        dbcon.Open();
        string sql2 = "UPDATE \"Matchs\"SET \"" + verre + "\" =" + balle + ",\"" + verre + "t\" = '" + mise + "'  Where \"ID\" = " + Game;
        Debug.Log(sql2);
        NpgsqlCommand dbcmd2 = dbcon.CreateCommand();
        dbcmd2.CommandText = sql2;
        dbcmd2.ExecuteScalar();
        dbcmd2.Dispose();
        dbcmd2 = null;
        dbcon.Close();
        dbcon = null;
    }

    public void AddVerreC(string verre, int balle, string mise)
    {

        string test = "=" + Game;
        List<string> contre = BDD.RLcontrestrSQl(verre.Substring(0, 2), test)[0];
        contre.Add(verre.Substring(3));
        contre.Add(""+balle);
        contre.Add(mise);
        string sqlcontre = BDD.LintoSQL(contre);
        BDD.WSQl("Update \"Matchs\" Set \""+ verre.Substring(0, 2) + "C\" = " + sqlcontre + " Where \"ID\" = " + Game);

    }

    public void SetWinner(int J)
    {
        string connectionString =
   "Server=localhost;" +
   "Database=BeerPong;" +
   "User ID=postgres;" +
   "Password=cheval;";

        NpgsqlConnection dbcon;
        dbcon = new NpgsqlConnection(connectionString);
        dbcon.Open();
        string sql2 = "UPDATE \"Matchs\"SET \"Winner\" =" + J + "  Where \"ID\" = " + Game;
        NpgsqlCommand dbcmd2 = dbcon.CreateCommand();
        dbcmd2.CommandText = sql2;
        dbcmd2.ExecuteScalar();
        dbcmd2.Dispose();
        dbcmd2 = null;
        dbcon.Close();
        dbcon = null;
    }

    //Début de partie
    public void Init()
    {
        balleJ1 = 0;
        balleJ2 = 0;
        balleJ1C = 0;
        balleJ2C = 0;
        nbT1 = 0;
        nbT2 = 0;
        touch1 = 0;
        touch2 = 0;
        contr2 = 0;
        contr1 = 0;
        Btot1 = 0;
        Btot2 = 0;
        mode = 1;
        Norm = true;
        Reb = false;
        Renv = false;
        NbReb = 0;
    }

    public void Start()
    {
        Normal.Select();
        BDD = BDDgame.GetComponent<ConnectBDD>();
        BDDm = UIM.transform.GetChild(0).GetChild(7).GetComponent<ConnectBDD>();
        Tj1 = J1engagt();
        Interface = UIG.GetComponent<MvmtVerre>();

    }

    public bool J1engagt()
    {
        //BDD = BDDgame.GetComponent<ConnectBDD>();
        int engagt =BDD.RintSQl("Select \"engagt\" FROM \"BO\" where \"ID\" =" + BO);
        bool engage = (BDD.GetJ_id(BDD.GetJ1(BO)) == engagt);
        return engage;
    }

    //Fin de game :
    public void EndGame()
    {
        string j1 = BDD.GetJ1(BO);
        string j2 = BDD.GetJ2(BO);
        BDD.WSQl("Update \"Matchs\" Set \"J1nbB\" = "+Btot1+" Where \"ID\" = "+Game);
        BDD.WSQl("Update \"Matchs\" Set \"J2nbB\" = " + Btot2 + " Where \"ID\" = " + Game);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        BDDm.Game = Game;
        BDDm.BO = BO;
        if (contr1>contr2)
        {
            SetWinner(BDD.GetJ_id(j1));
            //aff.afficheB(j1, j2, BDD.Get_scoreJ1("=" + BDD.Game)[0] + " - " + BDD.Get_scoreJ2("=" + BDD.Game)[0], "");
        }
        else
        {
            SetWinner(BDD.GetJ_id(j2));
            //aff.afficheB(j2,j1, BDD.Get_scoreJ1("=" + BDD.Game)[0] + " - " + BDD.Get_scoreJ2("=" + BDD.Game)[0], "");
        }
    }

    public void JoueContre(Transform verre)
    {
        if (Tj1 && touch1 < 9 )
        {
            Btot1 += 1;
            AddVerre(verre.name, nbT1, "m");
            balleJ1 += 1;
            Destroy(verre.gameObject.transform.parent.gameObject);
            touch1 += 1;
            contr1 += 1;
        }
        else
        {
            if (!Tj1 && touch2 < 9 )
            {
                Btot2 += 1;
                AddVerre(verre.name, nbT2, "m");
                balleJ2 += 1;
                Destroy(verre.gameObject.transform.parent.gameObject);
                touch2 += 1;
                contr2 += 1;
            }
            else
            {
                if (Tj1)
                {
                    Btot1 += 1;
                    AddVerreC(verre.name, nbT1, "m");
                    balleJ1C += 1;
                    touch1 += 1;
                    contr1 += 1;
                }
                else
                {
                    Btot2 += 1;
                    AddVerreC(verre.name, nbT2, "m");
                    balleJ2C += 1;
                    touch2 += 1;
                    contr2 += 1;
                }
                //Perdu
                if (Tj1 && contr1<contr2 && balleJ1C>=balleT)
                {
                    EndGame();
                }
                else
                {
                    if (!Tj1 && contr1 > contr2 && balleJ2C>= balleT)
                    {
                        EndGame();
                    }
                    else
                    {
                        if (balleJ1C >= balleT || balleJ2C >= balleT)
                        {
                            chgtTour();
                        }
                    }
                }
            }
        }
        
    }

    public void JoueContreR(Transform verre)
    {
        if (NbReb == 2)
        {
            if (Tj1 && touch1 < 9)
            {
                Btot1 += 1;
                AddVerre(verre.name, nbT1, "r");
                balleJ1 += 1;
                Destroy(verre.gameObject);
                touch1 += 1;
                contr1 += 1;
            }
            else
            {
                if (!Tj1 && touch2 < 9)
                {
                    Btot2 += 1;
                    AddVerre(verre.name, nbT2, "r");
                    balleJ2 += 1;
                    Destroy(verre.gameObject);
                    touch2 += 1;
                    contr2 += 1;
                }
                else
                {
                    if (Tj1)
                    {
                        Btot1 += 1;
                        AddVerreC(verre.name, nbT1, "r");
                        balleJ1C += 1;
                        touch1 += 1;
                        contr1 += 1;
                    }
                    else
                    {
                        Btot2 += 1;
                        AddVerreC(verre.name, nbT2, "r");
                        balleJ2C += 1;
                        touch2 += 1;
                        contr2 += 1;
                    }
                }
            }
        }
        else
        {
            if (Tj1 && touch1 < 9)
            {
                AddVerre(verre.name, nbT1, "r");
                Destroy(verre.gameObject);
                touch1 += 1;
            }
            else
            {
                if (!Tj1 && touch2 < 9)
                {
                    AddVerre(verre.name, nbT2, "r");
                    Destroy(verre.gameObject);
                    touch2 += 1;
                }
                else
                {
                    if (Tj1)
                    {
                        AddVerreC(verre.name, nbT1, "r");
                        touch1 += 1;
                    }
                    else
                    {
                        AddVerreC(verre.name, nbT2, "r");
                        touch2 += 1;
                    }
                    //Perdu
                    if (Tj1 && contr1 < contr2 && balleJ1C >= balleT)
                    {
                        EndGame();
                    }
                    else
                    {
                        if (!Tj1 && contr1 > contr2 && balleJ2C >= balleT)
                        {
                            EndGame();
                        }
                        else
                        {
                            if (balleJ1C >= balleT || balleJ2C >= balleT)
                            {
                                chgtTour();
                            }
                        }
                    }
                }
            }
        }
    }

    public void JoueContreT(Transform verre)
    {
        if (Tj1 && touch1 < 9)
        {
            Btot1 += 1;
            AddVerre(verre.name, nbT1, "t");
            Destroy(verre.gameObject);
            touch1 += 1;
            contr1 += 1;
        }
        else
        {
            if (!Tj1 && touch2 < 9)
            {
                Btot2 += 1;
                AddVerre(verre.name, nbT2, "t");
                Destroy(verre.gameObject);
                touch2 += 1;
                contr2 += 1;
            }
            else
            {
                if (Tj1)
                {
                    Btot1 += 1;
                    AddVerreC(verre.name, nbT1, "t");
                    touch1 += 1;
                    contr1 += 1;
                }
                else
                {
                    Btot2 += 1;
                    AddVerreC(verre.name, nbT2, "t");
                    touch2 += 1;
                    contr2 += 1;
                }
            }
        }

    }

    //déroulment du jeu
    public void MissJ1()
    {
        if (Tj1)
        {
            Normal.onClick.Invoke();
            Btot1 += 1;
            if (touch2 >= 9)
            {
                balleJ1C += 1;
            }
            else
            {
                balleJ1 += 1;
            }
            if (balleJ1 + balleJ1C >= balleT)
            {

                if (contr2 > contr1)
                {
                    EndGame();
                }
                else
                {
                    chgtTour();
                }
            }
        }

    }

    public void MissJ2()
    {
        if (!Tj1)
        {
            Btot2 += 1;
            if (touch2 >= 9)
            {
                balleJ2C += 1;
            }
            else
            {
                balleJ2 += 1;
            }
            if (balleJ2 + balleJ2C >= balleT)
            {
                if (contr2 < contr1)
                {
                    EndGame();
                }
                else
                {
                    chgtTour();
                }
            }
        }
    }

    public void chgtTour()
    {
        if (Tj1)
        {
            Tj1 = !Tj1;
            balleJ1 = 0;
            nbT1 += 1;
            contr2 = 0;
            balleJ1C = 0;
            if (touch1 <= 9)
            {
                contr1 = 0;
            }
        }
        else
        {
            Tj1 = !Tj1;
            balleJ2 = 0;
            nbT2 += 1;
            contr1 = 0;
            balleJ2C = 0;
            if (touch2 <= 9)
            {
                contr2 = 0;
            }
        }
    }

    void DeroulementNorm()
    {
        Transform hitted;
        if (SelectVerre(out hitted))
        {
            if (Tj1 && touch1 < 9 && contr2 == 0)
            {
                AddVerre(hitted.name, nbT1, "m");
                balleJ1 += 1;
                Btot1 += 1;
                Destroy(hitted.gameObject.transform.parent.gameObject);
                touch1 += 1;
            }
            else
            {
                if (!Tj1 && touch2 < 9 && contr1 == 0)
                {
                    AddVerre(hitted.name, nbT2, "m");
                    balleJ2 += 1;
                    Btot2 += 1;
                    Destroy(hitted.gameObject.transform.parent.gameObject);
                    touch2 += 1;
                }
                else
                {
                    JoueContre(hitted);
                }
            }
        }
        if (balleT == 1)
        {
            if (balleJ2 >= balleT)
            {
                chgtTour();
            }
            if (balleJ1 >= balleT)
            {
                chgtTour();
            }
        }
    }

    void DeroulementRenv()
    {
        Transform hitted;
        if (SelectVerre(out hitted))
        {
            if (Tj1 && touch1 < 9 && contr2 == 0)
            {
                AddVerre(hitted.name, nbT1, "t");
                Btot1 += 1;
                Destroy(hitted.gameObject.transform.parent.gameObject);
                touch1 += 1;
            }
            else
            {
                if (!Tj1 && touch2 < 9 && contr1 == 0)
                {
                    AddVerre(hitted.name, nbT2, "t");
                    Btot2 += 1;
                    Destroy(hitted.gameObject.transform.parent.gameObject);
                    touch2 += 1;
                }
                else
                {
                    JoueContreT(hitted);
                }
            }
        }
    }

    void DeroulementReb()
    {
        Transform hitted;
        if (SelectVerre(out hitted))
        {

            NbReb++;
            if (NbReb == 2)
            {
                ChangeNorm();
                if (Tj1 && touch1 < 9 && contr2 == 0)
                {
                    Normal.Select();
                    AddVerre(hitted.name, nbT1, "r");
                    balleJ1 += 1;
                    Btot1 += 1;
                    Destroy(hitted.gameObject.transform.parent.gameObject);
                    touch1 += 1;
                }
                else
                {
                    if (!Tj1 && touch2 < 9 && contr1 == 0)
                    {
                        Normal.Select();
                        AddVerre(hitted.name, nbT2, "r");
                        balleJ2 += 1;
                        Btot2 += 1;
                        Destroy(hitted.gameObject.transform.parent.gameObject);
                        touch2 += 1;
                    }
                    else
                    {
                        JoueContreR(hitted);
                    }
                }
                if (balleT == 1)
                {
                    if (balleJ2 >= balleT)
                    {
                        chgtTour();
                    }
                    if (balleJ1 >= balleT)
                    {
                        chgtTour();
                    }
                }
                NbReb = 0;
            }
            else
            {
                if (Tj1 && touch1 < 9 && contr2 == 0)
                {
                    AddVerre(hitted.name, nbT1, "r");
                    Destroy(hitted.gameObject.transform.parent.gameObject);
                    touch1 += 1;
                }
                else
                {
                    if (!Tj1 && touch2 < 9 && contr1 == 0)
                    {
                        AddVerre(hitted.name, nbT2, "r");
                        Destroy(hitted.gameObject.transform.parent.gameObject);
                        touch2 += 1;
                    }
                    else
                    {
                        JoueContreR(hitted);
                    }
                }
            }
        }

    }

    bool InputDown()
    {
        return (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space));
    }

    bool SelectVerre(out Transform hitted)
    {

        Ray ray = cmera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            hitted = hit.transform;
            return (Physics.Raycast(ray, out hit));
        }
        return (Interface.Selected(out hitted));
    }

    void Mode()
    {
        if (Renv)
        {
            Renversé.Select();
            Norm = false;
        }
        else 
        {
            if (Reb)
            {
                Rebond.Select();
                Norm = false;
            }
            else
            {

                if (Norm)
                {
                    Normal.Select();
                }
                else
                {
                    Norm = true;
                }
            }
        }
    }

    private void Update()
    {
        Mode();
        if (InputDown())
        {
            if (Tj1) { cmera = cameraJ1; }
            else { cmera = cameraJ2; }
            if (Norm)
            {
                DeroulementNorm();
            }
            if (Renv)
            {
                DeroulementRenv();
            }
            if (Reb)
            {
                DeroulementReb();
            }
        }

    }
}
