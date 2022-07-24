using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//�ݶ��̴��� ������ ��Ż Ȱ��ȭ == Scene �ε���
//�ϴ� ��ü������ �ۼ� ���� �� -> �Ϲ�ȭ ����
public class Potal : MonoBehaviour
{
    //����
    public BoxCollider2D collider_Trigger;

    //�̸�
    public string goToSceneName;
    //���ǰ˻�
    bool isEnterPotal;

    //ĳ���� ��ġ Ȯ��
    //public Transform 
    void Start()
    {
        //�Ҵ�
        collider_Trigger = GetComponent<BoxCollider2D>();
    }


    private void Update()
    {
        if (isEnterPotal)
        {
            SceneManager.LoadScene(goToSceneName);
        }
    }

    //���� ���� ��
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

