   M S B A M L   `   `   `  ���� E  Acrm_system, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:crm_system
crm_system      crm_system.add_rolls   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:crm_system     -     � Title$   ��$	��400��$	��340�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=�.��� .+��� .+���  �� Orientation$ 
Horizontal=�$��	50,10,0,0q�.+��� .+��� $��Имя роли��$
��13pt(��� $��10,0,0,0q�.+��� -   $��	roll_name��$	��100��$
��13pt(�   TextBoxStyle#���   �� .+�&   System.Windows.Controls.DataGrid  -   $��permis_grid��$
��12pt(�$	��260��   AutoGenerateColumns .  $	��300��$��	10,50,0,0q�  DataGridStyle#���   � ContextMenu �� .@�m� -   $	��del��$��Удалить право��m� -   $	��add��$!��Доьавить право��   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 
Права��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 300 1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 �� 
 �� Path$
 caption��� -   $��
add_or_upd��$	��150��$��10q�$��Сохранить��$
��12pt(�  
DefaultBtn#���  