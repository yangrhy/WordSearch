using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WordSearch
{
    public partial class WordSearch : Form
    {
        private StreamReader fileReader;
        private int numOfPuzzles = 0;
        List<string> inputStrings = new List<string>(); // collection of strings from input file
        private List<string> stringsOfPuzzles = new List<string>(); // takes strings of puzzles to throw into a 2d char array
        private int numOfRows = 0; // get num of rows to make grid with
        private int numOfCols = 0; // get num of cols to make grid with
        private int lineCount = 1; // keep count of what index currently at in inputStrings
        private int puzzleNumber = 1;
        char[,] puzzData = new char[0, 0];

        public WordSearch()
        {
            InitializeComponent();
        }

        // clear datagridview highlighted colors
        private void clearDataGridView()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    dataGridView1.Rows[row.Index].Cells[col.Index].Style.BackColor = Color.Empty;

                }
            }
        }
        /*
        // find word at [row,col] where first char was found going to the right
        private bool findWordDown(string word, int row, int col)
        {

            return false;
        }

        // find word at [row,col] where first char was found going to the right
        private bool findWordUp(string word, int row, int col)
        {

            return false;
        }

        // find word at [row,col] where first char was found going to the right
        private bool findWordLeft(string word, int row, int col)
        {
            char[] wordChars = word.ToCharArray();
            int a = 0;
            for (int i = row; i < dataGridView1.RowCount; i--)
            {
                for (int j = col; j < dataGridView1.ColumnCount; j++)
                {
                    if( i == 0)
                    {
                        i = dataGridView1.RowCount;
                    }

                    if (wordChars[a] == puzzData[i, j])
                    {
                        a++;
                    }
                    return true;
                }
            }
            return false;
        }

        // find word at [row,col] where first char was found going to the right
        private bool findWordRight(string word, int row, int col, int index)
        {
            //clearDataGridView();
            char[] wordChars = word.ToCharArray();
            string wordFound = string.Empty;
            int i = row;

            for (int j = col; j < wordChars.Length; j++)
            {
                if (wordChars[index] == puzzData[i, j])
                {
                    wordFound += wordChars[index];
                    // highlights the cell if matches
                    dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.LightGreen;
                    index++;
                }
                else
                { return false; }
            }

            if (wordFound == word)
            {
                return true;
            }

            return false;
        }
        */
        private bool wordSearch(char letter, int row, int col)
        {

            return false;
        }

        // find if first char is in the puzzle
        private List<Point> findChar(string word)
        {
            char[] wordChars = word.ToCharArray();
            List<Point> rowCol = new List<Point>();

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    if (wordChars[0] == puzzData[i, j])
                    {
                        Point point = new Point(i,j);
                        rowCol.Add(point);
                    }                   
                }
            }
            return rowCol;
        }
        
        // create word search puzzle
        private void createTable(string puzzString)
        {
            char[] puzzChars = puzzString.ToCharArray();
            puzzData = new char[numOfRows, numOfCols];
            int i = 0;
            clearDataGridView();

            for (int row = 0; row < numOfRows; row++)
            {
                for (int col = 0; col < numOfCols; col++)
                {
                    puzzData[row, col] = puzzChars[i];
                    i++;
                }
            }

            // create number of columns according to current puzzle
            for (int col = 0; col < puzzData.GetLength(1); col++)
            {
                dataGridView1.ColumnCount += 1;
                dataGridView1.Columns[col].Width = 30;
            }

            // create number of columns according to current puzzle
            // and insert char data into the puzzles
            for (int x = 0; x < puzzData.GetLength(0); x++)
            {
                string[] row = new string[puzzData.GetLength(1)];

                for (int y = 0; y < puzzData.GetLength(1); y++)
                {
                    row[y] = puzzData[x, y].ToString();
                }

                dataGridView1.Rows.Add(row);
            }

            this.dataGridView1.ClearSelection();
        }

        private void splitStrings(List<string> inputStrings)
        {

            string[] splitRowsCols = new string[2]; // string array to get num of rows and cols           
            string puzzStringConc = string.Empty;
            List<string> wordsToFind = new List<string>(); // list to hold words to find in word search puzzle
            listBox1.Items.Add(numOfPuzzles);           

            try
            {
                for (int i = lineCount; i < lineCount + 1; i++)
                {
                    splitRowsCols = inputStrings[i].Split(' ');
                }
            }
            catch
            {
                MessageBox.Show("Error: No more puzzles");
            }
            lineCount += 1;

            try
            {
                numOfRows = int.Parse(splitRowsCols[0]);
                numOfCols = int.Parse(splitRowsCols[1]);
            }
            catch
            {
                MessageBox.Show("An error has occured.");
            }
            // get characters for crossword puzzle
            for (int i = lineCount; i < (lineCount + numOfRows); i++)
            {
                puzzStringConc += string.Join("", inputStrings[i]);
            }

            //stringsOfPuzzles.Add(puzzStringConc); // place concatenated string into stringOfPuzzles

            createTable(puzzStringConc);

            lineCount += numOfRows;

            string numOfWords = inputStrings[lineCount];

            int numWords;
            var getNumWords = int.TryParse(numOfWords, out numWords);

            lineCount += 1;
            // for loop to get string of words
            for (int i = lineCount; i < lineCount + numWords; i++)
            {
                wordsToFind.Add(inputStrings[i]);
            }

            lineCount += numWords;

            listBox1.Items.Clear();
            foreach (string s in wordsToFind)
                listBox1.Items.Add(s);

            puzzStringConc = ""; // reset string to blank for next puzzle
            wordsToFind.Clear(); // reset list of words to find for next puzzle

            numOfPuzzles--;

            if (numOfPuzzles < 1)
                getPuzzleButton.Enabled = false;
        }

        private void openTextFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog;
            string fileName;            

            // open text file of choice with dialog box
            using (OpenFileDialog openFile = new OpenFileDialog())
            {
                openFile.Filter = "Text Files (.txt)| *.txt"; // set to search for text files only
                dialog = openFile.ShowDialog();
                fileName = openFile.FileName;
            }

            if (dialog == DialogResult.OK)
            {
                // show error if user specified invalid file
                if (string.IsNullOrEmpty(fileName))
                {
                    MessageBox.Show("Invalid File Name", "Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        // create FileStream to obtain read access to file
                        FileStream input = new FileStream(
                           fileName, FileMode.Open, FileAccess.Read);

                        // set file from where data is read
                        fileReader = new StreamReader(input);
                        getPuzzleButton.Enabled = true;

                    }
                    catch (IOException exception)
                    {
                        MessageBox.Show(exception.ToString());
                    }
                }
            }

            try
            {
                string input;
                while ((input = fileReader.ReadLine()) != null)
                {
                    inputStrings.Add(input);
                }
                numOfPuzzles = int.Parse(inputStrings[0]);
                textBox1.Text = numOfPuzzles.ToString();
            }
            catch
            {
                MessageBox.Show("Error Reading from File", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void getPuzzleButton_Click(object sender, EventArgs e)
        {
            wordSearchLabel.Text = "Word Search Puzzle #" + puzzleNumber;
            splitStrings(inputStrings);
            textBox1.Text = numOfPuzzles.ToString();
            searchWordsButton.Enabled = true;
            puzzleNumber++;
        }

        private void WordSearch_Load(object sender, EventArgs e)
        {
            searchWordsButton.Enabled = false;
            this.dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 15);
            this.dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void searchWordsButton_Click(object sender, EventArgs e)
        {
            List<Point> rowCol = new List<Point>();
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Select a word to find.");
            }
            else
            {
                string currentWord = listBox1.SelectedItem.ToString();
                char[] wordChars = currentWord.ToCharArray();
                int row = 0;
                int i = 0;

                foreach (Point p in findChar(currentWord))
                {
                    rowCol.Add(p);
                    richTextBox1.Text += $"{p.row} {p.col}\n";
                }        

                /*
                foreach (int num in findChar(currentWord))
                {
                    rowCol.Add(num);
                }

                if(findWordRight(currentWord, rowCol[0], rowCol[1], 0))
                {
                    richTextBox1.Text += $"{currentWord} found at row: {rowCol[0] + 1} column: {rowCol[1] + 1} going right.";
                }

                richTextBox1.Text += findWordRight(currentWord, rowCol[0], rowCol[1], 0); // see if true or not
                */
            }
        }
    }
}
