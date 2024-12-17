using CRUD_System.FileHandlers;
using System;
using System.IO;
using System.Windows.Forms;
namespace CRUD_System
{
    public class UploadFile
    {
        /// <summary>
        /// Opens a file dialog to allow the user to select a file.
        /// Copies the selected file to a specified destination path.
        /// If the file already exists at the destination, it is overwritten.
        /// </summary>
        public static void Upload(string uploadPath)
        {
            // Open a file dialog to let the user select a file
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Check if the user selected a file
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the path of the selected file
                string sourceFilePath = openFileDialog.FileName;  // The file chosen by the user
                //string destinationFilePath = @"C:\Users\Public\Pictures\image_copy.jpg";  // The destination path for the file
                string destinationFilePath = $"{uploadPath}";  // The destination path for the file

                // Verify if the source file exists
                if (File.Exists(sourceFilePath))
                {
                    // Copy the file to the destination path, overwriting any existing file
                    File.Copy(sourceFilePath, destinationFilePath, true);  // Overwrite if the file already exists
                    Console.WriteLine($"File successfully copied to {destinationFilePath}");
                }
                else
                {
                    // Inform the user if the source file does not exist
                    Console.WriteLine("The specified file does not exist.");
                }
            }
        }
    }
}
