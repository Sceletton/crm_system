   M S B A M L   `   `   `  ����E  Acrm_system, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:crm_system
crm_system  5   	       crm_system.add_call   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:crm_system     5   	   -   5          � Title$   ��5   	   $	��410��6   $	��400��6    � 
ResizeMode$ NoResize=�6B    � WindowStartupLocation$ CenterScreen=�6X   .��5	      � .+�5
   
   �� .+�5      �� $��20q�6    �� Orientation$ 
Horizontal=�6%   .+�5      �� .+�5      �� $��Организация��6    $
��16pt(�63   $��0,0,0,25q�6C   �� 5      $��Дата звонка��6    $
��16pt(�63   $��0,0,0,70q�6C   �� 5      $��Цель звонка��6    $
��16pt(�63   $��0,0,0,0q�6C   5      �� 5      $	��200��6   $��20,0,0,0q�6)   .+�5      �� -   5        $	��org��5      $
��16pt(�6*   $��0,0,0,20q�6:    �� DisplayMemberPath$
 name��6o    �� SelectedValuePath$ value��6�      ComboBoxFlatStyle#���   6�   ��6L   �� *
comboItems+6�   (  "System.Windows.Controls.DatePicker  5      -   5        $��	call_date��5   !   $
��16pt(�62   $��0,0,0,20q�6B   �� 5      -   5        $��call_traget��5      $
��16pt(�61   $	��200��6A   $	��120��6M    �� TextWrapping$
 Wrap=�6Z     TextBoxStyle#���  6n   6�   5      5      �� 5      -   5        $��call_buttons��5      
 �� Top$	 280��6-   $��20q�6>   $ 
Horizontal=�6J   .+�5      �� -   5        $��add_cal��5      $��Добавить��6(   $
��16pt(�6;   $��0,0,10,0q�6K   $	��175��6]     
DefaultBtn#���  6   �� 5      -   5        $��cancel��5      $��Отмена��6'   $
��16pt(�68   $	��175��6H   #���  6T   6y   5      5      5      5      