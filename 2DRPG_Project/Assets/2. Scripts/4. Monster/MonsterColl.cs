using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;

namespace Main
{
    //공격 히트박스 검사
    public class MonsterColl : MonoBehaviour
    {
        //스크립트
        public BoxCollider2D hitBox; //공격판정 범위

        //조정용
        public float temp_FrontBox_X; //히트박스 위치 조정용 ( TorchLighter.x +-0.3044 )
        public float temp_FrontBox_Y; //히트박스 위치 조정용

        void Start()
        {
            //컴포넌트
            hitBox = GetComponent<BoxCollider2D>();

            //임의조정 (SCV 몬스터마다 다르게 설정 필요)
            temp_FrontBox_X = 0.3044f;
            temp_FrontBox_Y = 0.5923f; 
        }

        //히트박스에 닿는 순간
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //tag 검사
            switch (collision.transform.tag)
            {
                //플래이어라면
                case "Player":
                    //플레이어의 피격함수에 호출 메세지를 전달
                    collision.SendMessage("Damaged");
                    break;
            }
        }

        //오른쪽을 바라볼때 (MonsterCtrl참조)
        public void RightDirBox()
        {
            //부모기준 위치 조정
            transform.localPosition =
                new Vector2(temp_FrontBox_X, temp_FrontBox_Y);
        }

        //왼쪽을 바라볼때 (MonsterCtrl참조)
        public void LeftDirBox()
        {
            //부모기준 위치 조정
           transform.localPosition = 
                new Vector2(-temp_FrontBox_X, temp_FrontBox_Y);
        }
    }
}