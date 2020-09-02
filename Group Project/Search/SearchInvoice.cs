using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project.Search
{
    /// <summary>
    /// class I created for the search window, these objects will be bound to to the invoice data grid through a binding list.
    /// </summary>
    public class SearchInvoice
    {
        

        /// <summary>
        /// holds the invoiceNumber for this invoice object
        /// </summary>
        public int invoiceNumber { get; set; }

        /// <summary>
        /// Holds the date for this invoice object
        /// </summary>
        public string invoiceDate { get; set; }

        /// <summary>
        /// variable that holds the total of the selected invoice.
        /// </summary>
        public double total { get; set; }

        /// <summary>
        /// constructor for the Invoice class. Creates the invoice with the tot, num, and date parameters
        /// </summary>
        /// <param name="tot"></param>
        /// <param name="num"></param>
        /// <param name="date"></param>
        public SearchInvoice(int num, string date, double tot)
        {
            try
            {
                total = tot;
                invoiceNumber = num;
                invoiceDate = date;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }

        /// <summary>
        /// overriding toString so the objects can be displayed in the invoiceDataGrid
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            try
            {
                return invoiceNumber + " : " + invoiceDate + " : " + total;        //number : date : total
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
