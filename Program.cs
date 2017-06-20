using System;

namespace BJ
{
	class Program
	{
		public static void Main(string[] args)
		{
			Card c = new Card(14, suit.diamonds);
			Console.WriteLine(c);
			Deck d = new Deck();
			Console.WriteLine(d);
			ConsoleKeyInfo ch;
			do {
				ch = Console.ReadKey(false);
			} while(ch.KeyChar!='y');
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}
