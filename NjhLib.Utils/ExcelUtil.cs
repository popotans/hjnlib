using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSExcel = Microsoft.Office.Interop.Excel;

namespace NjhLib.Utils
{
    public class ExcelUtil
    {
          // TODO: Fixed bug of regional settings 
        // http://support.microsoft.com/?kbid=320369
        // var ci = new System.Globalization.CultureInfo("en-US");
        //_workbook.GetType().InvokeMember("Add", System.Reflection.BindingFlags.InvokeMethod, null, _workbook, null, ci);

        MSExcel.Application _excel;
        MSExcel.Workbook _workbook;
        MSExcel.Worksheet _worksheet;
        bool _visible = false;
        System.Globalization.CultureInfo _oldCI;

        public ExcelUtil()
        {

        }

        public ExcelUtil(string xlsPath, string lang = "en-US")
        {
            this.Open(xlsPath, lang);
        }



        public void Open(string xlsPath, string lang = "en-US")
        {
            _excel = new MSExcel.Application();

            _oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            // TODO: Get Office Language
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(lang);

            _excel.Visible = _visible;
            _excel.DisplayAlerts = _visible;

            //var ci = new System.Globalization.CultureInfo("en-US");
            //var objBooks = _excel.Workbooks;
            //objBooks.GetType().InvokeMember("Add", System.Reflection.BindingFlags.InvokeMethod, null, objBooks, null, ci);

            _workbook = _excel.Workbooks.Open(xlsPath);

            this.SetActiveSheet(1);
        }




        public void Save()
        {
            _workbook.Save();
        }

        public void Close()
        {
            if (!_visible)
            {
                if (null != _excel)
                    _excel.Quit();
            }
            _worksheet = null;
            _workbook = null;
            _excel = null;
            GC.Collect();

            if (null != _oldCI)
                System.Threading.Thread.CurrentThread.CurrentCulture = _oldCI;
        }

        public void Dispose()
        {
            this.Close();
        }

        public bool ExcelVisible
        {
            get
            {
                return _visible;
            }

            set
            {
                _visible = value;

                _excel.Visible = _visible;
                _excel.DisplayAlerts = _visible;
            }
        }

        public void SetActiveSheet(int index)
        {
            _worksheet = (MSExcel.Worksheet)_workbook.Sheets[index];
        }

        public void AddWorkSheet(int count)
        {
            _worksheet = (MSExcel.Worksheet)_workbook.Worksheets.Add(Type.Missing, _workbook.Worksheets[this.GetWorkSheetsCount()], count, Type.Missing);
        }

        private int GetWorkSheetsCount()
        {
            return _workbook.Worksheets.Count;
        }

        public void WriteRow(int rowIndex, object[] values)
        {
            MSExcel.Range cell;
            if (null != values)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    cell = (MSExcel.Range)_worksheet.Cells[rowIndex, i];
                    cell.Value = values[i];
                }
            }
        }

        public void WriteRowData(int row, object[] coluValues)
        {
            MSExcel.Range cell;
            int length = coluValues.Length;
            if (null != coluValues)
            {
                for (int i = 0; i < length; i++)
                {
                    cell = (MSExcel.Range)_worksheet.Cells[row, i + 1];
                    cell.Value = coluValues[i];
                }
            }
        }

        public void WriteCell(int rowIndex, int colIndex, object value)
        {
            MSExcel.Range cell;
            cell = (MSExcel.Range)_worksheet.Cells[rowIndex, colIndex];
            cell.Value = value;
        }

        public object ReadCell(int rowIndex, int colIndex)
        {
            MSExcel.Range cell;
            cell = (MSExcel.Range)_worksheet.Cells[rowIndex, colIndex];
            return cell.Value;
        }

        public object[] ReadRowArray(int rowIndex, int colCount)
        {
            var objs = new object[colCount];

            for (int j = 1; j <= colCount; j++)
            {
                MSExcel.Range cell;
                cell = (MSExcel.Range)_worksheet.Cells[rowIndex, j];
                objs[j - 1] = cell.Value;
            }
            return objs;
        }

        public void GetUsedRange(out int rowCount, out int colCount)
        {
            MSExcel.Range range;
            range = _worksheet.UsedRange;
            rowCount = range.Rows.Count;
            colCount = range.Columns.Count;
        }
    }
}
