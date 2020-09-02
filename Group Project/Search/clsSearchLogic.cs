using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project.Search
{
    /// <summary>
    /// class that contains all of the code for the search window.
    /// </summary>
    class clsSearchLogic

    {
        /// <summary>
        /// Static int made available to all classes to signal which invoice is selected, used with the "Select Invoice" Button. Easy to communicate with the main window
        /// </summary>
        public static int currInvoiceID = -1;

        /// <summary>
        /// int that is used for the Invoice Number drop down box in the search menu. Used to limit the scope of invoices in the data grid
        /// </summary>
        public int invoiceNumber;

        /// <summary>
        /// dateTime variable used for the Invoice Number drop down box in the search menu. Used to limit the scope of invoices in the data grid
        /// </summary>
        public DateTime invoiceDate;

        /// <summary>
        /// variable used for the invoice number drop down box in the search menu. Used to limit the scope of invoices in the data grid.
        public double chargeAmount;


        /// <summary>
        /// establish the database connection here
        /// </summary>
        clsDataAccess db = new clsDataAccess();

        

        /// <summary>
        /// default getInvoices method that populates the invoice list before any parameters in the combo boxes are set.
        /// </summary>
        /// <returns></returns>
        public DataSet getInvoices()
        {
            try
            {
                DataSet ds;
                int rows = 0;
               
                ds = db.ExecuteSQLStatement(clsSearchSQL.sGetInvoicesSQL, ref rows);

                

                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }
        /// <summary>
        /// Class to get the invoices from the database. Parameters are the ID only. Used for the InvoiceID combo box
        /// </summary>
        /// <param name="num"></param>
        /// <param name="date"></param>
        /// <param name="charge"></param>
        public DataSet getInvoices(int num)
        {
            try
            {
                DataSet ds;
                int rows = 0;
                
                ds = db.ExecuteSQLStatement(clsSearchSQL.sGetInvoicesWithID_SQL + num, ref rows);

                //Go through the dataset and put each one in the binding list.

                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }

        /// <summary>
        /// method to get the invoices from the database. parameters are the date only
        /// </summary>
        /// <param name="date"></param>
        /// <param name="charge"></param>
        /// <returns></returns>
        public DataSet getInvoices(string date)
        {
            try
            {
                DataSet ds;
                //do the same thing as the other getInvoice methods but this one just has the date as the only parameter.
                int rows = 0;
                
                ds = db.ExecuteSQLStatement(clsSearchSQL.createGetInvoicesString_Date(date), ref rows);
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }

        }


        /// <summary>
        ///Used to get the invoices from the database, charge is the only parameter for this method
        /// </summary>
        /// <param name="charge"></param>
        /// <returns></returns>
        public DataSet getInvoices(double charge)
        {
            try
            {
                DataSet ds;
                //do the same thing as the other getInvoice methods but this one just has the date as the only parameter.
                int rows = 0;
                
                ds = db.ExecuteSQLStatement(clsSearchSQL.createGetInvoicesString_Charge(charge), ref rows);
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }

        }
        /// <summary>
        /// method called when date and charge are both selected in the combo boxes.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="charge"></param>
        /// <returns></returns>
        public DataSet getInvoices(string date, double charge)
        {
            try
            {
                DataSet ds;
                //do the same thing as the other getInvoice methods but this one just has the date as the only parameter.
                int rows = 0;
                
                ds = db.ExecuteSQLStatement(clsSearchSQL.createGetInvoicesString_DateAndCharge(date, charge), ref rows);
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }

        }


    }
}
