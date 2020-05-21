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
<img src="https://github.com/yangrhy/WordSearch/blob/master/Screenshots/Initial.JPG">

### Click "Open Text File" and open "ACM.txt"
<img src="https://github.com/yangrhy/WordSearch/blob/master/Screenshots/textfile.jpg">

### "Number of Puzzles" textbox should now have a value
<img src="https://github.com/yangrhy/WordSearch/blob/master/Screenshots/Num_puzzles.JPG">

### Clicking "Get Puzzle" button will deduct the "Number of Puzzles" and add "Words to Search" into the listbox
<img src="https://github.com/yangrhy/WordSearch/blob/master/Screenshots/Puzzle1_before.JPG">

### Select a word and click "Search Words"
#### Here are the results from the first puzzle:
<img src="https://github.com/yangrhy/WordSearch/blob/master/Screenshots/Search_word.JPG"/>
<img src="https://github.com/yangrhy/WordSearch/blob/master/Screenshots/Search_search.JPG"/>
<img src="https://github.com/yangrhy/WordSearch/blob/master/Screenshots/Search_knightro.JPG"/>
<img src="https://github.com/yangrhy/WordSearch/blob/master/Screenshots/Search_underfund.JPG"/>
<img src="https://github.com/yangrhy/WordSearch/blob/master/Screenshots/Search_ingesting.JPG"/>

### Click "Open Text File" and open "ACM.txt"
<img src="https://github.com/yangrhy/WordSearch/blob/master/Screenshots/textfile.jpg">

### Click "Open Text File" and open "ACM.txt"
<img src="https://github.com/yangrhy/WordSearch/blob/master/Screenshots/textfile.jpg">

### Click "Open Text File" and open "ACM.txt"
<img src="https://github.com/yangrhy/WordSearch/blob/master/Screenshots/textfile.jpg">

### Click "Open Text File" and open "ACM.txt"
<img src="https://github.com/yangrhy/WordSearch/blob/master/Screenshots/textfile.jpg">
