using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;

namespace Main
{
    //��Ȳ�� ���� ī�޶� ��鸲 ����
    //�Ͻ��� ����.https://chameleonstudio.tistory.com/55
    //�ֱ��� ����.https://www.youtube.com/watch?v=FoCJG0d0Iu8
    public class ShakeCam : MonoBehaviour
    {
        UIBookCtrl uiCtrl;    //isPopUp���� �����Ҷ�, �����̴�����.. ī�޶� ��鸲 ������ ���ߵ��� �ϱ� ����
        //�������� 
        float shakeAmount = 0.05f;
        
        //���� ����
        Vector3 initPosition; //ī�޶� ��ġ
        Vector3 addShake; //����

        //�����ð� 
        float shakeTime = 0.2f;
        
        void Start()
        {
            uiCtrl = FindObjectOfType<UIBookCtrl>();
        }

        //�ٸ� ������ �Ͻ��������� ������ �Լ��� ����Ѵ�.
        public void ViberateForOneTime(float shakeScale, float shakeTimer)
        {
            shakeTime = shakeTimer;
            shakeAmount = shakeScale;
        }

        void Ctrl()
        {
            //ī�޶� �̵��ϹǷ�
            initPosition = Camera.main.transform.position;
            addShake.x = Random.value * shakeAmount * 2 - shakeAmount;
            addShake.y = Random.value * shakeAmount * 2 - shakeAmount;

            initPosition.x += addShake.x;
            initPosition.y += addShake.y;

            if (shakeTime > 0f)
            {
                //�ð� ������ŭ
                shakeTime -= Time.deltaTime;

                //ī�޶� ��������
                Camera.main.transform.position = initPosition;

            }
            else
            {
                //��
                shakeTime = 0;

            }
        }

        void Update()
        {
            //å�ڸ� ���� ���¿����� �����δ�
            if (!uiCtrl.isPopUp)
            {
                Ctrl();
            }
        }//update
    }//class
}//namespace