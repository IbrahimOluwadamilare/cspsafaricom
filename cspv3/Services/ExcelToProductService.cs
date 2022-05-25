using cspv3.Data;
using cspv3.Models;
using cspv3.Models.BundleModels;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace cspv3.Services
{
    public class ExcelToProductService : IExcelToProductService
    {
        private readonly ApplicationDbContext _context;
        public ExcelToProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task ConvertFileToProductString(string filePath)
        {
            var read = ReadFromExcel<List<BundleRes>>(filePath);
            var read2 = ReadFromExcel<List<BundleRes>>(filePath, false);
            
        }

        private  T ReadFromExcel<T>(string path, bool hasHeader = true)
        {
            var excelPack = new ExcelPackage(new FileInfo(path));

          
                //Lets Deal with first worksheet.(You may iterate here if dealing with multiple sheets)
                var ws = excelPack.Workbook.Worksheets["Sheet1"];


            var productFromExcel = new List<BundleCategory>();


            //Get all details as DataTable -because Datatable make life easy :)
            DataTable excelasTable = new DataTable();
                foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                {
                    //Get colummn details
                    if (!string.IsNullOrEmpty(firstRowCell.Text))
                    {
                        string firstColumn = string.Format("Column {0}", firstRowCell.Start.Column);
                        excelasTable.Columns.Add(hasHeader ? firstRowCell.Text : firstColumn);
                    productFromExcel.Add(new BundleCategory
                    {
                        CategoryName = firstColumn,

                    });
                    }
                _context.AddRange(productFromExcel);
                _context.SaveChanges();
                }

            var startRow = hasHeader ? 2 : 1;
                //Get row details
                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, excelasTable.Columns.Count];
                    DataRow row = excelasTable.Rows.Add();
                    foreach (var cell in wsRow)
                    {
                        row[cell.Start.Column - 1] = cell.Text;
                    }
                }


              //  var generatedType = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(excelasTable));

                var generatedType = JsonConvert.DeserializeObject<List<BundleRes>>(JsonConvert.SerializeObject(excelasTable));

      
           

                return (T)Convert.ChangeType(generatedType, typeof(T));
            }
        
    }
}
