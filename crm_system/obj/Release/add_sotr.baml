   M S B A M L   `   `   `  ���� E  Acrm_system, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:crm_system
crm_system      crm_system.add_sotr   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:crm_system     -     � Title$   ��$	��320��$	��340�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=�.��� .+��� .+��� $��20,20q�.+���  �� Orientation$ 
Horizontal=�.+��� $��0,0,10,0q�.+��� $��Организация��$
��14pt(�$��0,0,0,15q��� $��Имя��$
��14pt(�$��0,0,0,13q��� $��Фамилия��$
��14pt(�$��0,0,0,13q��� $��Отчество��$
��14pt(�$��0,0,0,10q��� $��Должность��$
��14pt(�$��0,0,0,10q��� .+��� -   $
��orgs��$	��150��$
��14pt(�$��0,0,0,10q�$��30�� �� DisplayMemberPath$
 name�� �� SelectedValuePath$ value��   ComboBoxFlatStyle#���   ���� *
comboitems+�� -   $
��name��$	��150��$
��14pt(�$��0,0,0,10q�  TextBoxStyle#���  �� -   $��surname��$	��150��$
��14pt(�$��0,0,0,10q�#���  �� -   $��lastname��$	��150��$
��14pt(�$��0,0,0,10q�#���  �� -   $��	job_title��$	��150��$
��14pt(�$��0,0,0,10q�$��30��$
 name��$ value��#���   ���� *
comboitems+�� $ 
Horizontal=�.+��� -   $��
add_or_upd��$	��120��$��Добавить��$��	10,0,20,0q�$
��14pt(�  
DefaultBtn#���  �� -   $��cancel��$	��120��$��Отмена��$
��14pt(�#���  