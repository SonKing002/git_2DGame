using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;
using UnityEngine.UI;

namespace Main
{
    //collider�� üũ�� �Ŀ�, ZŰ�� ������ lerp�� �÷��̾� ��ġ���� �������� ����´�.
    //ZŰ�� ���� ���� + �÷��̾� �ݶ��̴��� �ε����� = ȹ�����

    public class PlayerTrigger : MonoBehaviour
    {
        //�� �ݶ��̴� �ȿ� ���Դ��� üũ
        bool isEnter;

        //npc �̸��
        public Animator equiptable;
        public Animator consumable;
        public Animator skill;
        public Animator bagStore;
        public Animator stage;

        //�Ϲ� ���� �� ���
        public GameObject selectView;
        //�Ϲ� ���� ���� �Ǹ� ���
        public GameObject objectListView;
        //�������� ���
        public GameObject stageView;
        //���� ��Ʈ�� GUIText
        public Text text_SelectViewHead;
        //�ӽ�_ĳ���Ϳ� ���� Object
        GameObject tempEnteredGameObject;

        //�������� velocity�� �̿�
        Rigidbody2D item;

        //ĳ����
        public PlayerMove playerMove;

        //�������� ���� ����� �̿��Ͽ� ������ ����
        float player_Destination;
        float destination;
        //���ǵ�
        float dragSpeed;

        //�ð� ���
        float timeCheck;
        float tempTime;
        void Start()
        {
            //player = FindObjectOfType<PlayerMove>().gameObject;
            dragSpeed = 2f;
        }

        void Update()
        {
            /*
            //Z��ư�� ������ ������ && �ݶ��̴� ������ ��������
            if (Input.GetKey(KeyCode.Z) && isEnter)
            {
                //�ð� ����
                timeCheck = Time.deltaTime;
                //�ð� ĳġ��
                tempTime = timeCheck;

                //�ϴ÷� �� ���ٰ�
                if (tempTime < 0.5f)
                {
                    item.AddForce(Vector2.up * Time.deltaTime, ForceMode2D.Force);
                }
                else //�÷��̾�� ���󰣴�
                {
                    destination = Vector2.Distance(player.transform.position, item.gameObject.transform.position);
                    print(isEnter);
                    //�ش� �������� ĳ���͸� ����´� //���� ����
                    player_Destination = player.transform.position.x - item.gameObject.transform.position.x;
                    player_Destination = (player_Destination < 0) ? -1 : 1;
                }

                //���� * �ӵ� = �Ÿ�
                item.velocity = new Vector2(player_Destination * dragSpeed, item.velocity.y);

                //������ �����ߴٸ�
                if (destination <= 1.05f)
                {
                    //�ı��ϱ�
                    Destroy(item.gameObject);
                }
            }
            else //���ų� �ڸ��� �־�����
            {
                //null ����
                if (item.gameObject.activeSelf == true)
                {
                    //�����
                    item.velocity = new Vector2(0, 0);
                }
            }

            */

        }//update

        //������ �����ϴ� ����
        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case "NPC_Skill":

                    skill.SetTrigger("question");
                    skill.SetBool("isQuestion",true);

                    print("��ų ����");
                    break;
                case "NPC_InventoryStore":

                    bagStore.SetTrigger("question");
                    bagStore.SetBool("isQuestion", true);

                    print("â��");
                    break;
                case "NPC_Equip":

                    equiptable.SetTrigger("question");
                    equiptable.SetBool("isQuestion", true);

                    print("��� ����");
                    break;
                case "NPC_Consum":

                    consumable.SetTrigger("question");
                    consumable.SetBool("isQuestion", true);

                    print("�Һ� ����");
                    break;
                case "NPC_Stage":

                    stage.SetTrigger("question");
                    stage.SetBool("isQuestion", true);
                    stageView.SetActive(true);

                    print("��������");
                    break;
                case "Sitting":
                    //���ڿ� �ٰ���
                    playerMove.isChairFind = true;
                    //��ư Ȱ��ȭ
                    playerMove.else_Act_btn.interactable = true;

                    break;

                case "Item":

                    print("������ ã��");
                    //�������� ����
                    item = collision.gameObject.GetComponent<Rigidbody2D>();
                    print(item.name);
                    //�������� �����ȿ� ����
                    isEnter = true;
                    break;

                case "Ladder":
                    print("��ٸ� ����");
                    //��ٸ� ã�� true
                    playerMove.isLadderFind = true;

                    playerMove.else_Act_btn.interactable = true;
                    break;

                case "Potal":
                    //���� �� �̵�
                    playerMove.isUsePotal = true;
                    break;
            }
        }

        //�־����� ����
        private void OnTriggerExit2D(Collider2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case "NPC_Skill":

                    skill.SetBool("isQuestion", false);
                    selectView.SetActive(false);

                    print("��ų ����");
                    break;
                case "NPC_InventoryStore":

                    bagStore.SetBool("isQuestion", false);
                    selectView.SetActive(false);

                    print("â��");
                    break;
                case "NPC_Equip":

                    equiptable.SetBool("isQuestion", false);
                    selectView.SetActive(false);

                    print("��� ����");
                    break;
                case "NPC_Consum":

                    consumable.SetBool("isQuestion", false);
                    selectView.SetActive(false);

                    print("�Һ� ����");
                    break;
                case "NPC_Stage":

                    stage.SetBool("isQuestion", false);
                    stageView.SetActive(false);
                    
                    print("��������");
                    break;
                case "Sitting":
                    //���ڿ��� ���
                    playerMove.isChairFind = false;
                    //��ư ��Ȱ��ȭ
                    playerMove.else_Act_btn.interactable = false;

                    break;

                case "Item":
                    isEnter = false;
                    break;

                case "Ladder":
                    print("��ٸ� ���");
                    //��ٸ� ã�� false
                    playerMove.isLadderFind = false;
                    playerMove.isStartLadder = false;

                    //mask �߰�
                    playerMove.Gruond_ColliderMask_Ctrl(true);
                    break;

                case "Potal":
                    //���� �� �̵�
                    playerMove.isUsePotal = true;
                    break;
            }
        }

        //npc �̸���� Ŭ���ϸ�, â�� Ȱ��ȭ (�����϶� ȭ�� ������ ����)
        public void OnClick_PopUpedEmotion_Btn()
        {
            selectView.SetActive(true);
        }

    }//class
}//namespace