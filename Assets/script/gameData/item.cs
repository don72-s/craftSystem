using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class item :ScriptableObject{
    public string item_ID;
    public Dictionary<string, (int, int)> tagList;
    public int current_quality;
    public int rank;
    public int count;
    public string Production_date;

}
