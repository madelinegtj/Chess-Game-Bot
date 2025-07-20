using System.Reflection.PortableExecutable;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advance;

/// <summary>
/// Represents our Advance Game.
/// </summary>
public class Game {

    /// <summary>
    /// Gets white Player object.
    /// </summary>
    public Player White { get; }
    /// <summary>
    /// Gets black Player object.
    /// </summary>
    public Player Black { get; }
    /// <summary>
    /// Gets the Board object.
    /// </summary>
    public Board Board { get; }

    public List<Wall> walls = new List<Wall>();

    /// <summary>
    /// Defines Game object constructor that has board and players as properties.
    /// </summary>
    public Game() {
        Board = new Board();
        White = new Player(Colour.White, this);
        Black = new Player(Colour.Black, this);
    }

    /// <summary>
    /// Returns a string that represents the current game state.
    /// </summary>
    /// <returns>A string that represents the current game state.</returns>
    public override string ToString()
    {
        StringWriter writer = new StringWriter();
        Write(writer);
        return writer.ToString();
    }

    /// <summary>
    /// Clears the board by removing all the Pieces from the board.
    /// </summary>
    public void Clear() {
        Black.Army.RemoveAllPieces();
        White.Army.RemoveAllPieces();
    }

    /// <summary>
    /// Reads rows and columns of the board state.
    /// </summary>
    /// <param name="reader">Text reader to read a sequential characters.</param>
    public void Read(TextReader? reader) {
        //Clears lines from previous reading
        Clear();

        //Reads each row of the board
        for (int row = 0; row < Board.Size; row++) {
            string? currentRow = reader.ReadLine();

            if (currentRow == null)
                throw new Exception("Ran out of data before reading full board");
            if (currentRow.Length != Board.Size)
                throw new Exception("Row ${row} is not the right length");

            //Reads each column of the row
            for (int col = 0; col < Board.Size; col++) 
            {
                Square? currentSquare = Board.Get(row, col);

                char icon = currentRow[col];
                if (icon != '.') 
                {
                    //if square is not empty, then decide whether Piece is black or white
                    Player currentPlayer = Char.IsUpper(icon) ? White : Black;
                    //then recruit new Piece, based on its icon, on the board Square
                    currentPlayer.Army.Recruit(icon, currentSquare);
                }
            }
        }
    }

    /// <summary>
    /// Write rows and columns of board state for output.
    /// </summary>
    /// <param name="writer">Text writer to write a sequential characters.</param>
    public void Write(TextWriter writer) {
        //Reads each row of the board
        for (int row = 0; row < Board.Size; row++) 
        {
            //Reads each column of the row
            for (int col = 0; col < Board.Size; col++) {
                Square currentSquare = Board.Get(row, col);
                Piece? currentPiece = currentSquare.Occupant;

                //Write the Piece icon based on occupant of each squares
                if (currentPiece == null)
                    writer.Write('.');
                else
                    writer.Write(currentPiece.Icon);
            }
            writer.WriteLine();
        }

    }
}