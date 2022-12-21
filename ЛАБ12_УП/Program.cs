using System;
using System.Text.RegularExpressions;

namespace ЛАБ12_УП
{
    class RegExp
    {

        public RegExp(string pattern, string txt)
        {
            r = new Regex(pattern.ToLower());
            text = txt;
        }
        private Regex r;
        private string text;

        public bool Exist()
        {
            if (r.IsMatch(text.ToLower()))
            {
                Console.WriteLine("Текст содержит фрагменты, соответствующие шаблону");
                return r.IsMatch(text);
            }
            else
            {
                Console.WriteLine("Текст не содержит фрагменты, соответствующие шаблону");
                return false;
            }
        }

        public void ShowMatches()
        {
            MatchCollection m = r.Matches(text.ToLower());
            foreach (Match x in m)
                Console.Write(x.Value + " ");
        }

        public string DeleteMatches()
        {
            MatchCollection m = r.Matches(text.ToLower());
            string s = text.ToLower();
            string ttt = text;
            foreach (Match x in m)
            {
                int i = s.IndexOf(x.Value);
                int l = x.Value.Length;
                s = s.Remove(i, l);
                ttt = ttt.Remove(i, l);
            }
            Console.WriteLine(ttt);
            text = ttt;
            return text;
        }

        public Regex R
        {
            get { return r; }
            set { r = value; }
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public bool TextCheck(out bool check)
        {
            if (text != string.Empty)
            {
                check = true;
                return check;
            }
            else
            {
                check = false;
                return check;
            }
        } 

       
        public object CheckType(int n)
        {
            if (n == 0)
            {
                Console.WriteLine(R);
                return new Regex("");
            }
            else if (n == 1)
            {
                Console.WriteLine(text);
                return "string";
            }
            else Console.WriteLine("ОШИБКА");
            return null;
        }



        public object this[int i]
        {

            set
            {

                var result = CheckType(i);
                if (result.GetType() == typeof(Regex))
                {
                    R = (Regex)value;
                }
                else
                {
                    text = value.ToString();
                }
            }

            get
            {
                var result = CheckType(i);
                if (result.GetType() == typeof(Regex))
                {
                    return R;
                }
                else
                {
                    return text;
                }

            }
        }

        public string AddText(ref string str)
        {
            Console.WriteLine(text + str);
            return text;
        }
        
        public static string operator -(RegExp obj, Regex r)
        {
            MatchCollection m = r.Matches(obj.text.ToLower());
            string s = obj.text.ToLower();
            string ttt = obj.text;
            foreach (Match x in m)
            {
                int i = s.IndexOf(x.Value);
                int l = x.Value.Length;
                s = s.Remove(i, l);
                ttt = ttt.Remove(i, l);
            }
            obj.text = ttt;
            return obj.text;
        }

        public static implicit operator string(RegExp obj)
        {
            return obj.R.ToString();
        }

        public static implicit operator RegExp(string Text)
        {
            RegExp reg = new RegExp(Text, "");
            return reg;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите ваш текст: ");
            string text = Console.ReadLine();
            Console.Write("Введите регулярное выражение: ");
            string pattern = Console.ReadLine();
            RegExp A = new RegExp(pattern, text);
            A.Exist();
            Console.WriteLine("Фрагменты, не соответствующие шаблону:");
            A.ShowMatches();
            Console.WriteLine("\nОставшийся текст, соответствующий шаблону:");
            A.Text = A.DeleteMatches();
            Console.Write("Введите индекс: ");
            int index = int.Parse(Console.ReadLine());
            A.CheckType(index);
            Console.Write("Этот текст пустой? Ответ: ");
            A.TextCheck(out bool check);
            if (check == true) Console.WriteLine("Нет");
            else Console.WriteLine("Да");
            Console.WriteLine("Удаление всех фрагментов, соответствующих значению шаблона");
            Regex regex = new Regex(pattern);
            Console.WriteLine(A - regex);
            Console.Write("Введите строку, которую надо добавить в текст: ");
            string str = Console.ReadLine();
            A.AddText(ref str);
            string d = (RegExp)A;
            Console.WriteLine("Преобразование шаблона в string: {0}", d);
            A = (string) d;
            Console.WriteLine("Преобразование шаблона в Regex: {0}", new Regex (A));
        }
    }
}
