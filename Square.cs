using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advance {

    /// <summary>
    /// Represents each of the squares object on the board.
    /// </summary>
    public class Square {

        /// <summary>
        /// Gets the board object.
        /// </summary>
        public Board Board { get; }
        /// <summary>
        /// Gets the row number of squares position on the board.
        /// </summary>
        public int Row { get; }
        /// <summary>
        /// Gets the column number of squares position on the board.
        /// </summary>
        public int Col { get; }

        /// <summary>
        /// Defines Square object constructor for instantiation.
        /// </summary>
        /// <param name="board">Current game board.</param>
        /// <param name="row">Current square's row number.</param>
        /// <param name="col">Current square's column number.</param>
        public Square(Board board, int row, int col) {
            Board = board;
            Row = row;
            Col = col;
        }

        /// <summary>
        /// Gets Piece on that particular square.
        /// </summary>
        /// <returns>Piece that occupies the square.</returns>
        private Piece? occupant;

        /// <summary>
        /// Gets and checks for square occupant.
        /// </summary>
        /// <returns>The Piece on that square, or null if there's none.</returns>
        public Piece? Occupant {
            get { return occupant; }

            //If the occupant above is null then it's exception error
            //Else the occupant is the piece sitting on that square
            private set {
                if (value == null) throw new ArgumentNullException();
                occupant = value;
            }
        }

        /// <summary>
        /// Checks if square is free or occupied.
        /// </summary>
        /// <returns>Boolean true if square has no occupant</returns>
        public bool IsFree {
            get {
                return Occupant == null;
            }
        }

        /// <summary>
        /// Checks if square is occupied or empty.
        /// </summary>
        /// <returns>Boolean true if square is not free</returns>
        public bool IsOccupied {
            get {
                return !IsFree;
            }
        }

        /// <summary>
        /// Determines whether the square contains a wall.
        /// </summary>
        public bool ContainsWall
        {
            get { return Occupant.Player == null; }
        }

        /// <summary>
        /// Places a piece on a particular square.
        /// </summary>
        /// <param name="piece">The Piece that is going to be placed.</param>
        public void Place(Piece piece) {
            if (IsOccupied) throw new ArgumentException("Piece cannot be placed in occupied square.");
            Occupant = piece;
        }

        /// <summary>
        /// Removes occupant Piece from a square.
        /// </summary>
        public void Remove() {
            occupant = null;
        }

        /// <summary>
        /// Gets the squares adjacent to this square in the four cardinal directions.
        /// </summary>
        public IEnumerable<Square> AdjacentSquares
        {
            get
            {
                List<Square> adjacentSquares = new List<Square>();

                // Square above
                if (Row - 1 >= 0)
                {
                    Square squareAbove = Board.Get(Row - 1, Col);
                    adjacentSquares.Add(squareAbove);
                }

                // Square below
                if (Row + 1 < Board.Size)
                {
                    Square squareBelow = Board.Get(Row + 1, Col);
                    adjacentSquares.Add(squareBelow);
                }

                // Left square
                if (Col - 1 >= 0)
                {
                    Square squareLeft = Board.Get(Row, Col - 1);
                    adjacentSquares.Add(squareLeft);
                }

                // Right square
                if (Col + 1 < Board.Size)
                {
                    Square squareRight = Board.Get(Row, Col + 1);
                    adjacentSquares.Add(squareRight);
                }

                return adjacentSquares;
            }
        }

        /// <summary>
        /// Gets all squares neighboring this square, including diagonals.
        /// </summary>
        public IEnumerable<Square> NeighbourSquares
        {
            get
            {
                List<Square> neighbourSquares = new List<Square>();

                // Square above
                if (Row - 1 >= 0)
                {
                    Square squareAbove = Board.Get(Row - 1, Col);
                    neighbourSquares.Add(squareAbove);
                }

                // Square below
                if (Row + 1 < Board.Size)
                {
                    Square squareBelow = Board.Get(Row + 1, Col);
                    neighbourSquares.Add(squareBelow);
                }

                // Left square
                if (Col - 1 >= 0)
                {
                    Square squareLeft = Board.Get(Row, Col - 1);
                    neighbourSquares.Add(squareLeft);
                }

                // Right square
                if (Col + 1 < Board.Size)
                {
                    Square squareRight = Board.Get(Row, Col + 1);
                    neighbourSquares.Add(squareRight);
                }

                // Top left square
                if (Row - 1 >= 0 && Col - 1 >= 0)
                {
                    Square squareTopLeft = Board.Get(Row - 1, Col - 1);
                    neighbourSquares.Add(squareTopLeft);
                }

                // Top right square
                if (Row - 1 >= 0 && Col + 1 < Board.Size)
                {
                    Square squareTopRight = Board.Get(Row - 1, Col + 1);
                    neighbourSquares.Add(squareTopRight);
                }

                // Bottom left square
                if (Row + 1 < Board.Size && Col - 1 >= 0)
                {
                    Square squareBottomLeft = Board.Get(Row + 1, Col - 1);
                    neighbourSquares.Add(squareBottomLeft);
                }

                // Bottom right square
                if (Row + 1 < Board.Size && Col + 1 < Board.Size)
                {
                    Square squareBottomRight = Board.Get(Row + 1, Col + 1);
                    neighbourSquares.Add(squareBottomRight);
                }

                return neighbourSquares;
            }
        }


        /// <summary>
        /// Lists all threatening pieces that can attack a particular Piece.
        /// </summary>
        /// <returns>Threatining Pieces in a list.</returns>
        public IEnumerable<Piece> ThreateningPieces(Player player)
        {
            List<Piece> threateningPieces = new List<Piece>();
            foreach (var piece in player.Army.Pieces)
            {
                if (!piece.OnBoard) continue;

                if (piece is General)
                {
                    // For General, check only the neighbour squares, not using IsThreatenedBy
                    if (piece.Square.NeighbourSquares.Any<Square>(square => square.Equals(this)))
                    {
                        threateningPieces.Add(piece);
                    }
                }
                else
                {
                    // If a piece can attack this square, it's threatened
                    if (piece.CanAttack(this))
                    {
                        threateningPieces.Add(piece);
                    }
                }

            }
            return threateningPieces;
        }

        /// <summary>
        /// Checks if a Piece is threatened.
        /// </summary>
        /// <returns>Boolean true if there's any threatening pieces.</returns>
        public bool IsThreatenedBy(Player player)
        {
            return ThreateningPieces(player).Count() != 0;
        }

        

        /// <summary>
        /// Converts and print value into a String sentence.
        /// </summary>
        /// <returns>Parsed text.</returns>
        public override string ToString() {
            if (IsFree)
                return $"Empty square at {Row}, {Col}";
            else
                return $"{Occupant}";
        }
    }
}