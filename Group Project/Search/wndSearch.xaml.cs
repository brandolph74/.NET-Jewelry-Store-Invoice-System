using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
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

namespace Group_Project.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        /// <summary>
        /// list of all the invoice numbers, used for the InvoiceID combo box
        /// </summary>
        public BindingList<int> invoiceListNumbers = new BindingList<int>();

        /// <summary>
        /// List of the dates for the dateComboBox
        /// </summary>
        public  BindingList<string> dateList = new BindingList<string>();

        /// <summary>
        /// list of all the charge ammounts for the invoices. Used for the charge
        /// </summary>
        public List<double> chargeList = new List<double>();

        /// <summary>
        /// List of invoices that will be bound to the data grid for the user to select.
        /// </summary>
        public BindingList<SearchInvoice> invoiceList = new BindingList<SearchInvoice>();

        clsSearchLogic searchClass = new clsSearchLogic();
        public wndSearch()
        {
            InitializeComponent();
            invoiceDataGrid.ItemsSource = invoiceList;       //bind the data grid to the invoice List
            invoiceIDComboBox.ItemsSource = invoiceListNumbers; //bind the combo box to the invoice number list
            dateComboBox.ItemsSource = dateList;   //bind the combo box to the date list
            chargeComboBox.ItemsSource = chargeList;     //bind the combo box to the charge list
            /*DataGridTextColumn invoiceColumn = new DataGridTextColumn();
            invoiceColumn.Header = "Invoices";
            invoiceDataGrid.Columns.Add(invoiceColumn); */
        }
        /// <summary>
        /// Code for the Select Invoice Button. Sets the static variable found in clsSearchLogic so the main window can open the desired Invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (invoiceDataGrid.SelectedIndex == -1)   // -1 means the the value has not changed from its original state, therefore no Invoice was selected.
                {
                    MessageBox.Show("Select an Invoice from the Invoice List");  //show the message box telling the user they have not selected anything
                }
                else
                {
                    
                    
                    clsSearchLogic.currInvoiceID = ((SearchInvoice)invoiceDataGrid.SelectedItem).invoiceNumber;        //set static variable currInvoiceID to the selected Invoice ID
                    this.Hide();                                                         //hide the window since an invoice was selected
                }
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

            /// <summary>
            /// button that sends the user back to the main menu. 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void cancelButton_Click(object sender, RoutedEventArgs e)
            {
            try
            {
                
                this.Hide();  //hide this window
            }
                catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
            

            }

        /// <summary>
        /// the method that kicks everything off. Calls the other methods I created. Everytime the window appears/dissapears it recompiles the lists of invoices.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if (this.Visibility == Visibility.Visible)
                {
                    invoiceList.Clear();
                    invoiceListNumbers.Clear();    //moved these from the cancel button, clears anything before opening the window in case it was already opened.
                    dateList.Clear();
                    chargeList.Clear();

                    DataSet invoices = searchClass.getInvoices();   //get the dataSet

                    for (int i = 0; i < invoices.Tables[0].Rows.Count; i++)
                    {
                        int numb;
                        double charge;
                        Int32.TryParse(invoices.Tables[0].Rows[i][0].ToString(), out numb);
                        Double.TryParse(invoices.Tables[0].Rows[i][2].ToString(), out charge);
                        SearchInvoice invoice = new SearchInvoice(numb, invoices.Tables[0].Rows[i][1].ToString(), charge);

                        invoiceList.Add(invoice); //add the invoice to the list
                                                  // num, date, charge is the order for the constructor


                        dateList.Add(invoice.invoiceDate.ToString());  //add the date to the date List;

                        if (chargeList.Contains(charge) == false) //could have just used UNIQUE in the SQL but I decided to do it here anyway
                        {
                            chargeList.Add(charge);  //add the specific charge to the charge combo box
                        }

                        invoiceListNumbers.Add(numb);  //add the specific invoice number to the invoice number combo box.

                    }
                    chargeList.Sort();   //sort the charge list in ascending order
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// method that will re-populate the data grid of invoices with the specified invoice selection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void invoiceIDComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.Visibility != Visibility.Hidden && invoiceIDComboBox.SelectedIndex != -1)
                {
                    invoiceList.Clear();  //clear the list of other invoices
                    int invoiceID;        //int used to pass in the ID of the invoice
                    Int32.TryParse(invoiceIDComboBox.SelectedItem.ToString(), out invoiceID);
                    DataSet invoices = searchClass.getInvoices(invoiceID);   //get the dataSet

                    for (int i = 0; i < invoices.Tables[0].Rows.Count; i++)
                    {
                        int numb;
                        double charge;
                        Int32.TryParse(invoices.Tables[0].Rows[i][0].ToString(), out numb);
                        Double.TryParse(invoices.Tables[0].Rows[i][2].ToString(), out charge);
                        SearchInvoice invoice = new SearchInvoice(numb, invoices.Tables[0].Rows[i][1].ToString(), charge);

                        invoiceList.Add(invoice); //add the invoice to the list
                                                  // num, date, charge is the order for the constructor


                        //dateList.Add(invoice.invoiceDate.ToString());  //add the date to the date List;

                        //chargeList.Add(charge);  //add the specific charge to the charge combo box

                        //invoiceListNumbers.Add(numb);  //add the specific invoice number to the invoice number combo box.

                        


                    }

                    dateComboBox.SelectedIndex = -1;       //reset the date and charge combo boxes
                    chargeComboBox.SelectedIndex = -1;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        
        /// <summary>
        /// method similar to invoiceIDComboBox_SelectionChanged Except this will change the data grid based on date (and charge if it is selected)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (chargeComboBox.SelectedIndex > -1 && this.Visibility != Visibility.Hidden && dateComboBox.SelectedIndex != -1)
                {
                    //if chargeComboBox has been selected


                    
                    invoiceList.Clear();  //clear the list of other invoices
                    string sDate;        //string used to pass in the date of the invoice
                    sDate = dateComboBox.SelectedItem.ToString(); //convert the date
                    //DateTime date = DateTime.ParseExact(sDate, "M/dd/yyyy hh:mm:ss tt", null);

                    double dCharge;
                    Double.TryParse(chargeComboBox.SelectedItem.ToString(), out dCharge);

                    DataSet invoices = searchClass.getInvoices(sDate, dCharge);   //get the dataSet


                    for (int i = 0; i < invoices.Tables[0].Rows.Count; i++)
                    {
                        int numb;
                        double charge;
                        Int32.TryParse(invoices.Tables[0].Rows[i][0].ToString(), out numb);
                        Double.TryParse(invoices.Tables[0].Rows[i][2].ToString(), out charge);
                        SearchInvoice invoice = new SearchInvoice(numb, invoices.Tables[0].Rows[i][1].ToString(), charge);

                        invoiceList.Add(invoice); //add the invoice to the list
                                                  // num, date, charge is the order for the constructor


                        //dateList.Add(invoice.invoiceDate.ToString());  //add the date to the date List;

                        //chargeList.Add(charge);  //add the specific charge to the charge combo box

                        //invoiceListNumbers.Add(numb);  //add the specific invoice number to the invoice number combo box.




                    }
                }                                                                              //the && bit below this comment is to stop bugs with the cancel button
                if (chargeComboBox.SelectedIndex == -1 && this.Visibility != Visibility.Hidden && dateComboBox.SelectedIndex != -1)
                {
                    //chargeComboBox has not been selected
                    invoiceList.Clear();  //clear the list of other invoices
                    string sDate;        //string used to pass in the date of the invoice
                    sDate = dateComboBox.SelectedItem.ToString(); //convert the date to a string
                    //DateTime date = DateTime.ParseExact(sDate, "M/dd/yyyy hh:mm:ss tt", null);
                    DataSet invoices = searchClass.getInvoices(sDate);   //get the dataSet
                    

                    for (int i = 0; i < invoices.Tables[0].Rows.Count; i++)
                    {
                        int numb;
                        double charge;
                        Int32.TryParse(invoices.Tables[0].Rows[i][0].ToString(), out numb);
                        Double.TryParse(invoices.Tables[0].Rows[i][2].ToString(), out charge);
                        SearchInvoice invoice = new SearchInvoice(numb, invoices.Tables[0].Rows[i][1].ToString(), charge);

                        invoiceList.Add(invoice); //add the invoice to the list
                                                  // num, date, charge is the order for the constructor


                        //dateList.Add(invoice.invoiceDate.ToString());  //add the date to the date List;

                        //chargeList.Add(charge);  //add the specific charge to the charge combo box

                        //invoiceListNumbers.Add(numb);  //add the specific invoice number to the invoice number combo box.




                    }
                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// same method as dateComboBox changed, this one just changes the data grid based on the charge (and date if it is selected also)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chargeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dateComboBox.SelectedIndex > -1 && this.Visibility != Visibility.Hidden && chargeComboBox.SelectedIndex != -1)
                {
                    //if dateComboBox has been selected



                    invoiceList.Clear();  //clear the list of other invoices
                    string sDate;        //string used to pass in the date of the invoice
                    sDate = dateComboBox.SelectedItem.ToString(); //convert the date
                    //DateTime date = DateTime.ParseExact(sDate, "M/dd/yyyy hh:mm:ss tt", null);

                    double dCharge;
                    Double.TryParse(chargeComboBox.SelectedItem.ToString(), out dCharge);

                    DataSet invoices = searchClass.getInvoices(sDate, dCharge);   //get the dataSet


                    for (int i = 0; i < invoices.Tables[0].Rows.Count; i++)
                    {
                        int numb;
                        double charge;
                        Int32.TryParse(invoices.Tables[0].Rows[i][0].ToString(), out numb);
                        Double.TryParse(invoices.Tables[0].Rows[i][2].ToString(), out charge);
                        SearchInvoice invoice = new SearchInvoice(numb, invoices.Tables[0].Rows[i][1].ToString(), charge);

                        invoiceList.Add(invoice); //add the invoice to the list
                                                  // num, date, charge is the order for the constructor


                        //dateList.Add(invoice.invoiceDate.ToString());  //add the date to the date List;

                        //chargeList.Add(charge);  //add the specific charge to the charge combo box

                        //invoiceListNumbers.Add(numb);  //add the specific invoice number to the invoice number combo box.




                    }
                }                                                                            //added the && here again to avoid errors with cancel button
                if (dateComboBox.SelectedIndex == -1 && this.Visibility != Visibility.Hidden && chargeComboBox.SelectedIndex != -1)
                {
                    //dateComboBox has not been selected
                    invoiceList.Clear();  //clear the list of other invoices
                    
                    double dCharge;
                    Double.TryParse(chargeComboBox.SelectedItem.ToString(), out dCharge);

                    //DateTime date = DateTime.ParseExact(sDate, "M/dd/yyyy hh:mm:ss tt", null);
                    DataSet invoices = searchClass.getInvoices(dCharge);   //get the dataSet


                    for (int i = 0; i < invoices.Tables[0].Rows.Count; i++)
                    {
                        int numb;
                        double charge;
                        Int32.TryParse(invoices.Tables[0].Rows[i][0].ToString(), out numb);
                        Double.TryParse(invoices.Tables[0].Rows[i][2].ToString(), out charge);
                        SearchInvoice invoice = new SearchInvoice(numb, invoices.Tables[0].Rows[i][1].ToString(), charge);

                        invoiceList.Add(invoice); //add the invoice to the list
                                                  // num, date, charge is the order for the constructor


                        //dateList.Add(invoice.invoiceDate.ToString());  //add the date to the date List;

                        //chargeList.Add(charge);  //add the specific charge to the charge combo box

                        //invoiceListNumbers.Add(numb);  //add the specific invoice number to the invoice number combo box.




                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// method for the clear button, sets all of the selected indexes back to -1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            invoiceIDComboBox.SelectedIndex = -1;
            dateComboBox.SelectedIndex = -1;        //reset all of the indexes
            chargeComboBox.SelectedIndex = -1;

            //do the same thing and repopulate the invoice list back to the default values it had when the window first opened.

            invoiceList.Clear();  //clear previous entries

            DataSet invoices = searchClass.getInvoices();   //get the dataSet

            for (int i = 0; i < invoices.Tables[0].Rows.Count; i++)
            {
                int numb;
                double charge;
                Int32.TryParse(invoices.Tables[0].Rows[i][0].ToString(), out numb);
                Double.TryParse(invoices.Tables[0].Rows[i][2].ToString(), out charge);
                SearchInvoice invoice = new SearchInvoice(numb, invoices.Tables[0].Rows[i][1].ToString(), charge);

                invoiceList.Add(invoice); //add the invoice to the list
                                          // num, date, charge is the order for the constructor


                //dateList.Add(invoice.invoiceDate.ToString());  //add the date to the date List;

                //chargeList.Add(charge);  //add the specific charge to the charge combo box

                //invoiceListNumbers.Add(numb);  //add the specific invoice number to the invoice number combo box.




            }

        }
    }

      
    }
