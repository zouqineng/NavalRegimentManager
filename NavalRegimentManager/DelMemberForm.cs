using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NavalRegimentManager
{
    public partial class DelMemberForm : Form
    {
        private MainForm mainForm;

        public DelMemberForm(MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            mainForm.DelMembers(this.txtDelInfo.Text.Trim());
            this.Close();
        }
    }
}
