   M S B A M L   `   `   `  ����E  Acrm_system, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:crm_system
crm_system  5   	       crm_system.add_sotr   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:crm_system     5   	   -   5          � Title$   ��5   	   $	��320��6   $	��340��6    � 
ResizeMode$ NoResize=�6B    � WindowStartupLocation$ CenterScreen=�6X   .��5	      � .+�5
   
   �� .+�5      �� $��20,20q�6   .+�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $��0,0,10,0q�6!   .+�5      �� $��Организация��6$   $
��14pt(�67   $��0,0,0,15q�6G   �� 5      $��Имя��6$   $
��14pt(�6/   $��0,0,0,13q�6?   �� 5      $��Фамилия��6$   $
��14pt(�63   $��0,0,0,13q�6C   �� 5      $��Отчество��6$   $
��14pt(�64   $��0,0,0,10q�6D   �� 5      $��Должность��6$   $
��14pt(�65   $��0,0,0,10q�6E   5      �� 5      .+�5      �� -   5        $
��orgs��5   /   $	��150��6#   $
��14pt(�6;   $��0,0,0,10q�6K   $��30��6]    �� DisplayMemberPath$
 name��6�    �� SelectedValuePath$ value��6�      ComboBoxFlatStyle#���   6�   ��6i   �� *
comboitems+6�   �� 5      -   5        $
��name��5   .   $	��150��6"   $
��14pt(�6:   $��0,0,0,10q�6J     TextBoxStyle#���  6\   6�   �� 5      -   5        $��surname��5   .   $	��150��6"   $
��14pt(�6=   $��0,0,0,10q�6M   #���  6_   6�   �� 5      -   5        $��lastname��5   .   $	��150��6"   $
��14pt(�6>   $��0,0,0,10q�6N   #���  6`   6�   �� 5      -   5        $��	job_title��5   /   $	��150��6#   $
��14pt(�6@   $��0,0,0,10q�6P   $��30��6b   $
 name��6�   $ value��6�   #���   6�   ��6n   �� *
comboitems+6�   5      5      �� 5      $ 
Horizontal=�6   .+�5      �� -   5        $��
add_or_upd��5      $	��120��6/   $��Добавить��6;   $��	10,0,20,0q�6N   $
��14pt(�6a     
DefaultBtn#���  6�   �� 5      -   5        $��cancel��5      $	��120��6+   $��Отмена��67   $
��14pt(�6H   #���  6X   6}   5      5       5!      5"      5#      