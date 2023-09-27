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

    protected List<int> printPossibleMoves(int[] array)
    {
        List<int> curatedList = new List<int>();
        foreach (var item in array)
        {
            if (item >= 0 & item < 64)
            {
                int temp = (item / 8) + 1;
                string temp1 = Globals.convNumToLetter(item);
                Console.Write(temp1 + temp.ToString() + " ");
                curatedList.Add(item);
            }
        }
        Console.WriteLine();
        return curatedList;
    }

    // public void movePiece(int location)
    // {
    //     //location is where the piece is going to, this method runs from the object that is moving

    //     if (Globals.board[location].getSide() != side & Globals.board[location].getSide() != 0)
    //     {
    //         takePiece(location);
    //     }
    //     else
    //     {
    //         Globals.board[location] = this;
    //     }
    // }
    // private void takePiece(int location)
    // {
    //     killCount += Globals.board[location].getValue();
    //     Random random = new Random();
    //     //play funny event
    //     //pick a number
    //     Console.WriteLine("You're trying to take a piece! \nPick a number between 1 and {0}", killCount);
    //     if (int.Parse(Console.ReadLine()) == random.Next(1, killCount + 1))
    //     {
    //         Globals.funnyStall();
    //         Console.WriteLine("It worked!");
    //         Globals.board[location] = this;
    //         Thread.Sleep(1000);
    //     }
    //     else
    //     {
    //         Globals.funnyStall();
    //         killCount -= Globals.board[location].getValue();
    //         Console.WriteLine("It failed!");
    //         Thread.Sleep(1000);
    //     }
    // }
    }
}