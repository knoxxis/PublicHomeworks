   M S B A M L   `   `   `  ����@  <Boook, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Boook.UserControlsBoook.UserControls  5          Boook.UserControls.Item1   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:Boook.UserControls     5      -   5        $��bookItem��5	   %   .��5      � ���#E5E9EA$��0 7 0 7q�6    $��Center=�61   $	��100��6N     � ColumnDefinitions  5   
   �� 5      $��80 �6   �� 5      $	��250 �6   �� 5      $	��150 �6   �� 5      $	��110 �6   �� 5      $��40 �6   .+�5   
   �� $��0��6   $��/Images/grey_book.png��6!   $��30��6@   $��30��6K   $
��Fill=�6W   �� 5   
   $��1��6   $��SemiBold"�6U   $��16(�6k   $��Center=�6y   ��6$   ��  �� Path$ Title� �� ElementName$ bookItem��6y   �� 5   
   $��1��6   ���#909090$��11(�6i   $��Center=�6w   $��0 38 0 0q�6�   ��6$   �� $
 ISBN�$ bookItem��6�   �� 5   
   $��2��6   $��Center=�6[   $��SemiBold"�6v   $��16(�6�   ��6$   �� $ Description�$ bookItem��6�   �� 5   
   $��3��6   $��Center=�6U   $��SemiBold"�6p   $��16(�6�   ��6$   �� $ Price�$ bookItem��6�   �� 5   
   -   5         ��Name$ 	btnSelect��5      $��4��6$   ��5       �� 5!       �� 
TargetType" ��6   .�5"      ��  �� Property � �� �� Value
 ���� 63   �� 5#       � �� 5$      �� 5%       �� 
TargetType" ��6.   .R�5&   "   �� #��z��6)   $��0q�6S   ._�5'   &   �� $��Center=�67   $��Center=�6R   5(   #   5)      5+       �� Triggers 5,      P� 5-      	 P� Property	 �@J�IsMouseOver
 P� Value
 . . �5.      ��  � �� �	#FFCACACA6;   5/      51      .��53      �� $��20��6   $��/Images/chevright.png��6   $��2 0 0 0q�6>   54      56      57      