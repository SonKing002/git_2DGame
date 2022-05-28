using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Main;
using UnityEngine.EventSystems;
using TMPro;

namespace Main
{
    //공격 , 피격 , 방어
    public class Player_Attack_Defend : MonoBehaviour
    {
        //컴포넌트
        [HideInInspector]
        public Animator anim; //애니메이션 : 공격,피격,방어
        PlayerMove playerMove; //움직임 스크립트: 땅 체크 변수참조
        public PlayerWeaponTrigger playerWeaponTrigger_Script; //콜라이더 체크 : 닿을때 명령 수행
        public CapsuleCollider2D playerWeaponTrigger_Component;
        //EnumPlayer_Information enumPlayer_Equipment; //현재 내 장비 스크립트 : 무기불러오기

        //타 컴포넌트
        public Slider popUpHp; //ui 반응형 체력
        public Slider popUpMp; //ui 반응형 마력
        public TextMeshProUGUI playerCurrentHP;//현재 남은 체력 text

        public Animator attackEffect;
        public Animator attackedEffect;
        ShakeCam shakeCam;
        public UsePopUp damagedUp;
        //MonsterCtrl monsterCtrl; //Enemy 스크립트

        //조이스틱 명령
        public Button basicAttack_btn;
        public Button shield_btn;
        public Button skill2_btn;
        public Button skill3_btn;

        //임시 변수
        [HideInInspector]
        public float hp; //플레이어 체력
        
        int randomMotion; // 공격모션: 0이 나오면 찌르기, 1이나오면 휘두르기

        //피격시 컬러 색 임시 변수
        [HideInInspector]
        public Color temp_damgedEffectColor;

        //제어 변수
        public int attackCount;  // 공격 횟수 제한용 일반공격 1회 스킬공격 몇회..
        public bool isDefense; // 막기 시전시 전방 공격 방어

        void Start()
        {
            //임시
            hp = 100;

            // 할당
            anim = GetComponentInChildren<Animator>(); //애니메이션
            playerMove = GetComponent<PlayerMove>(); //캐릭터 움직임 스크립트
            //enumPlayer_Equipment = GetComponent<EnumPlayer_Information>(); //현재 장비 스크립트

            // Find 할당
            //monsterCtrl = FindObjectOfType<MonsterCtrl>(); //몬스터
            popUpHp = GameObject.Find("SliderPlayerHp").GetComponent<Slider>(); //체력
            popUpMp = GameObject.Find("SliderPlayerMp").GetComponent<Slider>(); //마력
            shakeCam = FindObjectOfType<ShakeCam>();
        }

        //공격 모션 조건
        public void Attack()
        {
            if (attackCount == 0 && !playerMove.isSitting)
            {
                //공격 카운트 증가
                attackCount++;

                //컴포넌트 활성화
                playerWeaponTrigger_Component.enabled = true;

                //랜덤 모션 결정
                randomMotion = Random.Range(0, 2);

                //공격 이펙트

                //땅에서 공격
                if (randomMotion == 0)
                {
                    // 애니메이션 호출 Receiver(애니메이션에서 북마크에 따라 공격 여부 작용)
                    anim.SetTrigger("stab");
                }
                //공중에서 공격
                else if (randomMotion == 1)
                {
                    anim.SetTrigger("swing");
                }
            }
        }

        //닿았을때 , MonsterColl로부터 SendMessage를 통해 전달 받는 함수
        public void Damaged()
        {
            if (isDefense)
            {
                //방어 이펙트
                attackedEffect.SetTrigger("Defense");
                shakeCam.ViberateForOneTime(0.02f, 0.1f); //0.005의 진동으로 0.5f초 동안 랜덤 흔들림

            }
            //방어를 하고 있지 않을때
            else
            {
                //피격 이펙트
                attackedEffect.SetTrigger("Damaged");
                shakeCam.ViberateForOneTime(0.07f,0.2f); //0.005의 진동으로 0.5f초 동안 랜덤 흔들림

                //임시
                hp -= 10;
                popUpHp.value = hp * 0.01f;
                playerCurrentHP.text = hp + "/" + "100";

                if (hp > 0)
                {
                    anim.SetBool("isDamaged", true);//애니메이션 조건 true
                    anim.SetTrigger("damaged");

                    //데미지 받을때 text 띄우기 호출함수
                    damagedUp.WhichDamaged(gameObject);
                }
                else
                {
                    playerCurrentHP.text = "0" + "/" + "100";
                    anim.SetTrigger("death");
                }
            }
        }

        //키를 누르면 -> 방어를 위한 콜라이더 활성화
        void Shield()
        {
            if (isDefense && !playerMove.isSitting) //방어 중이라면
            {
                attackCount = 0;

                playerWeaponTrigger_Component.enabled = false;

                if (playerMove.isGround)//지상
                {
                    //방어모션
                    anim.SetTrigger("shield");
                    anim.SetBool("isDenfend", true);
                }
                else//공중
                {
                    //방어모션
                    anim.SetTrigger("jumpShield");
                    anim.SetBool("isDenfend", true);
                }
            }
        }

        void Update()
        {
            print("공격카운트 : " +attackCount);
            //Event.current.isMouse.
            //공격 버튼을 눌렀을때  + 유아이 위에 마우스가 없을때
            if (Input.GetButtonDown("Fire1") && !EventSystem.current.IsPointerOverGameObject())
            {
                //공격 모션
                Attack();
            }

            /*
            //막기 버튼을 눌렀을때 //모바일 버튼이랑 겹침
            if (Input.GetButton("Fire3"))
            {
                //방어 활성화
                isDefense = true;
            }
            else
            {
                //방어 비활성화
                isDefense = false;
            }
            */

            //방어
            Shield();

        }//update
    }//class
}//namespace