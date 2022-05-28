using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Main;

namespace Main
{

    public class MiniMapCtrl : MonoBehaviour
    {
        //�̴ϸ� â
        public GameObject miniMapView;
        public Text infrom_Text; //�ȳ� �ؽ�Ʈ

        bool isOpen;//������ Ŭ������ true ����

        private void Start()
        {
            //�ʱ�ȭ
            isOpen = false;
            infrom_Text.text = "����";
        }

        //�������� Ŭ���ϸ� �̴ϸ��� Ȯ���� �� �ִ�
        public void OnClick_CtrlMiniMap_Btn()
        {
            //����ġó�� �۵�
            isOpen = !isOpen;

            //���� ����
            if (isOpen)
            {
                //Ȱ��ȭ
                miniMapView.SetActive(true);
                infrom_Text.text = "����";
            }
            else//���� ����
            {
                //��Ȱ��ȭ
                miniMapView.SetActive(false);
                infrom_Text.text = "��ġ��";
            }
        }
    }//class
}//namespace