   M S B A M L   `   `   `  ���� E  Acrm_system, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:crm_system
crm_system  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 	  Tabit  �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius ��Name �� Triggers P� Property P� Value	 �� 
TargetName ' #RowBackgroundAlternationIndex2Brush
 �� Color  RowBackgroundSelectedBrush  RowForegroundSelectedBrush)   #System.Windows.Controls.DataGridRow  DataGridDemoRowStyle �� Triggers  DataGridCellStyle*  $System.Windows.Controls.DataGridCell + 'DatagridColumnHeaderCustomTemplateStyle=  7System.Windows.Controls.Primitives.DataGridColumnHeader  DataGridStyle&   System.Windows.Controls.DataGrid  BtnFocusVisual J� SnapsToDevicePixels �� StrokeDashArray 	 
DefaultBtn 
 Static.Background.Brush  Static.Foreground.Brush  Static.Border.Brush ��Name  TextBoxStyle ��Name u� Bottom u� Left u� Right
 u� Top3  -System.Windows.Media.Effects.DropShadowEffect   ShadowDepth   
BlurRadius   Color  PasswordBoxStyle  ComboBoxNormalBorderBrush ! ComboBoxNormalBackgroundBrush # ComboBoxDisabledForegroundBrush # ComboBoxDisabledBackgroundBrush  ComboBoxDisabledBorderBrush   ComboBoxToggleButtonTemplate � ColumnDefinitions  ComboBoxFlatStyle �� 	ClickMode d� 	IsChecked �� Path �� RelativeSource �� Mode J� IsHitTestVisible �� SelectionBoxItem �� SelectionBoxItemTemplate  �� 
IsReadOnly! �� 
IsReadOnly" � AllowsTransparency# �� IsDropDownOpen$ � ActualWidth% �� MaxDropDownHeight-  'System.Windows.Input.KeyboardNavigation&   DirectionalNavigation  CheckBoxStyle1' � 	Resources  Key( g� 	BeginTime) �� 
TargetName* �� TargetProperty+ O� KeyTime, O� Value- p� KeyTime. p� Value/ ]�Name0 �� ContentStringFormat1 �� ContentStringFormat2 J� RenderTransformOrigin3 �� ScaleX4 �� ScaleY5 �� AngleX6 �� AngleY7 �� Angle8 T� X9 T� Y: �� EndPoint; �� 
StartPoint< � Color= � Offset> N� ExitActions? ��Name  OnUnchecking@ N� EnterActions  
OnChecking�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     "localclr-namespace:crm_system  %�  &	        &	 L    &	 ]    &	 m    &	 ~    7  7  7  &	     &	 _    &	 �    7  7  7  7  &	 �    &		     &	     &	 �
    &	     &	     &	 %    &	 5    &	 E    &	 U    7  7  7  7  7  7  7  &	 "    7  7  7  &	     7  7  �� "  ��.���  � �� �� " ��.R��� $��Border��$��1,1,1,0q�
�������$ 4,4,0,0��$	��2,0q�._��� $ ContentSite��$��Center=�$��Center=�$��Header��$
��10,2q� P�  �@��
IsSelected . . ��� $	 Border�� � ��
 ��·�P�  �@��
IsSelected .  . ��� $	 Border�� � ��
 ������� $
 Beige���� $

 Gray���� $
 White���� "     P�  �@  AlternationIndex$ 2��. ���  � ��8   P�  �@  
IsSelected . . ���  � ��8  ��  � ��8  �� "   .���  � ��
 ���� ��  � ��
 ���� ��  � ��$ 1q��� "   .���  � ��
 �   ���  � ��$ Center=��� "   .���   �@ AlternatingRowBackground8   ��  �@ RowStyle8  ��  �@ 	CellStyle8  ��  �@ ColumnHeaderStyle8  ��   �@ HorizontalGridLinesBrush
 ��ح���  �@ VerticalGridLinesBrush
 ��ح���  �@ RowHeaderWidth$ 12Q���  � ��
 ��  ��� .���  � �� �� .R��� $��2q� . #��� ��$	 1 2R�$��1���� "  ��.���  �@�OverridesDefaultStyle . ��  �@�FocusVisualStyle# �  ��  � ��$ 12,11,12,10q���  � ��$	 100����  �@J�SnapsToDevicePixels . ��  � ��$ Center=���  � ��$ Center=���  �@��ClearTypeHint$ Enabled=���  �@��BitmapScalingMode$
 Fant=���  � ��$ 12(���  � ��$  Helvetica Arial sans-serif)���  � ��$ Normal"���  � ��# � 
 ��  � ��# �  ��  � ��# �  ��  � �� �� " ��.R��� $ buttonBorder��$ 4��
����+��$��2q�
����ح�#��z��#��z��._��� #��z��#��z��#��z��#��z�� P�  �@J�IsMouseOver . . ���  � ��$
 Bold"�P�  � �� . . ��� $	 buttonBorder�� � �� �	#FF5476E8�� $	 buttonBorder�� � ��
 ��  �P�  � {� .  . ���  � ��
 ������� $	 buttonBorder�� � ��
 ����� P�  � {� .  . ���  �@J�Opacity$ .6Q�P�  �@J�IsMouseOver . . ���  �@�Cursor$
 Hand���� "  ��.���  �@��	MaxLength$ 50����  � ��$	 Top=���  � ��
 �   ���  � ��$ LeftToRight=���  � �� �� .R��� $ Bd��#��z��#��z��#��z��$ 4,4,4,4��._��� $ PART_ContentHost�� P�  � {� .  . ��� # � ��$	 Bd�� � ���� # � �� � ��P�  � ��$
 Auto��. ���  � ��$	 100��P�  � ��$
 Auto��. ���  � ��$ 20����  � �� u� $ 2Q�$ 1Q�$ 1Q�$ 1Q� P�  �@J�	IsFocused . . ���  �@J�Effect   $ 0Q�$ 8Q�$ 	#FF1C67C7��P�  �@J�IsMouseOver . . ���  �@J�Effect   $ 0Q�$ 8Q�$ 	#FF9DBADF���� "  G�.���  � ��$	 Top=���  � ��
 �   ���  � ��$ LeftToRight=���  � �� �� .R��� $ Bd��#��z��#��z��#��z��$ 4,4,4,4��._��� $ PART_ContentHost�� P�  � {� .  . ��� # � ��$	 Bd�� � ���� # � �� � ��P�  � ��$
 Auto��. ���  � ��$	 100��P�  � ��$
 Auto��. ���  � ��$ 20����  � �� u� $ 2Q�$ 1Q�$ 1Q�$ 1Q� P�  �@J�	IsFocused . . ���  �@J�Effect   $ 0Q�$ 8Q�$ 	#FF1C67C7��P�  �@J�IsMouseOver . . ���  �@J�Effect   $ 0Q�$ 8Q�$ 	#FF9DBADF���� $
 #e3e9ef���� $

 #fff���� $

 #888���� $

 #eee���� $

 #888���� " d�.R��  �� �� $��20 �.+��� $��Border��$��2��8��  $ 0��$��
1, 1, 1, 1q�8�� �� $��ButtonBorder��$��1��$��
1, 1, 1, 1q����#444$ 
0, 0, 0, 0��$��
0, 0, 0, 0q�8�� F� $��Arrow��$��1��=����        �    �� � 	= ��[ � z �� � z     � 	=  	= 	$��Center=����#444$��Center=� P�  �@J�IsMouseOver . . ��� $	 ButtonBorder�� � ��
 �����P�  �@d�	IsChecked$
 TrueY�. ��� $	 ButtonBorder�� � ��
 ������� $	 Arrow�� � �� �	#FF8D979EP�  � {� .  . ��� $	 Border��8   � ���� $	 ButtonBorder��8   � ���� $	 ButtonBorder��8   � ���� 8   � ���� $	 Arrow�� � �� �#999�� "  ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � ��$
 Auto=���  � ��$
 Auto=���  � �� . ��  � ��
 �   ���  �@�FocusVisualStyle Z� ��  �@��MaxDropDownHeight$	 150����  � �� �� " ��.R�� .+�d� $��ToggleButton��$��2��$ Press=�|�.  8��   �� $ IsDropDownOpen� �� *TemplatedParent+$ TwoWay=��� $��ContentSite��$��5, 3, 23, 3q� .  $
��Left=�$��Center=�#��z #��z #��z���� $��PART_EditableTextBox��$��3, 3, 23, 3q�#! z  $y�Hidden=�
������ $
��Left=�$��Center=�|�. .������ " ��.R��� $��PART_ContentHost��|�.  .��� $��Popup��$��Bottom=�|�.  " . #��z# $��Slide=�.��� $��DropDown�� . #��z$ #��z% .+��� $��DropDownBorder��
�������$��
0, 1, 0, 0q�$ 0��$��1,1,1,1q�8�� �� $��4q� . .���� $& 	Contained=� P�  �@��HasItems .  . ��� $	 DropDownBorder�� � ��$ 95��P�  � {� .  . ��� 8   � ��P�  �@��
IsGrouping . . ���  � �� .  P�  �@��
IsEditable . . ���  �@ 	IsTabStop .  �� $	 PART_EditableTextBox�� � y�$ Visible=��� $	 ContentSite�� � y�$ Hidden=��� "  ��.���  � ��# � ����  � ��# � ����  � �� �� " ��' ���� 
OnChecking .��U� $( 00:00:00X�$) slider��$U* O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)�.\��� $+ 00:00:00.3000000��$, 25Q��� OnUnchecking .��U� $( 00:00:00X�$) slider��$U* O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)�.\��� $+ 00:00:00.3000000��$, 0Q�r� $( 00:00:00X�$) slider��$* (FrameworkElement.Margin)�.	��� $- 00:00:00.3000000��$. 1,1,1,1q�.R�]� $/ 	dockPanel��.+��� # z #��z��#1 z0 #��z����. $��Center=�� $��5,5,0,5q�$��50�����	#FFC0CCD9.+��� $��ON��$
��Wrap=�$
��Bold"�$��12(�$��Right=�$��0,0,3,0q��� $
��Left=�$��2,0,0,0q�$��12(�$
��Bold"�$	��OFF��$
��Wrap=��� $ slider��$
��Left=�$��23��$��1,1,1,1q�$ 3,3,3,3��$2 0.5,0.5&�$��1,1,1,1q�z�V� .~��� $3 1Q�$4 1Q��� $5 0Q�$6 0Q��� $7 0Q�T� $8 0Q�$9 0Q����� $: 0.5,1&�$; 0.5,0&�.��� $< 	#FFFFFFFF��$= 0Q�� $< 	#FF4490FF��$= 1Q����� $: 0.5,1&�$; 0.5,0&�.��� $< 	#FF8AB4FF��$= 1Q�� $< 	#FFD1E2FF��$= 0Q� P�  �@d�	IsChecked$
 TrueY�> �� $"? OnUnchecking_BeginStoryboard��8��  @ �� $ ? OnChecking_BeginStoryboard��8�� P�  � {� .  . ��� # � �� � ��