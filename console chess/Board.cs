using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace console_chess
{
    public class Board
    {
    //Properties
    private int value; private int side; private int killCount;
    private string name; private string identifier;

    //Methods
    public void assignPiece(string type, int Side)
        {
            switch (type.ToLower())
            {
                default:
                    break;
                case "r":
                    value = 5;
                    name = "Rook";
                    identifier = "R";
                    break;
                case "b":
                    value = 3;
                    name = "Bishop";
                    identifier = "B";
                    break;
                case "n":
                    value = 3;
                    name = "Knight";
                    identifier = "N";
                    break;
                case "q":
                    value = 9;
                    name = "Queen";
                    identifier = "Q";
                    break;
                case "k":
                    value = 20;
                    name = "King";
                    identifier = "K";
                    break;
                case "p":
                    value = 1;
                    name = "Pawn";
                    identifier = "P";
                    break;
                case "Y": //custom pieces
                    value = 6;
                    name = "Knook";
                    identifier = "Y";
                    break;
                case "Z":
                    value = 6;
                    name = "Knishop";
                    identifier = "Z";
                    break;
                case "empty":
                    value = 0;
                    name = "Empty";
                    identifier = "-";
                    break;
            }
            if (Side == 1)
            {
                identifier = identifier.Insert(0, "w");
                side = 1;
            }
            else if (Side == 2)
            {
                identifier = identifier.Insert(0, "b");
                side = 2;
            }
            else if (Side == 0)
            {
                identifier = identifier.Insert(0, " ");
                side = 0;
            }
        }
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
    public List<int> validMoves(int square)
    {
        switch (identifier.ToLower().Substring(1, 1))
        {
            default:
                break;
            case "p": return validatePawn(square); //Violent
            case "r": return validateRook(square); //Violent
            case "n": return validateKnight(square); //Violent
            case "b": return validateBishop(square); //Violent
            case "q": return validateRook(square).Concat(validateBishop(square)).ToList();
            case "k": return validateKing(square); //Violent
                //Knook
            case "y": return validateKnight(square).Concat(validateRook(square)).ToList();
                //Knishop
            case "z": return validateKnight(square).Concat(validateBishop(square)).ToList();
            case "-":
                Console.WriteLine("Idk how this got activated");
                break;
        }
        return null;
    }

    }
}