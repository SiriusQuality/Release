using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SiriusView.LockerTrialVersion
{
    public partial class SerialBox : UserControl
    {
        private PassTextBox[] _Boxes = new PassTextBox[5];


        #region -> Behavior

        ///<summary>
        ///Indicate wich type of characters are acceptable
        ///</summary>
        [Category("Behavior"), DefaultValue(PassTextBox.TypeEntries.All),
            Description("Indicate wich type of characters are acceptable")]
        public PassTextBox.TypeEntries AcceptableChars
        {
            get
            {
                return _Boxes[0].AcceptableChars;
            }
            set
            {
                foreach (PassTextBox FT in _Boxes)
                    FT.AcceptableChars = value;
            }
        }

        ///<summary>
        ///Indicate Wich characters can't insert in texboxes
        ///</summary>
        [Category("Behavior"), DefaultValue(""),
            Description("Indicate Wich characters can't insert in texboxes")]
        public string ForbidenChars
        {
            get
            {
                return _Boxes[0].ForbidenChars;
            }
            set
            {
                foreach (PassTextBox FT in _Boxes)
                    FT.ForbidenChars = value;
            }
        }

        ///<summary>
        ///Only captle letters can insert
        ///</summary>
        [Category("Behavior"), DefaultValue(false),
            Description("Only captle letters can insert")]
        public bool CaptleLettersOnly
        {
            get
            {
                return _Boxes[0].CaptleLettersOnly;
            }
            set
            {
                foreach (PassTextBox FT in _Boxes)
                    FT.CaptleLettersOnly = value;
            }
        }

        ///<summary>
        ///The text is Readonly
        ///</summary>
        [Category("Behavior"), DefaultValue(false),
            Description("The text is Readonly")]
        public bool ReadOnly
        {
            get
            {
                return _Boxes[0].ReadOnly;
            }
            set
            {
                foreach (PassTextBox FT in _Boxes)
                    FT.ReadOnly = value;
            }
        }

        #endregion

        public SerialBox()
        {
            InitializeComponent();

            // Set Array of boxes for easier working
            _Boxes[0] = Box1;
            _Boxes[1] = Box2;
            _Boxes[2] = Box3;
            _Boxes[3] = Box4;
            _Boxes[4] = Box5;
        }

        #region -> Events

        private void TextBox_Enter(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            t.SelectAll();
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            PassTextBox ft = (PassTextBox)sender;
            if (ft.Text.Length == 5)
                this.SelectNextControl(ft, true, true, false, false);
        }

        #endregion

        #region -> Overrides

        public override RightToLeft RightToLeft
        {
            get
            {
                return base.RightToLeft;
            }
            set
            {
                base.RightToLeft = value;
                if (base.RightToLeft == RightToLeft.Yes)
                {
                    Box5.TabIndex = 0;
                    lbl4.TabIndex = 1;
                    Box4.TabIndex = 2;
                    lbl4.TabIndex = 3;
                    Box3.TabIndex = 4;
                    lbl4.TabIndex = 5;
                    Box2.TabIndex = 6;
                    lbl4.TabIndex = 7;
                    Box1.TabIndex = 8;
                }
                else
                {
                    Box5.TabIndex = 8;
                    lbl1.TabIndex = 7;
                    Box4.TabIndex = 6;
                    lbl1.TabIndex = 5;
                    Box3.TabIndex = 4;
                    lbl1.TabIndex = 3;
                    Box2.TabIndex = 2;
                    lbl1.TabIndex = 1;
                    Box1.TabIndex = 0;
                }
            }
        }

        [Browsable(true)]
        public override string Text
        {
            get
            {
                if (base.RightToLeft == RightToLeft.Yes)
                    return Box5.Text + Box4.Text + Box3.Text + Box2.Text + Box1.Text;
                else
                    return Box1.Text + Box2.Text + Box3.Text + Box4.Text + Box5.Text;
            }
            set
            {
                ClearBoxes();
                int len;
                for (int i = 0; i < value.Length && i < 25; i += 5)
                {
                    len = (i + 5) > value.Length ? value.Length - i : 5;
                    if (base.RightToLeft == RightToLeft.No)
                        _Boxes[i / 5].Text = value.Substring(i, len);
                    else
                        _Boxes[4 - (i / 5)].Text = value.Substring(i, len);
                }
            }
        }

        #endregion

        private void ClearBoxes()
        {
            foreach (PassTextBox ft in _Boxes)
                ft.Text = string.Empty;
        }
    }
}
