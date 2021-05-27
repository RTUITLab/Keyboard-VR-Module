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

    public void KeyClicked()
    {
        switch (key)
        {
            case Keys.Symbol: // Самая распространненая клавиша. Вводит символ из подчиненного текстового поля.
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
            case Keys.Clear:
                keyboard.ClearAll();
                break;
            case Keys.Backspace:
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

#if UNITY_EDITOR
    /// <summary>
    /// Сделано для упрощения создания новых раскладок, обновление происходит только в редакторе.
    /// </summary>
    private void Update()
    {
        if (Application.isEditor && !Application.isPlaying)
        {
            if (keyField)
            {
                // Делаем все клавиши заглавными для упрощения, но в плеймоде они могут становиться строчными.
                keyField.text = keyField.text.ToUpper();

                // Именуем объекты в иерархии по шаблону.
                gameObject.name = "Key_" + keyField.text; 

                if (key == Keys.Symbol && keyField.text.Length > 1)
                {
                    Debug.LogError($"Клавиша {gameObject.name} имеет функцию символа (key == Keys.Symbol), " +
                        $"\nдлина текста не может превышать 1 символ.");
                }
            }

            // Обновляем коллайдеры кнопок для взаимодействия в VR в соответствии с шириной и высотой RectTransform.
            var rect = GetComponent<RectTransform>().rect;
            GetComponent<BoxCollider>().size = new Vector2(rect.width, rect.height);
        }
    }
#endif
}

public enum Keys
{
    Symbol, Enter, Space, Shift,
    Clear, Backspace, SwitchLayout
}
