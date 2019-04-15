using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using DataTable = System.Data.DataTable;
using IFont = NPOI.SS.UserModel.IFont;

namespace Common
{
    public static class ExcelUtility
    {
        /// <summary>
        /// 将DataTable转换成流
        /// </summary>
        public static Stream RenderDataTableToExcel(DataTable sourceTable)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            ISheet sheet = workbook.CreateSheet("Sheet1");
            IRow headerRow = sheet.CreateRow(0);
            // handling header. 
            foreach (DataColumn column in sourceTable.Columns)
            {
                headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
            }
            // handling value. 
            int rowIndex = 1;
            foreach (DataRow row in sourceTable.Rows)
            {
                IRow dataRow = sheet.CreateRow(rowIndex);
                foreach (DataColumn column in sourceTable.Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                }
                rowIndex++;
            }
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;
            return ms;
        }

        /// <summary>
        /// 将DataTable转换成流
        /// </summary>
        public static Stream RenderDataTableToExcelAndImg(DataTable sourceTable, string path)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();

            ISheet sheet = workbook.CreateSheet("Sheet1");

            HSSFRow hssfRow = (HSSFRow)sheet.CreateRow(1);

            HSSFCellStyle cellStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            cellStyle.Alignment = HorizontalAlignment.Center;
            hssfRow.HeightInPoints = 120;
            HSSFFont cellfont = (HSSFFont)workbook.CreateFont();
            cellfont.FontHeightInPoints = 10;
            cellStyle.SetFont(cellfont);
            byte[] picByte = File.ReadAllBytes(path);
            workbook.AddPicture(picByte, PictureType.PNG);
            IRow headerRow = sheet.CreateRow(0);
            HSSFPatriarch patriarch = (HSSFPatriarch)sheet.CreateDrawingPatriarch();
            //picture.Resize();
            int rowIndex = 1;
            foreach (DataRow row in sourceTable.Rows)
            {
                IRow dataRow = sheet.CreateRow(rowIndex);
                foreach (DataColumn column in sourceTable.Columns)
                {
                    if (column.Ordinal == 0)
                    {
                        HSSFClientAnchor anchor = new HSSFClientAnchor(0, 10, 1023, 0, 2 + (column.Ordinal), 0, 2,
                            1 + (column.Ordinal));
                        HSSFPicture picture = (HSSFPicture)patriarch.CreatePicture(anchor, 1);
                        dataRow.CreateCell(column.Ordinal).CellStyle = cellStyle;
                    }
                    else
                    {
                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                    }
                }
                rowIndex++;
            }
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;
            return ms;
        }

        /// <summary>
        /// 设置字体颜色大小
        /// </summary>
        public static Stream ApplyStyleToFile(Stream inputStream, string fontName, short fontSize)
        {
            HSSFWorkbook workbook = new HSSFWorkbook(inputStream);
            ICellStyle style = workbook.CreateCellStyle();
            ICellStyle style1 = workbook.CreateCellStyle();
            IFont cellFont = workbook.CreateFont();
            cellFont.FontHeightInPoints = 10;
            cellFont.FontName = "黑体";
            IFont font = workbook.CreateFont();
            font.FontHeightInPoints = fontSize;
            font.FontName = fontName;
            font.Boldweight = short.MaxValue;
            style.SetFont(font);
            style.Alignment = HorizontalAlignment.Center;
            style1.SetFont(cellFont);
            style1.Alignment = HorizontalAlignment.Center;
            MemoryStream ms = new MemoryStream();
            int i;
            for (i = 0; i < workbook.NumberOfSheets; i++)
            {
                ISheet sheets = workbook.GetSheetAt(0);
                for (int k = sheets.FirstRowNum; k <= sheets.LastRowNum; k++)
                {
                    sheets.AutoSizeColumn(k);
                    IRow row = sheets.GetRow(k);
                    for (int l = row.FirstCellNum; l < row.LastCellNum; l++)
                    {
                        if (k == 0)
                        {
                            ICell cell = row.GetCell(l);
                            cell.CellStyle = style;
                        }
                        else
                        {
                            ICell cell = row.GetCell(l);
                            cell.CellStyle = style1;
                        }
                    }
                }
            }
            workbook.Write(ms);
            ms.Flush();
            return ms;
        }

        /// <summary>
        /// 读取DataTable列并返回Excel
        /// </summary>
        public static Stream RenderDataTableToExcelCols(DataTable sourceTable)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            ISheet sheet = workbook.CreateSheet("Sheet1");
            IRow headerRow = sheet.CreateRow(0);
            // handling header. 
            foreach (DataColumn column in sourceTable.Columns)
                headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;
            return ms;
        }

        /// <summary>
        /// 读取Excel列并返回DataTable
        /// </summary>
        public static DataTable RenderDateTableFromExcelCols(Stream excelFileStream, int sheetIndex, int headerRowIndex)
        {
            DataTable table = new DataTable();
            HSSFWorkbook workbook = new HSSFWorkbook(excelFileStream);
            ISheet sheet = workbook.GetSheetAt(sheetIndex);
            IRow headerRow = sheet.GetRow(headerRowIndex);
            int cellCount = headerRow.LastCellNum;
            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }
            excelFileStream.Close();
            return table;
        }

        /// <summary>
        /// 读取Excel并返回DataTable
        /// </summary>
        public static DataTable RenderDataTableFromExcel(Stream excelFileStream, int sheetIndex, int headerRowIndex)
        {
            DataTable table = new DataTable();
            HSSFWorkbook workbook = new HSSFWorkbook(excelFileStream);
            ISheet sheet = workbook.GetSheetAt(sheetIndex);
            IRow headerRow = sheet.GetRow(headerRowIndex);
            int cellCount = headerRow.LastCellNum;
            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }
            for (int i = (sheet.FirstRowNum + 1); i < sheet.LastRowNum + 1; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow dataRow = table.NewRow();
                for (int j = 0; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                    {
                        var cellVale = row.GetCell(j).ToString();
                        dataRow[j] = cellVale;
                    }
                }
                table.Rows.Add(dataRow);
            }
            excelFileStream.Close();
            return table;
        }

        /// <summary>
        /// 将Excel 转换成DataTable 
        /// </summary>
        public static DataTable CreateExcelDataSource(string excelPath, string sheetName = null)
        {
            if (string.IsNullOrEmpty(sheetName))
            {
                sheetName = "Sheet1";
            }
            DataTable dt = null;
            try
            {
                string connetionStr = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + excelPath +
                                      ";Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'";
                OleDbConnection conn = new OleDbConnection(connetionStr);
                conn.Open();
                //返回Excel的架构，包括各个sheet表的名称,类型，创建时间和修改时间等  

                //从指定的表明查询数据,可先把所有表明列出来供用户选择
                string strExcel = "select * from [" + sheetName + "$]";
                var myCommand = new OleDbDataAdapter(strExcel, connetionStr);
                try
                {
                    dt = new DataTable();
                    myCommand.Fill(dt);
                }
                finally
                {
                    myCommand.Dispose();
                    conn.Close();
                    conn.Dispose();
                }

            }
            catch
            {
                string connetionStr = " Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = " + excelPath +
                                      ";Extended Properties=Excel 8.0";
                OleDbConnection conn = new OleDbConnection(connetionStr);
                conn.Open();
                //返回Excel的架构，包括各个sheet表的名称,类型，创建时间和修改时间等  
                DataTable dtSheetName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,
                    new object[] { null, null, null, "Table" });
                //包含excel中表名的字符串数组

                string strExcel = "select * from [" + sheetName + "$]";
                var myCommand = new OleDbDataAdapter(strExcel, connetionStr);
                try
                {
                    dt = new DataTable();
                    myCommand.Fill(dt);
                }
                finally
                {
                    myCommand.Dispose();
                    conn.Close();
                    conn.Dispose();
                }
            }
            return dt;
        }

        /// <summary>
        /// 获取Excel表名
        /// </summary>
        public static string[] GetSheetName(string excelPath)
        {
            string[] sheetName = { };
            try
            {
                string connetionStr = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + excelPath +
                                      ";Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'";
                OleDbConnection conn = new OleDbConnection(connetionStr);
                conn.Open();
                //返回Excel的架构，包括各个sheet表的名称,类型，创建时间和修改时间等  
                DataTable dtSheetName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,
                    new object[] { null, null, null, "Table" });
                //包含excel中表名的字符串数组
                if (dtSheetName != null)
                {
                    sheetName = new string[dtSheetName.Rows.Count];
                    for (int k = 0; k < dtSheetName.Rows.Count; k++)
                    {
                        sheetName[k] = dtSheetName.Rows[k]["TABLE_NAME"].ToString();
                    }
                    conn.Close();
                    conn.Dispose();

                }

            }
            catch (Exception)
            {
                string connetionStr = " Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = " + excelPath +
                                      ";Extended Properties=Excel 8.0";
                OleDbConnection conn = new OleDbConnection(connetionStr);
                conn.Open();
                //返回Excel的架构，包括各个sheet表的名称,类型，创建时间和修改时间等  
                DataTable dtSheetName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,
                    new object[] { null, null, null, "Table" });
                //包含excel中表名的字符串数组
                if (dtSheetName != null)
                {
                    sheetName = new string[dtSheetName.Rows.Count];
                    for (int k = 0; k < dtSheetName.Rows.Count; k++)
                    {
                        sheetName[k] = dtSheetName.Rows[k]["TABLE_NAME"].ToString();
                    }
                    conn.Close();
                    conn.Dispose();
                }
            }
            return sheetName;
        }
    }
}
