using System.Collections.Generic;

/// <summary>
/// Clase que guarda el Mapa que leemos de fichero.
/// </summary>
public class Board
{
    public int index { get; set; }
    public List<string> layout { get; set; }
    public List<List<int>> path { get; set; }
}
