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

        public WordSearch()
        {
            InitializeComponent();
        }

        private void splitStrings(List<string> inputStrings)
        {
            numOfPuzzles = int.Parse(inputStrings[0]);

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
