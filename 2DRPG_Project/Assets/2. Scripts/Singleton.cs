using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;

namespace Main
{

    //유아이, 다른 오브젝트들 자체가 유지되도록 싱글톤을 작성
    // -> (단순하게 동작하기 위한 오브젝트들 컨트롤)

    //실시간 = 스크립트 : 모든 메니저 (변수 저장용 = 전역변수) = 클래스namespace <- 사용 (싱글톤x)
    // -> (유저가 진행하여 변경이된  변수 수치들을 컨트롤)

    //참고.https://glikmakesworld.tistory.com/2

    public class Singleton : MonoBehaviour
    {
        // region = 구역 정의
        #region 변수
        // 보안private
        private static Singleton instance;

        #endregion

        #region 프로퍼티 Awake
        void Awake()
        {
            //객체가 null이라면
            if (null == instance)
            {
                //이 스크립트가 변수에 대입된다.
                instance = this;

                //게임오브젝트는 파괴되지 않게 한다.
                DontDestroyOnLoad(this.gameObject);
            }
            else //객체가 null이 아니라면
            {
                //씬 이동시 이미 존재한다면 == 중복
                //새로운 씬의 오브젝트를 삭제한다
                Destroy(this.gameObject);
            }
        }
        #endregion

        #region 프로퍼티 Instance
        //인스턴스에 접근할 수 있는 public 프로퍼티. static이므로 다른 클래스에서 맘껏 호출할 수 있다.
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
//사용법참고.https://simppen-gamedev.tistory.com/4