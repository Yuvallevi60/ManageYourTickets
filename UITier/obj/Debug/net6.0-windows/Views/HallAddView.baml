   M S B A M L   `   `   `  ����A  =UITier, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:UITier.ViewsUITier.Views  5          UITier.Views.HallAddView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:UITier.Views     5      ] YXceed.Wpf.Toolkit.NET5, Version=4.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      .��5      � $
��Left=�6   $��10q�6&     � RowDefinitions  5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6    � ColumnDefinitions 5   
   �� 5      $	��230 �6   �� 5      .+�5   
   �� $��0��6   $��0��6!   $��Add New Hall��61      
FormHeader#��[  6E   �� 5   
   $��1��6   $��1��6!   $��5��61     FormErrorMessage#��[ 6`   ��6B   �� *ErrorMessage+6`   �� 5    
   $��1��6   $
��0,10q�6    $��General Data��6.   .��5!      �� .+�5"      � $��5,5,0,0q�6    5#      �� 5$      $
��auto �6+   �� 5%      �� 5&      $
��auto �6+   .+�5(      �� $��0��6    $��0,0,5,0q�60   $��Hall ID:��6A   �� 5)      $��1��6   $	��5,0q�6.   ��6;   �� *
IdNumber+ �� UpdateSourceTrigger$ PropertyChanged=��� 5*      $��2��6   $
��Rand��6-   ��6<   �� *RandIdCommand+5+      � 5-      $��5,5,0,0q�6    5.      �� 5/      $
��auto �6+   �� 50      .+�52      �� $��0��6    $��0,0,5,0q�60   $��
Hall Name:��6A   �� 53      $��1��6   ��6/   �� *
HallName+$ PropertyChanged=�54      � 56      $��5,5,0,0q�6    57      �� 58      $
��auto �6+   �� 59      .+�5;      �� $��0��6    $��0,0,5,0q�60   $��
Hall Type:��6A   �� 5<      $��1��6   ��60   �� *HallTypesList+ �� SelectedItem 6V   �� *SelectedType+5=      � 5?      $��5,5,0,0q�6    5@      �� 5A      $
��auto �6+   �� 5B      $��* �6+   �� 5C      $
��auto �6+   �� 5D      $��* �6+   .+�5F      �� $��0��6    $��0,0,5,0q�60   $��Lines:��6A   �� 5G      $��1��6   ��6.   �� *LinesNumber+$ PropertyChanged=��� 5H      $��2��6    $��5,0,5,0q�60   $��Seats In Line:��6A   �� 5I      $��3��6   ��60   �� *SeatsInLine+$ PropertyChanged=�5J      5K      5L      �� 5N   
   $��2��6   $
��0,10q�6    $��Address��6.   .��5O      �� .+�5P      � $��5,5,0,0q�6    5Q      �� 5R      $
��auto �6+   �� 5S      .+�5U      �� $��0��6    $��0,0,5,0q�60   $��City:��6A   �� 5V      $��1��6   ��6/   �� *City+$ PropertyChanged=�5W      � 5Y      $��5,5,0,0q�6    5Z      �� 5[      $
��auto �6+   �� 5\      $��3* �6+   �� 5]      $
��auto �6+   �� 5^      $��* �6+   .+�5`      �� $��0��6    $��0,0,5,0q�60   $��Street:��6A   �� 5a      $��1��6   ��6/   �� *
StreetName+$ PropertyChanged=��� 5b      $��2��6    $��10,0,2,0q�60   $��#��6B   �� 5c      $��3��6   ��6/   �� *StreetNumber+$ PropertyChanged=�5d      5e      5f      �� 5i   
   $��3��6   $
��0,10q�6    $��Activity time��6.   .��5j      �� .+�5k      �   5l      �� 5m      �� 5n       5p      �� 5q      $
��auto �6+   �� 5r      .+�5t      �� $��0��6    $��0��60   $��5q�6=   $��Opening:��6H   "  Xceed.Wpf.Toolkit.TimePicker  5u      $��1��6&   $��0��66   $��5q�6C   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.DateTime, System.Runtime, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a]], System.Runtime, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a]]   ShowButtonSpinner$ False��6N      TimeInterval$ 00:30h�6h      Value 6}   �� *OpeningHour+�� 5w      $��0��6    $��1��60   $��5q�6=   $��Closing:��6H     5x      $��1��6&   $��1��66   $��5q�6C   $ False��6N   $ 00:30h�6h    6}   �� *ClosingHour+5y      5z      5{      �� 5}   
   $��4��6   $
��0,10q�6    $��
Facilities��6.   .��5~      �� .+�5      �� $��Parking��6   $��5q�6-    d� 	IsChecked 68   �� *
HasParking+�� 5�      $��	Cafeteria��6   $��5q�6/    6:   �� *HasCafeteria+�� 5�      $��Restroom��6   $��5q�6.    69   �� *HasRestroom+5�      5�      �� 5�   
   $��5��6   $��0,10,0,0q�6"    �� Orientation$ 
Horizontal=�64   $��Center=�6M   .+�5�      �� $	��Add��6   ��6#   �� *
AddCommand+�� 5�      $��10,0,0,0q�6   $��Clear��6'   ��67   �� *ClearCommand+5�      5�      5�      