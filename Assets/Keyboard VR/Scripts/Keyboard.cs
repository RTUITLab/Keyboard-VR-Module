using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{
    [HideInInspector] public List<KbKey> kbKeys;
    [Multiline] public string Text;
    private bool shiftPressed = true;

    [SerializeField] private GameObject[] layouts;
    private int currentLayout = 0;

    [SerializeField] private GameObject layoutParent;

    private void Start()
    {
        foreach (var layout in layouts) layout.SetActive(false);
        layouts[currentLayout].SetActive(true);

        StartCoroutine(UpdateKeysField());
    }

    public void Enable()
    {
        layoutParent.SetActive(true);
    }

    public void Disable()
    {
        layoutParent.SetActive(false);
    }

    #region Ввод

    public void AddChar(char input)
    {
        if (shiftPressed)
        {
            Text += char.ToUpper(input);

            // Шифт работает на одно нажатие, как на клавиатурах у мобилок.
            shiftPressed = false;
            StartCoroutine(UpdateKeysField());
        }
        else
        {
            Text += char.ToLower(input);
        }
    }

    public void RemoveChar()
    {
        Text = Text.Remove(Text.Length - 1);
    }

    public void ForceSetInput(string text)
    {
        Text = text;
    }
    public void ClearAll()
    {
        Text = "";
    }

    /// <summary>
    /// Срабатывает при нажатии на клавишу шифта.
    /// </summary>
    public void ToggleShift()
    {
        shiftPressed = !shiftPressed;
        StartCoroutine(UpdateKeysField());
    }

    /// <summary>
    /// Используется для обновления текста на клавише (заглавная или строчная буква).
    /// </summary>
    private IEnumerator UpdateKeysField()
    {
        yield return new WaitForEndOfFrame();
        foreach (var key in kbKeys)
        {
            if (key)
                key.UpdateKeyField(shiftPressed);
        }
    }

    #endregion

    #region Раскладки

    public void ChangeLayout()
    {
        layouts[currentLayout].SetActive(false);
        currentLayout++;
        currentLayout %= layouts.Length;
        layouts[currentLayout].SetActive(true);

        StartCoroutine(UpdateKeysField());
    }

    #endregion
}
