using System;

namespace BJ
{
    public enum suit { spades = 6, hearts = 3, diamonds = 4, clubs = 5 };
    [Serializable]
    public class Card
    {
        private suit s;
        private int value;
        public Card(int v, suit s)
        {
            this.s = s;
            this.value = v;
        }

        public int GetValue() { return value; }

        public suit GetSuit() { return s; }

        public override string ToString()
        {
            string tmp = " ";
            switch (value)
            {
                case 10: { tmp += "0"; break; }
                case 11: { tmp += "J"; break; }
                case 12: { tmp += "Q"; break; }
                case 13: { tmp += "K"; break; }
                case 14: { tmp += "A"; break; }
                default: { tmp += value; break; }
            }
            tmp += Convert.ToChar(s);
            if (tmp[1] == '0')
                tmp = "10" + tmp[2];
            return tmp;
        }
    };
}
