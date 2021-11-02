using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using USB_Barcode_Scanner;

namespace Barcode_project
{
    public partial class Form1 : Form
    {
        String productionNumber = "";
        List<String> articleNr = new List<string>();

        String csvLine = "";
        int fieldsInLineCounter = 0;
        public Form1()
        {
            InitializeComponent();
            BarcodeScanner barcodeScanner = new BarcodeScanner(txt_Scanned);
            barcodeScanner.BarcodeScanned += BarcodeScanner_BarcodeScanned;
        }

        private void BarcodeScanner_BarcodeScanned(object sender, BarcodeScannerEventArgs e)
        {
            txt_Scanned.Text = e.Barcode;

            addRecord("articlenr1", "articlenr2", "articlenr3", "test.csv");

            if (e.Barcode.StartsWith("PRO2") || e.Barcode.StartsWith("540014142006"))
            {
                productionNumber = e.Barcode;
            }
            else if (fieldsInLineCounter < 2 && productionNumber != "")
            {
                if (fieldsInLineCounter == 0) csvLine = productionNumber;

                
                fieldsInLineCounter++;
            }
            else if (fieldsInLineCounter == 2) fieldsInLineCounter = 0;
        }

        private void addRecord(string articlenr1, string articlenr2, string articlenr3, string filepath)
        {
            try
            {
                using(System.IO.StreamWriter file = new System.IO.StreamWriter(@filepath, true))
                {
                    file.WriteLine(articlenr1 + "," + articlenr1 + "," + articlenr3);
                    file.Flush();
                    file.Close();
                }
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Something went wrong: ", ex);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
