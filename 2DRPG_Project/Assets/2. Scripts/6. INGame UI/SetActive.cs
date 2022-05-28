using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;

namespace Main
{
    //������Ʈ Ǯ�� ����� ������, ������Ʈ Ȱ��ȭ �����Ҷ� ��ũ��Ʈ
    public class SetActive : MonoBehaviour
    {
        //
        bool isSwitchON;

        //ȣ�� �Լ�
        public void Make_SetActive_On()
        {
            gameObject.SetActive(true);
        }

        //�ִϸ��̼� ������ �ٿ����� ȣ�� �Լ�
        public void Make_SetActive_Off()
        {
            gameObject.SetActive(false);
        }

        //��ġ �ݺ��� �Լ�
        public void Switch()
        {
            //����
            isSwitchON = !isSwitchON;

            //true�϶�
            if (isSwitchON)
            {
                //Ȱ��ȭ
                gameObject.SetActive(true);
            }
            //false�϶�
            else
            {
                //��Ȱ��ȭ
                gameObject.SetActive(false);
            }
        }

    }//class
}//namespace
