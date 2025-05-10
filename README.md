# Tic-Tac-Toe

`Guillermo Blanco Núñez` & `Fiz Garrido Escudero` 

---

## División de clases

El código se divide en ocho clases. El objetivo de esta división es independizar las clases entre sí y que cada clase cumpla una sola función. De esta forma si hace falta añadir o modificar funcionalidades se no hace falta cambiar todas las clases. Además, de esta forma se pueden reutilizar clases en proyectos futuros, y se pueden ejecutar unit test en cada clase para ver si hay algún fallo y poder identificar rápido dónde está.

A continuación se explicará cada clase individualmente:<br><br><br>

## class Player

Clase muy corta que hace de clase padre de las clases Agent_Player y Human_Player. Se crea para que en el resto del código no se tenga que especificar si el jugador es humano o agente. El único método que tiene es GetMove, introduciendo el tablero, que después se modificará obligatoriamente en las clases hijas ya que se trata de una clase Interface. 

```csharp
public interface Player 
{
    Move GetMove(Board board);
}
``` 
<br><br><br>

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


La función Minimax es una función recursiva que toma como parámetros el tablero actual, un booleano de si es el turno del humano, el valor de alfa y beta, la profundidad y los símbolos de agente y humano. Lo primero que se define son los nodos terminales: si el agente gana se devuelve 10-depth, si el humano gana se devuelve -10+depth y si hay empate se devuelve 0. Esto se hace así para que el agente priorice victorias más rápidas sobre otras en más movimiento. Se utiliza 10 para que no prefiera acabar la partida antes aunque sea perdiendo. 

```csharp
if (board.IsWin(agentSymbol)) return 10 - depth;
if (board.IsWin(humanSymbol)) return -10 + depth;
if (board.IsTie()) return 0;
```
 Lo sigueinte en definirse es la parte recursiva de la función, que es en sí muy similar a la función FindBestMove, pero en vez de devolver la mejor jugada devuelve el valor de esa jugada. Lo que hace es recorrer todas las jugadas posibles dependiendo del turno que sea y aplicar cada una, después hará una llamada recursiva añadiendo uno a depth e invirtiendo la variable booleana turnHuman. Al hacerse esto para todas las posibles jugadas, se acabará llenando el tablero de todas las combinaciones posibles para así poder ver qué jugada te acerca más a la victoria. 

Las podas α-β cuando es el turno del agente actualiza α con el máximo entre α y el valor retornado, y cuando es el turno del humano actualiza β con el mínimo entre β y el valor retornado. Si en cualquier punto β ≤ α, el bucle se interrumpe  con un `break`, descartando subárboles que no pueden mejorar el resultado y haciendo la búsqueda del movimiento óptimo más rápida.
```csharp
beta = Math.Max(beta, worst);
if (beta <= alpha) break;

alpha = Math.Min(alpha, best);
if (beta <= alpha) break;
```
<br><br><br><br>

## class Move

La estructura Move representa un movimiento en el tablero mediante las coordenadas `row` y `col`. Su diseño como struct se usa para que sea más eficiente y evitar referencias compartidas y copias innecesarias en memoria.
<br><br><br><br>

## class Board

La clase Board representa el tablero de tres en raya, modelado como una matriz de enteros de 3x3, donde los valores indican el estado de cada celda (0 para vacío, 1 para jugador X, y -1 para jugador O). Su diseño encapsula la lógica del tablero, proporcionando métodos para verificar si una celda está vacía (`IsEmpty`), aplicar un movimiento (`ApplyMove`), obtener movimientos disponibles (`GetAvailableMoves`), y determinar si hay un ganador (`IsWin`) o un empate (`IsTie`). Además, sobrescribe `ToString` para generar una representación visual del tablero.
<br><br><br><br>

## class Game 

La clase `Game` se encarga de manejar todo el flujo del juego de tres en raya. Coordina a los jugadores (`playerX` y `playerO`), el tablero (`board`), y el turno actual. Su método principal, `Run`, utiliza un bucle infinito para alternar entre los jugadores, mostrando el estado del tablero y pidiendo movimientos. También verifica si alguien ganó (`IsWin`) o si el juego terminó en empate (`IsTie`) y termina el bucle si alguna de estás dos opciones ocurre. La opción de pausar entre turnos (`waitBetweenTurns`) se utiliza en partidos de Agente vs. Agente para poder ver el progreso de la partida tras cada jugada y que el estado del tablero no salte directamente al final.
<br><br><br><br>

## class Program

La clase `Program` actúa como el punto de entrada del juego de tres en raya, permitiendo al usuario elegir entre tres modos de juego: Jugador vs. Jugador, Jugador vs. Agente, o Agente vs. Agente. Utiliza un bucle infinito para mostrar un menú interactivo, leer la opción seleccionada y crear una instancia de la clase Game con los jugadores correspondientes en la que configura los jugadores según el modo elegido.