using System;

namespace BJ
{
    [Serializable]
    public class Player
    {
        private string name;
        public Card[] hand;
        private int points; //очки
        private int nCards; //всего карт в руках
        private bool TakeCard; //признак брал ли игрок карту
        private int money; //деньги
        private int bid; //текущая ставка
        public Player(string name)
        {
            points = 0;
            nCards = 0;
            TakeCard = true;
            money = 100;
            bid = 0;
            this.name = name;
            hand = new Card[20];
        }

        public void DrawCard(Deck aDeck)
        {
            hand[nCards] = aDeck.GetCard();
            if (hand[nCards] != null)
                nCards++;
        }

        public Card[] GetHand()
        {
            if (nCards > 0) return hand;
            else return null;
        }

        public void Reset()
        {
            hand = new Card[20];
            nCards = 0;
            points = 0;
            TakeCard = true;
        }

        public int GetNCards()
        { return nCards; }

        public bool isTakeCard()
        { return TakeCard; }

        public void SetTakeCard(bool TakeCard)
        { this.TakeCard = TakeCard; }

        public void SetMoney(int money)
        {
            if (money > 0)
                this.money = money;
            else
                this.money = 0;
        }

        public int GetMoney()
        { return this.money; }

        public bool SetBid(int bid)
        {
            if (bid <= this.money)
            {
                money -= bid;
                this.bid = bid;
                return true;
            }
            else
                return false;
        }

        public int GetBid()
        { return this.bid; }

        public void SetPoints(int points)
        { this.points = points; }

        public int GetPoints()
        { return this.points; }

        public string GetName()
        {
            return this.name;
        }

        public override string ToString()
        {
            var res = new System.Text.StringBuilder();
            res.Append(this.name);
            res.Append(": ");
            for (int i = 0; i < nCards; i++)
            {
                res.Append(hand[i].ToString());
            }
            return res.ToString();
        }
    };
}
