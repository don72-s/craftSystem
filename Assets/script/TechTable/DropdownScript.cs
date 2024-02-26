using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownScript : MonoBehaviour
{

    public Dropdown dropdown;

    public int curIdx = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (TechType _tech in System.Enum.GetValues(typeof(TechType))) { 
            dropdown.options.Add(new Dropdown.OptionData(_tech.ToString()));
        }

        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);


    }

    private void OnDropdownValueChanged(int idx)
    {
        curIdx = idx;
    }

    public TechType getDropdownTechtype() {

        return (TechType)curIdx;

    }

}
