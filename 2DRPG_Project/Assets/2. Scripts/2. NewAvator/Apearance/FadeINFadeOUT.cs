using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Main;

namespace Main
{
    //SetActive (true false) 의 느낌이 흐름을 끊어버리는 것 같아서 컬러의 알파값으로 연출
    //네이밍 규칙 설명 : 에디터에서 SetActive를 false로 두어야 한다. ( _ + name )
    //네이밍 규칙 설명 : 에디터에서 color 알파값를 0로 두어야 한다.  ( . + name )

    public class FadeINFadeOUT : MonoBehaviour
    {
        //좌하단
        bool isClick; //true false 

        int num;
        public float transitionSpeed;//화면전환 조금 더 편리하게
        public Image[] uiImages;// 컨트롤할 대상  . + name
        public Text[] uiTexts; // 컨트롤할 텍스트 . + name
        public GameObject[] canvases; //컨트롤할 캔버스

        private void Start()
        {
            if (canvases.Length > 0)
            {
                //캔버스 끄기
                canvases[0].SetActive(false);
                canvases[1].SetActive(false);
            }
        }

        private void Update()
        {
            if (!isClick)//클릭 전이라면
            {
                if (canvases.Length > 0)//안전장치
                {
                    //캔버스 끄기
                    canvases[0].SetActive(false);
                    canvases[1].SetActive(false);
                }

                if (uiImages.Length > 0)//안전장치
                {
                    for (int i = 0; i < uiImages.Length; i++)
                    {
                        //이미지의 알파값을 0으로 lerp
                        uiImages[i].color = Color.Lerp(uiImages[i].color, new Color(uiImages[i].color.r, uiImages[i].color.g, uiImages[i].color.b, 0f), Time.deltaTime * transitionSpeed);
                    }
                }

                if (uiTexts.Length > 0)//안전장치
                {
                    for (int i = 0; i < uiTexts.Length; i++)
                    {
                        //텍스트 컬러의 알파값을 0으로 lerp
                        uiTexts[i].color = Color.Lerp(uiTexts[i].color, new Color(uiTexts[i].color.r, uiTexts[i].color.g, uiTexts[i].color.b, 0f), Time.deltaTime * transitionSpeed);
                    }
                }
            }
            else//클릭 이후라면
            {
                if (uiImages.Length > 0)//안전장치
                {
                    for (int i = 0; i < uiImages.Length; i++)
                    {
                        //이미지의 알파값을 1로 lerp
                        uiImages[i].color = Color.Lerp(uiImages[i].color, new Color(uiImages[i].color.r, uiImages[i].color.g, uiImages[i].color.b, 1f), Time.deltaTime * transitionSpeed);
                    }
                }

                if (uiTexts.Length > 0)//안전장치
                {
                    for (int i = 0; i < uiTexts.Length; i++)
                    {
                        //텍스트 컬러의 알파값을 1로 lerp
                        uiTexts[i].color = Color.Lerp(uiTexts[i].color, new Color(uiTexts[i].color.r, uiTexts[i].color.g, uiTexts[i].color.b, 1f), Time.deltaTime * transitionSpeed);
                    }
                }
                //uiImage[num].color = Color.Lerp(uiImage[num].color, new Color(uiImage[num].color.r, uiImage[num].color.g, uiImage[num].color.b, 1f), Time.deltaTime * 3f);
            }
        }
        public void OnClick_AllFadeOut_btn()
        {
            isClick = false;
        }

        public void OnClick_AllFadeIn_btn()
        {
            isClick = true;

            if (canvases.Length > 0)//안전장치
            {
                //캔버스 켜기
                canvases[0].SetActive(true);
            }
        }
        
        public void ChooseOne_FadeContrl(int i)
        {
            num = i;
        }
    }
}