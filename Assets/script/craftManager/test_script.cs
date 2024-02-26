using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class test_script : MonoBehaviour
{
    public Button item_operation_test_button;
    public Text item_operation_test_id_input;
    public Text item_operation_test_id_List;
    public Text item_operation_test_result;
    public inventory inventory_;
    public item_table item_tables;
    public craftManager craftManager;

    public void item_operation_test(){
        
        string[] item_list;
        string pre_data = item_operation_test_id_input.text;
        item_list = pre_data.Split(',');
        string debug_log_item_list = "입력 아이템 리스트\n";
        Dictionary<string,item_data> craft_item_datas = new Dictionary<string,item_data>();
        
        foreach(string inventory_id in  item_list){
            if (inventory_.inventory_container.ContainsKey(inventory_id)){
                string item_id = inventory_.inventory_container[inventory_id].item_ID;
                item_table_data  current_item_data = item_tables.item_data_table[item_id];
                debug_log_item_list +=  "\nID : " + inventory_id  
                                        + "=> 이름 : " + current_item_data.item_name;
                craft_item_datas[inventory_id] = inventory_.inventory_container[inventory_id];
            }
            else{
                Debug.Log(inventory_id+"는 인벤토리에 없는 아이템입니다.");
                foreach(string current_inventory_id in  inventory_.inventory_container.Keys)
                    Debug.Log("인벤토리에 있는 인벤토리 id : "+current_inventory_id);
            }
        }
        item_operation_data result = item_tables.item_main_operation(craft_item_datas);
        Debug.Log(debug_log_item_list);
        item_operation_test_id_List.text = debug_log_item_list;
        item_operation_test_result.text = result.log;
        // inventory_.
    }

    // Start is called before the first frame update
    void Start()
    {
        item_operation_test_button.onClick.AddListener(item_operation_test);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
