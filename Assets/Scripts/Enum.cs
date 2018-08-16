using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum Command
{
    None,
    Active,
    Up,
    Down,
    Left,
    Right,
    Pick,
    Put,
    Pause
}

public enum Hand
{
    None,
    Big,
    Small,
    Human
}

public struct Coordinate
{
    public int x;
    public int y;

    public Coordinate(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
}