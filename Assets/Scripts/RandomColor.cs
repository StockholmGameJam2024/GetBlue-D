using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomColor : MonoBehaviour
{
    public SpriteRenderer[] playerColor;
    public Image[] playerTargetColor;
    public Color[] color;
    private int index;
    
    // Start is called before the first frame update
    void Start()
    {
        color = new Color[4];
        //playerColor = new SpriteRenderer[4];
        RandomColorGenerator();
        PlayerColorAssign();
        ColorAssignPlayerTarget();
    }

    public void RandomColorGenerator()
    {
        float hueRandom = Random.Range(0.0f, 0.26f);
        color[0] = Color.HSVToRGB(hueRandom, 1,1);
        color[1] = Color.HSVToRGB(hueRandom + 0.25f, 1,1);
        color[2] = Color.HSVToRGB(hueRandom + 0.50f, 1,1);
        color[3] = Color.HSVToRGB(hueRandom + 0.75f, 1,1);
    }

    public void PlayerColorAssign()
    {
        index = Random.Range(0, 4);
        for (var i = 0; i < playerColor.Length; i++)
        {
            
            playerColor[i].color = color[index];
            index++;
            if (index == 4)
            {
                index = 0;
            
            }
        }
    }

    public void ColorAssignPlayerTarget()
    {
        playerTargetColor[2].color = playerColor[0].color;
        playerTargetColor[3].color = playerColor[1].color; 
        playerTargetColor[0].color = playerColor[2].color; 
        playerTargetColor[1].color = playerColor[3].color;
    }

    public void ColorAdding()
    {
        
    }
}
