using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ted.MySnake
{
	partial class LevelSelectForm : Form
	{
		ASCIILevelTable mLevels;
		int mSelectedLevel = 1;
		int mSecretUnlock = 0;

		public LevelSelectForm(ASCIILevelTable levelTable)
		{
			InitializeComponent();

			mLevels = levelTable;
			UpdateSelectedLevelLabel();

			int thumbSize = 3;
			int rows = 4;
			int columns = (int)Math.Ceiling((float)mLevels.Count / 4.0f);
			int spacing = 2;
			int edgeSpacing = 6;

			Size size = GetLargestSize();
			for (int yi = 0; yi < columns; yi++) {
				for (int xi = 0; xi < rows; xi++) {
					int index = yi * rows + xi + 1;
					if (index >= mLevels.Count)
						break;

					Point location = new Point(edgeSpacing + (xi * size.Width * thumbSize) + (xi * spacing), edgeSpacing + (yi * size.Height * thumbSize) + (yi * spacing));
					Panel pnl = new Panel();
					pnl.Location = location;
					pnl.Size = new Size(size.Width * thumbSize, size.Height * thumbSize);
					pnl.BackgroundImage = mLevels[index].GetThumbnail(thumbSize);
					pnl.BackgroundImageLayout = ImageLayout.Center;
					pnl.Tag = index;
					pnl.MouseDown += new MouseEventHandler(pnl_MouseDown);

					string name = mLevels[index].Name.Replace('|', ' ');
					mainToolTip.SetToolTip(pnl, name);

					if (index == mSelectedLevel)
						pnl.BorderStyle = BorderStyle.Fixed3D;

					pnlLevels.Controls.Add(pnl);
				}
			}

			ClientSize = new Size(20 + (size.Width * thumbSize * rows) + (spacing * rows) + (2 * edgeSpacing), Size.Height);

			//ClientSize = new Size((size.Width * thumbSize * rows) + (spacing * rows) + (2 * edgeSpacing),
			//	(size.Height * thumbSize * columns) + (spacing * columns) + (spacing * edgeSpacing));
		}

		void pnl_MouseDown(object sender, MouseEventArgs e)
		{
			foreach (Panel p in pnlLevels.Controls) {
				p.BorderStyle = BorderStyle.None;
			}

			Panel pnl = (Panel)sender;
			pnl.BorderStyle = BorderStyle.Fixed3D;
			mSelectedLevel = (int)pnl.Tag;
			UpdateSelectedLevelLabel();
		}

		private void UpdateSelectedLevelLabel()
		{
			string text;
			if (mSelectedLevel == 0)
				text = "Secret";
			else
				text = mSelectedLevel.ToString();
			lblSelectedLevel.Text = "Starting Level: " + text;
		}

		private Size GetLargestSize()
		{
			Size max = new Size();
			foreach (ASCIILevel level in mLevels) {
				if (level.Size.Width > max.Width)
					max.Width = level.Size.Width;
				if (level.Size.Height > max.Height)
					max.Height = level.Size.Height;
			}

			return max;
		}

		private void lblSelectedLevel_DoubleClick(object sender, EventArgs e)
		{
			mSecretUnlock++;
			if (mSecretUnlock >= 5) {
				mSelectedLevel = 0;
				UpdateSelectedLevelLabel();
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		public int SelectedLevel
		{
			get
			{
				return mSelectedLevel;
			}
			set
			{
				mSelectedLevel = value;
				UpdateSelectedLevelLabel();
			}
		}
	}
}
