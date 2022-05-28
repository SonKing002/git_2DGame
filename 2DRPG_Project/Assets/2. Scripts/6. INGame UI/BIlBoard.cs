using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;

namespace Main
{
    //그림자, 체력바, 닉네임이 캐릭터를 따라가도록 한다. == tag : Fallowing 
    //이유 회전값이 따라가므로 페런츠를 걸지 않는다

  
    public class BIlBoard : MonoBehaviour
    {
        //게임 오브젝트
        GameObject player;// 플레이어
        
        //변수
        public Vector2 dir; //필요한 만큼 인스펙터에서 수정


        private void Start()
        {
            //플래이어 find 할당
            player = FindObjectOfType<PlayerMove>().gameObject;

        }
        void Update()
        {
                transform.position = new Vector2(player.transform.position.x, player.transform.position.y) + dir;
            
        }
    }//class
}//namespace