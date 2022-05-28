using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;//Ui
using Main;
using UnityEngine.EventSystems;//�̺�Ʈ ó��

namespace Main
{
    //�̱��� ������ �Լ� ���
    public class CursorCtrl : MonoBehaviour
    {   
        //Ŀ��
        public GameObject cursor;
        //Ŀ�� ����Ʈ
        public Animator clickEffect;

        //Ŀ�� �ִϸ��̼�
        public Animator cursorCtrl;
        //Ŀ�� ����
        public Image additionalState;

        //�̹��� ���� ����
        public GameObject[] onlyDrag = new GameObject[5];
        int partIndex;

        //������ �ִ� ��
        public bool isClick;

        GameObject selectedGameObejct;

        public ItemType tab = ItemType.Equiptable;
        public EquipParts item = EquipParts.None;

        void Start()
        {
            //������ Ŀ�� ���ֱ�
            Cursor.visible = false;
            //Ŀ�� ��ױ�
            Cursor.lockState = CursorLockMode.Confined;

            //�ʱ�ȭ
            clickEffect.gameObject.SetActive(false);
        }

        //����.https://yoonstone-games.tistory.com/70
        void Hit()
        {
            //0�� ���� ���ÿ�
            for (int i = 1; i < 6; i++)
            {
                //���� �ϴ� ��Ȱ��ȭ
                onlyDrag[i - 1].SetActive(false);

                //���ӿ�����Ʈ�� Ȱ��ȭ �Ǿ��ִٸ�
                if (selectedGameObejct.transform.GetChild(i).gameObject.activeSelf)
                {
                    // i : 1 = �Ƹ�, 2 = ��, 3 = ����, 4 = ����, 5 = �̿� (-1��)
                    //�ش� ������Ʈ�� Ȱ��ȭ
                    onlyDrag[i - 1].SetActive(true);

                    //�� ������Ʈ�� �ε����� ���
                    partIndex = i;

                    //������ ����
                    item = selectedGameObejct.GetComponent<SlotUnit>().itemPart;
                }
            }
        }

        //���콺 Ŀ�� �⺻ ����
        void CursorCtrllor()
        {
            //����
            cursor.transform.position = Input.mousePosition + new Vector3(30, -30, 0);

            //���콺 Ŭ���Ҷ�
            if (Input.GetMouseButtonDown(0))
            {
                //���콺 Ŭ�� �ִϸ��̼� ����ϱ� ����
                cursorCtrl.SetTrigger("Click");

                //��� Ŭ���� ���� ������Ʈ�� ����
                selectedGameObejct = EventSystem.current.currentSelectedGameObject;

                //�� ������Ʈ Ŭ���� �ƴҶ�
                if (selectedGameObejct != null)
                {
                    //�� �����Դ��� Ȯ�ο�
                    print("���� ������ ������Ʈ�� " + selectedGameObejct.name + ", " + "�ױ״� " + selectedGameObejct.tag);
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

                    //���� �����̶�� ��Ī�� ���ԵǾ��ִٸ�
                    if (selectedGameObejct.name.Contains("Unit_Slot1"))
                    {
                        //Ȱ��ȭ
                        isClick = true;
                    }
                }

                //���콺 Ŭ�� ����Ʈ Ȱ��ȭ
                clickEffect.GetComponent<SetActive>().Make_SetActive_On();

                //����Ʈ �ִϸ��̼� �۵�
                clickEffect.SetTrigger("FadeShine");
            }

            //Ŭ���� Ȱ��ȭ �Ǿ����� == ������ �ִ� ����
            if (isClick && selectedGameObejct != null)
            {
                Hit();
            }

            //���콺���� ���� �� ��
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
