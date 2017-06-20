using System;

namespace BJ
{
	/// <summary>
	/// Description of View.
	/// </summary>
	public class View
	{
		public struct Coord
		{
			public Coord(int X, int Y)
			{
				this.X = X;
				this.Y = Y;
			}
			public int X;
			public int Y;
		}
		//Координаты вывода сообщений
		static readonly Coord DeckXY = new Coord(1,6);
		static readonly Coord PlayerXY = new Coord(35,12);
		static readonly Coord DealerXY = new Coord(35,0);
		static readonly Coord WinnerXY = new Coord(30,10);
		static readonly Coord AskXY = new Coord(20,22);
		static readonly Coord BidXY = new Coord(70,10);
		static readonly int MINBID = 10;

		public static void AskBid(Player aPlayer)
		{
			Coord cur = AskXY;
			do {
				Console.SetCursorPosition(cur.X, cur.Y);
				Console.Write("Enter your bid (minimum 10$):          ");
				Console.SetCursorPosition(cur.X + 30, cur.Y);
				int bid = 0;
				try {
					bid = Convert.ToInt32(Console.ReadLine());
					if (bid <= aPlayer.GetMoney() && bid >= 10) {
						aPlayer.SetBid(bid);
						//Затираем запрос и возможное сообщение об ошибке
						Console.SetCursorPosition(cur.X, cur.Y);
						Console.Write("                                        ");
						Console.SetCursorPosition(cur.X, cur.Y + 1);
						Console.Write("                                        ");
						break;
					}
					if (bid >= aPlayer.GetMoney()) {
						Console.SetCursorPosition(cur.X, cur.Y + 1);
						Console.Write("ERROR: You do not have enough money!");
					} else if (bid < 10) {
						Console.SetCursorPosition(cur.X, cur.Y + 1);
						Console.Write("ERROR: The minimum bet is 10$");
					}
				} catch (FormatException) {
					Console.SetCursorPosition(cur.X, cur.Y + 1);
					Console.Write("ERROR: Please input an integer.");
				}
			} while (true);
		}
		//Показ ставки игрока
		public static void showBid(Player aPlayer)
		{
			Coord cur = BidXY;
			Console.SetCursorPosition(cur.X, cur.Y);
			Console.Write("BID: " + aPlayer.GetBid() + "$  ");
		}

		//Отрисовка карт
		public static void showCard(Coord cur, Card c = null)
		{
			Console.SetCursorPosition(cur.X, cur.Y);
			if (c == null) {
				//Карта не задана - печать рубашки
				//Console.Write((char)218 + (char)196 + (char)196 + (char)196 + (char)196 + (char)196 + (char)196 + (char)191 + ' ');
				Console.Write("┌──────┐ ");
				Console.SetCursorPosition(cur.X, cur.Y + 1);
				//Console.Write((char)179 + (char)177 + (char)176 + (char)177 + (char)177 + (char)176 + (char)177 + (char)179 + ' ');
				Console.Write("│▒░▒▒░▒│ ");
				Console.SetCursorPosition(cur.X, cur.Y + 2);
				//Console.Write((char)179 + (char)176 + (char)177 + (char)176 + (char)176 + (char)177 + (char)176 + (char)179 + ' ');
				Console.Write("│░▒░░▒░│ ");
				Console.SetCursorPosition(cur.X, cur.Y + 3);
				//Console.Write((char)179 + (char)177 + (char)176 + (char)177 + (char)177 + (char)176 + (char)177 + (char)179 + ' ');
				Console.Write("│▒░▒▒░▒│ ");
				Console.SetCursorPosition(cur.X, cur.Y + 4);
				//Console.Write((char)179 + (char)176 + (char)177 + (char)176 + (char)176 + (char)177 + (char)176 + (char)179 + ' ');
				Console.Write("│░▒░░▒░│ ");
				Console.SetCursorPosition(cur.X, cur.Y + 5);
				//Console.Write((char)179 + (char)177 + (char)176 + (char)177 + (char)177 + (char)176 + (char)177 + (char)179 + ' ');
				Console.Write("│▒░▒▒░▒│ ");
				Console.SetCursorPosition(cur.X, cur.Y + 6);
				//Console.Write((char)192 + (char)196 + (char)196 + (char)196 + (char)196 + (char)196 + (char)196 + (char)217 + ' ');
				Console.Write("└──────┘ ");
				Console.SetCursorPosition(cur.X, cur.Y + 7);
				Console.Write("         ");
			} else {
				//Карта задана - печать карты
				//cout << (char)201 << (char)205 << (char)205 << (char)205 << (char)205 << (char)205 << (char)205 << (char)187;
				Console.Write("╔══════╗");
				Console.SetCursorPosition(cur.X, cur.Y + 1);
				Console.Write("║" + c.ToString() + "   ║");
				Console.SetCursorPosition(cur.X, cur.Y + 2);
				for (int n = 0; n < 4; n++) {
					Console.Write("║      ║");
					Console.SetCursorPosition(cur.X, cur.Y + 3 + n);
				}
				//cout << (char)200 << (char)205 << (char)205 << (char)205 << (char)205 << (char)205 << (char)205 << (char)188;
				Console.Write("╚══════╝");
			}
		}
		//Показ колоды
		public static void showDeck(Deck aDeck)
		{
			Coord cur = DeckXY;
			Console.SetCursorPosition(cur.X, cur.Y);
			Console.Write("Deck");
			if (aDeck.GetCountCards() > 0) {
				cur.X = 1;
				cur.Y++;
				View.showCard(cur);
				//Если в колоде больше 1 карты - показ стопки карт
				if (aDeck.GetCountCards() > 1) {
					cur.X++;
					cur.Y++;
					View.showCard(cur);
				}
			} else {
				Console.SetCursorPosition(cur.X - 5, cur.Y);
				for (int i = 0; i < 6; i++)
					Console.WriteLine("       ");
			}
		}
		//Показ игрока
		public static void showPlayer(Player p)
		{
			Coord cur = PlayerXY;
			Console.SetCursorPosition(cur.X, cur.Y);  //Position of player name
			Console.Write(p.GetName());
			int firstX = cur.X;
			cur.X -= 5;
			cur.Y++;
			// показ всех карт игрока со сдвигом на 4 символа
			for (int i = 0; i < p.GetNCards(); i++) {
				View.showCard(cur, p.GetHand()[i]);
				cur.X += 4;
			}
			cur.X = firstX;
			cur.Y += 7;
			Console.SetCursorPosition(cur.X, cur.Y);
			if (p.GetPoints() > 0)
				Console.Write(p.GetPoints() + "     "); //показ очков если они есть
			Console.SetCursorPosition(cur.X + 21, cur.Y);
			Console.Write("Money: " + p.GetMoney() + "$  ");
		}
		//Показ крупье
		public static void showDealer(Player p, bool open = false)
		{
			Coord cur = DealerXY;
			Console.SetCursorPosition(cur.X, cur.Y);
			Console.Write(p.GetName() + "\t\tMoney: " + p.GetMoney() + "$  ");
			int FirstX = cur.X;
			cur.X -= 5;
			cur.Y++;
			if (p.GetNCards() > 0)
				View.showCard(cur, p.GetHand()[0]);
			cur.X += 4;
			for (int i = 1; i < p.GetNCards(); i++) {
				View.showCard(cur, open ? p.GetHand()[i] : null);
				cur.X += 4;
			}
			if (open) {
				cur.X = FirstX;
				cur.Y += 7;
				Console.SetCursorPosition(cur.X, cur.Y);
				Console.Write(p.GetPoints() + "     ");
			}
		}
		//Показ стола
		public static void showTable(Table aTable)
		{
			Console.Clear();
			showDeck(aTable.GetDeck());
			showDealer(aTable.GetPlayer(1));
			showPlayer(aTable.GetPlayer(0));
			showBid(aTable.GetPlayer(0));
		}
		//Объявление победителя
		public static void showWinner(Player aPlayer)
		{
			Coord cur = WinnerXY;
			Console.SetCursorPosition(cur.X, cur.Y);
			if (aPlayer != null) {
				Console.Write(aPlayer.GetName() + " win!");
				showBid(aPlayer);
			} else {
				Console.Write("          DRAW!!!    ");
			}
		}
		//Обработчик ответа да/нет
		public static bool Select()
		{
			do {
				var ch = Console.ReadKey(true);
				if (ch.Key == ConsoleKey.Y) {
					Console.Write("y");
					return true;
				}
				if (ch.Key == ConsoleKey.N) {
					Console.Write("n");
					return false;
				}
				if(ch.Modifiers == ConsoleModifiers.Alt)
				{
					switch(ch.Key)
					{
						case ConsoleKey.F1:
						case ConsoleKey.F2:
						case ConsoleKey.F3:
						case ConsoleKey.F4:
						case ConsoleKey.F5:
						case ConsoleKey.F6:
						case ConsoleKey.F7:
						case ConsoleKey.F8:
						case ConsoleKey.F9:
						case ConsoleKey.F10:
							{
								throw new ApplicationException(ch.KeyChar.ToString());
							}
					}
				}
			} while (true);
		}
		
	//Запрос взять карту
	public static bool TakeCard()
	{
		Coord cur = AskXY;
		Console.SetCursorPosition(cur.X, cur.Y);
		Console.Write("Do you want to take another card? (y/n) ");
		bool res = Select();
		Console.SetCursorPosition(cur.X, cur.Y);
		Console.Write("                                             ");
		return res;
	}
		//Запрос продолжить игру
	public static bool NextPlay()
	{
		Coord cur = AskXY;
		Console.SetCursorPosition(cur.X, cur.Y);
		Console.Write("You want to play more? (y/n) ");
		bool res = Select();
		Console.SetCursorPosition(cur.X, cur.Y);
		Console.Write("                                             ");
		if(!res) View.GameOver(2);
		return res;
	}

	//Объявление о конце игры
	public static void GameOver(int isWin)
	{
		Coord cur = WinnerXY;
		Console.SetCursorPosition(cur.X, cur.Y);
		switch (isWin)
		{
		case 0:
		{
			cout << "You lose all the money.";
			gotoxy(cur.X-4, cur.Y + 1);
			cout << "Home will have to travel on foot.";
			break;
		}
		case 1:
		{
            gotoxy(cur.X-8, cur.Y);
			cout << "Congratulations you have cleaned the casino!";
			gotoxy(cur.X-19, cur.Y + 1);
			cout << "At the output you expect responsible people, they will guide you ...";
			break;
		}
		case 2:
		{
			cout << "You take your money and go to rest...";
			gotoxy(cur.X, cur.Y + 1);
			cout << "Until we meet again!";
			break;
		}
		}
		gotoxy(0, 21);
	}
	}
}
