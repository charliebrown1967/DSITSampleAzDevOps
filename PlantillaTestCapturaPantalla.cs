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
        screenshot.SaveAsFile("Evidencia1.png");
        TestContext.AddResultFile("Evidencia1.png");
        
        base.WaitForSeconds(5);
        Assert.IsNotNull(screenshot) ;
       
    }

    [ClassCleanup]
    public static void CleanupClass()
    {
        TestCaseBase.CleanupClass();
    }
}