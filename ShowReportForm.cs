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
    public partial class ShowReportForm : Form
    {
        public ShowReportForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads the report content into the form controls from the provided report content string.
        /// </summary>
        /// <param name="reportContent">The content of the report as a comma-separated string.</param>
        public void LoadReport(string reportContent)
        {
            if (!string.IsNullOrEmpty(reportContent))
            {

                Debug.WriteLine($"ShowReportForm reportContent: {reportContent}");

                // Split the content of the report based on commas
                string[] reportData = reportContent.Split(',');

                // Ensure that the data contains enough elements to assign to the controls
                if (reportData.Length >= 4)
                {
                    // Assign the split data to the appropriate fields
                    txtDate.Text = reportData[0];            // Date
                    txtCreatedBy.Text = reportData[1];        // Creator
                    txtSubject.Text = reportData[2];         // Subject
                    rtxtDisplayReport.Text = reportData[3];  // Report text

                    //this.Refresh();
                }
                else
                {
                    // Show an error message if the data does not match the expected format
                    Debug.WriteLine("The report does not match the expected format.");
                    MessageBox.Show("The report does not match the expected format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Show an error message if the report content is empty
                Debug.WriteLine("The report content is empty.");
                MessageBox.Show("The report content is empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void btnCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
