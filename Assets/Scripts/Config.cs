using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config
{
    public static List<LevelConfig> LevelConfigs;
    public static float RoundTime;

    static Config()
    {
        LevelConfigs = new List<LevelConfig>();
        RoundTime = 2.0f;

        //teaching level
        LevelConfig Lv0 = new LevelConfig(0);
        Lv0.ExitPos.Add(new Coordinate(5,6));
        Lv0.NormalPos.Add(new Coordinate(3,3));
        LevelConfigs.Add(Lv0);

        //Level 1
        LevelConfig Lv1 = new LevelConfig(1);
        Lv1.ExitPos.Add(new Coordinate(1,6));
        Lv1.FitPos.Add(new Coordinate(4, 3));
        Lv1.FitPos.Add(new Coordinate(4, 6));
        LevelConfigs.Add(Lv1);

        //Level2 
        LevelConfig Lv2 = new LevelConfig(2);
        Lv2.ExitPos.Add(new Coordinate(4,4));
        Lv2.FlexiblePos.Add(new Coordinate(4,3));
        Lv2.FlexiblePos.Add(new Coordinate(4,5));
        Lv2.NormalPos.Add(new Coordinate(3,4));
        Lv2.ComplexPos.Add(new Coordinate(6,2));
        LevelConfigs.Add(Lv2);

        //Level3
        LevelConfig Lv3 = new LevelConfig(3);
        Lv3.ExitPos.Add(new Coordinate(1, 8));
        Lv3.ExitPos.Add(new Coordinate(8, 1));
        Lv3.FlexiblePos.Add(new Coordinate(8, 0));
        Lv3.FlexiblePos.Add(new Coordinate(8, 2));
        Lv3.FlexiblePos.Add(new Coordinate(8, 3));
        Lv3.NormalPos.Add(new Coordinate(7, 0));
        Lv3.HeavyPos.Add(new Coordinate(7, 2));
        Lv3.ComplexPos.Add(new Coordinate(3, 6));
        LevelConfigs.Add(Lv3);
    }
}

public struct LevelConfig
{
    public int currentLevel;

    public List<Coordinate> ExitPos;
    public List<Coordinate> NormalPos;
    public List<Coordinate> FitPos;
    public List<Coordinate> HeavyPos;
    public List<Coordinate> ComplexPos;
    public List<Coordinate> FlexiblePos;

    public LevelConfig(int level)
    {
        currentLevel = level;

        ExitPos = new List<Coordinate>();
        NormalPos = new List<Coordinate>();
        FitPos = new List<Coordinate>();
        HeavyPos = new  List<Coordinate>();
        ComplexPos = new List<Coordinate>();
        FlexiblePos = new List<Coordinate>();
    }
}