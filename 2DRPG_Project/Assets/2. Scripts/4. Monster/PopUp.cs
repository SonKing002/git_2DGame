using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Main;

namespace Main
{
    //���� �������� �˾��� �ǰ� õõ�� ������� ����  : ������ ������ ���� �̼��� �ӽ� -10�ϵ��ڵ�

    public class PopUp : MonoBehaviour
    {
        //�θ������Ʈ�� transform
        public GameObject p_EnemyDamaged;// ���� �θ� ������Ʈ
        public GameObject p_PlayerDamaged;// �÷��̾��� �θ� ������Ʈ

        //����� �˾� ������ Text
        public Text[] enemyText = new Text[20];
        public Text[] playerText = new Text[20];
        int tempPlayerCount;
        int tempEnemyCount;

        //�����̻� �̹���
        //�÷��̾��� �»�� ����â

        //����
        public bool isAttackedPlayer; //�÷��̾� ��ȭ�� �ʿ��Ҷ� = true; //trigger ������ false
        public bool isAttackedEnemy; // �� ��ȭ�� �ʿ��Ҷ� = true; //trigger ������ false

        bool isHealedPlayer; // ����ȿ���� �� (ȸ��,���� �Լ�) = true; 
        bool isHealedEnemy; // ����ȿ���� �� (ȸ��,���� �Լ�) = true; 

        void Start()
        {

            //�÷��̾��� ������ƮǮ ����
            for (int i = 0; i < playerText.Length; i++)
            {
                //text ������Ʈ (�÷��̾� ĵ���� ������ ����)
                GameObject objectText1 = Instantiate(Resources.Load("PlayerDamagedPower"), p_PlayerDamaged.transform) as GameObject;

                //����
                playerText[i] = objectText1.GetComponent<Text>();

                //��Ȱ��ȭ
                playerText[i].gameObject.SetActive(false);
            }

            //���� ������ƮǮ ���� 
            for (int i = 0; i < enemyText.Length; i++)
            {
                //text ������Ʈ (�� ĵ���� ������ ����)
                GameObject objectText2 = Instantiate(Resources.Load("MonsterDamagedPower"), p_EnemyDamaged.transform) as GameObject;

                //����
                enemyText[i] = objectText2.GetComponent<Text>();

                //��Ȱ��ȭ
                enemyText[i].gameObject.SetActive(false);
            }
        }

        //���ݹ��� ��ü�� �����ϴ� �Լ� : PlayerAttack, MonseterCtrl �Ǵ� PlayerColl ��ũ��Ʈ���� ����
        public void WhichDamaged(GameObject thisObject)
        {
            //�ױ� �� == �÷��̾��϶�
            if(thisObject.transform.CompareTag("Player"))
            {
                //�����ϴ� ������ :true ��û
                isAttackedPlayer = true; //�÷��̾ �ǰݴ��ϴ� ��
                //isAttackedEnemy = false;

                //���ݹ޴� �Լ�(�÷��̾� ǥ�ÿ� ������Ʈ ����ֱ�)
                DamagedScore(thisObject, playerText[tempPlayerCount]);

                //Ȯ�ο�
                //print(tempEnemyCount);

                //index ����ϴ� ���
                if (tempPlayerCount < 20)
                {
                    //���� �غ�Ǿ� �ִ� ������Ʈ ���(��� ���Ѵ�.)
                    tempPlayerCount++;
                }
                else
                {
                    //������ �ٽ� ó������
                    tempPlayerCount = 0;
                }
            }

            //�ױ� �� == ���϶�
            if (thisObject.transform.CompareTag("Enemy"))
            {
                //�����ϴ� ������ :false ��û
                //isAttackedPlayer = false;
                isAttackedEnemy = true; //�� �ǰ� ���ϴ� ��

                //���ݹ޴� �Լ�(�� ǥ�ÿ� ������Ʈ ����ֱ�)
                DamagedScore(thisObject,enemyText[tempEnemyCount]);

                //Ȯ�ο�
                print(tempEnemyCount);

                //index ����ϴ� ���
                if (tempEnemyCount < 20)
                {
                    //���� �غ�Ǿ� �ִ� ������Ʈ ���(��� ���Ѵ�.)
                    tempEnemyCount++;
                }
                else
                {
                    //������ �ٽ� ó������
                    tempEnemyCount = 0;
                }
            }

            //�ٸ� ��ü�϶�
            if (!thisObject.transform.CompareTag("Player") && !thisObject.transform.CompareTag("Enemy"))
            {
                isAttackedPlayer = false;
                isAttackedEnemy = false;

            }
        }

        //���� ���� tempGameObject ����,�÷��̾� (���߿� ����ȭ�Ҷ� ����� ���� ������ �׳� ����)
        public void DamagedScore(GameObject tempGameObject,Text whichText)
        {

            //�ڷ�ƾ ������
            StartCoroutine(AnimationPopUpCorutine(whichText));
        }

        //�ڷ�ƾ �ǰݹ޾����� ���ǿ� ���� trigger �۵�
        IEnumerator AnimationPopUpCorutine(Text whichText)
        {
            //��Ȱ�� ���¶��
            if (!whichText.gameObject.activeSelf)
            {

                //Ȱ��ȭ�� �Ŀ�
                whichText.gameObject.SetActive(true);

                //�÷��̾��϶�
                if (isAttackedPlayer && !isAttackedEnemy)
                {
                    //�÷��̾� �ǰ� text ��� (���� : ���İ� ����)
                    whichText.GetComponent<Animator>().SetTrigger("NormalDamage");//�ִϸ��̼� ���� ��Ȱ��ȭ false ����

                }
                //���϶�
                if (!isAttackedPlayer && isAttackedEnemy)
                {
                    //�� �ǰ� text ��� (���� : ���İ� ����)
                    whichText.GetComponent<Animator>().SetTrigger("NormalDamage");//�ִϸ��̼� ���� ��Ȱ��ȭ false ����
                }

                //�����Ҷ� tag�� �����Ͽ� �ѹ��� �����ϱ� ����
                isAttackedPlayer = false;
                isAttackedEnemy = false;

                yield return new WaitForSeconds(0.5f);
            }
        }

        //���� ȸ����
        void RestoreScore()
        { 
        
        }
    }
}