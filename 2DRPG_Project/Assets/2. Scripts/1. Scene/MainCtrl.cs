using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // �� �ε��ϱ� ����
using UnityEngine.UI; // ������ ����ϱ� ����
using Main;// Main


namespace Main
{
    //LoadScene ��� Ȯ���Ұ� 
    //ùȭ��: ����ȭ��
    
    public class MainCtrl : MonoBehaviour
    {
        //�� ���� 5����
        public SpriteRenderer dotLight; // �������� ���� ���� SetActive
        public GameObject sharpLight; // Arpha�� 1�� Lerp
        public SpriteRenderer smallCircleLight; //���� �ӵ��� Arpha Lerp 
        public SpriteRenderer bigCircleLight; //���� �ӵ��� Arpha Lerp 
        public SpriteRenderer starLight; // �� �� ������ Arpha Lerp

        bool isLight;//���� ���°�

        public void Start()
        {

        }

        //GameStart_btn ���� ���� ��ư
        public void OnClick_Start_btn()
        {
            //�� ��ȯ : '�� �ƹ�Ÿ' �����
            SceneManager.LoadScene("2. Lobby");
        }

        void Update()
        {
            //���� �����鼭
            dotLight.color = Color.Lerp(dotLight.color, new Color(1, 1, 1, 1), Time.deltaTime * 1f);
            if (dotLight.color.a >= 0.8f)
            {
                isLight = true;
            }

            if (isLight)
            {
                //���� ��½
                sharpLight.SetActive(true);

                //���� ������ ��
                smallCircleLight.color = Color.Lerp(smallCircleLight.color, new Color(1, 1, 1, 0.6039216f), Time.deltaTime * 1f);
                bigCircleLight.color = Color.Lerp(bigCircleLight.color, new Color(1, 1, 1, 0.6039216f), Time.deltaTime * 1f);
                starLight.color = Color.Lerp(starLight.color, new Color(1, 1, 1, 1), Time.deltaTime * 0.8f);
            }

        }

        //Quit_btn ���� ��ư
        public void OnClick_Quit_btn()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; //������
#else
            Application.Quit(); //���ø����̼�
#endif
        }

    }//class
}//namespace