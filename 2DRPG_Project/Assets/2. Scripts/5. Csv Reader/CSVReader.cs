using System.Collections.Generic;
//추가한 유징
using System.Text.RegularExpressions; //Regex가 정의 되어있음 사용하기 위해 using : 정규표현식
using UnityEngine;

namespace Main
{
    //앞으로 csv 파일을 읽어들일때 사용할 클래스

    /*참고 사이트
    
    //참고0.http://www.devkorea.co.kr/bbs/board.php?bo_table=m03_lecture&wr_id=3775 (테이블데이터 간단소개)
    //참고1.https://m.blog.naver.com/PostView.naver?isHttpsRedirect=true&blogId=bbulle&logNo=220158917236 (엑셀테이블을 활용)
    //참고2.https://bravenewmethod.com/2014/09/13/lightweight-csv-reader-for-unity/#comment-7111 (리더 스크립트 코드 복사)

    //참고3.https://mentum.tistory.com/214 (에러사항)
    // \n을 LINE_SPLIT_RE로 사용하기 때문에 개행문자를 입력못하는 문제가 있어서 <br>을 대신 사용하도록 변경.
    // 개행을 엔터로 해버리면 아래코드에서는 The given key was not present in the dictionary.오류가 나니 주의
    */

    public class CSVReader
    {
        static string split_re = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
        static string line_splite_re = @"\r\n|\n\r|\n|\r";
        static char[] trim_chars = { '\"' };

        //@ 문자를 그대로 인식, 여러줄 문자열 사용시, 복수 문자열의 데이터 지정
        // \r CR Cariage return \n Line feed :타자기 유래

        //정규표현식 = 텍스트관련 프로세싱 람다식
        //무슨무슨 포맷의 패턴을 찾을때 사용함 -> 다른 문자열로 변환도 가능 , 유효성검사 가능

        public static List<Dictionary<string, object>> Read(string file)
        {
            //리스트
            var list = new List<Dictionary<string, object>>();
            TextAsset data = Resources.Load(file) as TextAsset;

            //행  = 정규표현식대로 data의 .text를 자른다.
            var lines = Regex.Split(data.text, line_splite_re);

            if (lines.Length <= 1)
            {
                return list;
            }


            var header = Regex.Split(lines[0], split_re);
            for (var i = 1; i < lines.Length; i++)
            {
                var values = Regex.Split(lines[i], split_re);
                if (values.Length == 0 || values[0] == "")
                {
                    continue;
                }

                var entry = new Dictionary<string, object>();
                for (var j = 0; j < header.Length && j < values.Length; j++)
                {
                    string value = values[j];
                    value = value.TrimStart(trim_chars).TrimEnd(trim_chars).Replace("\\", "");

                    value = value.Replace("<br>", "\n"); // 추가된 부분. 개행문자를 \n대신 <br>로 사용한다.
                    value = value.Replace("<c>", ",");

                    object finalvalue = value;

                    int n;
                    float f;

                    if (int.TryParse(value, out n))
                    {
                        finalvalue = n;
                    }
                    else if (float.TryParse(value, out f))
                    {
                        finalvalue = f;
                    }

                    entry[header[j]] = finalvalue;
                }
                list.Add(entry);
            }
            return list;
        }// public static List<Dictionary<string, object>> Read(string file)
        //코드 수행하고 난 후, List 안에 2차원 배열의 형태로 데이터가 저장된다.
        //data[행번지수]["Header이름"] = Value

    }//class
}//namespace