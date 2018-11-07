using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UiImageToButtonImage : MonoBehaviour {
    [SerializeField]private Image img;

    private void Start()
    {
        if(img==null)
            img = GetComponent<Image>();
    }


    private void OnGUI()
    {
        if (img != null)
        {
            if (img.enabled && img.gameObject.activeSelf)
            {
                Rect R = TamanhoDoUI.RectSize(img.rectTransform);
                GUIStyle tempStyle = new GUIStyle(((GUISkin)Resources.Load("meuSkin")).box);
                Texture2D texturaSacana = img.sprite.texture;
                tempStyle.normal.background = texturaSacana;
                tempStyle.hover.background = texturaSacana;
                tempStyle.active.background = texturaSacana;
                GUI.Box(R, "", tempStyle);
            }
        }

    }
}

public class TamanhoDoUI
{
    public static Rect RectSize(RectTransform img)
    {
        Vector2 sizeDelta = img.sizeDelta;
        Canvas[] canvasX = MonoBehaviour. FindObjectsOfType<Canvas>();
        Canvas canvas = MonoBehaviour.FindObjectOfType<Canvas>();
        Transform xBase = img.transform.parent;
        while (xBase != null)
        {
            Canvas C = xBase.GetComponent<Canvas>();
            if (C != null)
                canvas = C;
            xBase = xBase.parent;
        }

        Vector3[] V = new Vector3[4];
        img.GetWorldCorners(V);

        return new Rect(V[1].x, 240 - V[1].y, sizeDelta.x * canvas.scaleFactor, sizeDelta.y * canvas.scaleFactor);
    }
}
