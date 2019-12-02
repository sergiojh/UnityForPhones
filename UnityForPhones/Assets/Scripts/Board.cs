using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Path
{
    public int x { get; set; }
    public int y { get; set; }
}

public class Board
{
    public int index { get; set; }
    public List<string> layout { get; set; }
    public List<Path> path { get; set; }
}
