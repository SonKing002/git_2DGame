using System.Collections.Generic;
//�߰��� ��¡
using System.Text.RegularExpressions; //Regex�� ���� �Ǿ����� ����ϱ� ���� using : ����ǥ����
using UnityEngine;

namespace Main
{
    //������ csv ������ �о���϶� ����� Ŭ����

    /*���� ����Ʈ
    
    //����0.http://www.devkorea.co.kr/bbs/board.php?bo_table=m03_lecture&wr_id=3775 (���̺����� ���ܼҰ�)
    //����1.https://m.blog.naver.com/PostView.naver?isHttpsRedirect=true&blogId=bbulle&logNo=220158917236 (�������̺��� Ȱ��)
    //����2.https://bravenewmethod.com/2014/09/13/lightweight-csv-reader-for-unity/#comment-7111 (���� ��ũ��Ʈ �ڵ� ����)

    //����3.https://mentum.tistory.com/214 (��������)
    // \n�� LINE_SPLIT_RE�� ����ϱ� ������ ���๮�ڸ� �Է¸��ϴ� ������ �־ <br>�� ��� ����ϵ��� ����.
    // ������ ���ͷ� �ع����� �Ʒ��ڵ忡���� The given key was not present in the dictionary.������ ���� ����
    */

    public class CSVReader
    {
        static string split_re = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
        static string line_splite_re = @"\r\n|\n\r|\n|\r";
        static char[] trim_chars = { '\"' };

        //@ ���ڸ� �״�� �ν�, ������ ���ڿ� ����, ���� ���ڿ��� ������ ����
        // \r CR Cariage return \n Line feed :Ÿ�ڱ� ����

        //����ǥ���� = �ؽ�Ʈ���� ���μ��� ���ٽ�
        //�������� ������ ������ ã���� ����� -> �ٸ� ���ڿ��� ��ȯ�� ���� , ��ȿ���˻� ����

        public static List<Dictionary<string, object>> Read(string file)
        {
            //����Ʈ
            var list = new List<Dictionary<string, object>>();
            TextAsset data = Resources.Load(file) as TextAsset;

            //��  = ����ǥ���Ĵ�� data�� .text�� �ڸ���.
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

                    value = value.Replace("<br>", "\n"); // �߰��� �κ�. ���๮�ڸ� \n��� <br>�� ����Ѵ�.
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
        //�ڵ� �����ϰ� �� ��, List �ȿ� 2���� �迭�� ���·� �����Ͱ� ����ȴ�.
        //data[�������]["Header�̸�"] = Value

    }//class
}//namespace