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
        private void ClearDataGridViewColor()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    dataGridView1.Rows[row.Index].Cells[col.Index].Style.BackColor = Color.Empty;
                }
            }
        }

        // return true if word is found going down
        private bool FindWordDown(char[] wordChars, int index, int row, int col)
        {
            // check to see if row has gone out of bounds
            // if so then wrap back around by setting row back to 0
   
            while (index < wordChars.Length)
            {
                if (row >= puzzData.GetLength(0))
                {
                    row = row % puzzData.GetLength(0);
                }
                if (puzzData[row, col] == wordChars[index])
                {
                    dataGridView1.Rows[row].Cells[col].Style.BackColor = Color.LightGreen;
                    //recursive call for the next letter and search down
                    FindWordDown(wordChars, ++index, ++row, col);
                }
                else if (puzzData[row, col] != wordChars[index])
                {
                    if (dataGridView1.Rows[row].Cells[col].Style.BackColor == Color.LightGreen)
                    {
                        return false;
                    }
                    else
                    {
                        dataGridView1.Rows[row].Cells[col].Style.BackColor = Color.Red;
                        return false;
                    }
                }
            }
            return true;
        }

        // return true if word is found searching up
        private bool FindWordUp(char[] wordChars, int index, int row, int col)
        {
            while (index < wordChars.Length)
            {
                if (puzzData[row, col] == wordChars[index])
                {
                    dataGridView1.Rows[row].Cells[col].Style.BackColor = Color.LightGreen;
                    // wrap row upwards if it goes out of bounds
                    if (row <= 0)
                    {
                        row = puzzData.GetLength(0);
                    }

                    //recursive call for the next letter and search up
                    FindWordUp(wordChars, ++index, --row, col);
                }
                else if (puzzData[row, col] != wordChars[index])
                {
                    if (dataGridView1.Rows[row].Cells[col].Style.BackColor == Color.LightGreen)
                    {
                        return false;
                    }
                    else
                    {
                        dataGridView1.Rows[row].Cells[col].Style.BackColor = Color.Red;
                        return false;
                    }                    
                }
            }
            return true;
        }

        // return true if word is found to left
        private bool FindWordLeft(char[] wordChars, int index, int row, int col)
        {
            while (index < wordChars.Length)
            {
                if (puzzData[row, col] == wordChars[index])
                {
                    dataGridView1.Rows[row].Cells[col].Style.BackColor = Color.LightGreen;
                    // wrap column back around to right end
                    if (col <= 0)
                    {
                        col = puzzData.GetLength(1);
                    }

                    //recursive call for the next letter and search to the left
                    FindWordLeft(wordChars, ++index, row, --col);
                }
                else if (puzzData[row, col] != wordChars[index])
                {
                    if (dataGridView1.Rows[row].Cells[col].Style.BackColor == Color.LightGreen)
                    {
                        return false;
                    }
                    else
                    {
                        dataGridView1.Rows[row].Cells[col].Style.BackColor = Color.Red;
                        return false;
                    }
                }
            }
            return true;
        }

        // return true if word is found to the right
        private bool FindWordRight(char[] wordChars, int index, int row, int col)
        {
            // check to see if column has gone out of bounds
            // if so then wrap back around by setting column back to 0

            while (index < wordChars.Length)
            {
                if (col >= (puzzData.GetLength(1) - 1))
                {
                    col = col % puzzData.GetLength(1);
                }

                if (puzzData[row, col] == wordChars[index])
                {
                    dataGridView1.Rows[row].Cells[col].Style.BackColor = Color.LightGreen;
                    //recursive call for the next letter and search to right
                    FindWordRight(wordChars, ++index, row, ++col);
                }
                else if (puzzData[row, col] != wordChars[index])
                {
                    if (dataGridView1.Rows[row].Cells[col].Style.BackColor == Color.LightGreen)
                    {
                        return false;
                    }
                    else
                    {
                        dataGridView1.Rows[row].Cells[col].Style.BackColor = Color.Red;
                        return false;
                    }
                }
            }
            return true;
        }

        // find if first char is in the puzzle
        private List<Point> FindFirstChar(char letter)
        {
            List<Point> rowCol = new List<Point>();

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    if (puzzData[i, j] == letter)
                    {
                        Point point = new Point(i,j);
                        rowCol.Add(point);
                    }                   
                }
            }
            return rowCol;
        }
        
        // create word search puzzle
        private void CreateTable(string puzzString)
        {
            char[] puzzChars = puzzString.ToCharArray();
            puzzData = new char[numOfRows, numOfCols];
            int i = 0;
            ClearDataGridViewColor();

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

        private void SplitStrings(List<string> inputStrings)
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

            CreateTable(puzzStringConc);

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
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            wordSearchLabel.Text = "Word Search Puzzle #" + puzzleNumber;
            SplitStrings(inputStrings);
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
            int index = 0;
            ClearDataGridViewColor();

            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Select a word to find.");
            }
            else
            {
                string currentWord = listBox1.SelectedItem.ToString();
                char[] wordChars = currentWord.ToCharArray();
                
                // get point for where the first char in word is found
                // since there can be multiple places
                foreach (Point p in FindFirstChar(wordChars[0]))
                {
                    rowCol.Add(p);                    
                }

                for (int i = 0; i < rowCol.Count; i++)
                {
                    if (FindWordRight(wordChars, index, rowCol[i].row, rowCol[i].col))
                    {
                        richTextBox1.Text += $"{currentWord} found at Row: {rowCol[i].row + 1} Col: {rowCol[i].col + 1} going RIGHT\n";
                    }
                    if (FindWordLeft(wordChars, index, rowCol[i].row, rowCol[i].col))
                    {
                        richTextBox1.Text += $"{currentWord} found at Row: {rowCol[i].row + 1} Col: {rowCol[i].col + 1} going LEFT\n";
                    }
                    if (FindWordUp(wordChars, index, rowCol[i].row, rowCol[i].col))
                    {
                        richTextBox1.Text += $"{currentWord} found at Row: {rowCol[i].row + 1} Col: {rowCol[i].col + 1} going UP\n";
                    }
                    if (FindWordDown(wordChars, index, rowCol[i].row, rowCol[i].col))
                    {
                        richTextBox1.Text += $"{currentWord} found at Row: {rowCol[i].row + 1} Col: {rowCol[i].col + 1} going DOWN\n";
                    }
                }            
            }
        }
    }
}
