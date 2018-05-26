using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions; 
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Data; 
//Add MySql Library
using MySql.Data.MySqlClient;

namespace data_process
{
    public delegate int process_html_text_callback(string id, string text, data_process context);

    public class DBConnect
    {
        public const int STOP_DATA_BASE_PROCESS = 10081;
        private MySqlConnection connection;
        private string server;
        private string port; 
        private string database;
        private string uid;
        private string password;
        private bool inited; 

        //Constructor
        public DBConnect()
        {
            inited = false; 
        }

        //Initialize values
        public int Initialize( 
            string _server, 
            string _port, 
            string _database, 
            string _uid, 
            string _password )
        {
            int ret = 0; 

            do
            {
                ret = _Initialize(ref connection, _server, _port, _database, _uid, _password); 
                if( ret == 0 )
                {
                    inited = true; 
                }
            } while (false); 
            return ret; 
        }

        public int _Initialize( ref MySqlConnection conn, 
            string _server, 
            string _port, 
            string _database, 
            string _uid, 
            string _password )
        {
            int ret = 0; 
            string connectionString;

            do
            {
                this.server = _server;
                this.database = _database;
                this.uid = _uid;
                this.port = _port; 
                this.password = _password;

                connectionString = "SERVER=" 
                    + server 
                    + ";" 
                    + "PORT="
                    + port 
                    + ";" 
                    + "DATABASE=" 
                    + database + 
                    ";" 
                    + "UID=" 
                    + uid 
                    + ";" 
                    + "PASSWORD=" 
                    + password 
                    + ";"
                    + "CHARSET=utf8" 
                    + ";";

                conn = new MySqlConnection(connectionString);
                if (false == _OpenConnection( ref conn ) )
                {
                    ret = -1;
                    break; 
                }

                if (conn.State != System.Data.ConnectionState.Open)
                {
                    ret = -1;
                    _CloseConnection( ref conn ); 
                    break;
                }

                _CloseConnection( ref conn ); 
            } while (false); 

            return ret; 
        }

        public bool initialized()
        {
            return inited; 
        }

        //open connection to database
        private bool OpenConnection()
        {
            return _OpenConnection( ref connection ); 
        }

             //open connection to database
        public bool _OpenConnection( ref MySqlConnection conn )
        {
            try
            {
                conn.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("服务没有响应");
                        break;

                    case 1045:
                        MessageBox.Show("用户名/密码不正确");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            return _CloseConnection( ref connection ); 
        }

        public bool _CloseConnection( ref MySqlConnection conn )
        {
            try
            {
                conn.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public void Disconnect()
        {
            CloseConnection(); 
        }

        private static string escape_string(string text)
        {
            return Regex.Replace(text, @"[\x00'""\b\n\r\t\cZ\\%_]",
                delegate(Match match)
                {
                    string v = match.Value;
                    switch (v)
                    {
                        case "\x00":            // ASCII NUL (0x00) character
                            return "\\0";
                        case "\b":              // BACKSPACE character
                            return "\\b";
                        case "\n":              // NEWLINE (linefeed) character
                            return "\\n";
                        case "\r":              // CARRIAGE RETURN character
                            return "\\r";
                        case "\t":              // TAB
                            return "\\t";
                        case "\u001A":          // Ctrl-Z
                            return "\\Z";
                        default:
                            return "\\" + v;
                    }
                });
        } 

        public static string mysql_escape(string str)
        {
            return Regex.Replace(str, @"[\x00'""\b\n\r\t\cZ\\%_]",
                delegate(Match match)
                {
                    string v = match.Value;
                    switch (v)
                    {
                        case "\x00":            // ASCII NUL (0x00) character
                            return "\\0";
                        case "\b":              // BACKSPACE character
                            return "\\b";
                        case "\n":              // NEWLINE (linefeed) character
                            return "\\n";
                        case "\r":              // CARRIAGE RETURN character
                            return "\\r";
                        case "\t":              // TAB
                            return "\\t";
                        case "\u001A":          // Ctrl-Z
                            return "\\Z";
                        default:
                            return "\\" + v;
                    }
                });
        } 

        public int _execute_sql(ref MySqlConnection conn, ref string sql)
        {
            int ret = 0;
            MySqlCommand cmd;

            do
            {
                if (conn.State == System.Data.ConnectionState.Closed
                    || conn.State == System.Data.ConnectionState.Broken)
                {
                    break; 
                }

                cmd = new MySqlCommand(sql, conn);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    //When handling errors, you can your application's response based on the error number.
                    //The two most common error numbers when connecting are as follows:
                    //0: Cannot connect to server.
                    //1045: Invalid user name and/or password.

                    System.Diagnostics.Debug.WriteLine(string.Format("execuate sql error {0} {1}\n", ex.Number, ex.Message));
                }
            } while (false);
            
            return ret; 
        }

        public int execute_sql(ref string sql)
        {
            int ret = 0;
            MySqlCommand cmd;

            do
            {
                if (false == OpenConnection())
                {
                    break;
                }

                ret = _execute_sql( ref connection, ref sql); 
                CloseConnection(); 
            } while (false);

            return ret;
        }

        //Insert statement
        public void Insert()
        {
            string query = "INSERT INTO tableinfo (name, age) VALUES('John Smith', '33')";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);
                
                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Update statement
        public void Update()
        {
            string query = "UPDATE tableinfo SET name='Joe', age='22' WHERE name='John Smith'";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Delete statement
        public void Delete( string table_name, string id )
        {
            string query = "DELETE FROM "; 
            query += table_name; 
            query += "WHERE id=";
            query += id;
            query += ";"; 

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        //Select statement
        public int Select(string table_name, ref DataTable data_table )
        {
            int ret = 0; 
            string query;
            MySqlDataAdapter adapter;
            bool conn_open = false; 

            do
            {
                query = "SELECT * FROM "; 
                query += table_name;

                //Open connection

                
                if (this.OpenConnection() != true)
                {
                    break;
                }

                conn_open = true;

                try
                {
                    //Create Command
                    adapter = new MySqlDataAdapter(query, connection);
                    if (adapter == null)
                    {
                        ret = -1;
                        break;
                    }

                    adapter.Fill(data_table);
                }
                catch (MySqlException ex)
                {
                    ret = -1;
                    MessageBox.Show(string.Format("查询表失败 {0}!", ex.Message.ToString()));
                    break;
                }
            } while (false);
            if (conn_open == true)
            {
                this.CloseConnection();
            }

            return ret; 
        }

        public const Int32 ROW_COUNT_ONCE = 5; 

        public int Select(string table_name,
            string column_name, 
            process_html_text_callback callback,
            data_process context)
        {
            int ret = 0;
            int _ret = 0;
            bool __ret; 

            Int32 read_row_count; 
            Int32 _row_count = 0; 
            Int32 row_index = 0;
            Int32 _row_index; 
            string query;
            MySqlCommand cmd; 
            MySqlDataReader dataReader; 
            string[] datas; 
            string[] ids; 

            do
            {
                if (column_name.Length == 0)
                {
                    ret = -1;
                    break;
                }

                if (this.OpenConnection() != true)
                {
                    break; 
                }

                query = "SELECT COUNT(*) FROM ";
                query += table_name;
                
                cmd = new MySqlCommand(query, connection);
                dataReader = cmd.ExecuteReader(); 
                cmd.Parameters.Clear();

                do
                {
                    _ret = 0; 
                    try
                    {
                        __ret = dataReader.Read();

                        if (__ret == false)
                        {
                            _ret = -1; 
                            break;
                        }
                    }
                    catch (MySqlException ex)
                    {
                        _ret = -1; 
   
                        //When handling errors, you can your application's response based on the error number.
                        //The two most common error numbers when connecting are as follows:
                        //0: Cannot connect to server.
                        //1045: Invalid user name and/or password.

                        System.Diagnostics.Debug.WriteLine(string.Format("execuate sql error {0} {1}\n", ex.Number, ex.Message));
                    }

                    if( _ret < 0 )
                    {
                        break; 
                    }

                    _row_count = dataReader.GetInt32(0); 
                }while( false ); 

                dataReader.Close();
                this.CloseConnection();

                row_index = 0; 

                if( row_index >= _row_count )
                {
                    break; 
                }
                
                datas = new string[ROW_COUNT_ONCE]; 
                ids = new string[ROW_COUNT_ONCE]; 

                for( ; ; )
                {
                    if( row_index >= _row_count )
                    {
                        break; 
                    }

                    if (this.OpenConnection() != true)
                    {
                        break; 
                    }

                    query = "SELECT * FROM ";
                    query += table_name;
                    query += " "; 
                    query += "LIMIT "; 
                    query += row_index.ToString(); 
                    query += ","; 

                    if( ( _row_count - row_index ) < ROW_COUNT_ONCE )
                    {
                        read_row_count = _row_count - row_index; 
                    }
                    else
                    {
                        read_row_count = ROW_COUNT_ONCE; 
                    }

                    query += read_row_count.ToString(); 
                    query += ";"; 
                    
                    cmd = new MySqlCommand(query, connection);

                    dataReader = cmd.ExecuteReader();
                    cmd.Parameters.Clear();

                    _row_index = 0; 

                    for( ; ; )
                    {
                        do 
                        {
                            _ret = 0; 

                            try
                            {
                                __ret = dataReader.Read();

                                if (__ret == false)
                                {
                                    _ret = -1; 
                                    break;
                                }
                            }
                            catch (MySqlException ex)
                            {
                                _ret = -1; 
                                System.Diagnostics.Debug.WriteLine(string.Format("execuate sql error {0} {1}\n", ex.Number, ex.Message));
                            }
                        }while( false );

                        if (_ret == -1)
                        {
                            break;
                        }

                        ids[ _row_index ] = dataReader["id"].ToString(); 
                        datas[ _row_index ] = dataReader[column_name].ToString();
                        _row_index += 1; 
                    }

                    dataReader.Close();
                    this.CloseConnection();

                    for( Int32 i = 0; i < _row_index; i ++ )
                    {
                        _ret = callback(ids[i], datas[ i ].ToString(), context);
                        if(_ret == STOP_DATA_BASE_PROCESS)
                        {
                            break; 
                        }
                    }

                    if (_ret == STOP_DATA_BASE_PROCESS)
                    {
                        break;
                    }

                    row_index += ROW_COUNT_ONCE; ;
                }
            } while (false); 

            return ret;
        }

        //Count statement
        public int Count( string table_name )
        {
            string query = "SELECT Count(*) FROM ";
            int Count = -1;

            query += table_name; 
            //Open Connection
            if (this.OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar()+"");
                
                //close Connection
                this.CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }

        //Backup
        public void Backup()
        {
            try
            {
                DateTime Time = DateTime.Now;
                int year = Time.Year;
                int month = Time.Month;
                int day = Time.Day;
                int hour = Time.Hour;
                int minute = Time.Minute;
                int second = Time.Second;
                int millisecond = Time.Millisecond;

                string path;
                path = "C:\\" + year + "-" + month + "-" + day + "-" + hour + "-" + minute + "-" + second + "-" + millisecond + ".sql";
                StreamWriter file = new StreamWriter(path);

                
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "mysqldump";
                psi.RedirectStandardInput = false;
                psi.RedirectStandardOutput = true;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}", uid, password, server, database);
                psi.UseShellExecute = false;

                Process process = Process.Start(psi);

                string output;
                output = process.StandardOutput.ReadToEnd();
                file.WriteLine(output);
                process.WaitForExit();
                file.Close();
                process.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show(string.Format("备份失败 {0}!", ex.Message.ToString()) );
            }
        }

        //Restore
        public void Restore()
        {
            try
            {
                //Read file from C:\
                string path;
                path = "C:\\MySqlBackup.sql";
                StreamReader file = new StreamReader(path);
                string input = file.ReadToEnd();
                file.Close();


                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "mysql";
                psi.RedirectStandardInput = true;
                psi.RedirectStandardOutput = false;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}", uid, password, server, database);
                psi.UseShellExecute = false;

                
                Process process = Process.Start(psi);
                process.StandardInput.WriteLine(input);
                process.StandardInput.Close();
                process.WaitForExit();
                process.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error , unable to Restore {0}!", ex.Message );
            }
        }
    }
}
