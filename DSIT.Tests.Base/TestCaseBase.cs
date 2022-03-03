using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.IO;
using System.Collections.Generic;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace DSIT.Tests.Base;

[TestClass]
public abstract class TestCaseBase
{    
        protected static IWebDriver driver;
        private StringBuilder verificationErrors;
        private static string baseURL;
        private static int timeout;
        private bool acceptNextAlert = true;

        protected abstract IWebDriver InitDriver();


        public TestCaseBase()
        {
            if (driver == null)
                driver = this.InitDriver();
            if (baseURL == null)
                baseURL = "about:blank";
            timeout = 30;
        }

        /// <summary> Termina la prueba, termina el proceso y cierra la ventana.
        /// Este método debe existir en todas las clases, con la misma etiqueta. </summary>
        [ClassCleanup]
        public static void CleanupClass()
        {
            try
            {
                driver.Quit();// quit does not close the window
                driver.Close();
                driver.Dispose();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

         [TestInitialize]
        public void InitializeTest()
        {
            verificationErrors = new StringBuilder();
        }

         [TestCleanup]
        public void CleanupTest()
        {
            Assert.AreEqual("", verificationErrors.ToString());
        }

        ///<summary> Navega a la url recibida por parámetro.</summary>
        ///<param name = "url"> URL a la cual se desea navegar. </param>
        public void NavigateToUrl(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        ///<summary>Pasa el mouse sobre en enlace con el texto recibido por parámetro. </summary>
        ///<param name = "partialText"> Texto del enlace sobre el cual se quiere pasar el mouse. </param>
        ///<param name = "wait"> True si se debe esperar a que cargue para ejecutar, false de lo contrario.</param>
        public void MouseOverPartialByLinkText(string partialText, bool wait)
        {
            By by = By.PartialLinkText ( partialText);
            if(wait)
            {
                WaitUntilLoaded(by);
            } 
            var mouseOverAction = new OpenQA.Selenium.Interactions.Actions(driver);
            var link= driver.FindElement (by);
            mouseOverAction.MoveToElement(link);
            mouseOverAction.Perform();
        }

        ///<summary>Retorna el elemento que tiene el texto recibido por parámetro. </summary>
        ///<param name = "PartialText">Texto del enlace del elemento buscado. </param>        
        ///<param name = "wait"> True si se debe esperar a que cargue para ejecutar, false de lo contrario.</param>
        ///<returns>Elemento encontrado</returns>
        public IWebElement GetLinkByText(string partialText, bool wait)
        {
            By by = By.PartialLinkText ( partialText);
            if(wait)
            {
                WaitUntilLoaded(by);
            } 
            return driver.FindElement (by);
        }

        ///<summary>Hace clic en el elemento cuyo texto del enlace es el recibido por parámetro.</summary>
        ///<param name = "text">Texto del enlace sobre el cual se quiere hacer clic.</param>
        ///<param name = "wait"> True si se debe esperar a que cargue para ejecutar, false de lo contrario.</param>
        public void ClickElementByLinkText(string text, bool wait)
        {
            By by = By.LinkText(text);
            if(wait)
            {
                WaitUntilLoaded(by);
            }  
            driver.FindElement(by).Click();
        }

        ///<summary>Detiene la ejecución durante la cantidad de segundos ingresado.</summary>
        ///<param name = "seconds">Cantidad de segundos por los cual se quiere detener la ejecución.</param>
        public void WaitForSeconds(int seconds)
        {
            Thread.Sleep(seconds*1000);
        }

        ///<summary>Retorna el elemento de la imagen con el título recibido por parámetro.</summary>
        ///<param name = "title">Título de la imagen buscada.</param>
        ///<param name = "wait"> True si se debe esperar a que cargue para ejecutar, false de lo contrario.</param>
        ///<returns>Elemento buscado.</returns>
        public IWebElement GetImageByTitle(string title, bool wait)
        {
            By by = By.CssSelector ("img[title='" + title + "']");
            if(wait)
            {
                WaitUntilLoaded(by);
            }  
            return driver.FindElement(by);
        }

        ///<summary>Retorna el elemento con las clases css recibida por parámetro.</summary>
        ///<param name = "classes">Clases del elemento buscado.</param>
        ///<param name = "wait"> True si se debe esperar a que cargue para ejecutar, false de lo contrario.</param>
        ///<returns>Elemento buscado.</returns>
        public IWebElement GetElementByCssClass(string classes, bool wait )
        {
            By by = By.CssSelector (classes);
            if(wait)
            {
                WaitUntilLoaded(by);
            }  
            return driver.FindElement(by);
        }

        ///<summary>Ingresa el valor recibido al campo de texto que tiene el id dado.</summary>
        ///<param name = "id">Id del campo de texto donde se quiere ingresar información.</param>
        ///<param name = "value">Texto que se quiere ingresar en el campo de texto.</param>
        ///<param name = "wait"> True si se debe esperar a que cargue para ejecutar, false de lo contrario.</param>
        ///<returns>Campo de texto donde se ingresó la información.</returns>
        public IWebElement EnterInformation (string id, string value, bool wait)
        {
            By by = By.Id(id);
            if(wait)
            {
                WaitUntilLoaded(by);
            }    
            IWebElement textbox = driver.FindElement(by);
            textbox.Clear();
            textbox.SendKeys(value);
            return textbox;
        }

        ///<summary>Hace clic en el botón con id dado y envía los datos del formulario al que pertenece.</summary>
        ///<param name = "id"> Id del botón sobre el cual se quiere hacer clic.</param>
        ///<param name = "wait"> True si se debe esperar a que cargue para ejecutar, false de lo contrario.</param>
        public void SubmitFormByID (string id, bool wait)
        {
            By by = By.Id(id);
            if(wait)
            {
                WaitUntilLoaded(by);
            } 
            driver.FindElement(by).Click();
        }

        ///<summary>Maximiza la ventana.</summary>
        public void Maximize()
        {
            driver.Manage().Window.Maximize();
        }

        ///<summary>Retorna el elemento con el enlace recibido por parámetro.</summary>
        ///<param name = "path">Enlace del elemento buscado.</param>
        ///<param name = "wait"> True si se debe esperar a que cargue para ejecutar, false de lo contrario.</param>
        ///<returns></returns>
        public IWebElement GetElementByXPath(string path, bool wait)
        {
            By by = By.XPath(path);
            if(wait)
            {
                WaitUntilLoaded(by);
            }    
             return driver.FindElement(by);
        }

        ///<summary>Lee el archivo con la ruta dada y retorna una lista con la información encontrada en el archivo.</summary>    
        ///<param name = "path">Ruta donde se encuentra el archivo.</param>
        ///<returns>Lista con la información leída en el archivo. En cada posición se encontrará un arreglo con la información de cada línea.</returns>
        public List<string[]> ReadCSVFile(string path)
        {
            List<string[]> infoFiles = new List<string[]>();
            using(var reader = new StreamReader(@path))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(';');
                    
                    infoFiles.Add(values);
                }
            }
            return infoFiles;
        }

    ///<summary>Espera que se cargue el elemento</summary>
    ///<param name = "by">Mecanismo que se usará para encontrar el elemento.</param>
    public void WaitUntilLoaded(By by)
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
        bool displayed =wait.Until(condition =>
        {
            try
            {
                return driver.FindElement(by).Displayed;           
            }
            catch (StaleElementReferenceException)
            {
                return false;
            } 
            catch (NoSuchElementException)
    {
                return false;
            }              
        });        
    }

    ///<summary>Retorna la url actual.</summary>
    ///<returns>URL donde se encuentra el navegador actulalmente.</returns>
    public string GetCurrentURL()
    {
        return driver.Url;
    }

}
