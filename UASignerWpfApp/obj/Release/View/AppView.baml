   M S B A M L   `   `   `  ���� Q  MWpfApplicationTutorial, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:UASigner.WpfApp.ViewModelUASigner.WpfApp.ViewModel  "    UASigner.WpfApp.View.AppView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     =	viewmodel'clr-namespace:UASigner.WpfApp.ViewModel     -     �Name$
  root�� � Title$ AppView��$	��700��$	��500�� � 	Resources 9  3UASigner.WpfApp.ViewModel.PkInfoTypeToIconConverter   pkInfoToIconConverter8  2UASigner.WpfApp.ViewModel.TimestampToIconConverter  tsToIconConverter  	BoolToVis��%-   &	        &	      &	 
         �� .���  � ColumnDefinitions �� $��* ��� $��* � � RowDefinitions �� $	��200 ��� $��* �.+��� $��2��$��0��.+�]� $��5q�.+��� $	��Top=�  HeaderStyle#��[ .��
Profiles��   ContentStyle#��[ $��Bottom=�._��� .+��� $��20q�$	��100������  �� Path$ ProfileList����� .R���  �� Orientation$ 
Horizontal=�
 � Tag �� *DataContext+ �� RelativeSource �� 	 �� AncestorType"	 ��
 � ContextMenu
 ��  � DataContext �� $ PlacementTarget.Tag� �� *Self+.@�m� $
��Edit�� m� Command �� *ProfileItemEdit+ m� CommandParameter �� *PlacementTarget.DataContext+ �� $	 ContextMenuL�m� $��Remove�� �� *ProfileItemDelete+ �� *PlacementTarget.DataContext+ �� $	 ContextMenuL�.+��� ���� $ DisplayName�� $��Right=�.+��� $	��Add��  
LinkButton#��[ ���� *ProfileItemAdd+�� $��0��$��1��.+�]� $��5q�.+��� $	��Top=�#��[ .��Keys�� #��[ $��Bottom=�._��� .+��� $��20q�$	��100������ $ 
PkInfoList����� .R��� $ 
Horizontal=� �� *DataContext+ �� "	 ��
 ��  �� $ PlacementTarget.Tag� �� *Self+.@�m� $
��Edit�� �� *KeyItemEdit+ �� *PlacementTarget.DataContext+ �� $	 ContextMenuL�m� $��Remove�� �� *KeyItemDelete+ �� *PlacementTarget.DataContext+ �� $	 ContextMenuL�.+��� ����  �� 	UriSource ��  �� 	Converter# [  �� ���� $ Alias�� $��Right=�.+��� $	��Add��#��[ ���� *
KeyItemAdd+�� $��1��$��1��.+�]� $��5q�.+��� $	��Top=�#��[ .��Timestamp Servers�� #��[ $��Bottom=�._��� .+��� $��20q�$	��100������ $ TimestampServerList����� .R��� $ 
Horizontal=� �� *DataContext+ �� "	 ��
 ��  �� $ PlacementTarget.Tag� �� *Self+.@�m� $
��Edit�� �� *
TsItemEdit+ �� *PlacementTarget.DataContext+ �� $	 ContextMenuL�m� $��Remove�� �� *TsItemDelete+ �� *PlacementTarget.DataContext+ �� $	 ContextMenuL�.+��� ����  �� # [ �� ���� $	 Url�� $��Right=�.+��� $	��Add��#��[ ���� *	TsItemAdd+