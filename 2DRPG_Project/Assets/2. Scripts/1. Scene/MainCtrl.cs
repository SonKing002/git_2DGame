using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 로드하기 위함
using UnityEngine.UI; // 유아이 사용하기 위함
using Main;// Main


namespace Main
{
    //LoadScene 사용 확인할것 
    //첫화면: 메인화면
    
    public class MainCtrl : MonoBehaviour
    {
        //빛 연출 5가지
        public SpriteRenderer dotLight; // 산위에서 빛이 나고 SetActive
        public GameObject sharpLight; // Arpha가 1로 Lerp
        public SpriteRenderer smallCircleLight; //같은 속도로 Arpha Lerp 
        public SpriteRenderer bigCircleLight; //같은 속도로 Arpha Lerp 
        public SpriteRenderer starLight; // 한 턴 느리게 Arpha Lerp

        bool isLight;//빛이 나는가

        public void Start()
        {

        }

        //GameStart_btn 게임 시작 버튼
        public void OnClick_Start_btn()
        {
            //씬 전환 : '새 아바타' 만들기
            SceneManager.LoadScene("2. Lobby");
        }

        void Update()
        {
            //빛이 나오면서
            dotLight.color = Color.Lerp(dotLight.color, new Color(1, 1, 1, 1), Time.deltaTime * 1f);
            if (dotLight.color.a >= 0.8f)
            {
                isLight = true;
            }

            if (isLight)
            {
                //빛이 번쩍
                sharpLight.SetActive(true);

                //원이 나오게 됨
                smallCircleLight.color = Color.Lerp(smallCircleLight.color, new Color(1, 1, 1, 0.6039216f), Time.deltaTime * 1f);
                bigCircleLight.color = Color.Lerp(bigCircleLight.color, new Color(1, 1, 1, 0.6039216f), Time.deltaTime * 1f);
                starLight.color = Color.Lerp(starLight.color, new Color(1, 1, 1, 1), Time.deltaTime * 0.8f);
            }

        }

        //Quit_btn 종료 버튼
        public void OnClick_Quit_btn()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; //에디터
#else
            Application.Quit(); //어플리케이션
#endif
        }

    }//class
}//namespace