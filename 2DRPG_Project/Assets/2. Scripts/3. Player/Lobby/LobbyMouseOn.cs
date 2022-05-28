using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Main;

namespace Main
{
    public class LobbyMouseOn : MonoBehaviour
    {
        //애니메이션
        public Animator playerAnim;

        //스탯창 (컬러.알파값 제어)
        public Image statView;
        //스탯 글자 (컬러.알파값 제어)
        public Text[] allText = new Text[10];

        public Animator textAnim;

        //마우스가 올려져 있는지
        bool isHighLight;
        //text anim의 트리거 한번만 작동하도록 한다
        bool isTriggerCtrl;

        //랜덤용 임시 변수
        int random;

        private void Start()
        {
            playerAnim.Play("Idle");
        }
        //마우스 클릭할때
        private void OnMouseDown()
        {
            //랜덤 리액션 
            random = Random.Range(0, 2);

            if (random == 0)
            {
                //피격모션
                playerAnim.SetTrigger("Deselected");
            }
            if (random == 1)
            {
                //공격모션
                playerAnim.SetTrigger("Selected");
            }
        }

        //마우스가 들어올때 
        private void OnMouseEnter()
        {
            //캐릭터 동작 제어
            isHighLight = true;
        }

        //마우스가 나갈때
        private void OnMouseExit()
        {
            //캐릭터 동작 제어
            isHighLight = false;
       
        }


        private void Update()
        {
            //캐릭터 안으로 마우스가 들어오면
            if (isHighLight)
            {
                playerAnim.SetBool("isHighLight", true);

              
                
                //텍스트 트리거 제어용 false일때
                if (!isTriggerCtrl)
                {
                    // 애니메이션 Fade In
                    textAnim.SetTrigger("On");
                    //true
                    isTriggerCtrl = true;
                }

                /* 렉걸림 현상 -> 애니메이터를 만들어서 사용했을때 버벅이지 않음
                //스탯창이 보인다
                statView.color = Color.Lerp(statView.color, new Color(1, 1, 1, 1), 2f * Time.deltaTime);

                //글자 보인다
                for (int i = 0; i < allText.Length; i++)
                {
                    allText[i].color = Color.Lerp(allText[i].color, new Color(0.1317f, 0.1357f, 0.1981f, 1), 1f * Time.deltaTime);
                }
                */
            }
            //캐릭터 밖으로 마우스를 빼면
            else
            {
                playerAnim.SetBool("isHighLight", false);
              
                //텍스트 트리거 제어용 true일때
                if (isTriggerCtrl)
                {
                    // 애니메이션 Fade out
                    textAnim.SetTrigger("Off");

                    //false
                    isTriggerCtrl = false;
                }

            }
        }

    }//class
}//namespace