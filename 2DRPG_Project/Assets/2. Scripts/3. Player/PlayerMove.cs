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
        public Button else_Act_btn;
        public Button basicAtt_btn;
        public Button shield_btn;
        public Button skillAtt1_btn;
        public Button skillAtt2_btn;
        
        public Animator anim_Effect;
        public Player_Attack_Defend player_Attack_Defend; //방어시 움직임 0
        public PlayerWeaponTrigger playerWeaponTrigger_Component; // disable 하기 위함 (행동이 트리거라서 도중에 다른 행동시 END이벤트함수 호출 안된다.)

        public PlatformEffector2D platformEffector2d_Ground;// 로테이션 오프셋을 180도 바꾸기 위함

        //움직임 
        Vector2 dir; //벡터
        public float h; //좌우
        float deshTimer, v, underJumpTimer, tempJumpTimer;  //시간체크 , 좌우, 상하
        public float deshTimeLimit ,dashSpeed, moveMaxSpeed, jumpPower ; //대쉬 속도 ,최대 이동 속도, 점프력
        int jumpCount;

        //불형 판단
        [HideInInspector]
        public bool isGround;
        public bool isDash;
        public bool isChairFind; //의자에 접근
        public bool isSitting;
        public bool isGround_ToAble_UnderJump;//하단 점프 가능한 발판인지
        public bool isStart_UnderJump;//하단점프 시작 = true //끝 = false 
        public bool isLadderFind; //사다리에 접근
        public bool isStartLadder; //사다리 사용
        public bool is_Ground_Collider_Enable; //update에서 한번에 처리 (하단점프, 사다리 조건)

        void Start()
        {
            //할당
            rig = GetComponent<Rigidbody2D>();
            anim = GetComponentInChildren<Animator>();
            jumpCount = 0;

            //초기화
            isSitting = false;
            is_Ground_Collider_Enable = true;
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

        //조건에 따라 움직임을 제한하는 함수
        void MoveLimit()
        {
            //방어 중에는 움직일 수 없다.
            if (!player_Attack_Defend.isDefense)
            {
                h = Input.GetAxisRaw("Horizontal");
                v = Input.GetAxisRaw("Vertical");
            }
            else
            {
                h = 0;
            }

            //사다리 오를 때는 좌우 움직일 수 없다. 
            if (!isStartLadder)
            {
                h = Input.GetAxisRaw("Horizontal");
            }
            else
            {
                h = 0;
            }

            //앉는 순간부터 움직일 수 없다.
            if (!isSitting)
            {
                Move();
            }
        }

        //점프 버튼을 눌렀을때 호출 함수
        public void Jump_btn()
        {
            //아래 방향키를 누르면
            if (v < 0)
            {
                //하단 점프 함수
                UnderJump();
            }
            //이외 상황에서
            else
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
        }

        //업데이트 점프체크 함수
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

        //"하단 점프" 버튼호출 함수
        public void UnderJump() //점프키를 누를 때
        {
            //베이스 발판이 아닌 순간( true로 반환한다면 )
            if (isGround_ToAble_UnderJump)
            {
                //초기화
                underJumpTimer = 0f;

                //발판 collider mask꺼짐
                is_Ground_Collider_Enable = false;

                //하단 점프 시작
                isStart_UnderJump = true;

                //애니메이션
                anim.SetTrigger("jump"); //점프모션 1번
                print(LayerMask.NameToLayer("Player") + " 존재하는가"); //7번
            }
        }//하단 점프 호출함수

        //하단 점프 : 콜라이더 꺼진 이후의 시간 체크 
        void CheckTimerUnderJump()
        {
            //하단 점프 시작할때
            if (isStart_UnderJump)
            {
                //시간 누적
                underJumpTimer += Time.deltaTime;
                tempJumpTimer = underJumpTimer;
                print(underJumpTimer);
                //예시 (하드코딩)
                if (tempJumpTimer >= 0.4f)
                {
                    //mask 추가
                    is_Ground_Collider_Enable = true;

                    //하단점프 끝
                    isStart_UnderJump = false;
                }
            }
        }//void CheckTimerUnderJump()


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
                deshTimer += Time.deltaTime;
                float tempCheck = deshTimer;

                //애니메이션동작
                anim.SetBool("isDash", true);
                

                //시간이 오버되면
                if (tempCheck > deshTimeLimit)
                {
                    isDash = false;
                    //애니메이션 끝
                    anim.SetBool("isDash", false);
                    deshTimer = 0f;
                }
            }
        }

        //포인터 호출함수 : 사다리 타기 시작
        public void OnClick_UseLadderPointerDown_Btn()
        {
            //사다리를 찾았다면
            if (isLadderFind)
            {
                //상하 키 아무거나 눌렀을 때
                if (v != 0)
                {
                    //사다리 타기 시작
                    isStartLadder = true;

                    //Mask를 꺼준다.
                    is_Ground_Collider_Enable = false;

                    //물리 힘 상하
                    rig.AddForce(Vector2.up * v, ForceMode2D.Impulse);

                    //중력 끄기, 마찰 키우기
                    rig.gravityScale = 0;
                    rig.drag = 13f;
                    rig.angularDrag = 13f;
                }
            }
        }
        //포인터 호출함수 : 사다리 타기 끝
        public void OnClick_UseLadderPointerUp_Btn()
        {
            //사다리 타기 시작
            isStartLadder = false;

            //중력 끄기, 마찰 키우기
            rig.gravityScale = 3;
            rig.drag = 3f;
            rig.angularDrag = 0.05f;

            //물리 힘 상하
            rig.AddForce(Vector2.right * h, ForceMode2D.Impulse);

            //mask 추가
            is_Ground_Collider_Enable = true;
        }

        //mask 컨트롤 함수
        void Gruond_ColliderMask_Ctrl()
        {
            //bool형 상태에 따라 동일 처리
            if (is_Ground_Collider_Enable)
            {
                //mask 추가
                platformEffector2d_Ground.colliderMask |= 1 << LayerMask.NameToLayer("Player");
            }
            else
            {
                //mask 제거
                platformEffector2d_Ground.colliderMask
                    = platformEffector2d_Ground.colliderMask & ~(1 << LayerMask.NameToLayer("Player"));
            }
        }//Ground_ColliderMask_Ctrl();

        //(다른 스크립트 일괄처리용) 버튼 조작 컨트롤 함수
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

        //Else버튼 호출함수
        /*
        public void Else_Action_Ctrl_Btn()
        {
            //trigger가 의자를 찾았다면
            if (isChairFind)
            {
                OnClick_Sitting_Btn();
            }

            //trigger가 사다리를 찾았다면
            if (isLadderFind)
            {
                OnClick_UseLadder_btn();
            }
        }
        */

        //"앉기" 버튼 호출함수
        public void OnClick_Sitting_Btn()
        {
            //trigger에 닿으면
            if (isChairFind)
            {
                //서 있다면
                if (!isSitting)
                {
                    //전부 비활성화
                    interactableCtrl(false);

                    //애니메이션 동작 앉기
                    anim.SetTrigger("sitStart");
                }
                else
                {
                    //전부 비활성화
                    interactableCtrl(false);

                    //조건 비활성
                    isSitting = false;
                }
            }
        }

        //앉을 때 애니메이션 재생 함수 
        void SittingAnimCtrl()
        {
            //애니메이션 컨트롤을 위함
            if (isSitting)//trigger 이벤트 함수에서 isSitting true /false
            {
                //앉아있는 상태
                anim.SetBool("isSitting", true);//앉아있는 상태 
            }
            else
            {
                anim.SetBool("isSitting", false);//서 있는 상태
            }
        }

      

        void Update()
        {
            //움직임 제한 함수
            MoveLimit();
            
            /* 모바일 가상 조이스틱용 컨트롤
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

            //하단 점프 시작 후 체크
            CheckTimerUnderJump();

            //공중 발판의 collider제어함수: 충돌마스크Player (추가/제거)
            Gruond_ColliderMask_Ctrl();

        }//update
    }//class
}//namespace