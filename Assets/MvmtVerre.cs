using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MvmtVerre : MonoBehaviour
{
    NoteScore Notation;
    public GameObject BDD;
    public GameObject J1, J2;
    public GameObject VerreSelected;
    Material sel;
    int curr;
    bool Tj;

    private void Start()
    {
        Notation = BDD.GetComponent<NoteScore>();
        curr = 0;
        Tj = Notation.Tj1;
        if (Notation.Tj1)
        {
            VerreSelected = J1.transform.GetChild(0).GetChild(0).gameObject;
            VerreSelected.GetComponent<MeshRenderer>().material.color = new Vector4(46f / 255f, 183f / 255f, 46f / 255f, 255f / 255f);
        }
        if (!Notation.Tj1)
        {
            VerreSelected = J2.transform.GetChild(0).GetChild(0).gameObject;
            VerreSelected.GetComponent<MeshRenderer>().material.color = new Vector4(46f / 255f, 183f / 255f, 46f / 255f, 255f / 255f);
        }
    }

    public bool Selected(out Transform hit)
    {
        hit = VerreSelected.transform;
        return (Input.GetKeyDown(KeyCode.Space));
    }

    void ChgmtVerre(int k)
    {
        curr += k;
        if (Notation.Tj1)
        {
            if (Notation.touch1 < 9)
            {
                curr += 10-Notation.touch1;
                curr %= Mathf.Max(1, 10 - Notation.touch1);
                VerreSelected.GetComponent<MeshRenderer>().material.color = new Vector4(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
                VerreSelected = J1.transform.GetChild(curr).GetChild(0).gameObject;
                VerreSelected.GetComponent<MeshRenderer>().material.color = new Vector4(46f / 255f, 183f / 255f, 46f / 255f, 255f / 255f);
            }
            else
            {
                curr = 0;
                VerreSelected = J1.transform.GetChild(0).GetChild(0).gameObject;
                VerreSelected.GetComponent<MeshRenderer>().material.color = new Vector4(46f / 255f, 183f / 255f, 46f / 255f, 255f / 255f);
            }

        }
        else
        {
            if (Notation.touch2 < 9)
            {
                curr += 10- Notation.touch2;
                curr %= Mathf.Max(1, 10 - Notation.touch2);
                VerreSelected.GetComponent<MeshRenderer>().material.color = new Vector4(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
                VerreSelected = J2.transform.GetChild(curr).GetChild(0).gameObject;
                VerreSelected.GetComponent<MeshRenderer>().material.color = new Vector4(46f / 255f, 183f / 255f, 46f / 255f, 255f / 255f);
            }
            else
            {
                curr = 0;
                VerreSelected = J2.transform.GetChild(0).GetChild(0).gameObject;
                VerreSelected.GetComponent<MeshRenderer>().material.color = new Vector4(46f / 255f, 183f / 255f, 46f / 255f, 255f / 255f);
            }
        }
    }

    private void Update()
    {
        if (VerreSelected == null)
        {
            if (Notation.Tj1)
            {

                    curr = 0;
                    VerreSelected = J1.transform.GetChild(0).GetChild(0).gameObject;
                    VerreSelected.GetComponent<MeshRenderer>().material.color = new Vector4(46f / 255f, 183f / 255f, 46f / 255f, 255f / 255f);
            }
            else
            {
                    curr = 0;
                    VerreSelected = J2.transform.GetChild(0).GetChild(0).gameObject;
                    VerreSelected.GetComponent<MeshRenderer>().material.color = new Vector4(46f / 255f, 183f / 255f, 46f / 255f, 255f / 255f);
            }
        }
        if (Tj != Notation.Tj1)
        {
            if (Notation.Tj1)
            {
                curr %= Mathf.Max(1, 10 - Notation.touch1);
                VerreSelected.GetComponent<MeshRenderer>().material.color = new Vector4(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
                VerreSelected = J1.transform.GetChild(curr).GetChild(0).gameObject;
                VerreSelected.GetComponent<MeshRenderer>().material.color = new Vector4(46f / 255f, 183f / 255f, 46f / 255f, 255f / 255f);
            }
            else
            {
                curr %= Mathf.Max(1, 10 - Notation.touch2);
                VerreSelected.GetComponent<MeshRenderer>().material.color = new Vector4(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
                VerreSelected = J2.transform.GetChild(curr).GetChild(0).gameObject;
                VerreSelected.GetComponent<MeshRenderer>().material.color = new Vector4(46f / 255f, 183f / 255f, 46f / 255f, 255f / 255f);
            }
        }
        Tj = Notation.Tj1;

        //Gestion des verres
        if ( Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChgmtVerre(1);
        }
        if ( Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChgmtVerre(-1);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            curr++;
            if (Notation.Tj1)
            {
                curr %= Mathf.Max(1, 10 - Notation.touch1);
                VerreSelected = J1.transform.GetChild(curr).GetChild(0).gameObject;
                VerreSelected.GetComponent<MeshRenderer>().material.color = new Vector4(46f / 255f, 183f / 255f, 46f / 255f, 255f / 255f);
            }
            else
            {
                curr %= Mathf.Max(1, 10 - Notation.touch2);
                VerreSelected = J2.transform.GetChild(curr).GetChild(0).gameObject;
                VerreSelected.GetComponent<MeshRenderer>().material.color = new Vector4(46f / 255f, 183f / 255f, 46f / 255f, 255f / 255f);
            }
        }

        //Miss
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (Notation.Tj1)
            {
                Notation.MissJ1();
            }
            else
            {
                Notation.MissJ2();
            }
        }

        //Gestion des modes
        if (Input.GetKeyDown(KeyCode.E)) { Notation.ChangeNorm(); }
        if (Input.GetKeyDown(KeyCode.R)) { Notation.ChangeReb(); }
        if (Input.GetKeyDown(KeyCode.T)) { Notation.ChangeRenv(); }
    }
}
