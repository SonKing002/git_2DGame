using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;

namespace Main
{
    //�׸���, ü�¹�, �г����� ĳ���͸� ���󰡵��� �Ѵ�. == tag : Fallowing 
    //���� ȸ������ ���󰡹Ƿ� �䷱���� ���� �ʴ´�

  
    public class BIlBoard : MonoBehaviour
    {
        //���� ������Ʈ
        GameObject player;// �÷��̾�
        
        //����
        public Vector2 dir; //�ʿ��� ��ŭ �ν����Ϳ��� ����


        private void Start()
        {
            //�÷��̾� find �Ҵ�
            player = FindObjectOfType<PlayerMove>().gameObject;

        }
        void Update()
        {
                transform.position = new Vector2(player.transform.position.x, player.transform.position.y) + dir;
            
        }
    }//class
}//namespace