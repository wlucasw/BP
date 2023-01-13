using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropJ : MonoBehaviour
{
    public TMP_Text TextBox;
    public TMP_Dropdown drop;
    public GameObject emptyBDD, Menu;
    ConnectBDD BDD;
    UpdateStats Stats;
    void DropdownItemSelected(TMP_Dropdown dropdown)
    {
        int index = dropdown.value;
        TextBox.text = dropdown.options[index].text;
        Stats.UpdateS(TextBox.text);

    }

    private void Start()
    {
        Stats = Menu.GetComponent<UpdateStats>();
        BDD = emptyBDD.GetComponent<ConnectBDD>();

        var dropdwown = drop.transform.GetComponent<TMP_Dropdown>();

        dropdwown.options.Clear();

        List<string> items = new List<string>();

        BDD.Readp(items);

        foreach (var item in items)
        {
            dropdwown.options.Add(new TMP_Dropdown.OptionData() { text = item });
        }

        DropdownItemSelected(dropdwown);

        dropdwown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdwown); });
    }
}
