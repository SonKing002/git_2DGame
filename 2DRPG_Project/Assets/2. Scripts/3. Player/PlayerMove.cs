using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;
using UnityEngine.UI;

namespace Main
{
    public class PlayerMove : MonoBehaviour
    {
        
        //컴포넌트
        Rigidbody2D rig; //물리작용
        Animator anim; //애니메이션 제어 

        //다른 컴포넌트
        public StickLeverContrl moveStick_Script;
        
        public Button jump_btn;
        public Button dash_btn;
        public Button sitting_btn;
        public Button basicAtt_btn;
        public Button shield_btn;
        public Button skillAtt1_btn;
        public Button skillAtt2_btn;
        
        public Animator anim_Effect;
        public Player_Attack_Defend player_Attack_Defend; //방어시 움직임 0
        public PlayerWeaponTrigger playerWeaponTrigger_Component; // disable 하기 위함 (행동이 트리거라서 도중에 다른 행동시 END이벤트함수 호출 안된다.)

        //움직임 
        Vector2 dir; //벡터
        public float h; //좌우
        float timer, v;  //시간체크 , 좌우, 상하
        public float timeLimit ,dashSpeed, moveMaxSpeed, jumpPower ; //대쉬 속도 ,최대 이동 속도, 점프력
        int jumpCount;

        //불형 판단
        [HideInInspector]
        public bool isGround;
        public bool isDash;
        public bool isSitting;

        //인트형 판단 
        public int sittingNum; //0 서 있기, 1 의자 앉아있기

        void Start()
        {
            //할당
            rig = GetComponent<Rigidbody2D>();
            anim = GetComponentInChildren<Animator>();
            jumpCount = 0;

            isSitting = false;
        }


        void Move()
        {
            //물리 힘 좌우
            rig.AddForce(Vector2.right * h, ForceMode2D.Impulse);

            //idle 상태
            if (rig.velocity.x == 0 || h == 0)
            {
                anim.SetBool("isWalk", false);
            }

            //walk 우측
            else if (rig.velocity.x > 0 )
            {
                
                //적에 의해서 방향이 틀어진다 => 내가 컨트롤 하고 있을때 조건추가
                if (h != 0)
                {
                    //방향 반전
                    transform.localScale = new Vector3(1f, 1f, 1f);
                }

                if (!isDash) //걸을 때
                {
                    anim.SetBool("isWalk", true);

                    //최대 속도
                    if (rig.velocity.x > moveMaxSpeed)
                    {
                        rig.velocity = new Vector2(moveMaxSpeed, rig.velocity.y);
                    }
                }
                else //대쉬일 때
                {
                    //최대 속도
                    if (rig.velocity.x > dashSpeed)
                    {
                        rig.velocity = new Vector2(dashSpeed, rig.velocity.y);
                    }
                }
            }

            //walk 좌측
            else if (rig.velocity.x < 0)
            {

                //적의 움직임에 의해서 틀어진다. => 내가 컨트롤 하고 있을때 조건추가
                if (h != 0)
                { 
                    //방향
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }

                if (!isDash)//걸을 때
                {
                    anim.SetBool("isWalk", true);

                    //최대 속도
                    if (rig.velocity.x < -moveMaxSpeed)
                    {
                        rig.velocity = new Vector2(-moveMaxSpeed, rig.velocity.y);
                    }
                }
                else //대쉬일 때
                {
                    //최대 속도
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
                //trigger 초기화
                player_Attack_Defend.attackCount = 0;

                //무기 콜라이더 꺼주기
                playerWeaponTrigger_Component.enabled = false;

                jumpCount++;

                anim.SetTrigger("jump"); //점프모션 1번
                anim_Effect.SetTrigger("Jump"); //점프이펙트
                rig.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }
        }

        void Jump()
        {
            //공중이라면 == 떨어지거나 떠있거나
            if (!isGround)
            {//떠있는 동안의 루프 애니메이션
                anim.SetBool("isGround", false);//공중에 있음
                anim.SetBool("isJump", true);//점프를 눌러 위로 올라가는 중( 올라가는 중의 공중true , 내려가는 중의 공중 false : 내려가는 중은 아직)
            }

            //땅이라면
            else
            {
                anim.SetBool("isGround", true);//땅에 있음
                anim.SetBool("isJump", false);// 내려가는 중 
                anim.SetTrigger("land");//땅에 닿을때 트리거 한번 발동함

                jumpCount = 0;
            }
        }

        //대쉬버튼 누르면 함수 호출
        public void OnClick_DashPointerDown_Btn()
        {
            //대쉬는 움직이는 동안에 동작
            if (h != 0 && !isSitting)
            {
                //trigger 초기화
                player_Attack_Defend.attackCount = 0;

                //무기 콜라이더 꺼주기
                playerWeaponTrigger_Component.enabled = false;

                //조건 활성화
                isDash = true;
                //애니메이션 시작
                anim.SetTrigger("Dash");//대쉬 모션
                anim_Effect.SetTrigger("DashDust"); //대쉬 이펙트
            }
        }

        //버튼 손을 떼면
        public void OnClick_DashPointerUp_Btn()
        {
            //조건 비활성화
            isDash = false;
            //애니메이션 끝
            anim.SetBool("isDash", false);
        }

        //대시시간 초과시 false
        public void DashTime()
        {
            //조건 활성화일때
            if (isDash)
            {
                //시간 재기
                timer += Time.deltaTime;
                float tempCheck = timer;

                //애니메이션동작
                anim.SetBool("isDash", true);
                

                //시간이 오버되면
                if (tempCheck > timeLimit)
                {
                    isDash = false;
                    //애니메이션 끝
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

        //버튼 호출
        public void OnClick_Sitting_Btn()
        {
            if (h == 0 && sittingNum == 0)
            {
                //전부 비활성화
                interactableCtrl(false);

                //애니메이션 동작 앉기
                anim.SetTrigger("sitStart");
            }
            if (sittingNum == 1)
            {
                //전부 비활성화
                interactableCtrl(false);

                //조건 비활성
                isSitting = false;
            }

        }

        //앉을 때 애니메이션 재생 함수 
        void SittingAnimCtrl()
        {
            //애니메이션 컨트롤을 위함
            if (isSitting)//trigger 이벤트 함수에서 isSitting true /false
            {
                //앉아있는 상태
                sittingNum = 1;//버튼 컨트롤을 위함

                anim.SetBool("isSitting", true);//앉아있는 상태 
            }
            else
            {
                sittingNum = 0;
                anim.SetBool("isSitting", false);//서 있는 상태
            }
        }
        void Update()
        {
            //방어 중에는 움직일 수 없다.
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
                //키 받아오기
                h = Input.GetAxisRaw("Horizontal");
            }
            */
            if (!isSitting)
            {
                Move();
            }
            

            //점프를 누르면 + 땅이 있을때
            if (Input.GetButtonDown("Jump") & jumpCount < 1)
            {
                jumpCount++;


                anim.SetTrigger("jump"); //점프모션 1번
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