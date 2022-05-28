using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Main;
using System;
using System.Text;

namespace Main
{
    //이름 작성하는 키보드 버튼 작동하도록
    //한영 변환, shift:(Eng) 대문자 소문자 전환, (Kor) 된소리Consonant 전환, Delete, Enter 이름 등록

    public class KeyBoardCtrl : MonoBehaviour
    {
        //인스펙터 키보드 : 입력용 자판 글자표시
        public Text[] keyRaws;

        //스페이스, 작성완료 버튼 (활성 비활성 제어하기 위함)
        public Button delete, shift, space, engKor, done;

        //안내용 text 
        public Text notice_Text;

        //임시 영어 자판 바꿔넣는 용도
        string[] tempEngKeys = new string[36]
        {
            "1","2","3","4","5","6","7","8","9","0"
            ,"q","w","e","r","t","y","u","i","o","p"
            ,"a","s","d","f","g","h","j","k","l"
            ,"z","x","c","v","b","n","m"
        };
        //임시 한글 자판 바꿔넣는 용도
        string[] tempKorKeys = new string[36]
        {
            "1", "2" , "3" , "4" , "5" , "6" , "7" , "8" , "9" , "0"
            ,"ㅂ","ㅈ","ㄷ","ㄱ","ㅅ","ㅛ","ㅕ","ㅑ","ㅐ","ㅔ"
            ,"ㅁ","ㄴ","ㅇ","ㄹ","ㅎ","ㅗ","ㅓ","ㅏ","ㅣ"
            ,"ㅋ","ㅌ","ㅊ","ㅍ","ㅠ","ㅜ","ㅡ"
        };

        /*규칙 찾기 : 참고사이트https://mchch.tistory.com/84 
         
        _1. 경우의 수_
        //자모, 자모자, 자모 자모, 자모자자, 자모자 자모  ex > 아, 알, 아라, 앍, 알가
        //모모, 자모모, 자모모자 자모모 자모, 자모모자자, 자모모자 자모 , ex > ㅘ, 와, 왈, 와라 ,왏, 왈하


        _2. 규칙과 예외의 경우_

        *if(자모 + 자(+모))
        
        //자모 자모 (받침없음) : ex 가가 누누
        //자모자 모 : 각ㅏ(방법) '각을 입력하고 (스페이스나 자음을 썻다가 지운 후) ㅏ를 입력

        *if(자모 + 모) 
        
        //자 모모(o) : ( if( ㅗ ㅜ ㅡ + ){}  else{} ) 
        //1. ㅗ + "ㅘ ㅙ ㅚ"
        //2. ㅜ + "ㅝ ㅞ ㅟ"
        //3. ㅡ + "ㅣ" 
        //자모 모 : 위 이외는 못 붙임, (모모 = 모)로 봄

        *if(자모 + 자(+자) + (+모))
        
        //종성 자자  (o) : "ㄳ","ㄵ","ㄶ","ㄺ","ㄻ","ㄼ","ㄽ","ㄾ","ㄿ","ㅀ","ㅄ"
        //종성 자 자 (x) : 위 이외는 못 붙임 ex) 자모'자 자모' : 국구
        //종성 자자 + if모음 : 왈하

        1단계 모음인가 자음인가 판단 필요

        //초성 중성 종성 -> 합친다 분리한다
        2단계 한 글자의 묶음 단위 정의 필요
        3단계 마지막 글자 뒤에 뭐가 붙냐에 따라 분기문 필요
        */
        /* 유니코드를 이용한 자모 분리 결합 :
         * 참고사이트https://storycompiler.tistory.com/212
         * 참고사이트https://www.stechstar.com/user/zbxe/WinApps/63202
         */

        //초성 분류 (한글 함수에 넣을 배열)
        string[] chosung = new string[19]
        {
             "ㄱ","ㄲ","ㄴ","ㄷ","ㄸ","ㄹ","ㅁ","ㅂ","ㅃ","ㅅ",
             "ㅆ","ㅇ","ㅈ","ㅉ","ㅊ","ㅋ","ㅌ","ㅍ","ㅎ"
        };

        //중성 분류 (한글 함수에 넣을 배열)
        string[] joongsung = new string[21]
        {
            "ㅏ","ㅐ","ㅑ","ㅒ","ㅓ","ㅔ","ㅕ","ㅖ","ㅗ","ㅘ",
            "ㅙ","ㅚ","ㅛ","ㅜ","ㅝ","ㅞ","ㅟ","ㅠ","ㅡ","ㅢ","ㅣ"
        };

        //종성 분류(한글 함수에 넣을 배열)
        string[] jongsung = new string[28]
        {
            "", "ㄱ", "ㄲ", "ㄳ", "ㄴ", "ㄵ", "ㄶ", "ㄷ", "ㄹ","ㄺ",
            "ㄻ", "ㄼ", "ㄽ", "ㄾ", "ㄿ", "ㅀ", "ㅁ","ㅂ","ㅄ", "ㅅ",
            "ㅆ", "ㅇ", "ㅈ", "ㅊ", "ㅋ", "ㅌ", "ㅍ", "ㅎ"
        };

        // name의 Length체크
        int num;
        //초성중성종성 등등 단계별로 ++누적 (case 1,2,3,4...)
        int korCounting;

        //임시_사용할 초 중 종성
        string temp_Chosung, temp_Joongsung, temp_Jongsung; 
        string temp_oldJongsung, temp_newJongsung; // (겹자음에서 -> 모음을 누르면) 임시저장할 변수가 필요하다.

        //완성형
        ushort temp_JoogsungUint16; 

        //완성된 유니코드 위치를 활용
        static ushort unicode_KorBase = 0xAC00;

        //자음모음 판단용
        bool isMoum;
        bool isGyeupJaum;

        //제어용
        bool isShift; //쉬프트 눌렸는지
        bool isKor; //한글로 바뀌었는지
        bool isConsonant; //된소리로 바꿔야 하는지
        [HideInInspector]
        public bool isDoneButtonClicked;//업데이트에서 작성완료를 제어하고 있음 중복 누르기 방지를 위함

        //이름 추가하기
        public Text showMyName; // 게임오브젝트 myName.text = name + blinkText;
        string nameText;//이름넣는 부분
        string blinkText;//깜박이는 부분
        public EnumPlayer_Information usingMyNewPlayer;
        //글자수 제어
        bool isMaxLength;

        float deltaTime;//초 시간세기
        float temp_TimeCheck;//시간 체크 임시변수

        //이름 작성중이면 흰색, 공란이면 회색
        public Color fontColor_default; //회색
        public Color fontColor_bright;  //흰색

        DataController saveData;

        private void Start()
        {
            nameText = "";

            saveData = FindObjectOfType<DataController>();
        }

        //외부 인스펙터에서의 연결 누르면 글자가 적히는 버튼키
        public void OnClick_Keyboard_btn(int i)
        {
            //최대 글자 넘지 않으면 (조작조건)
            if (!isMaxLength)
            {
                //한글을 입력할 때 (한글 키 && 0~9까지 숫자를 제외)
                if (isKor && i >= 10)
                {
                    //# 자음모음 검사 (모음 자음에 따라 경우의 수 처리)
                    Check(char.Parse(keyRaws[i].text));

                    //# 초성 -> 중성 -> 종성: 단계별로 작성하기 위함 = korCounting

                    //# 어떤 것이 들어 있는지 알아야 결합 또는 다음 글자로 넘길 수 있음

                    //중성: 모음 + 모음 ㅗ + "ㅘ ㅙ ㅚ"
                    //중성: 모음 + 모음 ㅜ + "ㅝ ㅞ ㅟ"
                    //중성: 모음 + 모음 ㅡ + "ㅣ" 
                    //종성: 자음 + 자음 "ㄳ","ㄵ","ㄶ","ㄺ","ㄻ","ㄼ","ㄽ","ㄾ","ㄿ","ㅀ","ㅄ"
                    //종성: 다음 + 모음 ex: 갉 + ㅏ -> 갈가

                    print(korCounting + "진행단계");

                    switch(korCounting)
                    {
                        case 0://첫 누름 (# 자음모음 판단)

                            //자음일때
                            if (!isMoum)
                            {
                                //초성으로 저장
                                temp_Chosung = keyRaws[i].text;

                                //일단 텍스트에 "text"표시
                                nameText += temp_Chosung;

                                //누름 카운트++
                                korCounting++;
                                print("0 자음");
                            }
                            //모음일때
                            else
                            {
                                print("누르면 안됩니다.");
                            }
                            break;
                       
                        case 1: //두번째누름  (# 초성 + 중성)

                            //(자음 눌렀다면 카운팅 1상태 유지)
                            if (!isMoum)
                            {
                                //새로운 초성
                                temp_Chosung = keyRaws[i].text;

                                //일단 텍스트에 "text"표시
                                nameText += temp_Chosung;
                                print("case 1 자음");
                            }
                            //모음일때
                            else
                            {
                                temp_Joongsung = keyRaws[i].text;

                                //자+모 : 결합되어서 수정해야함
                                if (nameText.Length >= 1)
                                {
                                    //분리된 상태를 먼저 지우고
                                   nameText = nameText.Substring(0, nameText.Length - 1);
                                }

                                //받침 없는 상태 = jongsung 0번째 인덱스  = "";
                                temp_Jongsung = jongsung[0];

                                //결합한 완성자를 다시 대입
                                nameText += AddChar(temp_Chosung, temp_Joongsung, temp_Jongsung);

                                //누름 카운트
                                korCounting++;

                                print("case 1 모음");
                            }
                            break;
                       
                        case 2: //세번째 누름 (모음 + 모음 결합인지 판단) # ㅗ ㅜ ㅡ 

                            //(자+모+자) 자음일때 = 종성
                            if (!isMoum)
                            {
                                temp_Jongsung = keyRaws[i].text;

                                //결합되어서 수정해야함
                                if (nameText.Length >= 1)
                                {
                                    //분리된 상태를 먼저 지우고
                                   nameText = nameText.Substring(0, nameText.Length - 1);
                                }

                                //결합한 완성자를 다시 대입
                                nameText += AddChar(temp_Chosung, temp_Joongsung, temp_Jongsung);

                                //누름 카운트
                                korCounting++;

                                print("case 2 자음"); //다음case3 판단 겹받침인지 판단
                            }
                            //모음일때
                            else
                            {
                                print("case 2 모음"); //다음case3 판단 겹받침인지 판단

                                //temp_Joongsung 이 모음(ㅗㅜㅡ)이라면
                                if (temp_Joongsung == "ㅗ" || temp_Joongsung == "ㅜ" || temp_Joongsung == "ㅡ")
                                {
                                    //+keyRaw[i].text 가 temp_Joongsung과 결합할 수 있다면
                                    temp_Joongsung = Joongsung(temp_Joongsung, keyRaws[i].text);

                                    //모+모 : 결합되어서 수정해야함
                                    if (nameText.Length >= 1)
                                    {
                                        //분리된 상태를 먼저 지우고
                                         nameText = nameText.Substring(0, nameText.Length - 1);
                                    }

                                    //받침이 없으므로 = jongsung 0번째 인덱스  = ""; 대입
                                    temp_Jongsung = jongsung[0];

                                    //확인용 
                                    print("중성 확인용 "+temp_Chosung + temp_Joongsung +"ㅘ" + keyRaws[i].text);

                                    //결합한 완성자를 다시 대입
                                    nameText += AddChar(temp_Chosung, temp_Joongsung, temp_Jongsung);

                                    print("case 2 모+모음");
                                    //자모모 => 자모 상태 1->2로 넘어가는 과정과 같다 : 카운트 유지
                                }
                                //temp_Joongsung 모음이 (ㅜㅗㅡ)가 아니라면
                                else
                                {
                                    myKorRullReset();

                                    print("case 2 새로운 모음");
                                }

                            }
                            break;

                        case 3: //자모자 상태  (# 겹받침 조건이 되는가)

                            //자음을 눌렀다면
                            if (!isMoum)
                            {
                                //전에 입력값이 겹자음으로 들어올 수 있을때
                                if (temp_Jongsung == "ㄱ" || temp_Jongsung == "ㄴ" || temp_Jongsung == "ㄹ" || temp_Jongsung == "ㅂ")
                                {
                                    //첫번째 겹자음 임시저장
                                    temp_oldJongsung = temp_Jongsung;

                                    //1. 겹자음인지 판단 "ㄳ","ㄵ","ㄶ","ㄺ","ㄻ","ㄼ","ㄽ","ㄾ","ㄿ","ㅀ","ㅄ"
                                    temp_Jongsung = Jongsung(temp_Jongsung, keyRaws[i].text);

                                    //겹자음이라면
                                    if (isGyeupJaum)
                                    {
                                        //두번째 겹자음 임시 저장
                                        temp_newJongsung = keyRaws[i].text;

                                        // 결합되어서 수정해야함
                                        if (nameText.Length >= 1)
                                        {
                                            //분리된 상태를 먼저 지우고
                                            nameText = nameText.Substring(0, nameText.Length - 1);
                                        }

                                        //완성자 넣기
                                        nameText += AddChar(temp_Chosung, temp_Joongsung, temp_Jongsung);
                                        print("case 3 겹자음 누름");
                                        //겹자음 항상 false
                                        isGyeupJaum = false;

                                        //겹자음 상태에서 모음을 누른다면 ++ case 4;
                                        korCounting++;
                                    }

                                    //# ex 아니라면 "난"  여기서 그냥 입력상태 + 자음 "ㅂ"
                                    else//겹자음이 아니라면
                                    {
                                        //키입력 그대로 받아서
                                        temp_Chosung = keyRaws[i].text;
                                        //초성 대입
                                        nameText += temp_Chosung;

                                        //case 1 으로 되돌아가기
                                        korCounting = 1;
                                        
                                        print("case 3 겹자음 조건에서 벗어난 새로운 자음");
                                    }
                                }//겹자음조건

                                else//겹자음 조건아님, 새 자음을 입력했을때
                                {
                                    //키입력 그대로 받아서
                                    temp_Chosung = keyRaws[i].text;
                                    //초성 대입
                                    nameText += temp_Chosung;

                                    //case 1 으로 되돌아가기
                                    korCounting = 1;

                                    print("case 3 그냥 새로운 자음");
                                }

                            }//if (!isMoum)

                            //모음을 눌렀다면
                            else
                            {
                                //1. 종성 + 모음 "난 + 모음 => 나니" 
                                //2. 겹받침 종성 "값 + 모음 => 갑시" 
                                //3. 다시 종성 넣는 위치로

                                // 결합되어서 수정해야함
                                if (nameText.Length >= 1)
                                {
                                    //분리된 상태를 먼저 지우고
                                    nameText = nameText.Substring(0, nameText.Length - 1);
                                }
                                //완성자 넣기 1
                                nameText += AddChar(temp_Chosung, temp_Joongsung, jongsung[0]);

                                //종성 -> 초성에 대입
                                temp_Chosung = temp_Jongsung;
                                //입력한 모음 완성자 만들어주고 
                                temp_Joongsung = keyRaws[i].text;
                                //완성자 넣기 2
                                nameText += AddChar(temp_Chosung, temp_Joongsung, jongsung[0]);

                                //case 2로 보낸다 모음 판단
                                korCounting = 2;

                                print("case 3 모음 누른 입력은 받침글자가 아님");

                            }
                            break;

                        case 4:
                            
                            //자음을 눌렀다면 새로운 초성
                            if (!isMoum)
                            {
                                //저장
                                temp_Chosung = keyRaws[i].text;

                                //대입
                                nameText += temp_Chosung;

                                //자음 하나만 있는 상태 case 1로 보냄
                                korCounting = 1;
                            }

                            else//1a. 겹자음 상태에서 모음을 눌렀다면! case 3에서 저장 완료 oldJongsung; newJongsung;
                            {
                                //길이 하나 줄고
                                if (nameText.Length >= 1)
                                {
                                    //분리된 상태를 먼저 지우고
                                    nameText = nameText.Substring(0, nameText.Length - 1);
                                }

                                // 1. (chosung + temp_joosung + oldJongsung)완성자
                                nameText += AddChar(temp_Chosung, temp_Joongsung, temp_oldJongsung);

                                //모음에 저장한 후
                                temp_Joongsung = keyRaws[i].text;

                                // 2. (newJongsung + keyRaw[i].text)완성자
                                nameText += AddChar(temp_newJongsung, temp_Joongsung, jongsung[0]);

                                //case 2로 보낸다 모음 판단
                                korCounting = 2;

                                print("case 4 겹자음 + 모음 = 받침글자가 아님");
                            }
                            break;
                    }//switch korCounting

                }//한국어 && 글자일때
                //한국어가 아닐떄
                if (!isKor)
                {
                    //알파벳은 바로 네임에 대입
                    nameText += keyRaws[i].text;
                }
                
            }//최대글자 이하라면

        }//keyboard_button

        public void Check(char ch)
        {
            //자음의 범위
            if (0x3131 <=ch && ch <= 0x314E)
            {
                isMoum = false;
            }
            //모음의 범위
            if (0x314F <= ch && ch <= 0x3163)
            {
                isMoum = true;
            }
        }

        // 합치기 위한 함수
        public string Joongsung(string prev, string now)
        {
            /*
            UInt16 temp;
            temp = 0x116A;// 옛한글임..
            string d;
            d = Convert.ToChar(temp).ToString();
            print(d); : ㅘ 없어서 나중에 완성자 사용시 -1나옴
            */

            switch (prev)
            {
                case "ㅗ":
                    if (now == "ㅏ")
                    {
                        /* 완성형으로 가지고 온다
                        char d= Convert.ToChar(0x116A); 0x116A는 옛 한글...ㅘ -> addchar에서 -1로 값이 안나옴
                        print(d); : ㅘ
                        */
                        temp_JoogsungUint16 = 0x3158;
                    }
                    if (now == "ㅐ")
                    {
                        //ㅙ
                        temp_JoogsungUint16 = 0x3159;
                    }
                    if (now == "ㅣ")
                    {
                        //ㅚ
                        temp_JoogsungUint16 = 0x315A;
                    }
                    //이외라면
                    if (now != "ㅏ" && now != "ㅐ" && now != "ㅣ")
                    {
                        return now;
                    }
                    break;

                case "ㅜ":
                    if (now == "ㅓ")
                    {
                        temp_JoogsungUint16 = 0x315D;
                    }
                    if (now == "ㅔ")
                    {
                        temp_JoogsungUint16 = 0x315E;
                    }
                    if (now == "ㅣ")
                    {
                        temp_JoogsungUint16 = 0x315F;
                    }
                    //이외라면
                    if (now != "ㅓ" && now != "ㅔ" && now != "ㅣ")
                    {
                        return now;
                    }
                    break;
                case "ㅡ":
                    if (now == "ㅣ")
                    {
                        temp_JoogsungUint16 = 0x3162;
                    }
                    if (now != "ㅣ")
                    {
                        return now;
                    }
                    break;
                    
            }
            now = Convert.ToChar(temp_JoogsungUint16).ToString();
            return now;
        }

        //합치기 위한 함수2 (겹받침 종성)
        public string Jongsung(string prev, string now)
        {

            /*jongsung[]
            "", "ㄱ", "ㄲ", "ㄳ", "ㄴ", "ㄵ", "ㄶ", "ㄷ", "ㄹ","ㄺ",
            "ㄻ", "ㄼ", "ㄽ", "ㄾ", "ㄿ", "ㅀ", "ㅁ","ㅂ","ㅄ", "ㅅ",
            "ㅆ", "ㅇ", "ㅈ", "ㅊ", "ㅋ", "ㅌ", "ㅍ", "ㅎ"
            */
            //"ㄳ","ㄵ","ㄶ","ㄺ","ㄻ","ㄼ","ㄽ","ㄾ","ㄿ","ㅀ","ㅄ"
            switch (prev)
            {
                case "ㄱ":
                    if (now == "ㅅ")
                    {
                        //temp_JongsungUint16 =
                        now = jongsung[3];
                        isGyeupJaum = true;
                    }
                    break;
                case "ㄴ":
                    if (now == "ㅈ")
                    {
                        //temp_JongsungUint16 = 
                        now = jongsung[5];
                        isGyeupJaum = true;
                    }
                    if (now == "ㅎ")
                    {
                        now = jongsung[6];
                        isGyeupJaum = true;
                    }

                    if (now != "ㅈ" && now != "ㅎ")
                    {
                        return now;
                    }

                    break;
                case "ㄹ":
                    if (now == "ㄱ")
                    {
                        now = jongsung[9];
                        isGyeupJaum = true;
                    }
                    if (now == "ㅁ")
                    {
                        now = jongsung[10];
                        isGyeupJaum = true;
                    }
                    if (now == "ㅂ")
                    {
                        now = jongsung[11];
                        isGyeupJaum = true;
                    }
                    if (now == "ㅅ")
                    {
                        now = jongsung[12];
                        isGyeupJaum = true;
                    }
                    if (now == "ㅌ")
                    {
                        now = jongsung[13];
                        isGyeupJaum = true;
                    }
                    if (now == "ㅍ")
                    {
                        now = jongsung[14];
                        isGyeupJaum = true;
                    }
                    if (now == "ㅎ")
                    {
                        now = jongsung[15];
                        isGyeupJaum = true;
                    }
                    if (now != "ㄱ" && now != "ㅁ" && now != "ㅂ" && now != "ㅅ" && now != "ㅌ" && now != "ㅍ" && now != "ㅎ")
                    {
                        return now;
                    }
                    break;
                case "ㅂ":
                    if (now == "ㅅ")
                    {
                        now = jongsung[18];
                        isGyeupJaum = true;
                    }
                    else
                    {
                        return now;
                    }
                    break;
            }

            return now;
        }

        //완성형 유니코드 참고사이트http://www.unicode.org/charts/PDF/UAC00.pdf
        //합치기 = KorBase유니코드 +
        //(초성 * 21 + 중성) * 28 + 종성;
        //문자로 반환
        public string AddChar(string cho, string joong, string jong)
        {
            //초성위치 중성위치 종성위치
            int indexCho, indexjoong, indexjong, intUnicode;
            string temp;

            //키입력에서 받아온 정보를 정리된 배열에 맞춰 대입 => 배열의 위치 인덱스저장
            indexCho = Array.FindIndex<string>(chosung, x => x == cho);
            indexjoong = Array.FindIndex<string>(joongsung, x => x == joong);
            indexjong = Array.FindIndex<string>(jongsung, x => x == jong);

            //위치만큼 더하기 = 완성자
            intUnicode = unicode_KorBase + (indexCho * 21 + indexjoong) * 28 + indexjong;

            //인트 -> 바이트로 반환하기 
            Byte[] byte_CompleteMerge = BitConverter.GetBytes((short)intUnicode);

            //코드값을 문자로 변환  System.Text.Unicode.GetBytes()
            temp = Encoding.Unicode.GetString(byte_CompleteMerge);

            //테스트검사용
            print( "에드차 탬프값 " + temp + " 유니코드 인트형 " + intUnicode);
            print("초성" + cho + indexCho + "중성" + joong + indexjoong + "종성" + jong + indexjong);

            return temp;
        }

        //shift , 한영 변환에 따른 함수
        public void ButtonCall()
        {
            //영어 소문자
            if (!isKor && !isShift)
            {
                for (int i = 0; i < keyRaws.Length; i++)
                {
                    keyRaws[i].text = tempEngKeys[i];
                    keyRaws[i].text = keyRaws[i].text.ToLower();
                }
            }
            //영어 대문자
            if (!isKor && isShift)
            {
                for (int i = 0; i < keyRaws.Length; i++)
                {
                    keyRaws[i].text = tempEngKeys[i];
                    keyRaws[i].text = keyRaws[i].text.ToUpper();
                }
            }
            //한글 쉬프트x
            if (isKor && !isShift)
            {
                isConsonant = false;

                for (int i = 0; i < keyRaws.Length; i++)
                {
                    keyRaws[i].text = tempKorKeys[i];
                }
            }
            //한글 쉬프트o
            if (isKor && isShift)
            {
                isConsonant = true;
                for (int i = 0; i < keyRaws.Length; i++)
                {
                    keyRaws[i].text = tempKorKeys[i];
                }
            }
        }

        //이름 깜빡이는 함수
        void NameOutput()
        {
            if (!isDoneButtonClicked)
            {
                //시간이 흐름 누적
                deltaTime += Time.deltaTime;
                //체크용 시간에 대입
                temp_TimeCheck = deltaTime;

                //최대 글자 10글자가 넘으면
                if (isMaxLength)
                {
                    //깜빡이 멈춤
                    blinkText = "";
                }
                else
                {
                    //1초가 지나면
                    if (temp_TimeCheck >= 1f)
                    {
                        //이름 _
                        blinkText = "_";

                        //2초가 지나면
                        if (temp_TimeCheck >= 2f)
                        {
                            //이름 
                            blinkText = "";

                            //0초로 초기화
                            deltaTime = 0;
                        }
                    }
                }//if(최대글자)

                 //오브젝트에 대입
                showMyName.text = nameText + blinkText;

            }
            else //if(isDone)엔터를 누르면
            {
                //깜빡이는 공란상태로
                blinkText = "";
                //대입
                showMyName.text = nameText;
            }//if(isDone)엔터를 누르면

            //color
            if (nameText.Length == 0)
            {
                //어둡게
                showMyName.color = fontColor_default;
            }
            else
            {
                //밝게
                showMyName.color = fontColor_bright;
            }
        }

        //쉬프트 제어 버튼 OnOffX
        public void OnClick_KeyShift_btn()
        {
            isShift = !isShift;
        }

        //한영키 제어 버튼 OnOff
        public void OnClick_KeyTranslate_btn()
        {
            isKor = !isKor;
            if (isKor)
            {
                myKorRullReset();
            }
        }

        // 지우기 버튼
        public void OnClick_DeleteKey_btn()
        {
            //Text 마지막 부터 하나씩 제거하기
            if (nameText.Length != 0)
            {
                nameText = nameText.Remove(nameText.Length - 1);

                myKorRullReset();
            }
        }

        //스페이스 버튼
        public void OnClick_Space_btn()
        {
            //띄어쓰기 또는 빈문자
            nameText += " ";
            
            myKorRullReset();
        }

        //enter버튼 함수
        public void OnClick_Done_btn()
        {
            //버튼 활성화 여부
            delete.interactable = false;
            shift.interactable = false;
            space.interactable = false;
            engKor.interactable = false;
            done.interactable = false;

            //이름짓기 완성
            isDoneButtonClicked = true;

            //이름을 대입한다. : 다른 참조에서 EnumPlayer_Information 스크립트를 참조했기 때문에 (6곳 정도)
            usingMyNewPlayer.myName.text = showMyName.text;

            notice_Text.text = "작성완료";
        }

        //초기화
        public void myKorRullReset()
        {
            //초성 중성 종성 초기화
            korCounting = 0;
            temp_Chosung = "";
            temp_Joongsung = "";
            temp_Jongsung = jongsung[0];
            isGyeupJaum = false;
        }

        void Update()
        {

            //이름 깜빡이기
            NameOutput();
            
            

            //shift와 한영키에 따른 키보드 전환
            ButtonCall();

            //한글 shift 누르면 ㅃㅉㄸㄲㅆ ㅒㅖ
            if (isConsonant)
            {
                //변화하는 것
                tempKorKeys[10] = "ㅃ";
                tempKorKeys[11] = "ㅉ";
                tempKorKeys[12] = "ㄸ";
                tempKorKeys[13] = "ㄲ";
                tempKorKeys[14] = "ㅆ";
                tempKorKeys[18] = "ㅒ";
                tempKorKeys[19] = "ㅖ";
            }
            else //쉬프트 뗌
            {
                tempKorKeys[10] = "ㅂ";
                tempKorKeys[11] = "ㅈ";
                tempKorKeys[12] = "ㄷ";
                tempKorKeys[13] = "ㄱ";
                tempKorKeys[14] = "ㅅ";
                tempKorKeys[18] = "ㅐ";
                tempKorKeys[19] = "ㅔ";
            }

            //이름이 " " 이면 곤란하다.  아무 글씨 적을때, 스페이스는 한번만
            if (nameText.Length != 0 && !name.Contains(" "))
            {
                    //스페이스 BTN 활성화 조건
                    space.interactable = true;
            }
            else
            {
                space.interactable = false;
            }

            //name의 글자가 10이상이면 작성x
            if (nameText.Length >= 10)
            {
                //입력을 할 수 없다.
                isMaxLength = true;
            }
            else
            {
                //입력을 할 수 있다.
                isMaxLength = false;
            }

            //2 이상일때 done 버튼 활성화
            if (nameText.Length >= 2 && !isDoneButtonClicked)
            {
                //이름 중복 검사
                for (int i = 0; i < saveData.gameData.myNames.Length; i++)
                {
                    //이름이 동일한 것이 있다면 
                    if (nameText == saveData.gameData.myNames[i].ToString())
                    {
                        //비활성화
                        done.interactable = false;

                        notice_Text.text = "이미 존재합니다 다른 이름을 입력해주세요";
                    }
                    if(nameText != saveData.gameData.myNames[0].ToString() &&
                        nameText != saveData.gameData.myNames[1].ToString() &&
                        nameText != saveData.gameData.myNames[2].ToString() &&
                        nameText != saveData.gameData.myNames[3].ToString())//이름이 다르다면
                    {
                        //활성화
                        done.interactable = true;
                    }
                }
            }
            else
            {
                done.interactable = false;
            }

        }//update
    }//class
}//namespace