using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SiriusModel.InOut;

namespace SiriusView
{
    public partial class ParameterSelector : UserControl
    {
        public ParameterSelector()
        {
            InitializeComponent();
            parameters = new Parameters();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            groupName.Text = GrouName;
            OnParametersChanged();
        }

        public string GrouName
        {
            get { return groupName.Text; }
            set { groupName.Text = value; }
        }

        private Parameters parameters;
        public Parameters Parameters
        {
            get { return parameters; }
            set { parameters = value; OnParametersChanged(); }
        }

        private void OnParametersChanged()
        {
            if (parameters == null)
            {
                notSelected.DataSource = null;
                selected.DataSource = null;
                UpdateListBoxes();
                return;
            }
            notSelected.DataSource = parameters.NotSelected;
            selected.DataSource = parameters.Selected;
            UpdateListBoxes();
        }

        private void select_Click(object sender, EventArgs e)
        {
            var item = notSelected.SelectedItem;
            if (item == null) return;
            parameters.Select(item.ToString());
        }

        private void deselect_Click(object sender, EventArgs e)
        {
            var item = selected.SelectedItem;
            if (item == null) return;
            parameters.Unselect(item.ToString());
        }

        private void notSelected_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateListBoxes();
        }

        private void selected_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateListBoxes();
        }

        private void UpdateListBoxes()
        {
            select.Enabled = notSelected.SelectedItems.Count != 0;
            deselect.Enabled = selected.SelectedItems.Count != 0;
        }
    }

    ///<summary>
    ///manages all the parameters (definition, selection, serialization)
    ///</summary>
    public class Parameters
    {
        private BindingList<string> selected;       // These lists allow you to select
        private BindingList<string> notSelected;    // (or not) a parameter

        #region Attributes (lists)
        public BindingList<string> Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;
            }
        }
       
        public BindingList<string> NotSelected
        {
            get
            {
                return notSelected;
            }
            set
            {
                notSelected = value;
            }
        }    
        #endregion

        #region Constructor
        public Parameters()
        {
            selected = new BindingList<string>();
            notSelected = new BindingList<string>();
        }
        #endregion

        #region Selection methodes
        ///<summary>
        ///Place in selected parameters list the selected parameter and remove it of the unselected parameter list
        ///</summary>
        ///<param name="parameterName"></param>
        public void Select(string parameterName)
        {
            if (NotSelected.Contains(parameterName))
            {
                Selected.Add(parameterName);
                NotSelected.Remove(parameterName);
            }
        }

        ///<summary>
        ///Place in unselected parameters list the parameter and remove it of the selected parameter list
        ///</summary>
        ///<param name="parameterName"></param>
        public void Unselect(string parameterName)
        {
            if (Selected.Contains(parameterName))
            {
                NotSelected.Add(parameterName);
                Selected.Remove(parameterName);
            }
        }
        #endregion
    }
}
