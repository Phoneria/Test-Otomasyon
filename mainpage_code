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
    public class RanorexHelper
    {
        private static string saveToTxt = @"C:\Users\mirsad\Desktop\Izzet\UIElements.txt";
        private static string imagesFolder = @"C:\Users\mirsad\Desktop\Izzet\ScreenShots";
        private static int counter = 0;

        private static Izzet_Test_V1Repository repo = Izzet_Test_V1Repository.Instance; // ✅ FIXED: Added repository instance

        /// <summary>
        /// Retrieves the base path for UI elements.
        /// </summary>
        /// <returns>Base path as string</returns>
        public static string GetBasePath()
        {
            try
            {
                string basePath = repo.AselsanKardelen.AbsoluteBasePath.ToString();
                Report.Log(ReportLevel.Info, $"Base path retrieved: {basePath}");
                return basePath;
            }
            catch (Exception ex)
            {
                Report.Error($"Failed to get base path: {ex.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        /// Retrieves UI elements based on the base path.
        /// </summary>
        /// <param name="basePath">Base path to search UI elements</param>
        /// <returns>List of UI elements</returns>
        public static IList<Unknown> GetUIElements(string basePath)
        {
            try
            {
                IList<Unknown> elements = Host.Local.Find<Unknown>(basePath + "//*");
                Report.Log(ReportLevel.Info, $"Found {elements.Count} UI elements.");
                return elements;
            }
            catch (Exception ex)
            {
                Report.Error($"Failed to find UI elements: {ex.Message}");
                return null;
            }
        }

       
        /// <summary>
        /// Captures and saves an image of the UI element at the given XPath.
        /// </summary>
        /// <param name="targetXPath">The XPath of the UI element to capture</param>
        /// <param name="savePath">The file path where the screenshot will be saved</param>
        public static void CaptureElementScreenshot(string targetXPath, string savePath)
        {
            try
            {
                // Find the element using XPath
				Unknown targetElement = Host.Local.FindSingle<Unknown>(targetXPath, 5000);
                // Ensure element exists and is visible before capturing
                if (targetElement != null)
                {
                    targetElement.EnsureVisible();

                    // Capture the screenshot of the element using correct format
					Imaging.CaptureImage(targetElement.Element).Save(savePath);

                    Report.Log(ReportLevel.Info, $"Screenshot saved successfully: {savePath}");
                }
                else
                {
                    Report.Error($"Element not found, screenshot not captured: {targetXPath}");
                }
            }
            catch (Exception ex)
            {
                Report.Error($"Failed to capture the element image: {ex.Message}");
            }
        }



        /// <summary>
        /// Saves UI elements' paths to a file.
        /// </summary>
        /// <param name="elements">List of UI elements</param>
        public static void SaveElements(IList<Unknown> elements, bool saveImage = true, bool saveTxt = true )
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(saveToTxt))
                {
                    foreach (var element in elements)
                    {
                        try
                        {
                            string elementPath = element.GetPath().ToString();
                            string imgPath = imagesFolder + "\\" + Convert.ToString(counter) + ".png";
                            
                            
                            if (saveTxt == true){
                        		
                            	// It writes XPaths to saveToTxt
                            	writer.WriteLine(elementPath);
                            
                            }
                            
                            if (saveImage == true){
                            	// It saves images to ImagesFolder
                            	counter += 1;
                            	CaptureElementScreenshot(elementPath, imgPath );
                            }
                            
                            
                        }
                        catch (Exception ex)
                        {
                            Report.Error($"Error writing element to file: {ex.Message}");
                        }
                    }
                }
                Report.Log(ReportLevel.Info, "UI element paths saved to file successfully.");
            }
            catch (Exception ex)
            {
                Report.Error($"Failed to write UI elements to file: {ex.Message}");
            }
        }
    
    		
			
        /*
  		 BU FONKSİYON RANOREXİN GÖRDÜĞÜ TÜM XPATH'LERİ TXT DOSYASINA KAYDEDİYOR 
  		*/
        public static void ProcessAllElements(bool saveImage = true, bool saveTxt = true){
        	
    	    string basePath = RanorexHelper.GetBasePath();
            if (string.IsNullOrEmpty(basePath)) return; // Stop execution if base path is invalid

            IList<Unknown> elements = RanorexHelper.GetUIElements(basePath);
            if (elements == null || elements.Count == 0) return; // Stop execution if no elements found

            RanorexHelper.SaveElements(elements, saveImage, saveTxt);
        }
    
  		
  		
  	
  		
    }


    
    public partial class Recording1
    {
    	// RanorexHelper.WriteAllElementsToTxt(); -> Main()
    	
        /// <summary>
        /// Entry point of the script
        /// </summary>
        public static void Main()
        {
            
           RanorexHelper.ProcessAllElements(true,true);

        }
        private void Init()
        {
            // Your recording specific initialization code goes here.
        }

        
    }

}
