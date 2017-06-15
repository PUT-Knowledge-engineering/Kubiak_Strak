# Kubiak_Strak
Recognition of blood vessels in angiographic images of the eye

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
