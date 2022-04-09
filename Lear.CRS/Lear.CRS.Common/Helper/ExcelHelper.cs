using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lear.CRS.Common
{
    public static class ExcelHelper
    {

        public static DisplayNameAttribute[] GetDesc(this PropertyInfo field)
        {
            if (field != null)
            {
                return (DisplayNameAttribute[])field.GetCustomAttributes(typeof(DisplayNameAttribute), false);
            }
            return null;
        }

       
        public static List<T> CheckAndConvertData<T>(IFormFile input, string sheetNameMatch)
        {
            int sheetIndex = 0;
            List<T> listResult = new List<T>();
            using (var package = new ExcelPackage(input.OpenReadStream()))
            {
                // 获取到第一个Sheet，也可以通过 Worksheets["name"] 获取指定的工作表
                var sheets = package.Workbook.Worksheets.Where(c => c.Name.Contains(sheetNameMatch));
                if (sheets == null)
                    throw new Exception("数据不存在");

              
                foreach (var sheet in sheets)
                {
                    var list = new List<ExcelModel>();
                    #region 获取开始和结束行列的个数，根据个数可以做各种校验工作

                    // +1 是因为第一行往往我们获取到的都是Excel的标题
                    int startRowNumber = sheet.Dimension.Start.Row + 1;
                    int endRowNumber = sheet.Dimension.End.Row;
                    int startColumn = sheet.Dimension.Start.Column;
                    int endColumn = sheet.Dimension.End.Column;
                    #endregion


                    // 检查模板是否匹配，并映射列位置
                    foreach (var item in typeof(T).GetProperties())
                    {

                        var desc = item.GetDesc();
                        if (desc.Length == 0)
                            continue;
                        var descriptions = desc[0].DisplayName;
                        list.Add(new ExcelModel
                        {
                            colName = descriptions,
                            name = item.Name,
                            index = -1,
                            type = item.PropertyType.Name
                        });


                    }

                    for (int i = startColumn; i <= endColumn; i++)
                    {

                        var colName = sheet.Cells[1, i].Text.Trim();

                        var clModel = list.Where(c => c.colName == colName).FirstOrDefault();
                        if (clModel != null)
                        {
                            clModel.index = i;
                        }

                    }


                    if (list.Any(c => c.index == -1))
                        throw new Exception($"未匹配到列{string.Join(";", list.Where(c => c.index == -1).Select(c => c.colName).ToArray())}");

                    //反射组装数据



                    for (int currentRow = startRowNumber; currentRow <= endRowNumber; currentRow++)
                    {

                        if (sheet.Cells[currentRow, startColumn] == null || string.IsNullOrEmpty(sheet.Cells[currentRow, startColumn].Text))
                            continue;
                        T t = default(T);
                        t = System.Activator.CreateInstance<T>();

                        foreach (var item in list)
                        {
                            string sheetValue = sheet.Cells[currentRow, item.index].Text;
                            if (item.type == typeof(Decimal).Name)
                            {
                                sheetValue = sheetValue.ToDecimal().ToString();
                            }
                            SetPropertyValue(t, item.name, sheetValue);

                        }

                        listResult.Add(t);

                    }
                }



                return listResult;

            }




        }

        private static void SetPropertyValue(object obj, string propertyName, string sValue)
        {
            PropertyInfo p = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (p != null)
            {
                object dynmicValue;

                if (p.PropertyType.IsArray)//数组类型,单独处理
                {
                    p.SetValue(obj, sValue, null);
                }
                else
                {
                    //根据属性类型动态转换属性的值
                    if (String.IsNullOrEmpty(sValue.ToString()))//空值
                        dynmicValue = p.PropertyType.IsValueType ? Activator.CreateInstance(p.PropertyType) : null;//值类型
                    else
                        dynmicValue = System.ComponentModel.TypeDescriptor.GetConverter(p.PropertyType).ConvertFromString(sValue.ToString());//创建对象

                    //调用属性的SetValue方法赋值
                    p.SetValue(obj, dynmicValue, null);
                }
            }
        }

        public static void ReportData(ExcelPackage excel, string _sheetName, List<string> columns,List<Dictionary<int,string>> dataList)
        {

            //向新建的Excel中添加一个sheet
            var sheet = excel.Workbook.Worksheets.Add(_sheetName);

            //注：Excel中行的索引从1开始，DataTable的索引从0开始
            int rowIndex = 1;   //起始行为第二行
            int columnIndex = 0;//起始列为第一列



           
            foreach (var item in columns)
            {
                columnIndex++;
                ExcelRange cell = sheet.Cells[rowIndex, columnIndex];
                cell.Value = item;
                //cell.Style.Font.Bold = true;                //字体为粗体
               // cell.Style.Font.Color.SetColor(Color.Red);  //字体颜色
                cell.Style.Font.Name = "微软雅黑";         //字体样式
                cell.Style.Font.Size = 12;                 //字体大小
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平居中
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;   //垂直居中
            }


            //绑定数据
            for (int i = 0; i < dataList.Count; i++)
            {
                for (int j = 0; j < columns.Count; j++)
                {
                    //从第二行开始绑定数据

                    //修改性别显示方式
                    sheet.Cells[i + 2, j + 1].Value = (dataList[i])[j];

                }
            }
        }
    }

    public class ExcelModel
    {
        public string colName { get; set; }

        public string type { get; set; }
        public int index { get; set; }
        public string name { get; set; }
    }



}
