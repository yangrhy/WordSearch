# Word Search Application
This application was made in lieu of the final exam. An ACM programming contest question was given, which we had to solve and give it a GUI. Input must be read from a given text file (ACM.txt in this case). Words can wrap around continuously.  

### How the Input File is Read
1. The '2' on line 1 - indicates the number of puzzles to be inputted.  
2. The '6' and '12' on line 2 - indicates the number of rows and columns of letters.  
3. The letters from lines 3-8 are the letters for the word search puzzle.  
4. The '5' on line 9 - indicates the number of words to be inputted.  
5. Lines 10-15 are the words to be inputted.  
6. Additional puzzles will repeat, starting from step 2 until the end.

### Search Algorithm
This uses a recursive algorithm to search for the words. It starts at a point (where the letter is found), and searches up, down, right, and left, until the next letter does not correctly match. Letters in the grid that match are highlighted in green. Letters that do not much are highlighted in red. If a word is found it is listed in the bottom right box, which indicates which row and col it was found in and what direction (up, down, left, right) to follow.

# Screenshots

### Upon Starting the Application
<img src="https://github.com/yangrhy/WordSearch/blob/master/Screenshots/Initial.JPG" width="80%" height="80%"/>

### Click "Open Text File" and open "ACM.txt"
<img src="https://github.com/yangrhy/WordSearch/blob/master/Screenshots/textfile.jpg" width="80%" height="80%"/>

### "Number of Puzzles" textbox should now have a value
<img src="https://github.com/yangrhy/WordSearch/blob/master/Screenshots/Num_puzzles.JPG" width="80%" height="80%"/>

### Clicking "Get Puzzle" button will deduct the "Number of Puzzles" and add "Words to Search" into the listbox
<img src="https://github.com/yangrhy/WordSearch/blob/master/Screenshots/Puzzle1_before.JPG" width="80%" height="80%"/>

### Select a word and click "Search Words"

#### First puzzle results
<img src="https://github.com/yangrhy/WordSearch/blob/master/Screenshots/Search_word.JPG" width="80%" height="80%"/>
<img src="https://github.com/yangrhy/WordSearch/blob/master/Screenshots/Search_search.JPG" width="80%" height="80%"/>
<img src="https://github.com/yangrhy/WordSearch/blob/master/Screenshots/Search_knightro.JPG" width="80%" height="80%"/>
<img src="https://github.com/yangrhy/WordSearch/blob/master/Screenshots/Search_underfund.JPG" width="80%" height="80%"/>
<img src="https://github.com/yangrhy/WordSearch/blob/master/Screenshots/Search_ingesting.JPG" width="80%" height="80%"/>

### Click "Get Puzzle" to get the next puzzle
<img src="https://github.com/yangrhy/WordSearch/blob/master/Screenshots/Puzzle2.JPG" width="80%" height="80%"/>

### Second puzzle results
<img src="https://github.com/yangrhy/WordSearch/blob/master/Screenshots/Search_age.JPG" width="80%" height="80%"/>
<img src="https://github.com/yangrhy/WordSearch/blob/master/Screenshots/Search_thethethetheth.JPG" width="80%" height="80%"/>

As you can see above, "thethethetheth" can be found by wrapping around 4 times.
