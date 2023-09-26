using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace console_chess
{
    public class Board
    {
    //Properties
    protected int value; protected int side; protected int killCount;
    protected string name; protected string identifier;

    //Methods
    public string getIdentifier()
    {
        return identifier;
    }
    public string getName()
    {
        return name;
    }
    public int getSide()
    {
        return side;
    }
    public int getValue()
    {
        return value;
    }
    // public List<int> validMoves(int square)
    // {
    //     switch (identifier.ToLower().Substring(1, 1))
    //     {
    //         default:
            
    //             break;
    //         case "p": return validatePawn(square); //Violent
    //         case "r": return validateRook(square); //Violent
    //         case "n": return validateKnight(square); //Violent
    //         case "b": return validateBishop(square); //Violent
    //         case "q": return validateRook(square).Concat(validateBishop(square)).ToList();
    //         case "k": return validateKing(square); //Violent
    //             //Knook
    //         case "y": return validateKnight(square).Concat(validateRook(square)).ToList();
    //             //Knishop
    //         case "z": return validateKnight(square).Concat(validateBishop(square)).ToList();
    //         case "-":
    //             Console.WriteLine("Idk how this got activated");
    //             break;
    //     }
    //     return null;
    // }

    }
}