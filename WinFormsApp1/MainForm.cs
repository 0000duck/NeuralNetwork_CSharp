using Med_System;
using neuron_with_oop;

namespace WinFormsApp1
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutDlg = new AboutBox1();
            aboutDlg.Show();
        }

        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var pictureConverter = new PictureConverter();
                var inputs = pictureConverter.Convert(openFileDialog.FileName);
                var result = Program.Controller.ImageNetwork.Predict(inputs).Output;
               
            }
        }

        private void enterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var EnterDataForm = new EnterData();
            var result = EnterDataForm.ShowForm();

        }
    }
}