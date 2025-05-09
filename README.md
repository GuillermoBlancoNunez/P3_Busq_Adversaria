# Tic-Tac-Toe

`Guillermo Blanco Núñez` & `Fiz Garrido Escudero` 

---

## División de clases

El código se divide en ocho clases. El objetivo de esta división es independizar las clases entre sí y que cada clase cumpla una sola función. De esta forma si hace falta añadir o modificar funcionalidades se no hace falta cambiar todas las clases. Además, de esta forma se pueden reutilizar clases en proyectos futuros, y se pueden ejecutar unit test en cada clase para ver si hay algún fallo y poder identificar rápido dónde está.

A continuación se explicará cada clase individualmente:<br><br><br>

## class Player

Clase muy corta que hace de clase padre de las clases Agent_Player y Human_Player. Se crea para que en el resto del código no se tenga que especificar si el jugador es humano o agente. El único método que tiene es GetMove, introduciendo el tablero, que después se modificará en las clases hijas. 

```csharp
public interface Player 
{
        Move GetMove(Board board);
}
``` 
<br><br><br><br><br>
## class Human_Player

Clase hija de la clase `Player` que hereda su método GetMove. En este caso el constructor toma como parámetros el símbolo que representa en el tablero  también como entero (1 para X y -1 para O), y también el símbolo en string ("X" o "O").

El método GetMove toma el tablero actual como parámetro. Crea un bucle en el que se solicita una entrada por pantalla al usuario pidiendo una posición en el formato `row, col`. Este bucle solo se corta cuando se encuentra una entrada de una posición válida y vacía y esta se devuelve. En el bucle se hacen tres comprobaciones. Primero que la entrada dada no sea un valor nulo ni esté en blanco. Si el if es cierto, entonces se muestra un mensaje de error por pantalla y el bucle vuelve a empezar.
```csharp 
if (string.IsNullOrWhiteSpace(input))
```
Después se divide la entrada, separando en cada coma y poniendo las partes en una lista de strings. Con esta lista se efectúa la segunda comprobación. En esta se comprueba si la lista tiene solo dos elementos (row y col), si estos son números enteros y son menores que tres pero mayores o igual a cero. 
```csharp
if (parts.Length == 2
                    && int.TryParse(parts[0], out int row)
                    && int.TryParse(parts[1], out int col)
                    && row >= 0 && row < 3 && col >= 0 && col < 3)
```
Si esta segunda comprobación se pasa entonces se entra en la tercera y última comprobación, en la que se comprueba si la celda está ocupada o vacía. Para esto se utiliza el método IsEmpty de la clase Board. Como este método no es estático se efectúa utilizando la instancia de Board que se pasa como parámetro en la función. Este método toma como parámetros la fila y columna (row y col) que se dieron de entrada. Si está vacía esa posición el método GetMove devuelve una nueva instancia de la clase Move con los valores dados y comprobados de row y col. 
```csharp
if (board.IsEmpty(row, col)) 
                    {
                        return new Move(row, col);
                    }

```
En caso de que alguna de estas dos comprobaciones sean falsas, se ejecuta un else que muestra un mensaje de error y finaliza la iteración, reiniciando el bucle de nuevo.
<br><br><br><br><br>

## class Agent_Player

La otra clase hija de la clase `Player`, también hereda su método GetMove. El constructor toma como parámetros el símbolo del agente y del humano, tomados como enteros (1 para símbolo X, -1 para símbolo O), para saber de cuál símbolo del tablero busca la victoria el agente. 
El método GetMove toma el tablero actual y llama al método estático FindBestMove de la clase MinimaxEngine pasándole el tablero y los símbolos de humano y agente. 
```csharp
public Move GetMove(Board board) 
        {
            return MinimaxEngine.FindBestMove(board, agentSymbol, humanSymbol);
        }
```
Al ser un método estático no se crea una instancia de la clase MinimaxEngine para llamar a su método FindBestMove.
<br><br><br><br><br>

## class MinimaxClass

Clase que implementa el algoritmo minimax, con padas alfa-beta y que penaliza la profundidad del nodo terminal. 

La clase es llamada desde la clase Agent_Player en su función GetMove (como se puede ver más arriba) pasándole como parámetros el tablero y los símbolos que deban ganar (agentSymbol) y el que deba perder (huanSymbol) como enteros a la función estática FindBestMove. Al ser una función estática, nunca se crea una instancia de la clase MinimaxClass. 

Para empezar se crean dos variables `bestValue` y `bestMove`en las que se guardan incialmente el valor entero mínimo en C# y un movimiento inválido (-1, -1) respectivamente. Ahora se entra a un bucle en el que se recorren todos los movimientos válidos con el tablero actual. En cada iteración se aplicará el movimiento para el agente y se buscará el valor de ese movimiento llamando a la función Minimax. Después se deshará el movimiento hecho, para que quede en una mera simulación y solo si el valor del movimiento es mayor que el mejor valor entonces el movimiento estudiado pasa a ser el mejor valor y el mejor movimiento. Una vez se hayan estudiado todos los movimiento posibles en el tablero, la función devolverá el mejor movimiento. 
