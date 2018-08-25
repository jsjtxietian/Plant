using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Linq;
using UnityEngine;

public class WorldMapController : MonoBehaviour
{
    public Grid[,] WorldMap = new Grid[10, 10];
    public List<GameObject> Parts = new List<GameObject>();
    public List<GameObject> Exits = new List<GameObject>();
    public Material YellowMaterial;

    public GameObject[,] Cubes = new GameObject[10, 10];
    private Helper Helper;
    private GameController GameController;

    void Start()
    {
        Helper = gameObject.GetComponent<Helper>();
        GameController = gameObject.GetComponent<GameController>();
        ConfigMap();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            PrintWorldMap();
        }
    }

    public void ResetMap()
    {
        WorldMap = new Grid[10, 10];

        Parts.Destroy();
        Parts.Clear();

        Exits.Destroy();
        Exits.Clear();

        ConfigMap();
    }

    public void AddHandToMap(Coordinate pos, Hand hand)
    {
        WorldMap[pos.x, pos.y].hand = hand;
    }

    public bool CanPlaceHand(Coordinate pos, Hand hand)
    {
        bool canPlace = true;

        canPlace = canPlace && (WorldMap[pos.x, pos.y].hand == Hand.None);

        if (hand == Hand.Big || hand == Hand.Small)
        {
            canPlace = canPlace && (WorldMap[pos.x, pos.y].map != WorldMapElement.Flexible);
        }

        return canPlace;
    }

    public GameObject GetPartByPos(Coordinate pos)
    {
        foreach (var p in Parts)
        {
            if (p.GetComponent<PartController>().CurrentPos.Equal(pos))
                return p;
        }
        return null;
    }

    public void AfterPick(Coordinate pos)
    {
        WorldMap[pos.x, pos.y].component = ComponentType.None;
    }

    public void AfterPut(Coordinate pos, ComponentType type)
    {
        if (WorldMap[pos.x, pos.y].map == WorldMapElement.Exit)
        {
            WorldMap[pos.x, pos.y].component = ComponentType.None;
            GameObject tobeRemoved = GetPartByPos(pos);
            Parts.Remove(tobeRemoved);
            Destroy(tobeRemoved);
        }
        else
        {
            WorldMap[pos.x, pos.y].component = type;
        }
    }

    public void AfterMove(Coordinate origin, Coordinate current, Hand type)
    {
        WorldMap[origin.x, origin.y].hand = Hand.None;
        WorldMap[current.x, current.y].hand = type;
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
        int i = GameController.currentLevel;

        LevelConfig currentConfig = Config.LevelConfigs[i];

        //exit
        currentConfig.ExitPos.ForEach(x =>
        {
            WorldMap[x.x, x.y].map = WorldMapElement.Exit;

            GameObject Exit = (GameObject) Resources.Load("Prefabs/Exit");
            Exit = Instantiate(Exit);
            Exit.transform.position = Helper.GetExitPos(x.x, x.y);
            Exits.Add(Exit);
        });

        //parts
        currentConfig.NormalPos.ForEach(x =>
        {
            WorldMap[x.x, x.y].component = ComponentType.Normal;

            GameObject Part = (GameObject) Resources.Load("Prefabs/NormalPart");
            Part = Instantiate(Part);
            Part.transform.position = Helper.GetPartPos(x.x, x.y, Part.transform.position.y);
            Part.GetComponent<PartController>().SetInitPos(x.x, x.y);
            Part.GetComponent<PartController>().type = ComponentType.Normal;
            Parts.Add(Part);
        });

        currentConfig.FitPos.ForEach(x =>
        {
            WorldMap[x.x, x.y].component = ComponentType.Fit;

            GameObject Part = (GameObject) Resources.Load("Prefabs/FitPart");
            Part = Instantiate(Part);
            Part.transform.position = Helper.GetPartPos(x.x, x.y);
            Part.GetComponent<PartController>().SetInitPos(x.x, x.y);
            Part.GetComponent<PartController>().type = ComponentType.Fit;
            Parts.Add(Part);
        });

        currentConfig.HeavyPos.ForEach(x =>
        {
            WorldMap[x.x, x.y].component = ComponentType.Heavy;

            GameObject Part = (GameObject) Resources.Load("Prefabs/HeavyPart");
            Part = Instantiate(Part);
            Part.transform.position = Helper.GetPartPos(x.x, x.y);
            Part.GetComponent<PartController>().SetInitPos(x.x, x.y);
            Part.GetComponent<PartController>().type = ComponentType.Heavy;

            Parts.Add(Part);
        });

        currentConfig.ComplexPos.ForEach(x =>
        {
            WorldMap[x.x, x.y].component = ComponentType.Complex;

            GameObject Part = (GameObject) Resources.Load("Prefabs/ComplexPart");
            Part = Instantiate(Part);
            Part.transform.position = Helper.GetPartPos(x.x, x.y);
            Part.GetComponent<PartController>().SetInitPos(x.x, x.y);
            Part.GetComponent<PartController>().type = ComponentType.Complex;
            Parts.Add(Part);
        });

        currentConfig.FlexiblePos.ForEach(x =>
        {
            WorldMap[x.x, x.y].map = WorldMapElement.Flexible;
            //Cubes[x.x, x.y].GetComponent<MeshRenderer>().material.color = Color.yellow;
            Cubes[x.x, x.y].GetComponent<MeshRenderer>().material = YellowMaterial;
            //new Color(255, 237, 174);
        });
    }
}