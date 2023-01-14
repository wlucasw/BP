using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Engagt : MonoBehaviour
{
    public TMP_Text J1;
    public TMP_Text J2;
    public TMP_Dropdown engagt;

    // Start is called before the first frame update
    void Start()
    {
        var dropdwown = engagt.transform.GetComponent<TMP_Dropdown>();

        dropdwown.options.Clear();
        
        List<string> items = new List<string>();

        items.Add("J1");
        items.Add("J2");

        foreach (var item in items)
        {
            dropdwown.options.Add(new TMP_Dropdown.OptionData() { text = item });
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (engagt.options[0].text != J1.text || engagt.options[1].text != J2.text)
        {
            /*var dropdwown = engagt.transform.GetComponent<TMP_Dropdown>();
            dropdwown.options.Clear();*/
            engagt.options[0].text = J1.text;
            engagt.options[1].text = J2.text;
            engagt.value += 1;
            engagt.value %= 2;
        }
    }
}
