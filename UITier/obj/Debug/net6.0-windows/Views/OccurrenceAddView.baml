   M S B A M L   `   `   `  ����A  =UITier, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:UITier.ViewsUITier.Views  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5	      $    UITier.Views.OccurrenceAddView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:UITier.Views     5      ] YXceed.Wpf.Toolkit.NET5, Version=4.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      0sys&clr-namespace:System;assembly=mscorlib  5	      .��5      � $
��Left=�6   $��10q�6&     � RowDefinitions  5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6    � ColumnDefinitions 5   
   �� 5      $	��230 �6   �� 5      .+�5   
   �� $��0��6   $��Add Occurrence��6!      
FormHeader#��[  67   �� 5   
   $��1��6   $��1��6!   $��6��61     FormErrorMessage#��[ 6B   ��6l   �� *ErrorMessage+� 5!   
   $��1��6   $��5q�6    5"      �� 5#      $
��auto �6#   �� 5$      �� 5%      $
��auto �6#   .+�5(      �� $��0��6   $��0,0,5,0q�6(   $	��ID:��69   �� 5)      $��1��6   $	��5,0q�6&   ��63   �� *
IdNumber+ �� UpdateSourceTrigger$ PropertyChanged=��� 5*      $��2��6   $
��Rand��6%   ��64   �� *RandIdCommand+5+      � 5/   
   $��2��6   $��5q�6    50      �� 51      $
��auto �6#   �� 52      .+�55      �� $��0��6   $��0,0,5,0q�6(   $��Event:��69   �� 56      $��1��6   ��6(   �� *
EventsList+ �� SelectedItem 6K   �� *SelectedEvent+��57      �� 58      .R�59      ��  �� Orientation$ 
Horizontal=�6%   .+�5:      �� $��#��6(   �� 5;      ��6(   �� *Id+�� 5<      $	�� - ��6(   �� 5=      ��6(   �� *Name+�� 5>      $�� (��6(   �� 5?      ��6(   �� *Demand+�� 5@      $��)��6(   5A      5B      5D      5E      � 5I   
   $��3��6   $��5q�6    5J      �� 5K      $
��auto �6#   �� 5L      .+�5O      �� $��0��6   $��0,0,5,0q�6(   $��Hall:��69   �� 5P      $��1��6   ��6(   �� *	HallsList+ 6J   �� *SelectedHall+��5Q      �� 5R      .R�5S      �� $ 
Horizontal=�6%   .+�5T      �� $��#��6(   �� 5U      ��6(   �� *Id+�� 5V      $	�� - ��6(   �� 5W      ��6(   �� *Name+5X      5Y      5[      5\      � 5`   
   $��4��6   $��5q�6    5a      �� 5b      $
��auto �6#   �� 5c      .+�5f      �� $��0��6   $��0,0,5,0q�6(   $��Date:��69   (  "System.Windows.Controls.DatePicker  5g      $��1��6    � Language$ en-US�6*   
 �� Now   DisplayDateStart# Z 6;      SelectedDate 6j   �� *SelectedDate+5h      � 5l   
   $��5��6   $��5q�6    5m      �� 5n      $
��auto �6#   �� 5o      .+�5r      �� $��0��6   $��0,0,5,0q�6(   $��Hour:��69   "  Xceed.Wpf.Toolkit.TimePicker  5s      $��1��6   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.DateTime, System.Runtime, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a]], System.Runtime, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a]]	   ShowButtonSpinner$	 False��6/   
   TimeInterval$
 00:30h�6h   &   Xceed.Wpf.Toolkit.DateTimeUpDown   Format$ 	ShortTime=�6}      Value 6I   �� *SelectedHour+6}   5t      �� 5x   
   $��6��6   $��	0,25,0,10q�6"   $ 
Horizontal=�65   $��Center=�6N   .+�5y      �� $	��Add��6   ��6#   �� *
AddCommand+�� 5z      $��10,0,0,0q�6   $��Clear��6'   ��67   �� *ClearCommand+5{      5}      5~      