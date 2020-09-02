using Group_Project.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Group_Project.Main
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : Window
    {

        #region Class Variables


        /// <summary>
        /// Class variable to access main logic
        /// </summary>
        clsMainLogic ml;

        /// <summary>
        /// Class variable that holds the invoice number
        /// </summary>
        string sInvNum;

        /// <summary>
        /// Class variable that hold the invoice date
        /// </summary>
        DateTime dtDate;

        #endregion

        #region Methods

        /// <summary>
        /// This is a constructor
        /// </summary>
        public wndMain()
        {
            InitializeComponent();
            ml = new clsMainLogic();
            EnableInvoiceElements(false, false, false, false, false, false, false, false, false, false);
        }

        /// <summary>
        /// Helper method that enables or disables different elements of the UI.
        /// </summary>
        /// <param name="bInvNum"></param>
        /// <param name="bDate"></param>
        /// <param name="bTotal"></param>
        /// <param name="bAdd"></param>
        /// <param name="bCost"></param>
        /// <param name="bEdit"></param>
        /// <param name="bDelInv"></param>
        /// <param name="bDelItem"></param>
        /// <param name="bSaveInv"></param>
        /// <param name="bDataGrid"></param>
        private void EnableInvoiceElements(bool bInvNum, bool bDate, bool bTotal, bool bAdd, bool bCost, bool bEdit, bool bDelInv, bool bDelItem, bool bSaveInv, bool bDataGrid)
        {
            try
            {
                tbInvoiceNum.IsEnabled = bInvNum;
                dpDate.IsEnabled = bDate;
                tbTotalCost.IsEnabled = bTotal;
                cbSelectItem.IsEnabled = bAdd;
                bAddItem.IsEnabled = bAdd;
                tbCost.IsEnabled = bCost;
                bEditInvoice.IsEnabled = bEdit;
                bDeleteInvoice.IsEnabled = bDelInv;
                bDeleteItem.IsEnabled = bDelItem;
                bSaveInvoice.IsEnabled = bSaveInv;
                dgMainInvoices.IsEnabled = bDataGrid;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Helper method to add items to the combo box
        /// </summary>
        private void LoadItems()
        {
            try
            {
                BindingList<string> items = ml.GetItemList();

                foreach (string s in items)
                {
                    cbSelectItem.Items.Add(s);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        #endregion

        #region Event Methods

        /// <summary>
        /// Executes when Open Search is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOpenSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Group_Project.Search.wndSearch wnd1 = new Group_Project.Search.wndSearch();
                wnd1.ShowDialog();
                if (clsSearchLogic.currInvoiceID > 4999)
                {
                    string sInvNum = clsSearchLogic.currInvoiceID.ToString();
                    tbInvoiceNum.Text = sInvNum;
                    dpDate.Text = ml.GetInvoiceDate(sInvNum);
                    tbTotalCost.Text = string.Format("${0:#.00}", Convert.ToDecimal(ml.GetTotalCost(sInvNum)));
                    dgMainInvoices.ItemsSource = new DataView(ml.SetSearchGrid(sInvNum));
                    LoadItems();
                    EnableInvoiceElements(true, true, true, false, false, true, true, false, false, true);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Executes when the New Invoice button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bNewInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EnableInvoiceElements(true, true, true, true, true, false, false, false, true, true);
                tbInvoiceNum.Text = ml.IncrementInvoiceNum();
                dgMainInvoices.ItemsSource = new DataView(ml.SetNewInvoiceGrid());
                LoadItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Executes when the Add Item button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dgMainInvoices.ItemsSource = new DataView(ml.AddRow(cbSelectItem.SelectedItem.ToString()));
                EnableInvoiceElements(true, true, true, true, true, false, true, true, true, true);
                tbTotalCost.Text = string.Format("${0:#.00}", Convert.ToDecimal(ml.UpdateTotalCost()));

            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Executes when a selected is changed in the add item combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbSelectItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (!ml.bIsInvoiceEmpty)
                {
                    string result = ml.GetCost(cbSelectItem.SelectedItem.ToString());
                    tbCost.Text = string.Format("${0:#.00}", Convert.ToDecimal(result));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Executes when the Save Invoice button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bSaveInvoice_Click(object sender, RoutedEventArgs e)
        {

            try {
                sInvNum = tbInvoiceNum.Text;
                dtDate = dpDate.SelectedDate.Value;
                ml.SaveInvoice(sInvNum, dtDate);
                EnableInvoiceElements(false, false, false, false, false, true, true, false, false, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// Executes when Edit Invoice button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bEditInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EnableInvoiceElements(true, true, true, true, true, false, true, true, true, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Executes when Delete Invoice button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bDeleteInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ml.DeleteInvoice(tbInvoiceNum.Text);
                EnableInvoiceElements(false, false, false, false, false, false, false, false, false, false);
                tbInvoiceNum.Text = "";
                dpDate.Text = "";
                tbTotalCost.Text = "";
                tbCost.Text = "";
                cbSelectItem.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Deletes an item from the invoice list and updates total cost
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ml.DeleteItem(dgMainInvoices.SelectedIndex.ToString());
                tbTotalCost.Text = string.Format("${0:#.00}", Convert.ToDecimal(ml.UpdateTotalCost()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Updates invoice cost (for now)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miRunUpdate_Click(object sender, RoutedEventArgs e)
        {
            tbTotalCost.Text = string.Format("${0:#.00}", Convert.ToDecimal(ml.UpdateTotalCost()));
        }


        #endregion


    }
}
