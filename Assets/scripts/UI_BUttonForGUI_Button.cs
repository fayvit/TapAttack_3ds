using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_BUttonForGUI_Button : MonoBehaviour
{
    private Button B;
    private Text txtBtn;
    [SerializeField]private GUISkin skin;

    private void Start()
    {
        B = GetComponent<Button>();

        if (B == null)
            enabled = false;
        else
            txtBtn = B.GetComponentInChildren<Text>();

        if (skin == null)
            skin = (GUISkin)(Resources.Load("meuSkin"));
    }

    private void OnGUI()
    {
        if (B.interactable && B.gameObject.activeSelf&& B.enabled)
        {
            Rect R = TamanhoDoUI.RectSize(B.image.rectTransform);

            string txt = txtBtn != null ? txtBtn.text : "";
            if (GUI.Button(R, txt, skin.button))
            {
                B.onClick.Invoke();
            }
        }
    }
}