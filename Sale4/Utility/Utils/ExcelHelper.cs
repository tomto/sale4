using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using NPOI;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.Formula.Eval;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Utility.Extensions;

namespace Utility.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class ExcelHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="titles"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static MemoryStream Export<T>(List<T> items, Dictionary<string, string> titles)
        {
            if (items.IsNullOrEmpty())
            {
                return null;
            }

            //创建Excel文件
            var book = new HSSFWorkbook();
            //添加一个sheet
            var sheet = book.CreateSheet("sheet1");
            var row = sheet.CreateRow(0);

            #region 样式
            var fCellStyle = (HSSFCellStyle)book.CreateCellStyle();
            var ffont = (HSSFFont)book.CreateFont();
            ffont.FontName = "宋体";
            ffont.Color = HSSFColor.Black.Index;
            ffont.Boldweight = short.MaxValue;//字体加粗
            fCellStyle.SetFont(ffont);

            fCellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//垂直对齐
            fCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;//水平对齐
            #endregion

            int index = 0;
            foreach (var title in titles)
            {
                var thisCell = row.CreateCell(index);
                thisCell.SetCellValue(title.Key);
                thisCell.CellStyle = fCellStyle;
                sheet.SetColumnWidth(index, 15 * 270);
                index++;
            }

            var data = ToDataTable(items);

            for (var i = 0; i < data.Rows.Count; i++)
            {
                var rowtemp = sheet.CreateRow(i + 1);
                var dr = data.Rows[i];

                index = 0;
                foreach (var title in titles)
                {
                    var value = "";
                    rowtemp.CreateCell(index).SetCellValue(dr[title.Value].ToString());
                    index++;
                }
            }
            //写入到客户端
            var ms = new MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return ms;//File(ms, "application/vnd.ms-excel", fileName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static MemoryStream Export(DataTable items)
        {
            if (items.IsNullOrEmpty())
            {
                return null;
            }

            //创建Excel文件
            var book = new HSSFWorkbook();
            //添加一个sheet
            var sheet = book.CreateSheet("sheet1");

            var row = sheet.CreateRow(0);

            #region 样式
            var fCellStyle = (HSSFCellStyle)book.CreateCellStyle();
            var ffont = (HSSFFont)book.CreateFont();
            ffont.FontName = "宋体";
            ffont.Color = HSSFColor.Black.Index;
            ffont.Boldweight = short.MaxValue;//字体加粗
            fCellStyle.SetFont(ffont);

            fCellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//垂直对齐
            fCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;//水平对齐
            #endregion

            int index = 0;
            foreach (var title in items.Columns)
            {
                var thisCell = row.CreateCell(index);
                thisCell.SetCellValue(title.ToString());
                thisCell.CellStyle = fCellStyle;
                sheet.SetColumnWidth(index, 15 * 270);
                index++;
            }

            for (var i = 0; i < items.Rows.Count; i++)
            {
                var rowtemp = sheet.CreateRow(i + 1);
                var dr = items.Rows[i];

                for (int j = 0; j < items.Columns.Count; j++)
                {
                    var value = dr[j].ToString();
                    rowtemp.CreateCell(j).SetCellValue(value);
                }
            }
            //写入到客户端
            var ms = new MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return ms;//File(ms, "application/vnd.ms-excel", fileName);
        }

        /// <summary>
        /// Convert a List{T} to a DataTable.
        /// </summary>
        private static DataTable ToDataTable<T>(IEnumerable<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }

        /// <summary>
        /// Determine of specified type is nullable
        /// </summary>
        public static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary>
        /// Return underlying type if type is Nullable otherwise return the type
        /// </summary>
        public static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }

        /// <summary>
        /// Excel文件导成Datatable
        /// </summary>
        /// <param name="strFilePath">Excel文件目录地址</param>
        /// <param name="strTableName">Datatable表名</param>
        /// <param name="iSheetIndex">Excel sheet index</param>
        /// <returns></returns>
        public static DataTable Import(string strFilePath, string strTableName = "", int iSheetIndex = 0)
        {
            string strExtName = Path.GetExtension(strFilePath);

            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(strTableName))
            {
                dt.TableName = strTableName;
            }

            if (strExtName != null && (strExtName.Equals(".xls") || strExtName.Equals(".xlsx")))
            {
                using (FileStream stream = new FileStream(strFilePath, FileMode.Open, FileAccess.Read))
                {
                    //HSSFWorkbook workbook = new HSSFWorkbook(stream);
                    var workbook = default(IWorkbook);

                    if (POIXMLDocument.HasOOXMLHeader(stream))
                    {
                        workbook = new XSSFWorkbook(stream);
                    }
                    else
                    {
                        workbook = new HSSFWorkbook(stream);
                    }

                    ISheet sheet = workbook.GetSheetAt(iSheetIndex);

                    //列头
                    foreach (ICell item in sheet.GetRow(sheet.FirstRowNum).Cells)
                    {
                        dt.Columns.Add(item.ToString(), typeof(string));
                    }

                    //写入内容
                    for (int index = sheet.FirstRowNum; index <= sheet.LastRowNum; index++)
                    {
                        var row = sheet.GetRow(index);

                        if (row.RowNum == sheet.FirstRowNum)
                        {
                            continue;
                        }

                        DataRow dr = dt.NewRow();
                        foreach (ICell cell in row.Cells)
                        {
                            dr[cell.ColumnIndex] = GetCellValue(cell);
                        }
                        dt.Rows.Add(dr);
                    }
                }
            }

            return dt;
        }

        private static dynamic GetCellValue(ICell cell)
        {
            dynamic value;
            try
            {
                switch (cell.CellType)
                {
                    case CellType.Boolean:
                        value = cell.BooleanCellValue;
                        break;
                    case CellType.Error:
                        value = ErrorEval.GetText(cell.ErrorCellValue);
                        break;
                    case CellType.Formula:
                        switch (cell.CachedFormulaResultType)
                        {
                            case CellType.Boolean:
                                value = cell.BooleanCellValue;
                                break;
                            case CellType.Error:
                                value = ErrorEval.GetText(cell.ErrorCellValue);
                                break;
                            case CellType.Numeric:
                                if (DateUtil.IsCellDateFormatted(cell))
                                {
                                    value = cell.DateCellValue.ToString("yyyy-MM-dd hh:MM:ss");
                                }
                                else
                                {
                                    value = cell.NumericCellValue;
                                }
                                break;
                            case CellType.String:
                                value = cell.StringCellValue;
                                break;
                            default:
                                value = cell.StringCellValue;
                                break;
                        }
                        break;
                    case CellType.Numeric:
                        if (DateUtil.IsCellDateFormatted(cell))
                        {
                            value = cell.DateCellValue.ToString("yyyy-MM-dd hh:MM:ss");
                        }
                        else
                        {
                            value = cell.NumericCellValue;
                        }
                        break;
                    case CellType.String:
                        value = cell.StringCellValue;
                        break;
                    default:
                        value = cell.ToString();
                        break;
                }
            }
            catch (Exception)
            {
                value = cell.ToString();
            }

            return value;
        }
    }
}