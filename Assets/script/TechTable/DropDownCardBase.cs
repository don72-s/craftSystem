using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownCardBase : MonoBehaviour
{
    public Dropdown dropdown;

    public int curIdx = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (System_Provid_ItemID _tech in System.Enum.GetValues(typeof(System_Provid_ItemID)))
        {
            dropdown.options.Add(new Dropdown.OptionData(_tech.ToString()));
        }

        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);


    }

    private void OnDropdownValueChanged(int idx)
    {
        curIdx = idx;
    }

    public System_Provid_ItemID getDropdownTechtype()
    {

        return (System_Provid_ItemID)curIdx;

    }
}
