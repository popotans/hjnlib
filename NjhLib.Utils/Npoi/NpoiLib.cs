using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.HPSF;
using NPOI.Util;
using NPOI.SS.Util;

namespace NjhLib.Utils.Npoi
{
    public class NpoiLib
    {
        private HSSFWorkbook _workbook = null;

        #region Constructor
        public NpoiLib()
        {
            this.CreateWorkBook();
        }
        public NpoiLib(string FileTemplate)
        {
            using (FileStream file = new FileStream(FileTemplate, FileMode.Open, FileAccess.Read))
            {
                _workbook = new HSSFWorkbook(file);
            }
        }
        #endregion

        private void CreateWorkBook()
        {
            _workbook = new HSSFWorkbook();
        }
        public Sheet CreateSheet(string sheetName)
        {
            return _workbook.CreateSheet(sheetName);
        }

        public void SaveExcel(string name)
        {
            using (FileStream sm = new FileStream(name, FileMode.Create, FileAccess.Write))
            {
                _workbook.Write(sm);
            }
        }


        public Sheet GetSheet(int index)
        {
            Sheet sheet = _workbook.GetSheetAt(index);
            //Force excel to recalculate all the formula while open
            sheet.ForceFormulaRecalculation = true;
            return sheet;
        }
        public Sheet GetSheet(string sheetName)
        {
            Sheet sheet = _workbook.GetSheet(sheetName);
            //Force excel to recalculate all the formula while open
            sheet.ForceFormulaRecalculation = true;
            return sheet;
        }

        /// <summary>
        /// 创建新行
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public Row CreateRow(Sheet sheet, int index)
        {
            return sheet.CreateRow(index);
        }


        public void WriteCell(Row row, int columindex, Object value)
        {
            row.CreateCell(columindex).SetCellValue(value.ToString());
        }
        public void WriteCell(Row row, int rowindex, int columindex, Object value)
        {

            row.CreateCell(columindex).SetCellValue(value.ToString());
        }



        public Row GetRow(Sheet sheet, int rowindex)
        {
            return sheet.GetRow(rowindex);
        }
        public Cell GetCell(Row row, int columnindex)
        {
            return row.GetCell(columnindex);
        }
        public Cell GetCell(Sheet sheet, int rowindex, int columnindex)
        {
            return GetCell(GetRow(sheet, rowindex), columnindex);
        }


        #region 设置格式
        /// <summary>
        /// 设置日期格式
        /// </summary>
        /// <param name="cell">ex:</param>
        /// <param name="dataFormat">yyyy年m月d日</param>
        public void SetCellDateTimeStyle(Cell cell, string dataFormat)
        {
            CellStyle style = _workbook.CreateCellStyle();
            DataFormat format = _workbook.CreateDataFormat();
            style.DataFormat = format.GetFormat(dataFormat);
            cell.CellStyle = style;
        }
        /// <summary>
        /// 设置数字格式 比如小数点
        /// </summary>
        /// <param name="cell">ex:0.00 </param>
        /// <param name="dataFormat"></param>
        public void SetDecimalStyle(Cell cell, string dataFormat)
        {
            CellStyle style = _workbook.CreateCellStyle();
            style.DataFormat = HSSFDataFormat.GetBuiltinFormat(dataFormat);
            cell.CellStyle = style;
        }

        /// <summary>
        /// 货币格式
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="dataFormat"></param>
        public void SetMoneyStyle(Cell cell, string dataFormat)
        {
            SetCellDateTimeStyle(cell, dataFormat);
        }

        public void SetFontStyle(Cell cell, string fontName, short size)
        {
            SetFontStyle(cell, fontName, size, false, "");
        }


        private HorizontalAlignment CellHorizationGetLayOut(string name)
        {
            HorizontalAlignment ha = HorizontalAlignment.LEFT;
            switch (name)
            {
                case "left": ha = HorizontalAlignment.LEFT;
                    break;
                case "right": ha = HorizontalAlignment.RIGHT;
                    break;
                case "center": ha = HorizontalAlignment.CENTER;
                    break;
                case "fill": ha = HorizontalAlignment.FILL;
                    break;
                default: ha = HorizontalAlignment.JUSTIFY;
                    break;
            }
            return ha
                ;
        }

        public void SetFontStyle(Cell cell, string fontName, short size, bool isBold, string HorizationLayOut)
        {
            if (string.IsNullOrEmpty(fontName)) fontName = "宋体";
            if (string.IsNullOrEmpty(HorizationLayOut)) HorizationLayOut = "left";
            Font font = _workbook.CreateFont();
            font.FontName = fontName;
            font.FontHeightInPoints = size;
            font.Boldweight = (int)FontBoldWeight.BOLD;
            if (isBold) { font.Boldweight = (int)FontBoldWeight.BOLD; }


            //  font.Color=H
            CellStyle style = _workbook.CreateCellStyle();
            style.Alignment = CellHorizationGetLayOut(HorizationLayOut);
            style.SetFont(font);
            cell.CellStyle = style;
        }

        /// <summary>
        /// 设置自动换行
        /// </summary>
        /// <param name="cell"></param>
        public void SetAutoWrap(Cell cell)
        {
            CellStyle style = _workbook.CreateCellStyle();
            style.WrapText = true;
            cell.CellStyle = style;

        }

        #endregion

        public void SetExcelDocumentSummaryInfo(string company, string subject)
        {

            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = company;
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = subject;


            _workbook.SummaryInformation = si;
            _workbook.DocumentSummaryInformation = dsi;
        }


        /// <summary>
        /// 读取excel 得到行
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Row> ReadExcel(string name)
        {
            using (FileStream files = new FileStream(name, FileMode.Open, FileAccess.Read))
            {
                _workbook = new HSSFWorkbook(files);
            }

            Sheet sheet = _workbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
            Row headrow = sheet.GetRow(0);

            List<Row> list = new List<Row>();
            list.Add(headrow);

            for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
            {
                list.Add(sheet.GetRow(i));
            }

            return list;
        }
        /// <summary>
        /// 读取excel 得到行
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Row> ReadExcel(string name, int sheetIndex)
        {
            using (FileStream files = new FileStream(name, FileMode.Open, FileAccess.Read))
            {
                _workbook = new HSSFWorkbook(files);
            }

            Sheet sheet = _workbook.GetSheetAt(sheetIndex);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
            Row headrow = sheet.GetRow(0);

            List<Row> list = new List<Row>();
            list.Add(headrow);

            for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
            {
                list.Add(sheet.GetRow(i));
            }

            return list;
        }
        /// <summary>
        /// 读取excel 得到行
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Row> ReadExcel(string name, string sheetName)
        {
            using (FileStream files = new FileStream(name, FileMode.Open, FileAccess.Read))
            {
                _workbook = new HSSFWorkbook(files);
            }

            Sheet sheet = _workbook.GetSheet(sheetName);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
            Row headrow = sheet.GetRow(0);

            List<Row> list = new List<Row>();
            list.Add(headrow);

            for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
            {
                list.Add(sheet.GetRow(i));
            }

            return list;
        }


        /// <summary>
        /// 生成下拉列表
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="firstRow"></param>
        /// <param name="lastRow"></param>
        /// <param name="FirstCol"></param>
        /// <param name="LastRow"></param>
        /// <param name="arr"></param>
        public void WriteDropDownList(Sheet sheet, int firstRow, int lastRow, int FirstCol, int LastCol, string[] arr)
        {
            NPOI.SS.Util.CellRangeAddressList ranglist = new NPOI.SS.Util.CellRangeAddressList();
            ranglist.AddCellRangeAddress(new CellRangeAddress(firstRow, lastRow, FirstCol, LastCol));

            DVConstraint dvconstraint = DVConstraint.CreateExplicitListConstraint(arr);
            HSSFDataValidation dataValidation = new HSSFDataValidation(ranglist, dvconstraint);
            ((HSSFSheet)sheet).AddValidationData(dataValidation);
        }
        /// <summary>
        /// 生成下拉列表
        /// 此方法暂时不能使用
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="firstRow"></param>
        /// <param name="lastRow"></param>
        /// <param name="FirstCol"></param>
        /// <param name="LastCol"></param>
        /// <param name="arr"></param>
        public void WriteDropDownList2(Sheet sheet, int firstRow, int lastRow, int FirstCol, int LastCol, string[] data)
        {
            string tmpShtDictionary = "tmpShtDictionary";
            Sheet tmpSheet = _workbook.CreateSheet(tmpShtDictionary);
            for (int i = 0; i < data.Length; i++)
            {
                tmpSheet.CreateRow(i).CreateCell(0).SetCellValue(data[i]);
            }

            Name range = _workbook.CreateName();
            range.RefersToFormula = tmpShtDictionary + "!$A1:$A" + data.Length.ToString();
            range.NameName = "dicRange";

            NPOI.SS.Util.CellRangeAddressList ranglist = new NPOI.SS.Util.CellRangeAddressList();
            ranglist.AddCellRangeAddress(new CellRangeAddress(firstRow, lastRow, FirstCol, LastCol));

            DVConstraint dvconstraint = DVConstraint.CreateFormulaListConstraint(range.NameName);
            HSSFDataValidation dataValidation = new HSSFDataValidation(ranglist, dvconstraint);
            ((HSSFSheet)sheet).AddValidationData(dataValidation);
            _workbook.RemoveSheetAt(_workbook.GetSheetIndex(tmpSheet));
            _workbook.RemoveName(range.NameName);
           
        }


        public string ReadRowData(Row row, int colum)
        {
            return row.GetCell(colum).ToString();
        }
    }
}
