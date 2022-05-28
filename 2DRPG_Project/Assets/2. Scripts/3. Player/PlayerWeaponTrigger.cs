using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;

namespace Main
{
    public class PlayerWeaponTrigger : MonoBehaviour
    {
        //불형 체크
        public bool isOnAttackRange; //공격범위에 들어오면

        //임시 에너미 받아오기
        public CapsuleCollider2D weapon_CapsuleCollider;
        public GameObject temp_Enemy;
        public int targetCount;

        private void Start()
        {
            targetCount = 0;
        }

        //무기에 어떤 물체가 왔을 때 
        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case "Enemy":
                    if (targetCount == 0)
                    {
                        print("데미지 시작");

                        //해당 몬스터 받아오기
                        temp_Enemy = collision.gameObject;

                        //몬스터의 피격
                        collision.gameObject.SendMessage("Damaged");
                        //한번 호출 후 콜라이더 끄기
                        weapon_CapsuleCollider.enabled = false;

                        targetCount++;
                    }

                    //초기화
                    targetCount = 0;

                    break;
            }
        }

        //무기에 어떤 물체가 나갔을 때
        private void OnTriggerExit2D(Collider2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case "Enemy":

                    /*
                    //초기화
                    temp_Enemy = null;
                    */
                    print("나갔다");
                    break;
            }
        }
    }

}