using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace data_process
{
    public partial class nlp_process_dlg : Form
    {
        public nlp_process_dlg()
        {
            InitializeComponent();
        }

        private void link_nlp_service_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string target = link_nlp_service.Text; 
            if (null != target )
                //&& target.StartsWith("http"))
            {
                System.Diagnostics.Process.Start("iexplore.exe", target);
            }
        }

        private void link_nlp_service_sec_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string target = "http://pan.baidu.com/share/home?uk=3191735299"; // link_nlp_service_sec.Text;
            if (null != target)
            //&& target.StartsWith("http"))
            {
                System.Diagnostics.Process.Start("iexplore.exe", target);
            }
        }
    }
}
