using CRUD_System.Handlers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_System
{
    public partial class ReportForm : Form
    {
        AdminMainControl adminControl;

        public ReportForm(AdminMainControl? adminControl = null)
        {
            this.adminControl = adminControl ?? new AdminMainControl();

            InitializeComponent();
            //ReturnAliasAndDir(); // Get directoryPath and alias
        }

        /*
        private void ReturnAliasAndDir()
        {
            string alias = AuthenticationService.CurrentUser!;
            string reportDirectory = FindCSVFiles.FindReportFile(alias!, "report");

            //LoadFilesIntoListView(reportDirectory, alias);
            LoadFilesIntoListView(reportDirectory);
        }
        */

        /*
        public void LoadFilesIntoListView(string directoryPath, string selectedAlias)
        {
            Debug.WriteLine($"\nReportForm ListView1 BEFORE visible: {listView1.Visible}");

            //listView1.Parent.Visible = true;
            //Debug.WriteLine($"\nReportForm ListView1 AFTER visible: {listView1.Visible}");

            listView1.Items.Clear();
            listView1.Columns.Clear();
            listView1.Columns.Add("File Name", 150);
            listView1.Columns.Add("Size", 100);
            listView1.Columns.Add("Created On", 150);

            Debug.WriteLine("\nListView1: adding 'TestFile.csv'");
            var item = new ListViewItem("TestFile.csv");
            Debug.WriteLine("ListView1: adding '12345 bytes'");
            item.SubItems.Add("12345 bytes");
            Debug.WriteLine("ListView1: adding 'Date'");
            item.SubItems.Add(DateTime.Now.ToString());

            Debug.WriteLine($"ReportForm ListView1 Items added to listView1 BEFORE Add(item): {listView1.Items.Count}");
            listView1.Items.Add(item);
            Debug.WriteLine($"ReportForm ListView1 Items added to listView1 AFTER Add(item): {listView1.Items.Count}");
            listView1.Refresh();
        }
        */


        /// <summary>
        /// Loads files from a specified directory into the ListView.
        /// </summary>
        /// <param name="directoryPath">The directory path to load the files from.</param>
        public void LoadFilesIntoListView(string directoryPath, string selectedAlias)
        {
            listView1.Visible = true;
            if (!string.IsNullOrEmpty(selectedAlias))
            {
                Debug.WriteLine($"ReportForm Selected Alias: {selectedAlias}");
                string reportDirectory = FindCSVFiles.FindReportFile(selectedAlias, "report");
                Debug.WriteLine($"ReportForm Directory: {reportDirectory}");
            }

            // Check if the specified directory exists
            if (Directory.Exists(directoryPath) && !string.IsNullOrEmpty(directoryPath))
            {
                // Clear the ListView before adding new items
                //listView1.Items.Clear();

                // Retrieve all CSV files in the directory
                string[] csvFiles = Directory.GetFiles(directoryPath, "*.csv");

                Debug.WriteLine($"ReportForm CSV Files: {string.Join(", ", csvFiles)}");

                // Check if any CSV files are found
                if (csvFiles.Length > 0)
                {
                    foreach (string file in csvFiles)
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        Debug.WriteLine($"ReportForm Adding File: {fileInfo.Name}");

                        //Debug.WriteLine($"ReportForm File Name: {fileInfo.Name}, Size: {fileInfo.Length}, Creation Time: {fileInfo.CreationTime}");

                        // Create a ListViewItem for each file
                        ListViewItem item = new ListViewItem(fileInfo.Name);
                        item.SubItems.Add(fileInfo.Length.ToString() + " bytes");
                        item.SubItems.Add(fileInfo.CreationTime.ToString());

                        Debug.WriteLine($"Items added to listView1 BEFORE Add(item): {listView1.Items.Count}");
                        // Add the item to the ListView
                        listView1.Items.Add(item);
                        Debug.WriteLine($"Items added to listView1 AFTER Add(item): {listView1.Items.Count}");;
                    }

                    // Force a refresh of the ListView to ensure it's displaying correctly
                    //listView1.Refresh();

                    // Debug output to confirm everything is correct
                    Debug.WriteLine($"\nReportForm View: {listView1.View}");
                    Debug.WriteLine($"ReportForm Columns: {listView1.Columns.Count}");
                    Debug.WriteLine($"ReportForm HeaderStyle: {listView1.HeaderStyle}");
                    Debug.WriteLine($"ReportForm Items in ListView: {listView1.Items.Count}\n");
                }
                else
                {
                    MessageBox.Show("No CSV files found in the specified directory.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //return;
                }
            }
            else
            {
                // Show an error message if the directory does not exist
                //MessageBox.Show("The specified directory does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
        }
    }
}
