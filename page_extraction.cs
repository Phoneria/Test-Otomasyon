using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using WinForms = System.Windows.Forms;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Repository;
using Ranorex.Core.Testing;

namespace Izzet_Test_V1
{
    public partial class Recording1
    {
        private void Init()
        {
            // Your recording specific initialization code goes here.
        }

        public void ExtractPath()
        {
            string basePath = repo.AselsanKardelen.AbsoluteBasePath.ToString();
            IList<Unknown> elements = Host.Local.Find<Unknown>(basePath + "//*");

            string screenshotFolder = @"C:\Users\FARUK\Desktop\Izzet\Others";

            // Ensure the directory exists
            if (!Directory.Exists(screenshotFolder))
            {
                Directory.CreateDirectory(screenshotFolder);
            }

            foreach (var element in elements)
            {
                try
                {
                    string elementPath = element.GetPath().ToString();
                    
                    // Find the target element
                    Unknown targetElement = Host.Local.FindSingle<Unknown>(elementPath, 5000);
                    
                    if (targetElement != null)
                    {
                        //targetElement.Click(); // Click the element

                        // Sanitize elementPath to create a valid filename
                        string safeFileName = Regex.Replace(elementPath, @"[\/:*?""<>|]", "_");

                        // Generate valid file path
                        string filePath = Path.Combine(screenshotFolder, $"{safeFileName}.png");

                        // Capture and save the screenshot
                        Imaging.CaptureImage(targetElement.Element).Save(filePath);

                        Report.Log(ReportLevel.Info, $"Screenshot saved: {filePath}");
                    }
                    else
                    {
                        Report.Warn($"Target element not found! {elementPath}");
                    }

                    Report.Log(ReportLevel.Info, $"Element Path: {elementPath}");
                }
                catch (Exception ex)
                {
                    Report.Error($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}
