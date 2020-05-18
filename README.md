# Chess-game-guide-using-c-

## Analysis of the success Criteria:

This illustration (Amin, 2018) below demonstrates the enormous amount of possible chess moves, suggesting it would be extremely difficult to be cracked through methods of permutation and requires a huge amount of computation due to the to the almost exponential nature of the game.  The diagram shows that from the start of the game, each of the white player’s possible moves is mapped to each black player, who then has a possible 20 moves, and this continues, leading it grow at into a ‘exponential explosion’. The best way to think about a game tree is as a ‘theoretical construct’, it cannot be put in terms of real-word quantities. The computer may be able to calculate all the positions but the difficultly is in determining from all the position, which is the best. From the start of the game each player has 20 possible moves, but this grows by a huge amount as the game progresses. For instance, then at level 2, there is 400 possible moves for black, depending on what white does to level 4 where there is 160,000 for black. From level 4, for all the possible positions 
‘the computer would need to evaluate 10¹²⁰ possible moves. This   would have to be kept track of.
Also, the development of a chess game is not just dependent on the individual moves but whether it is a tactical or beneficial move and the expertise levels of the player have a huge influence on the development of the game, which almost suggests there is an almost infinite amount of unique possibilities, which is highly dependent on the individual. I must make sure to remember that chess is a strategy game and try to cater to this notion. My user considers themselves at a moderate level and finds difficulty in getting themselves out of check. From my research, I have concluded It would be impractical and require a huge amount of processing power to run a full-time chess game, but rather a snapshot of the game, to determine whether once my user is in check, they are then able to get out of check through usage of the king and other pieces.
![chess image](https://cdn.hswstatic.com/gif/chess1.gif)

<a href="https://imgbb.com/"><img src="https://i.ibb.co/CbmH218/img1.png" alt="img1" border="0"></a>

## Chess Design
<a href="https://imgbb.com/"><img src="https://i.ibb.co/3RNKtXQ/img2.png" alt="img2" border="0"></a>

This appears to be quite simple chessboard, but it’s still requires a lot of detail. For Instance, the pieces are being denoted by the first letter of the actual piece’s name. The black and white pieces are being separated using uppercase and lowercase characters. The method behind instantiating the pieces on the chessboard are quite difficult and board is able to use the coordinates of the pieces to predict movements from other pieces and how they could be a potential harm to the king. It also must be able to read through an instantiated game board and represent empty spaces within the board. 
## Flow Chart Representation
<a href="https://imgbb.com/"><img src="https://i.ibb.co/sRv55QR/img3.png" alt="img3" border="0"></a>

The basic principles of my chess game is to find if the user is in check & find if the pieces can help in that position, if the pieces cannot help, then the king has to move to a new legal position and repeat this process again until the king has moved all its possible moves. Now this is the fundamentals for a checkmate engine, because if the king is in check in all these possible position and no other pieces can help, then it is in checkmate.
## Class diagram
<a href="https://imgbb.com/"><img src="https://i.ibb.co/wBfz3WC/img4.png" alt="img4" border="0"></a>

I decided to use inheritance in my Chess model because all the pieces share similar behaviour in the sense they have only a set of required legal moves that they are to move on the board, but they must be validated first so that the move is allowed. However, how each piece moves on the board is dependent on the type of piece. For example, a pawn only moves one square forward whilst a knight moves in an L shape, so I would have to produce an algorithm for the separate pieces. I have shown the dependency the board has on the pieces class, as its only able to calculate if a piece is in check by accessing the piece class to check if ‘enemy’ pieces (i.e. not on the king’s side) are able to move to the position allocated by the king. I was originally, going to include the chessboard in the piece class, but I realised that the pieces don’t need to know where they are on the board, just if where there moving is accessible. The board is recording how the piece moves without knowing what constitutes a ‘legal’ move.
I’ve already started planning the different functions required for the piece class. When mapping out how the pieces move on the board I realised a queen is just a combination of a rook and bishop. So rather than repeating the code, I’m able to keep the function within the pieces class and share it between the other classes. 
This is quite a simple model, but the general concept is that the board is using the pieces to validate a move but only the board ‘knows’ where the pieces are. The pieces do not need to know their position on the board, just how to move legally. The board will perform checks and checkmate through the piece. A pawn will be promoted on the board. At first, I thought to include the pawn promotion in the pieces class, but I’m just adding unnecessary confusion to the pieces because it may not be able to differentiate how different pieces move, if I start including all the movements as subroutines in the piece class, rather than being unique to the piece. Also, my main program only needs to access the board class, passing in choices from the user would further create unnecessary issues.
## Limitations To the system
My skills and knowledge – I’m  unable map all the possible moves and re-enact a full-time chess game from the start of the game and selecting the best as it would require a huge amount of processing power and would be too difficult for me with the resources I have and the time constraints I am subject to.

## Planning

<a href="https://ibb.co/MCmWjf1"><img src="https://i.ibb.co/kStkrJD/img1.png" alt="img1" border="0"></a>
<a href="https://ibb.co/bRPHzt1"><img src="https://i.ibb.co/0VcBh5q/img2.png" alt="img2" border="0"></a>

Now, when I am instantiating a hash table, I need a method of decrementing the value of the key. I did some simple pseudocode  on how I would decrement the value of the pieces. In the above pseudocode, a hash table is passed into the subroutine with a corresponding key and, you minus one from the value of the key to decrement it. 

 I also implemented some pseudocode   for assigning values to the key to represent, how many pieces are initially on the board.  This is just a general idea, I still need to store separate hash tables for black and white pieces. Also, for a pawn promotion, the pawn would obviously not be allowed to promote to a king and itself. For a pawn promotion I’m making sure the value is greater than 0 as that would be an indication of any remaining pieces not on the board that the pawn could promote to.  However, for checking that the number of pieces on the board does not exceed the acceptable limit, I’m making sure the value of the key is not less than 0 as that would mean the user is using more pieces on the board, than what they initially had. For instance, a value for a pawn would be set to 8, but as pieces are added on the board, the value decreases. Now, if the board was to instantiate 9 black pawns on the board from the user’s input, there would be an error as the value of the pawn is now -1.

 

 ## Pseudocode for forming my checkmate Algorithm

 <a href="https://ibb.co/CnWxVf6"><img src="https://i.ibb.co/mHSQh30/img3.png" alt="img3" border="0"></a>

 Now this appears quite simple at first, but the basics of a check, is basically to see if the coordinates of an ‘enemy’ piece is legally able to move to the king’s coordinates. The availableMove would be a function that returns a true value if the piece can legally move to that position.


 ## Evaluation

 No	Objective	Performance Criteria 	Evaluation
1	User can Input Pieces onto the board	•	Denote uppercase characters as white pieces
•	Denote lowercase characters as black pieces 
•	Make sure the user cannot enter more than the maximum pieces on the board

	Completely Achieved.
This has been achieved from the use of a text file, where the user can physically input pieces on the board corresponding to an actual piece on the gameboard. I also implemented a hash table to keep track of the number of pieces on the board, to make sure the user could not go over the allowed amount.

However, this can be improved 
to make it easier for the user to input perhaps, using windows forums would be a better option than console when working on the user interface as I could implement a technique where the user can drag and drop pieces on the board. This is in response to my user, who when was playing the game said that it required a bit of patience getting the hang of inputting pieces on the board. For instance, when replacing a dash with a piece on the board, you must take care not to completely remove a dash on the line where the text file is being read or those ‘spaces’ would not be able to set to any instance, creating errors on the board. 

Although, he though my characters used to denote pieces was simple to get learn as it was just the first letter of the actual name of the piece.
2	Allocate coordinates to pieces on the board	•	create a text file that reads the chessboard file

•	A gameboard array to store the pieces on the board, that can be read from the text file and printed onto the screen for the user. 

.	This was achieved by use of a case-switch statement. I first instantiated an array to represent the gameboard using the Pieces class. Using my representations of characters on the board (for example a letter ‘p’ is listed as pawn), I used a case-switch statement so that  every time the board would come across a letter corresponding to a piece when reading the text file, it would then allocate a coordinate to this piece and pass it through the load board() function, so the piece could be instantiated on the gameboard.

This also involved the use of a hash table to make sure the correct number of pieces where on the board. initially before the board is instantiated, no pieces are recorded on the board, so the values of the keys would be set to the amount of that kind of piece found on the board. For instance, a pawn would have a value of 8.  This meant when using the case switch statements to initialise pieces on the board, it would decrement the value of the key of a piece. However, the game would find an error if the value of the key was less than 0 because that means the user is instantiating pieces on the gameboard that is not representative of the total amount of pieces on the chessboard. For instance, there couldn’t be 2 black kings, there can only be 1 black king.
3	An 8 x 8 chessboard displayed on the screen
	•	Gameboard must display pieces corresponding to what the user has inputted.
•	An empty space needs to be indicated as a symbol on the board, for e.g. ‘xx’ or it would have a null value.	Completely Achieved 
This was achieved by use of an array to instantiate all the pieces on the board, and I then used the print board () function to display the board to user, using a nested for loop. I also must make sure I considered the null values on the board, to ensure they were could be displayed as an ‘xx’ on the board or I would get an error.
This has been achieved as the program displays an 8 x8 board to represent a chessboard on screen.

However, this can be improved to make the user-interface more applicable to the user. My user commented that even though it achieved by the objective ‘it wasn’t very exciting to look at’. From this feedback, I could’ve used a window forum to create actual images of the pieces on the board and perhaps an alternating black and white square to embody an actual chessboard, like the application my user was using at the start.
4	Pieces can move within their ‘legal’ move
	•	define how each different piece moves on the board
•	Implement a way for the board to validate movements on specific pieces on the board through their legal requirements 
•	Check if a specific piece is blocking the movement of another piece as all pieces cannot ‘jump’ over other pieces except the king

	Completely Achieved
This has been achieved through my testing, in which each piece is able to move within their required moves.

This was achieved using the availableMove function used by each piece on the board, this was done by using the coordinates on the initial position of the piece and the final coordinates of where the piece would want to move to. 
For a pawn, this was done through a set of conditions to check if a pawn could diagonally capture an ‘enemy’ piece. I also had to validate what colour the piece was on the board as that would determine how the piece could move up/down using a condition statement.

For the knight- I implemented the AvailableMove using a Maths.Abs() function and condition statements as it can move an L shape in any direction, so it could either move 2 squares up or down and then one square up or down. It could also move in reverse by one square up or down, followed by two squares up or down.

For the king- this was done in a similar way to the knight by mapping all the positions it could move but instead one square forward.

Programming the Rook and Bishop was the hardest by far. At first, I would check the coordinates of the initial position against the final position but within validating that, I also had to check for any pieces obstructing the path. This required me to use the board class, to check for the presence of other pieces because a rook and bishop can move any set number of squares, So I had to embed a while loop to increment their required move by one each time, checking if there is a piece in the way. If there was a piece in the way, it’s not a legal move, because these pieces are unable to ‘jump’ over pieces.

Programming the Queen was the easiest as a queen is just a combination of a rook and bishop movement, so the MovingDiagonally() function and the MovingStraight() function could be placed in the pieces class and shared by the  queen, rook and bishop. This made my testing much easier, because if I was able to if the rook and bishop test where a success, that meant the queen could also move successfully.



I’m proud with how this turned out because it required a lot of debugging, especially with while loops I used, and I had to plan out how different pieces were moving on the board and find the patterns between the source and destination coordinates. For example, when I was playing around with how a bishop moves, I realised that the absolute value of the difference in x coordinates should equal the absolute value of the difference in y coordinates as moving diagonally would require you meet move the same magnitude in both the x and y axis in any direction. 

To improve this, I would include further ‘special’ moves made in chess like castling where the king would be able to move two squares to the left or right and the rook must be moved to stand on the opposite side of the king. Castling is a good technique to protect the king as you move the king from action to a safe place in the corner and the rook can move to the middle of board. Playing chess, I’ve realised the king is invaluable in its movements, most of the time you’re trying to get the king out of danger as if it is not in a safe position then the game could be lost. 

5	A pawn can be promoted to another piece not on the board, except the king
	•	implement a hash table to keep track of the pieces remaining on the board 
•	allow the user to choose from a selection of numbers what the pieces could be promoted to.	Partially Achieved
This was partially achieved using a hash table where the key would be the character of a piece e.g. a 'q' means it’s a queen and the value would be the number of that kind of piece that has been removed.  Hence, initially before the board is instantiated, no pieces are recorded on the board, so the pawn could promote to any other piece except the king. However, when using the case switch statements to initialise pieces on the board, it would decrement the value of the key of a piece, as if that piece was already on the board, the pawn could not promote to it. This also required the use of a case-switch statement as the user had to select the option of what they wanted to promote to and if the key was equal to zero, that option would not be given for the user to promote its pawn to.
 My user found that when a pawn had reached an ‘enemy’ line and was given the option to promote, even though all the available rooks had been used on the board, so it was not listed as option, when she entered 2, the program still allowed her to promote her pawn. This is because, when moving to the case switch statements, the option to promote to a rook is still available it just not has been told in the console to the user.

If I could improve this, I would implement a condition that further checks the value of the key if the user promotes incorrectly promotes the piece, so an error message could be shown that its unavailable to promote to pieces on the board.


6	Identify if a king is in check
	•	find if any enemy piece of the king could move to the coordinate that the king currently occupies
•	check if any pieces on the king side could help the king by being able to legally move to the coordinate of the enemy piece.
•	Highlight to the user what piece(s) is putting the king in check	Completely Achieved.

This was achieved through my checkforcheck function, where I passed the value of the piece that could potentially capture the king, and coordinates of king through the parameters. This meant the board would use the pieces to evaluate if that piece could occupy the coordinate its enemy king is in, hence successfully indicating check.

Ironically, forming the general idea of a check was the easiest part of my code, it became more difficult when you had to consider other pieces on the board and the checkmate function, which was very repetitive and hence susceptible to errors. I think this was the most efficient solution in my code, I was able to use the function all throughout the code. 
My user liked the way the program provided information on the actual pieces putting the king in danger, because in the application he was using, it couldn’t indicate what piece(s) where putting the king in check, which could help in the process of coming up with a defence to protect their king.
7	Identify if a king is in checkmate
	•	map all the possible positions that a king could move
•	perform a check in each of those positions, and if each position is in check, then it can be indicated as checkmate	Completely Achieved

This was achieved through my checkmate function. Whenever the program came across a check, it would then check for a checkmate. This involved me mapping out all the possible coordinate the king could move to and then performing a checkforcheck() in all those positions. If each position the king moved to was in check and no pieces could help, then it was in checkmate. I also had to initially destroy the original position of the king because I’m not physically moving my king. For instance, when I’m trying to check all the positions the king could move to because the original king is 'in the way' then if the  for instance a rook was to move diagonally down then it wouldn't be able to move there because  if a piece is legally able to capture the king in its new position like the bishop was, it wouldn’t be recognised when the board class uses the piece class because the 'old' king is blocking its movement.

This required the most work because coding a checkmate can be quite tedious, so I found myself always trying to make it as efficient as possible and facilitating the use of subroutines wherever necessary, and even when looking at more simplified ways to code it, it would get confusing when reading it through myself, So I tried to make it as readable as possible.
8	If the user is in check, suggest a set of possible moves the user could make to get out of check
	•	identify if a king is in check 
•	list the coordinates of pieces that could help the king.	Completely Achieved
This was achieved from canthePiecesHelp() function, where I used a nested for-loop to go through all the pieces on the board and the checkforcheck() function to see if any pieces could capture the piece putting the king in danger
I found this initially confusing when coding, so I constantly had to self-document my work, so when looking back it made sense as to why I was using a piece with a colour opposite the piece putting the king in check.
My user found this guidance to how the pieces can move very helpful because it was able to direct them in where they could move the pieces as well as, highlighting the coordinates of the specific pieces. This helped my user make the final decision on what move they could use and how it might affect the other pieces.
Although, to improve on this my user suggested, that even if other pieces can help if the user is in check, it should also move the king in its legal moves as well, so the user could be provided with more possible moves. This is because, initially, when a piece is in check but other pieces on the king’s side can help, then the check statement is automatically set to true, hence it would not follow through with the checkmate function, which involves moving the king around.

