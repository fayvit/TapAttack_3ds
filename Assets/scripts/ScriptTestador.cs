using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScriptTestador : MonoBehaviour
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
            Vector2 sizeDelta = B.image.rectTransform.sizeDelta;
            Canvas canvas = FindObjectOfType<Canvas>();

            Vector3[] V = new Vector3[4];
            B.image.rectTransform.GetWorldCorners(V);

            Rect R = new Rect(V[1].x, 240 - V[1].y, sizeDelta.x * canvas.scaleFactor, sizeDelta.y * canvas.scaleFactor);

            string txt = txtBtn != null ? txtBtn.text : "";
            if (GUI.Button(R, txt, skin.button))
            {
                B.onClick.Invoke();
            }
        }
    }
}