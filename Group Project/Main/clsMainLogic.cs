using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Group_Project.Main
{
    class clsMainLogic
    {
        #region Class Variables

        /// <summary>
        /// Class variable of SQL database
        /// </summary>
        clsMainSQL db;

        /// <summary>
        /// Class variable of current dataset
        /// </summary>
        DataSet dsCurrentDataSet;

        /// <summary>
        /// Class variable of datatable object
        /// </summary>
        DataTable dtCurrentInvoiceTable;

        /// <summary>
        /// Class variable that contains total of current invoice
        /// </summary>
        private int iTotalCost;

        /// <summary>
        /// Class variable to determine if working DataTable is for new invoice - true if new, false if searched invoice
        /// </summary>
        private bool bIsNewInvoice;

        /// <summary>
        /// Class variable to determine if working DataTable is empty - true if empty, false otherwise
        /// </summary>
        public bool bIsInvoiceEmpty;

        #endregion

        #region Methods

        /// <summary>
        /// This is a constructor
        /// </summary>
        public clsMainLogic()
        {
            db = new clsMainSQL();
            dtCurrentInvoiceTable = new DataTable();
            iTotalCost = 0;
            bIsInvoiceEmpty = true;
        }

        /// <summary>
        /// Method to return an invoice date when passed in an invoice number
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string GetInvoiceDate(string s)
        {

           
            try
            {
                string sResult = "";
                sResult = db.ExecuteScalarSQL("SELECT InvoiceDate " +
                                            "FROM  Invoices " +
                                            "WHERE InvoiceNum=" + s);
                return sResult;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                return "";
            }
        }

        /// <summary>
        /// Returns line items of invoice provided
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public DataTable SetSearchGrid(string s)
        {
            //DataSet dv = new DataView();
            try
            {
                int iRet = 0;
                SetNewInvoiceGrid();
                dsCurrentDataSet = db.ExecuteSQLStatement("SELECT id.ItemDesc " +
                                            "FROM  ItemDesc id INNER JOIN LineItems li " +
                                            "ON id.ItemCode = li.ItemCode " +
                                            "WHERE li.InvoiceNum=" + s, ref iRet);

                for (int i = 0; i < dsCurrentDataSet.Tables[0].Rows.Count; i++)
                {
                    AddRow(dsCurrentDataSet.Tables[0].Rows[i][0].ToString());
                }

                bIsNewInvoice = false;

                return dtCurrentInvoiceTable;

            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                return dtCurrentInvoiceTable;
            }
        }

        /// <summary>
        /// Method to return an invoice total cost when passed in an invoice number
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string GetTotalCost(string s)
        {
            try
            {
                string sResult = "";
                sResult = db.ExecuteScalarSQL("SELECT TotalCost " +
                                            "FROM  Invoices " +
                                            "WHERE InvoiceNum=" + s);
                return sResult;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                return "";
            }
        }

        /// <summary>
        /// Returns the cost of an item when provided the item description
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string GetCost(string s)
        {
            try
            {
                return db.ExecuteScalarSQL("SELECT Cost FROM ItemDesc WHERE ItemDesc=\"" + s + "\"");
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                return "";
            }

        }


        /// <summary>
        /// Increments invoice number - SHOULD BE IN LOGIC CLASS AND THERE'S A BETTER WAY TO DO THIS
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string IncrementInvoiceNum()
        {

            try
            {
                int iRef = 0;
                dsCurrentDataSet = db.ExecuteSQLStatement("SELECT * FROM Invoices", ref iRef);
                return Convert.ToString((dsCurrentDataSet.Tables[0].Rows.Count + 5000));

            }

            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

            return "";


        }

        /// <summary>
        /// Deletes an invoice
        /// </summary>
        /// <param name="s"></param>
        public void DeleteInvoice(string s)
        {
            try
            {
                bIsInvoiceEmpty = true;
                db.ExecuteNonQuery("DELETE FROM Invoice.LineItems " +
                                    "WHERE InvoiceNum=" + s);

                db.ExecuteNonQuery("DELETE FROM Invoice.Invoices " +
                                    "WHERE InvoiceNum=" + s);

                ClearDataTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Helper method to clear the working DataTable
        /// </summary>
        private void ClearDataTable()
        {
            dtCurrentInvoiceTable.Clear();
            dtCurrentInvoiceTable.Rows.Clear();
            dtCurrentInvoiceTable.Columns.Clear();
        }

        /// <summary>
        /// Creates a new empty DataTable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public DataTable SetNewInvoiceGrid()
        {
            
            try
            {
                bIsNewInvoice = true;
                bIsInvoiceEmpty = false;
                ClearDataTable();
                DataColumn colItem1 = new DataColumn("ItemDesc", Type.GetType("System.String"));
                DataColumn colItem2 = new DataColumn("Cost", Type.GetType("System.Int32"));
                dtCurrentInvoiceTable.Columns.Add(colItem1);
                dtCurrentInvoiceTable.Columns.Add(colItem2);
                dtCurrentInvoiceTable.AcceptChanges();

                return dtCurrentInvoiceTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                return dtCurrentInvoiceTable;
            }
        }


        /// <summary>
        /// Method that returns the item descriptions in the database
        /// </summary>
        /// <returns></returns>
        public BindingList<String> GetItemList()
        {
            BindingList<String> items = new BindingList<String>();
            try
            {
                
                //Create a DataSet to hold the data
                DataSet ds;

                //Number of return values
                int iRet = 0;

                //Get all the values from the Authors table
                ds = db.ExecuteSQLStatement("SELECT ItemDesc FROM ItemDesc", ref iRet);

                //Loop through all the values returned
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //Add the first and last name to the list box
                    items.Add(ds.Tables[0].Rows[i][0].ToString());
                }

                return items;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                return items;
            }
        } 

        /// <summary>
        /// Add item to the invoice
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public DataTable AddRow(string s)
        {
            try
            {
                string sItemDesc = s;

                DataRow DR = dtCurrentInvoiceTable.NewRow();

                DR[0] = sItemDesc;
                DR[1] = GetCost(sItemDesc);
                dtCurrentInvoiceTable.Rows.Add(DR);
                dtCurrentInvoiceTable.AcceptChanges();

                return dtCurrentInvoiceTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                return dtCurrentInvoiceTable;
            }
        }

        /// <summary>
        /// Updates the total cost when items are added or removed
        /// </summary>
        /// <returns></returns>
        public string UpdateTotalCost()
        {
            try
            {
                iTotalCost = 0;
                int cost = 0;
                for (int i = 0; i < dtCurrentInvoiceTable.Rows.Count; i++)
                {
                    Int32.TryParse(dtCurrentInvoiceTable.Rows[i][1].ToString(), out cost);
                    iTotalCost += cost;
                }

                return iTotalCost + "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                return "";
            }
        }

        /// <summary>
        /// Deletes an item in the current invoice
        /// </summary>
        /// <param name="s"></param>
        public void DeleteItem(string s)
        {
            try
            {
                int temp;
                Int32.TryParse(s, out temp);
                DataRow dr = dtCurrentInvoiceTable.Rows[temp];

                dr.Delete();

                dtCurrentInvoiceTable.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Method to save the invoice to the database
        /// </summary>
        /// <param name="sInvNum"></param>
        /// <param name="dtDate"></param>
        public void SaveInvoice(string sInvNum, DateTime dtDate)
        {
            try
            {

                int iInvNum;
                Int32.TryParse(sInvNum, out iInvNum);

                if (bIsNewInvoice)
                {
                    db.ExecuteInsertInvoice(iInvNum, dtDate, iTotalCost);

                    
                } else if(!bIsNewInvoice)
                {
                    db.ExecuteUpdateInvoice(iInvNum, dtDate, iTotalCost);
                    db.ExecuteDeleteRows(iInvNum);
                }

                for (int i = 0; i < dtCurrentInvoiceTable.Rows.Count; i++)
                {
                    string sItemDesc = dtCurrentInvoiceTable.Rows[i][0].ToString();
                    Char[] cItemCode = db.ExecuteScalarSQL("SELECT ItemCode FROM ItemDesc WHERE ItemDesc=\"" + sItemDesc + "\"").ToCharArray();
                    char cCode = cItemCode[0];
                    int iLineNum = i + 1;
                    db.ExecuteAddRows(iInvNum, iLineNum, cCode);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }
    }
    #endregion
}
