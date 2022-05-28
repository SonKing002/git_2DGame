using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;
using UnityEngine.UI;

namespace Main
{
    //애니메이션에 의한 페이지 변화를 담당
    public class BookAnimatorCtrl : MonoBehaviour
    {
        //타 스크립트
        UIBookCtrl uiCtrl;
        public Button turn_LeftPage;
        public Button turn_RightPage;

        //백업용 페이지
        int prev_BookNum;//열었을때 아이콘이랑 닫을때 아이콘이 다를 수 있는 경우 == 열었을때 인덱스 번호 저장
        [HideInInspector]
        public bool isPageChangeMotion; //true 모션 변하는 중
        void Start()
        {
            //Find 할당
            uiCtrl = FindObjectOfType<UIBookCtrl>();
        }

        // 오픈하는 트리거 애니메이션이 끝날때 호출 함수 RealOpen, (전 페이지 전, 다음 아이콘) 버튼 누를때 호출 함수 
        public void RealOpen(int i)//(int i는 book인덱스에 해당하는 것을 열어달라고 요청)
        {
            //백업 페이지 = 열었을때의 인덱스 저장
            prev_BookNum = uiCtrl.bookNum;

            //애니메이션 비교 재생역할, 기능은 (event function에서 실제 사용할 함수는 RealTurn...함수에 제작)
            if (!uiCtrl.isPrevExist)// 처음 펼칠때
            {
                //오른쪽 애니메이션 후, 
                uiCtrl.bookAnim.SetTrigger("PageTurnRight");
                //백업 페이지 존재
                uiCtrl.isPrevExist = true;
            }
            else// 백업 페이지가 있는 경우
            {
                //버튼 (전페이지 : i = -1, 다음 페이지 : i = +1);
                uiCtrl.bookNum = uiCtrl.bookNum + i;

                //백업 페이지 인덱스보다 큰 경우 
                if (uiCtrl.bookNum > prev_BookNum)
                {
                    //백업페이지 비활성화
                    uiCtrl.books[prev_BookNum].SetActive(false);

                    //오른쪽으로 페이지를 넘기는 애니메이션
                    uiCtrl.bookAnim.SetTrigger("PageTurnRight");
                }
                else//백업 페이지 인덱스가 큰 경우
                {
                    //백업페이지 비활성화
                    uiCtrl.books[prev_BookNum].SetActive(false);

                    //왼쪽으로 페이지를 넘기는 애니메이션
                    uiCtrl.bookAnim.SetTrigger("PageTurnLeft");
                }
            }
        }//void RealOpen()


        //오른쪽 넘기는 트리거 애니메이션이 끝날때 호출 함수
        public void RealTurnRight()
        {
            //처음 펼친 페이지라면
            if (!uiCtrl.isPrevExist)
            {
                //페이지 활성화
                uiCtrl.books[uiCtrl.bookNum].SetActive(true);
            }
            else //다음페이지로 넘어갈때
            {
                //안전장치
                if (uiCtrl.bookNum <= 5)
                {
                    //해당 페이지로 이동
                    uiCtrl.books[uiCtrl.bookNum].SetActive(true);
                }
            }
        }
        //왼쪽 넘기는 트리거 애니메이션이 끝날때 호출 함수
        public void RealTurnLeft()
        {
            //안전장치
            if (uiCtrl.bookNum >= 1)
            {
                //해당 페이지로 이동
                uiCtrl.books[uiCtrl.bookNum].SetActive(true);
            }
        }

        //클로즈하는 트리거 애니메이션이 끝날때 호출 함수
        public void RealClose()
        {
            //전부 0 초기화
            prev_BookNum = uiCtrl.bookNum = 0;
            
            //애니메이션이 끝나고 마지막 남은 책 오브젝트 비활성화
            gameObject.SetActive(false);
        }

        void Update()
        {
            //페이지 넘기는 버튼 활성화 조건
            switch (uiCtrl.isPopUp)
            {
                case true:
                    turn_LeftPage.gameObject.SetActive(true);
                    turn_RightPage.gameObject.SetActive(true);

                    //맨 첫페이지 이하라면
                    if (uiCtrl.bookNum <= 1)
                    {
                        turn_LeftPage.interactable = false;
                    }
                    else
                    {
                        turn_LeftPage.interactable = true;
                    }
                    //맨 뒷페이지이거나 애니메이션 페이지라면
                    if (uiCtrl.bookNum == 5 || uiCtrl.bookNum == 0)
                    {
                        turn_RightPage.interactable = false;
                    }
                    else
                    {
                        turn_RightPage.interactable = true;
                    }
                    break;

                default:
                    turn_LeftPage.gameObject.SetActive(false);
                    turn_RightPage.gameObject.SetActive(false);
                    break;
            }
        }//update
    }//class
}//namespace