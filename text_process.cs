using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace data_process
{
    public class text_process
    {
        public static int is_digit(char ch)
        {
            int ret = 0;
            do
            {
                if (ch >= '0' &&
                    ch <= '9')
                {
                    break;
                }
                ret = -1; 
            } while (false);
            return ret; 
        }

        static public int filter_senseless_text(ref string text)
        {
            int ret = 0;
            int index;

            //去掉HTML中无意义的文本(HTML语法有关) 将过长的兔子裁短
            do
            {
                text = text.Replace("\n", " ");
                text = text.Replace("\r", " ");
                text = text.Replace("=", " ");
                //text = text.Replace(" ", "");
                text = text.Replace("%", "");
                text = text.Replace(">", " ");
                text = text.Replace("<", " ");
                text = text.Replace("'", " ");
                text = text.Replace("\"", " ");
                text = text.Replace("/", " ");
                text = text.Replace("{", " ");
                text = text.Replace("}", " ");
                //text = text.Replace("\"", "");
                //text = text.Replace("\"", "");
                //text = text.Replace("\"", "");
            } while (false);
            return ret; 
        }

        static public int format_csv_field_text(ref string text)
        {
            int ret = 0;
            int index;
            string _text;
            _text = text;
            do
            {
                _text = _text.Replace(',', ' ');

                _text = _text.Replace("\"", "\"\"");

                index = _text.IndexOf('\n', 0);
                if (index >= 0)
                {
                    _text.Insert(0, "\"");
                    text += '\"';
                    break;
                }

                index = _text.IndexOf('\r', 0);
                if (index >= 0)
                {
                    _text.Insert(0, "\"");
                    text += '\"';
                    break;
                }
            } while (false);
            text = _text;
            return ret;
        }

        public static int is_number(string text)
        {
            int ret = 0; 
            int i;
            int dot_count = 0; 

            for (i = 0; i < text.Length; i++)
            {
                if( text[ i ] < '0'
                    && text[i] > '9')
                {
                    ret = -1; 
                    break; 
                }

                if (text[i] == '.')
                {
                    if (dot_count > 0)
                    {
                        ret = -1; 
                        break; 
                    }

                    dot_count++; 
                }
            }

            return ret; 
        }

        public static int extract_decimal( ref string text)
        {
            int ret = 0;
            int ch_index; 
            int decimal_begin;
            int decimal_end;
            int dot_index;
            int minus_index; 
            string decimal_text; 

            do
            {
                if (text.Length == 0)
                {
                    ret = -1; 
                    break; 
                }

                decimal_begin = -1;
                decimal_end = -1;
                dot_index = -1;
                minus_index = -1; 

                for( ch_index = 0; ch_index < text.Length; ch_index ++ )
                {
                    if ( 0 == is_digit( text.ElementAt( ch_index ) ) )
                    {
                        if (decimal_begin == -1)
                        {
                            decimal_begin = ch_index; 
                        }
                    }
                    else if (text.ElementAt(ch_index) == '-')
                    {
                        if (decimal_begin != -1)
                        {
                            if (minus_index == -1)
                            {
                                minus_index = ch_index;
                            }
                            else if (decimal_begin == ch_index - 1 )
                            {
                                minus_index = ch_index;
                                decimal_begin = ch_index; 
                            }
                            else
                            {
                                decimal_end = ch_index;
                                break;
                            }
                        }
                        else
                        {
                            minus_index = ch_index;
                            decimal_begin = ch_index; 
                        }
                    }
                    else if (text.ElementAt(ch_index) == '.')
                    {
                        if (decimal_begin != -1)
                        {
                            if (dot_index == -1)
                            {
                                dot_index = ch_index;
                            }
                            else
                            {
                                decimal_end = ch_index;
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (decimal_begin != -1)
                        {
                            decimal_end = ch_index;
                            break; 
                        }
                    }
                }

                if( decimal_end == -1 )
                {
                    if( decimal_begin != -1 )
                    {
                        decimal_end = text.Length; 
                    }
                }
                if (decimal_end <= decimal_begin)
                {
                    ret = -1; 
                    break; 
                }

                decimal_text = text.Substring(decimal_begin, decimal_end - decimal_begin);
                text = decimal_text; 
            } while (false);

            return ret; 
        }
    }
}
