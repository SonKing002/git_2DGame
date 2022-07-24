using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//콜라이더가 닿으면 포탈 활성화 == Scene 로드방식
//일단 구체적으로 작성 구현 후 -> 일반화 정리
public class Potal : MonoBehaviour
{
    //선언
    public BoxCollider2D collider_Trigger;

    //이름
    public string goToSceneName;
    //조건검사
    bool isEnterPotal;

    //캐릭터 위치 확인
    //public Transform 
    void Start()
    {
        //할당
        collider_Trigger = GetComponent<BoxCollider2D>();
    }


    private void Update()
    {
        if (isEnterPotal)
        {
            SceneManager.LoadScene(goToSceneName);
        }
    }

    //접근 들어올 때
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                isEnterPotal = true;
                break;
        } 
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                isEnterPotal = false;
                break;
        }
    }
}

