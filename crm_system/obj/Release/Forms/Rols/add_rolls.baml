   M S B A M L   `   `   `  ���� E  Acrm_system, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:crm_system
crm_system      crm_system.add_rolls   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:crm_system     -     � Title$   ��$	��540��$	��500�� � 
ResizeMode$ CanMinimize=� � WindowStartupLocation$ CenterScreen=�.��� .+��� .+��� .+���  �� Orientation$ 
Horizontal=�$��	50,10,0,0q�$��Center=�.+��� .+��� $��Имя роли:��$
��13pt(��� -   $��	roll_name��$	��350��$
��13pt(�   TextBoxStyle#���    �� 	MaxLength$ 20���� -   $��except��$L��FУ роли обязательно должны быить права!��$��Center=�$
��12pt(�
���  ��$��0,0,0,0q�$��0���� .+�&   System.Windows.Controls.DataGrid  -   $��permis_grid��$
��12pt(�$	��330��   AutoGenerateColumns .  $	��465��$��	10,10,0,0q�  DataGridStyle#���     Columns 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn   Header �� -      CellTemplate �� .R��� $��Center=�$��50��	 d� 	IsChecked	 �� 
 �� Path$
 is_check� �� UpdateSourceTrigger$ PropertyChanged=�0  *System.Windows.Controls.DataGridTextColumn  $ Название��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 350 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� $

 name��� -   $��
add_or_upd��$	��150��$��10q�$��Сохранить��$
��12pt(�  
DefaultBtn#���  