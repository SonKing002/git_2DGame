using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Main;
using TMPro;

namespace Main
{
    //유항상태 머신 변형
    enum StateMachine
    { 
        Patron, //순찰 ( idle, walk 애니메이션이 랜덤, 레이케스트, 낭떨어지 판단 )
        Fallow, //추격 ( 플래이어방향으로만 이동, 낭떨어지 앞이라면 순찰 )
        Attack, //공격 ( 공격주기로 공격만 )
        Damaged,//피격 ( sendMassage받아서 체력 체크후 피격판정, 순찰상태로 )
        Death,  //죽음 ( 체력 체크후 애니메이션 재생 끝나면 이벤트 호출, 전부 삭제 )
    };

    //적 컨트롤
    public class MonsterCtrl : MonoBehaviour
    {
        //컴포넌트
        public Rigidbody2D rig;
        public SpriteRenderer sr;//자신의 이미지 (이미지의 방향전환)
        public Slider hpBar;
        public Animator anim;
        public CapsuleCollider2D collider2d;
        public MonsterHitBoxColl monsterColl; //히트박스 체크용
        public Transform hitbox_Direction;// 방향전환시 히트박스 위치 수정용

        public Animator emotion;

        //체크용
        public bool isChanged; //땅인지 체크
        public int turnDir; //-1 0 1 왼쪽 멈춤 오른쪽
        public bool isDead; //죽으면 모든 움직임 통제

        //임의 보정값
        public float speed, maxSpeed; //속도 , 최대속도
        public Vector3 ground_Position; //위치 조정

        //임시
        public float hp;
        public Text hp_txt;
        public TextMeshProUGUI name_txt;//csv에서 불러와 받을것 (체력,방어력, 공격력, 이름)

        //체력 비교
        public Player_Attack_Defend player_Attack_Defend;

        //거리재기 
        float distance; //플레이어와의 거리재기
        public float destination; //플레이어에게 접근
        public Transform player;

        //공격
        float tempSpeed, maxTempSpeed;
        public float attackTimer;//공격주기
        public float temp_TimeCheck; //임시 시간검사용

        public float attack_Distance; //공격가능거리 0.8f
        public float fallow_Distance; //추격가능거리 1.3f
        public float patrol_Distance; //추격가능거리 6f

        public Vector3 hitbox_SizeCtrl; //히트박스의 범위 수정

        //피격시 
        public UsePopUp damagedPopUp;

        //초기화
        StateMachine state = StateMachine.Patron;

        //기즈모를 이용한 시야각 설정 //추격범위, 공격 범위제한
        public Vector3 gizmo_Position;

        [SerializeField] bool debugMode = false;

        [Header("View Config 내용")]
        [Range(0f, 360f)]
        [SerializeField] float horizontalView_Angle        = 0f;
        [SerializeField] float viewRadius                  = 1f;

        [Range(-180f, 180f)]//로테이션 없이 탐지 방향이 몬스터 방향과 일치하지 않아야 할때,
        [SerializeField] float z_viewRotate                = 0f;

        [SerializeField] LayerMask targetMask;
        [SerializeField] LayerMask obstacleMask;

        //target 검색에 유리한 자료형으로 수정
        public Dictionary<Collider2D, string> hitedTargetContainer2 = new Dictionary<Collider2D, string>();
        //public List<Collider2D> hitedTargetContainer = new List<Collider2D>();

        float horizontalView_HalfAngle = 0f;

        private void Awake()
        {
            horizontalView_HalfAngle = horizontalView_Angle * 0.5f;
        }

        void Start()
        {
            /* public으로 inspector에서 직접 연결하는 것이 덜 무겁다
            //할당
            //rig = GetComponent<Rigidbody2D>(); //움직임 제어
            //sr = GetComponent<SpriteRenderer>(); //색 알파값 서서히 변하게 연출, 좌우 반전 간편하게 사용
            //hpBar = GetComponentInChildren<Slider>(); //하위 오브젝트의 슬라이더
            //anim = GetComponent<Animator>(); //애니메이션 제어
            //collider2d = GetComponent<CapsuleCollider2D>(); //죽은 이후에 걸리적 거리지 않도록 꺼주기 위해 가져옴

            //find 할당
            //player = FindObjectOfType<PlayerMove>().transform; //플래이어 정보
            //monsterColl = FindObjectOfType<MonsterColl>();
            */

            ground_Position = new Vector3(1, -1, 0); //보정

            //초기화
            turnDir = 1;

            //임시
            hp = 100;

            hp_txt.text = hp + " / " + 100; //체력 표시
        }

        //반시계 방향 = 음수, 시계방향 = 양수
        //입력한 -180~180을 Up Vector 기준  Direction으로 변환해주는 AngleToDirZ 함수
        private Vector3 AngleToDirZ(float angleInDegree)
        {
            //로컬 Direction으로 변환하기 위함
            float radian = (angleInDegree - transform.eulerAngles.z) * Mathf.Deg2Rad;
            return new Vector3(Mathf.Sin(radian), Mathf.Cos(radian), 0f);
        }

        //시야각 기즈모 함수
        private void OnDrawGizmos()
        {
            if (debugMode)
            {
                horizontalView_HalfAngle = horizontalView_Angle * 0.5f;

                Vector3 originPos = transform.position + gizmo_Position;

                Gizmos.DrawWireSphere(originPos, viewRadius);

                //오른/왼 경계선, 보는 방향
                Vector3 horizontalRightDir = AngleToDirZ(-horizontalView_HalfAngle + z_viewRotate);
                Vector3 horizontalLeftDir = AngleToDirZ(horizontalView_HalfAngle + z_viewRotate);
                Vector3 lookDir = AngleToDirZ(z_viewRotate);

                //범위 경계선 그리기
                Debug.DrawRay(originPos, horizontalLeftDir * viewRadius, Color.cyan);
                Debug.DrawRay(originPos, lookDir * viewRadius, Color.green);
                Debug.DrawRay(originPos, horizontalRightDir * viewRadius, Color.cyan);

                //누가 내 시야에 들어와있고 어디에 시야가 막혀있는지 확인 할 수 있따.
                FindViewTargets();
            }
        }

        public Dictionary<Collider2D,string> FindViewTargets()
        {
            //hitedTargetContainer.Clear();
            hitedTargetContainer2.Clear();

            Vector2 originPos = transform.position  + gizmo_Position;
            //위치에서, 반지름에 있는 Mask의 Collider 타겟을 넣기 (범위내 선별된 타겟 확인)
            Collider2D[] hitedTargets = Physics2D.OverlapCircleAll(originPos, viewRadius, targetMask);

            foreach (Collider2D hitedTarget in hitedTargets)
            {
                Vector2 targetPos = hitedTarget.transform.position + gizmo_Position;     //닿은 콜라이더 물체 위치
                Vector2 dir       = (targetPos - originPos).normalized; //방향 벡터 (적방향)
                Vector2 lookDir   = AngleToDirZ(z_viewRotate);          //(시야각 방향)

                //float angle = Vector.Angle(lookDir, dir) 아래 두줄은 이 코드와 동일 동작 구현도 동일
                float dot   = Vector2.Dot(lookDir, dir);//두 방향 벡터의 내적을 구함
                float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;// Acos에 내적을 넣어 Degree로 변환 (시야각 -> 적방향의 각도 : 양수로 나옴)

                if (angle <= horizontalView_HalfAngle)// 범위 내 내적이 들어와 있다면 == 시야각 내에 존재
                {

                    //내 위치, 적 방향, 반지름 크기, 선별할 장애물 마스크
                    RaycastHit2D rayHitedTarget = Physics2D.Raycast(originPos, dir, viewRadius, obstacleMask);

                    print(rayHitedTarget);
                    
                    if (rayHitedTarget)//존재한다면
                    {
                        if (debugMode)
                        {
                            //장애물때문에 타겟을 보지 못한다 
                            Debug.DrawLine(originPos, rayHitedTarget.point, Color.yellow);
                        }
                    }
                    else //존재하지 않는다면
                    {
                        //hitedTargetContainer.Add(hitedTarget);// 확실히 볼 수 있는 존재로 Add를 해준다.
                        hitedTargetContainer2.Add(hitedTarget, hitedTarget.gameObject.tag);

                        if (debugMode)
                        {
                            Debug.DrawLine(originPos, targetPos, Color.red);
                        }
                    }
                }
            }//foreach

            if (hitedTargetContainer2.Count > 0)
            {
                //eturn hitedTargetContainer.ToArray();
                return hitedTargetContainer2;
            }
            else
            {
                return null;
            }
        }

        //피격 Player_Attack_Defend()에서 공격시 애니메이션으로 (북마크)
        //이벤트함수의 호출에 따라 SendMassage로 전달
        void Damaged()
        {
            //임시
            hp -= 10;
            
            //체력바 표시
            hpBar.value = hp * 0.01f;

            if (hp > 0)//체력이 남았다면
            {
                //피격 애니메이션
                anim.SetTrigger("damaged");

                //피격상태로 전환
                state = StateMachine.Damaged;

                //text에 표시
                hp_txt.text = hp + " / " + 100;

                //이 오브젝트가 피격
                damagedPopUp.WhichDamaged(gameObject);
            }
            else//체력이 0 이하라면
            {
                //죽었다
                isDead = true;

                //죽은 애니메이션
                anim.SetTrigger("death");

                //text에 표시
                hp_txt.text = 0 + " / " + 100;

                //체력이 없다면 죽음 상태로 전환
                state = StateMachine.Death;
            }
        }

        //피격이 끝날때 애니메이션 이벤트 함수
        void DamagedEnd()
        {
            //순찰 상태로
            state = StateMachine.Fallow;
        }

        //순찰 동작
        void PatronMove()
        {
            //움직임 (방향 * 속도 , y)
            rig.velocity = new Vector2(turnDir * speed, rig.velocity.y);

            //레이케스트 땅 체크
            Check();
        }

        //땅인지 체크
        void Check()
        {
            //에디터에 레이선 보이기
            Debug.DrawRay(transform.position + ground_Position, Vector3.down * 1);
            //쏘는 방향에 있는 물체
            RaycastHit2D hit = Physics2D.Raycast(transform.position + ground_Position, Vector3.down, 1f);

            //맞은 물체가 없다면 == 낭떨어지
            if (!hit)
            {
                //방향을 바꿔주고
                TurnDir();
  
                //추격상태일때 더이상 쫒지 않고
                if (state == StateMachine.Fallow)
                {
                    //다시 순찰하도록 한다
                    state = StateMachine.Patron;
                }
            }
            else //맞은 물체가 있다면
            {
                //테그검색
                switch (hit.collider.gameObject.tag)
                {
                    case "Ground"://땅이라면 유지
                        isChanged = false;
                        break;
                    case "Player"://플레이어도 유지 
                        //하이어라키 캐릭터 오브젝트 중 : TalkingRange에 걸림 (해당 Tag : Player 설정해야 반응)
                        isChanged = false;

                        break;
                    default:// 이외의 물체, 방향 전환
                        //어떤 걸림돌이 있다면 순찰상태에서는
                        if (state == StateMachine.Patron)
                        {
                            //방향을 바꿔준다.
                            TurnDir();
                        }
                        break;
                }
            }//if(hit)

            //캐릭터 방향전환
            switch (turnDir)
            {
                case -1:
                    ground_Position = new Vector3(-1, -1, 0); // 레이케스트 위치 보정
                    anim.SetBool("isWalk", true); //애니메이션
                    sr.flipX = true; //이미지 뒤집기

                    z_viewRotate = Z_turn_View(turnDir, z_viewRotate); //시야각 방향전환시 
                    //transform.localScale = new Vector2(2,2); 스케일을 사용하면 자식 오브젝트들이 전부 뒤집혀진다 -> UI도 전부 뒤집어줘야 한다.
                    break;
                case 0:
                    anim.SetBool("isWalk", false);

                    z_viewRotate = Z_turn_View(turnDir, z_viewRotate); //시야각 방향전환시 
                    break;
                case 1:
                    ground_Position = new Vector3(1, -1, 0); // 레이케이트 위치 보정
                    anim.SetBool("isWalk", true); //애니메이션 활성화
                    sr.flipX = false;//이미지 뒤집기

                     z_viewRotate = Z_turn_View(turnDir, z_viewRotate); //시야각 방향전환시 
                    //transform.localScale = new Vector2(-2,2);
                    break;
            }
        }//check

        //i == case 방향 , z == 로테이션 값 : 방향이 바뀔 때 계산해주는 함수
        float Z_turn_View(int i,float z)
        {
            //i  양수방향 일때
            if (i == 1)
            {
                //음수 방향을 양수로
                if (z < 0)
                {
                    z *= -1;
                    return z;
                }
                else
                {
                    return z;
                }
            }
            //i 음수방향 일때
            else if (i == -1)
            {
                //양수 방향을 음수로
                if (z > 0)
                {
                    z *= -1;
                    return z;
                }
                else
                {
                    return z;
                }
            }
            //이외 
            else
            {
                //그대로
                return z;
            }
        }

        //방향 수정용
        void TurnDir()
        {
            //방향이 안 바뀌었다면
            if (!isChanged)//false
            {
                //방향전환
                turnDir *= -1;
                z_viewRotate *= -1;

                // 방향 수정 완료 true
                isChanged = true;

                //겹치면 낭떨어지로 추락할 수 있어서
                CancelInvoke();

                //랜덤으로 돌려주기
                Invoke("RandomDir", Random.Range(2f, 4f));
            }
        }

        //랜덤
        void RandomDir()
        {
            //방향전환
            turnDir = Random.Range(-1, 2);

            //재귀호출
            Invoke("RandomDir", Random.Range(2f, 4f));
        }

        void Update()
        {
            //머리위에서 따라가도록
            hp_txt.transform.position = new Vector2(transform.position.x, hp_txt.transform.position.y);
            name_txt.transform.position = new Vector2(transform.position.x, name_txt.transform.position.y);
            hpBar.transform.position = new Vector2(transform.position.x, hpBar.transform.position.y);

            print(state);

            //살아있다면
            if (!isDead)
            {
                //거리재기
                distance = Vector2.Distance(transform.position, player.position);

                //player까지의 거리 (방향)
                destination = player.position.x - transform.position.x;
                //양수 음수에 따라 방향설정
                destination = (destination < 0) ? -1 : 1;

                //무한정 계산하면 누적되므로
                if (attackTimer <= 3f)
                {
                    //공격주기 = 시간 초
                    attackTimer += Time.deltaTime;
                }
                
                //진짜 사용할 검사용 변수에 대입
                temp_TimeCheck = attackTimer;

                //상태에 따른 조건문
                switch (state)
                {
                    case StateMachine.Patron: //순찰 상태
                        Patron();
                        break;
                    case StateMachine.Fallow: //추격 상태
                        Fallow();
                        break;
                    case StateMachine.Attack: //공격 상태
                        Attack();
                        break;
                    case StateMachine.Damaged: //피격 상태
                                             //캐릭터로부터 SendMessage를 통해 호출
                        break;
                    case StateMachine.Death: //죽음 상태
                                             //애니메이션이 끝날때 이벤트함수로 호출
                        break;
                }

                //거리에 따른 유항상태 제어
                //Act_Distance();

            }//if(!isDead) 살아있다면
        }//update

        //거리에 따른 유항상태 제어 함수
        /*
        void Act_Distance()
        {

            print("순찰거리");
            //순찰상태
            state = StateMachine.Patron;

            //공격상태
            state = StateMachine.Attack;

            //거리에 따른 조건
            if (distance < fallow_Distance) //공격거리
            {
                print("공격거리");

                //공격상태
                state = StateMachine.Attack;

            }
            else if (fallow_Distance <= distance && distance < patrol_Distance) //추격거리
            {

                if (player)
                    //추적상태
                    state = StateMachine.Fallow;
            }
            else if (fallow_Distance <= distance) //순찰거리
            {
                print("순찰거리");
                //순찰상태
                state = StateMachine.Patron;
            }
            
        }
        */

        //순찰 함수
        void Patron()
        {
            print("순찰상태");
            //순찰 움직임
            PatronMove();

            //체력이 있고 시야각에 들어온다면
            if (player_Attack_Defend.hp > 0  && hitedTargetContainer2.ContainsValue(player.tag))
            {
                //print(hitedTargetContainer2.ContainsValue(player.tag)); true 확인

                //순찰돌다가 추격하러고 할 때 (전체 거리 기준)
                if (fallow_Distance <= distance && distance < patrol_Distance) //추격거리
                {
                    //추격상태
                    state = StateMachine.Fallow;
                    emotion.SetTrigger("Dead");
                    emotion.SetBool("isDead", true);
                }
                //순찰돌다가 공격하려고 할 때 (전체 거리 기준)
                if (distance < fallow_Distance) //공격거리
                {
                    //공격상태
                    state = StateMachine.Attack;
                    emotion.SetTrigger("Dead");
                    emotion.SetBool("isDead", true);
                }
            }
        }

        //추격 함수
        void Fallow()
        {
            if (hitedTargetContainer2.ContainsValue(player.tag))
            {
                print("추격상태");
                //방향값에 따른 움직임 수정 (PatronMove에서 그대로 가져오면 애니메이션이 제멋대로)
                switch (destination)
                {
                    case 0:
                        //멈춤
                        anim.SetBool("isWalk", false);

                        z_viewRotate = Z_turn_View(0, z_viewRotate); //시야각 방향전환시 
                        break;
                    case 1:
                        //오른쪽 움직임
                        anim.SetBool("isWalk", true);
                        sr.flipX = false;

                        z_viewRotate = Z_turn_View(1, z_viewRotate); //시야각 방향전환시 

                        hitbox_Direction.localPosition = hitbox_SizeCtrl; //히트박스 수정
                                                                          //monsterColl.RightDirBox();
                        break;
                    case -1:
                        //왼쪽 움직임
                        anim.SetBool("isWalk", true);
                        sr.flipX = true;

                        z_viewRotate = Z_turn_View(-1, z_viewRotate); //시야각 방향전환시 

                        hitbox_Direction.localPosition = new Vector3(-hitbox_SizeCtrl.x, hitbox_SizeCtrl.y); //히트박스 수정
                                                                                                             //monsterColl.LeftDirBox();
                        break;
                }
                //추격하기
                rig.velocity = new Vector2(destination * maxSpeed, rig.velocity.y);

                if (distance < fallow_Distance) //가까워지면 공격거리
                {
                    //공격상태
                    state = StateMachine.Attack;
                    emotion.SetBool("isDead", true);
                }
            }//시야각에 들어오면

            //추격하다가 멀어지거나, 콜라이더가 없다면
            if (patrol_Distance <= distance) //순찰거리
            {
                //순찰상태
                state = StateMachine.Patron;
                emotion.SetBool("isDead", false);
            }
        }//추격함수

        void Attack()
        {
            //체력이 남아있을 때 
            if (player_Attack_Defend.hp > 0)
            {
                if (distance < attack_Distance)
                {
                    //멈춤
                    destination = 0;
                    anim.SetBool("isWalk", false);
                }

                print("공격상태");

                //2초가 지난다면
                if (temp_TimeCheck >= 2f)
                {
                    //공격 애니메이션 재생
                    anim.SetBool("isAttack", true);
                    anim.SetTrigger("attack");

                    print("재생");

                    //공격 주기 초기화
                    attackTimer = 0f;
                    temp_TimeCheck = 0;
                }
            }//if (player_Attack_Defend.hp >= 0 )

            else//죽음
            {
                //순찰상태
                state = StateMachine.Patron;

                //공격 애니메이션 비활성화
                anim.SetBool("isAttack", false);
                emotion.SetBool("isDead", false);
            }

            //공격하다가 거리가 멀어지면
            if (fallow_Distance <= distance && distance < patrol_Distance ) //추격거리
            {
                //추격상태
                state = StateMachine.Fallow;

                //공격 애니메이션 비활성화
                anim.SetBool("isAttack", false);
            }
            //추격거리보다 멀어지거나 시야각에서 벗어나면
            if (patrol_Distance <= distance ) //순찰거리
            {
                //순찰상태
                state = StateMachine.Patron;

                //공격 애니메이션 비활성화
                anim.SetBool("isAttack", false);
                emotion.SetBool("isDead", false);
            }
        }


        //공격시작시, 애니메이션에 호출할 이벤트 함수
        public void AttackStart()
        {
            //공격에 따른 히트박스collider 켜주기
            monsterColl.hitBox.enabled = true;

            //속도 저장
            tempSpeed = speed;
            maxTempSpeed = maxSpeed;

            //0 공격시에는 움직임 x 
            speed = 0;
            maxSpeed = 0;
        }

        //공격 끝날시, 애니메이션에 호출할 이벤트 함수
        public void AttackEnd()
        {
            //공격에 따른 히트박스collider 꺼주기
            monsterColl.hitBox.enabled = false;

            //저장한 속도 다시 대입
            speed = tempSpeed;
            maxSpeed = maxTempSpeed;
        }

        //에니메이션 이벤트함수에서 호출
        void Death()
        {
            //콜라이더 끄기
            collider2d.enabled = false;
            //리지드 바디 끄기
            rig.simulated = false;

            //사라지다가 삭제
            sr.color = Vector4.Lerp(sr.color, new Color(sr.color.r, sr.color.g, sr.color.b, 0f), 1f);

            if (sr.color.a <= 0.3f)
            {
                anim.enabled = false;

                //관련 파괴
                Destroy(gameObject);
            }
        }

    }//class
}//namespace