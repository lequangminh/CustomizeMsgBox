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
            MsgBox.Show("Are you sure you want to exit?", "Eror", MsgBox.Buttons.OKCancel, MsgBox.Icon.Error,
                MsgBox.AnimateStyle.ZoomIn);
            DialogResult result = MsgBox.Show("Are you sure you want to exit?", "Exit", MsgBox.Buttons.YesNoCancel,
                MsgBox.Icon.Shield, MsgBox.AnimateStyle.ZoomIn);

            if (result == DialogResult.Yes)
            {
                MessageBox.Show("Exiting now");
            }
        }
    }
}