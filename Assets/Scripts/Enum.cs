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

public enum WorldMapElement
{
    None,
    Exit,
    Flexible
}

public struct Grid
{
    public WorldMapElement map;
    public ComponentType component;
    public Hand hand;
}

public enum ComponentType
{
    None,
    Complex,
    Fit,
    Normal,
    Heavy
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
