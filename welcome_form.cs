using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using System.IO;
using System.Configuration;
using System.Reflection; 

namespace data_process
{
    public partial class welcome_form : Form
    {
        //private int create_resource()
        //{
        //    int ret = 0;
        //    StreamReader welcome_text; 

        //    do
        //    {
        //        ResourceWriter rw = new ResourceWriter("data_process.res");
        //        rw.Generate();

        //        welcome_text = File.OpenText(".\\welcom.txt");
        //        rw.AddResource("welcome", "123");
        //        rw.Generate();
        //        rw.Close(); 
        //    } while (false);
        //    return ret; 
        //}

        public static Int32 notify_program_starting()
        {
            Int32 ret = 0; 
            const string POST_USER_INFO_URL = "http://www.simplestai.com/webkits/post_info.php";

            {
                TO_SERVER_INFO[] info;
                info = new TO_SERVER_INFO[1];
                info[0].name = "info";
                info[0].value = "starting";
                http_comm.to_server(POST_USER_INFO_URL, ref info);
            }

            return ret; 
        }

        public welcome_form()
        {
            InitializeComponent();
            Assembly assembly =  Assembly.GetExecutingAssembly(); 
            //create_resource(); 

            do
            {
                StartPosition = FormStartPosition.CenterScreen; 

                try
                {
                    //string[] resNames = assembly.GetManifestResourceNames();
                    //if (resNames.Length == 0)
                    //    Console.WriteLine("   No resources found.");

                    //foreach (var resName in resNames)
                    //    Console.WriteLine("   Resource: {0}", resName.Replace(".resources", ""));

                    ResourceManager rm = new ResourceManager("data_process.resource", assembly);

                    object res = rm.GetObject("welcome");
                    if (res == null)
                    {
                        break;
                    }
                    rich_text_welcome.Text = res.ToString();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("load resource error{0}\n", ex.Message);
                }

                notify_program_starting(); 
            } while (false); 
        }
    }
}
