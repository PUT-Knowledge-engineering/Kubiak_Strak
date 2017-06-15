# Kubiak_Strak
Recognition of blood vessels in angiographic images of the eye
Rozpoznawanie naczyń krwionośnych na zdjęciach angiograficznych oka zostało zaimplementowane w dwóch językach, C# z wykorzystaniem biblioteki EmguCV oraz C++ z wykorzystaniem biblioteki OpenCV. Projekty są do siebie analogiczne i wykorzystują ten sam algorytm wykrywania naczyń krwionośnych.
W projekcie znajduje się 11 przykładowych zdjęć oraz wyników przeprowadzonej analizy rozpoznawania naczyń krwionośnych.

## Rozpozanawnie naczyń krwionośnych na zdjęciach angiograficznych oka
Projekt pozwala na identyfikację naczyń krwionośnych oka na podstawie zdjęć angiograficznych oka. Użytkownik ma możliwość zmiany parametru progowania pomiędzy wartościami 190 a 230 w zależności od jasności podawanego obrazu

Proces rozpoznawania użyty w projekcie:
1. Konwersja obrazu na skalę szarości
2. Filtr Sobela dla wartości pionowych
3. Filtr Sobela dla wartości poziomych 
4. Sumowanie obrazów wynikowych
5. Wyrównanie histogramu
6. Progowanie obrazu dla zadanej wartości
7. Wyszukiwanie konturów dla otrzymanego obrazka
8. Zapis wartości wektora wektorów punktów do pliku wyjściowego oraz wyrysowanie obrazu wyjściowego 



### Założenia prawidłowej pracy programu
Aby program prawidłowo rozpoznawał naczynia krwionośne należy wybrać obraz z fazy badania w przypadku, kiedy kontrast wypełnił żyły i tętnice, czyli najjaśniejszy etap wszystkich faz badania angiograficznego. W przypadku wyboru innego zdjęcia, np. fazy przed rozświetleniem algorytm może nie poradzić sobie z prawidłowym ustawieniem konturów naczyń krwionośnych. Każde wykrycie wiąże się z utworzeniem piku *.output* w którym zapisany jest wektor wektora punktów.

Głównym problemem w przypadku tego rozpoznawania są szumy generowane przy tarczy nerwu wzrokowego, najjaśniejszego punktu całego zdjęcia.

## Przykład działania programu
Poniżej przedstawiamy zdjęcia angiograficzne, każde z nich pochodzi z innej sekwencji zdjęć. Przy wyborze kierowaliśmy się kryteriami określonymi wczesniej co do wyboru zdjęć. Faza badania - rozświetlone kontrastem tętnice i żyły.

### 1: Zdjęcie wejściowe:
![1](https://user-images.githubusercontent.com/6911021/27201043-713b8dbe-521c-11e7-9997-6da3c4f1f994.jpg)
Wynik:
![1_ven](https://user-images.githubusercontent.com/6911021/27201234-3e4e8608-521d-11e7-8397-3306de2f92a9.JPG)

### 2: Zdjęcie wejściowe:
![2](https://user-images.githubusercontent.com/6911021/27201249-4a3fedf8-521d-11e7-8302-d8a049010a5f.jpg)
Wynik:
![2_ven](https://user-images.githubusercontent.com/6911021/27201254-4ba3bd3c-521d-11e7-8bbd-3b8530fed65f.PNG)

### 3: Zdjęcie wejściowe:
![3](https://user-images.githubusercontent.com/6911021/27201277-66ad53d6-521d-11e7-9f42-ac5dfa6d45a9.jpg)
Wynik:
![3_ven](https://user-images.githubusercontent.com/6911021/27201278-67a759f8-521d-11e7-8b66-8e821a0ea272.PNG)

### 4: Zdjęcie wejściowe:
![4](https://user-images.githubusercontent.com/6911021/27201279-6a9f1cf4-521d-11e7-8544-dccbf14af6ed.jpg)
Wynik:
![4_ven](https://user-images.githubusercontent.com/6911021/27201281-6c093f70-521d-11e7-89a6-3f90f8c30eb3.PNG)

### 5: Zdjęcie wejściowe:
![5](https://user-images.githubusercontent.com/6911021/27201283-6db5104c-521d-11e7-8c97-20c872fe0ec3.jpg)
Wynik:
![5_ven](https://user-images.githubusercontent.com/6911021/27201290-705f3d68-521d-11e7-92ac-aab1bcdd92d1.PNG)

### 6: Zdjęcie wejściowe:
![6](https://user-images.githubusercontent.com/6911021/27201348-a5bffe20-521d-11e7-8d01-a31b0a94e130.jpg)
Wynik:
![6_ven](https://user-images.githubusercontent.com/6911021/27201350-a6bd94c2-521d-11e7-81a4-cf59448ba829.PNG)

### 7: Zdjęcie wejściowe:
![8](https://user-images.githubusercontent.com/6911021/27201353-a9f93f24-521d-11e7-8435-326cb9083157.jpg)
Wynik:
![8_ven](https://user-images.githubusercontent.com/6911021/27201356-ade1e280-521d-11e7-8485-225c54e0b4a3.PNG)

### 8: Zdjęcie wejściowe:
![9](https://user-images.githubusercontent.com/6911021/27201378-cff7c056-521d-11e7-896a-e31429241a9d.jpg)
Wynik:
![9_ven](https://user-images.githubusercontent.com/6911021/27201384-d18932ec-521d-11e7-94a2-c1e4694db27d.PNG)

### 9: Zdjęcie wejściowe:
![10](https://user-images.githubusercontent.com/6911021/27201386-d278b51a-521d-11e7-9036-99a950d94b7a.jpg)
Wynik:
![10_ven](https://user-images.githubusercontent.com/6911021/27201389-d3948c94-521d-11e7-93d8-c431cee81dfb.PNG)

### 10: Zdjęcie wejściowe:
![11](https://user-images.githubusercontent.com/6911021/27201392-d4aada2a-521d-11e7-8064-9f8774622a20.jpg)
Wynik:
![11_ven](https://user-images.githubusercontent.com/6911021/27201393-d5e24ab8-521d-11e7-9b7f-4cdd773b5923.PNG)

