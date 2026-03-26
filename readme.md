# Instrukcja uruchomienia:
Prezentacja funkcjonalności programu została zaimplementowana w metodzie Main w pliku Program.cs.
Został tam stworzony przykładowy scenariusz testowy, w którym przedstawione jest działanie programu w róznych przypadkach (poprawne wypożyczenia, próby złamania limitów, zwroty po terminie, generowanie raportów).
Aby zobaczyć działanie programu nalezy uruchomić plik Program.cs z poziomu IDE

# Krótki opis projektu:

Projekt został podzielony na komponenty domenowe w folderze Domain i serwisy w folderze Services.

## Domena:
### Sprzęt:
Do prezentacji sprzętów wykorzystałem klasę abstrakcyjną Equipment z uniwersalnymi polami name, brand, model oraz status do którego prezentacji użyłem enuma.
Różne rodzaje sprzętu prezentowane są za pomocą dedykowanych klas dziedziczących po Equipment, dodając unikalne pola dla każdego typu.
### Użytkownicy
Analogicznie użytkownicy maja bazową klasę User oraz unikalne klasy dziedziczące. Aby rozróżnić typ użytkownika również wykorzystałem enum.

Ten podział uznałem za sensowny dzięki temu, że umożliwia łatwe dodawanie nowych rodzajów sprzętów i użytkowników. Jednocześnie pozwala na przechowywanie ich w uniwersalnych kolekcjach co umożliwia bezproblemowe operowanie na obiektach niezależnie od typu.
### Wypożyczenie
Pojedyncze wypożyczenie reprezentowane jest przez klasę Rental która przechowuje następujące informacje:
    - kto wypożycza
    - jaki sprzęt
    - kiedy (pole typu datetime inicjalizowane automatycznie aktualną datą w momencie wypożyczania)
    - na ile (wartość typu int reprezentująca liczbę dni)
    - do kiedy (automatycznie wyliczane na podstawie liczby dni)
    - datę zwrotu (nullable - inicjalizowane dopiero w momencie zwrotu sprzętu)
    - karę za ewentualne spóźnienie

stworzyłem tą klasę w ten sposób aby mieć łatwy dostęp do wszystkich najważniejszych danych o wypożyczeniu w jednym miejscu.

## Serwis:
Klasa RentalService zawiera w sobie wszystkie metody niezbędne do poprawnego działania wypożyczalni. Pozwala na dodanie nowych użytkowników i sprzętów, wypożyczenie oraz zwrot sprzętu, zmianę jego statusu oraz zawiera metody potrzebne do raportowania o stanie systemu.
Do obliczania dodatkowej kary za wypożyczenie użyłem dodatkowej klasy FeeCalculator którą wykorzystuje w serwisie do wypożyczania. Dzięki temu umożliwiam ewentualną rozbudowę systemu która mogłaby polegać na implementacji innych zasad naliczania kary poprzez stworzenie nowych klas i ewentualnym stosowaniu ich w zależności od potrzeb.

