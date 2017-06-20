using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BJ
{
    public class Game
    {
        private Table aTable;
        private readonly int MINBID = 10;
        private int currentTable = 0;
        MemoryStream[] Tables;
        public void start()
        {
            aTable = new Table();
            Tables = new MemoryStream[10];
            bool nextPlay = true;
            do
            {
                try
                {
                    do
                    {
                        aTable.GetPlayer(0).Reset();
                        aTable.GetPlayer(1).Reset();
                        aTable.GetDeck().Reset();
                        View.showTable(aTable,currentTable+1);
                        View.AskBid(aTable.GetPlayer(0));
                        FirstTurn(); //сдать первые карты
                        View.showTable(aTable,currentTable+1);
                        NextTurn(); //пока игроки берут карты
                        View.showWinner(Winner()); //Объявляем победителя и обрабатываем ставки
                        if (aTable.GetPlayer(0).GetMoney() < MINBID)
                        {
                            View.GameOver(0);
                            //Игра окончена обновляем стол
                            aTable = new Table();
                            break;
                        }
                        if (aTable.GetPlayer(1).GetMoney() <= 0)
                        {
                            View.GameOver(1);
                            //Игра окончена обновляем стол
                            aTable = new Table();
                            break;
                        }
                    } while (nextPlay = View.NextPlay());
                }
                catch (FException e)
                {
                    Console.WriteLine(e.Key);
                    int num = Convert.ToInt32(e.Key.ToString().Trim(new char[] { 'F' })) - 1;
                    if (num != currentTable)
                    {
                        Tables[currentTable] = new MemoryStream();
                        new BinaryFormatter().Serialize(Tables[currentTable], aTable);
                        if (Tables[num] != null)
                        {
                            Tables[num].Seek(0, SeekOrigin.Begin);
                            aTable = (Table)new BinaryFormatter().Deserialize(Tables[num]);
                        }
                        else
                        {
                            aTable = new Table();
                        }
                        currentTable = num;
                    }
                }
            } while (nextPlay);
        }

        //Первая раздача по 2 карты
        void FirstTurn()
        {
            for (int i = 0; i < 2; i++)
            {
                for (int n = 0; n < aTable.GetnPlayers(); n++)
                    aTable.GetPlayer(n).DrawCard(aTable.GetDeck());
            }
            for (int n = 0; n < aTable.GetnPlayers(); n++)
                CalculatePoints(aTable.GetPlayer(n));
        }

        //Набор карт
        void NextTurn()
        {
            //Игрок берет карты и не перебор
            while (aTable.GetPlayer(0).GetPoints() < 21 && View.TakeCard())
            {
                aTable.GetPlayer(0).DrawCard(aTable.GetDeck());  //тянет карту
                CalculatePoints(aTable.GetPlayer(0));  //считаем очки
                View.showPlayer(aTable.GetPlayer(0)); //Обновляем игрока
                View.showDeck(aTable.GetDeck());      //Обновляем колоду
            }
            //Крупье берет карты. берёт ещё если меньше чем у игрока и у игрока не перебор
            while (aTable.GetPlayer(1).GetPoints() < aTable.GetPlayer(0).GetPoints() && aTable.GetPlayer(0).GetPoints() < 22)
            {
                aTable.GetPlayer(1).DrawCard(aTable.GetDeck());
                CalculatePoints(aTable.GetPlayer(1));
                View.showDeck(aTable.GetDeck());
                View.showDealer(aTable.GetPlayer(1));
            }
            View.showDealer(aTable.GetPlayer(1), true); //Крупье открывает карты
        }

        //Подсчёт очков и запись в поле игрока
        void CalculatePoints(Player aPlayer)
        {
            int res = 0;
            int countAce = 0;
            for (int i = 0; i < aPlayer.GetNCards(); i++)
            {
                switch (aPlayer.GetHand()[i].GetValue())
                {
                    case 14:
                        {
                            if (res + 11 < 22)
                            {
                                countAce++;
                                res += 11;
                            }
                            else
                                res += 1;
                            break;
                        }
                    case 11:
                    case 12:
                    case 13:
                        {
                            res += 10;
                            break;
                        }
                    default:
                        {
                            res += aPlayer.GetHand()[i].GetValue();
                            break;
                        }
                }
                if (res > 21 && countAce > 0)
                {
                    res -= 10;
                    countAce--;
                }
            }
            aPlayer.SetPoints(res);
        }

        //Определение победителя и обработка ставок
        public Player Winner()
        {
            //Одинаково очков или у обоих перебор - ничья
            if (aTable.GetPlayer(0).GetPoints() == aTable.GetPlayer(1).GetPoints()
                || (aTable.GetPlayer(0).GetPoints() > 21 && aTable.GetPlayer(1).GetPoints() > 21))
            {
                aTable.GetPlayer(0).SetMoney(aTable.GetPlayer(0).GetMoney() + aTable.GetPlayer(0).GetBid());
                aTable.GetPlayer(0).SetBid(0);
                return null;
            }
            //Игрок победил если:
            bool isPlayerWin = ((aTable.GetPlayer(1).GetPoints() > 21)  //у крупье перебор
                || ((aTable.GetPlayer(0).GetPoints() < 22)              //или (у игрока не перебор и больше чем у крупье)
                    && ((aTable.GetPlayer(0).GetPoints()) > (aTable.GetPlayer(1).GetPoints()))));
            if (isPlayerWin)
            {
                //снимаем с банка деньги равные ставке игрока
                aTable.GetPlayer(1).SetMoney(aTable.GetPlayer(1).GetMoney() - aTable.GetPlayer(0).GetBid());
                //заносим на счёт игрока выигрыш = 2 ставки
                aTable.GetPlayer(0).SetMoney(aTable.GetPlayer(0).GetMoney() + aTable.GetPlayer(0).GetBid() * 2);
                //обнуляем текущую ставку
                aTable.GetPlayer(0).SetBid(0);
            }
            else
            {
                //заносим на счёт крупье ставку
                aTable.GetPlayer(1).SetMoney(aTable.GetPlayer(1).GetMoney() + aTable.GetPlayer(0).GetBid());
                //обнуляем текущую ставку
                aTable.GetPlayer(0).SetBid(0);
            }
            View.showPlayer(aTable.GetPlayer(0));
            View.showDealer(aTable.GetPlayer(1), true);
            return aTable.GetPlayer(isPlayerWin ? 0 : 1);
        }
    };
}
