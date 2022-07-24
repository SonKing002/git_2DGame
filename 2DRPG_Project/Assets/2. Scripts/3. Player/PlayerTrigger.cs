using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;
using UnityEngine.UI;

namespace Main
{
    //collider로 체크한 후에, Z키를 누르면 lerp로 플레이어 위치까지 아이템을 끌어온다.
    //Z키를 누른 상태 + 플래이어 콜라이더에 부딪히면 = 획득상태

    public class PlayerTrigger : MonoBehaviour
    {
        //이 콜라이더 안에 들어왔는지 체크
        bool isEnter;

        //npc 이모션
        public Animator equiptable;
        public Animator consumable;
        public Animator skill;
        public Animator bagStore;
        public Animator stage;

        //일반 상점 탭 목록
        public GameObject selectView;
        //일반 상점 구매 판매 목록
        public GameObject objectListView;
        //스테이지 목록
        public GameObject stageView;
        //상점 멘트용 GUIText
        public Text text_SelectViewHead;
        //임시_캐릭터와 닿은 Object
        GameObject tempEnteredGameObject;

        //아이템의 velocity를 이용
        Rigidbody2D item;

        //캐릭터
        public PlayerMove playerMove;

        //도착까지 음수 양수를 이용하여 방향을 설정
        float player_Destination;
        float destination;
        //스피드
        float dragSpeed;

        //시간 재기
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
            //Z버튼을 누르고 있으면 && 콜라이더 범위로 들어왔을때
            if (Input.GetKey(KeyCode.Z) && isEnter)
            {
                //시간 설정
                timeCheck = Time.deltaTime;
                //시간 캐치용
                tempTime = timeCheck;

                //하늘로 붕 떳다가
                if (tempTime < 0.5f)
                {
                    item.AddForce(Vector2.up * Time.deltaTime, ForceMode2D.Force);
                }
                else //플래이어에게 따라간다
                {
                    destination = Vector2.Distance(player.transform.position, item.gameObject.transform.position);
                    print(isEnter);
                    //해당 아이템이 캐릭터를 따라온다 //방향 설정
                    player_Destination = player.transform.position.x - item.gameObject.transform.position.x;
                    player_Destination = (player_Destination < 0) ? -1 : 1;
                }

                //방향 * 속도 = 거리
                item.velocity = new Vector2(player_Destination * dragSpeed, item.velocity.y);

                //가까이 접근했다면
                if (destination <= 1.05f)
                {
                    //파괴하기
                    Destroy(item.gameObject);
                }
            }
            else //떼거나 자리가 멀어지면
            {
                //null 방지
                if (item.gameObject.activeSelf == true)
                {
                    //멈춘다
                    item.velocity = new Vector2(0, 0);
                }
            }

            */

        }//update

        //가까이 접근하는 순간
        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case "NPC_Skill":

                    skill.SetTrigger("question");
                    skill.SetBool("isQuestion",true);

                    print("스킬 상인");
                    break;
                case "NPC_InventoryStore":

                    bagStore.SetTrigger("question");
                    bagStore.SetBool("isQuestion", true);

                    print("창고");
                    break;
                case "NPC_Equip":

                    equiptable.SetTrigger("question");
                    equiptable.SetBool("isQuestion", true);

                    print("장비 상인");
                    break;
                case "NPC_Consum":

                    consumable.SetTrigger("question");
                    consumable.SetBool("isQuestion", true);

                    print("소비 상인");
                    break;
                case "NPC_Stage":

                    stage.SetTrigger("question");
                    stage.SetBool("isQuestion", true);
                    stageView.SetActive(true);

                    print("스테이지");
                    break;
                case "Sitting":
                    //의자에 다가옴
                    playerMove.isChairFind = true;
                    //버튼 활성화
                    playerMove.else_Act_btn.interactable = true;

                    break;

                case "Item":

                    print("아이템 찾음");
                    //아이템을 저장
                    item = collision.gameObject.GetComponent<Rigidbody2D>();
                    print(item.name);
                    //아이템이 범위안에 들어옴
                    isEnter = true;
                    break;

                case "Ladder":
                    print("사다리 닿음");
                    //사다리 찾기 true
                    playerMove.isLadderFind = true;

                    playerMove.else_Act_btn.interactable = true;
                    break;

                case "Potal":
                    //다음 맵 이동
                    playerMove.isUsePotal = true;
                    break;
            }
        }

        //멀어지는 순간
        private void OnTriggerExit2D(Collider2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case "NPC_Skill":

                    skill.SetBool("isQuestion", false);
                    selectView.SetActive(false);

                    print("스킬 상인");
                    break;
                case "NPC_InventoryStore":

                    bagStore.SetBool("isQuestion", false);
                    selectView.SetActive(false);

                    print("창고");
                    break;
                case "NPC_Equip":

                    equiptable.SetBool("isQuestion", false);
                    selectView.SetActive(false);

                    print("장비 상인");
                    break;
                case "NPC_Consum":

                    consumable.SetBool("isQuestion", false);
                    selectView.SetActive(false);

                    print("소비 상인");
                    break;
                case "NPC_Stage":

                    stage.SetBool("isQuestion", false);
                    stageView.SetActive(false);
                    
                    print("스테이지");
                    break;
                case "Sitting":
                    //의자에서 벗어남
                    playerMove.isChairFind = false;
                    //버튼 비활성화
                    playerMove.else_Act_btn.interactable = false;

                    break;

                case "Item":
                    isEnter = false;
                    break;

                case "Ladder":
                    print("사다리 벗어남");
                    //사다리 찾기 false
                    playerMove.isLadderFind = false;
                    playerMove.isStartLadder = false;

                    //mask 추가
                    playerMove.Gruond_ColliderMask_Ctrl(true);
                    break;

                case "Potal":
                    //다음 맵 이동
                    playerMove.isUsePotal = true;
                    break;
            }
        }

        //npc 이모션을 클릭하면, 창이 활성화 (움직일때 화면 가려서 불편)
        public void OnClick_PopUpedEmotion_Btn()
        {
            selectView.SetActive(true);
        }

    }//class
}//namespace