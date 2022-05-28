using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Main;

namespace Main
{
    //유아이 관리자
    public class UIBookCtrl : MonoBehaviour
    {
        //받아오는 오브젝트
        public Slider menu;
        public GameObject menu_contents; //(능력치,가방, 스킬 목록) 아이콘의 부모 fill 오브젝트 받아오기 
        public GameObject joyStickPad;

        //제어
        bool isSliderClick; //삼각형을 클릭하면 true : 메뉴가 길게 나온다

        //책 관련
        public GameObject screenShielder;// 화면 보호기
        public GameObject[] books; //안내 책자 가이드
                                   //(0 애니메이션) (1 목차) (2 능력창) (3 가방,장비창) (4 스킬창) (5 설정창)
        [HideInInspector]
        public Animator bookAnim; //책 애니메이션

        [HideInInspector]
        public int bookNum; //인덱스 저장용 (책 닫을때 0 초기화)

        public bool isPopUp; // true일때 책자 창을 띄운다 
        public bool isPrevExist; // true = 페이지 이동한 적 있음, false = 처음 책을 펼침 (책 닫을때 false 초기화)
                                 //애니메이션 페이지 좌우를 구분하기 위함

        bool isScreenShielderOn; //스크린 쉴더 lerp로 사용하기 위해서 제어용
        public BookAnimatorCtrl bookAnimatorCtrl; 
        public Text bookTitle_txt; //무슨 창인지 알려주는 텍스트

        void Start()
        {
            //초기화
            isPrevExist = false;
            isSliderClick = false;
            menu.value = 0f;
            bookNum = 0;

            //책중에서
            for (int i = 0; i < books.Length; i++)
            {
                //애니메이터가 있다면
                if (books[i].GetComponent<Animator>())
                {
                    //대입을 한다.
                    bookAnim = books[i].GetComponent<Animator>();
                }
            }
            //정상 print(bookAnim.name);

            //bookAnimatorCtrl = FindObjectOfType<BookAnimatorCtrl>(); 꺼놓고 시작하기 때문에 할당을 찾을 수 없다. 널 레퍼런스 익셉션
        }

        //메뉴버튼을 누르면 호출되는 함수
        public void OnClick_SliderMenu_btn()
        {
            //true false 조건
            isSliderClick = !isSliderClick;
            print(isSliderClick);
        }

        //우상단 슬라이더 열고 닫기
        public void SliderMenu()
        {
            //true일때
            if (isSliderClick)
            {
                //메뉴를 보여준다.
                menu.value = Mathf.Lerp(menu.value, 1f , Time.deltaTime * 4f);//lerp 슬라이더 질문, lerp 화면보호 질문

                //중간에 
                if (0f < menu.value && menu.value < 1f)
                {
                    //반응 못하도록 한다.
                    menu.interactable = false;
                }

                //아이콘 보이기
                menu_contents.SetActive(true);

            }
            //false일때
            else if(!isSliderClick)
            {

                //메뉴를 가린다.
                menu.value = Mathf.Lerp(menu.value, 0f , Time.deltaTime * 4f);
                
                //중간에
                if (0f < menu.value && menu.value < 1f)
                {
                    //반응 못하도록 한다.
                    menu.interactable = false;
                }

                if (menu.value <= 0.004f)
                {
                    //아이콘 가리기
                    menu_contents.SetActive(false);
                }
            }
        }

        //팝업창 : 장비창, 인벤토리, 스킬창, 설정창 
        public void OnClick_Something_Btn(int i)
        {
            if (!isPopUp) //딱 한번만 
            {
                //스위치
                isPopUp = true;

                //제목 활성화
                bookTitle_txt.gameObject.SetActive(true);

                //책자 활성화
                books[0].SetActive(true);
                
                //화면 보호 켜기
                screenShielder.SetActive(true);
                //가상패드 끄기
                joyStickPad.SetActive(false);

                //업데이트에서 화면보호기 arpha값 제어 
                isScreenShielderOn = true;

                //저장 : bookAnimatorCtrl에서 해당 페이지 열 수 있음
                bookNum = i;

                //책자 애니메이션
                bookAnim.SetTrigger("OpenBook"); //애니메이션의 끝부분에 event function 넣어줌 : bookAnimatorCtrl

            }

            //책자를 집어넣으면
            else
            {
                // 목록창 아이콘을 눌렀다면 즉시 종료 가능
                if (bookNum == 1)
                {
                    isPopUp = false;

                    //제목 비활성화
                    bookTitle_txt.gameObject.SetActive(false);

                    //업데이트에서 화면보호기 arpha값 제어
                    isScreenShielderOn = false;

                    //화면 보호 끄기
                    screenShielder.SetActive(false);
                    //가상패드 켜기
                    joyStickPad.SetActive(true);


                    for (int ii = 0; ii < books.Length; ii++)
                    {
                        //애니메이션만 제외하고
                        if (ii != 0)
                        {
                            //전부 종료
                            books[ii].SetActive(false);
                        }
                    }
                    //책자 애니메이션
                    bookAnim.SetTrigger("CloseBook"); //애니메이션의 끝부분에 event function 넣어줌 : bookAnimatorCtrl
                }
            }
        }

        //x 를 눌렀다면 또는 뒤로가기를 눌렀다면
        public void OnClick_Exit_btn()
        {
            if (isPopUp)
            {
                //목록창에선
                if (bookNum == 1)
                {
                    isPopUp = false;

                    //제목 비활성화
                    bookTitle_txt.gameObject.SetActive(false);

                    //화면 보호기 끄라고 명령
                    isScreenShielderOn = false;

                    //화면 보호 끄기
                    screenShielder.SetActive(false);
                    //가상패드 켜기
                    joyStickPad.SetActive(true);

                    for (int ii = 0; ii < books.Length; ii++)
                    {
                        //애니메이션만 제외하고
                        if (ii != 0)
                        {
                            //전부 종료
                            books[ii].SetActive(false);
                        }
                    }
                    //책자 애니메이션
                    bookAnim.SetTrigger("CloseBook"); //애니메이션의 끝부분에 event function 넣어줌 : bookAnimatorCtrl
                }
                else //다른 어느 창에서든
                {
                    //기존 작성 활용
                    bookAnimatorCtrl.SendMessage("RealOpen",-bookNum +1); 
                    //bookAnimatorCtrl.RealOpen(); leftTurn애니메이션 동작하게 하고 bookNum = 1 목록창을 띄운다
                }
            }
        }

        //특정 페이지로 이동시키기
        public void OnClick_GotoPage_btn(int i)
        {
            //+페이지
            bookAnimatorCtrl.SendMessage("RealOpen", i);
        }


        //키보드 입력관리
        void KeyboardInput()
        {

            //I 키보드에서 인벤토리창을 열기
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (!isPopUp) //한번만 실행
                {
                    OnClick_Something_Btn(3);
                }
            }
            //K 키보드에서 스킬창을 열기
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (!isPopUp)
                {
                    OnClick_Something_Btn(4);
                }
            }
            //U 키보드에서 능력치를 열기
            if (Input.GetKeyDown(KeyCode.L))
            {
                if (!isPopUp)
                {
                    OnClick_Something_Btn(2);
                }
            }
            //ESC 키보드에서 설정창 -> 목록 -> 닫기 
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!isPopUp)
                {
                    //설정창
                    OnClick_Something_Btn(5);
                }
                else
                {
                    //뒤로가기, 창 나가기
                    OnClick_Exit_btn();
                }
            }
        }//키보드

        void Update()
        {
            //쉴더가 켜지면
            if (isScreenShielderOn)
            {
                //스크린 실더 켜기
                screenShielder.SetActive(true);
                //가상패드 끄기
                joyStickPad.SetActive(false);

                //시간 멈추기
                Time.timeScale = Mathf.Lerp(Time.timeScale, 0f, 4f);
                //애니메이터 시간 제한x
                bookAnim.updateMode = AnimatorUpdateMode.UnscaledTime;
            }
            else//쉴더가 꺼지면
            {
                //스크린 실더 끄기
                screenShielder.SetActive(false);
                //가상패드 켜기
                joyStickPad.SetActive(true);

                //시간 정상
                Time.timeScale = Mathf.Lerp(Time.timeScale, 1f, 4f);
                //애니메이터 시간 영향
                if (Time.timeScale == 1)
                {
                    bookAnim.updateMode = AnimatorUpdateMode.Normal;
                }
            }

            //대입

            //슬라이더 제어
            SliderMenu();

            //제목 글씨 표시용 (팝업상태일때, 페이지 전환이 아닐때
            if (isPopUp)
            {
                switch (bookNum)
                {

                    case 1:
                        bookTitle_txt.text = "1장. 목차";
                        break;
                    case 2:
                        bookTitle_txt.text = "2장. 기본 능력치";
                        break;
                    case 3:
                        bookTitle_txt.text = "3장. 가방 & 장비";
                        break;
                    case 4:
                        bookTitle_txt.text = "4장. 스킬 창";
                        break;
                    case 5:
                        bookTitle_txt.text = "5 장. 설정";
                        break;
                }//1,2,3,4,5 
             
            }//isPopUp

            //키보드 컨트롤
            KeyboardInput();
            
        }
    }//class
}//namespace