using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;
using UnityEngine.UI;

namespace Main
{

    public class Title_Motion : MonoBehaviour
    {
        //아랫마을 타이틀
        //검은 테두리 144 width ~ 874.2355
        //흰 테두리   255 width ~ 887.73
        public Image whiteBG_Color;
        public Image blackBG_Color;
        public Image background_Color;
        public Image background2_Color;
        public Text text1;
        public Text text2;
        Color arphaZero;
        Vector3 tempSize;
        //Color tempColor;
        //Color tempColor2;
        //Color tempColor3;
        //Color tempColor4;

        float time;
        float tempTime;

        //int count;

        void Start()
        {
            //백업
            //count = 0;
            arphaZero = new Color(1, 1, 1, 0);
            //tempColor = background_Color.color;
            //tempColor2 = whiteBG_Color.color;
            //tempColor3 = text1.color;
            //tempColor4 = text2.color;
            tempSize = background_Color.rectTransform.sizeDelta;

            //초기화
            /*
            background_Color.color = arphaZero;
            whiteBG_Color.color = arphaZero;
            text1.color = arphaZero;
            text2.color = arphaZero;
            */
        }

        void Title_FadeIn()
        {
            //동작 구분
            /*
            if (count == 0)
            {
                //컬러 페이드 인
                //background_Color.color = Color.Lerp(arphaZero, tempColor, 1.2f * Time.deltaTime);

                //whiteBG_Color.color = Color.Lerp(arphaZero, tempColor2, 1.2f * Time.deltaTime);
                //text1.color = Color.Lerp(arphaZero, tempColor3, 1.2f * Time.deltaTime);
                //text2.color = Color.Lerp(arphaZero, tempColor4, 1.2f * Time.deltaTime);

                if (text2.color.a >= 1f)
                {
                    //동작 끝
                    //count = 1;
                }
            }
            */
        }//title_FadeIn()

        void Title_FadeOut()
        {
            //동작구분 if(count ==1)
           
            background_Color.color = Color.Lerp(background_Color.color, arphaZero, 2 * Time.deltaTime);
            background2_Color.color = Color.Lerp(background2_Color.color, arphaZero, 1.2f * Time.deltaTime);

            whiteBG_Color.color = Color.Lerp(whiteBG_Color.color, arphaZero, 1.5f * Time.deltaTime);
            blackBG_Color.color = Color.Lerp(blackBG_Color.color, arphaZero, 1.5f * Time.deltaTime);
            text1.color = Color.Lerp(text1.color, arphaZero, 1.5f * Time.deltaTime);
            text2.color = Color.Lerp(text2.color, arphaZero, 1.5f * Time.deltaTime);

            //페이드 아웃 중에 오브젝트 파괴
            if (text1.color.a <= 0.02f)
            {
                Destroy(blackBG_Color.gameObject);//재사용 안함
            }
           
        }//title_FadeOut()

        void Update()
        {
            //시간 재기
            time += Time.deltaTime;
            tempTime = time;

            //연출
            background_Color.rectTransform.sizeDelta 
                = Vector3.Lerp(background_Color.rectTransform.sizeDelta, tempSize * 2f, 0.2f * Time.deltaTime);

            //Title_FadeIn();
            if (tempTime >= 1f)
            {
                Title_FadeOut();
            }
        }
    }//class
}//namespace