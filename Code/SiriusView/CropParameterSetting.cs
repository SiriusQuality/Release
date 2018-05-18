using SiriusModel.InOut;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SiriusView
{
    public partial class CropParameterSetting : Form
    {
        public CropParameterSetting()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            varietyItemsBindingSource1.DataSource = ProjectFile.This.FileContainer.VarietyFile.Items;
            nonVarietyItemsBindingSource1.DataSource = ProjectFile.This.FileContainer.NonVarietyFile.Items;

            var variety = varietyItemsBindingSource1.Current as SiriusModel.InOut.CropParameterItem;
            var nonVariety = nonVarietyItemsBindingSource1.Current as SiriusModel.InOut.CropParameterItem;

            foreach (var item in variety.ParamValue.Keys)
                this.parameterSelector1.Parameters.Selected.Add(item);

            foreach (var item in nonVariety.ParamValue.Keys)
                this.parameterSelector1.Parameters.NotSelected.Add(item);
        }

        ///<summary>
        ///Update the disposition of crop parameters (separation between varietal and non-varietal parameters
        ///</summary>
        ///<param name="sender"></param>
        ///<param name="e"></param>
        private void applyButton_Click(object sender, EventArgs e)
        {
            var variety = varietyItemsBindingSource1.Current as SiriusModel.InOut.CropParameterItem;
            List<string> oldVarietyList = variety.ParamValue.Keys.ToList<string>();

            foreach (var item in parameterSelector1.Parameters.Selected)
            {
                if (!oldVarietyList.Contains(item))
                {
                    // Delete the parameter of all non-variety and compute the new Value
                    double newValue = 0;

                    foreach (CropParameterItem current in nonVarietyItemsBindingSource1)
                    {
                        newValue += current.ParamValue[item];
                        current.ParamValue.Remove(item);
                    }

                    newValue /= nonVarietyItemsBindingSource1.Count;

                    // Add the parameter in all variety
                    foreach (CropParameterItem current in varietyItemsBindingSource1)
                        current.ParamValue.Add(item, newValue);                
                }

                // Delete the parameter in the temporary variety variable
                oldVarietyList.Remove(item);
            }

            // Les params restant dans variety temporaire
            for (int i = 0; i < oldVarietyList.Count; i++)
            {
                string item = oldVarietyList[i];
                // Delete the parameter of all non-variety
                double newValue = 0;

                foreach (CropParameterItem current in varietyItemsBindingSource1)
                {
                    newValue += current.ParamValue[item];
                    current.ParamValue.Remove(item);
                }

                newValue /= varietyItemsBindingSource1.Count;

                // Add the parameter in all non-variety

                foreach (CropParameterItem current in nonVarietyItemsBindingSource1)
                    current.ParamValue.Add(item, newValue);
            }
        }
    }
}
