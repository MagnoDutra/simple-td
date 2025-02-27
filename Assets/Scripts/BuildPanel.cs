using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BuildPanel : MonoBehaviour
{
    public List<Image> towerButtons;

    [SerializeField]
    [Range(0, 1)]
    private float unselectedOpacity = 0.5f;

    [SerializeField]
    [Range(0, 1)]
    private float selectedOpacity = 1f;

    void Start()
    {
        towerButtons = transform.GetComponentsInChildren<Image>(false).Where(img => img.gameObject != gameObject).ToList();
    }

    void Update()
    {

    }

    public void ButtonSelectedOpacity(Image buttonClicked)
    {
        Color actualColor;

        foreach (Image img in towerButtons)
        {
            actualColor = img.color;
            img.color = new Color(actualColor.r, actualColor.g, actualColor.b, unselectedOpacity);
        }

        actualColor = buttonClicked.color;
        buttonClicked.color = new Color(actualColor.r, actualColor.g, actualColor.b, selectedOpacity);
    }
}
