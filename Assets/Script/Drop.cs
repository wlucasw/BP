using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Drop : MonoBehaviour
{
    public TMP_Dropdown drop;
    public GameObject emptyBDD;
    ConnectBDD BDD;

    private void Start()
    {
        BDD = emptyBDD.GetComponent<ConnectBDD>();

        var dropdwown = drop.transform.GetComponent<TMP_Dropdown>();

        dropdwown.options.Clear();

        List<string> items = new List<string>();

        BDD.Readp(items);

        foreach(var item in items)
        {
            dropdwown.options.Add(new TMP_Dropdown.OptionData() { text = item });
        }
    }
}
