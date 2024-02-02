using System.Diagnostics.Contracts;
using System.Formats.Tar;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Threading;

namespace console_chess
{
    class bot
    {
        //Value piece move
        int playerColour = 1;
        bool gameOver = false;
        public static bool botPlaying = false;
        public object[] GlobalBoard = new object[64];
        private List<byte[]> allMoveValidations(object[] board, int start, int end) //runs move validation for every position on the board and adds it to a list
        {
            List<byte[]> list = new List<byte[]>();
            for (int i = start; i < end; i++)
            {
                //aaarg[0] is the position of the piece
                //aaarg[1] is the position of the place the piece wants to go to
                //aaarg[2] is what sides both the piece and destination are
                object[] parameters = new object[2];
                parameters[0] = i;
                parameters[1] = board;

                List<int> validmoves = Globals.validateMoves(board[i], parameters);

                //Console.Clear();
                for (int j = 0; j < validmoves.Count();j++)
                {
                    byte[] aaarg = new byte[3];
                    int item = validmoves[j];
                    if (Globals.mDside(board[i]) == playerColour)
                    {
                        //allmoves.Add(item);
                        //aaarg[0] = convStringToByteArray(Globals.mDid(board[i]) + i + Globals.mDside(board[i])); //position of piece
                        //convert string to byte array( id of the piece + number + side of the piece )
                        aaarg[0] = Convert.ToByte(i);
                        //aaarg[1] = convStringToByteArray(Globals.mDid(board[i]) + item.ToString() + Globals.mDside(board[i])); //destination of piece
                        aaarg[1] = Convert.ToByte(item);
                        aaarg[2] = convStringToByteArray(Globals.mDside(board[i]).ToString() + Globals.mDside(board[item]).ToString());
                        list.Add(aaarg);
                    }
                }
            }
            return list;
        }
        private byte convStringToByteArray(string str)
        {
            //REPUROSED now it splits the byte into two nibbles, the lhs stores data about the original piece, the rhs stores data about the destination

            //each byte stores both side and type of piece
            //e.g. black horsey would be 0001 0011, white bishop would be 0000 0100
            byte tmp = 0;
            // switch (str.Substring(0, 1).ToLower())
            // {
            //     default:break;
            //     //case "empty": tmp += 0; break;
            //     case "p": tmp += 1; break;
            //     case "r": tmp += 2; break;
            //     case "n": tmp += 3; break;
            //     case "b": tmp += 4; break; 
            //     case "q": tmp += 5; break;
            //     case "k": tmp += 6; break;
            //     //case "knook": tmp += 7; break;
            //     //case "knishop": tmp += 8; break;
            // }
            
            //lhs
            if (str.Substring(0,1) == "2") //black
            {
                tmp += 16;
            }

            //rhs
            if (str.EndsWith("2"))
            {
                tmp += 1;
            }
            else if (str.EndsWith("0"))
            {
                tmp += 8;
            }

            return tmp;
        }
        private object[] generateNewBoard(byte[] byteArray, byte[][] prevMoves)
        {
            // //object[] board = Globals.board;
            object[] board = new object[Globals.board.Length];
            for (int i = 0; i < Globals.board.Length; i++)
            {
                switch (Globals.mDid(Globals.board[i]).ToLower())
                {
                    default:
                        break;
                    case "p":
                        board[i] = new pawn((pawn)Globals.board[i], true);
                        break;
                    case "r":
                        board[i] = new rook((rook)Globals.board[i], true);
                        break;
                    case "n":
                        board[i] = new knight((knight)Globals.board[i], true);
                        break;
                    case "b":
                        board[i] = new bishop((bishop)Globals.board[i], true);
                        break;
                    case "q":
                        board[i] = new queen((queen)Globals.board[i], true);
                        break;
                    case "k":
                        board[i] = new king((king)Globals.board[i], true);
                        break;
                    case "i": //knishop
                        board[i] = new knishop((knishop)Globals.board[i], true);
                        break;
                    case "o": //knook
                        board[i] = new knook((knook)Globals.board[i], true);
                        break;
                }
            }

            if (prevMoves != null)
            {
                //prevMoves = prevMoves.Reverse().ToArray();
                foreach (byte[] item in prevMoves)
                {
                    if (item != null)
                    {
                        board[item[1]] = board[item[0]];
                        board[item[0]] = null;
                    }
                    if (Globals.mDname(board[item[1]]) == "Pawn") {((pawn)board[item[1]]).hasMoved = true;}
                    if ((item[1] / 8) * 8 == 0 && Globals.mDname(board[item[1]]) == "Pawn") {board[item[1]] = new queen(2);}
                }
            }

            if (Globals.mDid(board[byteArray[1]]) == "K")
            {
                gameOver = true;
            }
            else
            {
                gameOver = false;
            }
            if (Globals.mDid(board[byteArray[0]]) == "K")
            {
                if (((king)board[byteArray[0]]).castledleft)
                {
                board[byteArray[1] + 1] = board[(byteArray[0] / 8) * 8];
                board[(byteArray[0] / 8) * 8] = null;
                }
                else if (((king)board[byteArray[0]]).casltedright)
                {
                    board[byteArray[1] - 1] = board[(byteArray[0] / 8) * 8 + 7];
                    board[(byteArray[0] / 8) * 8 + 7] = null;
                }
            }
            board[byteArray[1]] = board[byteArray[0]];
            board[byteArray[0]] = null;
            return board;
        }
        public void minimaxinitialisaiton()
        {
            botPlaying = true;
            byte[][] prevMove = new byte[0][];
            int depth = 4;
            //Console.WriteLine("\n\n\n" + minimax(Globals.board, 4, 4, -1000, 1000, true, null, test));
            byte[] move = minimax(Globals.board, depth, depth, -1000, 1000, true, null, prevMove);
            if (Globals.mDid(Globals.board[move[1]]).ToUpper() == "K")
            {
                Globals.kingDead = 1;
            }
            if (Globals.mDid(Globals.board[move[0]]) == "K")
            {
                if (((king)Globals.board[move[0]]).castledleft)
                {
                    Globals.board[move[1] + 1] = Globals.board[(move[0] / 8) * 8];
                    Globals.board[(move[0] / 8) * 8] = null;
                }
                else if (((king)Globals.board[move[0]]).casltedright)
                {
                    Globals.board[move[1] - 1] = Globals.board[(move[0] / 8) * 8 + 7];
                    Globals.board[(move[0] / 8) * 8 + 7] = null;
                }
            }
            Globals.board[move[1]] = Globals.board[move[0]];
            Globals.board[move[0]] = null;
            if ((move[1] / 8) * 8 == 0 && Globals.mDname(Globals.board[move[1]]) == "Pawn") {Globals.board[move[1]] = new queen(2);}
            botPlaying = false;
        }
        private byte[] minimax(object[] position, int depth, int InitalDepth, int alpha, int beta, bool maxPlayer, byte[] moveData, byte[][] prevMove)
        {
            //system for keeping previous moves in a branch so new boards can be correctly generated
            byte[][] prevMoves = new byte[InitalDepth - depth][];
            for (int i = 0; i < prevMove.Length; i++)
            {
                prevMoves[i] = prevMove[i];
            }
            if (depth < InitalDepth) {prevMoves[InitalDepth - depth - 1] = moveData;}


            if (depth == 0 | gameOver)
            {
                //return value of position
                byte[] temp = new byte[1] {altAssignValue(position)};
                return moveData.Concat(temp).ToArray();
            }
            if (maxPlayer) //the bot / black
            {
                playerColour = 2; //sets the colour to black for move validation

                byte[] maxEval = new byte[4] {0, 0, 0, 0};
                //maxEval is the highest value move that has been calculated so far
                //first 3 values are move data, 4th is the important one, the value of the move.
                //The value is initially set to 0 which is intentionally a value that cannot be achieved in the game, the lowest possible value is 1.
                //We want the initial value to be effectively -∞ so the first move calculated will always be the new best move.

                //for every possible black move from the current board position
                foreach (var child in allMoveValidations(position, 0, 64))
                {
                    moveData = child; //copy over the child move to moveData

                    //int eval = minimax(position with next move (generate new board), depth - 1, alpha, beta, false)
                    //int eval = minimax(generateNewBoard(child, prevMoves), depth - 1, InitalDepth, alpha, beta, false, moveData, prevMoves);
                    byte[] eval = new byte[4] {moveData[0], moveData[1], moveData[2], minimax(generateNewBoard(child, prevMoves), depth - 1, InitalDepth, alpha, beta, false, moveData, prevMoves)[3]};
                    //eval is similar to maxEval in structure, with the first 3 values being the child move data
                    //However when setting the fourth value we chose to call minimax again, this time with the board being with the new child move (and all previous moves in the branch), the depth being 1 lower, the player being evaluated switching to white, and the child move plus previous moves being passed through.
                    //Eventually the 4th item will be set to the value of the move, bit complex to explain in comments.


                    //Compares which move is better, the one being evaluated or the current best move.
                    if (maxEval[3] < eval[3])
                    {
                        maxEval[0] = eval[0];
                        maxEval[1] = eval[1];
                        maxEval[2] = eval[2];
                        maxEval[3] = eval[3]; 
                    }

                    //Compares if the value of the move being evaluated is larger than alpha, and sets alpha to the value if it is
                    alpha = alpha > eval[3] ? alpha : eval[3]; //if alpha greater than value then alpha equals alpha, else (value is greater than alpha) then alpha equals value
                    
                    //if beta is less than or equal to alpha, then "prune" this branch as it will never be the best move.
                    if (beta <= alpha)
                    {
                        break;
                    }
                }
                return maxEval;
            }
            else //the player
            {
                playerColour = 1;
                byte[] minEval = new byte[4] {0, 0, 0, 255}; //255 is the largest number a bit can store and the value of the board caps at 228

                foreach (var child in allMoveValidations(position, 0, 64))
                {
                    moveData = child;
                    byte[] eval = new byte[4] {moveData[0], moveData[1], moveData[2], minimax(generateNewBoard(child, prevMoves), depth - 1, InitalDepth, alpha, beta, true, moveData, prevMoves)[3]};
                    
                    //minEval = min between minEval and eval
                    if (minEval[3] > eval[3])
                    {
                        minEval[0] = eval[0];
                        minEval[1] = eval[1];
                        minEval[2] = eval[2];
                        minEval[3] = eval[3]; 
                    }

                    //beta = min between beta and eval
                    beta = beta < eval[3] ? beta : eval[3];
                    if (beta <= alpha)
                    {
                        break;
                    }
                }
                return minEval;
            }
        }
        private byte altAssignValue(object[] board)
        {
            int totalSum = 120;
            for (int i = 0; i < board.Length; i++)
            {
                if (Globals.mDside(board[i]) == 1) //white
                {
                    totalSum -= Globals.mDvalue(board[i]);
                }
                else if (Globals.mDside(board[i]) == 2) //black
                {
                    totalSum += Globals.mDvalue(board[i]);
                    switch (i % 8)
                    {
                        default:
                            break;
                        case 0 or 7:
                            totalSum -= 1;
                            break;
                        case 1 or 6:
                            totalSum -= 0;
                            break;
                        case 2 or 5:
                            totalSum += 1;
                            break;
                        case 3 or 4:
                            totalSum += 2;
                            break;
                    }
                    if (i / 8 == 3 | i / 8 == 4)
                    {
                        totalSum += 1;
                    }
                }
            }
            return Convert.ToByte(totalSum);
        }
    }
}