using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Rail
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string ReportName = "Report1";

                LocalReport report = new LocalReport();

                //report.ReportEmbeddedResource = "Rail." + ReportName + ".rdlc";
                report.ReportPath = ReportName + ".rdlc";

                ReportParameter rp1 = new ReportParameter("Dated", Convert.ToDateTime(txtDated.Text.ToString()).ToString("dd/MM/yyy"));
                ReportParameter rp2 = new ReportParameter("PayTo", txtPayto.Text.ToString());
                ReportParameter rp3 = new ReportParameter("theSumOfRupees", txttheSumOfRupees.Text.ToString());
                ReportParameter rp4 = new ReportParameter("Rs", txtRs.Text.ToString());

                report.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });

                report.PrintToPrinter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnPdf_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Warning[] warnings;
                string extension;
                string[] streamids;
                string mimeType;
                string encoding;

                ReportViewer _reportViewer = LoadReport();

                if (_reportViewer != null)
                {

                    byte[] bytes = _reportViewer.LocalReport.Render(
                       "PDF", null, out mimeType, out encoding,
                        out extension,
                       out streamids, out warnings);

                    using (FileStream fs = new FileStream("output.pdf", FileMode.Create))
                    {
                        fs.Write(bytes, 0, bytes.Length);
                    }

                    MessageBox.Show("File Created.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private ReportViewer LoadReport()
        {
            try
            {
                ReportViewer _reportViewer = new ReportViewer();
                _reportViewer.Reset();

                string ReportName = "Report1";

                _reportViewer.LocalReport.ReportEmbeddedResource = "Rail." + ReportName + ".rdlc";
                //_reportViewer.LocalReport.ReportPath = ReportName + ".rdlc";

                ReportParameter rp1 = new ReportParameter("Dated", Convert.ToDateTime(txtDated.Text.ToString()).ToString("dd/MM/yyy"));
                ReportParameter rp2 = new ReportParameter("PayTo", txtPayto.Text.ToString());
                ReportParameter rp3 = new ReportParameter("theSumOfRupees", txttheSumOfRupees.Text.ToString());
                ReportParameter rp4 = new ReportParameter("Rs", txtRs.Text.ToString());

                _reportViewer.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });

                _reportViewer.LocalReport.Refresh();

                return _reportViewer;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
    }
}
