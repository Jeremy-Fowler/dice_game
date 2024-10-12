public class Player
{
  public Player(string name)
  {
    Name = name;
    DiceRolls = [];
  }
  public string Name { get; }
  public int Score { get; set; }
  public List<int> DiceRolls { get; }
  public int DiceRollsSum
  {
    get
    {
      return DiceRolls.Sum();
    }
  }
}