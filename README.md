# Chess Game Bot (Object Oriented Programming)


## Program Overview:
#### At this point, this Advance Game bot is currently capable of doing basic moves, that is checking for legal moves and attacks for each piece. that will read board state from an input file, decide which legal actions to do, execute a move or attack where possible.

## Classes and objects:
### 1. Program
```
   Overview: The main program of our game which shows the outline of our game: Parse Args -> Load/Read board -> Play one move -> Save/Write board
   Methods:
   - ParseArguments()
     Parses the arguments that is passed from the input file, to find out about board state and move instruction.
   - Load()
     Uses StreamReader() to read the input file.
   - PlayOneTurn()
     Choose a move by using ChooseMove() method, then execute the chosen move by using DoAction() method.
   - Save()
     Uses StreamWriter() to write board state into the output file.
```

### 2. Action
```
   Overview: Deals with actions (move and attack) in the game. It keeps track of who the current player and the target player is, and the score after an action is executed.
   Methods:
   - DoAction()
     Returns a boolean true if an action, either move or attack, is successfully executed.
   SubClasses:
	a) Move
	   Contains common behaviour when making a move Action.
	b) Attack
	   Contains common behaviour when making an attack Action.
```

### 3. Army
```
   Overview: Deals with all types of pieces as a whole, therefore called as army. Defines the default position of each piece and their symbols.
   Methods:
   - RemoveAllPieces()
     Makes all the pieces leave the board.
   - Recruit()
     Recruits Piece to the Board based on their icon given in board state. 
```

### 4. Board
```
   Overview: Deals with everything about the board including the size of the board, and the cells/squares on the board.
   Methods:
   - Set()
     Sets a row and column number to a square.
   - Get()
     Gets a square from the specified row and column number.
```

### 5. Player
```
   Overview: Contains identity, state, and behaviour of the current player, such as its direction(upwards or downwards), opponent, and possible moves. Players are differed by their color.
   Methods:
   - ChooseMove()
     Picks an action (can be move or attack) randomly from a list of actions.
   - FindPossibleActions()
     Pass each actions into FindPossibleMoves() and FindPossibleAttacks()
   - FindPossibleMoves()
     Iterate through each pieces and all squares on the board to collect all the possible moves.
   - FindPossibleAttacks()
     Iterate through each pieces and all squares on the board to collect all the possible attacks.
```

### 6. Square
```
   Overview: Contains the state and behaviour of the squares; is the square free or occupied, who the square occupant is, and if it is threatened.
   Methods:
   - Place()
     Places a piece as the occupant of a square.
   - Remove()
     Removes the occupant piece from the square.
```

### 7. Game
```
   Overview: Deals with the overall game, such as the initial board state and the outcome board state.
   Methods:
   - Read()
     Reads the board state that is in the form of text, rows by rows, columns by columns.
   - Write()
     Writes the board state as text.
   - Clear()
     Clears the text, i.e. removed all the pieces.
```

### 8. Piece
```
   Overview: Contains common methods and behaviours of all types of the pieces.
   Methods:
   - MoveTo()
     Move Piece to a new square (if conditions are met) and returns boolean true after successfully moved to new square.
   - CanMoveTo()
     Returns boolean true if Piece can move to the given square.
   - Attack()
     Attack a target square (if conditions are met) and returns boolean true after successfully made the attack.
   - CanAttack()
     Returns boolean true if Piece can attack the given square.
   - LeaveBoard()
     Removes a piece from the square.
   - Defect()
     To change the team of a Piece by changing its color.


   Subclasses:
	a) Builder:
	   Can move AND capture any of the 8 adjoining squares.
           Can build walls to stop enemy pieces.
	b) Catapult:
	   Can move in 4 cardinal directions, but only 1 square at a time.
	   Can capture pieces that are 3 squares away in cardinal directions, and pieces that are 2 squares away in two perpendicular cardinal directions.
	c) Dragon:
	   Can move horizontally, vertically, or diagonally in any number of squares, but cannot leap/jump over an intervening piece.
	   Can capture horizontally, vertically, or diagonally in any number of squares as well, except for the 8 adjoining squares.
	d) General
	   Can move AND capture on any of the 8 adjoining squares.
	   Has to anticipate a movement to get away from enemy piece when in danger/ threatened.
	e) Jester:
	   Can move on any of the 8 adjoining squares.
	   Cannot capture/attack.
	   Can swap a Piece's team/colour.
	   Can switch place with a friendly Pieces, except fellow Jester.
	f) Miner:
	   Can move AND capture in one of the 4 cardinal directions.
	   Can capture Wall built by Builder.
	g) Sentinel:
	   Can move two squares in one cardinal direction and one square in a perpendicular direction, and can jump over any intervening pieces and walls.
	   Can capture two squares in one cardinal diretion.
	h) Wall:
	   Don't belong to any player. Cannot move and cannot attack.
	i) Zombie:
	   Can move AND capture in 3 adjacent squares.
	   Can additionally do a leaping attack if there's empty piece there and the intermediate square is empty.

```

## Custom Types:
### 1. Colour
#### represents the colour of the piece. It is an enum created to differ two players in the game; one is Black, and another is White.

### How to pass arguments : 

```
.\Program.exe [color] [test/input file path] [output file path].txt
```

Specifications that are not implemented yet:
-Choosing best moves (currently, moves are picked randomly)
-Score keeping (initalized the dictionary but haven't implemented it on the pieces yet)
-Higher order reasoning