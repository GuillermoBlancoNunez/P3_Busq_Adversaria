# Tic-Tac-Toe

`Guillermo Blanco Núñez` & `Fiz Garrido Escudero` 

---

## 1. AGENTE

### 1.1. Variables `AGENT` y `HUMAN` y constructor clase **Agent**

* **¿Qué hace?** Define con dos enteros qué símbolo, 1 para 'X' o -1 para 'O', representa al agente y al humano.
* **OBJETIVO** Evitar valores mágicos y facilitar:

  * Reutilizar el agente con distintos símbolos (X/O).
  * Ajustar configuraciones sin cambiar la lógica.
  
  De esta forma, se puede crear un agente que busque la victoria del usuario 'X' (`new Agent(1, -1)`) o uno que busque la victoria del usuario 'O' (`new Agent(-1, 1)`). Esto es especialmente útil en el modo de juego Agente Vs Agente.

```csharp
private int AGENT;
private int HUMAN;
public Agent(int AGENT, int HUMAN) 
{
    this.AGENT = AGENT;
    this.HUMAN = HUMAN;
}
```

### 1.2. Método `NextMove`

* **¿Qué hace?** Recorre el tablero buscando celdas vacías en las que pueda jugar, simula cada posible jugada del agente evaluando la posibilidad de victoria haciendo esa jugada y retorna la posición que más acerque a la victoria al agente.
* **OBJETIVO** El objetivo del agente es minimizar la ventaja del adversario (agente es jugador MIN).

### 1.3. Función `Minimax` recursiva

* **¿Qué hace?** Explora exhaustivamente el árbol de juego desde el estado actual hasta nodos terminales, asigna un valor de utilidad y propaga esos valores hacia arriba para elegir la mejor jugada.
* **OBJETIVO**

  * Reflejar fielmente la teoría de búsqueda adversaria para un agente óptimo.
  * Mantener la implementación clara y modular, preparando el terreno para optimizaciones (poda alpha‑beta, heurísticas).

**Implementación paso a paso:**

```csharp
int Minimax(int[,] board, bool turnHuman) {
    // 1. Caso base: estado terminal
    if (Win(HUMAN, board))      // si el humano gana
        return +1;              // utilidad máxima para MAX
    if (Win(AGENT, board))      // si el agente gana
        return -1;              // utilidad mínima para MIN
    if (Tie(board))             // si empate
        return 0;               // utilidad neutra

    // 2. Inicializar mejor valor según tipo de turno
    if (turnHuman) {
        // MAX: el humano busca maximizar su utilidad
        int bestValue = int.MinValue;
        // 2.1. Generar sucesores: probar cada celda vacía
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                if (board[i, j] == 0) {
                    board[i, j] = HUMAN;                         // simular movimiento
                    int eval = Minimax(board, false);           // evaluar recursivamente
                    board[i, j] = 0;                             // deshacer (backtracking)
                    bestValue = Math.Max(bestValue, eval);       // elegir el mayor
                }
            }
        }
        return bestValue;
    } else {
        // MIN: el agente busca minimizar la utilidad del humano
        int bestValue = int.MaxValue;
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                if (board[i, j] == 0) {
                    board[i, j] = AGENT;
                    int eval = Minimax(board, true);
                    board[i, j] = 0;
                    bestValue = Math.Min(bestValue, eval);       // elegir el menor
                }
            }
        }
        return bestValue;
    }
}
```
El algoritmo Minimax se implementa de la siguiente forma: 

1. **Parámetros**:

   * `board`: matriz 3×3 con valores `1`, `-1` o `0`.
   * `turnHuman`: `true` para turno MAX (humano), `false` para MIN (agente).
2. **Casos base**: determinan si el estado es victoria, derrota o empate y devuelven la utilidad correspondiente.
3. **Generación de sucesores**: doble bucle recorre celdas vacías, simulando cada posible movimiento.
4. **Backtracking**: tras cada llamada recursiva, el estado original se restaura para explorar otras ramas sin interferencias.
5. **Propagación de valores**:

   * En MAX, se toma el máximo de las utilidades de los sucesores.
   * En MIN, se toma el mínimo.

Este diseño garantiza una exploración completa y una correcta toma de decisiones bajo el modelo de adversarial search.

---

## 2. Program.cs: interfaz y flujo de juego

### 2.1. Métodos de juego (`PlayerVsPlayer`, `PlayerVsAgent`, `AgentVsAgent`)

* **¿Qué hace?** Configuran quiénes juegan (humano o agente) y lanzan la mecánica común.
* **¿Por qué?**

  * Mantener claridad: cada modo separado dedica su configuración propia.
  * Facilitar la incorporación de nuevos modos sin tocar la lógica interna.

### 2.2. Función genérica `PlayGame`

* **¿Qué hace?** Gestiona el bucle de turnos usando delegados para obtener movimientos de X y O.
* **OBJETIVO**

  * Desacoplar la lógica de turno de quién toma la decisión.
  * Evitar duplicar el bucle en cada modo de juego.

### 2.3. Lectura y validación `Input`

* **¿Qué hace?** Pide fila y columna al usuario, verifica que la posición dada esté en el formato correcto, dentro de rango y libre.
* **OBJETIVO**

  * Evitar errores de índice y mantener la experiencia fluida.
  * Informar al usuario con mensajes claros en caso de entradas inválidas.

### 2.4. Utilidades de resultado (`Win`, `Tie`, `PrintBoard`)

* **¿Qué hacen?** Verificar condiciones de victoria/empate e imprimir el tablero en consola.
* **OBJETIVO**

  * Seguir el principio de responsabilidad única: cada función hace una sola tarea.
  * Facilitar tests unitarios de la lógica de victoria sin necesidad de UI.

---

## 3. Ventajas del enfoque Minimax

* **Óptimo contra adversario perfecto:** garantiza la mejor estrategia defensiva y ofensiva.
* **Transparente y depurable:** cada valor de utilidad se asigna explícitamente.
* **Base para mejoras:** se puede introducir poda alpha-beta o heurísticas sin reestructurar el código.

---

