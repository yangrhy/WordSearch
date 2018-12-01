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
        private int numOfRows = 0;
        private int numOfCols = 0;
        private List<string> stringsOfPuzzles = new List<string>(); // takes strings of puzzles to throw into a 2d char array

        public WordSearch()
        {
            InitializeComponent();
        }

        private void splitStrings(List<string> inputStrings)
        {
            numOfPuzzles = int.Parse(inputStrings[0]);

            string[] splitRowsCols = new string[2]; // string array to get num of rows and cols
            int lineCount = 1; // keep count of what index currently at in inputStrings
            string puzzStringConc = string.Empty;
            List<string> wordsToFind = new List<string>();
            listBox1.Items.Add(numOfPuzzles);
            // keep count of what index currently at in inputStrings with a variable set to 0
            // get numOfPuzzles to know how many times to iterate, increment lineCount
            // get num of columns and rows, increment lineCount
            // num of rows = #, that # should indicate how many indexes to input string for puzzle (place into string for puzzles)
            // next num is for num of words (place into list of int), increment lineCount
            // get words and place strings into new list of words using lineCount
            // repeat if anything else is left
            while (numOfPuzzles > 1)
            {
                richTextBox1.Text += "SPLIT HERE\t\n";

                for (int i = lineCount; i < lineCount + 1; i++)
                {
                    splitRowsCols = inputStrings[i].Split(' ');
                }
                lineCount += 1;
                richTextBox1.Text += "SPLIT ROWS" + splitRowsCols;
                listBox1.Items.Add(numOfRows);
                listBox1.Items.Add(numOfCols);
                numOfRows = 6;

                // get characters for crossword puzzle
                for (int i = lineCount; i < (lineCount + numOfRows); i++)
                {
                    puzzStringConc += string.Join("", inputStrings[i]);
                }
                stringsOfPuzzles.Add(puzzStringConc); // place concatenated string into stringOfPuzzles
                lineCount += numOfRows;
                // ***code good up to here***

                string numOfWords = inputStrings[lineCount];
                listBox1.Items.Add("This is num of words: " + numOfWords);
                int numWords;
                var getNumWords = int.TryParse(numOfWords, out numWords);

                lineCount += 1;
                // for loop to get string of words
                for (int i = lineCount; i < lineCount + numWords; i++)
                {
                    wordsToFind.Add(inputStrings[i]);
                }

                foreach (string s in wordsToFind)
                {
                    richTextBox1.Text += s + " ";
                }
                lineCount += numWords;
                listBox1.Items.Add("This is int NumWords" + numWords);
                listBox1.Items.Add("This is int lineCount" + lineCount);

                richTextBox1.Text += lineCount.ToString();

                richTextBox1.Text += puzzStringConc;

                puzzStringConc = ""; // reset string to blank for next puzzle
                numOfPuzzles--;
            }

        }

        private void openTextFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog;
            string fileName;
            List<string> inputStrings = new List<string>();

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
                splitStrings(inputStrings);
            }
            catch
            {
                MessageBox.Show("Error Reading from File", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
