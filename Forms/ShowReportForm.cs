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
                // Split the content of the report based on commas (considering that content can be inside quotes)
                string[] reportData = reportContent.Split(new[] { ',' }, 5); // Split into 4 parts maximum

                // Ensure that the data contains enough elements to assign to the controls
                if (reportData.Length == 5)
                {
                    // Assign the split data to the appropriate fields
                    txtDate.Text = FormatDate(reportData[0]); // Date
                    txtAliasCreator.Text = reportData[1];   // Creator Alias
                    txtSelectedAlias.Text = reportData[2];  // Selected Alias
                    txtSubject.Text = reportData[3];        // Subject
                    rtxtDisplayReport.Text = reportData[4].Trim('"'); // Report text
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

        /// <summary>
        /// Parses a date string and returns it in "dd-MM-yyyy" format.
        /// </summary>
        /// <param name="dateString">The date string to parse.</param>
        /// <returns>The formatted date string, or "Invalid Date" if parsing fails.</returns>
        private string FormatDate(string dateString)
        {
            if (DateTime.TryParse(dateString, out DateTime parsedDate))
            {
                return parsedDate.ToString("dd-MM-yyyy"); // Format as "dd-MM-yyyy"
            }
            else
            {
                Debug.WriteLine($"Failed to parse the date: {dateString}");
                return "Invalid Date"; // Fallback for invalid date
            }
        }


        public void btnCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
