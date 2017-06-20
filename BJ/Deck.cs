using System;

namespace BJ
{
    [Serializable]
    public class Deck
    {
        private Card[] cards = null; //Массив карт в колоде
        private int nCurrentCard; //Количество выданных карт
        private int N; //Общее число карт в колоде
        private void shuffle()
        {
            Random rand = new Random();
            //Мешаем случайное количество раз, но не меньше 5
            int count = rand.Next(5, 100);
            Card c = null;
            for (int i = 0; i < count; i++)
                for (int x = 0; x < cards.Length; x++)
                {
                    int r = rand.Next(cards.Length);
                    c = cards[x];
                    cards[x] = cards[r];
                    cards[r] = c;
                }
        }
        public Deck(int N = 52)
        {
            //Количество карт не больше 52 и кратное 4
            this.N = (N <= 52 && (N % 4 == 0)) ? N : 52;
            nCurrentCard = 0;
            cards = new Card[this.N];
            int MinCard = 2; //Минимальный номинал карты
            if (this.N == 36)
                MinCard = 6;
            int MaxCard = this.N / 4 + MinCard; //Максимальный номинал карты
            int index = 0;
            int mod = this.N / 4;
            for (int i = MinCard; i < MaxCard; i++)
            {
                cards[index] = new Card(i, suit.spades);
                cards[index + mod] = new Card(i, suit.hearts);
                cards[index + 2 * mod] = new Card(i, suit.diamonds);
                cards[index++ + 3 * mod] = new Card(i, suit.clubs);
            }
            shuffle();
        }

        //Количество карт в колоде
        public int GetCountCards()
        { return this.N - this.nCurrentCard; }

        //Выдаёт одну карту
        public Card GetCard()
        {
            if (this.nCurrentCard < this.N)
            {
                this.nCurrentCard++;
                return cards[this.nCurrentCard - 1];
            }
            else return null;
        }

        public void Reset()
        {
            this.nCurrentCard = 0;
            shuffle();
        }

        public override string ToString()
        {
            var res = new System.Text.StringBuilder();
            foreach (Card c in cards)
            {
                res.Append(c.ToString());
            }
            return res.ToString();
        }
    };
}
