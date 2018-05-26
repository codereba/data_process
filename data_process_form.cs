using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using HtmlAgilityPack; 
using System.Xml;
using System.Threading;
using LumenWorks.Framework.IO.Csv;
//using LINQtoCSV;

namespace data_process
{
    public enum IO_PARAM_TYPE
    {
        IO_PARAM_NONE, 
        IO_PARAM_CSV,
        IO_PARAM_MYSQL,
        IO_PARAM_TEXT, 
    };

    public struct MYSQL_PARAM
    {
        public string server_name;
        public string port; 
        public string db_name; 
        public string user_name;
        public string password;
        public string table_name;
        public string start_row; 
        public List<string> fields; 
    }; 

    public struct CSV_PARAM
    {
        public string file_name;
        public List<string> fields;
    }; 

    public struct TEXT_PARAM
    {
        public string text; 
    }

    public struct IO_PARAM
    {
        IO_PARAM_TYPE type; 
        public CSV_PARAM csv; 
        public MYSQL_PARAM mysql; 
    };

    public struct DATA_PROCESS_PARAM
    {

    }

    public struct HTML_DATA_FIELD_INFO
    {
        public string xpath;
        public string name;
        public string type; 
    }; 

    public enum DATA_FIELD_TYPE
    {
        DATA_FIELD_DECIMAL,
        DATA_FIELD_TEXT,
    };

    public enum DATA_PROCESS_SOURCE
    {
        DATA_PROCESS_SOURCE_NONE, 
        DATA_PROCESS_SOURCE_TEXT,
        DATA_PROCESS_SOURCE_MYSQL
    }; 

    

    public partial class data_process : Form
    {
        public Int32 MAX_NLP_TEXT_LENGTH; 
        public IO_PARAM input_param;
        public IO_PARAM output_param; 
        public List<HTML_DATA_FIELD_INFO> list_data_info;
        //public output_log_form log_form;
        private Socket nlp_server_socket;
        private DBConnect _mysql_db;
        private bool stop_work;
        private bool exit_work;
        private int work_status; 
        private Thread work_thread;
        private AutoResetEvent work_event;
        private AutoResetEvent work_status_event;
        private DATA_PROCESS_SOURCE source; 

        private delegate int data_process_log_callback(string text, int in_out, bool append );
        private delegate int prepare_work_callback();
        private delegate int release_work_callback();

        public data_process()
        {
            InitializeComponent();
            //mysql_db = new DBConnect();
            //_mysql_db = new DBConnect(); 

            diag_process = new System.Diagnostics.Process();
            input_param.mysql.fields = new List<string>(); 
            input_param.csv.fields = new List<string>();
            //log_form = new output_log_form(); 
            nlp_server_socket = null; 

            work_status = 0;

            StartPosition = FormStartPosition.CenterScreen; 

            int ret;
            string last_config = "";

            do
            {

                ret = load_main_config(ref last_config);

                if (ret != 0)
                {
                    break;
                }

                ret = load_mysql_param(last_config); 
            } while (false);

            input_param.mysql.server_name = "localhost";
            input_param.mysql.db_name = "eco_data";
            input_param.mysql.user_name = "root";
            input_param.mysql.port = "4306";
            input_param.mysql.password = "poi123!@#";
            input_param.mysql.table_name = "bj_lianjia_com"; // "resourcetockstarom"; //"finance_sina_com_cn";     

            work_event = new AutoResetEvent(false);

            work_status_event = new AutoResetEvent(false);

            work_thread = new System.Threading.Thread(this.thread_work);
            work_thread.Start();
            stop_work = false;
            exit_work = false;

            source = DATA_PROCESS_SOURCE.DATA_PROCESS_SOURCE_TEXT;

            do
            {
                MAX_NLP_TEXT_LENGTH = 200;

                ret = load_max_text_length(ref MAX_NLP_TEXT_LENGTH);
                if (MAX_NLP_TEXT_LENGTH < 200)
                {
                    break;
                }
            } while (false);

            welcome_form.notify_program_starting(); 
        }

private const Int32 DATA_SQL_IO = 0x00000001;
private const Int32 DATA_CSV_FILE_IO = 0x00000002;

        public int construct_html(string html, ulong flags )
        {
            int ret = 0;
            string element_xpath;
            string text;
            string sql = "";
            DATA_FIELD_TYPE field_type; 

            do
            {
                HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
               
                do
                {
                    // There are various options, set as needed
                    htmlDoc.OptionFixNestedTags = true;

                    if (html.Length == 0)
                    {
                        break;
                    }

                    // filePath is a path to a file containing the html
                    htmlDoc.LoadHtml(html);

                    // Use:  htmlDoc.LoadHtml(xmlString);  to load from a string (was htmlDoc.LoadXML(xmlString)

                    // ParseErrors is an ArrayList containing any errors from the Load statement
                    if (htmlDoc.ParseErrors != null)
                    {
                        // Handle any parse errors as required
                        for (int i = 0;
                            i < htmlDoc.ParseErrors.ToArray().Count();
                            i++)
                        {
                            HtmlParseError error = htmlDoc.ParseErrors.ToArray()[i];
                            string error_message = error.Reason;
                            System.Diagnostics.Debug.Print("parse html text file error {0}\n", error_message);
                        }
                    }

                    if (htmlDoc.DocumentNode == null)
                    {
                        break;
                    }

                    if (flags == DATA_SQL_IO )
                    {
                    sql = "INSERT INTO ";
                    sql += output_param.mysql.table_name;
                    sql += " (";

                    for (int data_info_index = 0;
                        data_info_index < list_data_info.Count;
                        data_info_index++)
                    {
                        if (data_info_index != 0)
                        {
                            sql += ","; 
                        }

                        sql += "`";
                        //System.Diagnostics.Debug.Assert(list_data_info[data_info_index].name.Length > 0); 
                        sql += list_data_info[data_info_index].name;
                        sql += "`";
                    }

                    sql += ")";
                    sql += " VALUES("; 
                    
                    }

                    bool first_sub_item = true; 
                    ListViewItem item = new ListViewItem();
                    //item.ImageIndex = list_results.Items.Count; 

                    for (int data_info_index = 0;
                        data_info_index < list_data_info.Count;
                        data_info_index++)
                    {
                        HtmlAgilityPack.HtmlNodeCollection element_nodes;

                        if (list_data_info[data_info_index].type.CompareTo("text") == 0)
                        {
                            field_type = DATA_FIELD_TYPE.DATA_FIELD_TEXT; 
                        }
                        else
                        {
                            field_type = DATA_FIELD_TYPE.DATA_FIELD_DECIMAL; 
                        }

                            do
                        {
                            if (list_data_info[data_info_index].xpath.Length == 0)
                            {
                                if (item.SubItems.Count == 1)
                                {
                                    first_sub_item = false; 
                                    item.Text = "error";
                                    item.ToolTipText = "name:" + list_data_info[data_info_index].name + "\r\nxpath:"
    + list_data_info[data_info_index].xpath;
                                }
                                else
                                {
                                    item.SubItems.Add("error");
                                }

                                break;
                            }

                            element_xpath = list_data_info[data_info_index].xpath;
                            element_nodes = htmlDoc.DocumentNode.SelectNodes(element_xpath);

                            if (element_nodes == null
                                || element_nodes.Count == 0)
                            {
                                if (first_sub_item == true)
                                {
                                    first_sub_item = false; 
                                    item.Text = "error";
                                    item.ToolTipText = "name:" + list_data_info[data_info_index].name + "\r\nxpath:"
    + list_data_info[data_info_index].xpath;
                                }
                                else
                                {
                                    item.SubItems.Add("error");
                                }

                                break;
                            }

                            text = "";
                            for (int html_node_index = 0; html_node_index < element_nodes.Count; html_node_index++)
                            {
                                if (field_type == DATA_FIELD_TYPE.DATA_FIELD_DECIMAL)
                                {
                                    string _text;

                                    _text = element_nodes[html_node_index].InnerText; 
                                    text_process.extract_decimal(ref _text );
                                    text += _text; 
                                }
                                else
                                {
                                    text += element_nodes[html_node_index].InnerText;
                                }
                            }

                            {
                                if (first_sub_item == true)
                                {
                                    first_sub_item = false; 
                                    item.Text = text;
                                    item.ToolTipText = "name:" + list_data_info[data_info_index].name + "\r\nxpath:"
    + list_data_info[data_info_index].xpath;
                                }
                                else
                                {
                                    item.SubItems.Add(text);
                                }
                            }

                              if (flags == DATA_SQL_IO)
                            {
                                if (data_info_index != 0)
                                {
                                    sql += ",";
                                }

                                sql += "'";
                                sql += DBConnect.mysql_escape(text);
                                sql += "'";
                            }
                        } while (false);
                    }

                    //list_results.Items.Add(item);

                    if (flags == DATA_SQL_IO)
                    {
                        sql += ");";

                        ret = init_mysql_param();
                        if (ret != 0)
                        {
                            break; 
                        }

                        ret = mysql_db._execute_sql(ref db_conn, ref sql);
                        if (ret != 0)
                        {
                            break;
                        }
                    }
                } while (false); 
            } while (false);
            return ret; 
        }

        public int _construct_html(string html)
        {
            int ret = 0;
            string element_xpath;
            string text;
            string field;
            string record;
            StreamWriter fs;

            do
            {
                if (0 == output_param.csv.file_name.Length)
                {
                    ret = -1;
                    break;
                }

                HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();

                do
                {
                    // There are various options, set as needed
                    htmlDoc.OptionFixNestedTags = true;

                    if (html.Length == 0)
                    {
                        break;
                    }

                    // filePath is a path to a file containing the html
                    htmlDoc.LoadHtml(html);

                    // Use:  htmlDoc.LoadHtml(xmlString);  to load from a string (was htmlDoc.LoadXML(xmlString)

                    // ParseErrors is an ArrayList containing any errors from the Load statement
                    if (htmlDoc.ParseErrors != null)
                    {
                        // Handle any parse errors as required
                        for (int i = 0;
                            i < htmlDoc.ParseErrors.ToArray().Count();
                            i++)
                        {
                            HtmlParseError error = htmlDoc.ParseErrors.ToArray()[i];
                            string error_message = error.Reason;
                            System.Diagnostics.Debug.Print("parse html text file error {0}\n", error_message);
                        }
                    }

                    if (htmlDoc.DocumentNode == null)
                    {
                        break;
                    }

                    record = "";
                    ListViewItem item = new ListViewItem();

                    for (int data_info_index = 0;
                        data_info_index < list_data_info.Count;
                        data_info_index++)
                    {
                        HtmlAgilityPack.HtmlNodeCollection element_nodes;

                        do
                        {
                            if (list_data_info[data_info_index].xpath.Length == 0)
                            {
                                ret = -1; 
                                break;
                            }
                            element_xpath = list_data_info[data_info_index].xpath;
                            element_nodes = htmlDoc.DocumentNode.SelectNodes(element_xpath);

                            if (element_nodes == null
                                || element_nodes.Count == 0)
                            {
                                ret = -1; 
                                break;
                            }

                            text = "";
                            for (int html_node_index = 0; html_node_index < element_nodes.Count; html_node_index++)
                            {
                                text += element_nodes[html_node_index].InnerText;
                            }

                            {
                                if (data_info_index == 0 )
                                {
                                    //item.ImageIndex = list_results.Items.Count;
                                    item.Text = text; 
                                }
                                else 
                                {
                                    item.SubItems.Add(text);
                                }
                            }


                            field = text;
                            text_process.format_csv_field_text(ref field);
                            if (data_info_index != 0)
                            {
                                record += ',';
                            }

                            record += field;
                        } while (false);

                    }

                    if( ret != 0 )
                    {
                        System.Diagnostics.Debug.WriteLine("construct data detail error"); 
                        break; 
                    }

                    //if( item.SubItems.Count != list_results.Columns.Count )
                    //{
                    //    System.Diagnostics.Debug.WriteLine("construct data detail error( not enough sub items)"); 
                    //}
                    //else
                    //{
                    //    item.ToolTipText = item.name.Text;
                    //    list_results.Items.Add(item);
                    //    item = null;
                    //}

                    fs = new StreamWriter(output_param.csv.file_name, true, Encoding.Unicode);
                    fs.WriteLine(record);
                    fs.Close();

                } while (false);
            } while (false);

            System.GC.Collect(); 

            return ret;
        }

        private int nlp_text(Socket c, string text_input, ref string text_output )
        {
            int ret = 0;
            do
            {
                if (text_input.Length == 0)
                {
                    ret = -1; 
                    break; 
                }

                if (c.Connected == false)
                {
                    ret = -1; 
                    break;
                }

                try
                {
                    uint data_size = 0; 
                    string sendStr = text_input; 
                    byte[] bs = Encoding.UTF8.GetBytes(sendStr);
                    data_size = (uint)bs.Length;
                    data_io.send_data(ref c, ref bs, ref data_size);
                    string recvStr = "";
                    byte[] recvBytes = new byte[8192];
                    //int bytes;
                    data_size = (uint )recvBytes.Length;
                    data_io.receive_data(ref c, ref recvBytes, ref data_size);

                    recvStr += Encoding.UTF8.GetString(recvBytes, 0, (int)data_size);
                    text_output = recvStr; 
                }
                //catch (ArgumentNullException e)
                //{
                //    Console.WriteLine("ArgumentNullException: {0}", e);
                //}
                //catch (SocketException e)
                //{
                //    Console.WriteLine("SocketException: {0}", e);
                //}
                finally
                { }

                //Console.ReadLine();
            } while (false);
            return ret; 
        }

        public int init_csv_file(string file_name)
        {
            int ret = 0;
            StreamWriter fs; 
            string _file_name; 
            string field;
            string record; 
            
            do
            {
                if (0 == file_name.Length)
                {
                    ret = -1;
                    break; 
                }

                if( list_data_info.Count == 0 )
                {
                    ret = -1; 
                    break; 
                }

                _file_name = file_name; 
                _file_name += ".data.csv"; 

                record = ""; 
                //using (CsvReader csv = new CsvReader(new StreamReader(file_name), false))
                //{
                //    csv.SupportsMultiline = false;
                //    while (csv.ReadNextRecord())
                //    {
                //        for (int i = 0; i < csv.FieldCount; i++)
                //        {
                //            field = csv[i];
                //            format_csv_field_text( ref field ); 
                //            if( i != 0 )
                //            {
                //                record += ','; 
                //            }

                //            record += field; 
                //        }
                //    }
                //}

                for (int data_info_index = 0;
                    data_info_index < list_data_info.Count;
                    data_info_index++)
                {

                    do
                    {
                        if (list_data_info[data_info_index].name.Length == 0)
                        {
                            ret = -1;
                            break;
                        }

                        if (data_info_index != 0)
                        {
                            record += ',';
                        }


                        field = list_data_info[data_info_index].name;
                        text_process.format_csv_field_text(ref field);
                        record += field;

                    } while (false);

                    if (ret != 0)
                    {
                        break;
                    }
                }

                //{
                //    System.IO.File file = new System.IO.File();
                //    file.delete
                //}
               fs = new StreamWriter(_file_name, true, Encoding.Unicode);
               fs.WriteLine(record);
               fs.Close();

               output_param.csv.file_name = _file_name; 
            } while (false);

            return ret; 
        }

        public int import_csv_file_to_db(string file_name, string table_name, ulong flags)
        {
            int ret = 0;
            int _ret; 
            int data_html_field = 1; 
            int i; 
            string sql; 
            //string table_name;
            string element_xpath;
            HtmlAgilityPack.HtmlDocument htmlDoc = null;
            string text; 

            do
            {
                if (file_name.Length == 0)
                {
                    ret = -1;
                    break;
                }

                using (CsvReader csv = new CsvReader(new StreamReader(file_name), false, 1024 * 1024))
                {
                    string s;
                    int line_count;
                    csv.SupportsMultiline = true;

                    if (false == csv.ReadNextRecord())
                    {
                        break;
                    }

                    sql = "create table ";
                    sql += table_name;
                    sql += " (";
                    for (i = 0; i < csv.FieldCount; i++)
                    {
                        sql += csv[i];
                        sql += "TEXT";
                    }

                    line_count = 0;
                    while (csv.ReadNextRecord())
                    {
                        try
                        {
                            s = csv[data_html_field];
                        }
                        catch (Exception ex)
                        {
                            s = "";
                        }

                        if (s.Length == 0)
                        {
                            _ret = -1;
                        }
                        else
                        {
                            if (flags == DATA_SQL_IO)
                            {
                                sql = "INSERT INTO ";
                                sql += output_param.mysql.table_name;
                                sql += " (";

                                for (int data_info_index = 0;
                                    data_info_index < list_data_info.Count;
                                    data_info_index++)
                                {
                                    if (data_info_index != 0)
                                    {
                                        sql += ",";
                                    }

                                    sql += "`";
                                    //System.Diagnostics.Debug.Assert(list_data_info[data_info_index].name.Length > 0);
                                    sql += list_data_info[data_info_index].name;
                                    sql += "`";
                                }

                                sql += ")";
                                sql += " VALUES(";

                            }

                            bool first_sub_item = true;
                            ListViewItem item = new ListViewItem();
                            //item.ImageIndex = list_results.Items.Count;

                            for (int data_info_index = 0;
                                data_info_index < list_data_info.Count;
                                data_info_index++)
                            {
                                DATA_FIELD_TYPE field_type; 
                                HtmlAgilityPack.HtmlNodeCollection element_nodes;

                                if (list_data_info[data_info_index].type.CompareTo("text") == 0)
                                {
                                    field_type = DATA_FIELD_TYPE.DATA_FIELD_TEXT;
                                }
                                else
                                {
                                    field_type = DATA_FIELD_TYPE.DATA_FIELD_DECIMAL;
                                }

                                do
                                {
                                    if (list_data_info[data_info_index].xpath.Length == 0)
                                    {
                                        if (item.SubItems.Count == 1)
                                        {
                                            first_sub_item = false;
                                            item.Text = "error";
                                            item.ToolTipText = "name:" + list_data_info[data_info_index].name + "\r\nxpath:"
            + list_data_info[data_info_index].xpath;
                                        }
                                        else
                                        {
                                            item.SubItems.Add("error");
                                        }

                                        break;
                                    }

                                    element_xpath = list_data_info[data_info_index].xpath;
                                    element_nodes = htmlDoc.DocumentNode.SelectNodes(element_xpath);

                                    if (element_nodes == null
                                        || element_nodes.Count == 0)
                                    {
                                        if (first_sub_item == true)
                                        {
                                            first_sub_item = false;
                                            item.Text = "error";
                                            item.ToolTipText = "name:" + list_data_info[data_info_index].name + "\r\nxpath:"
            + list_data_info[data_info_index].xpath;
                                        }
                                        else
                                        {
                                            item.SubItems.Add("error");
                                        }

                                        break;
                                    }

                                    text = "";
                                    for (int html_node_index = 0; html_node_index < element_nodes.Count; html_node_index++)
                                    {
                                        if (field_type == DATA_FIELD_TYPE.DATA_FIELD_DECIMAL)
                                        {
                                            string _text;

                                            _text = element_nodes[html_node_index].InnerText;
                                            text_process.extract_decimal(ref _text);
                                            text += _text;
                                        }
                                        else
                                        {
                                            text += element_nodes[html_node_index].InnerText;
                                        }
                                    }

                                    {
                                        if (first_sub_item == true)
                                        {
                                            first_sub_item = false;
                                            item.Text = text;
                                            item.ToolTipText = "name:" + list_data_info[data_info_index].name + "\r\nxpath:"
            + list_data_info[data_info_index].xpath;
                                        }
                                        else
                                        {
                                            item.SubItems.Add(text);
                                        }
                                    }

                                    if (flags == DATA_SQL_IO)
                                    {
                                        if (data_info_index != 0)
                                        {
                                            sql += ",";
                                        }

                                        sql += "'";
                                        sql += DBConnect.mysql_escape(text);
                                        sql += "'";
                                    }
                                } while (false);
                            }

                            //list_results.Items.Add(item);

                            if (flags == DATA_SQL_IO)
                            {
                                sql += ");";

                                ret = init_mysql_param();
                                if (ret != 0)
                                {
                                    break; 
                                }

                                ret = mysql_db._execute_sql(ref db_conn, ref sql);
                                if (ret != 0)
                                {
                                    break;
                                }
                            }
                        } while (false) ;
                    } while (false) ;

                    line_count++;
                }
            } while (false); 
            return ret; 
        }

        private const int data_html_field = 5;
        public int process_csv_file( string file_name )
        {
            int ret = 0;
            int _ret; 

            do
            {
                if (file_name.Length == 0)
                {
                    ret = -1;
                    break;
                }

                using (CsvReader csv = new CsvReader(new StreamReader(file_name), false))
                {
                    csv.SupportsMultiline = false;
                    string s;
                    if( false == csv.ReadNextRecord() )
                    {
                        break; 
                    }

                    while (csv.ReadNextRecord())
                    {
                        try
                        {
                            s = csv[data_html_field];
                        }
                        catch(Exception ex)
                        {
                            s = ""; 
                        }

                        if (s.Length == 0 )
                        {
                            _ret = -1; 
                        }
                        else
                        {
                            _ret = _construct_html(s);
                            if (0 != _ret)
                            {
                                System.Diagnostics.Debug.WriteLine("process html error");
                            }
                        }
                    }
                }
            } while (false);
            return ret; 
        }

        private int init_result_list()
        {
            int ret = 0;
            int data_info_index;

            //list_results.Items.Clear(); 
            //list_results.Columns.Clear();

            do
            {
                for (data_info_index = 0; 
                    data_info_index < list_data_info.Count;
                    data_info_index++)
                {
                    //list_results.Columns.Add(list_data_info[data_info_index].name.Text);
                }
            } while (false);

            return ret; 
        }

        private void btn_csv_file_Click(object sender, EventArgs e)
        {
            Int32 ret; 
            do
            {
            if( 0 == input_param.csv.file_name.Length)
            {
                break; 
            }

            //import_csv_file_to_db(text_input.Text); 

            ret = init_result_list(); 
            if( ret != 0 )
            {
                break; 
            }

            ret = init_csv_file(input_param.csv.file_name);
            if (ret != 0)
            {
                break;
            }

            ret = process_csv_file(input_param.csv.file_name); 
        
            }while( false ); 
        }

        public int get_next_sentence(string text, ref int start_index, ref string text_output)
        {
            int ret = 0; 
            //string output;
            int begin_index;
            int end_index;

            //注意最长的字符串不能超过最大长度
            do
            {
                text_output = ""; 

                if (start_index >= text.Length)
                {
                    ret = -1;
                    break;
                }

                do
                {
                    begin_index = start_index; 
                    end_index = text.IndexOf("\r\n", begin_index);
                    
                    if (begin_index <= end_index)
                    {
                        text_output = text.Substring(begin_index, end_index - begin_index);
                        break;
                    }

                    end_index = text.IndexOf("\n", begin_index);

                    if (begin_index <= end_index)
                    {
                        text_output = text.Substring(begin_index, end_index - begin_index);
                        break;
                    }

                    end_index = text.IndexOf("\r", begin_index);

                    if (begin_index <= end_index)
                    {
                        text_output = text.Substring(begin_index, end_index - begin_index);
                        break;
                    }

                    end_index = text.IndexOf(" ", begin_index);

                    if (begin_index <= end_index)
                    {
                        text_output = text.Substring(begin_index, end_index - begin_index);
                        break;
                    }

                    end_index = text.IndexOf(".", begin_index);
                    if (begin_index <= end_index)
                    {
                        text_output = text.Substring(begin_index, end_index - begin_index);
                        break;
                    }

                    end_index = text.IndexOf("。", begin_index);
                    if (begin_index <= end_index)
                    {
                        text_output = text.Substring(begin_index, end_index - begin_index);
                        break;
                    }

                    end_index = text.IndexOf("，", begin_index);
                    if (begin_index <= end_index)
                    {
                        text_output = text.Substring(begin_index, end_index - begin_index);
                        break;
                    }

                    text_output = text;
                    break;
                } while (false); 

                        
                if( text_output.Length > MAX_NLP_TEXT_LENGTH )
                {
                    //end_index = text.LastIndexOf("，", begin_index);
                    //if (begin_index <= end_index)
                    //{
                    //    text_output = text.Substring(begin_index, end_index - begin_index);
                    //    break;
                    //}

                    text_output = text_output.Substring(0, MAX_NLP_TEXT_LENGTH); 
                }

                start_index = begin_index + text_output.Length + 1; 

            } while (false); 
            return ret; 
        }

        private int prepare_data_process_work()
        {
            int ret = 0;

            do
            {
                //diag_process.StartInfo.FileName = "cmd.exe";
                //diag_process.StartInfo.RedirectStandardOutput = true;
                //diag_process.StartInfo.RedirectStandardInput = true;
                //diag_process.StartInfo.UseShellExecute = false;
                //diag_process.StartInfo.CreateNoWindow = true;
                //diag_process.Start();

                //log_form.Show(this);
                ret = init_construct_table();
                if( ret != 0 )
                {
                    break; 
                }

            } while (false);
            return ret; 
        }

        private int release_data_process_work()
        {
            int ret = 0;

            do
            {
                //log_form.Visible = false;

            } while (false);
            return ret;
        }

        private int output_data_process_log(string text, int in_out, bool append)
        {
            int ret = 0; 
            //if (this.InvokeRequired == false)   //如果调用该函数的线程和控件位于同一个线程  
            if (in_out == 0)
            {
                if (append == true)
                {
                    this.text_input_param.Text += text;
                    ///this.log_form.text_input.Text += text;
                }
                else
                {
                    this.text_input_param.Text = text; 
                    //this.log_form.text_input.Text = text;
                }

                this.text_input_param.SelectionStart = this.text_input_param.Text.Length;
                this.text_input_param.ScrollToCaret();
                //this.log_form.text_input.SelectionStart = this.log_form.text_input.Text.Length;
                //this.log_form.text_input.ScrollToCaret();
            }
            else
            {
                if (append == true)
                {
                    this.text_process_mode.Text += text;

                    //this.log_form.text_output.Text += text;
                }
                else
                {
                    this.text_process_mode.Text = text; 
                    //this.log_form.text_output.Text = text;
                }

                this.text_process_mode.SelectionStart = this.text_process_mode.Text.Length;
                this.text_process_mode.ScrollToCaret(); 
                //this.log_form.text_output.SelectionStart = this.log_form.text_output.Text.Length;
                //this.log_form.text_output.ScrollToCaret();
            }
            return ret;
        }

        int call_output_data_process_log(string text, int in_out, bool append)
        {
            int ret = 0;
            do
            {
                data_process_log_callback callback = new data_process_log_callback(output_data_process_log);
                ret = ( int )Invoke(callback, text, in_out, append);
            } while (false);
            return ret; 
        }

        int call_prepare_data_process()
        {
            int ret = 0;
            do
            {
                prepare_work_callback callback = new prepare_work_callback(prepare_data_process_work);
                ret = (int)Invoke(callback );
            } while (false);
            return ret;
        }

        int call_release_data_process()
        {
            int ret = 0;
            do
            {
                release_work_callback callback = new release_work_callback(release_data_process_work);
                ret = (int)Invoke(callback );
            } while (false);
            return ret;
        }

        public static int process_data_from_database(string id, string text, data_process context)
        {
            int ret = 0;
            int _ret; 
            string text_output = "";
            string _text_output = ""; 
            string sentence = "";
            int start_index = 0;
            //StreamWriter stream_writer = context.diag_process.StandardInput;

            context.call_output_data_process_log(text, 0, false);
            context.call_output_data_process_log("", 1, false); 

            for (; ; )
            {
                if(context.stop_work == true )
                {
                    ret = DBConnect.STOP_DATA_BASE_PROCESS;
                    break; 
                }

                _ret = context.get_next_sentence(text, ref start_index, ref sentence);
                if(_ret != 0)
                {
                    //stream_writer.WriteLine(string.Format("parse error:{0}", text)); 
                    break; 
                }

                do
                {
                    if (context.stop_work == true)
                    {
                        ret = DBConnect.STOP_DATA_BASE_PROCESS;
                        break;
                    }

                    if (sentence.Length == 0)
                    {
                        break;
                    }

                    _ret = text_process.filter_senseless_text(ref sentence);

                    if (sentence.Length == 0)
                    {
                        break;
                    }

                    _ret = context.nlp_text(context.nlp_server_socket, sentence, ref text_output);
                    if (_ret != 0)
                    {
                        break;
                    }

                    context.call_output_data_process_log(text_output, 1, true);
                    _text_output += text_output; 
                    //stream_writer.WriteLine(text_output); 
                } while (false);
                //System.Console.WriteLine(text_output);
            }

            do
            {
                string sql;

                if (context.stop_work == true)
                {
                    ret = DBConnect.STOP_DATA_BASE_PROCESS;
                    break;
                }

                if (_text_output.Length == 0)
                {
                    break;
                }

                sql = "UPDATE ";
                sql += context.input_param.mysql.table_name;
                sql += " SET ";
                sql += context.input_param.mysql.fields[0];
                sql += "_nlp";
                sql += "=";
                sql += "'";
                sql += DBConnect.mysql_escape( _text_output );
                sql += "'";
                sql += " WHERE";
                sql += " id=";
                sql += id;
                sql += ";";

                ret = context._mysql_db.execute_sql(ref sql);
                if (ret != 0)
                {
                    break;
                }

            } while (false); 
            //context.construct_html(html, DATA_SQL_IO); 
            return ret;
        }

        private int init_construct_table()
        {   
            int ret = 0;
            string table_name;
            string column_name; 
            string sql; 
       
            do
            {
                if (input_param.mysql.table_name.Length == 0)
                {
                    break;
                }

                column_name = input_param.mysql.fields[0];
                column_name += "_nlp"; 

                table_name = input_param.mysql.table_name;

                sql = "ALTER TABLE ";
                sql += table_name;
                sql += " ADD COLUMN ";
                sql += column_name;
                sql += " LONGTEXT";
                sql += ";";

                ret = init_mysql_param();
                if (ret != 0)
                {
                    break;
                }

                ret = mysql_db.execute_sql(ref sql);
                if (ret != 0)
                {
                    break;
                }

    //            if( text_table_name.Text.Length == 0 )
    //            {
    //                break; 
    //            }

    //            table_name = text_table_name.Text;
    //            table_name += "_data";

    //            sql = "CREATE TABLE IF NOT EXISTS ";
    //            sql += table_name; 
    //            sql += "("; 
    //            sql += "id INT(4) NOT NULL PRIMARY KEY AUTO_INCREMENT"; 

    ////	create table MyClass(
    ////> id int(4) not null primary key auto_increment,
    ////> name char(20) not null,
    ////> sex int(4) not null default '0',
    ////> degree double(16,2)); 

    //            for (int data_info_index = 0;
    //                data_info_index < list_data_info.Count;
    //                data_info_index++)
    //            {
    //                sql += ","; 
    //                sql += list_data_info[ data_info_index ].SubItems[ 0 ].Text; 
    //                sql += " LONGTEXT";
    //            }

    //            sql += ") DEFAULT CHARSET=utf8;";  
    //            ret = mysql_db.execute_sql( ref sql ); 
    //            if( ret != 0 )
    //            {
    //                break; 
    //            }

    //            output_table_name = table_name; 
            } while (false); 
            
            return ret;
        }

        private const ushort NLP_SERVICE_PORT = 8001;
        private const string NLP_SERVICE_IP_ADDRESS = "127.0.0.1";

        private void thread_work()
        {
            int ret = 0; 

            for(;; )
            {
                do
                {
                    work_status = 0;
                    work_status_event.Set(); 
                    if ( false == work_event.WaitOne() )
                    {
                        ret = -1; 
                        break; 
                    }

                    if (stop_work == true)
                    {
                        break; 
                    }

                    if (exit_work == true)
                    {
                        break;
                    }

                    work_status = 1;
                    work_status_event.Set(); 
                    do_work(); 
                } while (false);

                work_status = 0;
                work_status_event.Set(); 

                if(ret != 0)
                {
                    break; 
                }

                if ( exit_work == true )
                {
                    break; 
                }
            }
        }

        private int do_work()
        {
            int ret = 0; 
            int port = NLP_SERVICE_PORT;
            string host = NLP_SERVICE_IP_ADDRESS;

            IPAddress ip = IPAddress.Parse(host);
            IPEndPoint ipe = new IPEndPoint(ip, port);

            do
            {
                this.nlp_server_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    this.nlp_server_socket.Connect(ipe);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("construct data detail error{0}", ex.Message);
                    MessageBox.Show("nlp服务没有启动完毕，等待一下再试");
                    break; 
                }

                if (this.nlp_server_socket.Connected == false)
                {
                    break;
                }

                call_prepare_data_process();

                if (this.source == DATA_PROCESS_SOURCE.DATA_PROCESS_SOURCE_TEXT)
                {
                    do
                    {
                        ret = process_data_from_text(text_input_param.Text, this);
                        if (ret != 0)
                        {
                            break;
                        }
                    } while (false); 
                }
                else if (this.source == DATA_PROCESS_SOURCE.DATA_PROCESS_SOURCE_MYSQL)
                {
                    do
                    {
                        ret = init_mysql_param();
                        if (ret != 0)
                        {
                            break;
                        }

                        mysql_db.Select(input_param.mysql.table_name, input_param.mysql.fields[0], process_data_from_database, this);
                    } while (false);
                }
                
                call_release_data_process(); 

                byte[] data = new byte[0];
                uint data_size = 0;

                ret = data_io.send_data(ref this.nlp_server_socket, ref data, ref data_size);
                if( ret != 0 )
                {

                }

                nlp_server_socket.Close(); 
            } while (false);
            return ret; 
        }

        private int stop_process_data()
        {
            int ret = 0;

            do
            {
                //if (work_thread.IsAlive == false)
                //{
                //    System.Diagnostics.Debug.Assert(work_thread.ThreadState == System.Threading.ThreadState.Unstarted
                //        || work_thread.ThreadState == System.Threading.ThreadState.Stopped
                //        || work_thread.ThreadState == System.Threading.ThreadState.Aborted); 

                //    ret = -1; 
                //    break;
                //}

                //Console.WriteLine("main thread: Stopping worker thread...");
                stop_work = true;
                work_event.Set(); 
            } while (false);

            return ret; 
        }

        private int init_mysql_param()
        {
            int ret = 0; 

            do
            {
                mysql_db = new DBConnect();
                _mysql_db = new DBConnect(); 

                if (mysql_db.initialized() == false)
                {
                    do
                    {
                        if (0 == input_param.mysql.server_name.Length)
                        {
                            ret = -1; 
                            break;
                        }

                        if (0 == input_param.mysql.db_name.Length)
                        {
                            ret = -1; 
                            break;
                        }

                        if (0 == input_param.mysql.user_name.Length)
                        {
                            ret = -1; 
                            break;
                        }

                        mysql_db.Initialize(input_param.mysql.server_name, input_param.mysql.port, input_param.mysql.db_name, input_param.mysql.user_name, input_param.mysql.password);
                    } while (false);
                }

                ret = mysql_db._Initialize(ref db_conn,
                    input_param.mysql.server_name,
                    input_param.mysql.port,
                    input_param.mysql.db_name,
                    input_param.mysql.user_name,
                    input_param.mysql.password);
                if (0 != ret)
                {
                    break;
                }

                if (false == mysql_db._OpenConnection(ref db_conn))
                {
                    ret = -1; 
                    break;
                }

                if (_mysql_db.initialized() == false)
                {
                    do
                    {
                        if (0 == input_param.mysql.server_name.Length)
                        {
                            ret = -1; 
                            break;
                        }

                        if (0 == input_param.mysql.db_name.Length)
                        {
                            ret = -1; 
                            break;
                        }

                        if (0 == input_param.mysql.user_name.Length)
                        {
                            ret = -1; 
                            break;
                        }

                        _mysql_db.Initialize(input_param.mysql.server_name, input_param.mysql.port, input_param.mysql.db_name, input_param.mysql.user_name, input_param.mysql.password);
                    } while (false);
                }

                ret = _mysql_db._Initialize(ref db_conn,
                    input_param.mysql.server_name,
                    input_param.mysql.port,
                    input_param.mysql.db_name,
                    input_param.mysql.user_name,
                    input_param.mysql.password);
                if (0 != ret)
                {
                    break;
                }

                if (false == _mysql_db._OpenConnection(ref db_conn))
                {
                    ret = -1; 
                    break;
                }

            } while (false); 

            return ret; 
        }

        private int start_process_data()
        {
            int ret = 0; 

            do
            {
                //if(stop_work == true)
                //{
                //    MessageBox.Show("先取消正在运行的数据处理工作"); 
                //    break; 
                //}

                if (source == DATA_PROCESS_SOURCE.DATA_PROCESS_SOURCE_MYSQL)
                {
                    if (input_param.mysql.fields.Count == 0)
                    {
                        ret = -1;
                        break;
                    }
                }

                //ret = init_mysql_param();
                //if (ret != 0)
                //{
                //    break;
                //}

                // Start the worker thread.
                //Console.WriteLine("main thread: Starting worker thread...");

                //if (false == mysql_db._CloseConnection(ref db_conn))
                //{
                //    break;
                //}

                stop_work = false;
                work_event.Set(); 

            } while (false);
            return ret; 
        }

        private void btn_disconnect_Click(object sender, EventArgs e)
        {
            mysql_db.Disconnect(); 
        }

        private void btn_learn_Click(object sender, EventArgs e)
        {

        }

        private void btn_format_text_Click(object sender, EventArgs e)
        {
            text_process_form form = new text_process_form();
            form.ShowDialog(); 
        }

        private void btn_input_db_Click(object sender, EventArgs e)
        {
            int ret; 
            db_config_form db_config;
            db_config = new db_config_form();
            db_config.set_config(input_param.mysql); 
            db_config.ShowDialog();

            do
            {
                if (db_config.param_is_valid() == false)
                {
                    break;
                }
                ret = db_config.get_input_paramters(ref input_param.mysql);
                if (ret != 0)
                {
                    break;
                }

                text_input_param.Text = string.Format("MYSQL服务器:{0}\n数据库:{1}\n表{2}",
                    input_param.mysql.server_name,
                    input_param.mysql.db_name,
                    input_param.mysql.table_name);

                source = DATA_PROCESS_SOURCE.DATA_PROCESS_SOURCE_MYSQL; 

            } while (false);
            return; 
        }

        private void btn_input_csv_Click(object sender, EventArgs e)
        {
            csv_file_config_dlg csv_config;
            csv_config = new csv_file_config_dlg();
            csv_config.ShowDialog(); 
        }

        private void btn_process_mysql_table_Click(object sender, EventArgs e)
        {
            html_process_form html_process;
            html_process = new html_process_form();
            html_process.ShowDialog(); 
        }

        private int check_nl_service_status()
        {
            int ret = 0; 
            bool nlp_service_ready = false;
            string nlp_service_path;

            do
            {
                System.Diagnostics.Process[] processList = System.Diagnostics.Process.GetProcesses();
                foreach (System.Diagnostics.Process process in processList)
                {
                    System.Diagnostics.Trace.TraceInformation(process.ProcessName.ToUpper()); 
 
                    if (process.ProcessName.ToUpper().IndexOf("NLP_SERVICE.EXE") >= 0
                        || ( process.ProcessName.ToUpper().IndexOf("NLP_SERVICE") >= 0 )
                        && (process.ProcessName.ToUpper().IndexOf("NLP_SERVICE.VSHOST") < 0))
                    {
                        nlp_service_ready = true;
                        break;
                    }
                }

                if (nlp_service_ready == false)
                {
                    nlp_service_path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                    nlp_service_path += "\\nlp_service\\nlp_service.exe";

                    //System.Windows.Forms.Application.ExecutablePath
                    if (File.Exists(nlp_service_path) == false)
                    {
                        nlp_process_dlg nlp_process;
                        nlp_process = new nlp_process_dlg();
                        nlp_process.ShowDialog();
                        ret = -1; 
                        break;
                    }

                    //链接: https://pan.baidu.com/s/1dEC1yLz 密码: hx19
                    //http://www.simplestai.com/webkits/nlp_service.zip


                    System.Diagnostics.Process nlp_service_process = System.Diagnostics.Process.Start(nlp_service_path); // 启动外部进程

                    if (nlp_service_process == null)
                    {
                        nlp_process_dlg nlp_process;
                        nlp_process = new nlp_process_dlg();
                        nlp_process.ShowDialog();
                        ret = -1; 
                        break;
                    }

                    nlp_service_ready = true;
                    break;
                }
            } while (false);

            if (true == nlp_service_ready)
            {
                text_process_mode.Text = "数据处理方式:\n文本语义解析";
            }

            return ret; 
        }

        private void btn_process_nlp_Click(object sender, EventArgs e)
        {
            check_nl_service_status(); 
        }

        private void btn_output_db_Click(object sender, EventArgs e)
        {
            db_config_form db_config;
            db_config = new db_config_form();
            db_config.ShowDialog(); 
        }

        private void btn_output_csv_Click(object sender, EventArgs e)
        {
            csv_file_config_dlg csv_config;
            csv_config = new csv_file_config_dlg();
            csv_config.ShowDialog(); 
        }

        public static int process_data_from_text(string text, data_process context)
        {
            int ret = 0;
            int _ret;
            string text_output = "";
            string _text_output = "";
            string sentence = "";
            int start_index = 0;
            //StreamWriter stream_writer = context.diag_process.StandardInput;

            context.call_output_data_process_log(text, 0, false);
            context.call_output_data_process_log("", 1, false);

            for (; ; )
            {
                if (context.stop_work == true)
                {
                    ret = DBConnect.STOP_DATA_BASE_PROCESS;
                    break;
                }

                _ret = context.get_next_sentence(text, ref start_index, ref sentence);
                if (_ret != 0)
                {

                    //stream_writer.WriteLine(string.Format("parse error:{0}", text)); 
                    break;
                }

                do
                {
                    if (context.stop_work == true)
                    {
                        ret = DBConnect.STOP_DATA_BASE_PROCESS;
                        break;
                    }

                    if (sentence.Length == 0)
                    {
                        break;
                    }

                    _ret = text_process.filter_senseless_text(ref sentence);

                    _ret = context.nlp_text(context.nlp_server_socket, sentence, ref text_output);
                    if (_ret != 0)
                    {
                        break;
                    }

                    context.call_output_data_process_log(text_output, 1, true);
                    _text_output += text_output;
                    //stream_writer.WriteLine(text_output); 
                } while (false);
                //System.Console.WriteLine(text_output);
            }
            return ret;
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            int ret;

            do
            {
                ret = check_nl_service_status();
                if (ret != 0)
                {
                    break; 
                }

                {
                    //if (false == work_status_event.WaitOne(2000))
                    //{
                    //    ret = -1; 
                    //    break; 
                    //}

                    if (work_status != 0)
                    {
                        ret = -1; 
                        break; 
                    }

                    ret = start_process_data();
                    if (ret != 0)
                    {
                        break;
                    }

                    if (true == work_status_event.WaitOne(2000))
                    {
                        //System.Diagnostics.Debug.Assert(work_status == 1); 
                        //btn_start.Enabled = false;
                        //btn_stop.Enabled = true;
                    }

                    break; 
                }
                ret = -1; 
            } while (false);
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            int ret;

            do
            {
                //if (false == work_status_event.WaitOne(2000))
                //{
                //    ret = -1;
                //    break;
                //}

                if (work_status == 0)
                {
                    ret = -1; 
                    break; 
                }

                ret = stop_process_data();
                if (ret != 0)
                {
                    break;
                }

                //btn_start.Enabled = true;
                //btn_stop.Enabled = false;
                if (true == work_status_event.WaitOne(2000))
                {
                    //System.Diagnostics.Debug.Assert(work_status == 0);
                }
            } while (false);
        }

        int _load_main_config(string name, ref string value )
        {
            int ret = 0; 
            string file_path;
            
            do
            {
                file_path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                file_path += "main.xml";
                
                XmlDocument xmlDoc = new XmlDocument();
                //XmlAttribute node_attr;
                XmlNode info_node;

                if (File.Exists(file_path) == false)
                {
                    ret = -1;
                    break;
                }

                xmlDoc.Load(file_path);

                //使用xpath表达式选择文档中所有的student子节点
                info_node = xmlDoc.SelectSingleNode("/main");

                if (null == info_node)
                {
                    ret = -1; 
                    break;
                }

                if( null == info_node.Attributes[name] )
                {
                    ret = -1; 
                    break; 
                }

                value = info_node.Attributes[name].InnerText;
                if (value.Length == 0)
                {
                    break;
                }
            } while (false); 

            return ret;
        }


        int load_main_config(ref string last_config )
        {
            int ret = 0; 
            ret = _load_main_config( "last_config", ref last_config ); 
            return ret; 
        }

        int load_max_text_length(ref Int32 max_text_len )
        {
            string _max_text_len = ""; 
            int ret = 0;
            do
            {
                ret = _load_main_config("max_text_len", ref _max_text_len);
                if (ret != 0)
                {
                    break;
                }

                max_text_len = System.Convert.ToInt32(_max_text_len); 
            } while (false); 
            return ret;
        }

        int _save_main_config( string name, string value )
        {
            int ret = 0;
            string file_path;

            do
            {
                if (value.Length == 0)
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

                node_attr = xmlDoc.CreateAttribute(name);
                node_attr.Value = value;
                root_node.Attributes.Append(node_attr);

                xmlDoc.AppendChild(root_node);
                xmlDoc.Save(file_path);
            } while (false);
            return ret;
        }

        int save_main_config( string last_config )
        {
            int ret = 0; 
            ret = _save_main_config( "last_config", last_config ); 
        
            return ret; 
        }

        int save_max_text_len( string max_text_len )
        {
            int ret = 0; 
            ret = _save_main_config( "max_text", max_text_len ); 
        
            return ret; 
        }

        private void btn_process_html_Click(object sender, EventArgs e)
        {
        }
        
        private void btn_save_Click(object sender, EventArgs e)
        {
            Int32 data_info_index;
            string file_path;

            do
            {
                if (input_param.mysql.fields.Count == 0)
                {
                    break;
                }

                SaveFileDialog dlg = new SaveFileDialog(); 
                if (dlg.ShowDialog() == DialogResult.OK )
                {
                    file_path = dlg.FileName; 
                }
                else
                {
                    break; 
                }

                if( file_path.Length == 0 )
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
                node_attr.Value = input_param.mysql.db_name; 
                info_node.Attributes.Append(node_attr);

                node_attr = xmlDoc.CreateAttribute("table_name");
                node_attr.Value = input_param.mysql.table_name; 
                info_node.Attributes.Append(node_attr);

                node_attr = xmlDoc.CreateAttribute("user_name");
                node_attr.Value = input_param.mysql.user_name; 
                info_node.Attributes.Append(node_attr);

                node_attr = xmlDoc.CreateAttribute("server_name");
                node_attr.Value = input_param.mysql.server_name; 
                info_node.Attributes.Append(node_attr);

                node_attr = xmlDoc.CreateAttribute("port");
                node_attr.Value = input_param.mysql.port; 
                info_node.Attributes.Append(node_attr);

                node_attr = xmlDoc.CreateAttribute("password");
                node_attr.Value = input_param.mysql.password; 
                info_node.Attributes.Append(node_attr);

                node_attr = xmlDoc.CreateAttribute("start_row");
                node_attr.Value = input_param.mysql.start_row; 
                info_node.Attributes.Append(node_attr);

                root_node.AppendChild(info_node); 

                for (data_info_index = 0; data_info_index < input_param.mysql.fields.Count; data_info_index++)
                {
                    do
                    {

                        info_node = xmlDoc.CreateElement("field");
                        if (null == info_node)
                        {
                            break;
                        }

                        node_attr = xmlDoc.CreateAttribute("name");
                        node_attr.Value = input_param.mysql.fields[data_info_index];
                        info_node.Attributes.Append(node_attr);

                        root_node.AppendChild(info_node);
                    } while (false);
                }

                //附加根节点
                xmlDoc.AppendChild(root_node);
                //保存Xml文档
                xmlDoc.Save(file_path);
                //Console.WriteLine("data information is saved");
            } while (false);
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

                input_param.mysql.db_name = info_node.Attributes["db_name"].InnerText;
                input_param.mysql.server_name = info_node.Attributes["server_name"].InnerText;
                input_param.mysql.port = info_node.Attributes["port"].InnerText;
                input_param.mysql.user_name = info_node.Attributes["user_name"].InnerText;
                input_param.mysql.password = info_node.Attributes["password"].InnerText;
                input_param.mysql.table_name = info_node.Attributes["table_name"].InnerText;
                input_param.mysql.start_row = info_node.Attributes["start_row"].InnerText;

                info_nodes = xmlDoc.SelectNodes("/data_params/field");

                if (info_nodes.Count == 0)
                {
                    ret = -1; 
                    break;
                }

                input_param.mysql.fields.Clear();

                for (data_info_index = 0; data_info_index < info_nodes.Count; data_info_index++)
                {
                    do
                    {
                        string field_name;
                        //创建student子节点
                        info_node = info_nodes.Item(data_info_index);
                        if (null == info_node)
                        {
                            ret = -1; 
                            break;
                        }


                        field_name = info_node.Attributes["name"].InnerText;
                        input_param.mysql.fields.Add(field_name);
                    } while (false);
                }


                save_main_config(file_path);
                //Console.WriteLine("data information is loaded");
            } while (false);
            return ret; 
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            string file_path; 
            OpenFileDialog dlg = new OpenFileDialog();

            do
            {
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

        private void btn_text_Click(object sender, EventArgs e)
        {
            text_input_param.Text = "直接解析文本";
            source = DATA_PROCESS_SOURCE.DATA_PROCESS_SOURCE_TEXT; 
        }
    }
}
