   M S B A M L   `   `   `  ���� E  Acrm_system, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:crm_system
crm_system      crm_system.add_call   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:crm_system     -     � Title$   ��$	��410��$	��400�� � 
ResizeMode$ CanMinimize=� � WindowStartupLocation$ CenterScreen=�.��� .+��� .+��� $��20q� �� Orientation$ 
Horizontal=�.+��� .+��� $��Организация��$
��16pt(�$��0,0,0,25q��� $��Дата звонка��$
��16pt(�$��0,0,0,70q��� $��Цель звонка��$
��16pt(�$��0,0,0,0q��� $	��200��$��20,0,0,0q�.+��� -   $	��org��$
��16pt(�$��0,0,0,20q� �� DisplayMemberPath$
 name�� �� SelectedValuePath$ value��   ComboBoxFlatStyle#���   ���� *
comboItems+(  "System.Windows.Controls.DatePicker  -   $��	call_date��$
��16pt(�$��0,0,0,20q��� -   $��call_traget��$
��16pt(�$	��200��$	��120�� �� TextWrapping$
 Wrap=�  TextBoxStyle#���  �� -   $��call_buttons��
 �� Top$	 280��$��20q�$ 
Horizontal=�.+��� -   $��add_cal��$��Сохранить��$
��16pt(�$��0,0,10,0q�$	��175��  
DefaultBtn#���  �� -   $��cancel��$��Отмена��$
��16pt(�$	��175��#���  