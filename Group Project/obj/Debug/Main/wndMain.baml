   M S B A M L   `   `   `  ����H  DGroup Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Group_Project.MainGroup_Project.Main  5   	        Group_Project.Main.wndMain   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:Group_Project.Main     5   	     � Title$&   Diamonds-n-Stuff Invoice Manager��5   	   $��463.725��62   $��620.344��6C   
�������.��5	      � .+�5
   
   �� -   5        $��bNewInvoice��5
      $��New Invoice��6$   $
��Left=�6:   $��	19,42,0,0q�6U   $	��Top=�6h   $��97��6�   $��26��6�   6�   �� 5   
   -   5        $��bEditInvoice��5      $��Edit Invoice��6%   $
��Left=�6<   $��	19,73,0,0q�6W   $	��Top=�6j   $��97��6�   $��26��6�   6�   �� 5   
   -   5        $��bDeleteInvoice��5      $��Delete Invoice��6'   $
��Left=�6@   $��
19,104,0,0q�6[   $	��Top=�6o   $��97��6�   $��26��6�   6�   &   System.Windows.Controls.DataGrid  5   
   -   5        $��dgMainInvoices��5      $
��Left=�6)   $	��227��6D   $��146,184,0,0q�6Q   $	��Top=�6f   $	��431��6~      ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6�   �� 5   
   -   5        $��lblDate��5      $��Date:��6   $
��Left=�6/   $��
289,49,0,0q�6J   $	��Top=�6^   $��23��6v   $��66��6�   �� 5   
   -   5        $��lblInvoiceNum��5      $��
Invoice #:��6%   $
��Left=�6:   $��
146,45,0,0q�6U   $	��Top=�6i    J� RenderTransformOrigin$ 	0.39,0.19&�6�   �� 5   
   -   5        $��tbInvoiceNum��5      $
��Left=�6&   $��28��6A   $��
211,49,0,0q�6M    �� TextWrapping$
 Wrap=�6a   $�� ��6u   $	��Top=�6}   $��73��6�    �� 
IsReadOnly . $��Center=�6�   ���	#FFDFDEDE6�   �� 5   
   -   5        $��bAddItem��5      $��Add Item��6!   $
��Left=�64   $��
457,95,0,0q�6O   $	��Top=�6c   $��97��6{   $��26��6�   $ 0.648,0.624&�6�   �� 5   
   -	   5        $��bDeleteItem��5      $��Delete Item��6$   $
��Left=�6:   $��
19,354,0,0q�6U   $	��Top=�6i   $��97��6�   $��26��6�   6�   �� 5   
   -
   5        $��lblTotalCost��5      $��Total Cost:��6$   $
��Left=�6:   $��
441,46,0,0q�6U   $	��Top=�6i   �� 5   
   -   5        $��tbTotalCost��5      $��Right=�6%   $��23��6A   $��	0,48,33,0q�6M   $
 Wrap=�6`   $�� ��6t   $	��Top=�6|   $��71��6�   $��Center=�6�   $��Right=�6�    . 6�   �� 5   
   -   5        $��bSaveInvoice��5      $��Save Invoice��6%   $
��Left=�6<   $��
19,385,0,0q�6W   $	��Top=�6k   $��97��6�   $��26��6�   6�   ]� 5   
   $
��Left=�6   $��19��6/   ��.  $	��Top=�6Q   $	��610��6i   .+�5      o� $	��Top=�6   $��19��6(   .@�5      m� $
��File��6   $��Bottom=�6)   $��Bottom=�6K   .@�5      m� -   5        $��miExit��5      $��_Exit��6-   5      m� 5      $��Search��6   $��Bottom=�6+   $��Bottom=�6M   .@�5      m� -   5        $��miOpenSearch��5      $��_Open Search��63   6I   5      m� 5      $��Update��6   $��Bottom=�6+   $��Bottom=�6M   .@�5      m� -   5        $��_Run Update��5      64   5       5!      �� 5"      6   5#      (  "System.Windows.Controls.DatePicker  5$   
   -   5        $��dpDate��5$      $
��Left=�6#   $��
325,49,0,0q�6>   $	��Top=�6R   $��28��6j   �� 5%   
   -   5        $��cbSelectItem��5%      $
��Left=�6'   $��
175,95,0,0q�6B   $	��Top=�6V   $	��252��6n   6z   �� 5&   
   -   5        $��tbCost��5&      $
��Left=�6    $��23��6;   $��325,136,0,0q�6G   $
 Wrap=�6\   $�� ��6p   $	��Top=�6x   $��73��6�   �� 5'   
   -   5        $��lblCost��5'      $
��Cost��6   $
��Left=�6.   $��256,136,0,0q�6I   $	��Top=�6^   $��64��6v   5(      5)      