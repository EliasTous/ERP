using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace AionHR.Web.UI.Forms.Utilities
{
    /// <summary>
    /// Class for managing the language of the interface, built on the singeleton pattern
    /// </summary>
    public class LocalisationManager
    {
        private static LocalisationManager instance;
        public static LocalisationManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new LocalisationManager();
                return instance;
            }
        }

        /// <summary>
        /// Set Arabic UI Culture
        /// </summary>
        public void SetArabicLocalisation()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("ar");
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
        }

        /// <summary>
        /// Set English UI Culture
        /// </summary>
        public void SetEnglishLocalisation()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
        }
    }
}