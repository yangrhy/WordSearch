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

        public WordSearch()
        {
            InitializeComponent();
        }

        private void createTable(string puzzString)
        {
            char[] puzzChars = puzzString.ToCharArray();
            char[,] puzzData = new char[numOfRows, numOfCols];
            int i = 0;
            listView1.Clear();

            for (int row = 0; row < numOfRows; row++)
            {
                for (int col = 0; col < numOfCols; col++)
                {
                    puzzData[row, col] = puzzChars[i];
                    listView1.Items.Add(puzzData[row, col].ToString());
                    i++;
                }
            }

            for (int col = 0; col < puzzData.GetLength(1); col++)
            {
                dataGridView1.ColumnCount += 1;
                dataGridView1.Columns[col].Width = 30;
            }

            for (int x = 0; x < puzzData.GetLength(0); x++)// array rows
            {
                string[] row = new string[puzzData.GetLength(1)];

                for (int y = 0; y < puzzData.GetLength(1); y++)
                {
                    row[y] = puzzData[x, y].ToString();
                }

                dataGridView1.Rows.Add(row);
            }
        }

        private void splitStrings(List<string> inputStrings)
        {

            string[] splitRowsCols = new string[2]; // string array to get num of rows and cols           
            string puzzStringConc = string.Empty;
            List<string> wordsToFind = new List<string>(); // list to hold words to find in word search puzzle

            listBox1.Items.Add(numOfPuzzles);

            // keep count of what index currently at in inputStrings with a variable set to 0
            // get numOfPuzzles to know how many times to iterate, increment lineCount
            // get num of columns and rows, increment lineCount
            // num of rows = #, that # should indicate how many indexes to input string for puzzle (place into string for puzzles)
            // next num is for num of words (place into list of int), increment lineCount
            // get words and place strings into new list of words using lineCount
            // repeat if anything else is left

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

            numOfRows = int.Parse(splitRowsCols[0]);
            numOfCols = int.Parse(splitRowsCols[1]);

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

            richTextBox1.Text = "The Puzz Strings = " + puzzStringConc;
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

            splitStrings(inputStrings);
            textBox1.Text = numOfPuzzles.ToString();
        }
    }
}
