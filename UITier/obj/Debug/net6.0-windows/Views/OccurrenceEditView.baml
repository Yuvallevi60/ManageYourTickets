   M S B A M L   `   `   `  ����A  =UITier, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:UITier.ViewsUITier.Views  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5	      %    UITier.Views.OccurrenceEditView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:UITier.Views     5      ] YXceed.Wpf.Toolkit.NET5, Version=4.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      0sys&clr-namespace:System;assembly=mscorlib  5	      .��5      � $
��Left=�6   $��10q�6&     � RowDefinitions  5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6    � ColumnDefinitions 5   
   �� 5      $	��240 �6   �� 5      .+�5   
   �� $��0��6   $��Edit Occurrence��6!      
FormHeader#��[  68   �� 5   
   $��1��6   $��1��6!   $��6��61     FormErrorMessage#��[ 6B   ��6l   �� *ErrorMessage+� 5    
   $��1��6   $��5q�6    5!      �� 5"      $
��auto �6#   �� 5#      �� 5$      $
��auto �6#   .+�5'      �� $��0��6   $��0,0,5,0q�6(   $��Event Date ID:��69   �� 5(      $��1��6   $	��5,0q�6&   ${�False��63   ��6E   �� *SelectedObject.Id+�� 5)      $��2��6   $��Enter��6%   ��65   �� *EnterIdCommand+5*      � 5.   
   $��2��6   $��5q�6    5/      �� 50      $
��auto �6#   �� 51      .+�54      �� $��0��6   $��0,0,5,0q�6(   $��Event:��69   �� 55      $��1��6   ${�False��6Q   ��6'   �� *SelectedObject.EventName+6Q   56      � 5:   
   $��3��6   $��5q�6    5;      �� 5<      $
��auto �6#   �� 5=      .+�5@      �� $��0��6   $��0,0,5,0q�6(   $��Hall:��69   �� 5A      $��1��6   ��6(   �� *	HallsList+ �� SelectedItem 6J   �� *SelectedHall+{�5B      �� *SelectedObject+  ObjectToBooleanConverter �� 	Converter# [ ��5C      �� 5D      .R�5E      ��  �� Orientation$ 
Horizontal=�6%   .+�5F      �� $��#��6(   �� 5G      ��6(   �� *Id+�� 5H      $	�� - ��6(   �� 5I      ��6(   �� *Name+5J      5K      5M      5N      � 5R   
   $��4��6   $��5q�6    5S      �� 5T      $
��auto �6#   �� 5U      .+�5X      �� $��0��6   $��0,0,5,0q�6(   $��Date:��69   (  "System.Windows.Controls.DatePicker  5Y      $��1��6    � Language$ en-US�6+   
 �� Now   DisplayDateStart# Z 6<      SelectedDate 6k   �� *SelectedDate+{�5Z      �� *SelectedObject+# [ 5[      � 5_   
   $��5��6   $��5q�6    5`      �� 5a      $
��auto �6#   �� 5b      .+�5e      �� $��0��6   $��0,0,5,0q�6(   $��Hour:��69   "  Xceed.Wpf.Toolkit.TimePicker  5f      $��1��6   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.DateTime, System.Runtime, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a]], System.Runtime, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a]]	   ShowButtonSpinner$	 False��6/   
   TimeInterval$
 00:30h�6h   &   Xceed.Wpf.Toolkit.DateTimeUpDown   Format$ 	ShortTime=�6}      Value 6I   �� *SelectedHour+{�5g      �� *SelectedObject+# [ 5h      �� 5l   
   $��6��6   $��	0,25,0,10q�6"   $ 
Horizontal=�65   $��Center=�6N   .+�5m      �� $
��Edit��6   ��6$   �� *EditCommand+�� 5n      $��10,0,0,0q�6   $��Reset��6'   ��67   �� *ResetCommand+5o      5q      5r      