using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;

namespace Main
{
    public class PlayerColl : MonoBehaviour
    {
        //컴포넌트
        PlayerMove playerMove; //캐릭터

        void Start()
        {
            playerMove = GetComponent<PlayerMove>(); //같은 오브젝트 내에 있음
        }

        //콜라이더가 닿은 순간
        private void OnCollisionEnter2D(Collision2D collision)
        {
            //태그 검색
            switch (collision.gameObject.tag)
            {
                case "Ground":
                    playerMove.isGround = true; // 땅에 있는가 

                    //베이스 땅이 아닐 때
                    if (collision.gameObject.name != "BGround")
                    {
                        //하단 점프 조건 성립
                        playerMove.isGround_ToAble_UnderJump = true;
                    }

                    break;

            }
        }

        //콜라이더가 떨어지는 순간
        private void OnCollisionExit2D(Collision2D collision)
        {
            //테그 검색
            switch (collision.gameObject.tag)
            {
                case "Ground":
                    playerMove.isGround = false;// 공중에 있는가
                    
                    playerMove.isGround_ToAble_UnderJump = false; //발판에서 벗어나면 false
                    
                    print("공중");
                    break;
            }
        }//trigger
    }//class
}//namespace