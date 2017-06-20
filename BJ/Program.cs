using System;

namespace BJ
{
	class Program
	{
		public static void Main(string[] args)
		{
            Game aGame = new BJ.Game();
            aGame.start();
			Console.Write("\n\nPress any key to exit . . . ");
			Console.ReadKey(true);
		}
	}
}
