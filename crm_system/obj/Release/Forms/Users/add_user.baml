   M S B A M L   `   `   `  ���� E  Acrm_system, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:crm_system
crm_system      crm_system.add_user   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:crm_system     -     � Title$  Регитсрация��$	��380��$	��330�� � 
ResizeMode$ CanMinimize=� � WindowStartupLocation$ CenterScreen=�.��� .+��� .+���  �� Orientation$ 
Horizontal=�$��
10,10,10,0q�.+��� $��0,0,20,0q�.+��� $��Имя��$
��15pt(�$��0,0,0,17q��� $��Фамилия��$
��15pt(�$��0,0,0,17q��� $��Отчество��$
��15pt(�$��0,0,0,17q��� $��
Логин��$
��15pt(�$��0,0,0,17q��� $��Пароль��$
��15pt(�$��0,0,0,18q��� $��Роль��$
��15pt(�$��0,0,0,15q��� .+��� -   $
��Name��$
��15pt(�$��0,0,0,15q�$	��180��   TextBoxStyle#���   �� -   $��Surname��$
��15pt(�$��0,0,0,15q�$	��180��#���   �� -   $��second_name��$
��15pt(�$��0,0,0,15q�$	��180��#���   �� -   $��	exception��$#��Логин уже занят!��
���  ��$��0���� -   $��Login��$
��15pt(�$��0,0,0,15q�$	��180��#���   G� -   $
��Pass��$
��15pt(�$��0,0,0,15q�$	��180��  PasswordBoxStyle#���  �� -   $
��rols��$
��15pt(�$��0,0,0,15q�$	��180�� �� DisplayMemberPath$
 name�� �� SelectedValuePath$ value��  ComboBoxFlatStyle#���  ���� *	comboitem+�� $ 
Horizontal=� �� Left$ 15��
 �� Top$	 280��.+��� -	   $
��save��$��Сохранить��$��0,0,10,0q�$
��15pt(�$	��140��  
DefaultBtn#���  �� -
   $��cancel��$��Отмена��$��0,0,10,0q�$
��15pt(�$	��140��#���  