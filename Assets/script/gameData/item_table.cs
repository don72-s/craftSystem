using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Packages.Rider.Editor.UnitTesting;
using UnityEngine;
// public enum item_tag_data{
//     힘,  안정,  오일,  유동,  헌신,  평화, 
//     회복,  순수,  고요,  기쁨,  인내,  사랑, 
//     금속,  변화,  불안정,  죽음, 
//     성스러움,  정화,  번영,  박막, 
//     보호,  아름다움,  형상_향로
// }


/// <summary>
/// 아이템의 기초데이터 struct
/// item_operation_List : T = >Dictionary(string, Dictionary(string, (string, int, int)))
/// = >(아이템규칙,  (테그 이름,  (사용규칙 키워드,  규칙 개수,  규칙 계수)))<br/>
/// img_src : T = > string || 이미지 주소.
/// item_name : T = > string || 아이템 이름
/// summary : T = > string || 아이템 설명
/// </summary>
public struct item_table_data{
    /// <summary>
    /// 아이템 규칙 리스트를 입력받는 부분. <br/>
    /// T = >Dictionary(string, Dictionary(string, (string, int, int)))<br/>
    /// = >(아이템규칙,  (테그 이름,  (사용규칙 키워드,  규칙 개수,  규칙 계수)))<br/>
    /// </summary>
    public Dictionary<string, (int, Dictionary<string, (string, int, int)>)> item_operation_data;
    public string Production_way;
    public Dictionary<string,  standard_tag_data> standard_tag;
    //시스템 개수 
    public bool can_destroy;
    public string img_src;//일단 src,  object로 변경될 가능성 존재.     // 스프라이트 컨테이너.
    public string item_name;//아이템 이름 컨테이너
    public string summary;// 아이템의 설명을 입력하는 구간,  임시로 string으로 구현해둠. 

}


public struct standard_tag_data{
    /// <summary>
    /// Item1 = > 기본 개수
    /// Item2 = > 제공 범위
    /// </summary>
    public (int, int) tag_num;
    /// <summary>
    /// Item1 = > 기본 개수
    /// Item2 = > 제공 범위
    /// </summary>
    public (int, int) quality;
}


public struct item_operation_data{
    public Dictionary<string, (int, List<int>)> item_data_container;
    public List<(string, int, Dictionary<string, (string, int, int)>)> item_operation_datas;
    public string log;
}
// item_operation_data.item_data_container = item_data_container;
/// <summary>
/// 아이템 오퍼레이션 클레스. 
/// 아이템의 사용규칙들을 모두 들고 있는 컨테이너. 
/// </summary>
public class item_operation_List{
    //아이템 규칙 델리게이트 선언
    public delegate item_operation_data
                        item_operation_delegate(
                                            item_operation_data item_data_container, 
                                            Dictionary<string, (string, int, int)> item_operation_datas
                                            );
    // 아이템  규칙 델리게이트 딕셔너리. 

    
    //아이템 규칙 입력 및 출력 로직.
    /// <summary>
    /// NAN_ = > 기본연산,  연산없음<br/>
    /// destroy = > 삭제 연산. 인자 : "match"(일치)|"contain"포함|"except"빼고 모두<br/>
    /// </summary>
    /// <returns>Dictionary(string, item_operation_delegate) 아이템 규칙 업데이트</returns>
    public Dictionary<string, item_operation_delegate>
        item_operations(){// 추가 로직은 아래,  딕셔너리에 입력
            Dictionary<string, item_operation_delegate> item_operation_Dict
                = new Dictionary<string, item_operation_delegate>();
            item_operation_Dict[nameof(NAN)] = NAN;
            item_operation_Dict[nameof(destroy)] = destroy;
            return item_operation_Dict;
        }

  
  

    public item_operation_data
            item_operation(item_data item_Data, item_operation_data item_data_container){
                Dictionary<string, item_operation_delegate> item_operation_Dict = item_operations();
                item_data_container.log += "\n아이템 연산 시작";
                Dictionary<string, (int, int)> item_tags = item_Data.tagList;
                // 현재 아이템 입력 연산
                foreach(KeyValuePair<string,  (int,  int)> current_tag in item_tags){
                    string tag_name = current_tag.Key;
                    List<int> quality_List;
                    int num = current_tag.Value.Item1;
                    int quality = current_tag.Value.Item2;
                    item_data_container.log += "\n현재 입력 테그|개수|계수 : " + tag_name + "|" + num + "|" + quality;
                    if (!item_data_container.item_data_container.ContainsKey(tag_name)){
                        quality_List = new List<int>(){current_tag.Value.Item2};
                        item_data_container.item_data_container[tag_name] = (num, quality_List);
                    }
                    else{
                        int current_num = item_data_container.item_data_container[tag_name].Item1;
                        quality_List = item_data_container.item_data_container[tag_name].Item2;
                        quality_List.Add(current_tag.Value.Item2);
                        item_data_container.item_data_container[tag_name] = (current_num + num, quality_List);
                    }
                }
                int item_operation_datas_index = 0;//
                item_data_container.log += "\n이번 연산의 아이템규칙 적용 시작";
                List<(string, int, Dictionary<string, (string, int, int)>)> operation_life_data = new List<(string, int, Dictionary<string, (string, int, int)>)>(item_data_container.item_operation_datas);
                //아이템 규칙 연산
                foreach(var operation_data in operation_life_data){
                    
                    string current_item_operation = operation_data.Item1;
                    int operation_life = operation_data.Item2;
                    item_data_container.log += "\n적용 아이템 규칙 : " + current_item_operation
                                                + "\n현재 아이템 규칙의 수명 : " + operation_life;
                    if(operation_life != 0 && item_operation_Dict.ContainsKey(current_item_operation)){
                        Dictionary<string, (string, int, int)> item_operation_conditions = operation_data.Item3;
                        item_data_container = item_operation_Dict[current_item_operation](item_data_container, item_operation_conditions);
                        item_data_container.item_operation_datas[item_operation_datas_index] = (current_item_operation, operation_life - 1, item_operation_conditions);
                    }
                    else{
                        if(item_operation_Dict.ContainsKey(current_item_operation)){
                            item_data_container.log += "\n 아이템 규칙 " + current_item_operation + "는 존재하지 않는 연산입니다.";
                            Debug.Log("\n 아이템 규칙 " + current_item_operation + "는 존재하지 않는 연산입니다.");
                        }
                        else if (operation_life == 0){
                            item_data_container.log += "\n 아이템 규칙 " + current_item_operation + "는 연산수명이 만료되었습니다.";
                            Debug.Log("\n 아이템 규칙 " + current_item_operation + "는 연산수명이 만료되었습니다.");
                        }
                    }
                    item_operation_datas_index ++ ;
                }
                return item_data_container;
            }

    //아이템 연산 규칙틀을 정의.
    public  item_operation_data
            NAN(item_operation_data item_data_container, 
                Dictionary<string, (string, int, int)> item_operation_datas){
                    
                    item_data_container.log += "\n 적용규칙이 없어 그대로 반환";
            return item_data_container;
            }

    public  item_operation_data
            destroy(item_operation_data item_data_container, 
                    Dictionary<string, (string, int, int)> item_operation_datas){
                        item_data_container.log += "\n아이템 파괴";
                foreach (KeyValuePair<string, (string, int, int)> current_operation_data in  item_operation_datas){
                    string condition = current_operation_data.Key;
                    (string, int, int) tag_condition = current_operation_data.Value;
                    string current_tag = tag_condition.Item1;

                    if(condition == "match"){
                        if(item_data_container.item_data_container.ContainsKey(current_tag)){
                            int tag_count = item_data_container.item_data_container[current_tag].Item1 - tag_condition.Item2;
                            item_data_container.item_data_container[current_tag].Item2.Add( - tag_condition.Item3);//테그에 대한 값 페널티.
                            item_data_container.log += "\n 파괴조건, 일치 테그";
                            item_data_container.log += "\n==>" + current_tag + "   " + tag_condition.Item2 + "개 파괴";
                            item_data_container.log += "\n==>" + current_tag + "   " + - tag_condition.Item3 + "만큼 계수 페널티";
                        }
                        
                    }
                    else if (condition == "contain"){
                        foreach (string tag in item_data_container.item_data_container.Keys){
                            if(tag.Contains(current_tag)){
                                int tag_count = item_data_container.item_data_container[tag].Item1 - tag_condition.Item2;
                                item_data_container.item_data_container[tag].Item2.Add( - tag_condition.Item3);//테그에 대한 값 페널티.
                                item_data_container.log += "\n 파괴조건, 포함 테그";
                                item_data_container.log += "\n ==>" + current_tag + "   " + tag_condition.Item2 + "개 파괴";
                                item_data_container.log += "\n ==>" + current_tag + "   " + - tag_condition.Item3 + "만큼 계수 페널티";
                            }
                        }
                    }
                    else if (condition == "except"){
                        if(item_data_container.item_data_container.ContainsKey(current_tag)){
                            (int, List<int>) values = item_data_container.item_data_container[current_tag];
                            item_data_container.item_data_container.Clear();//값 전부 삭제.
                            item_data_container.item_data_container[current_tag] = values;//찾은 값만 재할당
                            item_data_container.log += "\n 파괴조건, 핻당테그 외 전부";
                            item_data_container.log += "\n ==>" + current_tag + "외 모드 테그 삭제";
                        }
                    }

                }
                return item_data_container;
            }
}



public class item_table : MonoBehaviour
{   // 이름,  아이디,  설명...... 들어있는거 넣는다. 

    public Dictionary <string, item_table_data> item_data_table;

    public Dictionary <string, item_table_data> init(){
        int test_range = 0;
        item_data_table = new Dictionary<string,  item_table_data>(){
            {"test", new item_table_data{
                    item_operation_data 
                    = new Dictionary<string, (int, Dictionary<string, (string, int, int)>)>(){
                        {"destroy", 
                            (8,  new Dictionary<string, (string, int, int)>(){
                                    {"match", ("갈망", 1, 15)}, 
                                }
                            )
                        }
                    }, 
                
                can_destroy = false, 
                Production_way = "admin", 
                standard_tag = new Dictionary<string,  standard_tag_data> {
                    {"냥", 
                        new standard_tag_data{
                            tag_num = (1, 10), //1,  1~10중 랜덤
                            quality = (2, 10) //2,  2~10중 랜덤
                        }
                    }, 
                    {"ㅁㄴㅇㅁㄴㅇㅁ", 
                        new standard_tag_data{
                            tag_num = (1, 10), //1,  1~10중 랜덤
                            quality = (2, 10) //2,  2~10중 랜덤
                        }
                    }, 
                    {"허브ㅇㅇㅇ", 
                        new standard_tag_data{
                            tag_num = (1, 10), //1,  1~10중 랜덤
                            quality = (2, 10) //2,  2~10중 랜덤
                        }
                    }, 
                    {"갸루", 
                        new standard_tag_data{
                            tag_num = (1, 10), //1,  1~10중 랜덤
                            quality = (2, 10) //2,  2~10중 랜덤
                        }
                    }
                }, 
                img_src = "ㄴㅇㅁㄴㅁㅇㅁㄴㅇㅁ", //일단 src,  object로 변경될 가능성 존재.     // 스프라이트 컨테이너.
                item_name = "test", //아이템 이름 컨테이너
                summary = "test,  야옹이는 귀여워서 야옹야옹"}
            }, 
            {"00001", new item_table_data{
                item_name = "샌달우드 가루", //아이템 이름 컨테이너
                item_operation_data 
                = new Dictionary<string, (int, Dictionary<string, (string, int, int)>)>(){
                    {"NAN", 
                        (8,  new Dictionary<string, (string, int, int)>(){// operation이 없는경우 입력 X
                            }
                        )
                    }
                }, 
                can_destroy = true, 
                Production_way = "system", 
                standard_tag = new Dictionary<string,  standard_tag_data> {
                    {"헌신", 
                        new standard_tag_data{
                            tag_num = (1, test_range), //1,  1~10중 랜덤
                            quality = (2, test_range) //2,  2~10중 랜덤
                        }
                    }, 
                    {"평화", 
                        new standard_tag_data{
                            tag_num = (1, test_range), //1,  1~10중 랜덤
                            quality = (2, test_range) //2,  2~10중 랜덤
                        }
                    }, 
                    {"허브", 
                        new standard_tag_data{
                            tag_num = (1, test_range), //1,  1~10중 랜덤
                            quality = (2, test_range) //2,  2~10중 랜덤
                        }
                    }, 
                    {"가루", 
                        new standard_tag_data{
                            tag_num = (1, test_range), //1,  1~10중 랜덤
                            quality = (2, test_range) //2,  2~10중 랜덤
                        }
                    }
                }, 
                img_src = "샌달우드 가루 아이템 경로", //일단 src,  object로 변경될 가능성 존재.     // 스프라이트 컨테이너.
                summary = "백단향나무를 말려서 빻아 만든 가루. 샌달우드라고도 부르는데,  다 자라는데 60년 이상 걸린다고 한다. 부드럽고 그윽한 향이 난다."}
                }, 
            {"00002", new item_table_data{
                item_name = "장미꽃", //아이템 이름 컨테이너
                item_operation_data 
                = new Dictionary<string, (int, Dictionary<string, (string, int, int)>)>(){
                    {"NAN", 
                        (8,  new Dictionary<string, (string, int, int)>(){// operation이 없는경우 입력 X
                            }
                        )
                    }
                }, 
            can_destroy = true, 
            Production_way = "system", 
            standard_tag = new Dictionary<string,  standard_tag_data> {
                {"사랑", 
                    new standard_tag_data{
                        tag_num = (1, 1), //1,  1~10중 랜덤
                        quality = (5, test_range) //2,  2~10중 랜덤
                    }
                }, 
                {"순수", 
                    new standard_tag_data{
                        tag_num = (1, 1), //1,  1~10중 랜덤
                        quality = (5, test_range) //2,  2~10중 랜덤
                    }
                }, 
                {"허브", 
                    new standard_tag_data{
                        tag_num = (1, 1), //1,  1~10중 랜덤
                        quality = (5, test_range) //2,  2~10중 랜덤
                    }
                }
            }, 
            img_src = "장미꽃 아이템 경로", //일단 src,  object로 변경될 가능성 존재. 
            summary = "아저씨는 거짓말쟁이에요. 꽃들은 연약하고 순수해요. 가시가 있으면 무섭게 보인다고 생각하는 거예요."}
            }, 
            {"00003", new item_table_data{
                item_name = "로즈마리 잎", //아이템 이름 컨테이너
                item_operation_data 
                = new Dictionary<string, (int, Dictionary<string, (string, int, int)>)>(){
                    {"NAN", 
                        (8,  new Dictionary<string, (string, int, int)>(){// operation이 없는경우 입력 X
                            }
                        )
                    }
                }, 
            can_destroy = true, 
            Production_way = "system", 
            standard_tag = new Dictionary<string,  standard_tag_data> {
                {"고요", 
                    new standard_tag_data{
                        tag_num = (1, 1), //1,  1~10중 랜덤
                        quality = (1, test_range) //2,  2~10중 랜덤
                    }
                }, 
                {"허브", 
                    new standard_tag_data{
                        tag_num = (1, 1), //1,  1~10중 랜덤
                        quality = (1, test_range) //2,  2~10중 랜덤
                    }
                }
            }, 
            img_src = "로즈마리 잎 아이템 경로", //일단 src,  object로 변경될 가능성 존재. 
            summary = "“요리에 활용되는 로즈마리는 향긋한 맛을 선사하며,  그 언제나 상쾌한 향은 마음을 싱그럽게 한다.”"}
            }, 
            {"00004", new item_table_data{
                    item_name = "물", //아이템 이름 컨테이너
                    item_operation_data 
                    = new Dictionary<string, (int, Dictionary<string, (string, int, int)>)>(){
                        {"NAN", 
                            (8,  new Dictionary<string, (string, int, int)>(){// operation이 없는경우 입력 X
                                }
                            )
                        }
                    }, 
                can_destroy = true, 
                Production_way = "system", 
                standard_tag = new Dictionary<string,  standard_tag_data> {
                    {"생명", 
                        new standard_tag_data{
                            tag_num = (1, 1), //1,  1~10중 랜덤
                            quality = (7, test_range) //2,  2~10중 랜덤
                        }
                    }, 
                    {"유동", 
                        new standard_tag_data{
                            tag_num = (1, 1), //1,  1~10중 랜덤
                            quality = (7, test_range) //2,  2~10중 랜덤
                        }
                    }, 
                    {"비정형", 
                        new standard_tag_data{
                            tag_num = (1, 1), //1,  1~10중 랜덤
                            quality = (7, test_range) //2,  2~10중 랜덤
                        }
                    }
                }, 
                img_src = "물 아이템 경로", //일단 src,  object로 변경될 가능성 존재. 
                summary = "“푸르고 투명하며 생명의 근원이자 주변에 널렸으면서도 깨끗한 걸 구하기는 의외로 힘든 것.”"}
                }
        } ;
        return item_data_table;
    }

    public string debug_item_tag_log(Dictionary<string, standard_tag_data> tag_datas, 
                                    List<string> keys
                                    ){
            string log = "";
            foreach(string current_data in tag_datas.Keys){
                // Debug.Log("기본테그 로그 입력 시작2");
                string tag_name = current_data;
                standard_tag_data tag_data = tag_datas[tag_name];
                // Debug.Log("기본테그 로그 입력 중");
                log += "테그 이름 : " + tag_name + "\n"
                        + "기준개수|범위 : " + tag_data.tag_num.Item1 + "|" + tag_data.tag_num.Item2
                        + "기준계수|범위 : " + tag_data.quality.Item1 + "|" + tag_data.quality.Item2;
            }

        return log;
    }
    public string debug_operation_log(Dictionary<string, (int, Dictionary<string, (string, int, int)>)>item_operation_data
                                    ){
        string log = "";
        foreach(KeyValuePair<string, (int, Dictionary<string, (string, int, int)>)> current_data in item_operation_data){
                string operation_name = current_data.Key;
                int life = current_data.Value.Item1;
                Dictionary<string, (string, int, int)> operation_condition = current_data.Value.Item2;
                // Debug.Log("아이템 규칙 로그 입력");
                log += "현재 아이템 규칙" + operation_name + "\n"
                        + "규칙 조건 리스트\n";
                    
                // Debug.Log("아이템 규칙 조건 입력 시작");
                foreach(KeyValuePair<string, (string, int, int)> current_condition in operation_condition){
                    string condition_name = current_condition.Key;
                    string tag_name_ = current_condition.Value.Item1;
                    int tag_num = current_condition.Value.Item2;
                    int tag_quality = current_condition.Value.Item3;
                    // Debug.Log("아이템 규칙 조건 입력");
                    log += condition_name + " : " + tag_name_ + "|" + tag_num + "|" + tag_quality + "\n";
                }
            }
            return log;
    }
    public string item_table_data_tester(Dictionary<string,  item_table_data> item_tables){
        string log = "아이템 테이블 로그\n";
        
        foreach(KeyValuePair<string,  item_table_data> current_item in  item_tables){
            string item_id = current_item.Key;

            item_table_data item_data = current_item.Value;
            // Debug.Log("아이템 별로 체크중,  로그 입력");
            log += " ------------------ \n";
            log += "아이템 아이디 : " + item_id + '\n'
                    + "아이템 이름 : " + item_data.item_name + "\n"
                    + "삭제여부  : " + item_data.can_destroy + "\n"
                    + "입수경로 : " + item_data.Production_way + "\n"
                    + "아이템 설명 : " + item_data.summary + "\n"
                    + "기본테그 :    \n";
            // Debug.Log("기본테그 로그 입력 시작");
            Dictionary<string, standard_tag_data> tag_datas = item_data.standard_tag;
            log += debug_item_tag_log(tag_datas, new List<string>(tag_datas.Keys)); 
            
            // Debug.Log("아이템 규칙 로그 입력 시작");  
                 
            Dictionary<string, (int, Dictionary<string, (string, int, int)>)>  item_operation_data = item_data.item_operation_data;
            log += "아이템규칙 리스트 \n";
            log += debug_operation_log(item_operation_data);
            log += " ------------------- \n";

        }
        // Debug.Log(log);
        return log;
    }
    
    
    
    
    public item_operation_data item_main_operation(Dictionary<string, item_data> item_list){
        var item_operation_list = new item_operation_List();
        Dictionary<string, (int, List<int>)> item_data_container = new Dictionary<string, (int, List<int>)>();
        item_operation_data item_Operation_Data = new item_operation_data();
        item_Operation_Data.item_data_container = item_data_container;
        item_Operation_Data.item_operation_datas = new List<(string, int, Dictionary<string, (string, int, int)>)>();
        //현재 아이템,  연산데이터 페어를 입력으로 준다. 
        //여기서의 string은 인벤토리상의 id이다. 
        foreach (KeyValuePair<string, item_data> current_item in item_list){
            string inventory_id = current_item.Key; // 아이템의 인벤토리 아이디./
            item_data current_item_data = current_item.Value;// 현재 아이템의 아이템 데이터. 
            string item_id = current_item_data.item_ID;//item_table상의 id,  item_table에서 해당 아이템 검색에 사용. 
            item_Operation_Data.log += "\n현재 입력중인 아이템\n item_id" + item_id
                            + "\n아이템 이름 : " + item_data_table[item_id].item_name;
            Dictionary<string,  (int,  int)>  item_tags = current_item_data.tagList;
            foreach(KeyValuePair<string, (int, Dictionary<string, (string, int, int)>)> current_item_operation_data 
                                    in  item_data_table[item_id].item_operation_data){
                    item_Operation_Data.log +=  "\n 적용 연산규칙 : " + current_item_operation_data.Key
                                        + "\n 연산규칙 수명 : " +  current_item_operation_data.Value.Item1;
                    item_Operation_Data.item_operation_datas.Add((current_item_operation_data.Key, //오퍼레이션 이름
                                            current_item_operation_data.Value.Item1, //오퍼레이션 수명
                                            current_item_operation_data.Value.Item2//오퍼레이션 condition Dict
                                            ));
            }
            
            item_Operation_Data = item_operation_list.item_operation(current_item_data, item_Operation_Data);//매 아이템,  입력마다 오퍼레이션을 호출.
        }


        return item_Operation_Data;
    }
    void Start() {
        item_data_table = init();
    }

    // Start is called before the first frame update
  
}



