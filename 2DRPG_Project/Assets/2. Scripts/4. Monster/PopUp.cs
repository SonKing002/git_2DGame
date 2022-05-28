using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Main;

namespace Main
{
    //받은 데미지가 팝업이 되고 천천히 사라지는 연출  : 몬스터의 데미지 아직 미설정 임시 -10하드코딩

    public class PopUp : MonoBehaviour
    {
        //부모오브젝트의 transform
        public GameObject p_EnemyDamaged;// 적의 부모 오브젝트
        public GameObject p_PlayerDamaged;// 플레이어의 부모 오브젝트

        //사용할 팝업 데미지 Text
        public Text[] enemyText = new Text[20];
        public Text[] playerText = new Text[20];
        int tempPlayerCount;
        int tempEnemyCount;

        //상태이상 이미지
        //플래이어의 좌상단 버프창

        //제어
        public bool isAttackedPlayer; //플레이어 변화가 필요할때 = true; //trigger 끝나면 false
        public bool isAttackedEnemy; // 적 변화가 필요할때 = true; //trigger 끝나면 false

        bool isHealedPlayer; // 좋은효과일 때 (회복,버프 함수) = true; 
        bool isHealedEnemy; // 좋은효과일 때 (회복,버프 함수) = true; 

        void Start()
        {

            //플래이어의 오브젝트풀 생성
            for (int i = 0; i < playerText.Length; i++)
            {
                //text 오브젝트 (플래이어 캔버스 하위에 생성)
                GameObject objectText1 = Instantiate(Resources.Load("PlayerDamagedPower"), p_PlayerDamaged.transform) as GameObject;

                //대입
                playerText[i] = objectText1.GetComponent<Text>();

                //비활성화
                playerText[i].gameObject.SetActive(false);
            }

            //적의 오브젝트풀 생성 
            for (int i = 0; i < enemyText.Length; i++)
            {
                //text 오브젝트 (적 캔버스 하위에 생성)
                GameObject objectText2 = Instantiate(Resources.Load("MonsterDamagedPower"), p_EnemyDamaged.transform) as GameObject;

                //대입
                enemyText[i] = objectText2.GetComponent<Text>();

                //비활성화
                enemyText[i].gameObject.SetActive(false);
            }
        }

        //공격받은 객체를 구분하는 함수 : PlayerAttack, MonseterCtrl 또는 PlayerColl 스크립트에서 참조
        public void WhichDamaged(GameObject thisObject)
        {
            //테그 비교 == 플래이어일때
            if(thisObject.transform.CompareTag("Player"))
            {
                //참조하는 곳에서 :true 요청
                isAttackedPlayer = true; //플래이어가 피격당하는 중
                //isAttackedEnemy = false;

                //공격받는 함수(플래이어 표시용 오브젝트 담아주기)
                DamagedScore(thisObject, playerText[tempPlayerCount]);

                //확인용
                //print(tempEnemyCount);

                //index 사용하는 방식
                if (tempPlayerCount < 20)
                {
                    //다음 준비되어 있는 오브젝트 사용(계속 더한다.)
                    tempPlayerCount++;
                }
                else
                {
                    //넘으면 다시 처음으로
                    tempPlayerCount = 0;
                }
            }

            //테그 비교 == 적일때
            if (thisObject.transform.CompareTag("Enemy"))
            {
                //참조하는 곳에서 :false 요청
                //isAttackedPlayer = false;
                isAttackedEnemy = true; //적 피격 당하는 중

                //공격받는 함수(적 표시용 오브젝트 담아주기)
                DamagedScore(thisObject,enemyText[tempEnemyCount]);

                //확인용
                print(tempEnemyCount);

                //index 사용하는 방식
                if (tempEnemyCount < 20)
                {
                    //다음 준비되어 있는 오브젝트 사용(계속 더한다.)
                    tempEnemyCount++;
                }
                else
                {
                    //넘으면 다시 처음으로
                    tempEnemyCount = 0;
                }
            }

            //다른 물체일때
            if (!thisObject.transform.CompareTag("Player") && !thisObject.transform.CompareTag("Enemy"))
            {
                isAttackedPlayer = false;
                isAttackedEnemy = false;

            }
        }

        //입은 피해 tempGameObject 몬스터,플래이어 (나중에 세분화할때 사용할 예정 지금은 그냥 나둠)
        public void DamagedScore(GameObject tempGameObject,Text whichText)
        {

            //코루틴 돌리기
            StartCoroutine(AnimationPopUpCorutine(whichText));
        }

        //코루틴 피격받았을때 조건에 따라 trigger 작동
        IEnumerator AnimationPopUpCorutine(Text whichText)
        {
            //비활성 상태라면
            if (!whichText.gameObject.activeSelf)
            {

                //활성화한 후에
                whichText.gameObject.SetActive(true);

                //플래이어일때
                if (isAttackedPlayer && !isAttackedEnemy)
                {
                    //플레이어 피격 text 재생 (연출 : 알파값 제어)
                    whichText.GetComponent<Animator>().SetTrigger("NormalDamage");//애니메이션 끝에 비활성화 false 존재

                }
                //적일때
                if (!isAttackedPlayer && isAttackedEnemy)
                {
                    //적 피격 text 재생 (연출 : 알파값 제어)
                    whichText.GetComponent<Animator>().SetTrigger("NormalDamage");//애니메이션 끝에 비활성화 false 존재
                }

                //공격할때 tag로 제어하여 한번만 실행하기 위함
                isAttackedPlayer = false;
                isAttackedEnemy = false;

                yield return new WaitForSeconds(0.5f);
            }
        }

        //받은 회복량
        void RestoreScore()
        { 
        
        }
    }
}