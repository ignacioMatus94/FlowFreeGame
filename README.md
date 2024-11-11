Flow Free Solver
Flow Free Solver es un juego de rompecabezas donde el objetivo es conectar pares de puntos de colores en una cuadrícula sin que los caminos se solapen ni dejen espacios vacíos. La solución utiliza algoritmos de búsqueda inteligentes para resolver las cuadrículas automáticamente.

Objetivo del Juego
El objetivo principal es conectar todos los pares de puntos de colores en una cuadrícula de forma que:

Todos los puntos de color estén conectados a su pareja correspondiente.
Los caminos entre los puntos no se crucen ni se solapen.
Todas las celdas de la cuadrícula estén ocupadas por un camino, sin dejar celdas vacías.
El juego se completa cuando todos los puntos están conectados correctamente y la cuadrícula está llena.

Cómo Jugar
Selección de Tablero: Al iniciar, elige el tablero que deseas resolver entre las siguientes opciones:

Tablero 2x2 con 1 color.
Tablero 3x3 con 1 color.
Tablero 5x5 con 2 colores.
Tablero 6x6 con 3 colores.
Seleccionar Algoritmo de Resolución: Una vez seleccionado el tablero, elige el algoritmo de búsqueda que deseas utilizar para resolverlo. Las opciones incluyen:

IDS (Iterative Deepening Search): Busca de manera iterativa, incrementando la profundidad de búsqueda hasta encontrar la solución.
A*: Encuentra el camino más corto basándose en una heurística de distancia.
IDA*: Similar a A*, pero realiza una búsqueda profunda limitada iterativamente.
CSP (Constraint Satisfaction Problem): Divide el problema en restricciones y verifica que cada color cumpla las reglas.
Beam Search: Explora un conjunto limitado de opciones en cada nivel de búsqueda, manteniéndose enfocado en los caminos más prometedores.
Ingresar Parámetros de Algoritmo (si es necesario): Algunos algoritmos, como IDS o Beam Search, requieren parámetros adicionales:

Para IDS, ingresa la profundidad máxima.
Para Beam Search, ingresa el ancho de búsqueda.
Observar el Resultado: El algoritmo elegido resolverá la cuadrícula y mostrará la solución en la consola, incluyendo métricas como:

Nodos Explorados: Cantidad de nodos evaluados para encontrar la solución.
Tiempo de Ejecución: Tiempo que tomó el algoritmo en resolver la cuadrícula.
Volver al Menú Principal o Probar Otro Algoritmo: Después de resolver el tablero, puedes volver al menú principal para seleccionar otro tablero o algoritmo.
