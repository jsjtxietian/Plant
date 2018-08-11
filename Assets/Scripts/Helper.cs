using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Helper {

    public static void PrintArray()
    {
        
    }

    public static void SetTransparent(Image image , float alpha)
    {
        image.color = new Color(image.color.r,image.color.g,image.color.b,alpha);
    }

}
