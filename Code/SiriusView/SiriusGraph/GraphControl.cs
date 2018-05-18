using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SiriusModel.InOut;
using WeifenLuo.WinFormsUI.Docking;

namespace SiriusView.Graph
{
    public partial class GraphControl : UserControl
    {
        public GraphControl()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            foreach (string graphID in Graph.GraphFormFactory.AllID)
            {
                this.listBox1.Items.Add(graphID);
            }
        }

        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                Graph.GraphFormFactory.This[(string)listBox1.SelectedItem].Show();
            }
        }

        public DockPanel GraphDockPanel
        {
            get { return graphDockPanel; }
        }
    }
}
