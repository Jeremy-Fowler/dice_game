internal class Program
{
  static List<Player> Players = [];
  static readonly int PointsToWin = 100;
  private static void Main()
  {
    Console.Clear();
    string logo = @"
          _____                    _____                    _____          
         /\    \                  /\    \                  /\    \         
        /::\    \                /::\    \                /::\    \        
       /::::\    \               \:::\    \              /::::\    \       
      /::::::\    \               \:::\    \            /::::::\    \      
     /:::/\:::\    \               \:::\    \          /:::/\:::\    \     
    /:::/__\:::\    \               \:::\    \        /:::/  \:::\    \    
   /::::\   \:::\    \              /::::\    \      /:::/    \:::\    \   
  /::::::\   \:::\    \    ____    /::::::\    \    /:::/    / \:::\    \  
 /:::/\:::\   \:::\____\  /\   \  /:::/\:::\    \  /:::/    /   \:::\ ___\ 
/:::/  \:::\   \:::|    |/::\   \/:::/  \:::\____\/:::/____/  ___\:::|    |
\::/    \:::\  /:::|____|\:::\  /:::/    \::/    /\:::\    \ /\  /:::|____|
 \/_____/\:::\/:::/    /  \:::\/:::/    / \/____/  \:::\    /::\ \::/    / 
          \::::::/    /    \::::::/    /            \:::\   \:::\ \/____/  
           \::::/    /      \::::/____/              \:::\   \:::\____\    
            \::/____/        \:::\    \               \:::\  /:::/    /    
             ~~               \:::\    \               \:::\/:::/    /     
                               \:::\    \               \::::::/    /      
                                \:::\____\               \::::/    /       
                                 \::/    /                \::/____/        
                                  \/____/                                  
                                                                           
";
    Console.WriteLine(logo);
    AddPlayer();
    Console.Clear();

    for (int i = 0; i < Players.Count; i++)
    {
      Player player = Players[i];
      Console.WriteLine($"Player {i + 1} {player.Name}");
      Console.WriteLine();
    }

    bool wonGame = false;
    int index = 0;

    while (!wonGame)
    {
      Player player = Players[index];
      Console.Clear();
      Console.WriteLine($"Turn for {player.Name}");
      Thread.Sleep(1000);
      wonGame = RollDice(player);
      player.DiceRolls.Clear();
      index++;
      if (index == Players.Count)
      {
        index = 0;
      }
    }

    Player? winner = Players.Find(player => player.Score >= PointsToWin);
    if (winner == null)
    {
      throw new Exception("What happen");
    }
    Console.WriteLine($"{winner.Name} wins!");

    Console.WriteLine("Would you like to play again?");
    ConsoleKeyInfo key = Console.ReadKey();
    if (key.Key == ConsoleKey.Y)
    {
      Players.Clear();
      Main();
    }
  }

  static void AddPlayer()
  {
    Console.WriteLine($"Please enter a name for player {Players.Count + 1}");
    string? name = Console.ReadLine();
    Console.WriteLine();
    if (name == null)
    {
      return;
    }
    Player newPlayer = new Player(name);
    Players.Add(newPlayer);

    if (Players.Count < 2)
    {
      AddPlayer();
      return;
    }

    Console.WriteLine("Add another player?");
    Console.WriteLine("y/n");
    ConsoleKeyInfo key = Console.ReadKey();
    Console.WriteLine();
    if (key.Key == ConsoleKey.Y)
    {
      AddPlayer();
      return;
    }
    return;
  }

  static bool RollDice(Player player)
  {
    Console.Clear();
    Console.WriteLine($"{player.Name} | Total score {player.Score}");
    Console.WriteLine();
    int randomNumber = new Random().Next(1, 7);

    player.DiceRolls.Add(randomNumber);
    if (randomNumber == 1)
    {
      Console.ForegroundColor = ConsoleColor.Red;
    }
    Console.WriteLine($"You rolled a {randomNumber}");
    Console.WriteLine("Rolls:");

    foreach (int diceRoll in player.DiceRolls)
    {
      Console.Write(diceRoll + " ");
    }
    Console.WriteLine();
    Console.WriteLine();

    if (randomNumber == 1)
    {
      Console.WriteLine("Too bad!");
      Thread.Sleep(3000);
      Console.ResetColor();
      return false;
    }

    if (player.Score + player.DiceRollsSum >= PointsToWin)
    {
      player.Score += player.DiceRollsSum;
      return true;
    }

    Console.WriteLine($"Turn score is {player.DiceRollsSum}");
    Console.WriteLine($"If you stop now, your total score will be {player.Score + player.DiceRollsSum}");
    Console.WriteLine($"You still need {PointsToWin - player.Score} points to win");

    Console.WriteLine("Would you like to roll again?");
    Console.WriteLine("y/n");
    ConsoleKeyInfo key = Console.ReadKey();

    if (key.Key == ConsoleKey.Y)
    {
      return RollDice(player);
    }

    player.Score += player.DiceRollsSum;
    return false;
  }
}