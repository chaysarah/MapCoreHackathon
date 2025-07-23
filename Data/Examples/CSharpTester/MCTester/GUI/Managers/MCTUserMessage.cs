using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MCTester.Managers
{
    public partial class MCTUserMessage : Form
    {
        public MCTUserMessage()
        {
            InitializeComponent();
        }

        public MCTUserMessage(string messageText, string formText, string btn1Text, string btn2Text = "", string btn3Text = ""):this()
        {
            textBox1.Text = messageText;
            button1.Text = btn1Text;
            if (btn1Text == "")
            {
                button1.Visible = false;
            }
            if (btn2Text == "")
            {
                button2.Visible = false;
            }
            else
            {
                button2.Text = btn2Text;
            }
            if (btn3Text == "")
            {
                button3.Visible = false;
            }
            else
            {
                button3.Text = btn3Text;
            }
            Text = formText;
        }
    }
}
