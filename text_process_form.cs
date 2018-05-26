using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using HtmlAgilityPack; 
using System.Xml;
using LumenWorks.Framework.IO.Csv;
//using LINQtoCSV;

namespace data_process
{
    public partial class text_process_form : Form
    {
        public text_process_form()
        {
            InitializeComponent();
        }

        private void btn_convert_Click(object sender, EventArgs e)
        {
            int ret = 0;
            int _ret;
            int i;
            string file_name;

            string _file_name;
            string field;
            string record;

            StreamWriter fs;

            do
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.InitialDirectory = System.Windows.Forms.Application.StartupPath;

                openFileDialog.Filter = "csv文件|*.csv|所有文件|*.*";
                openFileDialog.RestoreDirectory = true;
                openFileDialog.FilterIndex = 1;

                if (openFileDialog.ShowDialog() != DialogResult.OK)
                {
                    break;
                }

                file_name = openFileDialog.FileName;

                if (file_name.Length == 0)
                {
                    ret = -1;
                    break;
                }

                _file_name = file_name;
                _file_name += ".data.csv";

                fs = new StreamWriter(_file_name, true, Encoding.Unicode);

                record = "";

                using (CsvReader csv = new CsvReader(new StreamReader(file_name), false))
                {
                    csv.SupportsMultiline = false;

                    if (false == csv.ReadNextRecord())
                    {
                        break;
                    }

                    for (i = 0; i < csv.FieldCount; i++)
                    {
                        try
                        {
                            field = csv[i];
                        }
                        catch (Exception ex)
                        {
                            field = "";
                        }

                        if (field.Length == 0)
                        {
                            _ret = -1;
                        }

                        if (i != 0)
                        {
                            record += ',';
                        }

                        text_process.format_csv_field_text(ref field);
                        record += field;
                    }

                    fs.WriteLine(record);
                    if (ret != 0)
                    {
                        break;
                    }

                    while (csv.ReadNextRecord())
                    {
                        record = ""; 
                        for (i = 0; i < csv.FieldCount; i++)
                        {
                            try
                            {
                                field = csv[i];
                            }
                            catch (Exception ex)
                            {
                                field = "";
                            }

                            if (field.Length == 0)
                            {
                                _ret = -1;
                            }

                            if (i != 0)
                            {
                                record += ',';
                            }

                            text_process.format_csv_field_text(ref field);
                            _ret = text_process.extract_decimal(ref field);
                            if (_ret != 0)
                            {
                                field = "0"; 
                            }
                            record += field;
                        }
                        fs.WriteLine(record);
                        if (ret != 0)
                        {
                            break;
                        }
                    }
                }
            } while (false);
            return;
        }
    }
}
