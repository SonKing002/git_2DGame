using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Main;

namespace Main
{

    public class MiniMapCtrl : MonoBehaviour
    {
        //미니맵 창
        public GameObject miniMapView;
        public Text infrom_Text; //안내 텍스트

        bool isOpen;//지구본 클릭여부 true 열기

        private void Start()
        {
            //초기화
            isOpen = false;
            infrom_Text.text = "접기";
        }

        //지구본을 클릭하면 미니맵을 확인할 수 있다
        public void OnClick_CtrlMiniMap_Btn()
        {
            //스위치처럼 작동
            isOpen = !isOpen;

            //오픈 상태
            if (isOpen)
            {
                //활성화
                miniMapView.SetActive(true);
                infrom_Text.text = "접기";
            }
            else//닫힌 상태
            {
                //비활성화
                miniMapView.SetActive(false);
                infrom_Text.text = "펼치기";
            }
        }
    }//class
}//namespace