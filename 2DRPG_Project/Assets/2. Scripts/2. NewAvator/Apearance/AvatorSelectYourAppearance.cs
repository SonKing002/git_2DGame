using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI 사용
using Main;

namespace Main
{

    // 컨텐츠
    // 0 인간, 1 요정, 2 오크, 3 마족 : (== 버튼 묶음 0)
    // 4 헤어스타일 (토글), 5 눈, 6 눈동자 4가지 : (== 버튼 묶음 1)
    // 7 윗옷, 8 바지, 9 무기 : (== 버튼 묶음 2)

    //버튼 클릭 : 게임 이미지 -> 내 스프라이트렌더러 대입
    //외형 설정 완료시 내 스프라이트 렌더러 + 능력치 -> 데이터베이스 저장

    public class AvatorSelectYourAppearance : MonoBehaviour
    {
        //선택창 끄고 켜기
        bool isSwitch; //false <-> true

        int nowContentsPage; //현재 페이지
        int nowbuttonsIndex; //현재 버튼 인덱스 위치

        //다른 오브젝트, 스크립트
        public GameObject[] contents; //선택 창 묶음 (활성 비활성)
        public ScrollRect scrollRect; //선책 창이 바뀌면 == 스크롤 연결도 바꿈

        public Button next, prev; // 종족 <-> 헤어스타일, 눈, 눈매 <-> 옷,무기
        public Text nextText, prevText; // 글씨 표시
        public GameObject[] buttons;//버튼들 묶음단위 (활성 비활성)

        void Start()
        {
            //초기화
            isSwitch = false;
            nowbuttonsIndex = 0;

            //처음 전부 비활성
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(false);
            }
            //종족선택창 활성화
            buttons[0].SetActive(true);
        }

        //선택창 컨트롤
        public void OnClick_Contents_Btn(int a)
        {
            //일단 전부 끄고
            for (int i = 0; i < contents.Length; i++)
            {
                contents[i].SetActive(false);
            }
            isSwitch = true;

            //true일때
            if (isSwitch)
            {
                //켜주고
                contents[a].SetActive(true);
                
                //현재 인덱스를 저장하고
                nowContentsPage = a;
                //(스크롤) 현재 인덱스의 페이지를 연결한다
                scrollRect.content = contents[nowContentsPage].GetComponent<RectTransform>();
            }
        }

        //인스펙터 button에 연결 호출 함수: 뒤로 갈때
        public void OnClick_PrevButtons_btn()
        {
            //전부 비활성
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(false);
            }

            //ii = 0,1,2 이 인덱스는 버튼 활성화 제어하기 위함
            nowbuttonsIndex--;
            buttons[nowbuttonsIndex].SetActive(true);

            //스위치 초기화
            isSwitch = false;
        }

        //인스펙터 button에 연결 호출 함수 : 앞으로 갈때
        public void OnClick_NextButtons_btn()
        {
            //전부 비활성
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(false);
            }

            nowbuttonsIndex++;
            buttons[nowbuttonsIndex].SetActive(true);

            //스위치 초기화
            isSwitch = false;
        }

        void Update()
        {
            //버튼 묶음 인덱스에 따라 활성 비활성
            switch (nowbuttonsIndex)
            {
                case 0://맨 앞
                    prev.interactable = false;
                    prevText.text = "";

                    next.interactable = true;
                    nextText.text = "헤어스타일 & 눈성형";
                    break;

                case 1://중간
                    prev.interactable = true;
                    prevText.text = "종족 선택";

                    next.interactable = true;
                    nextText.text = "의류 & 무기";
                    break;
                case 2://맨 뒤
                    prev.interactable = true;
                    prevText.text = "헤어스타일 & 눈성형";

                    next.interactable = false;
                    nextText.text = "";
                    break;
            }
   
            //false일때
            if (!isSwitch)
            {
                //현재 페이지 꺼준다
                contents[nowContentsPage].SetActive(false);

                //(스크롤)현재 인덱스는 없다
                scrollRect.content = null;
            }
        }//update



    }//class
}//namespace