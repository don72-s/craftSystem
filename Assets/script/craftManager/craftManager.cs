using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using static recipe;

struct item_operation_datas{
    public Dictionary<string, item_data> item_data_list;
    // public Dictionary<>
}
public class craftManager : MonoBehaviour
{
    [SerializeField]
    public Text  test_item_recipe;
    public Text  test_item_operation_log;
    public Text  test_recipe_table;
    public Text  test_item_table;
    public Text  test_inventory;
    public InputField  id_input;
    public Button test_button;
    public Button destroy_button;
    public InputField destroy_id;
    public Text  destroy_tweet;
    public Button produce_button;
    public InputField produce_id;
    public Text  produce_tweet;
    public Button craft_button;
    public inventory inventory_;
    public item_table item_Table;
    public recipe Recipe;
  
    
    public Dictionary<string, item_data> inventory_container;
    public Dictionary<string, item_table_data> item_table_data;
    // public item_table Item_Table;
    // inventory.inventory_container
    // 인벤토리 정보 확인용 컨테이너. 

    /// <summary>
    /// 테스트 실험 함수.
    /// 테스트 버튼을 누를 경우 임시로 연산결과를 출력함.
    /// </summary>
    public void test_recipe_operation(){
        // var item_criteria=Recipe.item_criteria;
        Dictionary<string, (int, int)> test_container = new Dictionary<string, (int,int)> (){
            {"생명",(2, 2)},
            {"갈망",(1, 10)},
            // {"죽음",(1,10)}   
        };
        Dictionary<string,  item_data> test_origin_item_datas = new Dictionary<string,  item_data>(){
            {"1111", new item_data{
                            item_ID = "0001", 
                            tagList = new Dictionary<string, (int, int)>
                                {
                                    {"생명", (2, 3)}
                                }, 
                            current_quality = 147, 
                            rank = 5, 
                            count = 3, 
                            Production_date = "222.07.14.01.00"

                        }
            }, 
            {"2224", new item_data{
                            item_ID = "0002", 
                            tagList = new Dictionary<string, (int, int)>{
                                {"생명", (2, 3)}, 
                                {"갈망", (1, 6)}
                            }, 
                            current_quality = 149, 
                            rank = 5, 
                            count = 3, 
                            Production_date = "222.07.14.02.00"
                        }
            }
        };
        string test_id = id_input.text;
        Debug.Log(test_id);
        if(test_id != ""){
            //test input값을,  대입하는 영역,  현재는 아이템 id로 테스트를 진행.
            //원래는 기준조건 충족 로직으로,  불충족 요건을 거르기 때문에, 
            //입력 아이템 컨테이너에서의 값 조전으로 실해여부 판단을 해야하는 상황.
            
            ///<summary>
            /// {"item_id", item_id} = >아이템아이디<br/>
            /// {"quality", quality}, {"rank", quality} = >, <br/>
            /// {"num_of_item", quality}, <br/>
            /// {"item_condition", item_conditions}, <br/>
            /// {"craft_date", DateTime.Now.ToString("yyy-MM-dd-HH-mm-ss")}, //임시 타임스템프<br/>
            /// {"log", recipe_operation_Debug_log}//로그<br/>
            ///</summary>
            result_item_data recipe_test_result = Recipe.recipe_operation(test_id, test_container, test_origin_item_datas);
            string recipe = Recipe.item_item_criterion_parser(test_id);
            
            
            string log = recipe_test_result.log;
            string test_recipe_table_string = Recipe.test_item_criteria();
            test_item_operation_log.text = log;//.Replace("\\n",  "\n")
            test_item_recipe.text = recipe;
            test_recipe_table.text = test_recipe_table_string;
            //인벤토리 지급 함수를 넣어서 추가 체크 필요
        }
        else
            Debug.Log("입력이 없어 레시피 테스트 수행이 안됩니다.");
    }
    //
    //테스트를 위한 임시 아이템 테이블
    Dictionary<string,  item_table_data> item_data_table;
    //아이템 테이블 작동여부 디버그 함수
    public void item_table_test(){
        
        Debug.Log("현재 값" + item_Table.item_table_data_tester(item_data_table));
       
        test_item_table.text = item_Table.item_table_data_tester(item_data_table);
        
    }
    //인벤토리 컨테이너 작동여부 디버그 함수
    public void inventory_container_table_test(){

        test_inventory.text = inventory_.inventory_tester();
    }
    // 인벤토리 아이템 지급 테스트
    public void test_produce(){
        string item_id = produce_id.text;
        // inventory_.inventory_item_generator();
        if(item_id != ""){
            item_data current_item_data = new item_data
            {
                count = 1, 
                current_quality = 14, 
                item_ID = item_id, 
                Production_date = System.DateTime.Today.ToString("yyy-MM-dd-HH-mm-ss"), 
                rank = 1, 
                tagList = inventory_.System_produce_item_tags(item_id)
            };

            // inventory_.inventory_producer(current_item_data);
            produce_tweet.text = inventory_.inventory_producer(current_item_data);// 값 수정ㄴ
            inventory_container_table_test();// 인벤토리 뷰어 정보 갱신
        }
        else Debug.Log("입력이 없습니다.");
    }

    public void test_destroy(){
        string inventory_id = destroy_id.text.Split(',')[0];
        Debug.Log(inventory_id + "|" + destroy_id.text);
        int destroy_num = Convert.ToInt32(destroy_id.text.Split(',')[1]);
        if(inventory_id != ""){
            
            destroy_tweet.text = inventory_.inventory_item_data_destroyer(inventory_id, destroy_num);
        }
        else  destroy_tweet.text = "입력이 없습니다.";
        
        inventory_container_table_test();
    }
 
    
    
    // 실행버튼을 기반으로 시작하는 로직.
    // 아이템데이터를 입력받는 함수를 실행하는 로직
    // 기술연산을 시행하는 로직.
    //조합 판별을 시행하는 로직.
    //결과물을 출력하고 인벤토리에 추가하는 로직. 
    
    // Start is called before the first frame update
    void Start()
    {   
        inventory_container = inventory_.test_inventory();
        inventory_.inventory_container = inventory_.test_inventory();
        item_data_table = item_Table.init();

        // test_inventory = GameObject.Find("test_inventory_data");
        // Recipe = this.gameObject.GetComponent<recipe>();
        test_button.onClick.AddListener(test_recipe_operation);
        test_button.onClick.AddListener(item_table_test);
        test_button.onClick.AddListener(inventory_container_table_test);
        destroy_button.onClick.AddListener(test_destroy);
        produce_button.onClick.AddListener(test_produce);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
