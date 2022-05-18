using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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

namespace CopyData
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AddHandler(Keyboard.KeyDownEvent, (KeyEventHandler)MainWindow_KeyDown);
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var list = new ObservableCollection<DataGridModel>();
            var obj = new DataGridModel();
            obj.Id = "1";
            obj.Title = "Grid数据网格";
            obj.Description = "1、实现对Grid类似于PLsql的数据复制功能，可以从excel复制过来，也可以复制到excel当中去。";
            list.Add(obj);
            obj = new DataGridModel();
            obj.Id = "2";
            obj.Title = "下拉列表框";
            obj.Description = "多选功能，筛选功能，分组功能";
            list.Add(obj);

            comBox.ItemsSource= list;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.V && (Keyboard.Modifiers & (ModifierKeys.Control)) == (ModifierKeys.Control))

            {
                DataGirdViewCellPaste();
            }
        }

        private void DataGirdViewCellPaste()
        {
            try
            {
                // 获取剪切板的内容，并按行分割     
                string pasteText = Clipboard.GetText();
                if (string.IsNullOrEmpty(pasteText))
                    return;
                var rows = pasteText.Split("\r\n");
                var dataGridModelList = new List<DataGridModel>();
                foreach (var row in rows)
                {
                    var dataGridModel = new DataGridModel();
                    if (!string.IsNullOrEmpty(row))
                    {
                        var columns = row.Split("\t");
                        if (columns.Length > 0)
                        {
                            dataGridModel.Id = columns[0];
                            dataGridModel.Title = columns[1];
                            dataGridModel.Description = columns[2];
                        }
                        dataGridModelList.Add(dataGridModel);
                    }
                }
                if (dataGrid.ItemsSource != null)
                {
                    dataGridModelList.AddRange(((List<DataGridModel>)dataGrid.ItemsSource));
                    dataGrid.ItemsSource = null;
                }
                else
                {
                    dataGrid.ItemsSource = dataGridModelList;
                }
                dataGrid.ItemsSource = dataGridModelList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void export_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filename = openFileDialog.FileName;
                var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                var excel = new ExcelPackage(fs);
                var sheet = excel.Workbook.Worksheets[0];
                var row = sheet.Dimension.End.Row;
                var col = sheet.Dimension.End.Column;
                var dataGridModelList = new List<DataGridModel>();

                for (var i = 1; i <= row; i++)
                {
                    var dataGridModel = new DataGridModel();
                    dataGridModel.Id = sheet.Cells[i, 1].Value.ToString();
                    dataGridModel.Title = sheet.GetValue(i, 2).ToString();
                    dataGridModel.Description = sheet.GetValue(i, 3).ToString();
                    dataGridModelList.Add(dataGridModel);
                }
                if (dataGrid.ItemsSource != null)
                {
                    dataGridModelList.AddRange((IEnumerable<DataGridModel>)dataGrid.ItemsSource);
                    dataGrid.ItemsSource = null;
                }
                else
                {
                    dataGrid.ItemsSource = dataGridModelList;
                }
            }

        }

        private void import_Click(object sender, RoutedEventArgs e)
        {
            var file = Directory.GetCurrentDirectory() + @"\text.xlsx";
            if (dataGrid.ItemsSource == null) return;
            FileInfo newFile = new FileInfo(file);
            if (newFile.Exists)
            {
                newFile.Delete();
                newFile = new FileInfo(file);
            }
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                var worksheet = package.Workbook.Worksheets.Add("test");

                worksheet.Cells[1, 1].Value = "Id";
                worksheet.Cells[1, 2].Value = "Title";
                worksheet.Cells[1, 3].Value = "Description";
                int row = 1;
                foreach (var line in dataGrid.ItemsSource)
                {
                    row++;
                    var dataGridModel = line as DataGridModel;
                    worksheet.Cells[row, 1].Value = dataGridModel?.Id;
                    worksheet.Cells[row, 2].Value = dataGridModel?.Title;
                    worksheet.Cells[row, 3].Value = dataGridModel?.Description;
                }
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.DefaultExt = ".xlsx";
                saveFileDialog.Filter = "Office 2007 File|*.xlsx|Office 2000-2003 File|*.xls|所有文件|*.*";
                if (saveFileDialog.ShowDialog() == true)
                {
                    if (saveFileDialog.FileName != "")
                    {
                        package.SaveAs(saveFileDialog.FileName);
                    }
                }

            }
        }

        private void setBG_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                var obj = (DataGridModel)dataGrid.SelectedItem;
                obj.State = 1;
                var dataGridModels = (IEnumerable<DataGridModel>)dataGrid.ItemsSource;
                //if (dataGridModels.Any())
                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = dataGridModels.OrderBy(row => row.State).ToList();
            }

        }

        private void comBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var cbObject in comBox.Items)
            {
                if (((DataGridModel)cbObject).IsChecked)
                    sb.AppendFormat("{0}, ", ((DataGridModel)cbObject).Title);
            }
            comBox.Text = sb.ToString().Trim().TrimEnd(',');
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var cbObject in comBox.Items)
            {
                if (((DataGridModel)cbObject).IsChecked)
                    sb.AppendFormat("{0}, ", ((DataGridModel)cbObject).Title);
            }
            comBox.Text = sb.ToString().Trim().TrimEnd(',');
        }

        private void comBox_KeyUp(object sender, KeyEventArgs e)
        {
            CollectionView itemsViewOriginal = (CollectionView)CollectionViewSource.GetDefaultView(comBox.ItemsSource);
            itemsViewOriginal.Filter = ((o) =>
            {
                if (String.IsNullOrEmpty(comBox.Text)) return true;
                else
                {
                    if (((DataGridModel)o).Title.Contains(comBox.Text)) return true;
                    else return false;
                }
            });
            itemsViewOriginal.Refresh();
        }
    }
    public class DataGridModel
    {
        public string? Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public int State { get; set; }

        public bool IsChecked { get; set; }

        public string SelectedText { get; set; }
    }
}
