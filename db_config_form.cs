using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Threading;

namespace data_process
{
    public partial class db_config_form : Form
    {
        DBConnect mysql_db;
        bool param_valid; 

        public int set_config( MYSQL_PARAM param )
        {
            this.text_server_name.Text = param.server_name; 
            this.text_db_name.Text = param.db_name;
            this.text_user_name.Text = param.user_name;
            this.text_port.Text = param.port; 
            this.text_password.Text = param.password;
            this.text_table_name.Text = param.table_name; //"finance_sina_com_cn";
            this.text_start_row.Text = param.start_row;
            if (text_start_row.Text.Length == 0)
            {
                text_start_row.Text = "0"; 
            }

            if (param.fields.Count > 0)
            {
                this.combo_column.Text = param.fields[0]; 
            }
            return 0; 
        }

        public db_config_form()
        {
            param_valid = false; 
            mysql_db = new DBConnect(); 
            InitializeComponent();
        }

        public bool param_is_valid()
        {
            return param_valid; 
        }

        public int get_input_paramters(ref MYSQL_PARAM param)
        {
            int ret = 0;
            
            do
            {
                param.db_name = text_db_name.Text;
                param.server_name = text_server_name.Text;
                param.user_name = text_user_name.Text;
                param.table_name = text_table_name.Text;
                param.password = text_password.Text;
                param.port = text_port.Text;
                param.start_row = text_start_row.Text;
                param.fields.Clear(); 
                param.fields.Add(combo_column.Text); 
            } while (false);
            return ret; 
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            int ret = 0; 
            do
            {
                if (combo_column.Text.Length == 0)
                {
                    break; 
                }

                ret = test_db_connect();
                if (ret != 0)
                {
                    break;
                }

                ret = test_db_table();
                if (ret != 0)
                {
                    break;
                }
            } while (false);

            if (ret == 0)
            {
                param_valid = true; 
                Close(); 
            }
            return; 
        }

        private int test_db_table()
        {
            int ret = 0;
            DataTable data_table; 

            do
            {
                if (text_table_name.Text.Length == 0)
                {
                    ret = -1; 
                    break;
                }

                data_table = new DataTable();

                ret = mysql_db.Select(text_table_name.Text, ref data_table);

                if (ret != 0)
                {
                    break;
                }

                if (data_table == null)
                {
                    break;
                }
            } while (false);
            return ret; 
        }

        private int test_db_connect()
        {
            int ret = 0; 

            do
            {
                if (0 == text_server_name.Text.Length)
                {
                    ret = -1; 
                    break;
                }

                if (0 == text_db_name.Text.Length)
                {
                    ret = -1; 
                    break;
                }

                if (0 == text_user_name.Text.Length)
                {
                    ret = -1; 
                    break;
                }

                ret = mysql_db.Initialize(text_server_name.Text, text_port.Text, text_db_name.Text, text_user_name.Text, text_password.Text);
                if (ret == 0)
                {

                    //MessageBox.Show("连接数据库成功!");
                    break;
                }

                //MessageBox.Show("连接数据库失败!");
            } while (false);
            return ret; 
        }

        private void btn_select_Click(object sender, EventArgs e)
        {
            int ret; 
            DataTable data_table; 

            do
            {
                ret = test_db_connect();
                if (ret != 0)
                {
                    break; 
                }

                if (text_table_name.Text.Length == 0)
                {
                    break;
                }

                data_table = new DataTable();

                ret = mysql_db.Select(text_table_name.Text, ref data_table);

                if (ret != 0)
                {
                    break;
                }

                if (data_table == null)
                {
                    break;
                }

                combo_column.Items.Clear(); 
                for (int i = 0;
                    i < data_table.Columns.Count;
                    i++)
                {
                    combo_column.Items.Add( data_table.Columns[ i ].ToString()); 
                }

                if (combo_column.Items.Count > 0)
                {
                    combo_column.Text = combo_column.Items[0].ToString(); 
                }
                //if (dgDisplay.Rows.Count > 0)
                //{
                //    dgDisplay.Rows.Clear();
                //}

                dgDisplay.DataSource = data_table; 
            } while (false); 
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Close(); 
        }

        private void db_config_form_Load(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            Int32 data_info_index;
            string file_path;

            do
            {
                //this.text_start_row.Text = param.start_row;
                if (text_server_name.Text.Length == 0)
                {
                    MessageBox.Show(string.Format("需配置MYSQL嗠器IP地址"));
                    break;
                }

                if (text_port.Text.Length == 0)
                {
                    MessageBox.Show(string.Format("需配置MYSQL嗠器端口号"));
                    break;
                }

                //if (text_db_name.Text.Length == 0)
                //{
                //    MessageBox.Show(string.Format("需配置数据库名"));
                //    break;
                //}
                //if (text_user_name.Text.Length == 0)
                //{
                //    MessageBox.Show(string.Format("需配置用户名"));
                //    break;
                //}
                //if (text_password.Text.Length == 0)
                //{
                //    MessageBox.Show(string.Format("需配置密码"));
                //    break;
                //}
                //if (text_table_name.Text.Length == 0)
                //{
                //    MessageBox.Show(string.Format("需配置表名"));
                //    break;
                //}

                //if (combo_column.Text.Length == 0)
                //{
                //    MessageBox.Show(string.Format("需配置字段名"));
                //    break;
                //}

                SaveFileDialog dlg = new SaveFileDialog();
                int path_delim; 

                string path = Application.ExecutablePath; 
                path_delim = path.LastIndexOf('\\');

                dlg.FileName = "db_config";
                dlg.DefaultExt = "xml";
                dlg.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";
                dlg.DefaultExt = "xml"; 
                dlg.InitialDirectory = path.Substring(0, path_delim); 

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    file_path = dlg.FileName;
                }
                else
                {
                    break;
                }

                if (file_path.Length == 0)
                {
                    break;
                }

                XmlDocument xmlDoc = new XmlDocument();
                XmlNode root_node;
                XmlAttribute node_attr;
                XmlNode info_node;

                //创建Xml声明部分，即<?xml version="1.0" encoding="utf-8" ?>
                xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes");

                //创建根节点
                root_node = xmlDoc.CreateElement("data_params");
                if (null == root_node)
                {
                    break;
                }

                info_node = xmlDoc.CreateElement("mysql_params");
                if (null == info_node)
                {
                    break;
                }

                node_attr = xmlDoc.CreateAttribute("db_name");
                node_attr.Value = text_db_name.Text;
                info_node.Attributes.Append(node_attr);

                node_attr = xmlDoc.CreateAttribute("table_name");
                node_attr.Value = text_table_name.Text;
                info_node.Attributes.Append(node_attr);

                node_attr = xmlDoc.CreateAttribute("user_name");
                node_attr.Value = text_user_name.Text;
                info_node.Attributes.Append(node_attr);

                node_attr = xmlDoc.CreateAttribute("server_name");
                node_attr.Value = text_server_name.Text;
                info_node.Attributes.Append(node_attr);

                node_attr = xmlDoc.CreateAttribute("port");
                node_attr.Value = text_port.Text;
                info_node.Attributes.Append(node_attr);

                node_attr = xmlDoc.CreateAttribute("password");
                node_attr.Value = text_password.Text;
                info_node.Attributes.Append(node_attr);

                node_attr = xmlDoc.CreateAttribute("start_row");
                node_attr.Value = text_start_row.Text;
                info_node.Attributes.Append(node_attr);

                root_node.AppendChild(info_node);

                do
                {

                    info_node = xmlDoc.CreateElement("field");
                    if (null == info_node)
                    {
                        break;
                    }

                    node_attr = xmlDoc.CreateAttribute("name");
                    node_attr.Value = combo_column.Text;
                    info_node.Attributes.Append(node_attr);

                    root_node.AppendChild(info_node);
                } while (false);

                //附加根节点
                xmlDoc.AppendChild(root_node);
                //保存Xml文档
                xmlDoc.Save(file_path);
                Console.WriteLine("data information is saved");
            } while (false);
        }

        int save_main_config(string last_config)
        {
            int ret = 0;
            string file_path;

            do
            {
                if (last_config.Length == 0)
                {
                    ret = -1;
                    break;
                }

                file_path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                file_path += "main.xml";

                XmlDocument xmlDoc = new XmlDocument();
                XmlNode root_node;
                XmlAttribute node_attr;
                XmlNode info_node;

                //创建Xml声明部分，即<?xml version="1.0" encoding="utf-8" ?>
                xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes");

                //创建根节点
                root_node = xmlDoc.CreateElement("main");
                if (null == root_node)
                {
                    break;
                }

                node_attr = xmlDoc.CreateAttribute("last_config");
                node_attr.Value = last_config;
                root_node.Attributes.Append(node_attr);

                xmlDoc.AppendChild(root_node);
                xmlDoc.Save(file_path);
            } while (false);
            return ret;
        }

        int load_mysql_param(string file_path)
        {
            int ret = 0;
            Int32 data_info_index;

            do
            {
                XmlDocument xmlDoc = new XmlDocument();
                //XmlAttribute node_attr;
                XmlNode info_node;
                XmlNodeList info_nodes;

                if (file_path.Length == 0)
                {
                    ret = -1;
                    break;
                }

                if (File.Exists(file_path) == false)
                {
                    ret = -1;
                    break;
                }

                xmlDoc.Load(file_path);


                //使用xpath表达式选择文档中所有的student子节点
                info_node = xmlDoc.SelectSingleNode("/data_params/mysql_params");

                if (null == info_node)
                {
                    ret = -1;
                    break;
                }

                text_db_name.Text = info_node.Attributes["db_name"].InnerText;
                text_server_name.Text = info_node.Attributes["server_name"].InnerText;
                text_port.Text = info_node.Attributes["port"].InnerText;
                text_user_name.Text = info_node.Attributes["user_name"].InnerText;
                text_password.Text = info_node.Attributes["password"].InnerText;
                text_table_name.Text = info_node.Attributes["table_name"].InnerText;
                text_start_row.Text = info_node.Attributes["start_row"].InnerText;

                info_nodes = xmlDoc.SelectNodes("/data_params/field");

                if (info_nodes.Count == 0)
                {
                    ret = -1;
                    break;
                }

                do
                {
                    string field_name;
                    //创建student子节点
                    info_node = info_nodes.Item(0);
                    if (null == info_node)
                    {
                        ret = -1;
                        break;
                    }


                    field_name = info_node.Attributes["name"].InnerText;
                    combo_column.Text = field_name;
                } while (false);

                save_main_config(file_path);
                Console.WriteLine("data information is loaded");
            } while (false);
            return ret;
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            string file_path;
            OpenFileDialog dlg = new OpenFileDialog();

            do
            {
                int path_delim; 
                string path = Application.ExecutablePath;
                path_delim = path.LastIndexOf('\\');

                dlg.FileName = "db_config";
                dlg.DefaultExt = "xml";
                dlg.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";
                dlg.DefaultExt = "xml";
                dlg.InitialDirectory = path.Substring(0, path_delim); 

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    file_path = dlg.FileName;
                }
                else
                {
                    break;
                }

                if (file_path.Length == 0)
                {
                    break;
                }

                load_mysql_param(file_path);

            } while (false);
        }
    }
}
