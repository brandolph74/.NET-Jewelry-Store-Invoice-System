using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Reflection;

namespace Group_Project.Search
{
    /// <summary>
    /// class that holds all of the SQL code in strings, but not the database connection code (located in clsDataAccess)
    /// </summary>
    class clsSearchSQL
    {
        /// <summary>
        /// Connection string to the database.
        /// </summary>
        private string sConnectionString;

        /// <summary>
        /// string that holds the SQL to get the invoices
        /// </summary>
        public static string sGetInvoicesSQL = "SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices";

        /// <summary>
        /// string that holds the SQL to get Invoices with a specific Invoice Number
        /// </summary>
        public static string sGetInvoicesWithID_SQL = "SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceNum = ";

        /// <summary>
        /// create the string to get the Invoices that are specific to a date
        /// </summary>
        public static string createGetInvoicesString_Date(string date)
        {
            try
            {
                return "SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceDate = " + "#" + date + "#";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// create the string to get the Invoices that are specific to a date
        /// </summary>
        public static string createGetInvoicesString_Charge(double charge)
        {
            try
            {
                return "SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE TotalCost = " + charge;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// create the string to get the Invoices with date and charge parameters
        /// </summary>
        /// <param name="charge"></param>
        /// <returns></returns>
        public static string createGetInvoicesString_DateAndCharge(string date, double charge)
        {
            try
            {
                return "SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE TotalCost = " + charge + " AND InvoiceDate = " + "#" + date + "#";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
