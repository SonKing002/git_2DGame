using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Main;
using UnityEngine.EventSystems;
using TMPro;

namespace Main
{
    //���� , �ǰ� , ���
    public class Player_Attack_Defend : MonoBehaviour
    {
        //������Ʈ
        [HideInInspector]
        public Animator anim; //�ִϸ��̼� : ����,�ǰ�,���
        PlayerMove playerMove; //������ ��ũ��Ʈ: �� üũ ��������
        public PlayerWeaponTrigger playerWeaponTrigger_Script; //�ݶ��̴� üũ : ������ ��� ����
        public CapsuleCollider2D playerWeaponTrigger_Component;
        //EnumPlayer_Information enumPlayer_Equipment; //���� �� ��� ��ũ��Ʈ : ����ҷ�����

        //Ÿ ������Ʈ
        public Slider popUpHp; //ui ������ ü��
        public Slider popUpMp; //ui ������ ����
        public TextMeshProUGUI playerCurrentHP;//���� ���� ü�� text

        public Animator attackEffect;
        public Animator attackedEffect;
        ShakeCam shakeCam;
        public UsePopUp damagedUp;
        //MonsterCtrl monsterCtrl; //Enemy ��ũ��Ʈ

        //���̽�ƽ ���
        public Button basicAttack_btn;
        public Button shield_btn;
        public Button skill2_btn;
        public Button skill3_btn;

        //�ӽ� ����
        [HideInInspector]
        public float hp; //�÷��̾� ü��
        
        int randomMotion; // ���ݸ��: 0�� ������ ���, 1�̳����� �ֵθ���

        //�ǰݽ� �÷� �� �ӽ� ����
        [HideInInspector]
        public Color temp_damgedEffectColor;

        //���� ����
        public int attackCount;  // ���� Ƚ�� ���ѿ� �Ϲݰ��� 1ȸ ��ų���� ��ȸ..
        public bool isDefense; // ���� ������ ���� ���� ���

        void Start()
        {
            //�ӽ�
            hp = 100;

            // �Ҵ�
            anim = GetComponentInChildren<Animator>(); //�ִϸ��̼�
            playerMove = GetComponent<PlayerMove>(); //ĳ���� ������ ��ũ��Ʈ
            //enumPlayer_Equipment = GetComponent<EnumPlayer_Information>(); //���� ��� ��ũ��Ʈ

            // Find �Ҵ�
            //monsterCtrl = FindObjectOfType<MonsterCtrl>(); //����
            popUpHp = GameObject.Find("SliderPlayerHp").GetComponent<Slider>(); //ü��
            popUpMp = GameObject.Find("SliderPlayerMp").GetComponent<Slider>(); //����
            shakeCam = FindObjectOfType<ShakeCam>();
        }

        //���� ��� ����
        public void Attack()
        {
            if (attackCount == 0 && !playerMove.isSitting)
            {
                //���� ī��Ʈ ����
                attackCount++;

                //������Ʈ Ȱ��ȭ
                playerWeaponTrigger_Component.enabled = true;

                //���� ��� ����
                randomMotion = Random.Range(0, 2);

                //���� ����Ʈ

                //������ ����
                if (randomMotion == 0)
                {
                    // �ִϸ��̼� ȣ�� Receiver(�ִϸ��̼ǿ��� �ϸ�ũ�� ���� ���� ���� �ۿ�)
                    anim.SetTrigger("stab");
                }
                //���߿��� ����
                else if (randomMotion == 1)
                {
                    anim.SetTrigger("swing");
                }
            }
        }

        //������� , MonsterColl�κ��� SendMessage�� ���� ���� �޴� �Լ�
        public void Damaged()
        {
            if (isDefense)
            {
                //��� ����Ʈ
                attackedEffect.SetTrigger("Defense");
                shakeCam.ViberateForOneTime(0.02f, 0.1f); //0.005�� �������� 0.5f�� ���� ���� ��鸲

            }
            //�� �ϰ� ���� ������
            else
            {
                //�ǰ� ����Ʈ
                attackedEffect.SetTrigger("Damaged");
                shakeCam.ViberateForOneTime(0.07f,0.2f); //0.005�� �������� 0.5f�� ���� ���� ��鸲

                //�ӽ�
                hp -= 10;
                popUpHp.value = hp * 0.01f;
                playerCurrentHP.text = hp + "/" + "100";

                if (hp > 0)
                {
                    anim.SetBool("isDamaged", true);//�ִϸ��̼� ���� true
                    anim.SetTrigger("damaged");

                    //������ ������ text ���� ȣ���Լ�
                    damagedUp.WhichDamaged(gameObject);
                }
                else
                {
                    playerCurrentHP.text = "0" + "/" + "100";
                    anim.SetTrigger("death");
                }
            }
        }

        //Ű�� ������ -> �� ���� �ݶ��̴� Ȱ��ȭ
        void Shield()
        {
            if (isDefense && !playerMove.isSitting) //��� ���̶��
            {
                attackCount = 0;

                playerWeaponTrigger_Component.enabled = false;

                if (playerMove.isGround)//����
                {
                    //�����
                    anim.SetTrigger("shield");
                    anim.SetBool("isDenfend", true);
                }
                else//����
                {
                    //�����
                    anim.SetTrigger("jumpShield");
                    anim.SetBool("isDenfend", true);
                }
            }
        }

        void Update()
        {
            print("����ī��Ʈ : " +attackCount);
            //Event.current.isMouse.
            //���� ��ư�� ��������  + ������ ���� ���콺�� ������
            if (Input.GetButtonDown("Fire1") && !EventSystem.current.IsPointerOverGameObject())
            {
                //���� ���
                Attack();
            }

            /*
            //���� ��ư�� �������� //����� ��ư�̶� ��ħ
            if (Input.GetButton("Fire3"))
            {
                //��� Ȱ��ȭ
                isDefense = true;
            }
            else
            {
                //��� ��Ȱ��ȭ
                isDefense = false;
            }
            */

            //���
            Shield();

        }//update
    }//class
}//namespace