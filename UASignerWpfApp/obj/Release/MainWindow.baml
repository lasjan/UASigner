   M S B A M L   `   `   `  ���� Q  MWpfApplicationTutorial, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:UASigner.WpfApp.SubTabsUASigner.WpfApp.SubTabs  G(clr-namespace:UASigner.WpfApp.PropertiesUASigner.WpfApp.Properties       UASigner.WpfApp.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     9tabData%clr-namespace:UASigner.WpfApp.SubTabs     :local(clr-namespace:UASigner.WpfApp.Properties       � Title$  
MainWindow��$	��390��$	��625��
���z���$��2q� � 	Resources ��%    .��� -   $��baseGrid��
������� � RowDefinitions �� $��40 ��� .+�� $��Right=�.+��� -   $��ComboBox_Language��$��Right=� �� SelectedValue$ en-US��$��10q����� .R�� .+��� ����  �� Path$
 Icon��� $��1��.@���    langRes.lbl_settings#���   .���  � ColumnDefinitions �� $	��77* ��� $	��55* ��� $
��172* ��� $
��303* �.+��� $��3��.+��� -   $��YourListBox��$��20q�$	��200������ .R���  �� Orientation$ 
Horizontal=� � ContextMenu �� .@�m� -   $
��Edit�� m� CommandParameter �� $ Id�m� -   $��Remove�� �� $ Id�.+��� ���� 	 �� 	UriSource	 �� $
 Icon�
 � ToolTip
 �� .+��� $��Light"�$��10(�$��0,0,0,5q����� $ ToolTip��� ���� $ Id��� �� 
��� � �$
��Bold"�.�:�� ���� $ DispName�� .+��� -   $��
20 0 20 0 q�$	��Add���� $��3��.+��� .+��� -   $��	PKListBox��$��20q�$	��200������ .R��� $ 
Horizontal=�.+��� ���� 	 �� $
 Icon�
 �� .+��� $��Light"�$��10(�$��0,0,0,5q����� $ ToolTip��� ���� $ Id��� �� 
��� � �$
��Bold"�.�:�� ���� $ DispName�� .+��� -   $��ButtonAdd_PKInfo��$��
20 0 20 0 q�$	��Add���� $��Process��.��(   "UASigner.WpfApp.SubTabs.ProcessTab  �� $��Details��.���� $��10q�.+��� .@��� .��ComboBox Item #1��  �� 
IsSelected . .��ComboBox Item #2�� .��ComboBox Item #3