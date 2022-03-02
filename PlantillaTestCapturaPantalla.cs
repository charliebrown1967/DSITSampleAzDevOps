using System.Reflection;
using System.Diagnostics;
using System.Timers;
using System.Buffers.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSIT.Tests.Base;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Threading;
using System.Drawing;
using System.IO;

namespace DSIT.Tests.PlantillaTests;

[TestClass]
public class PlantillaTestCapturaPantalla : TestCaseBase
{
    public TestContext TestContext { get; set; }     
    protected override IWebDriver InitDriver()
    {
        return new ChromeDriver();
    }
   
    [TestMethod]
    public void TestMethodCaptureScreen()
    {
        base.NavigateToUrl("https://plantillatest.uniandes.edu.co");
        base.Maximize();

        
        var screenshot = (TestCaseBase.driver as ITakesScreenshot).GetScreenshot();
        screenshot.SaveAsFile("C:\\Testing\\DSIT\\Pruebas automaticas\\DSIT.Tets.SampleAzDevOpsPipeline\\Evidencia1.png");
        
        base.WaitForSeconds(5);
      
         var e = base.GetElementByXPath("//*[text()='La Facultad']",true);
         // JavaScript Executor to scroll to element
         ((IJavaScriptExecutor)e)
         .ExecuteScript("arguments[0].scrollIntoView(true);", e); 

          
       
    }

    [ClassCleanup]
    public static void CleanupClass()
    {
        TestCaseBase.CleanupClass();
    }
}