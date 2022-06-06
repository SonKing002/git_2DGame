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
        public Button else_Act_btn;
        public Button basicAtt_btn;
        public Button shield_btn;
        public Button skillAtt1_btn;
        public Button skillAtt2_btn;
        
        public Animator anim_Effect;
        public Player_Attack_Defend player_Attack_Defend; //���� ������ 0
        public PlayerWeaponTrigger playerWeaponTrigger_Component; // disable �ϱ� ���� (�ൿ�� Ʈ���Ŷ� ���߿� �ٸ� �ൿ�� END�̺�Ʈ�Լ� ȣ�� �ȵȴ�.)

        public PlatformEffector2D platformEffector2d_Ground;// �����̼� �������� 180�� �ٲٱ� ����

        //������ 
        Vector2 dir; //����
        public float h; //�¿�
        float deshTimer, v, underJumpTimer, tempJumpTimer;  //�ð�üũ , �¿�, ����
        public float deshTimeLimit ,dashSpeed, moveMaxSpeed, jumpPower ; //�뽬 �ӵ� ,�ִ� �̵� �ӵ�, ������
        int jumpCount;

        //���� �Ǵ�
        [HideInInspector]
        public bool isGround;
        public bool isDash;
        public bool isChairFind; //���ڿ� ����
        public bool isSitting;
        public bool isGround_ToAble_UnderJump;//�ϴ� ���� ������ ��������
        public bool isStart_UnderJump;//�ϴ����� ���� = true //�� = false 
        public bool isLadderFind; //��ٸ��� ����
        public bool isStartLadder; //��ٸ� ���
        public bool is_Ground_Collider_Enable; //update���� �ѹ��� ó�� (�ϴ�����, ��ٸ� ����)

        void Start()
        {
            //�Ҵ�
            rig = GetComponent<Rigidbody2D>();
            anim = GetComponentInChildren<Animator>();
            jumpCount = 0;

            //�ʱ�ȭ
            isSitting = false;
            is_Ground_Collider_Enable = true;
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

        //���ǿ� ���� �������� �����ϴ� �Լ�
        void MoveLimit()
        {
            //��� �߿��� ������ �� ����.
            if (!player_Attack_Defend.isDefense)
            {
                h = Input.GetAxisRaw("Horizontal");
                v = Input.GetAxisRaw("Vertical");
            }
            else
            {
                h = 0;
            }

            //��ٸ� ���� ���� �¿� ������ �� ����. 
            if (!isStartLadder)
            {
                h = Input.GetAxisRaw("Horizontal");
            }
            else
            {
                h = 0;
            }

            //�ɴ� �������� ������ �� ����.
            if (!isSitting)
            {
                Move();
            }
        }

        //���� ��ư�� �������� ȣ�� �Լ�
        public void Jump_btn()
        {
            //�Ʒ� ����Ű�� ������
            if (v < 0)
            {
                //�ϴ� ���� �Լ�
                UnderJump();
            }
            //�̿� ��Ȳ����
            else
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
        }

        //������Ʈ ����üũ �Լ�
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

        //"�ϴ� ����" ��ưȣ�� �Լ�
        public void UnderJump() //����Ű�� ���� ��
        {
            //���̽� ������ �ƴ� ����( true�� ��ȯ�Ѵٸ� )
            if (isGround_ToAble_UnderJump)
            {
                //�ʱ�ȭ
                underJumpTimer = 0f;

                //���� collider mask����
                is_Ground_Collider_Enable = false;

                //�ϴ� ���� ����
                isStart_UnderJump = true;

                //�ִϸ��̼�
                anim.SetTrigger("jump"); //������� 1��
                print(LayerMask.NameToLayer("Player") + " �����ϴ°�"); //7��
            }
        }//�ϴ� ���� ȣ���Լ�

        //�ϴ� ���� : �ݶ��̴� ���� ������ �ð� üũ 
        void CheckTimerUnderJump()
        {
            //�ϴ� ���� �����Ҷ�
            if (isStart_UnderJump)
            {
                //�ð� ����
                underJumpTimer += Time.deltaTime;
                tempJumpTimer = underJumpTimer;
                print(underJumpTimer);
                //���� (�ϵ��ڵ�)
                if (tempJumpTimer >= 0.4f)
                {
                    //mask �߰�
                    is_Ground_Collider_Enable = true;

                    //�ϴ����� ��
                    isStart_UnderJump = false;
                }
            }
        }//void CheckTimerUnderJump()


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
                deshTimer += Time.deltaTime;
                float tempCheck = deshTimer;

                //�ִϸ��̼ǵ���
                anim.SetBool("isDash", true);
                

                //�ð��� �����Ǹ�
                if (tempCheck > deshTimeLimit)
                {
                    isDash = false;
                    //�ִϸ��̼� ��
                    anim.SetBool("isDash", false);
                    deshTimer = 0f;
                }
            }
        }

        //������ ȣ���Լ� : ��ٸ� Ÿ�� ����
        public void OnClick_UseLadderPointerDown_Btn()
        {
            //��ٸ��� ã�Ҵٸ�
            if (isLadderFind)
            {
                //���� Ű �ƹ��ų� ������ ��
                if (v != 0)
                {
                    //��ٸ� Ÿ�� ����
                    isStartLadder = true;

                    //Mask�� ���ش�.
                    is_Ground_Collider_Enable = false;

                    //���� �� ����
                    rig.AddForce(Vector2.up * v, ForceMode2D.Impulse);

                    //�߷� ����, ���� Ű���
                    rig.gravityScale = 0;
                    rig.drag = 13f;
                    rig.angularDrag = 13f;
                }
            }
        }
        //������ ȣ���Լ� : ��ٸ� Ÿ�� ��
        public void OnClick_UseLadderPointerUp_Btn()
        {
            //��ٸ� Ÿ�� ����
            isStartLadder = false;

            //�߷� ����, ���� Ű���
            rig.gravityScale = 3;
            rig.drag = 3f;
            rig.angularDrag = 0.05f;

            //���� �� ����
            rig.AddForce(Vector2.right * h, ForceMode2D.Impulse);

            //mask �߰�
            is_Ground_Collider_Enable = true;
        }

        //mask ��Ʈ�� �Լ�
        void Gruond_ColliderMask_Ctrl()
        {
            //bool�� ���¿� ���� ���� ó��
            if (is_Ground_Collider_Enable)
            {
                //mask �߰�
                platformEffector2d_Ground.colliderMask |= 1 << LayerMask.NameToLayer("Player");
            }
            else
            {
                //mask ����
                platformEffector2d_Ground.colliderMask
                    = platformEffector2d_Ground.colliderMask & ~(1 << LayerMask.NameToLayer("Player"));
            }
        }//Ground_ColliderMask_Ctrl();

        //(�ٸ� ��ũ��Ʈ �ϰ�ó����) ��ư ���� ��Ʈ�� �Լ�
        public void interactableCtrl(bool temp)
        {
            //
            dash_btn.interactable       = temp;
            jump_btn.interactable       = temp;
            shield_btn.interactable     = temp;
            else_Act_btn.interactable    = temp;
            skillAtt1_btn.interactable  = temp;
            skillAtt2_btn.interactable  = temp;
            basicAtt_btn.interactable   = temp;
        }

        //Else��ư ȣ���Լ�
        /*
        public void Else_Action_Ctrl_Btn()
        {
            //trigger�� ���ڸ� ã�Ҵٸ�
            if (isChairFind)
            {
                OnClick_Sitting_Btn();
            }

            //trigger�� ��ٸ��� ã�Ҵٸ�
            if (isLadderFind)
            {
                OnClick_UseLadder_btn();
            }
        }
        */

        //"�ɱ�" ��ư ȣ���Լ�
        public void OnClick_Sitting_Btn()
        {
            //trigger�� ������
            if (isChairFind)
            {
                //�� �ִٸ�
                if (!isSitting)
                {
                    //���� ��Ȱ��ȭ
                    interactableCtrl(false);

                    //�ִϸ��̼� ���� �ɱ�
                    anim.SetTrigger("sitStart");
                }
                else
                {
                    //���� ��Ȱ��ȭ
                    interactableCtrl(false);

                    //���� ��Ȱ��
                    isSitting = false;
                }
            }
        }

        //���� �� �ִϸ��̼� ��� �Լ� 
        void SittingAnimCtrl()
        {
            //�ִϸ��̼� ��Ʈ���� ����
            if (isSitting)//trigger �̺�Ʈ �Լ����� isSitting true /false
            {
                //�ɾ��ִ� ����
                anim.SetBool("isSitting", true);//�ɾ��ִ� ���� 
            }
            else
            {
                anim.SetBool("isSitting", false);//�� �ִ� ����
            }
        }

      

        void Update()
        {
            //������ ���� �Լ�
            MoveLimit();
            
            /* ����� ���� ���̽�ƽ�� ��Ʈ��
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

            //�ϴ� ���� ���� �� üũ
            CheckTimerUnderJump();

            //���� ������ collider�����Լ�: �浹����ũPlayer (�߰�/����)
            Gruond_ColliderMask_Ctrl();

        }//update
    }//class
}//namespace