   M S B A M L   `   `   `  ���� Q  MWpfApplicationTutorial, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:UASigner.WpfApp.ViewModelUASigner.WpfApp.ViewModel  =#clr-namespace:UASigner.WpfApp.ModelUASigner.WpfApp.Model  )    #UASigner.WpfApp.View.AddProfileView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     =	viewmodel'clr-namespace:UASigner.WpfApp.ViewModel     5model#clr-namespace:UASigner.WpfApp.Model     Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/    � Title$  Add/Edit Profile��$	��850��$	��500��$	��500��$	��500�� � 	Resources B  <UASigner.WpfApp.ViewModel.SelectedLocationTypeToVisConverter   locationToBoolConverter  	BoolToVis;   5UASigner.WpfApp.ViewModel.LocationTypeToIconConverter  locationToIconConverter��%-   &	        &	      &	 
       ��   .��]� .+��� $��10q�$	��Top=�.+�]� $��0 0 0 20q�.+��� $	��Top=�  HeaderStyle#��[ .��Input�� $��Bottom=�$��0 0 0 0q�  ContentStyle#��[ ._��  � ColumnDefinitions �� $	��150 ��� $��* �.+��� .+�� $��0 0 0 0q� �� $	��150 ��� $��* �.+��� $��0q�
�������$��0��._���  �� Orientation$ 
Horizontal=�.+�� $��Center=�.+��� $��Type:��  	LabelText#��[ �� $��1��$ 
Horizontal=�$��Center=�$��2 5 2 5q�.+��� $��0 0 0 0q�$	��100������ *AvilableLocationTypes+ �� SelectedItem �� *SelectedInLocationType+���� .R�� .+��� ����  �� Path$ LocationTypeName��� $��1��.+���   VisibleAnimation#��[ y�`�  `� 	Converter# [  (   "UASigner.WpfApp.Model.LocationType   	DIRECTORY `� ConverterParameter# Z .1��� $ SelectedInLocationType�.+�� $��0 0 0 0q� �� $��70 ��� $��* �	 � RowDefinitions	 �� $��* ��� .+��� $��0��$��
Directory:��#��[ �� $��1��$ Vertical=�$��0 5 0 5q�.+��� $��Center=�  validationErrorTemplate
 =� ErrorTemplate#
 [ ���� *Profile.InLocationAccess.Path+" �� ValidatesOnNotifyDataErrors . � $��Right=�.+��� $��Right=�  ShowFolderButton#��[ $��InLocationAccess������ *
FolderOpen+�� $��1��$��	FileMask:��$��0��#��[ �� $��1��$��1��$��Center=�$��0 5 0 5q����� *#!Profile.InLocationAccess.FileMask+�� $	��Top=�#��[ y�`� # [  
   FTP# Z .1��� $ SelectedInLocationType�.+�� $��0 0 0 0q� �� $	��100 ��� $��* �	 �� $��* ��� .+��� $��FtpAddress:��$��0��#��[ �� $��1��$��Center=�#
 [ $��0 5 0 15q����� *" Profile.InLocationAccess.Address+�� $��Port:��$��0��$��1��#��[ �� $��1��$��1��$��Center=�#
 [ $��0 5 0 15q����� *Profile.InLocationAccess.Port+ �� TargetNullValue$  ��]� $��0 0 0 20q�.+��� $	��Top=�#��[ .��Output�� #��[ $��Bottom=�._��� $��0 5 0 0q�.+��� y��� $ ShowOutputGrid� �� 	Converter# [ .+��� $%��there are no items on this list��#��[ y��� $ HasEmptyOutput�# [ �� #��[ y��� $ HasOutputElements�# [ .+��� $��Stretch=����� $$ Profile.OutLocationsCollection����� .R�]� ��. .+�� $	��Top=�$��30�����	#FFB4CDEC �� $��50 ��� $��* �.+��� $��0�����	#FF8FA9E0$��0 0 0 1q�$
��Type���� $��1�����	#FF8FA9E0$��0 0 0 1q�$��Description���� ���� .R�� $	��0,2q�
 � Tag �� *DataContext+ �� RelativeSource ��  �� AncestorType" �� �� $��50 ��� $��* � � ContextMenu ��  � DataContext �� $ PlacementTarget.Tag� �� *Self+.@�m� $��Remove�� m� Command �� *OutputLocationDelete+ m� CommandParameter �� *PlacementTarget.DataContext+ �� $ ContextMenuL�.+��� $��16��$
��Left=�����  �� 	UriSource �� # [ �� $��1������ *DisplayName+� $��0 5 0 0 q�$��Right=�.+��� $	��Add�� 	 
LinkButton#��[	 ���� *AddOutputOpen+�� $��0 2 0 0q�#��[ y��� $ ShowAddOutputPanel�# [ .+�� $��0 0 0 0q� �� $	��150 ��� $��* �	 �� $��* ��� .+��� $��0q�
�������$��0��._��� $ 
Horizontal=�.+�� $��0��.+��� $��Type:���� .+��� $��0 5 0 5q����� *AvilableLocationTypes+ �� *SelectedOutLocationType+���� .R�� .+��� ���� $ LocationTypeName��� $��1��.+��� #��[ y�`� # [  # Z .1��� $ SelectedOutLocationType�.+�� $��0 0 0 0q� �� $��70 ��� $��* �	 �� $��* ��� .+��� $��0��$��
Directory:��#��[ �� $��1��$ Vertical=�$��0 5 0 5q�.+��� $��Center=�#
 [ ���� *OutLocationAccess.Path+� $��Right=�.+��� $��Right=�#��[ $��OutLocationAccess������ *
FolderOpen+�� $��1��$��	FileMask:��$��0��#��[ �� $��1��$��1��$��Center=�#
 [ $��0 5 0 5q����� *OutLocationAccess.FileMask+�� $	��Top=�#��[ y�`� # [  # Z .1��� $ SelectedOutLocationType�.+�� $��0 0 0 0q� �� $	��100 ��� $��* �	 �� $��* ��� .+��� $��FtpAddress:��$��0��#��[ �� $��1��$��Center=�#
 [ $��0 5 0 15q����� *OutLocationAccess.Address+�� $��Port:��$��0��$��1��#��[ �� $��1��$��1��$��Center=�#
 [ $��0 5 0 15q����� *OutLocationAccess.Port+$  ���� $��1��$��2��$ Vertical=�$��Stretch=�$��0 15 0 0q�.+��� $��Stretch=�
�������$��1���� $ 
Horizontal=�$��Right=�$��0 10 0 0q�.+�� $��Right=�.+��� $��Cancel��#��[	 ���� *LocationOutAddCancel+� $��5 0 0 0 q�$��Right=�.+��� $
��Save��#��[	 ���� *LocationOutAdd+�  � Orientation$ 
Horizontal=�$	��420��$
��Left=�$��1��.+��� $��1��#
 [ $
��Wrap=� �� $$ Profile.OutLocationsCollection��  �� $��* ��� $��* �.+�]� $��0 0 5 20q�.+��� #��[ $	��Top=�.��
Key select�� $��Bottom=�#��[ ._��� .+��� $��0q����� *PkInfoKeysSelection+���� .R��� $ 
Horizontal=�$��Center=�.+��� $��Center=� d� 	IsChecked �� *	IsChecked+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� $��Center=����� *DisplayName+� $ 
Horizontal=�$	��220��$
��Left=�$��1��.+��� $��1��#
 [ $
��Wrap=� �� $  Profile.UsedKeysCollection�]� $��5 0 0 20q�$��1��.+��� #��[ $	��Top=�.��TimeStamp Servers�� $��Bottom=�#��[ ._��� .+��� $��0q� �� SelectionMode$ Single=����� *TimestampServerCollection+���� .R��� $ 
Horizontal=�$��Center=�.+��� $��Center=� �� *	IsChecked+$ TwoWay=��� $��Center=����� *DisplayName+� $ 
Horizontal=�$	��220��$
��Left=�$��1��.+��� $��1��#
 [ $
��Wrap=� �� $! Profile.UsedTimestampServer��� $��0 0 0 20q�.+��� .+��� #��[ .��Sign Options�� $��0 10 0 0q�.+��  �� $��* ��� $��* �.+��� $��0��$��0q�.+��� $��0 0 1 0q�
�������._��� .+�� .+��� .��	Profile�� $��0 0 0 5q����� *CadesProfilesCollection+ �� *SelectedCadesProfile+���� .R�� .+��� ���� $ DisplayName��� $��5 0 5 0q�.+��� $
��Wrap=�$��12(�$��Italic$����� *SelectedCadesProfile.ToolTip+�� $��1��$��5q�.+�� $	��100�� �� $��* ��� $��30 �	 �� $��* ��� $��* ��� $��* �.+�� .+��� $��Add certificate��� $��1��.+���  �� *Profile.AddCertificate+� $��0��$��1��.+��� $��Add content TimeStamp��$
��Wrap=�� $��1��$��1��.+���  �� *Profile.AddContentTimeStamp+� $��0��$��2��.+��� $��Add validation data��� $��1��$��2��.+���  �� *Profile.AddValidationData+�� $��Bottom=�.+��� .+��� $��Stretch=�
�������$��1��]� .+�� $��Right=�.+��� $��Cancel��$��0 5 0 0q�#��[	 ���� *Cancel+����  �� *FindAncestor+$ WindowL��� $
��Save��$��0 5 0 0q� 
 OkButton#��[
 ���� *Save+����  �� *FindAncestor+$ WindowL��� $
��Left=�$��Stretch=�.+��� #
 [  �� $ Profile�� $��Right=�.+�"  FontAwesome.WPF.ImageAwesome     Icon$ Spinner=�   Spin .    SpinDuration$ 4Q�$��16��