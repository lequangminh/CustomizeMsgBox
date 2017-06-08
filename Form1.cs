// Copyright (C) 2014-2015, phamtuan Research Inc.
//  
// All rights are reserved. Reproduction or transmission in whole or in part, in any form or by
// any means, electronic, mechanical or otherwise, is prohibited without the prior written
// consent of the copyright owner.
// ---------------------------------------------------------------------------------

#region

using System;
using System.Windows.Forms;
using xDialog;

#endregion

namespace MessageBoxDemo
{
    public partial class frmDemo : Form
    {
        public frmDemo()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
			MessageBox.Show("Exiting now","dfdf\r\n343434");
			MsgBox.Show("Are you sure you want to exit?", "Eror", MsgBox.Buttons.OKCancel, MsgBox.Icon.Error);
            DialogResult result = 
				MsgBox.Show("Are you sure you want to exit?\r\nAre you sure you want to exit?Are you sure you want to exit?\r\nAre you sure you want to exit?Are you sure you want to exit?\r\nAre you sure you want to exit?Are you su\r\nre you\r\n want to exit?A\r\nre you s\r\nure you want to exit?Are you sure you want to exit?Are you sure you want to exit?",
				"Exit", MsgBox.Buttons.YesNoCancel,
                MsgBox.Icon.Shield);

            if (result == DialogResult.Yes)
            {
                MessageBox.Show("Exiting now");
            }
        }
    }
}