using System;
using System.Collections.Generic;
using System.Drawing;
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
        /// <summary>
        /// This method gets called right after the recording has been started.
        /// It can be used to execute recording specific initialization code.
        /// </summary>
        private void Init()
        {
            // Your recording specific initialization code goes here.
        }

        public void extract_path()
        {
            string basePath = repo.AselsanKardelen.AbsoluteBasePath.ToString();
            IList<Unknown> elements = Host.Local.Find<Unknown>(basePath + "//*");

            List<string> namesList = new List<string>();

            foreach (var element in elements)
            {
                string targetPath = element.GetPath().ToString();

                // Check if element contains @accessiblename=, @caption=, or @title=
                Match match = Regex.Match(targetPath, @"@(?:accessiblename|caption|title)='([^']*)'", RegexOptions.IgnoreCase);

                if (match.Success)
                {
                    string name = match.Groups[1].Value;
                    namesList.Add(name);
                    Report.Log(ReportLevel.Info, $"Extracted Name: {name}");

                    // Capture and save the image of the element
                    CaptureElementImage(element, name, "Captured");
                }
                else{
                    CaptureElementImage(element, targetPath, "Others");
                }
            }

            // Log all names together
            if (namesList.Count > 0)
            {
                Report.Log(ReportLevel.Success, $"Extracted Names: {string.Join(", ", namesList)}");
            }
            else
            {
                Report.Log(ReportLevel.Warn, "No accessible names, captions, or titles found.");
            }
        }

        private void CaptureElementImage(Unknown element, string name, string folder)
        {
            try
            {
	        	// Ensure the element is visible before capturing the image
			    if (!element.Visible)
			    {
			        Report.Warn($"Element '{name}' is not visible, skipping screenshot.");
			        return;
			    }
			    
				string screenshotsPath = $@"C:\\Users\\FARUK\\Desktop\\Izzet\\{folder}";


                if (!System.IO.Directory.Exists(screenshotsPath))
                {
                    System.IO.Directory.CreateDirectory(screenshotsPath);
                }

                string filePath = System.IO.Path.Combine(screenshotsPath, $"{name}.png");

                // Capture screenshot of the UI element
                Imaging.CaptureImage(element.Element).Save(filePath);
                Report.Log(ReportLevel.Info, $"Screenshot saved: {filePath}", filePath);
            }
            catch (Exception ex)
            {
                Report.Warn($"Failed to capture image for {name}: {ex.Message}");
            }
        }
    }
}
