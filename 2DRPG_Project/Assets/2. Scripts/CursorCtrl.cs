using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;//Ui
using Main;
using UnityEngine.EventSystems;//이벤트 처리

namespace Main
{
    //싱글톤 제레릭 함수 상속
    public class CursorCtrl : MonoBehaviour
    {   
        //커서
        public GameObject cursor;
        //커서 이펙트
        public Animator clickEffect;

        //커서 애니메이션
        public Animator cursorCtrl;
        //커서 상태
        public Image additionalState;

        //이미지 파츠 제어
        public GameObject[] onlyDrag = new GameObject[5];
        int partIndex;

        //누르고 있는 중
        public bool isClick;

        GameObject selectedGameObejct;

        public ItemType tab = ItemType.Equiptable;
        public EquipParts item = EquipParts.None;

        void Start()
        {
            //윈도우 커서 꺼주기
            Cursor.visible = false;
            //커서 잠그기
            Cursor.lockState = CursorLockMode.Confined;

            //초기화
            clickEffect.gameObject.SetActive(false);
        }

        //참고.https://yoonstone-games.tistory.com/70
        void Hit()
        {
            //0은 슬롯 선택용
            for (int i = 1; i < 6; i++)
            {
                //전부 일단 비활성화
                onlyDrag[i - 1].SetActive(false);

                //게임오브젝트가 활성화 되어있다면
                if (selectedGameObejct.transform.GetChild(i).gameObject.activeSelf)
                {
                    // i : 1 = 아머, 2 = 옷, 3 = 바지, 4 = 무기, 5 = 이외 (-1씩)
                    //해당 오브젝트에 활성화
                    onlyDrag[i - 1].SetActive(true);

                    //그 오브젝트의 인덱스를 기억
                    partIndex = i;

                    //아이템 파츠
                    item = selectedGameObejct.GetComponent<SlotUnit>().itemPart;
                }
            }
        }

        //마우스 커서 기본 설정
        void CursorCtrllor()
        {
            //보정
            cursor.transform.position = Input.mousePosition + new Vector3(30, -30, 0);

            //마우스 클릭할때
            if (Input.GetMouseButtonDown(0))
            {
                //마우스 클릭 애니메이션 재생하기 위함
                cursorCtrl.SetTrigger("Click");

                //방금 클릭한 게임 오브젝트를 저장
                selectedGameObejct = EventSystem.current.currentSelectedGameObject;

                //빈 오브젝트 클릭이 아닐때
                if (selectedGameObejct != null)
                {
                    //잘 가져왔는지 확인용
                    print("지금 선택한 오브젝트는 " + selectedGameObejct.name + ", " + "테그는 " + selectedGameObejct.tag);
                    if (selectedGameObejct.name.Contains("Equiptble"))
                    {
                        tab = ItemType.Equiptable;
                    }
                    if (selectedGameObejct.name.Contains("Consumable"))
                    {
                        tab = ItemType.Consumable;
                    }
                    if (selectedGameObejct.name.Contains("Etc"))
                    {
                        tab = ItemType.Etc;
                    }

                    //유닛 슬롯이라는 명칭이 포함되어있다면
                    if (selectedGameObejct.name.Contains("Unit_Slot1"))
                    {
                        //활성화
                        isClick = true;
                    }
                }

                //마우스 클릭 이펙트 활성화
                clickEffect.GetComponent<SetActive>().Make_SetActive_On();

                //이펙트 애니메이션 작동
                clickEffect.SetTrigger("FadeShine");
            }

            //클릭이 활성화 되었을때 == 누르고 있는 동안
            if (isClick && selectedGameObejct != null)
            {
                Hit();
            }

            //마우스에서 손을 뗄 때
            if (Input.GetMouseButtonUp(0))
            {
                isClick = false;
                cursorCtrl.SetBool("isDefault", true);

                onlyDrag[0].SetActive(false);
                onlyDrag[1].SetActive(false);
                onlyDrag[2].SetActive(false);
                onlyDrag[3].SetActive(false);
                onlyDrag[4].SetActive(false);

                item = EquipParts.None;
            }
        }

        void Update()
        {
            CursorCtrllor();

        }//update



    }//class
}//namespace
