﻿// Copyright (C) 2014-2015, phamtuan Research Inc.
//  
// All rights are reserved. Reproduction or transmission in whole or in part, in any form or by
// any means, electronic, mechanical or otherwise, is prohibited without the prior written
// consent of the copyright owner.
// ---------------------------------------------------------------------------------

#region

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

#endregion

namespace xDialog
{
	internal class MsgBox : Form
	{
		// Bóng đổ
		private const int CS_DROPSHADOW = 0x00020000;

		private static MsgBox _msgBox;

		// Header, Footer và Icon
		private Panel _plHeader = new Panel();
		private Label _lblTitle;
		private Panel _plFooter = new Panel();
		private Panel _plIcon = new Panel();
		private PictureBox _picIcon = new PictureBox();

		// THông điệp
		private Label _lblMessage;

		// Panel
		private FlowLayoutPanel _flpButtons = new FlowLayoutPanel();

		// List các button
		private List<Button> _buttonCollection = new List<Button>();

		// Kết quả
		private static DialogResult _buttonResult;

		private MsgBox()
		{
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			//this.BackColor = Color.FromArgb(45, 45, 48);
			this.BackColor = SystemColors.Window;
			this.StartPosition = FormStartPosition.CenterScreen;
			this.Padding = new System.Windows.Forms.Padding(3);
			this.Width = 400;

			// Header
			_lblTitle = new Label();
			_lblTitle.ForeColor = Color.Black;
			_lblTitle.Font = new System.Drawing.Font("Segoe UI", 18);
			_lblTitle.Dock = DockStyle.Top;
			_lblTitle.Height = 60;

			// Thông điệp
			_lblMessage = new Label();
			_lblMessage.ForeColor = Color.Black;
			_lblMessage.Font = new System.Drawing.Font("Segoe UI", 10);
			_lblMessage.Dock = DockStyle.Fill;

			_flpButtons.FlowDirection = FlowDirection.RightToLeft;
			_flpButtons.Dock = DockStyle.Fill;

			_plHeader.Dock = DockStyle.Fill;
			_plHeader.Padding = new Padding(20);
			_plHeader.Controls.Add(_lblMessage);
			_plHeader.Controls.Add(_lblTitle);

			_plFooter.Dock = DockStyle.Bottom;
			_plFooter.Padding = new Padding(20);
			_plFooter.BackColor = Color.FromArgb(37, 37, 38);
			_plFooter.Height = 80;
			_plFooter.Controls.Add(_flpButtons);

			_picIcon.Width = 32;
			_picIcon.Height = 32;
			_picIcon.Location = new Point(30, 50);

			_plIcon.Dock = DockStyle.Left;
			_plIcon.Padding = new Padding(20);
			_plIcon.Width = 70;
			_plIcon.Controls.Add(_picIcon);

			// Add controls vào form
			this.Controls.Add(_plHeader);
			this.Controls.Add(_plIcon);
			this.Controls.Add(_plFooter);
		}

		public static void Show(string message)
		{
			_msgBox = new MsgBox();
			_msgBox._lblMessage.Text = message;
			_msgBox.ShowDialog();
		}

		public static void Show(string message, string title)
		{
			_msgBox = new MsgBox();
			_msgBox._lblMessage.Text = message;
			_msgBox._lblTitle.Text = title;
			_msgBox.Size = MsgBox.MessageSize(message);
			_msgBox.ShowDialog();
		}

		public static DialogResult Show(string message, string title, Buttons buttons)
		{
			_msgBox = new MsgBox();
			_msgBox._lblMessage.Text = message;
			_msgBox._lblTitle.Text = title;
			_msgBox._plIcon.Hide();

			MsgBox.InitButtons(buttons);

			_msgBox.Size = MsgBox.MessageSize(message);
			_msgBox.ShowDialog();
			return _buttonResult;
		}

		public static DialogResult Show(string message, string title, Buttons buttons, Icon icon)
		{
			_msgBox = new MsgBox();
			_msgBox._lblMessage.Text = message;
			_msgBox._lblTitle.Text = title;

			MsgBox.InitButtons(buttons);
			MsgBox.InitIcon(icon);

			_msgBox.Size = MsgBox.MessageSize(message);
			_msgBox.ShowDialog();
			return _buttonResult;
		}

		private static void InitButtons(Buttons buttons)
		{
			switch (buttons)
			{
				case MsgBox.Buttons.AbortRetryIgnore:
					_msgBox.InitAbortRetryIgnoreButtons();
					break;

				case MsgBox.Buttons.OK:
					_msgBox.InitOKButton();
					break;

				case MsgBox.Buttons.OKCancel:
					_msgBox.InitOKCancelButtons();
					break;

				case MsgBox.Buttons.RetryCancel:
					_msgBox.InitRetryCancelButtons();
					break;

				case MsgBox.Buttons.YesNo:
					_msgBox.InitYesNoButtons();
					break;

				case MsgBox.Buttons.YesNoCancel:
					_msgBox.InitYesNoCancelButtons();
					break;
			}

			foreach (Button btn in _msgBox._buttonCollection)
			{
				btn.ForeColor = Color.FromArgb(170, 170, 170);
				btn.Font = new System.Drawing.Font("Segoe UI", 8);
				btn.Padding = new Padding(3);
				btn.FlatStyle = FlatStyle.Flat;
				btn.Height = 30;
				btn.FlatAppearance.BorderColor = Color.FromArgb(99, 99, 98);

				_msgBox._flpButtons.Controls.Add(btn);
			}
		}

		private static void InitIcon(Icon icon)
		{
			switch (icon)
			{
				case MsgBox.Icon.Application:
					_msgBox._picIcon.Image = SystemIcons.Application.ToBitmap();
					break;

				case MsgBox.Icon.Exclamation:
					_msgBox._picIcon.Image = SystemIcons.Exclamation.ToBitmap();
					break;

				case MsgBox.Icon.Error:
					_msgBox._picIcon.Image = SystemIcons.Error.ToBitmap();
					break;

				case MsgBox.Icon.Info:
					_msgBox._picIcon.Image = SystemIcons.Information.ToBitmap();
					break;

				case MsgBox.Icon.Question:
					_msgBox._picIcon.Image = SystemIcons.Question.ToBitmap();
					break;

				case MsgBox.Icon.Shield:
					_msgBox._picIcon.Image = SystemIcons.Shield.ToBitmap();
					break;

				case MsgBox.Icon.Warning:
					_msgBox._picIcon.Image = SystemIcons.Warning.ToBitmap();
					break;
			}
		}

		private void InitAbortRetryIgnoreButtons()
		{
			Button btnAbort = new Button();
			btnAbort.Text = "Abort";
			btnAbort.Click += ButtonClick;

			Button btnRetry = new Button();
			btnRetry.Text = "Retry";
			btnRetry.Click += ButtonClick;

			Button btnIgnore = new Button();
			btnIgnore.Text = "Ignore";
			btnIgnore.Click += ButtonClick;

			this._buttonCollection.Add(btnAbort);
			this._buttonCollection.Add(btnRetry);
			this._buttonCollection.Add(btnIgnore);
		}

		private void InitOKButton()
		{
			Button btnOK = new Button();
			btnOK.Text = "OK";
			btnOK.Click += ButtonClick;

			this._buttonCollection.Add(btnOK);
		}

		private void InitOKCancelButtons()
		{
			Button btnOK = new Button();
			btnOK.Text = "OK";
			btnOK.Click += ButtonClick;

			Button btnCancel = new Button();
			btnCancel.Text = "Cancel";
			btnCancel.Click += ButtonClick;


			this._buttonCollection.Add(btnOK);
			this._buttonCollection.Add(btnCancel);
		}

		private void InitRetryCancelButtons()
		{
			Button btnRetry = new Button();
			btnRetry.Text = "OK";
			btnRetry.Click += ButtonClick;

			Button btnCancel = new Button();
			btnCancel.Text = "Cancel";
			btnCancel.Click += ButtonClick;


			this._buttonCollection.Add(btnRetry);
			this._buttonCollection.Add(btnCancel);
		}

		private void InitYesNoButtons()
		{
			Button btnYes = new Button();
			btnYes.Text = "Yes";
			btnYes.Click += ButtonClick;

			Button btnNo = new Button();
			btnNo.Text = "No";
			btnNo.Click += ButtonClick;


			this._buttonCollection.Add(btnYes);
			this._buttonCollection.Add(btnNo);
		}

		private void InitYesNoCancelButtons()
		{
			Button btnYes = new Button();
			btnYes.Text = "Abort";
			btnYes.Click += ButtonClick;

			Button btnNo = new Button();
			btnNo.Text = "Retry";
			btnNo.Click += ButtonClick;

			Button btnCancel = new Button();
			btnCancel.Text = "Cancel";
			btnCancel.Click += ButtonClick;

			this._buttonCollection.Add(btnYes);
			this._buttonCollection.Add(btnNo);
			this._buttonCollection.Add(btnCancel);
		}

		private static void ButtonClick(object sender, EventArgs e)
		{
			Button btn = (Button) sender;

			switch (btn.Text)
			{
				case "Abort":
					_buttonResult = DialogResult.Abort;
					break;

				case "Retry":
					_buttonResult = DialogResult.Retry;
					break;

				case "Ignore":
					_buttonResult = DialogResult.Ignore;
					break;

				case "OK":
					_buttonResult = DialogResult.OK;
					break;

				case "Cancel":
					_buttonResult = DialogResult.Cancel;
					break;

				case "Yes":
					_buttonResult = DialogResult.Yes;
					break;

				case "No":
					_buttonResult = DialogResult.No;
					break;
			}

			_msgBox.Dispose();
		}

		private static Size MessageSize(string message)
		{
			Graphics g = _msgBox.CreateGraphics();
			int width = 350;
			int height = 230;

			SizeF size = g.MeasureString(message, new System.Drawing.Font("Segoe UI", 10));

			if (message.Length < 150)
			{
				if ((int) size.Width > 350)
				{
					width = (int) size.Width;
				}
			}
			else
			{
				string[] groups = (from Match m in Regex.Matches(message, ".{1,180}") select m.Value).ToArray();
				int lines = groups.Length + 1;
				width = 700;
				height += (int) (size.Height + 10)*lines;
			}
			return new Size(width, height);
		}

		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.ClassStyle |= CS_DROPSHADOW;
				return cp;
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			Graphics g = e.Graphics;
			Rectangle rect = new Rectangle(new Point(0, 0), new Size(this.Width - 1, this.Height - 1));
			Pen pen = new Pen(Color.FromArgb(0, 151, 251));

			g.DrawRectangle(pen, rect);
		}

		public enum Buttons
		{
			AbortRetryIgnore = 1,
			OK = 2,
			OKCancel = 3,
			RetryCancel = 4,
			YesNo = 5,
			YesNoCancel = 6
		}

		public enum Icon
		{
			Application = 1,
			Exclamation = 2,
			Error = 3,
			Warning = 4,
			Info = 5,
			Question = 6,
			Shield = 7,
			Search = 8
		}

		public enum AnimateStyle
		{
			SlideDown = 1,
			FadeIn = 2,
			ZoomIn = 3
		}
	}

	internal class AnimateMsgBox
	{
		public Size FormSize;
		public MsgBox.AnimateStyle Style;

		public AnimateMsgBox(Size formSize, MsgBox.AnimateStyle style)
		{
			this.FormSize = formSize;
			this.Style = style;
		}
	}
}