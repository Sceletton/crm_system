   M S B A M L   `   `   `  ����E  Acrm_system, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:crm_system
crm_system  5   	       crm_system.add_rolls   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:crm_system     5   	   -   5          � Title$   ��5   	   $	��480��6   $	��340��6    � 
ResizeMode$ NoResize=�6B    � WindowStartupLocation$ CenterScreen=�6X   .��5	      � .+�5
   
   �� .+�5      �� .+�5      ��  �� Orientation$ 
Horizontal=�6   $��	50,10,0,0q�66   .+�5      �� .+�5      �� $��Имя роли��6    $
��13pt(�60   5      �� 5      $��10,0,0,0q�6   .+�5      �� -   5        $��	roll_name��5      $	��100��6/   $
��13pt(�6;      TextBoxStyle#���   6K   6r   5      5      �� 5      .+�5      &   System.Windows.Controls.DataGrid  -   5        $��permis_grid��5   =   $
��12pt(�6   $	��350��6-      AutoGenerateColumns .  $	��310��6l   $��	10,10,0,0q�6x     DataGridStyle#���  6�      Columns 5      4  .System.Windows.Controls.DataGridTemplateColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header 5      �� 5   "   -   5        5   H      CellTemplate 5      �� 5   "   .R�5   &   �� $��Center=�6/   $��50��6M    d� 	IsChecked 6X   �� 	 �� Path$	 is_check�
 �� UpdateSourceTrigger$
 PropertyChanged=�5   #   5       0  *System.Windows.Controls.DataGridTextColumn  5!      $ Название��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$	 350 6?   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6K   �� $
	 name�5"      5$      �� 5%      -   5        $��
add_or_upd��5%      $	��150��6+   $��10q�67   $��Сохранить��6C   $
��12pt(�6W     
DefaultBtn#���  6�   5&      5'      5(      5)      5*      