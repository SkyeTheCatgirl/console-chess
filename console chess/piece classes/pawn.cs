using System.Reflection;
using System.Runtime.InteropServices;
using console_chess;

public class pawn : Board
{
    public bool hasMoved = false;
    public bool passantAble = false;
    public pawn(int input)
    {
        value = 1;
        name = "Pawn";
        identifier = "P";
        side = input;
    }
    public pawn(pawn input, bool x)
    {
        value = input.value;
        side = input.side;
        killCount = input.killCount;
        name = input.name;
        identifier = input.identifier;
        hasMoved = input.hasMoved;
        passantAble = input.passantAble;
    }

    public List<int> validatepawn(int square, object[] board)
    {
        int[] tempArrayPawn = new int[4] { -1, -1, -1, -1 }; //the first two places are for forward movement, the latter 2 are for taking pieces

        //moving and taking pieces
            if (side == 1 && square > 7) //white
            {
                //moving forward
                if (square > 7 && board[square + 8] == null)
                {
                    tempArrayPawn[0] = square + 8;
                    if (hasMoved == false && (square / 8) * 8 == 8 && board[square + 16] == null)
                    {
                        tempArrayPawn[1] = square + 16;
                    }
                }
        
                if (square % 8 != 0 && Globals.mDside(board[square + 7]) == 2)
                {
                    tempArrayPawn[2] = square + 7;
                }
                if (square % 8 != 7 && Globals.mDside(board[square + 9]) == 2)
                {
                    tempArrayPawn[3] = square + 9;
                }
            }
            else if (side == 2 && square > 7) //black
            {
                //moving forward
                if (board[square - 8] == null)
                {
                    tempArrayPawn[0] = square - 8;
                    if (hasMoved == false && (square / 8) * 8 == 48 && board[square - 16] == null)
                    {
                        tempArrayPawn[1] = square - 16;
                    }
                }

                if (square % 8 != 7 && Globals.mDside(board[square - 7]) == 1)
                {
                    tempArrayPawn[2] = square - 7;
                }
                if (square % 8 != 0 && Globals.mDside(board[square - 9]) == 1)
                {
                    tempArrayPawn[3] = square - 9;
                }
            }

        return printPossibleMoves(tempArrayPawn);
    }
    public bool en_passant(int square, bool botPlaying)
    {
        if (Globals.vcchoice == 2 && botPlaying) {Globals.playerColour = Globals.playerColour == 1 ? 2 : 1;}
        //white
        if (side == 1)
        {
            if (square % 8 != 0 && Globals.playerColour == side && Globals.mDid(Globals.board[square - 1]) == "P" && ((pawn)Globals.board[square - 1]).passantAble)
            {
                Console.WriteLine("En Passant is forced.");
                Console.WriteLine("Press enter to En Passant");
                Console.ReadLine();
                movePiece(square + 7, square);
                Globals.board[square - 1] = null;
                if (Globals.vcchoice == 2 && botPlaying) {Globals.playerColour = Globals.playerColour == 1 ? 2 : 1;}
                return true;
            }
            else if (square % 8 != 7 && Globals.playerColour == side && Globals.mDid(Globals.board[square + 1]) == "P" && ((pawn)Globals.board[square + 1]).passantAble)
            {
                Console.WriteLine("En Passant is forced.");
                Console.WriteLine("Press enter to En Passant");
                Console.ReadLine();
                movePiece(square + 9, square);
                Globals.board[square + 1] = null;
                if (Globals.vcchoice == 2 && botPlaying) {Globals.playerColour = Globals.playerColour == 1 ? 2 : 1;}
                return true;
            }
        }
        else
        {
            if (square % 8 != 7 && Globals.playerColour == side && Globals.mDid(Globals.board[square + 1]) == "P" && ((pawn)Globals.board[square + 1]).passantAble)
            {
                Console.WriteLine("En Passant is forced.");
                Console.WriteLine("Press enter to En Passant");
                Console.ReadLine();
                movePiece(square - 7, square);
                Globals.board[square + 1] = null;
                if (Globals.vcchoice == 2 && botPlaying) {Globals.playerColour = Globals.playerColour == 1 ? 2 : 1;}
                return true;
            }
            else if (square % 8 != 0 && Globals.playerColour == side && Globals.mDid(Globals.board[square - 1]) == "P" && ((pawn)Globals.board[square - 1]).passantAble)
            {
                Console.WriteLine("En Passant is forced.");
                Console.WriteLine("Press enter to En Passant");
                Console.ReadLine();
                movePiece(square - 9, square);
                Globals.board[square - 1] = null;
                if (Globals.vcchoice == 2 && botPlaying) {Globals.playerColour = Globals.playerColour == 1 ? 2 : 1;}
                return true;
            }
        }
        if (Globals.vcchoice == 2 && botPlaying) {Globals.playerColour = Globals.playerColour == 1 ? 2 : 1;}
        return false;
    }
    public override bool movePiece(int location, int currentPos)
    {
        bool taking = false; bool takingOutcome = true;
        //location is where the piece is going to, this method runs from the object that is moving
        if (Math.Abs(currentPos - location) == 16)
        {
            ((pawn)Globals.board[currentPos]).passantAble = true;
        }
        else
        {
            ((pawn)Globals.board[currentPos]).passantAble = false;
        }
        if (Globals.mDside(Globals.board[location]) != side && Globals.mDside(Globals.board[location]) != 0)
        {
            takingOutcome = takePiece(location);
            taking = true;
        }
        if ((location / 8 == 0 | location / 8 == 7) && identifier.ToLower() == "p" && takingOutcome)
        {
            //promotion
            Console.WriteLine("Your pawn's epic adventure has met it's mighty conclusion. Through it's trials \nand tribulations it has now specialised itself into a new piece and has \nthe skills necessary to pick one.");
            Console.WriteLine("Promote to...? Knight[N] Rook[R] Bishop[B] Queen[Q] Knook[o] Knishop[I] ???[?] \nExplosives[E] Corpse[C] Politician[P] Marching Band[M] Experience[X]");
            switch (Console.ReadLine().ToUpper())
            {
                default:
                    break;
                case "N":
                    KnightPromotion(location);
                    break;
                case "R":
                    RookPromotion(location);
                    break;
                case "B":
                    BishopPromotion(location);
                    break;
                case "Q":
                    QueenPromotion(location);
                    break;
                case "O":
                    KnookPromotion(location);
                    break;
                case "I":
                    KnishopPromotion(location);
                    break;
                case "?":
                    Random random = new Random();
                    switch (random.Next(1,6))
                    {
                        default:
                            break;
                        case 1:
                            KnightPromotion(location);
                            break;
                        case 2:
                            RookPromotion(location);
                            break;
                        case 3:
                            BishopPromotion(location);
                            break;
                        case 4:
                            QueenPromotion(location);
                            break;
                        case 5:
                            KnookPromotion(location);
                            break;
                        case 6:
                            KnishopPromotion(location);
                            break;
                    }

                    break;
                case "E":
                    ExplosivesPromotion(location);
                    break;
                case "C":
                    Console.WriteLine("Unfortunately these trials have worn on the pawn and it's not going to make it. \nYour pawn is now a corpse, gross. The nearby pieces sweep it away.");
                    Console.WriteLine("Press enter to continue");
                    Console.ReadLine();
                    break;
                case "P":
                    Console.WriteLine("Your newly gained politician realises that they're almost taking an active role in something and promptly flees the scene, likely to spread misinformation and hate in a bid to gain voters.\n(You lose your pawn, but maybe one day that politician will be of use?)");
                    Console.WriteLine("Press enter to continue");
                    Console.ReadLine();
                    break;
                case "M":
                    Console.WriteLine("From out of nowhere, a marching band appears, stomping across the board and taking your mighty pawn with them, all whilst playing a delightful tune");
                    Console.WriteLine("Press enter to continue");
                    Console.ReadLine();
                    break;
                case "X":
                    Console.WriteLine("You feel a sense of greater knowledge as you watch your beloved pawn vanish forever (+1 EXP) ((Note: exp and levels coming in a future update))");
                    Console.WriteLine("Press enter to continue");
                    Console.ReadLine();
                    break;
            }
        }
        else if (!taking)
        {
            Globals.board[location] = this;
        }
        return takingOutcome;
    }
    //promotions
    protected void KnightPromotion(int location)
    {
        Globals.board[location] = new knight(side);
    }
    protected void RookPromotion(int location)
    {
        Globals.board[location] = new rook(side);
    }
    protected void BishopPromotion(int location)
    {
        Globals.board[location] = new bishop(side);
    }
    protected void QueenPromotion(int location)
    {
        Globals.board[location] = new queen(side);
    }
    protected void KnookPromotion(int location)
    {
        Globals.board[location] = new knook(side);
    }
    protected void KnishopPromotion(int location)
    {
        Globals.board[location] = new knishop(side);
    }
    protected void ExplosivesPromotion(int location)
    {
        Console.WriteLine("Your pawn transforms into neatly layered explosives, ready to cause destruction.\n(Any adjacent pieces are going to be destroyed)");
        Console.WriteLine("Say any goodbyes then press enter to continue");
        Console.ReadLine();
        Console.WriteLine("5");
        Thread.Sleep(1000); Console.WriteLine("4");
        Thread.Sleep(1000); Console.WriteLine("3");
        Thread.Sleep(1000); Console.WriteLine("2");
        Thread.Sleep(1000); Console.WriteLine("1");
        Thread.Sleep(1000); Console.WriteLine("BOOM!");

        if (!(location - 8 < 0))
        {
            Globals.board[location - 8] = null; //up
            if (!(location % 8 == 0))
            {
                Globals.board[location - 9] = null; //up left
                Globals.board[location - 1] = null; //left
            }
            if (!(location % 8 == 7))
            {
                Globals.board[location - 7] = null; //up right
                Globals.board[location + 1] = null; //right
            }
        }
        else
        {
            Globals.board[location + 8] = null; //down
            if (!(location % 8 == 0))
            {
                Globals.board[location + 7] = null; //down left
                Globals.board[location - 1] = null; //left
            }
            if (!(location % 8 == 7))
            {
                Globals.board[location + 9] = null; //down right
                Globals.board[location + 1] = null; //right
            }
        }
    }
}