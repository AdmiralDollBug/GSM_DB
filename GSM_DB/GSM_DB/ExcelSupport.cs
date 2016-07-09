using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace GSM_DB
{
    class ExcelSupport
    {
        public static void DisplayInExcel() {
            var excelApp = new Microsoft.Office.Interop.Excel.Application();
            excelApp.Visible = true;
        }
    }
}
