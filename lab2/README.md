# Лабораторна робота №2
### Для побудови та запуску застосунку виконайте:
```
msbuild build.proj /p:LabName=lab2 /t:Run

```
### Для тестування застосунку:
```
msbuild build.proj /p:LabName=lab2 /t:Test

```
## Варіант 28

Задано цілий прямокутний масив M×N. Необхідно визначити прямокутну область масиву, сума елементів якого максимальна.

У першому рядку вхідного файлу INPUT.TXT записані два натуральні числа N і M (1 ≤ N, M ≤ 100) – кількість рядків і стовпців прямокутної матриці. Далі йдуть N рядків по M чисел, записаних через пропуск - елементи масиву, цілі числа, що не перевищують 100 по абсолютній величині.

У вихідний файл OUTPUT.TXT необхідно вивести цілу кількість – суму елементів знайденого прямокутного підмасиву. Підмасив має містити хоча б один елемент.



## Приклади

| №  | INPUT.TXT        | OUTPUT.TXT         |
|----|------------------|------------------- |
| 1  | <pre>2 3<br>5 0 9<br>1 2 7</pre> | 24 |
| 2  | <pre>4 5<br>-7  8 -1  0 -2<br> 2 -9  2  4 -6<br>-7  0  6  8  1<br> 4 -8 -1  0 -6</pre> | 20| 
