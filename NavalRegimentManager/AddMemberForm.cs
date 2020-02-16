﻿using System;
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
    public partial class AddMemberForm : Form
    {
        private MainForm mainForm;

        public AddMemberForm(MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            mainForm.AddMembers(this.txtMemberInfo.Text.Trim());
            this.Close();
        }
    }
}