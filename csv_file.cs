using System;
using System.Collections.Generic;
using System.Collections; 
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace HtmlDataProcess
{
    /// <summary>
/// Determines how empty lines are interpreted when reading CSV files.
/// These values do not affect empty lines that occur within quoted fields
/// or empty lines that appear at the end of the input file.
/// </summary>
public enum EmptyLineBehavior
{
    /// <summary>
    /// Empty lines are interpreted as a line with zero columns.
    /// </summary>
    NoColumns,
    /// <summary>
    /// Empty lines are interpreted as a line with a single empty column.
    /// </summary>
    EmptyColumn,
    /// <summary>
    /// Empty lines are skipped over as though they did not exist.
    /// </summary>
    Ignore,
    /// <summary>
    /// An empty line is interpreted as the end of the input file.
    /// </summary>
    EndOfFile,
}

/// <summary>
/// Common base class for CSV reader and writer classes.
/// </summary>
public abstract class CsvFileCommon
{
    /// <summary>
    /// These are special characters in CSV files. If a column contains any
    /// of these characters, the entire column is wrapped in double quotes.
    /// </summary>
    protected char[] SpecialChars = new char[] { ',', '"', '\r', '\n' };

    // Indexes into SpecialChars for characters with specific meaning
    private const int DelimiterIndex = 0;
    private const int QuoteIndex = 1;

    /// <summary>
    /// Gets/sets the character used for column delimiters.
    /// </summary>
    public char Delimiter
    {
        get { return SpecialChars[DelimiterIndex]; }
        set { SpecialChars[DelimiterIndex] = value; }
    }

    /// <summary>
    /// Gets/sets the character used for column quotes.
    /// </summary>
    public char Quote
    {
        get { return SpecialChars[QuoteIndex]; }
        set { SpecialChars[QuoteIndex] = value; }
    }
}

/// <summary>
/// Class for reading from comma-separated-value (CSV) files
/// </summary>
public class CsvFileReader : CsvFileCommon, IDisposable
{
    // Private members
    private StreamReader Reader;
    private string CurrLine;
    private int CurrPos;
    private EmptyLineBehavior EmptyLineBehavior;

    /// <summary>
    /// Initializes a new instance of the CsvFileReader class for the
    /// specified stream.
    /// </summary>
    /// <param name="stream">The stream to read from</param>
    /// <param name="emptyLineBehavior">Determines how empty lines are handled</param>
    public CsvFileReader(Stream stream,
        EmptyLineBehavior emptyLineBehavior = EmptyLineBehavior.NoColumns)
    {
        Reader = new StreamReader(stream);
        EmptyLineBehavior = emptyLineBehavior;
    }

    /// <summary>
    /// Initializes a new instance of the CsvFileReader class for the
    /// specified file path.
    /// </summary>
    /// <param name="path">The name of the CSV file to read from</param>
    /// <param name="emptyLineBehavior">Determines how empty lines are handled</param>
    public CsvFileReader(string path,
        EmptyLineBehavior emptyLineBehavior = EmptyLineBehavior.NoColumns)
    {
        Reader = new StreamReader(path);
        EmptyLineBehavior = emptyLineBehavior;
    }

    /// <summary>
    /// Reads a row of columns from the current CSV file. Returns false if no
    /// more data could be read because the end of the file was reached.
    /// </summary>
    /// <param name="columns">Collection to hold the columns read</param>
    public bool ReadRow(List<string> columns)
    {
        // Verify required argument
        if (columns == null)
            throw new ArgumentNullException("columns");

    ReadNextLine:
        // Read next line from the file
        CurrLine = Reader.ReadLine();
        CurrPos = 0;
        // Test for end of file
        if (CurrLine == null)
            return false;
        // Test for empty line
        if (CurrLine.Length == 0)
        {
            switch (EmptyLineBehavior)
            {
                case EmptyLineBehavior.NoColumns:
                    columns.Clear();
                    return true;
                case EmptyLineBehavior.Ignore:
                    goto ReadNextLine;
                case EmptyLineBehavior.EndOfFile:
                    return false;
            }
        }

        // Parse line
        string column;
        int numColumns = 0;
        while (true)
        {
            // Read next column
            if (CurrPos < CurrLine.Length && CurrLine[CurrPos] == Quote)
                column = ReadQuotedColumn();
            else
                column = ReadUnquotedColumn();
            // Add column to list
            if (numColumns < columns.Count)
                columns[numColumns] = column;
            else
                columns.Add(column);
            numColumns++;
            // Break if we reached the end of the line
            if (CurrLine == null || CurrPos == CurrLine.Length)
                break;
            // Otherwise skip delimiter
            Debug.Assert(CurrLine[CurrPos] == Delimiter);
            CurrPos++;
        }
        // Remove any unused columns from collection
        if (numColumns < columns.Count)
            columns.RemoveRange(numColumns, columns.Count - numColumns);
        // Indicate success
        return true;
    }

    /// <summary>
    /// Reads a quoted column by reading from the current line until a
    /// closing quote is found or the end of the file is reached. On return,
    /// the current position points to the delimiter or the end of the last
    /// line in the file. Note: CurrLine may be set to null on return.
    /// </summary>
    private string ReadQuotedColumn()
    {
        // Skip opening quote character
        Debug.Assert(CurrPos < CurrLine.Length && CurrLine[CurrPos] == Quote);
        CurrPos++;

        // Parse column
        StringBuilder builder = new StringBuilder();
        while (true)
        {
            while (CurrPos == CurrLine.Length)
            {
                // End of line so attempt to read the next line
                CurrLine = Reader.ReadLine();
                CurrPos = 0;
                // Done if we reached the end of the file
                if (CurrLine == null)
                    return builder.ToString();
                // Otherwise, treat as a multi-line field
                builder.Append(Environment.NewLine);
            }

            // Test for quote character
            if (CurrLine[CurrPos] == Quote)
            {
                // If two quotes, skip first and treat second as literal
                int nextPos = (CurrPos + 1);
                if (nextPos < CurrLine.Length && CurrLine[nextPos] == Quote)
                    CurrPos++;
                else
                    break;  // Single quote ends quoted sequence
            }
            // Add current character to the column
            builder.Append(CurrLine[CurrPos++]);
        }

        if (CurrPos < CurrLine.Length)
        {
            // Consume closing quote
            Debug.Assert(CurrLine[CurrPos] == Quote);
            CurrPos++;
            // Append any additional characters appearing before next delimiter
            builder.Append(ReadUnquotedColumn());
        }
        // Return column value
        return builder.ToString();
    }

    /// <summary>
    /// Reads an unquoted column by reading from the current line until a
    /// delimiter is found or the end of the line is reached. On return, the
    /// current position points to the delimiter or the end of the current
    /// line.
    /// </summary>
    private string ReadUnquotedColumn()
    {
        int startPos = CurrPos;
        CurrPos = CurrLine.IndexOf(Delimiter, CurrPos);
        if (CurrPos == -1)
            CurrPos = CurrLine.Length;
        if (CurrPos > startPos)
            return CurrLine.Substring(startPos, CurrPos - startPos);
        return String.Empty;
    }

    // Propagate Dispose to StreamReader
    public void Dispose()
    {
        Reader.Dispose();
    }
}

/// <summary>
/// Class for writing to comma-separated-value (CSV) files.
/// </summary>
public class CsvFileWriter : CsvFileCommon, IDisposable
{
    // Private members
    private StreamWriter Writer;
    private string OneQuote = null;
    private string TwoQuotes = null;
    private string QuotedFormat = null;

    /// <summary>
    /// Initializes a new instance of the CsvFileWriter class for the
    /// specified stream.
    /// </summary>
    /// <param name="stream">The stream to write to</param>
    public CsvFileWriter(Stream stream)
    {
        Writer = new StreamWriter(stream);
    }

    /// <summary>
    /// Initializes a new instance of the CsvFileWriter class for the
    /// specified file path.
    /// </summary>
    /// <param name="path">The name of the CSV file to write to</param>
    public CsvFileWriter(string path)
    {
        Writer = new StreamWriter(path);
    }

    /// <summary>
    /// Writes a row of columns to the current CSV file.
    /// </summary>
    /// <param name="columns">The list of columns to write</param>
    public void WriteRow(List<string> columns)
    {
        // Verify required argument
        if (columns == null)
            throw new ArgumentNullException("columns");

        // Ensure we're using current quote character
        if (OneQuote == null || OneQuote[0] != Quote)
        {
            OneQuote = String.Format("{0}", Quote);
            TwoQuotes = String.Format("{0}{0}", Quote);
            QuotedFormat = String.Format("{0}{{0}}{0}", Quote);
        }

        // Write each column
        for (int i = 0; i < columns.Count; i++)
        {
            // Add delimiter if this isn't the first column
            if (i > 0)
                Writer.Write(Delimiter);
            // Write this column
            if (columns[i].IndexOfAny(SpecialChars) == -1)
                Writer.Write(columns[i]);
            else
                Writer.Write(QuotedFormat, columns[i].Replace(OneQuote, TwoQuotes));
        }
        Writer.WriteLine();
    }

    // Propagate Dispose to StreamWriter
    public void Dispose()
    {
        Writer.Dispose();
    }
}
Because the .NET stream classes generally seem to be split into reading and writing, I decided to follow that same pattern with my CSV code and split it into CsvFileReader and CsvFileWriter. This also simplifies the code because neither class needs to worry about which mode the file is in or protect against the user switching modes.

Starting at the top of the code is the EmptyLineBehavior enum. After careful review, I realized there were a few valid ways to interpret an empty line within a CSV file. So the CsvFileReader class' constructor takes an argument of this type to specify how empty lines should be handled. Note that this does not affect empty lines within a quoted value, or an empty line at the end of the input file.

Next is my CsvFileCommon class. There are a few settings common to both the reader and writer classes and so I use this abstract base class to track the special characters within a CSV value that require the value to be enclosed in quotes. It also provides a way to change the characters used as delimiters and quotes.

The CsvFileReader class comes after that. This is the class that reads data from a CSV file and is the most complex class presented here. I modeled its behavior after how Microsoft Excel interprets CSV files. There are two constructors: one that accepts the name of the input file and another that accepts an input Stream. As mentioned previously, both constructors also accept an EmptyLineBehavior argument to control how empty lines are handled. The ReadRow() method is used to read a single row from the input file and populate a List<string> collection with the values read. For each value, it dispatches the appropriate parsing routine based on whether or not the first character is a quote character.

Finally, the CsvFileWriter class writes data to a CSV file. As with the CsvFileReader class, this class has two constructors. Call the WriteRow() method to write a single row to the target file using a collection of values. Each time WriteRow() is called, it checks to see if the current quote character has changed. If so, it updates the strings used to correctly format quoted output.

Both the reader and writer classes implement IDisposable, which is delegated to the StreamReader or StreamWriter class. This allows you to enclose your use of either class within a using statement to ensure the file is closed in a timely manner

Using the Code

The code was designed to be as easy as possible to use. When you call CsvFileWriter.WriteRow(), you supply a collection of the values to be written to the file. And when you call CsvFileReader.ReadRow(), the collection argument is populated with the values read in.

Listing 2 demonstrates using the classes.

Listing 2: Sample Code to Write and Read CSV files

private void WriteValues()
{
    using (var writer = new CsvFileWriter("WriteTest.csv"))
    {
        // Write each row of data
        for (int row = 0; row < 100; row++)
        {
            // TODO: Populate column values for this row
            List<string> columns = new List<string>();
            writer.WriteRow(columns);
        }
    }
}

private void ReadValues()
{
    List<string> columns = new List<string>();
    using (var reader = new CsvFileReader("ReadTest.csv"))
    {
        while (reader.ReadRow(columns))
        {
            // TODO: Do something with columns' values
        }
    }
}

    public class CsvStreamReader
    {
        private ArrayList rowAL;         //行链表,CSV文件的每一行就是一个链
        private string fileName;        //文件名
        private Encoding encoding;        //编码
        public CsvStreamReader()
        {
            this.rowAL = new ArrayList();
            this.fileName = "";
            this.encoding = Encoding.Default;
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="fileName">文件名,包括文件路径</param>
        public CsvStreamReader(string fileName)
        {
            this.rowAL = new ArrayList();
            this.fileName = fileName;
            this.encoding = Encoding.Default;
            LoadCsvFile();
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="fileName">文件名,包括文件路径</param>
        /// <param name="encoding">文件编码</param>
        public CsvStreamReader(string fileName, Encoding encoding)
        {
            this.rowAL = new ArrayList();
            this.fileName = fileName;
            this.encoding = encoding;
            LoadCsvFile();
        }
        /// <summary>
        /// 文件名,包括文件路径
        /// </summary>
        public string FileName
        {
            set
            {
                this.fileName = value;
                LoadCsvFile();
            }
        }
        /// <summary>
        /// 文件编码
        /// </summary>
        public Encoding FileEncoding
        {
            set
            {
                this.encoding = value;
            }
        }
        /// <summary>
        /// 获取行数
        /// </summary>
        public int RowCount
        {
            get
            {
                return this.rowAL.Count;
            }
        }
        /// <summary>
        /// 获取列数
        /// </summary>
        public int ColCount
        {
            get
            {
                int maxCol;
                maxCol = 0;
                for (int i = 0; i < this.rowAL.Count; i++)
                {
                    ArrayList colAL = (ArrayList)this.rowAL[i];
                    maxCol = (maxCol > colAL.Count) ? maxCol : colAL.Count;
                }
                return maxCol;
            }
        }
        /// <summary>
        /// 获取某行某列的数据
        /// row:行,row = 1代表第一行
        /// col:列,col = 1代表第一列  
        /// </summary>
        public string this[int row, int col]
        {
            get
            {
                //数据有效性验证
                CheckRowValid(row);
                CheckColValid(col);
                ArrayList colAL = (ArrayList)this.rowAL[row - 1];
                //如果请求列数据大于当前行的列时,返回空值
                if (colAL.Count < col)
                {
                    return "";
                }
                return colAL[col - 1].ToString();
            }
        }
        /// <summary>
        /// 根据最小行，最大行，最小列，最大列，来生成一个DataTable类型的数据
        /// 行等于1代表第一行
        /// 列等于1代表第一列
        /// maxrow: -1代表最大行
        /// maxcol: -1代表最大列
        /// </summary>
        public DataTable this[int minRow, int maxRow, int minCol, int maxCol]
        {
            get
            {
                //数据有效性验证
                CheckRowValid(minRow);
                CheckMaxRowValid(maxRow);
                CheckColValid(minCol);
                CheckMaxColValid(maxCol);
                if (maxRow == -1)
                {
                    maxRow = RowCount;
                }
                if (maxCol == -1)
                {
                    maxCol = ColCount;
                }
                if (maxRow < minRow)
                {
                    throw new Exception("最大行数不能小于最小行数");
                }
                if (maxCol < minCol)
                {
                    throw new Exception("最大列数不能小于最小列数");
                }
                DataTable csvDT = new DataTable();
                int i;
                int col;
                int row;
                //增加列
                for (i = minCol; i <= maxCol; i++)
                {
                    csvDT.Columns.Add(i.ToString());
                }
                for (row = minRow; row <= maxRow; row++)
                {
                    DataRow csvDR = csvDT.NewRow();
                    i = 0;
                    for (col = minCol; col <= maxCol; col++)
                    {
                        csvDR[i] = this[row, col];
                        i++;
                    }
                    csvDT.Rows.Add(csvDR);
                }
                return csvDT;
            }
        }
        /// <summary>
        /// 检查行数是否是有效的
        /// </summary>
        /// <param name="col"></param>  
        private void CheckRowValid(int row)
        {
            if (row <= 0)
            {
                throw new Exception("行数不能小于0");
            }
            if (row > RowCount)
            {
                throw new Exception("没有当前行的数据");
            }
        }
        /// <summary>
        /// 检查最大行数是否是有效的
        /// </summary>
        /// <param name="col"></param>  
        private void CheckMaxRowValid(int maxRow)
        {
            if (maxRow <= 0 && maxRow != -1)
            {
                throw new Exception("行数不能等于0或小于-1");
            }
            if (maxRow > RowCount)
            {
                throw new Exception("没有当前行的数据");
            }
        }
        /// <summary>
        /// 检查列数是否是有效的
        /// </summary>
        /// <param name="col"></param>  
        private void CheckColValid(int col)
        {
            if (col <= 0)
            {
                throw new Exception("列数不能小于0");
            }
            if (col > ColCount)
            {
                throw new Exception("没有当前列的数据");
            }
        }
        /// <summary>
        /// 检查检查最大列数是否是有效的
        /// </summary>
        /// <param name="col"></param>  
        private void CheckMaxColValid(int maxCol)
        {
            if (maxCol <= 0 && maxCol != -1)
            {
                throw new Exception("列数不能等于0或小于-1");
            }
            if (maxCol > ColCount)
            {
                throw new Exception("没有当前列的数据");
            }
        }
        /// <summary>
        /// 载入CSV文件
        /// </summary>
        private void LoadCsvFile()
        {
            //对数据的有效性进行验证
            if (this.fileName == null)
            {
                throw new Exception("请指定要载入的CSV文件名");
            }
            else if (!File.Exists(this.fileName))
            {
                throw new Exception("指定的CSV文件不存在");
            }
            else
            {
            }
            if (this.encoding == null)
            {
                this.encoding = Encoding.Default;
            }
            StreamReader sr = new StreamReader(this.fileName, this.encoding);
            string csvDataLine;
            csvDataLine = "";
            while (true)
            {
                string fileDataLine;
                fileDataLine = sr.ReadLine();
                if (fileDataLine == null)
                {
                    break;
                }
                if (csvDataLine == "")
                {
                    csvDataLine = fileDataLine;//GetDeleteQuotaDataLine(fileDataLine);
                }
                else
                {
                    csvDataLine += "\\r\\n" + fileDataLine;//GetDeleteQuotaDataLine(fileDataLine);
                }
                //如果包含偶数个引号，说明该行数据中出现回车符或包含逗号
                if (!IfOddQuota(csvDataLine))
                {
                    AddNewDataLine(csvDataLine);
                    csvDataLine = "";
                }
            }
            sr.Close();
            //数据行出现奇数个引号
            if (csvDataLine.Length > 0)
            {
                throw new Exception("CSV文件的格式有错误");
            }
        }
        /// <summary>
        /// 获取两个连续引号变成单个引号的数据行
        /// </summary>
        /// <param name="fileDataLine">文件数据行</param>
        /// <returns></returns>
        private string GetDeleteQuotaDataLine(string fileDataLine)
        {
            return fileDataLine.Replace("\"\"", "\"");
        }
        /// <summary>
        /// 判断字符串是否包含奇数个引号
        /// </summary>
        /// <param name="dataLine">数据行</param>
        /// <returns>为奇数时，返回为真；否则返回为假</returns>
        private bool IfOddQuota(string dataLine)
        {
            int quotaCount;
            bool oddQuota;
            quotaCount = 0;
            for (int i = 0; i < dataLine.Length; i++)
            {
                if (dataLine[i] == '\"')
                {
                    quotaCount++;
                }
            }
            oddQuota = false;
            if (quotaCount % 2 == 1)
            {
                oddQuota = true;
            }
            return oddQuota;
        }
        /// <summary>
        /// 判断是否以奇数个引号开始
        /// </summary>
        /// <param name="dataCell"></param>
        /// <returns></returns>
        private bool IfOddStartQuota(string dataCell)
        {
            int quotaCount;
            bool oddQuota;
            quotaCount = 0;
            for (int i = 0; i < dataCell.Length; i++)
            {
                if (dataCell[i] == '\"')
                {
                    quotaCount++;
                }
                else
                {
                    break;
                }
            }
            oddQuota = false;
            if (quotaCount % 2 == 1)
            {
                oddQuota = true;
            }
            return oddQuota;
        }
        /// <summary>
        /// 判断是否以奇数个引号结尾
        /// </summary>
        /// <param name="dataCell"></param>
        /// <returns></returns>
        private bool IfOddEndQuota(string dataCell)
        {
            int quotaCount;
            bool oddQuota;
            quotaCount = 0;
            for (int i = dataCell.Length - 1; i >= 0; i--)
            {
                if (dataCell[i] == '\"')
                {
                    quotaCount++;
                }
                else
                {
                    break;
                }
            }
            oddQuota = false;
            if (quotaCount % 2 == 1)
            {
                oddQuota = true;
            }
            return oddQuota;
        }
        /// <summary>
        /// 加入新的数据行
        /// </summary>
        /// <param name="newDataLine">新的数据行</param>
        private void AddNewDataLine(string newDataLine)
        {
            //System.Diagnostics.Debug.WriteLine("NewLine:" + newDataLine);
            ////return;
            ArrayList colAL = new ArrayList();
            string[] dataArray = newDataLine.Split(',');
            bool oddStartQuota;        //是否以奇数个引号开始
            string cellData;
            oddStartQuota = false;
            cellData = "";
            for (int i = 0; i < dataArray.Length; i++)
            {
                if (oddStartQuota)
                {
                    //因为前面用逗号分割,所以要加上逗号
                    cellData += "," + dataArray[i];
                    //是否以奇数个引号结尾
                    if (IfOddEndQuota(dataArray[i]))
                    {
                        colAL.Add(GetHandleData(cellData));
                        oddStartQuota = false;
                        continue;
                    }
                }
                else
                {
                    //是否以奇数个引号开始
                    if (IfOddStartQuota(dataArray[i]))
                    {
                        //是否以奇数个引号结尾,不能是一个双引号,并且不是奇数个引号
                        if (IfOddEndQuota(dataArray[i]) && dataArray[i].Length > 2 && !IfOddQuota(dataArray[i]))
                        {
                            colAL.Add(GetHandleData(dataArray[i]));
                            oddStartQuota = false;
                            continue;
                        }
                        else
                        {
                            oddStartQuota = true;
                            cellData = dataArray[i];
                            continue;
                        }
                    }
                    else
                    {
                        colAL.Add(GetHandleData(dataArray[i]));
                    }
                }
            }
            if (oddStartQuota)
            {
                throw new Exception("数据格式有问题");
            }
            this.rowAL.Add(colAL);
        }
        /// <summary>
        /// 去掉格子的首尾引号，把双引号变成单引号
        /// </summary>
        /// <param name="fileCellData"></param>
        /// <returns></returns>
        private string GetHandleData(string fileCellData)
        {
            if (fileCellData == "")
            {
                return "";
            }
            if (IfOddStartQuota(fileCellData))
            {
                if (IfOddEndQuota(fileCellData))
                {
                    return fileCellData.Substring(1, fileCellData.Length - 2).Replace("\"\"", "\""); //去掉首尾引号，然后把双引号变成单引号
                }
                else
                {
                    throw new Exception("数据引号无法匹配" + fileCellData);
                }
            }
            else
            {
                //考虑形如""    """"      """"""   
                if (fileCellData.Length > 2 && fileCellData[0] == '\"')
                {
                    fileCellData = fileCellData.Substring(1, fileCellData.Length - 2).Replace("\"\"", "\""); //去掉首尾引号，然后把双引号变成单引号
                }
            }
            return fileCellData;
        }
    }
}
