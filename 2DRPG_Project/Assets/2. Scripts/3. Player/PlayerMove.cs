using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;
using UnityEngine.UI;

namespace Main
{
    public class PlayerMove : MonoBehaviour
    {
        
        //������Ʈ
        Rigidbody2D rig; //�����ۿ�
        Animator anim; //�ִϸ��̼� ���� 

        //�ٸ� ������Ʈ
        public StickLeverContrl moveStick_Script;
        
        public Button jump_btn;
        public Button dash_btn;
        public Button sitting_btn;
        public Button basicAtt_btn;
        public Button shield_btn;
        public Button skillAtt1_btn;
        public Button skillAtt2_btn;
        
        public Animator anim_Effect;
        public Player_Attack_Defend player_Attack_Defend; //���� ������ 0
        public PlayerWeaponTrigger playerWeaponTrigger_Component; // disable �ϱ� ���� (�ൿ�� Ʈ���Ŷ� ���߿� �ٸ� �ൿ�� END�̺�Ʈ�Լ� ȣ�� �ȵȴ�.)

        //������ 
        Vector2 dir; //����
        public float h; //�¿�
        float timer, v;  //�ð�üũ , �¿�, ����
        public float timeLimit ,dashSpeed, moveMaxSpeed, jumpPower ; //�뽬 �ӵ� ,�ִ� �̵� �ӵ�, ������
        int jumpCount;

        //���� �Ǵ�
        [HideInInspector]
        public bool isGround;
        public bool isDash;
        public bool isSitting;

        //��Ʈ�� �Ǵ� 
        public int sittingNum; //0 �� �ֱ�, 1 ���� �ɾ��ֱ�

        void Start()
        {
            //�Ҵ�
            rig = GetComponent<Rigidbody2D>();
            anim = GetComponentInChildren<Animator>();
            jumpCount = 0;

            isSitting = false;
        }


        void Move()
        {
            //���� �� �¿�
            rig.AddForce(Vector2.right * h, ForceMode2D.Impulse);

            //idle ����
            if (rig.velocity.x == 0 || h == 0)
            {
                anim.SetBool("isWalk", false);
            }

            //walk ����
            else if (rig.velocity.x > 0 )
            {
                
                //���� ���ؼ� ������ Ʋ������ => ���� ��Ʈ�� �ϰ� ������ �����߰�
                if (h != 0)
                {
                    //���� ����
                    transform.localScale = new Vector3(1f, 1f, 1f);
                }

                if (!isDash) //���� ��
                {
                    anim.SetBool("isWalk", true);

                    //�ִ� �ӵ�
                    if (rig.velocity.x > moveMaxSpeed)
                    {
                        rig.velocity = new Vector2(moveMaxSpeed, rig.velocity.y);
                    }
                }
                else //�뽬�� ��
                {
                    //�ִ� �ӵ�
                    if (rig.velocity.x > dashSpeed)
                    {
                        rig.velocity = new Vector2(dashSpeed, rig.velocity.y);
                    }
                }
            }

            //walk ����
            else if (rig.velocity.x < 0)
            {

                //���� �����ӿ� ���ؼ� Ʋ������. => ���� ��Ʈ�� �ϰ� ������ �����߰�
                if (h != 0)
                { 
                    //����
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }

                if (!isDash)//���� ��
                {
                    anim.SetBool("isWalk", true);

                    //�ִ� �ӵ�
                    if (rig.velocity.x < -moveMaxSpeed)
                    {
                        rig.velocity = new Vector2(-moveMaxSpeed, rig.velocity.y);
                    }
                }
                else //�뽬�� ��
                {
                    //�ִ� �ӵ�
                    if (rig.velocity.x < -dashSpeed)
                    {
                        rig.velocity = new Vector2(-dashSpeed, rig.velocity.y);
                    }
                }
            }
        }

        public void Jump_btn()
        {
            if (jumpCount < 1 && !isSitting)
            {
                //trigger �ʱ�ȭ
                player_Attack_Defend.attackCount = 0;

                //���� �ݶ��̴� ���ֱ�
                playerWeaponTrigger_Component.enabled = false;

                jumpCount++;

                anim.SetTrigger("jump"); //������� 1��
                anim_Effect.SetTrigger("Jump"); //��������Ʈ
                rig.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }
        }

        void Jump()
        {
            //�����̶�� == �������ų� ���ְų�
            if (!isGround)
            {//���ִ� ������ ���� �ִϸ��̼�
                anim.SetBool("isGround", false);//���߿� ����
                anim.SetBool("isJump", true);//������ ���� ���� �ö󰡴� ��( �ö󰡴� ���� ����true , �������� ���� ���� false : �������� ���� ����)
            }

            //���̶��
            else
            {
                anim.SetBool("isGround", true);//���� ����
                anim.SetBool("isJump", false);// �������� �� 
                anim.SetTrigger("land");//���� ������ Ʈ���� �ѹ� �ߵ���

                jumpCount = 0;
            }
        }

        //�뽬��ư ������ �Լ� ȣ��
        public void OnClick_DashPointerDown_Btn()
        {
            //�뽬�� �����̴� ���ȿ� ����
            if (h != 0 && !isSitting)
            {
                //trigger �ʱ�ȭ
                player_Attack_Defend.attackCount = 0;

                //���� �ݶ��̴� ���ֱ�
                playerWeaponTrigger_Component.enabled = false;

                //���� Ȱ��ȭ
                isDash = true;
                //�ִϸ��̼� ����
                anim.SetTrigger("Dash");//�뽬 ���
                anim_Effect.SetTrigger("DashDust"); //�뽬 ����Ʈ
            }
        }

        //��ư ���� ����
        public void OnClick_DashPointerUp_Btn()
        {
            //���� ��Ȱ��ȭ
            isDash = false;
            //�ִϸ��̼� ��
            anim.SetBool("isDash", false);
        }

        //��ýð� �ʰ��� false
        public void DashTime()
        {
            //���� Ȱ��ȭ�϶�
            if (isDash)
            {
                //�ð� ���
                timer += Time.deltaTime;
                float tempCheck = timer;

                //�ִϸ��̼ǵ���
                anim.SetBool("isDash", true);
                

                //�ð��� �����Ǹ�
                if (tempCheck > timeLimit)
                {
                    isDash = false;
                    //�ִϸ��̼� ��
                    anim.SetBool("isDash", false);
                    timer = 0f;
                }
            }
        }

        public void interactableCtrl(bool temp)
        {
            //
            dash_btn.interactable       = temp;
            jump_btn.interactable       = temp;
            shield_btn.interactable     = temp;
            sitting_btn.interactable    = temp;
            skillAtt1_btn.interactable  = temp;
            skillAtt2_btn.interactable  = temp;
            basicAtt_btn.interactable   = temp;
        }    

        //��ư ȣ��
        public void OnClick_Sitting_Btn()
        {
            if (h == 0 && sittingNum == 0)
            {
                //���� ��Ȱ��ȭ
                interactableCtrl(false);

                //�ִϸ��̼� ���� �ɱ�
                anim.SetTrigger("sitStart");
            }
            if (sittingNum == 1)
            {
                //���� ��Ȱ��ȭ
                interactableCtrl(false);

                //���� ��Ȱ��
                isSitting = false;
            }

        }

        //���� �� �ִϸ��̼� ��� �Լ� 
        void SittingAnimCtrl()
        {
            //�ִϸ��̼� ��Ʈ���� ����
            if (isSitting)//trigger �̺�Ʈ �Լ����� isSitting true /false
            {
                //�ɾ��ִ� ����
                sittingNum = 1;//��ư ��Ʈ���� ����

                anim.SetBool("isSitting", true);//�ɾ��ִ� ���� 
            }
            else
            {
                sittingNum = 0;
                anim.SetBool("isSitting", false);//�� �ִ� ����
            }
        }
        void Update()
        {
            //��� �߿��� ������ �� ����.
            if (!player_Attack_Defend.isDefense)
            {
                h = Input.GetAxisRaw("Horizontal");
            }
            else
            {
                h = 0;
            }

            /*
            if (moveStick_Script.isMoblie)
            {
                h = moveStick_Script.dir_x;
            }
            else
            {
                //Ű �޾ƿ���
                h = Input.GetAxisRaw("Horizontal");
            }
            */
            if (!isSitting)
            {
                Move();
            }
            

            //������ ������ + ���� ������
            if (Input.GetButtonDown("Jump") & jumpCount < 1)
            {
                jumpCount++;


                anim.SetTrigger("jump"); //������� 1��
                anim_Effect.SetTrigger("Jump");
                rig.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }
            Jump();

            print(jumpCount);

            DashTime();

            SittingAnimCtrl();


        }//update
    }//class
}//namespace