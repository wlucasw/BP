using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camj2 : MonoBehaviour
{
    public Camera cmera;
    public GameObject BDDG;
    bool Tj1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Tj1 = BDDG.GetComponent<NoteScore>().Tj1;
        if (Tj1)
        {
            cmera.fieldOfView = 120f;
        }
        else
        {
            cmera.fieldOfView = 60f;
        }

    }
}
