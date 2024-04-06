using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;


public class HeaderChromaWheel : MonoBehaviour
{
    private UIDocument uiDocument;
    private Label title;
    private float maxColorValue = 180f;
    
    private float r = 180f;
    private float g = 0f;
    private float b = 0f;
    public float step = 1f;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        title = uiDocument.rootVisualElement.Q<Label>("title");
        StartCoroutine(nameof(ColorWheelCoroutine));
    }

    public IEnumerator ColorWheelCoroutine()
    {
        while (true)
        {
            if (r < maxColorValue && g == 0 && b == 0)
            {
                r += step;
            }
            else if (r == maxColorValue && g < maxColorValue && b == 0)
            {
                g += step;
            }
            else if (r > 0 && g == maxColorValue && b == 0)
            {
                r -= step;
            }
            else if (r == 0 && g == maxColorValue && b < maxColorValue)
            {
                b += step;
            }
            else if (r == 0 && g > 0 && b == maxColorValue)
            {
                g -= step;
            }
            else if (r < maxColorValue && g == 0 && b == maxColorValue)
            {
                r += step;
            }
            else if (r == maxColorValue && g == 0 && b > 0)
            {
                b -= step;
            }

            title.style.color = new Color(r / 255, g / 255, b / 255);
            yield return new WaitForSeconds(0.01f);
        }
    }

}