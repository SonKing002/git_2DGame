using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;

namespace Main
{

    //������, �ٸ� ������Ʈ�� ��ü�� �����ǵ��� �̱����� �ۼ�
    // -> (�ܼ��ϰ� �����ϱ� ���� ������Ʈ�� ��Ʈ��)

    //�ǽð� = ��ũ��Ʈ : ��� �޴��� (���� ����� = ��������) = Ŭ����namespace <- ��� (�̱���x)
    // -> (������ �����Ͽ� �����̵�  ���� ��ġ���� ��Ʈ��)

    //����.https://glikmakesworld.tistory.com/2

    public class Singleton : MonoBehaviour
    {
        // region = ���� ����
        #region ����
        // ����private
        private static Singleton instance;

        #endregion

        #region ������Ƽ Awake
        void Awake()
        {
            //��ü�� null�̶��
            if (null == instance)
            {
                //�� ��ũ��Ʈ�� ������ ���Եȴ�.
                instance = this;

                //���ӿ�����Ʈ�� �ı����� �ʰ� �Ѵ�.
                DontDestroyOnLoad(this.gameObject);
            }
            else //��ü�� null�� �ƴ϶��
            {
                //�� �̵��� �̹� �����Ѵٸ� == �ߺ�
                //���ο� ���� ������Ʈ�� �����Ѵ�
                Destroy(this.gameObject);
            }
        }
        #endregion

        #region ������Ƽ Instance
        //�ν��Ͻ��� ������ �� �ִ� public ������Ƽ. static�̹Ƿ� �ٸ� Ŭ�������� ���� ȣ���� �� �ִ�.
        public static Singleton Instance
        {
            get
            {
                if (null == instance)
                {
                    return null;
                }
                return instance;
            }
        }
        #endregion
    }//class
}//namespace
//��������.https://simppen-gamedev.tistory.com/4