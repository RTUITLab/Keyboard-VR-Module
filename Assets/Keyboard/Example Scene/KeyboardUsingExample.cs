using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardUsingExample : MonoBehaviour
{
    [SerializeField] private Keyboard keyboard; // Cсылка на клавиатуру.
    [SerializeField] private InputField inputField; // Куда будем выводить текст.

    private void Update()
    {
        string text = keyboard.Text; // Получаем значение введенного текста.

        inputField.text = text; // В данном примере просто выводим его на экран.
    }

    private void Methods()
    {
        keyboard.ForceSetInput("Новый текст"); // Введенный текст заменится указанным.

        keyboard.ClearAll(); // Введенный текст очистится до пустого поля.
    }
}
