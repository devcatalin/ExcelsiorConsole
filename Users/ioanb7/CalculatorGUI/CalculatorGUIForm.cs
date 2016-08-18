using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using org.mariuszgromada.math.mxparser;

namespace ExcelsiorConsole.Users.ioanb7.CalculatorGUI
{
    public partial class CalculatorGUIForm : Form
    {
        public CalculatorGUIForm()
        {
            InitializeComponent();
        }

        private void calculateButton_Click(object sender, EventArgs e)
        {
            Expression expression = new Expression(calculateInput.Text);
            MessageBox.Show(expression.calculate().ToString());
        }
    }
}
