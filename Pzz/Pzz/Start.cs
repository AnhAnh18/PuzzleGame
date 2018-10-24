using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pzz
{
    public partial class Start : Form
    {
        public static Start _main;
        bool Eight = false, TwentyFour = false;

        private void cmbx_size_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbx_size.SelectedItem.ToString())
            {
                case "3 * 3":
                    Eight = true; TwentyFour = false;
                    break;
                case "5 * 5":
                    Eight = false; TwentyFour = true;
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Eight_Puzzle _8 = new Eight_Puzzle();
            Twenty_Four_Puzzle _24 = new Twenty_Four_Puzzle();
            if (Eight)
            {
                _8.Show();
                _main.Hide();
            }
            else if (TwentyFour)
            {

                _24.Show();
                _main.Hide();
            }
        }

        public Start()
        {
            InitializeComponent();
            _main = this;
        }
    }
}
