

# Keyboard-VR-Module

Модуль с VR-клавиатурой, который можно за несколько минут применить в любом проекте для SteamVR.

## Инструкция по установке

Проект в разработке.
<!---

[//]: # (TODO: Скриншоты по установке)

1. Устанавливаем SteamVR, если этого еще не сделали;
2. Скачиваем Unity Package из вкладки Releases;
3. Импортируем файл Unity Package;
4. Добавляем на обе руки игрока префаб KbKeyPointer;
5. Добавляем на сцену префаб клавиатуры;
6. Указываем ссылки на два KbKeyPointer компоненту на префабе клавиатуры.

-->

## Взаимодействие с клавиатурой

1. `keyboard.Text` — получение введенного с клавиатуры текста;
2. `Keyboard.Enable()` — включение клавиатуры вместе с лазерами для ввода;
3. `Keyboard.Disable()` — выключение клавиатуры вместе с лазерами для ввода;
4. `Keyboard.ClearAll()` — очистка введенного с клавиатуры текста;
5. `Keyboard.ForceSetInput("New text")` — замена введенного текста на указанный.

**В случае возникновения любых вопросов смотрим на ExampleScene.**

## Создание новых раскладок

Клавиатура имеет русскую и английскую раскладки, а также дополнительную со специальными символами (#$%^* и т.п.).

Если клавиша KbKey является клавишей Symbol, то вводимый символ будет браться с подчиненного текстового поля (принта на клавише). Клавиши Enter, Shift, Clear и др. обладают функционалом, описанным в методе KeyClicked.

После создания раскладки по аналогии с русской и английской, необходимо указать на нее ссылку компоненту на префабе клавиатуры.

[//]: # (TODO: Скриншот массива ссылок на раскладки)