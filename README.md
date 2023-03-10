# Cross_Zero_API
Реализован API для игры крестики нолики:
В файле user json хранятся id игр с текущими статусами игровых полей 
1. NewGame
Метод создает новую игру
Генерируется новый id для игры и выдается пользователю 
На вход параметры не подаются
На выходе пользователь получает седующую информацию в виде JSON

number - номер игры 

Winner - победитель (если есть) , либо игра продолжается, либо ничья

PositionsOfWinner- клетки выигрышной комбинации, если есть . Если  нет - null.

gameBoard - актуальное игровое поле в виде массива 

PlayerSymbol - символ, который должен делать следующий ход

2. ChangeField
Метод вызывается после хода одного из игроков
На вход подаются следующие параметры :
board - поле в виде массива

number - номер игры

Далее сохраняется текущее состояние поля
И проверяется состояние игры после хода игрока ( победа одного из игроков, ничья, игра продолжается)
На выходе пользователь получает седующую информацию в виде JSON
number - номер игры 

Winner - победитель (если есть) , либо игра продолжается, либо ничья

PositionsOfWinner- клетки выигрышной комбинации, если есть . Если  нет - null.

gameBoard - актуальное игровое поле в виде массива 

PlayerSymbol - символ, который должен делать следующий ход

3. GetGame
Метод возвращает игру из базы данных по по ее номеру
На вход подаются следующие параметры :

number - номер игры

Далее проверяется наличие данной игры
Если она существует, то возвращается следующий JSON

number - номер игры 

Winner - победитель (если есть) , либо игра продолжается, либо ничья

PositionsOfWinner- клетки выигрышной комбинации, если есть . Если  нет - null.

gameBoard - актуальное игровое поле в виде массива 

PlayerSymbol - символ, который должен делать следующий ход

4. RandomChange
Метод который делает случайный ход за игрока
На вход подаются следующие параметры :

number - номер игры

board - поле в виде массива

Далее делается случайный ход и   возвращается следующий JSON
number - номер игры 

Winner - победитель (если есть) , либо игра продолжается, либо ничья

PositionsOfWinner- клетки выигрышной комбинации, если есть . Если  нет - null.

gameBoard - актуальное игровое поле в виде массива 

PlayerSymbol - символ, который должен делать следующий ход

5. Delete
Метод который удаляет игру по номеру
На вход подаются следующие параметры :

number - номер игры

Далее если игра существует, то происходит ее удаление из БД, и  возвращается результат операции ( успешно или нет)


Также реализована поддержка Docker
