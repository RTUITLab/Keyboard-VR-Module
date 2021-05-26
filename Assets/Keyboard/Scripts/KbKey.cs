using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[ExecuteInEditMode]
public class KbKey : MonoBehaviour
{
    [Header("Параметры клавиши")]
    [Tooltip("Symbol означает стандартную клавишу. " +
        "Значение будет браться с подчиненного текстового поля.")]
    [SerializeField] private Keyboard keyboard;
    [SerializeField] private Keys key;
    private Text keyField;

    [HideInInspector] public UnityEvent EventClick;
    [HideInInspector] public UnityEvent EventInside;
    [HideInInspector] public UnityEvent EventOutside;

    private void Start()
    {
        keyField = GetComponentInChildren<Text>();
        keyboard = GetComponentInParent<Keyboard>();
        keyboard.kbKeys.Add(this);

        EventClick.AddListener(KeyClicked);
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (keyField)
            gameObject.name = "Key_" + keyField.text.ToUpper();

        // Обновляет коллайдеры кнопки для взаимодействия в VR в соответствии с шириной и высотой RectTransform.
        // Да, GetComponent'ы в апдейте, но кешировать эти ссылки на эти компоненты не стоит.
        GetComponent<BoxCollider>().size = new Vector2(GetComponent<RectTransform>().rect.width,
                                                       GetComponent<RectTransform>().rect.height);
    }
#endif

    public void KeyClicked()
    {
        switch (key)
        {
            case Keys.Symbol:
                char symbol = keyField.text[0];
                keyboard.AddChar(symbol);
                break;
            case Keys.Enter:
                keyboard.AddChar('\n');
                break;
            case Keys.Space:
                keyboard.AddChar(' ');
                break;
            case Keys.Shift:
                keyboard.ToggleShift();
                break;
            case Keys.Clear: // Стерка всего.
                keyboard.ClearAll();
                break;
            case Keys.Backspace: // Стерка одной буквы.
                keyboard.RemoveChar();
                break;
            case Keys.SwitchLayout:
                keyboard.ChangeLayout();
                break;
        }
    }

    public void UpdateKeyField(bool shiftPressed)
    {
        if (key == Keys.Symbol)
            if (shiftPressed)
                keyField.text = keyField.text.ToUpper();
            else
                keyField.text = keyField.text.ToLower();
    }
}

public enum Keys
{
    Symbol, Enter, Space, Shift,
    Clear, Backspace, SwitchLayout
}
