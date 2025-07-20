namespace Advance;

/// <summary>
/// Main Program of our Advance Game Bot.
/// </summary>
public class Program {

    /// <summary>
    /// Contains main functionalities of the bot workflow:
    /// Initialize a new game, take arguments, play one turn, then save the outcome.
    /// </summary>
    /// <param name="args">Argument to be parsed.</param>
    private static void Main(string[] args)
    {
        Console.WriteLine("Bot: March");
        Game game = new Game();

        //If ParseArguments method return the parsed args, meaning argument is in valid format,
        //then we can start playing
        if (ParseArguments(args, out Colour colour, out string infile,
                out string outfile))
        {
            //1. Load the board state by reading from input file.
            Load(game, infile);
            //2. Execute one move based on whose turn it is to play.
            PlayOneTurn(game, colour);
            //3. Save the new board state by writing in output file.
            Save(game, outfile);
        }

        //Else ParseArguments() return false, then print error
        else
        {
            Console.WriteLine("Error processing command line arguments.");
        }
    }

    /// <summary>
    /// Parse an argument that contains current player, input file and output file.
    /// </summary>
    /// <param name="args">Argument to be parsed.</param>
    /// <param name="colour">Player colour parsed from args.</param>
    /// <param name="infile">Input file to read the board state.</param>
    /// <param name="outfile">Output file to store new board state.</param>
    /// <returns>The parsed player colour as Colour enum or false when not found.</returns>
    private static bool ParseArguments(
        string[] args,
        out Colour colour,
        out string infile,
        out string outfile
    )
    {
        //Initialize the infile, outfile, and colour
        infile = string.Empty;
        outfile = string.Empty;
        colour = Colour.White;

        //If arguments have the length of 3 (infile, outfile, colour),
        //then it is in valid format and can be parsed
        if (args.Length >= 3)
        {
            //Assign to each variables and return te result
            infile = args[1];
            outfile = args[2];
            return Enum.TryParse<Colour>(args[0], ignoreCase: true, out colour);
        }

        //Else argument is invalid, then return false
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Load board state for the game by reading from the input file.
    /// </summary>
    /// <param name="game">The Game object.</param>
    /// <param name="infile">Input file to read the board state.</param>
    private static void Load(Game game, string infile) 
    {
        //Read board state from infile and handle Exceptions
        try
        {
            using StreamReader reader = new StreamReader(infile);
            game.Read(reader);
        }
        catch (Exception ex) {
            Console.WriteLine("Error loading game from '{0}': {1}", infile, ex.Message);
        }
    }

    /// <summary>
    /// Play one move for player who is currently in turn, if there's possible legal move.
    /// </summary>
    /// <param name="game">The Game object.</param>
    /// <param name="colour">Colour of the player.</param>
    private static void PlayOneTurn(Game game, Colour colour) 
    {
        //Decide which player's turn it is, and choose one of its possible moves
        Player currentPlayer = colour == Colour.White ? game.White : game.Black;
        Advance.Action nextMove = currentPlayer.ChooseMove();

        //and play one move for it if there's possible move
        if (nextMove != null) {
            nextMove.DoAction();
        }
    }

    /// <summary>
    /// Save the updated board state into the output file.
    /// </summary>
    /// <param name="game">The Game object.</param>
    /// <param name="outfile">Output file where the new board state will be stored.</param>
    private static void Save(Game game, string outfile) 
    {
        //Write board state into outfile and handle Exceptions
        try {
            using StreamWriter writer = new StreamWriter(outfile);
            game.Write(writer);
        }
        catch (Exception ex) {
            Console.WriteLine("Error saving game to '{0}': {1}", outfile, ex.Message);
        }
    }

}
