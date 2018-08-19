using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WorldMapController : MonoBehaviour {

    public Grid[,] WorldMap = new Grid[10,10];

	void Start () {
	    ConfigMap();
	}
	
	void Update () {

	    if (Input.GetKeyDown(KeyCode.M))
	    {
	        PrintWorldMap();
	    }
	}

    private void PrintWorldMap()
    {
        StreamWriter s = new StreamWriter("WorldMap.txt");
        for (int j = 9; j >= 0; j--)
        {
            for (int i = 0; i < 10; i++)
            {
                s.Write(WorldMap[i, j].map.ToString() + ' ' + WorldMap[i, j].component.ToString() + ' '
                    + WorldMap[i, j].hand.ToString() + '\t');
            }
            s.WriteLine(' ');
        }

        s.Dispose();
        s.Close();
    }

    private void ConfigMap()
    {
        //todo read from playerprefabs

        int i = 0;

        LevelConfig currentConfig = Config.LevelConfigs[i];

        currentConfig.ExitPos.ForEach(x =>
        {
            WorldMap[x.x, x.y].map = WorldMapElement.Exit;
        });

        currentConfig.NormalPos.ForEach(x =>
        {
            WorldMap[x.x, x.y].component = ComponentType.Normal;
        });

        currentConfig.FitPos.ForEach(x =>
        {
            WorldMap[x.x, x.y].component = ComponentType.Fit;
        });

        currentConfig.HeavyPos.ForEach(x =>
        {
            WorldMap[x.x, x.y].component = ComponentType.Heavy;
        });

        currentConfig.HeavyPos.ForEach(x =>
        {
            WorldMap[x.x, x.y].component = ComponentType.Complex;
        });

        currentConfig.FlexiblePos.ForEach(x =>
        {
            WorldMap[x.x, x.y].map = WorldMapElement.Flexible;
        });
        
    }
}
