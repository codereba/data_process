using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HtmlAgilityPack; 

namespace data_process
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            welcome_form form = new welcome_form();
            Application.Run(form);
          
            //form.ShowDialog();
            data_process _form = new data_process();
            Application.Run(_form);
            //_form.ShowDialog(); 
        }
    }
}
