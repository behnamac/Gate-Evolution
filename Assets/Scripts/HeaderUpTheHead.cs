using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeaderUpTheHead : MonoBehaviour
{
    public static HeaderUpTheHead Instance;
    public Image hotbarFill;
    public Image hotBar;
    public Text clothTypeText;

    private void Awake()
    {
        Instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 namePos = Camera.main.WorldToScreenPoint(this.transform.position);
        hotbarFill.transform.position = namePos;
    }

    public void UpdateHotBar(float value)
    {
        hotBar.fillAmount = value;
    }

    public void ChangeClothText(string clothText)
    {
        clothTypeText.text = clothText.ToUpper();
    }
}
