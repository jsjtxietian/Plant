﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Helper : MonoBehaviour
{
    #region SpriteDictionary

    public GameObject NormalSprite;
    public static Dictionary<Command, Sprite> CommandSpriteDictionary = new Dictionary<Command, Sprite>();
    public static Dictionary<Hand, Sprite> HandSpriteDictionary = new Dictionary<Hand, Sprite>();

    public Sprite GetChildSprite(int i)
    {
        return NormalSprite.transform.GetChild(i).gameObject.GetComponent<Image>().sprite;
    }

    #endregion

    void Start()
    {
        CommandSpriteDictionary[Command.Up] = GetChildSprite(0);
        CommandSpriteDictionary[Command.Down] = GetChildSprite(1);
        CommandSpriteDictionary[Command.Left] = GetChildSprite(2);
        CommandSpriteDictionary[Command.Right] = GetChildSprite(3);
        CommandSpriteDictionary[Command.Pause] = GetChildSprite(4);
        CommandSpriteDictionary[Command.Put] = GetChildSprite(5);
        CommandSpriteDictionary[Command.Pick] = GetChildSprite(6);

        HandSpriteDictionary[Hand.Big] = GetChildSprite(7);
        HandSpriteDictionary[Hand.Small] = GetChildSprite(8);
        HandSpriteDictionary[Hand.Human] = GetChildSprite(9);
    }

    public static void SetTransparent(Image image, float alpha)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
    }

    public static Command GetCommandTypeFromString(string spriteName)
    {
        if (spriteName.Contains("forward"))
            return Command.Up;
        if (spriteName.Contains("left"))
            return Command.Left;
        if (spriteName.Contains("right"))
            return Command.Right;
        if (spriteName.Contains("back"))
            return Command.Down;
        if (spriteName.Contains("pause"))
            return Command.Pause;
        if (spriteName.Contains("put"))
            return Command.Put;
        if (spriteName.Contains("pick"))
            return Command.Pick;
        return Command.None;
    }

    public static int GetChildOrder(GameObject child)
    {
        Transform father = child.transform.parent;
        for (int i = 0; i < father.childCount; i++)
        {
            if (father.GetChild(i).gameObject.Equals(child))
            {
                return i;
            }
        }

        return -1;
    }

    public static Hand GetHandTypeFromString(string spritename)
    {
        if (spritename.Contains("small"))
            return Hand.Small;
        else if (spritename.Contains("big"))
            return Hand.Big;
        else if (spritename.Contains("human"))
            return Hand.Human;
        else return Hand.None;
    }
}