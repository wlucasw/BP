using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rename : MonoBehaviour
{
    public GameObject j;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform v in j.transform.GetComponentInChildren<Transform>())
        {
            string name = j.name + "_" + v.gameObject.name;
            v.gameObject.name = name;
            v.gameObject.transform.GetChild(0).name = name;
        }
    }


}
