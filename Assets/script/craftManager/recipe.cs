using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 아이템 제작에 필요한 조건.<br/>
/// name  = > T : string ||아이템 이름<br/>
/// standard_conditions  = > T : Dictionary(string, (string, int))|| 기준 조건 리스트<br/>
/// operations  = > T : string ||연산 로직 종류<br/>
/// standard_quality = > T : int || 기준 품질 <br/>
/// </summary>
public struct item_criterion{
    /// <summary>
    /// name  = > T : string ||아이템 이름
    /// </summary>
    public string name;
    /// <summary>
    /// standard_conditions  = > T : Dictionary(string, (string, int))|| 기준 조건 리스트<br/>
    /// Dictionary(string, (string, int)) = > Dictionary(테그, (연산조건, 개수))
    /// 연산조건  =  ex). "match"일치를 의미. 추후 조건리스트를 추가. 
    /// bool check_standard_conditions()에 동일한 함수명으로 연산로직 추가.
    /// </summary>
    public Dictionary<string,  (string, int)> standard_conditions;
    /// <summary>
    /// operations  = > T : string ||연산 로직 종류
    /// operations class에 동명 함수 등록 후 사용. 
    /// </summary>
    public string operations;
    /// <summary>
    /// standard_quality  = > T : int || 기준 품질
    /// </summary>
    public (int, int) standard_quality;
    /// <summary>
    /// T : string || 등급 연산로직 종류
    /// rank_operations class 에 동명 함수 등록 후 사용.
    /// </summary>
    public string rank_operation;
    /// <summary>
    /// T : string ||개수 연산 로직 종류
    /// count_operation class에 동명 함수 등록 후 사용
    /// </summary>
    public string count_operation;
}
/// <summary>
/// 품질 연산 규칙 클래스들.
/// </summary>
public class quality_operations{
    // 연산함수 셀렉터
    // 아이템 데이터 리스트 쓰고싶음.
    // 확장 가능성 필요한가?  = > 염두에 둔 구조르 짜봄. 다만,  좆되도 ㄱㅊ
    public delegate int quality_operation_delegate(
                                        Dictionary<string, (string, int)> standard_conditions,
                                        Dictionary<string, (int, int)> item_conditions,
                                        Dictionary<string, item_data> origin_item_datas,
                                        bool debug_log_Use = false
                                            );
    
    public Dictionary<string,  quality_operation_delegate> quality_operation_list(){
        Dictionary<string,  quality_operation_delegate> quality_operation_Dict
                     = new Dictionary<string,  quality_operation_delegate>();
        quality_operation_Dict["harmony"] = harmony;
        quality_operation_Dict["amplification"] = amplification;
        return quality_operation_Dict;
    }
    /// <summary>
    /// "조화" 연산 함수. 
    /// "조화" 연산조건을 가진 아이템의 연산 결과를 제공.
    /// </summary>
    /// <param name = "standard_conditions">해당 레시피의 item_criterion.standard_conditions를 인자로 받음</param>
    /// <param name = "item_conditions">현재 조합 결과 컨테이너의 데이터를 받음</param>
    /// <param name = "origin_item_datas">조합 사용원본 아이템 데이터.</param>
    /// <param name = "debug_log_Use">디버그 여부,  True시 디버그 로그 출력</param>
    /// <returns>int,  정수값만 반환함,  소수점 아래 버림</returns>
    public static int harmony(
                        Dictionary<string, (string, int)> standard_conditions, 
                        Dictionary<string, (int, int)> item_conditions,
                        Dictionary<string, item_data> origin_item_datas,
                        bool debug_log_Use = false){
        int standard_conditions_quality = 0;//기준 조건의 품질
        int not_item_conditions_quality = 0;// 기준조건을 불만족 하는 item_conditions의 태그들의 품질
        //standard_conditions에 있는 테그의 리스트 컬렉터,  디버그용.
        Dictionary<string,  int> item_condition_list  =  new Dictionary<string,  int>();
        //standard_conditions을 불만족하는 테그 리스트 컬렉터,  디버그용.
        Dictionary<string,  int> not_item_conditions = new Dictionary<string,  int>();
        
        string Debug_Log = "match tags";//디버그 로그를 위한 텍스트
        foreach(KeyValuePair<string,  (string, int)> standard_condition in standard_conditions){
            //조건이 "match"인 항목들중,  item_conditions내의 값들 체크 if문
            if (standard_condition.Value.Item1 == "match"&&item_conditions.ContainsKey(standard_condition.Key)){
                //item_condition_list에 리스트 저장
                item_condition_list[standard_condition.Key] = standard_condition.Value.Item2;
                //실제 품질 값 연산
                standard_conditions_quality += item_conditions[standard_condition.Key].Item2
                                                * item_conditions[standard_condition.Key].Item1;
                if(debug_log_Use == true){// 디버그 ok일 때만 출력하기 위한 if문
                    Debug_Log+= "\n "+standard_condition.Key+
                                " | "+standard_condition.Value.Item2+
                                " * "+item_conditions[standard_condition.Key].Item1;//디버그 로그를 위한 텍스트
                }
                
            }
        }
        Debug_Log+= "\n unmatched tags";
        foreach(KeyValuePair<string,  (int, int)> item_condition in item_conditions){
            //item_condition_list에  item_condition.Key인 현재 테그가 없는 경우.
            if (!item_condition_list.ContainsKey(item_condition.Key)){
                //
                not_item_conditions[item_condition.Key] = item_condition.Value.Item2;
                not_item_conditions_quality+= item_condition.Value.Item2;

                if(debug_log_Use == true){// 디버그 ok일 때만 출력하기 위한 if문
                    Debug_Log+= "\n "+item_condition.Key+
                                " | "+item_condition.Value.Item2+
                                " * "+item_condition.Value.Item1;
                }
            }
        }
        if(debug_log_Use == true)
            Debug.Log(Debug_Log);
        
        return standard_conditions_quality-not_item_conditions_quality;

    }
    
    public static int amplification(
                        Dictionary<string,  (string,  int)> standard_conditions,  
                        Dictionary<string,  (int,  int)> item_conditions, 
                        Dictionary<string, item_data> origin_item_datas, 
                        bool debug_log_Use = false){
        return 0;
    }
}
/// <summary>
/// 기준 개수 연산 로직 클래스
/// </summary>
public class count_operations{
    
    public delegate int count_operation_delegate(Dictionary<string,  (string, int)> standard_conditions, 
                         Dictionary<string,  (int,  int)> item_conditions);
    public Dictionary<string,  count_operation_delegate> count_operation_list(){
        Dictionary<string,  count_operation_delegate> count_operation_Dict
                     = new Dictionary<string,  count_operation_delegate>();
        count_operation_Dict[nameof(standard_count)] = standard_count;
        
        return count_operation_Dict;
    }
    /// <summary>
    /// 기본개수 연산함수<br/>
    /// 현재 int값이 오버플로우 날 수도 있어서,  막는 방법 준비 필요.<br/> 
    /// </summary>
    /// <param name = "standard_conditions">item_criterion.standard_conditions를 인자로 받음</param>
    /// <param name = "item_conditions">"기술 연산 결과 데이터"를 입력으로 받음</param>
    /// <returns></returns>
    public static int standard_count ( Dictionary <string, (string, int)> standard_conditions,
                         Dictionary<string, (int, int)> item_conditions){
        int result_count = 2147483646;
        foreach(KeyValuePair<string, (string, int)> standard_condition in standard_conditions){
            string condition = standard_condition.Value.Item1; //연산종류
            string tag = standard_condition.Key;                //테그
            //연산종류 일치 시, 
            if (condition == "match" && item_conditions.ContainsKey(tag)){
                // 현재 테그 수/기준테그 수
                // 예를들어, 기준 테그가 2,2,1 이고, 보유테그가 4,4,1이면
                // 나눈 값이 2,2,1이 되는데, 그중 가장 작은 1이 결과 개수가 됨. 
                // 만약, 2,2,1이고 4,4,2면, 2개가 결과값이됨.일괄 작업을 상정한 개수 반환코드임.
                int current_count = item_conditions[tag].Item1 / standard_condition.Value.Item2;
                if(result_count > current_count)
                    result_count = current_count;
            }
        } 
        return result_count; 
    }
}
/// <summary>
/// 등급(랭크) 연산로직
/// </summary>
public class rank_operations{
    public delegate int rank_operation_delegate(int current_quality, (int,int) standard_quality);
    public Dictionary<string, rank_operation_delegate> rank_operation_list(){
        Dictionary<string, rank_operation_delegate> rank_operation_Dict
                    = new Dictionary<string, rank_operation_delegate>();
        rank_operation_Dict[nameof(standard_rank)]=standard_rank;
        
        return rank_operation_Dict;
    }
    int normalize(int current_quality, int mid){
        if (current_quality < mid)
            return (mid - current_quality) / mid;
        else if(current_quality > mid)
            return (current_quality - mid) / mid;
        else
            return 1;
    }
    public int max_rank = 10;
    /// <summary>
    /// 기본 랭크 연산
    /// </summary>
    /// <param name="current_quality">품질연산 규칙 결과값</param>
    /// <param name="standard_quality">기준 품질</param>
    /// <returns>int 형, current_quality/standard_quality를 반환</returns>
    public int standard_rank(int current_quality, (int, int) standard_quality){
        int mid = (standard_quality.Item1 + standard_quality.Item2) / 2;
        return max_rank * normalize(current_quality, mid);
    }
    
}



public class recipe : MonoBehaviour
{
    

    public Dictionary<string,  item_criterion> item_criteria  =  new Dictionary<string,  item_criterion>();
    
    public string item_item_criterion_parser(string item_id){
        item_criterion currunt_criteria = item_criteria[item_id];
        string item_name = item_criteria[item_id].name;
        string output_item_criterion =
                      "아이템 ID : " + item_id + //현재 아이템 id
                      "\n아이템 이름 : " + currunt_criteria.name + //현재 아이템의 이름, item_criterion_data의 Value는 item_criterion형식을 가지고 있음.
                      "\n아이템 기준 품질 :  " + currunt_criteria.standard_quality;// item_criterion의  standard_quality 변수값을 입력., 
            output_item_criterion += "\n----------\n테그|조건|개수";// 세퍼레이터.
            //standard_conditions의 값들을 output_item_criterion에 입력
            foreach(KeyValuePair<string, (string, int)> tag_data in currunt_criteria.standard_conditions){
                output_item_criterion +=    "\n" + tag_data.Key +        //현재 테그
                                            "|" + tag_data.Value.Item1 + //item_criterion의 값.(int,int) : {개수,계수}
                                            "|" + tag_data.Value.Item2; //
            }
        return output_item_criterion;
    }
    /// <summary>
    /// 아이템 데이터가 정상적으로 저장되었는지 체크하는 함수. <br/>
    /// 저장된 아이템 마다의 값들을 출력
    /// </summary>
    public string test_item_criteria(){
        string Debug_log = "임시 디버그 로그\n";
        
        foreach(KeyValuePair<string, item_criterion> item_criterion_data in item_criteria){
            Debug_log += "--------------------\n"+item_criterion_data.Key;
            //디버그 출력 문자열입력
            string output_item_criterion  =  item_item_criterion_parser(item_criterion_data.Key);
            // Debug.Log(output_item_criterion);
            Debug_log += output_item_criterion+"\n";
        }
        return Debug_log;
    }




    /// <summary>
    /// 조합법 데이터를 정의하는 구간,  여기에서 정의하면 변수에 조합법이 업로드 됨.
    /// </summary>
    void main(){
        item_criteria["0001"] = new item_criterion(){
                                name = "기초 힐포션",
                                standard_conditions = new Dictionary<string, (string, int)>{
                                    {"생명", ("match", 1)},
                                    {"갈망", ("match", 1)},
                                    {"죽음", ("be_none", 1)}
                                    },
                                rank_operation = "standard_rank",
                                operations = "harmony",
                                standard_quality =(10, 100) ,
                                count_operation = "standard_count"
                            };
        item_criteria["0002"] = new item_criterion(){
                                name = "고양이의 힐포션",
                                standard_conditions = new Dictionary<string, (string, int)>{
                                    {"생명", ("match", 1)},
                                    {"갈망", ("match", 1)},
                                    {"죽음", ("be_none", 1)},
                                    {"고양이의 울음소리", ("match", 1)}
                                    },
                                rank_operation = "standard_rank",
                                operations = "harmony",
                                standard_quality = (30, 50),
                                count_operation = "standard_count"
                            };
                            

        // test_item_criteria();
    }



/// <summary>
/// 기준품질 충족여부 확인
/// </summary>
/// <param name = "current_quality">결과 품질을 입력,  operations하위 함수의 결과값을 인자로 받음</param>
/// <param name = "standard_quality">기준 품질을 입력,  item_criteria.standard_quality를 받음</param>
/// <returns> 만족할 경우 true,  불만족할 경우 false 반환</returns>
    bool check_standard_quality(int current_quality, (int, int) standard_quality){
        
        if(current_quality >= standard_quality.Item1 
            && current_quality <= standard_quality.Item2)
            return true;
        else
            return false;
    }



/// <summary>
/// 기준조건 충족 여부 확인
/// </summary>
/// <param name="standard_conditions">기준 조건 item_criteria.standard_conditions를 인자로 받음</param>
/// <param name="item_conditions">조합 결과 컨테이너를 받음. 기술연산 결과 데이터.</param>
/// <returns>하나라도 조건을 불만족 하면, 불반족으로 판정</returns>
    bool check_standard_conditions( Dictionary<string, (string, int)> standard_conditions,
                                    Dictionary<string, (int, int)> item_conditions){
        bool check_conditions_bool=true;
        foreach(KeyValuePair<string,(string,int)> check_condition in standard_conditions){

            int item_conditions_num = check_condition.Value.Item2;
            int item_num = item_conditions[check_condition.Key].Item1;
            if( check_condition.Value.Item1 == "match"
                &&!item_conditions.ContainsKey(check_condition.Key)
                &&item_conditions_num != item_num){

                check_conditions_bool = false;
            }
            else if(check_condition.Value.Item1 == "be_none"
                    &&item_conditions.ContainsKey(check_condition.Key)
                    &&item_conditions_num<item_num){ //check_conditions_bool=false;
                    check_conditions_bool = false;
            }
        }
        return check_conditions_bool;
    }


    /// <summary>
    /// 기준조건 중촉 레시피 리스트를 반환하는 함수
    /// </summary>
    /// <param name = "item_conditions">조합 결과 컨테이너를 인자로 받음</param>
    /// <returns>레시피의 아이템 ID리스트를 반환</returns>
    List<string> recipe_List_checker(Dictionary<string,  (int,  int)> item_conditions){
        List<string> may_available_Item_list = new List<string>();
        foreach(KeyValuePair<string,  item_criterion> item_criterion_data in item_criteria){
            if(check_standard_conditions(item_criterion_data.Value.standard_conditions,  item_conditions)){
                may_available_Item_list.Add(item_criterion_data.Key);
            }
        }
        return may_available_Item_list;
    }

public struct result_item_data{
    public item_data item_Data;
    public string log;
    
}

/// <summary>
/// 임시 조합 연산 함수. 
/// </summary>
/// <param name = "item_id">아이템 아이디</param>
/// <param name = "item_conditions">조합 결과 컨테이너</param>
/// <returns>
/// {"item_id", item_id}, <br/>
/// {"quality", quality}, {"rank", quality}, <br/>
/// {"num_of_item", quality}, <br/>
/// {"item_condition", item_conditions}, <br/>
/// {"craft_date", DateTime.Now.ToString("yyy-MM-dd-HH-mm-ss")}, //임시 타임스템프<br/>
/// {"log", recipe_operation_Debug_log}//로그<br/>
/// </returns>
    public result_item_data recipe_operation(
                            string item_id,  
                            Dictionary<string,  (int,  int)> item_conditions, 
                            Dictionary<string,  item_data> origin_item_datas
                        ){

        //item_criteria에서 레시피를 가져와 임시저장
        item_criterion current_recipe  =  item_criteria[item_id];

        //operations에서 품질계산 규칙 로직을 로드
        var q_op = new quality_operations();//클레스 인스턴스 생성
        var Quality_operation = q_op.quality_operation_list();//델리게이트 딕셔너리 로 초기화

        //count_operations에서 기준개수 로직을 로드
        var c_op = new count_operations();//클레스 인스턴스 생성
        var count_operation = c_op.count_operation_list();//델리게이트 딕셔너리 를 참조해 초기화

        //rank_operations에서 등급 산정 규칙 로직을 로드
        var r_op  =  new rank_operations();//클레스 인스턴스 생성
        var rank_operation = r_op.rank_operation_list();//델리게이트 딕셔너리 로 초기화
        
        int quality = 0;  // 최종 품질
        int rank = 0;    // 산정 등급
        int num_of_item = 0;//산정 개수

        //디버그용 조합로그
        string recipe_operation_Debug_log = "조합과 로그\n";
        // 품질연산규칙 연산
        quality = Quality_operation[current_recipe.operations](   
                                                    current_recipe.standard_conditions,  
                                                    item_conditions,  
                                                    origin_item_datas,  
                                                    true);
        recipe_operation_Debug_log += "조합연산 = " + current_recipe.operations
                                    + "\n품질연산규칙 결과 : " + quality;//디버그 로그 저장.
        // 기준품질 확인
        if(!check_standard_quality(quality,current_recipe.standard_quality)){
            recipe_operation_Debug_log += "\n조합 실패";    //디버그 로그 저장
            Debug.Log(recipe_operation_Debug_log);
        }
        else{
            //등급산정 연산 
            rank  =  rank_operation[current_recipe.rank_operation](   quality, 
                                                                    current_recipe.standard_quality);
            recipe_operation_Debug_log += "\n산정등급 : "+rank;//디버그 로그 저장
            //개수 산정 연산
            num_of_item = count_operation[current_recipe.count_operation](
                                                                        current_recipe.standard_conditions, 
                                                                        item_conditions);
            recipe_operation_Debug_log += "\n결과 개수 : "+num_of_item;//디버그 로그 저장.
            recipe_operation_Debug_log += "\n조합 성공!";//디버그 로그 저장
        }
        
        result_item_data result_Item_Data = new result_item_data{
            item_Data = new item_data{
                item_ID = item_id,
                tagList = item_conditions,
                current_quality = quality,
                rank = rank,
                count = num_of_item,
                Production_date = DateTime.Now.ToString("yyy-MM-dd-HH-mm-ss"),

            },
            log = recipe_operation_Debug_log
        };
        Debug.Log(recipe_operation_Debug_log);// 디버그 로그 출력
        return result_Item_Data;
    }
    public static recipe instance;

    private void Awake() {
        if(recipe.instance == null)
            recipe.instance = this;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        main();
    }



    // Update is called once per frame
    void Update()
    {
     
    }

}
