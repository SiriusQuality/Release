using System;
using System.Windows.Forms;
using SiriusModel.InOut;

namespace SiriusView
{
    public partial class AboutSiriusQuality : Form
    {
        public AboutSiriusQuality()
        {
            InitializeComponent();
            labelVersion.Text = ProjectFile.Version;
            labelBuild.Text = ProjectFile.Build;
        }

        private void buttonOK_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        /*private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:pierre.stratonovitch@rothamsted.ac.uk?subject=SiriusQuality&body=Sirius Quality build:" + ProjectFileWithOpt.Build);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:pierre.martre@clermont.inra.fr?subject=SiriusQuality&body=Sirius Quality build:" + ProjectFileWithOpt.Build);
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }*/
    }
}
