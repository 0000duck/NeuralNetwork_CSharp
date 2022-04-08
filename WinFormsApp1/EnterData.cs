using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Med_System;
using WinFormsApp1;

namespace Med_System
{
    public partial class EnterData : Form
    {
        private List<TextBox> Inputs = new List<TextBox>();
        public EnterData()
        {
            InitializeComponent();

            var propInfo = typeof(Patient).GetProperties();
            for(int i = 0; i < propInfo.Length; i++)
            {
                var property = propInfo[i];
                var textbox = CreateTextBox(i, property);
                Controls.Add(textbox);
                Inputs.Add(textbox);
            }
        }

        public bool? ShowForm()
        {
            var form = new EnterData();
            if(form.ShowDialog() == DialogResult.OK)
            {
                var patient = new Patient();

                foreach (var textBox in form.Inputs)
                {
                    patient.GetType().InvokeMember(textBox.Tag.ToString(), 
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty,
                        Type.DefaultBinder, patient, new object[] {textBox.Text } );
                }

                var result = Program.Controller.DataNetwork.Predict().Output;
                return result == 1.0;
            }

            return null;
        }

        private void EnterData_Load(object sender, EventArgs e)
        {

        }

        private TextBox CreateTextBox(int number, PropertyInfo property)
        {
            var y = number * 25 + 12;
            var textbox = new TextBox
            {
                Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right),
                Location = new Point(12, y),
                Name = "textBox" + number,
                Size = new Size(335, 20),
                TabIndex = number,
                Text = property.Name,
                Tag = property.Name,
                Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Italic, GraphicsUnit.Point, 205),
                ForeColor = Color.Gray
        };

            textbox.GotFocus += Textbox_GotFocus;
            textbox.LostFocus += Textbox_LostFocus;
            return textbox;
        }

        private void Textbox_LostFocus(object? sender, EventArgs e)
        {
            var textbox = sender as TextBox;
            if (textbox.Text == "")
            {
                textbox.Text = textbox.Tag.ToString();
                textbox.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Italic, GraphicsUnit.Point, 205);
                textbox.ForeColor = Color.Gray;
            }

        }

        private void Textbox_GotFocus(object? sender, EventArgs e)
        {
            var textbox = sender as TextBox;
            if (textbox.Text == textbox.Tag.ToString())
            {
                textbox.Text = "";
                textbox.Font = new Font("Microsoft Sans Serif", 8.25F,FontStyle.Italic,GraphicsUnit.Point,205);
                textbox.ForeColor = Color.Black;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
