using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace data_process
{
    public partial class html_process_form : Form
    {
        public html_process_form()
        {
            InitializeComponent();
        }

        string process_xpath(string xpath)
        {
            return xpath.Replace('\"', '\'');
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            ListViewItem item = new ListViewItem();
            //ListViewItem.ListViewSubItem sub_item; 

            do
            {
                if (text_name.Text.Length == 0)
                {
                    break;
                }

                if (text_xpath.Text.Length == 0)
                {
                    break;
                }

                if (combo_type.Text.Length == 0)
                {
                    break;
                }

                item.ImageIndex = list_data_info.Items.Count;

                item.Text = text_name.Text;
                item.ToolTipText = text_name.Text;

                item.SubItems.Add(process_xpath(text_xpath.Text));

                item.SubItems.Add(combo_type.Text);

                item.ToolTipText = "name:" + text_name.Text + "\r\nxpath:" + text_xpath.Text;

                list_data_info.Items.Add(item);
            } while (false); 
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {

        }

        private void list_data_info_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            do
            {
                if (list_data_info.SelectedItems.Count == 0)
                {
                    break;
                }

                text_name.Text = list_data_info.SelectedItems[0].SubItems[0].Text;
                text_xpath.Text = list_data_info.SelectedItems[0].SubItems[1].Text;
                combo_type.Text = list_data_info.SelectedItems[0].SubItems[2].Text;
            } while (false);
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            Int32 data_info_index;
            string file_path;

            do
            {

                if (text_file_path.Text.Length == 0)
                {
                    break;
                }

                file_path = text_file_path.Text;

                XmlDocument xmlDoc = new XmlDocument();
                //XmlAttribute node_attr;
                XmlNode info_node;
                XmlNodeList info_nodes;
                ListViewItem item;

                xmlDoc.Load(file_path);

                //使用xpath表达式选择文档中所有的student子节点
                info_nodes = xmlDoc.SelectNodes("/data_infos/data_info");

                if (info_nodes.Count == 0)
                {
                    break;
                }

                list_data_info.Clear();

                for (data_info_index = 0; data_info_index < info_nodes.Count; data_info_index++)
                {
                    do
                    {

                        //创建student子节点
                        info_node = info_nodes.Item(data_info_index);
                        if (null == info_node)
                        {
                            break;
                        }

                        item = new ListViewItem();
                        if (null == item)
                        {
                            break;
                        }

                        item.ImageIndex = list_data_info.Items.Count;
                        item.Text = info_node.Attributes["name"].InnerText;
                        item.SubItems.Add(process_xpath(info_node.Attributes["xpath"].InnerText));
                        item.SubItems.Add(info_node.Attributes["type"].InnerText);
                        item.ToolTipText = "name:" + info_node.Attributes["name"].InnerText + "\r\nxpath:" + info_node.Attributes["xpath"].InnerText;
                        list_data_info.Items.Add(item);
                    } while (false);
                }

                Console.WriteLine("data information is loaded");
            } while (false);
        }

        private void btn_remove_all_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < list_data_info.SelectedItems.Count; i++)
            {
                list_data_info.Items.Remove(list_data_info.SelectedItems[i]);
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            Int32 data_info_index;
            string file_path;

            do
            {
                if (list_data_info.Items.Count == 0)
                {
                    break;
                }

                if (text_file_path.Text.Length == 0)
                {
                    break;
                }

                file_path = text_file_path.Text;

                XmlDocument xmlDoc = new XmlDocument();
                XmlNode root_node;
                XmlAttribute node_attr;
                XmlNode info_node;

                //创建Xml声明部分，即<?xml version="1.0" encoding="utf-8" ?>
                xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes");

                //创建根节点
                root_node = xmlDoc.CreateElement("data_infos");
                if (null == root_node)
                {
                    break;
                }

                for (data_info_index = 0; data_info_index < list_data_info.Items.Count; data_info_index++)
                {
                    do
                    {

                        info_node = xmlDoc.CreateElement("data_info");
                        if (null == info_node)
                        {
                            break;
                        }

                        node_attr = xmlDoc.CreateAttribute("name");
                        node_attr.Value = list_data_info.Items[data_info_index].SubItems[0].Text;
                        info_node.Attributes.Append(node_attr);

                        node_attr = xmlDoc.CreateAttribute("xpath");
                        node_attr.Value = list_data_info.Items[data_info_index].SubItems[1].Text;
                        info_node.Attributes.Append(node_attr);

                        node_attr = xmlDoc.CreateAttribute("type");
                        node_attr.Value = list_data_info.Items[data_info_index].SubItems[2].Text;
                        info_node.Attributes.Append(node_attr);

                        root_node.AppendChild(info_node);
                    } while (false);
                }

                //附加根节点
                xmlDoc.AppendChild(root_node);
                //保存Xml文档
                xmlDoc.Save(file_path);
                Console.WriteLine("data information is saved");
            } while (false);
        }
    }
}
