using System;
using System.Media;
using System.Text;
using System.Windows.Forms;

namespace SiriusView
{
    public partial class ErrorDialog : Form
    {
        #region Fields

        ///<summary>
        ///The caption of the error dialog.
        ///</summary>
        private readonly string caption;

        ///<summary>
        ///Format of the exception.
        ///</summary>
        private readonly string message;

        ///<summary>
        ///Stack trace of the message.
        ///</summary>
        private readonly string stackTrace;

        #endregion

        ///<summary>
        ///Create a new error dialog.
        ///</summary>
        public ErrorDialog()
            : this(null, null)
        {
        }

        ///<summary>
        ///Create a new error dialog.
        ///</summary>
        ///<param name="errorCaption">Error title.</param>
        ///<param name="errorException">Error exception.</param>
        public ErrorDialog(string errorCaption, Exception errorException)
        {
            InitializeComponent();
            caption = errorCaption;
            if (errorException != null)
            {
                stackTrace = errorException.StackTrace;

                var i = errorException;
                var messageBuidler = new StringBuilder();

                while (i != null)
                {
                    messageBuidler.AppendFormat("\n{0}\n", i.Message);
                    i = i.InnerException;
                }
                message = messageBuidler.ToString();
            }
            else
            {
                message = "?";
                stackTrace = "?";
            }
            
            Load += ErrorDialog_Load;
        }

        ///<summary>
        ///Show the error
        ///</summary>
        ///<param name="sender"></param>
        ///<param name="e"></param>
        void ErrorDialog_Load(object sender, EventArgs e)
        {
            SystemSounds.Beep.Play();
            Text = caption;
            textBox1.Text = message;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void buttonShowStackTrace_Click(object sender, EventArgs e)
        {
            MessageBox.Show(stackTrace, caption);
        }
    }
}
