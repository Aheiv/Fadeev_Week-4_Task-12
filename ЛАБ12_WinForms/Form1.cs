using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ЛАБ12_WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            textBox4.Text = "";
            string pattern = textBox2.Text;
            string text = textBox1.Text;
            RegExp A = new RegExp(text, pattern);
            A.Exist(textBox5);
            A.ShowMatches(textBox3);
            A.Text = A.DeleteMatches(textBox4);
            A.TextCheck(out bool check);
            if (check == true) textBox7.Text = "Нет";
            else textBox7.Text = "Да";
            label6.Visible = true;
            button2.Visible = true;
            numericUpDown1.Visible = true;
            textBox6.Visible = true;
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index = int.Parse(numericUpDown1.Text);
            string pattern = textBox2.Text;
            string text = textBox4.Text;
            RegExp A = new RegExp(text, pattern);
            A.CheckType(index, textBox6);
            label8.Visible = true;
            textBox8.Visible = true;
            button3.Visible = true;
            Regex regex = new Regex(pattern);
            textBox10.Text = (A - regex);
            textBox10.Visible = true;
            label10.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string pattern = textBox2.Text;
            string text = textBox4.Text;
            RegExp A = new RegExp(text, pattern);
            string str = textBox8.Text;
            A.AddText(ref str, textBox9);
            textBox9.Visible = true;
            label9.Visible = true;
            textBox11.Visible = true;
            label11.Visible = true;
            textBox12.Visible = true;
            label12.Visible = true;
            string d = (RegExp)A;
            textBox11.Text = d;
            A = (string)d;
            textBox12.Text = textBox11.Text;
        }
    }
    class RegExp
    {
        public RegExp(string txt, string pattern)
        {
            r = new Regex(pattern.ToLower());
            text = txt;
        }
        private Regex r;
        private string text;

        public bool Exist(TextBox textBox)
        {
            if (r.IsMatch(text.ToLower()))
            {
                textBox.Text = "Да";
                return r.IsMatch(text);
            }
            else
            {
                textBox.Text = "Нет";
                return false;
            }
        }

        public void ShowMatches(TextBox textBox)
        {
            MatchCollection m = r.Matches(text.ToLower());
            foreach (Match x in m)
                textBox.Text += $"{x.Value} ";
        }

        public string DeleteMatches(TextBox textBox)
        {
            MatchCollection m = r.Matches(text.ToLower());
            string s = text.ToLower();
            foreach (Match x in m)
            {
                int i = s.IndexOf(x.Value);
                int l = x.Value.Length;
                s = s.Remove(i, l);
                text = text.Remove(i, l);
            }
            textBox.Text += text;
            return s;
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


        public object CheckType(int n, TextBox textBox)
        {
            if (n == 0)
            {
                textBox.Text = R.ToString();
                return new Regex("");
            }
            else if (n == 1)
            {
                textBox.Text = text;
                return "string";
            }
            else textBox.Text = "ОШИБКА";
            return null;
        }

        public object this[int i, System.Windows.Forms.TextBox f]
        {

            set
            {

                var result = CheckType(i, f);
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
                var result = CheckType(i, f);
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
            string g = ttt;
            obj.text = ttt;
            return obj.text;
        }

        public string AddText(ref string str, TextBox textBox)
        {
            textBox.Text = text + str;
            return text;
        }

        public static implicit operator string(RegExp obj)
        {
            return obj.R.ToString();
        }

        public static implicit operator RegExp(string Text)
        {
            RegExp reg = new RegExp(Text, " ");
            return reg;
        }
    }
}
