using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이템 데이터를 저장할 스트럭처
public struct item_data {
    public string item_ID;
    public Dictionary<string, (int, int)> tagList;
    public int current_quality;
    public int rank;
    public int count;
    // public string produce_skill;
    public string Production_date;

}

public class inventory :MonoBehaviour
{

    



    public item_table item_Table_Data;
    // 실제 데이터를 저장할 컨테이너
    public Dictionary<string, item_data> inventory_container = new Dictionary<string, item_data>();
    /// <summary>
    /// 인벤토리 id생성 메서드<br/>
    /// </summary>
    /// <returns>랜덤한 인벤토리에 없는 ID반환</returns>
    //인벤토리 입력 메서드
    public string inventory_id_generator(){
         List<string> list_id = new List<string>();
        for(int i=100000 ; i<999999 ;  ++ i){ //사용가능한 유니크한 ID를 생성-정렬하는 로직
            string id = i.ToString();
            if (!inventory_container.ContainsKey(id))
                list_id.Add(id);
        }
        //위에서 생성한 Id중 랜덤으로 하나를 골라서 반환
        return list_id[Random.Range(0, list_id.Count)];
    }





    /// <summary>
    /// 인벤토리에 아이템을 생성하는 메서드. <br/>
    /// 랜덤아이디를 생성한 뒤 인벤토리에 추가. 
    /// </summary>
    /// <param name="data"> 추가할 아이템의 아이템 데이터 </param>
    /// <returns>수행 연산 데이터를 반환 => 디버그용</returns>
    public string inventory_producer(item_data data){
        string new_id = inventory_id_generator();//id생성
        inventory_container[new_id] = data;//인벤토리에 입력
        string item_name = item_Table_Data.item_data_table[data.item_ID].item_name;//디버그용
        string result = "아이템" + item_name + "을 인벤토리에 id" + new_id + "로 생성";
        Debug.Log(result);
        return result;//디버그용
    }




    /// <summary>
    /// 시스템 제공 아이템을 생성하여 전달하는 로직. <br/>
    /// 제공범위에 따라 랜덤하게 제공.
    /// </summary>
    /// <param name="id">제공할 아이템의 id를 입력값으로 받음</param>
    /// <returns>제공할 아이템의 데이터를 반환</returns>
    public Dictionary<string, (int, int)> System_produce_item_tags(string id){
        //반환할 아이템데이터를 미리 선언
        Dictionary<string, (int, int)> tag_data = new Dictionary<string,  (int,  int)>();
        //아이템 데이터 테이블에서 입력받은 id의 아이템 데이터를 받기.
        item_table_data item_data = item_Table_Data.item_data_table[id];
        //아이템 데이터 중 테그리스트에 대한 정보를 각 테그별로 확인.
        foreach (KeyValuePair<string, standard_tag_data> tag in item_data.standard_tag){
            string current_tag = tag.Key;//테그이름
            //테그 정보를 습득
            standard_tag_data current_tag_property = tag.Value;
            //테그의 개수 관련 데이터 임시 저장
            int standard_count = current_tag_property.tag_num.Item1;
            int standard_count_range = current_tag_property.tag_num.Item2;
            //테그의 계수 관련 데이터 임시 저장
            int standard_quality = current_tag_property.quality.Item1;
            int standard_quality_range = current_tag_property.quality.Item2;
            // 제공 범위에 따라 테그의 제공범위 정의
            (int, int) produce_tag_data=(standard_count + Random.Range(0, standard_count_range), 
                                        standard_quality + Random.Range(0, standard_quality_range));
            //각 테그별 제공 데이터를 테그리스트에 추가
            tag_data[current_tag]=produce_tag_data;
        }
        //추가 완료된 테그리스트를 반환.
        return tag_data;
    }






    /// <summary>
    /// 인벤토리 데이터 삭제로직.
    /// </summary>
    /// <param name="inventory_id">삭제할 아이템의 인벤토리 id</param>
    /// <param name="destroy_count">삭제할 아이템의 개수</param>
    /// <returns>삭제 여부를 확인을 위한 디버그 info</returns>
    public string inventory_item_data_destroyer(string inventory_id,  int destroy_count){
        //해당 아이템의 데이터를 획득
        item_data current_data = inventory_container[inventory_id];
        string item_table_id = current_data.item_ID;// 아이템 테이블 ID를 획득
        string result;//디버그 반환정보를 선언
        //해당 아이템이 아이템 테이블에 존재할 경우.
        if(item_Table_Data.item_data_table.ContainsKey(item_table_id)){
            //디버그 로그를 위한 아이템 이름 임시저장.
            string item_name = item_Table_Data.item_data_table[item_table_id].item_name;
            //삭제 개수 == 아이템 개수인 경우
            if(inventory_container[inventory_id].count==destroy_count){
                inventory_container.Remove(inventory_id);//인벤토리에서삭제
                //디버그 로그 추가.
                result =    "인벤토리 ID : " + inventory_id
                            + "\n아이템 이름 : " + item_name 
                            + "인 아이템이 "+ destroy_count + "개 삭제되었습니다.";
                Debug.Log(result);
                return result;//디버그 로그 반환
            }
            //삭제 개수 < 아이템 개수인 경우
            else if (inventory_container[inventory_id].count > destroy_count){
                current_data.count = current_data.count - destroy_count;//삭제 개수만큼 제거.
                inventory_container[inventory_id] = current_data;//제거한 정보로 수정.
                result = "인벤토리 ID : " + inventory_id  //디버그 로그
                            + "\n아이템 이름 : " + item_name + "인 아이템이 " + destroy_count + "개 삭제되었습니다.";
                Debug.Log(result);
                return result;//디버그 로그 반환
            }
            //삭제 개수 > 아이템 개수인 경우
            else {
                //오류 로그 반환
                result =    "인벤토리 ID : " + inventory_id 
                            + "\n아이템 이름 : " + item_name 
                            + "인 아이템이 " + destroy_count + "개 보다 적습니다.";
                Debug.Log(result);
                return result;//디버그 로그 반환
            }
        }
        //인벤토리에 없는 ID인 경우, 오류로그 반환
        else return "잘못된 접근입니다." + item_table_id + "는 아이템 테이블에 없는 Id입니다." + destroy_count + "개를 삭제하지 못했습니다.";
        
    }



    /// <summary>
    /// 인벤토리 인포 확인용 함수
    /// </summary>
    /// <returns>string 현재 인벤토리 정보를 화면에 출력.</returns>
    public string inventory_tester(){
        string log = "인벤토리 데이터 리스트\n";
        
        
        foreach(KeyValuePair<string, item_data> current_item in inventory_container){
            string inventory_id = current_item.Key;
            item_data item_Data = current_item.Value;
            string item_table_id = item_Data.item_ID;
            Debug.Log(item_Data.item_ID);
            if (item_Table_Data.item_data_table.ContainsKey(item_table_id)){
                log +=  "\n======================"
                        + "\n아이템의 인벤토리 id : " + inventory_id
                        + "\n아이템의 아이템 테이블 id : " + item_table_id
                        + "\n아이템의 이름 : " + item_Table_Data.item_data_table[item_table_id].item_name
                        + "\n현재 품질 : " + item_Data.current_quality
                        + "\n현재 등급 : " + item_Data.rank
                        + "\n생산일 : " + item_Data.Production_date
                        + "\n현재 개수 : " + item_Data.count
                        + "\n테그리스트 :";
                foreach(KeyValuePair<string, (int, int)> tag_data in item_Data.tagList){
                    log += "\n" + tag_data.Key + "|" + tag_data.Value.Item1 + "|" + tag_data.Value.Item1;
                }
            }
            else{
                log += item_Data.item_ID + "는 잘못된 아이디 입니다\n";
            }
        }
        return log;
    }


    
    /// <summary>
    /// 임시 실험용 인벤토리 데이터 추가용 함수. 
    /// 실험용 인벤토리 딕셔너리를 반환. 
    /// </summary>
    /// <returns>Dictionary(string, item_data)형태의 임시 인벤토리를 반환</returns>
    public Dictionary<string, item_data> test_inventory()
    {
        inventory_container["1111"] = new item_data{
            item_ID = "test",
            tagList = new Dictionary<string, (int, int)>{
                    {"냥", (1, 2)},
                    {"ㅁㄴㅇㅁㄴㅇㅁ", (2, 5)},
                    {"허브ㅇㅇㅇ", (1, 6)},
                    {"갸루", (1, 7)}
            },
            current_quality = 50,
            rank = 2,
            count = 1,
            Production_date = "222.07.14.01.00"
        };
        inventory_container["2224"] = new item_data{
            item_ID = "00001",
            tagList = new Dictionary<string, (int, int)>{
                {"헌신", (1, 3)},
                {"평화", (1, 6)},
                {"허브", (1, 6)},
                {"가루", (1, 6)}
            },
            current_quality = 50,
            rank = 2,
            count = 3,
            Production_date = "222.07.14.02.00"
        };
        inventory_container["2225"] = new item_data{//장미꽃
            item_ID = "00002",
            tagList = new Dictionary<string, (int, int)>{
                {"사랑", (1, 5)},
                {"순수", (1, 5)},
                {"허브", (1, 5)}
            },
            current_quality = 15,
            rank = 1,
            count = 1,
            Production_date = "222.07.14.02.00"
        };
        inventory_container["2255"] = new item_data{//로즈마리 잎
            item_ID = "00003",
            tagList = new Dictionary<string, (int, int)>{
                {"고요", (1, 1)},
                {"허브", (1, 1)}
            },
            current_quality = 3,
            rank = 1,
            count = 1,
            Production_date = "222.07.14.02.00"
        };
        inventory_container["12415"] = new item_data{//물
            item_ID = "00004",
            tagList = new Dictionary<string, (int, int)>{
                {"생명", (1, 7)},
                {"유동", (1, 7)},
                {"비정형", (1, 7)}
            },
            current_quality = 14,
            rank = 1,
            count = 1,
            Production_date = "222.07.14.02.00"
        };
        return inventory_container;
        
    }

    
}

                
       