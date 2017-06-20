using System;

namespace BJ
{
	public class Table
	{
		public readonly string PCName = "Dealer";
		private Player[] players; //Массив игроков
		private int nPlayers; //Количество игроков
		private Deck aDeck; // Колода карт
		public Table(int nCards = 52)
		{
			nPlayers=2;
			players = new Player[this.nPlayers];
			players[0] = new Player("Player");
			players[1] = new Player(PCName);
			players[1].SetMoney(1000);
			aDeck = new Deck(nCards);
		}

		public Player GetPlayer(int number)
		{
			if (number > -1 && number < nPlayers) return players[number];
			else return null;
		}

		public Deck GetDeck()
		{
			return aDeck;
		}

		public int GetnPlayers()
		{
			return nPlayers;
		}
	};
}
