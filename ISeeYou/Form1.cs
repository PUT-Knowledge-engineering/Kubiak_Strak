using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace ISeeYou
{
    public partial class Form1 : Form
    {
        List<string> filenames = new List<string>();
        List<Entry> entries = new List<Entry>();
        Entry currentEntry;
        private VesselRecognizer vesselImage;
        int index;
        StreamWriter sw = File.AppendText("outout.json");
        public Form1()
        {
            InitializeComponent();
        }


        private void openFolderButton_Click(object sender, EventArgs e)
        {
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                filenames.Clear();
                index = 0;
                foreach (string file in Directory.EnumerateFiles(folderBrowser.SelectedPath, "*.*", SearchOption.AllDirectories))
                {
                    filenames.Add(file);
                }
                
                pictureBox.Image = readFile(filenames[index]);
            }
        }
        
        private Image readFile(string path)
        {
            Image image = Image.FromFile(path);
            
            // Przekazanie obrazku do VesselRecognition
            vesselImage = new VesselRecognizer(path);
            vesselImage.TresholdForSobel = trackBar1.Value;
            
            return image;
        }

#pragma region Navigation Button
        private void previousButton_Click(object sender, EventArgs e)
        {
           
                index = (index == 0 ? filenames.Count - 1 : index - 1);
                try
                {
                    pictureBox.Image = readFile(filenames[index]);

                }
                catch (OutOfMemoryException ignored)
                {
                }
           
        }

    
        private void nextButton_Click(object sender, EventArgs e)
        {
            
                index = (index == filenames.Count - 1 ? 0 : index + 1);
                try
                {
                    pictureBox.Image = readFile(filenames[index]);
                }
                catch (OutOfMemoryException ignored)
                {
                }
      
        }
#pragma endregion

        private void pictureBox_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.vesselImage.recognizeVessel();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.vesselImage.TresholdForSobel = trackBar1.Value;
            label1.Text = trackBar1.Value.ToString();
        }
    }
}
