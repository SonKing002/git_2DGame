using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;

namespace Main
{
    //몬스터에게 닿은 물체 검사
    public class MonsterColl : MonoBehaviour
    {
        public MonsterCtrl monsterCtrl;

        //닿은 순간
        //(콜라이더에서 캐릭터랑 닿으면 + !isAttack 일때) 공격
        private void OnCollisionEnter2D(Collision2D collision)
        {
            switch (collision.collider.tag)
            {
                //플래이어
                case "Player":
                    //공격중이 아닐 때
                    if (!monsterCtrl.isAttack)
                    {
                        //공격 애니메이션 재생
                        monsterCtrl.anim.SetBool("isAttack", true);
                        monsterCtrl.anim.SetTrigger("attack");

                        print("재생");

                        //공격 주기 초기화
                        monsterCtrl.attackTimer = 0f;
                        monsterCtrl.temp_TimeCheck = 0;
                    }
                    break;
            }
        }

        //떨어지는 순간
        private void OnCollisionExit2D(Collision2D collision)
        {
            switch (collision.collider.tag)
            {
                //플래이어
                case "Player":
                    
                    break;
            }
        }
    }
}
