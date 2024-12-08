using CRUD_System.FileHandlers;
using CRUD_System.Handlers;
using CRUD_System.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_System
{
    internal class ReportManager
    {
        private readonly AdminMainControl adminControl = new AdminMainControl();
        private readonly ListView listView = new ListView();
        private readonly RepositoryMessageBoxes message = new RepositoryMessageBoxes();
        private readonly ReportManager reportManager = new ReportManager();


        public void ButtonSaveReport_ClickHandler()
        {
            var currentUser = AuthenticationService.CurrentUser;
            string selectedAlias = adminControl.txtAlias.Text;
            string newReportText = $"\"{adminControl.rtxNewReport.Text}\"";
            string subject = adminControl.comboBoxSubjectReport.Text;
            string dateFile = DateTime.Now.ToString("ddMMyyyy-HHmmss");

            if (!string.IsNullOrEmpty(newReportText) && !string.IsNullOrEmpty(selectedAlias) && adminControl.comboBoxSubjectReport.Text != "Subject:")
            {

                DialogResult dr = message.MessageConfirmSaveNote(selectedAlias);
                if (dr == DialogResult.No)
                {
                    return;
                }

                // Create report file: {alias}_report.csv, format {Date},{aliasCreator},{aliasUser},{subject},{Report}
                // DateTime.Now.ToString()
                CreateCSVFiles.CreateReportsCSV(dateFile, currentUser!, selectedAlias, subject, newReportText);

                // Refresh ListViewFiles to show the new file
                RefreshListViewFiles();
            }
            else
            {
                Debug.WriteLine("Not Valid!");
                MessageBox.Show("Not Valid");
                return;
            }

            // Clean up rtxNewReport
            adminControl.rtxNewReport.Text = string.Empty;
        }

        public void RefreshListViewFiles()
        {
            // Clear existing items
            listView.Items.Clear();

            // Reload files from the directory
            string reportDirectory = FindCSVFiles.FindReportFile(adminControl.txtAlias.Text, "report");

            if (Directory.Exists(reportDirectory))
            {
                string[] reportFiles = Directory.GetFiles(reportDirectory, "*.csv");

                // Create an array of FileInfo objects for sorting
                FileInfo[] fileInfos = reportFiles.Select(file => new FileInfo(file)).ToArray();

                // Sort the files by CreationTime in descending order (newest first)
                Array.Sort(fileInfos, (f1, f2) => f2.CreationTime.CompareTo(f1.CreationTime));

                // Use the sorted fileInfos array to populate the ListView
                foreach (var fileInfo in fileInfos)
                {
                    // Create a ListViewItem for each file, using Name and Date
                    ListViewItem item = new ListViewItem(fileInfo.Name);
                    item.SubItems.Add(fileInfo.CreationTime.ToString("dd/MM/yyyy"));

                    // Add the full file path to the Tag property
                    item.Tag = fileInfo.FullName;

                    // Add the item to the ListView
                    listView.Items.Add(item);
                }

                // Ensure the ListView refreshes visually
                listView.Refresh();
            }
            else
            {
                Debug.WriteLine($"Directory not found: {reportDirectory}");
            }
        }
    }
}
