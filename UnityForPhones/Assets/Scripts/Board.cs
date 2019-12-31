using System.Collections.Generic;

public class Board
{
    public int index { get; set; }
    public List<string> layout { get; set; }
    public List<List<int>> path { get; set; }
}
