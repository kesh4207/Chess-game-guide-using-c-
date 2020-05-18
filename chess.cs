using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame_
{

    class Board
    {
        const string Path = "";
        const string ChessBoardFile = Path + "ChessBoard.txt"; // text file used by the user to add the pieces .  Installer - edit to match desired file path.
        Pieces[,] GameBoard_ = new Pieces[8, 8]; // instantiation of the pieces on the board - ann array of piece objects
        char[,] GameBoard = new char[8, 8]; // used to read from the textile to indicate the different pieces on the board
        private int BlackSourceX; //row of the black king
        private int BlackSourceY; //column of the black king
        private int WhiteSourceX; // row of the white king
        private int WhiteSourceY; //column of the white king 
        Hashtable BlackPiecesRemaining = new Hashtable();// these are used for defensive programing, to ensure the user is not entering more pieces than available on the chess board
        Hashtable WhitePiecesRemaining = new Hashtable();
        // have to create separate white and black pieces as there would be a different amount of black and white pieces on the board.
        Hashtable BlackPawnPromotion = new Hashtable();

        Hashtable WhitePawnPromotion = new Hashtable();
        // when Promoting the pawn I needed a way of counting how many pieces were lost on each side. So the key in this case would be the piece and the  value could be the number of that kind of piece that's been removed.
        // As the pieces are initialised on the board, you would decrement the value of the key, as the key represents the number of pieces NOT on the board.
        //so if the value of the hash is greater than 0, that means the user can promote the pawn to that piece. This is because the value represents the number of that kind of pieces that are not on the board and the pawn can only be promote to any piece that is not on the board, except the king in this game
        // in the hash table the  key would be the char of that particular piece e.g. a 'q' means it’s a queen and the value would be the number of that kind of piece that has been removed
        // different amount of black and white pieces on the board, hence have as a separate hash table.



        public Board()
        {
            // these all set the initial amounts of values to the keys in the hash tables
            PiecesRemaining(BlackPawnPromotion, true); // different amount of white pieces on the board, hence have to create separate hash tables for black and white pieces
            PiecesRemaining(WhitePawnPromotion, false);
            InitialPieces(BlackPiecesRemaining, true);
            InitialPieces(WhitePiecesRemaining, false);


            LoadBoard(ChessBoardFile, ref GameBoard);




        }
        private void InitialPieces(Hashtable PiecesOnTheBoard, bool IsBlack)
        {
            // These are all the pieces required in a chess game. In total there are 16 pieces for each chess player
            PiecesRemaining(PiecesOnTheBoard, IsBlack); // adding the number of pieces for queen, rook, bishop, and knight
            PiecesOnTheBoard.Add(IsBlack == true ? 'k' : 'K', 1); // initially there is one king on the chessboard
            PiecesOnTheBoard.Add(IsBlack == true ? 'p' : 'P', 8);// initially there is 8 pawns on the chess board

        }



        private void PiecesRemaining(Hashtable PawnPromotion, bool IsBlack)
        {
            // these are all the pieces a pawn could potentially promote to, but later in the case switch statements, the value will be decremented if the character, indicated by the key, corresponds to a piece being instantiated on the board 

            PawnPromotion.Add((IsBlack == true ? 'q' : 'Q'), 1); //all the initial pieces on the board that a pawn could 'promote to'. only 1 queen on the board initially.
            PawnPromotion.Add((IsBlack == true ? 'r' : 'R'), 2); //e.g. there are 2 rook pieces initially on the chess board
            PawnPromotion.Add((IsBlack == true ? 'b' : 'B'), 2);
            PawnPromotion.Add((IsBlack == true ? 'n' : 'N'), 2);

        }

        private int ChoiceOfUser(Hashtable PawnPromotion, bool IsBlack)
        // this meets objective 5 as as the user chooses what to promote the piece to base on whats not on the board. Using a hash table means if there is already a queen on the board, then the pawn cannot promote to a queen as it is not a 'captured' piece.
        {
            if ((int)PawnPromotion[(IsBlack == true ? 'q' : 'Q')] > 0) // pieces remaining are greater than zero, than the user can use the piece to promote to pawn
            {
                Console.WriteLine("Please enter 1 if you would like to promote the pawn to a Queen?");


            }
            if ((int)PawnPromotion[(IsBlack == true ? 'r' : 'R')] > 0) // pieces remaining are greater than zero, than the user can use the piece to promote  a pawn 
            {
                Console.WriteLine("Please enter 2 if you would like to promote the pawn to a Rook?");


            }
            if ((int)PawnPromotion[(IsBlack == true ? 'b' : 'B')] > 0) // pieces remaining are greater than zero, than the user can use the piece to promote to pawn
            {
                Console.WriteLine("Please enter 3 if you would like to promote the Pawn to a Bishop?");


            }
            if ((int)PawnPromotion[(IsBlack == true ? 'n' : 'N')] > 0) // pieces remaining are greater than zero, than the user can use the piece to promote to pawn
            {
                Console.WriteLine(" Please enter 4 if you would like to promote the Pawn to a Knight?");



            }

            // if the value of the key (piece) is 0, then this would mean that all the available  pieces of that type  is on the board and hence the user cannot promote to it as its not a 'removed' piece.
            Console.WriteLine("Please enter 0 for neither");
            int Choice;

            while (!int.TryParse(Console.ReadLine(), out Choice) || Choice < 0 || Choice > 4) // validate the choice to make sure its an integer
                Console.Write("Please enter a valid Choice (Integer from 0 to 4): ");
            return Choice;




        }



        public void PawnPromotion()
        {

            for (int i = 0; i < 8; i++)
            {
                if (GameBoard_[0, i] != null)
                {
                    if (GameBoard_[0, i].GetName() == "pawn") // if a black piece as travelled to the end of the board
                    {
                        ChangingThePawn(ChoiceOfUser(BlackPawnPromotion, true), 0, i, true);  // promoting the pawn to another piece

                    }
                }
                if (GameBoard_[7, i] != null)
                {

                    if (GameBoard_[7, i].GetName() == "Pawn")
                    {
                        ChangingThePawn(ChoiceOfUser(WhitePawnPromotion, true), 0, i, false); // user chooses what they want the pawn to promote to base on what has been removed from the board

                    }
                }

            }




        }

        private void ChangingThePawn(int Choice, int SourceX, int SourceY, bool IsBlack)
        {
            // this meets objective 5 as it replaces the pawn with the promotion to another piece based on the users choice.
            switch (Choice)
            {
                case 1:
                    GameBoard_[SourceX, SourceY] = new Queen(IsBlack, IsBlack == true ? "queen" : "Queen"); // promotes the pawn to a queen on the board
                    break;
                case 2:
                    GameBoard_[SourceX, SourceY] = new Rook(IsBlack, IsBlack == true ? "rook" : "Rook"); //promotes the pawn to a rook on the board 
                    break;
                case 3:
                    GameBoard_[SourceX, SourceY] = new Rook(IsBlack, IsBlack == true ? "bishop" : "Bishop"); //promotes the pawn to a bishop on the board
                    break;
                case 4:
                    GameBoard_[SourceX, SourceY] = new Knight(IsBlack, IsBlack == true ? "knight" : "Knight"); // promotes the pawn to a knight on the board
                    break;

                default:
                    GameBoard_[SourceX, SourceY] = GameBoard_[SourceX, SourceY]; // if there is no promotion then the piece stays as a pawn 
                    break;

                // the bool IsBlack is used to indicate what colour the piece is as white pieces is denoted by an uppercase, whilst black is denoted by lowercase.






            }

        }




        private void DecrementTheValue(Hashtable PiecesOnTheBoard, char key)
        {
            // every time a piece is Instantiated  on the board, the value  would decrease by 1 as, the value represents pieces that are NOT on the board.
            //for e.g. if a pawn was intantated on the board,and the value was initially 8, its new value would be 7, which represents that there are 7 pawns not on the board
            // initially all the pieces on the board but as go through the board, the pieces that are not present on the board have been removed, hence what is left over is what has been removed from the board.
            int old = (int)PiecesOnTheBoard[key];
            PiecesOnTheBoard[key] = old - 1; // decrements the value by 1 for every piece of a specific type on the board


        }
        private void LoadBoard(string ChessBoard, ref char[,] GameBoard)
        {

            string Line = "";

            StreamReader ChessBoard_ = new StreamReader(ChessBoardFile);
















            for (int Row = 0; Row < 8; Row++)  // goes through the whole text file
            {
                try
                {
                    Line = ChessBoard_.ReadLine();
                }
                catch
                {

                    Console.WriteLine("The board and piece defilitions file could not be loaded.");
                    Console.WriteLine("Please check that the file " + Path + ChessBoardFile + " is installed");
                }
                for (int Column = 0; Column < 8; Column++)
                {


                    GameBoard[Row, Column] = Convert.ToChar((Line.Substring(Column, 1))); // sets a value to a coordinate, corresponding to a piece. e.g. at [1,0]= could be assigned the value 'p', which indicates its a black pawn.

                    SetUpBoard(ref GameBoard, Row, Column);



                }





            }




        }

        // this meets objective 2 as it allocating coordinates to a piece based on what the user has inputted on the board

        private void CorrectNoOfPieces(Hashtable PiecesOnBoard, char key)
        {
            int NumberOnBoard = (int)PiecesOnBoard[key]; // this finds the value of key, which represents pieces not on the board
            if (NumberOnBoard < 0) // if the value is -1, then that would mean an incorrect number of pieces have been instantiated  on the board
            {
                // For example if the user had entered two black kings, it's inital value would be 1, but now it would be -1 when the two king have been Instantiated, which would cause an error because they're can only be one black king on the board
                Console.WriteLine("Sorry, there is an incorrect number of pieces on the board, Please refill the board and try again. \n Press any key to continue");

                Console.ReadKey();
                Environment.Exit(0); // this exits the game when user enters a key, so they can refill out the board



            }

        }

        private void SetUpBoard(ref char[,] GameBoard, int SourceX, int SourceY)
        {

            // this meets objective 1 & 2 as I have allowed the user to differentiate the colour of a piece using lowercase to identify a black piece and uppercase to indicate a white ppiece


            switch (GameBoard[SourceX, SourceY])
            {
                case 'p':
                    GameBoard_[SourceX, SourceY] = new Pawn(true, "pawn"); // this actually initialise the piece on the Gameboard_, so uses the assigned coordinates from the string Gameboard  to allocate a piece type on the actual board
                    DecrementTheValue(BlackPiecesRemaining, 'p'); // reducing the value of the pawn
                    CorrectNoOfPieces(BlackPiecesRemaining, 'p'); // checks to make sure that there is still a correct number of pieces on the board, each time a pawn is instansiated
                    break;
                case 'P':
                    GameBoard_[SourceX, SourceY] = new Pawn(false, "Pawn");
                    DecrementTheValue(WhitePiecesRemaining, 'P');
                    CorrectNoOfPieces(WhitePiecesRemaining, 'P');
                    break;
                case 'k':
                    GameBoard_[SourceX, SourceY] = new King(true, "king"); // this would be a black king 
                    DecrementTheValue(BlackPiecesRemaining, 'k');
                    CorrectNoOfPieces(BlackPiecesRemaining, 'k');
                    BlackSourceX = SourceX;
                    BlackSourceY = SourceY;
                    break;

                case 'K':
                    GameBoard_[SourceX, SourceY] = new King(false, "King"); //this would be a white king
                    DecrementTheValue(WhitePiecesRemaining, 'K');
                    CorrectNoOfPieces(WhitePiecesRemaining, 'K');
                    WhiteSourceX = SourceX;
                    WhiteSourceY = SourceY;
                    break;
                case 'r':
                    GameBoard_[SourceX, SourceY] = new Rook(true, "rook"); // this would be a black rook
                    DecrementTheValue(BlackPiecesRemaining, 'r');
                    CorrectNoOfPieces(BlackPiecesRemaining, 'r');
                    DecrementTheValue(BlackPawnPromotion, 'r'); // if the piece is already on the board than a pawn cannot 'promote' to it.

                    break;
                case 'R':
                    GameBoard_[SourceX, SourceY] = new Rook(false, "Rook"); // this would be a white rook...
                    DecrementTheValue(WhitePiecesRemaining, 'R');
                    CorrectNoOfPieces(WhitePiecesRemaining, 'R');
                    DecrementTheValue(WhitePawnPromotion, 'R'); // if a rook is on the board, then -1 from the value as the value represents how many pieces of different types have been removed from the board
                    break;
                case 'n':
                    GameBoard_[SourceX, SourceY] = new Knight(true, "knight"); // this is a black knight
                    DecrementTheValue(BlackPiecesRemaining, 'n');
                    CorrectNoOfPieces(BlackPiecesRemaining, 'n');
                    DecrementTheValue(BlackPawnPromotion, 'n');
                    break;
                case 'N':
                    GameBoard_[SourceX, SourceY] = new Knight(false, "Knight");  // uppercase indicates white pieces while lower case indicates black pieces
                    DecrementTheValue(WhitePiecesRemaining, 'N');
                    CorrectNoOfPieces(WhitePiecesRemaining, 'N');
                    DecrementTheValue(WhitePawnPromotion, 'N'); // store separate hashtable dependant on the piece colour as could be a  different number of black and white pieces remaining on the board
                    break;
                case 'B':
                    GameBoard_[SourceX, SourceY] = new Bishop(false, "Bishop");
                    DecrementTheValue(WhitePiecesRemaining, 'B');
                    CorrectNoOfPieces(WhitePiecesRemaining, 'B');
                    DecrementTheValue(WhitePawnPromotion, 'B');
                    break;
                case 'b':
                    GameBoard_[SourceX, SourceY] = new Bishop(true, "bishop");
                    DecrementTheValue(BlackPiecesRemaining, 'b');
                    CorrectNoOfPieces(BlackPiecesRemaining, 'b');
                    DecrementTheValue(BlackPawnPromotion, 'b');
                    break;
                case 'Q':
                    GameBoard_[SourceX, SourceY] = new Queen(false, "Queen");
                    DecrementTheValue(WhitePiecesRemaining, 'Q');
                    CorrectNoOfPieces(WhitePiecesRemaining, 'Q');
                    DecrementTheValue(WhitePawnPromotion, 'Q');
                    break;
                case 'q':

                    GameBoard_[SourceX, SourceY] = new Queen(true, "queen");
                    DecrementTheValue(BlackPiecesRemaining, 'q');
                    CorrectNoOfPieces(BlackPiecesRemaining, 'q');
                    DecrementTheValue(BlackPawnPromotion, 'q');
                    break;
                default:
                    GameBoard_[SourceX, SourceY] = null;

                    break;

            }
        }


        public void MovePiece(bool IsBlack, int SourceX, int SourceY, int DestX, int DestY)
        {

            if (GameBoard_[SourceX, SourceY] != null)
            {

                if (GameBoard_[SourceX, SourceY].AvailableMove(SourceX, SourceY, DestX, DestY, ref GameBoard_) == true) // checks if the move is valid by seeing if the movement is within the legal movements of the pieces
                {

                    GameBoard_[DestX, DestY] = GameBoard_[SourceX, SourceY];
                    GameBoard_[SourceX, SourceY] = null;

                }
                else  // if it is not  a legal move
                {
                    Console.WriteLine("Move is not valid");
                }
            }
        }


        public void check() // this meets objective 6
        {
            for (int i = 0; i < 8; i++) // this for loop is used to go through the entire board, checking to see if any pieces could capture the king.
            {
                for (int j = 0; j < 8; j++)
                {
                    // condition to check its not an empty space black or white king as a king can put itself in check, one of the pieces have to put the king in check
                    if ((GameBoard_[i, j] != null) && (GameBoard_[i, j] != GameBoard_[BlackSourceX, BlackSourceY]) && (GameBoard_[i, j] != GameBoard_[WhiteSourceX, WhiteSourceY]))
                    {
                        if (GameBoard_[i, j].GetIsblack()) // if black piece 
                        {
                            bool incheck = CheckForCheck(i, j, WhiteSourceX, WhiteSourceY);// check if white king is in check 
                            if (incheck) // if the white king is in check 
                            {
                                Console.WriteLine("white is in check by the piece at {0}, {1}", i, j); // gives the coordinates of the piece puting the king in check, hence meets objective 5
                                incheck = incheck == true ? incheck = (CanThePiecesHelp(i, j, true, WhiteSourceX, WhiteSourceY)) : false; // if its still in check even if the other pieces 'try' to help, then we check if its in checkmate

                                if (incheck) // if its stll incheck and no pieces can help
                                {
                                    bool IsInCheckmate = CheckMate(WhiteSourceX, WhiteSourceY, false); // once in check, its now checking it its in checkmate, hence meeting objective 7
                                    if (IsInCheckmate)
                                    {
                                        Console.WriteLine("White is in Checkmate"); // able to identify which side is in checkmate
                                        break;
                                    }
                                }




                            }
                        }
                        else
                        {
                            bool incheck = CheckForCheck(i, j, BlackSourceX, BlackSourceY); //check if black king is in check
                            if (incheck)
                            {
                                Console.WriteLine("black is in check by the piece at {0}, {1}", i, j);
                                incheck = incheck == true ? incheck = (CanThePiecesHelp(i, j, false, BlackSourceX, BlackSourceY)) : false; // if its still in check even if the other pieces 'try' to help, then we check if its in checkmate
                                if (incheck)
                                {

                                    bool IsInCheckmate = CheckMate(BlackSourceX, BlackSourceY, true);// then check if its in checkmate or can move out of check by moving the king to another one of its legal moves
                                    if (IsInCheckmate)
                                    {

                                        Console.WriteLine("Black is in Checkmate");
                                        break;
                                    }

                                }


                            }


                        }

                    }

                }

            }

        }

        private bool CheckForCheck(int PieceX, int PieceY, int kingX, int kingY)
        {
            if (GameBoard_[PieceX, PieceY].AvailableMove(PieceX, PieceY, kingX, kingY, ref GameBoard_)) // if the kings legal move is within an enemy piece, then it is in check.
            { // this function  allows the board class to use the pieces  class, by passing th coordinates of the 'enemy' piece that could attack the king  to find if the new coordinates i.e. the kings coordinates could be a legal move of that enemy piece, hence it could capture a king
                // this is able to correctly identify check, hence meeting objective 6
                return true;
            }
            return false;
        }






        private bool CheckMate(int KingX, int KingY, bool IsBlack) // this meets objective 7, as I go through the whole chessboard to see if an enemy piece interferes with the king
        {
            bool incheck1 = false, incheck2 = false, incheck3 = false, incheck4 = false, incheck5 = false, incheck6 = false, incheck7 = false, incheck8 = false; // these represent all the different checks you have to do in the 8 the king could move, as you basically producing another check. i Have to make separate variables for the checks as if I made iy all one single variable, it could return true from one of the conditions, making it in 'checkmate'
            for (int SourceX = 0; SourceX < 8; SourceX++)
            {
                for (int SourceY = 0; SourceY < 8; SourceY++)
                {

                    if ((GameBoard_[SourceX, SourceY] != null) && (GameBoard_[SourceX, SourceY] != GameBoard_[KingX, KingY]) && (GameBoard_[SourceX, SourceY].GetIsblack() != GameBoard_[KingX, KingY].GetIsblack()))
                    {
                        GameBoard_[KingX, KingY] = null; //this destroys the king as if im trying to check where the new king could move to, other pieces may not classify the move as a legal move  because the 'old' king is blocking its movement. e.g. the bishop cannot jump over pieces, but if the king was to move diagonally down, it could still capture the king in the new position

                        bool Checking1 = (KingX == 7) ? false : CheckingForCheck(SourceX, SourceY, KingX + 1, KingY, IsBlack);
                        incheck1 = incheck1 || Checking1;// i'm 'OR' ing the old value of incheck1 with the new value as if it goes through the loop and another piece doesn't put it in check then that same in check is set to false so at the end it won’t be able to tell if it’s in checkmate because incheck1, incheck2 is constantly being changed by other pieces
                        // hence I OR the values so  it won't be affected when it is OR with a false, when its initially set to true  but when you OR it with true it will persist.
                        bool Checking2 = (KingX == 0) ? false : CheckingForCheck(SourceX, SourceY, KingX - 1, KingY, IsBlack);
                        incheck2 = incheck2 || Checking2;

                        bool Checking3 = (KingY == 7) ? false : CheckingForCheck(SourceX, SourceY, KingX, KingY + 1, IsBlack); // im also checking if the king is on the side or end as, it wouldn't be able to move if its on the edge of the board, so it wuldn't need to check if other pieces can hel, because the king cannot move of the board
                        incheck3 = incheck3 || Checking3;

                        bool Checking4 = (KingY == 0) ? false : CheckingForCheck(SourceX, SourceY, KingX, KingY - 1, IsBlack);
                        incheck4 = incheck4 || Checking4;

                        bool Checking5 = ((KingX == 7) || (KingY == 0)) ? false : CheckingForCheck(SourceX, SourceY, KingX + 1, KingY - 1, IsBlack);
                        incheck5 = incheck5 || Checking5;

                        bool Checking6 = ((KingX == 7) || (KingY == 7)) ? false : CheckingForCheck(SourceX, SourceY, KingX + 1, KingY + 1, IsBlack);
                        incheck6 = incheck6 || Checking6;

                        bool Checking7 = (((KingX == 0) || KingY == 7)) ? false : CheckingForCheck(SourceX, SourceY, KingX - 1, KingY + 1, IsBlack);
                        incheck7 = incheck7 || Checking7;

                        bool Checking8 = (KingX == 0) || (KingY == 0) ? false : CheckingForCheck(SourceX, SourceY, KingX - 1, KingY - 1, IsBlack);
                        incheck8 = incheck8 || Checking8;

                        if (incheck1 & incheck2 & incheck3 & incheck4 & incheck5 & incheck6 & incheck7 & incheck8) // if its incheck after every mapped position that the king moves 
                        {
                            return true; // // this meets objective 7 as it lists all the potential moves a king can make and checks if its in check, and indicates if its in checkmate by all the inchecks being true



                        }
                        GameBoard_[KingX, KingY] = new King(IsBlack, IsBlack == true ? "king" : "King");// puting the 'destroyed' king back on the board




                    }



                }

            }
            return false;
        }


        private bool CheckingForCheck(int SourceX, int SourceY, int KingX, int KingY, bool IsBlack)
        {
            // this meets objective 5, because it only ensures check once no other pieces on the board are available to help
            bool InCheck;

            InCheck = CheckForCheck(SourceX, SourceY, KingX, KingY);// checking if a piece could move to the coordinates  of king, hence it would be able to put that king in check 
            if (InCheck) //if it is in check 
            {

                bool WeCanHelp = CanThePiecesHelp(SourceX, SourceY, IsBlack, KingX, KingY);  // checking if other pieces can help as it would then no longer be in check
                if (WeCanHelp)
                {

                    return false;
                }

                else
                {
                    return true;

                }





            }

            return false;
        }

        private bool CanThePiecesHelp(int SourceX, int SourceY, bool IsBlack, int KingX, int KingY)
        //  this meets objective 8 as it lists all the moves an opponent could make to protect its pieces as well as listing all the possible moves the player could make in the chess game
        // gives the coordinates of the piece puting the king in danger 
        {
            // if that piece that is putting the king in check can be killed by another piece then 'a piece can help' 

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (GameBoard_[i, j] != null && GameBoard_[i, j] != GameBoard_[KingX, KingY] && GameBoard_[i, j] != GameBoard_[BlackSourceX, BlackSourceY] && GameBoard_[i, j] != GameBoard_[WhiteSourceX, WhiteSourceY])
                    {


                        if (GameBoard_[i, j].GetIsblack() != IsBlack) // if space is empty and its an 'enemy' piece 
                        {
                            bool WeCanHelp = (GameBoard_[i, j].AvailableMove(i, j, SourceX, SourceY, ref GameBoard_));// if the pieces can be captured by an enemy piece 
                            if (WeCanHelp) // pieces can help the king move out of check
                            {

                                Console.WriteLine(" the {0} piece at {1},{2} can move to {3},{4} and capture the {5}", GameBoard_[i, j].GetName(), i, j, SourceX, SourceY, GameBoard_[SourceX, SourceY].GetName());
                                Console.WriteLine("The {0} king can move to or stay at {1},{2}", IsBlack == false ? "White" : "Black", KingX, KingY); // if its a black piece then it would be puting the white King in danger & vice versa
                                return true; // hence not in check

                                // through this method of going through all the pieces on the board, i'm able to tell the user all the piecesit could be available to move to, meeting objective 8

                            }


                        }

                    }
                }
            }

            return false; // pieces cannot help the king move out of check
        }






        public void PrintBoard()
        {
            // Displays the 8x8 chessBoard, meeting objective 3
            for (int i = 0; i < 8; i++)
            {
                Console.Write("  " + i + " "); // prints out the numbers on the top 
            }
            Console.WriteLine();



            for (int i = 0; i < 8; i++)
            {
                Console.Write(i + "|"); // prints out the row of numbers
                for (int j = 0; j < 8; j++)
                {
                    if (GameBoard_[i, j] != null) // not every coordinate is allocated a piece
                    {
                        Console.Write(GameBoard_[i, j].GetName().Substring(0, 2) + " |"); // show particular piece at that spot
                    }
                    else
                    {
                        Console.Write("xx |");
                    }
                    if (j == 9)
                    {
                        Console.Write(" | "); // end of the board
                    }


                }
                Console.WriteLine();
            }
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame_
{
    class Pieces
    {
        //inheritance allow to share code between classes
        //public or private refers scoping of variables/  if private than can only be  modified within class but these need to be inherited

        private bool isBlack;

        private string Name;
        public Pieces(bool _IsBlack, string _Name) //since you are inheriting from Pieces, you have to call a base constructor. Since you explicitly defined the  constructor to require (bool,string) now you need to pass that up the chain.
        {
            this.isBlack = _IsBlack;
            this.Name = _Name;
        }


        public string GetName()
        {


            return Name;
        }
        public bool GetIsblack()
        {
            return isBlack;
        }
        public virtual bool AvailableMove(int SourceX, int SourceY, int DestX, int DestY, ref Pieces[,] GameBoard_)
        {
            return false;

            // wont be able to call availabe move directly/ pawn is a piece have to ensure that avilabe move in piece class and do dynamic linking to get to pawn class
        }
        public bool MoveStraight(int SourceX, int SourceY, int DestX, int DestY, ref Pieces[,] GameBoard_) // piece moving straight in a particular direction any number of squares up to 7
        {
            //this meets objective 4 as the piece check the coordinates if the new destination to make sure its a legal move.
            Boolean isBlack = GetIsblack();
            int x = SourceX;
            int y = SourceY;
            if ((DestX > SourceX) && (SourceY == DestY)) // if the rook is moving down. e.g. if the  rook  is at position 5 & moves to position 8. then the destinaion will be greater than the original position
            {

                while (DestX > x)  // while the destinaion is greater then original piece
                {

                    if ((GameBoard_[x, DestY] != null) && (x != SourceX)) //checking if there is a piece in the way that could block rook movement.
                    {

                        return false; // if there is a piece in the way blocking the rook path

                    }

                    x++; // keep moving down the board 
                }

                return true;
            }








            else if ((DestX < SourceX) && (SourceY == DestY))//bishop is moving up
            {

                while (DestX < x)
                {
                    if ((GameBoard_[x, DestY] != null) && (x != SourceX)) // if space is not empty i.e. occupied by another piece 
                    {
                        return false; // bishop is being blocked by a piece

                    }

                    x--; // bishop is moving up the board so its final x-coordinate will get smaller. e.g. if the bishop is at position 5 and moves to position 2, then its final position goes down.

                }

                return true;





            }
            // if the dest Y is less than Source Y, than bishop has to be moving to the left  of the board i.e. if bishop is in column 5 but moves to 4. then 4 is less than 5
            else if ((DestY < SourceY) && (SourceX == DestX))// if the bishop is moving to the left 
            {


                while (DestY < y)
                {
                    if (GameBoard_[DestX, y] != null && (y != SourceY)) // if it is not empty
                    {
                        return false; // bishop is being blocked by another piece

                    }

                    y--;

                }

                return true;
            }
            else if ((DestY > SourceY) && (SourceX == DestX)) //bishop is moving to the right of the gameboard
            {
                while (DestY > y)
                {
                    if (GameBoard_[DestX, y] != null && (y != SourceY)) //if it not empty
                    {
                        return false; //bishop is being blocked by another piece

                    }

                    y++; //keep incrementing the moves to find if there are any pieces obstructing the movement

                }

                return true;

            }

            return false;
        }

        public bool MoveDiagonally(int SourceX, int SourceY, int DestX, int DestY, ref Pieces[,] GameBoard_)
        {
            int x = SourceX;
            int y = SourceY;

            //this meets objective 4 as the piece check the coordinates if the new destination to make sure its a legal move.
            Boolean isBlack = GetIsblack();
            if (Math.Abs(SourceX - DestX) == Math.Abs(SourceY - DestY)) // for a bishop to move diagonally it would have to travel the same distance vertically, as well as the distance horizontally in any direction
            {
                if (DestX < SourceX) // bishop is moving up 
                {
                    if (DestY > SourceY) // if bishop is moving to the right
                    {
                        while (DestY > y) // moving to the right 
                        {
                            if (GameBoard_[x, y] != null && x != SourceX && y != SourceY) // if the space is not empty then move is not permitted as piece cannot 'jump' over pieces
                            {
                                return false;
                            }
                            x--;
                            y++;


                        }

                    }




                    else if (DestY < SourceY)  // moving to the left
                    {
                        while (DestY < y)
                        {
                            if (GameBoard_[x, y] != null && x != SourceX && y != SourceY)
                            {
                                return false;
                            }
                            x--;
                            y--;
                        }

                    }
                }

                else if (DestX > SourceX) // bishop is moving down 
                {
                    if (DestY > SourceY) // if bishop is moving to the right
                    {
                        while (DestY > y) // moving to the right 
                        {
                            if (GameBoard_[x, y] != null && x != SourceX && y != SourceY)
                            {
                                return false;
                            }
                            x++;
                            y++;


                        }

                    }

                    else if (DestY < SourceY)  // moving to the left
                    {
                        while (DestY < y)
                        {
                            if (GameBoard_[x, y] != null && x != SourceX && y != SourceY) // if the piece is not being blocked by another piece
                            {
                                return false;
                            }
                            x++;
                            y--; // increment the pieces to check to ensure each square up to its destination is not being blocked by another piece.
                        }

                    }
                }
                return true;








            }

            return false;

        }

        //this meets objective 4 as the piece are unable to capture a piece that is on its own side as this would not be a valid move

        public bool CaptureAPiece(int DestX, int DestY, ref Pieces[,] GameBoard_)
        {
            if ((isBlack) && (GameBoard_[DestX, DestY] != null && GameBoard_[DestX, DestY].GetIsblack()))
            {
                return false; // if its a black piece and wants to capture a black piece i.e. not permitted.
            }

            else if ((isBlack == false) && (GameBoard_[DestX, DestY] != null && GameBoard_[DestX, DestY].GetIsblack() == false))
            {
                return false; // if it is a white piece and wants to capture a white piece i.e. not pertmitted


            }
            return true; // able to capture an enemy piece
        }


    }

    class Pawn : Pieces
    {
        public Pawn(bool _IsBlack, string _Name)
            : base(_IsBlack, _Name)
        {
        }


        public override Boolean AvailableMove(int SourceX, int SourceY, int DestX, int DestY, ref Pieces[,] GameBoard_) // overrides the Available move method as would be different to every piece
        {
            //white piece
            int x = SourceX;

            Boolean isBlack = GetIsblack();

            //this meets objective 4 as the piece check the coordinates if the new destination to make sure its a legal move.


            if (isBlack)

            // if pawn moves diagnolly to capture enemy piece
            {
                if (SourceX > 0 && SourceY < 7)
                {
                    if (GameBoard_[SourceX - 1, SourceY + 1] != null && GameBoard_[SourceX - 1, SourceY + 1].GetIsblack() == false)
                    {
                        if (GameBoard_[SourceX - 1, SourceY + 1] == GameBoard_[DestX, DestY])
                        {
                            return true;// if space is not empty and the piece is a white piece
                        }
                    }
                }
                if (SourceX > 0 && SourceY > 0)
                {
                    if (GameBoard_[SourceX - 1, SourceY - 1] != null && GameBoard_[SourceX - 1, SourceY - 1].GetIsblack() == false)
                    {
                        if (GameBoard_[SourceX - 1, SourceY - 1] == GameBoard_[DestX, DestY])
                        {

                            return true;
                        }
                    }
                }


                else if (SourceY == DestY && ((DestX - SourceX) == -1)) // if a black piece where to move up then its final  position would decrease by 1. e.g. black piece at row 6, moves up to row 5. then 5-6=-1. 
                {
                    if (GameBoard_[DestX, DestY] == null) // if the destination is empty it wants to move infrnt
                    {


                        return true; // then it can move


                    }
                    return false; //otherwise it is being blocked by a piece.
                }
            }
            else
            {
                //white piece
                if (SourceX < 7 && SourceY < 7)
                {
                    if (GameBoard_[SourceX + 1, SourceY + 1] != null && GameBoard_[SourceX + 1, SourceY + 1].GetIsblack() == true)
                    {
                        if (GameBoard_[SourceX + 1, SourceY + 1] == GameBoard_[DestX, DestY])
                        {
                            return true;// if space is not empty and the piece is a  black   piece
                        }
                    }
                }
                if (SourceX < 7 && SourceY > 0)
                {

                    if (GameBoard_[SourceX + 1, SourceY - 1] != null && GameBoard_[SourceX + 1, SourceY - 1].GetIsblack() == true)
                    {
                        if (GameBoard_[SourceX + 1, SourceY - 1] == GameBoard_[DestX, DestY])
                        {
                            return true; //if space is not empty and the piece is a black   piece
                        }
                    }
                }


                else if (SourceY == DestY && ((DestX - SourceX) == 1))//e.g. white piece at row 5 and movs down to row 6. then difference would be 6-5=1
                {


                    if (GameBoard_[DestX, DestY] == null) // if the destination is empty i.e. the pawn is not being blocked by a piece in front of it 
                    {
                        return true; //then it can move

                    }
                    return false;
                }

            }



            return false;





        }






    }

    class Rook : Pieces
    {
        public Rook(bool _IsBlack, string _Name)
            : base(_IsBlack, _Name)
        {
        }
        //this meets objective 4 as the piece check the coordinates if the new destination to make sure its a legal move.

        public override Boolean AvailableMove(int SourceX, int SourceY, int DestX, int DestY, ref Pieces[,] GameBoard_)
        {
            bool Straight = MoveStraight(SourceX, SourceY, DestX, DestY, ref GameBoard_); // rook can move straight any number of squares up to 7 in particular direction
            bool PieceCaptured = CaptureAPiece(DestX, DestY, ref GameBoard_);


            if (Straight) // if the move is permitted
            {
                if (PieceCaptured)
                {
                    return true;
                }
                return false;
            }
            return false; // else move is not within the rook's legal moves
        }




    }

    class Queen : Pieces
    {

        public Queen(bool _IsBlack, string _Name)
            : base(_IsBlack, _Name)
        {
        }
        // Combination of Rook & Bishop hence can use the functions for moving straight and diagonal from the piece class

        public override Boolean AvailableMove(int SourceX, int SourceY, int DestX, int DestY, ref Pieces[,] GameBoard_)
        {

            //this meets objective 4 as the piece check the coordinates if the new destination to make sure its a legal move.

            bool Diagonal = MoveDiagonally(SourceX, SourceY, DestX, DestY, ref GameBoard_);
            bool Straight = MoveStraight(SourceX, SourceY, DestX, DestY, ref GameBoard_);
            bool PieceCaptured = CaptureAPiece(DestX, DestY, ref GameBoard_);
            if (Straight)   //can either move straight or diagonally 
            {
                //if move is permitted
                if (PieceCaptured)
                {
                    return true;
                }
                return false;

            }
            else if (Diagonal)
            {
                if (PieceCaptured)
                {
                    return true;
                }
                return false;

            }
            return false; //if move is not within the legal movements

        }




    }

    class Knight : Pieces //only piece that can jump over 
    {
        public Knight(bool _IsBlack, string _Name)
            : base(_IsBlack, _Name)
        {
        }
        public override Boolean AvailableMove(int SourceX, int SourceY, int DestX, int DestY, ref Pieces[,] GameBoard_)// overrides the Available move method as would be different to every piece
        //this meets objective 4 as the piece check the coordinates if the new destination to make sure its a legal move.
        {
            //if white knight 
            Boolean isBlack = GetIsblack();
            if (Math.Abs(SourceX - DestX) == 2 && Math.Abs(SourceY - DestY) == 1 || Math.Abs(SourceX - DestX) == 1 && Math.Abs(SourceY - DestY) == 2) //the  piece move in an L shape in any direction, so it could either move 2 squares to the left or right and one square up or in the other direction
            {
                // no while loop needed as don't need to check if it can 'jump' over a piece

                bool PieceCaptured = CaptureAPiece(DestX, DestY, ref GameBoard_);

                if (PieceCaptured) // if it is able to capture an 'enemy piece'
                {
                    return true;
                }
                return false; // not able to capture a piece, as this could be a piece on its own side.

            }
            return false;

        }



    }

    class King : Pieces
    {

        public King(bool _IsBlack, string _Name)
            : base(_IsBlack, _Name)
        {
        }

        //this meets objective 4 as the piece check the coordinates if the new destination to make sure its a legal move.

        public override Boolean AvailableMove(int SourceX, int SourceY, int DestX, int DestY, ref Pieces[,] GameBoard_)// overrides the Available move method as would be different to every piece
        {
            Boolean isBlack = GetIsblack();
            if (Math.Abs(SourceX - DestX) == 1 || Math.Abs(SourceY - DestY) == 1) //can move one square in any direction
            {

                bool PieceCaptured = CaptureAPiece(DestX, DestY, ref GameBoard_); // validation to ensure its not capturing its own piece

                if (PieceCaptured)
                {
                    return true;
                }
                return false;

            }


            return false;

        }



    }

    class Bishop : Pieces
    {
        public Bishop(bool _IsBlack, string _Name)
            : base(_IsBlack, _Name)
        {
        }
        public override Boolean AvailableMove(int SourceX, int SourceY, int DestX, int DestY, ref Pieces[,] GameBoard_)
        {

            //this meets objective 4 as the piece check the coordinates if the new destination to make sure its a legal move.

            bool Diagonal = MoveDiagonally(SourceX, SourceY, DestX, DestY, ref GameBoard_);
            bool PieceCaptured = CaptureAPiece(DestX, DestY, ref GameBoard_);


            if (Diagonal) //if move is permitted
            {
                if (PieceCaptured)
                {
                    return true;
                }
                return false;
            }
            return false;

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame_
{
    class Program
    {
        static void Main(string[] args)
        {
            Board GameBoard = new Board();

            GameBoard.PrintBoard();
            GameBoard.PawnPromotion();
            GameBoard.PrintBoard();
            GameBoard.check();

            Console.ReadLine();


        }
    }
}


