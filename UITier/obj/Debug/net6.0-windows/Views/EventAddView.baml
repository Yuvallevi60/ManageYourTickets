   M S B A M L   `   `   `  ����A  =UITier, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:UITier.ViewsUITier.Views  5          UITier.Views.EventAddView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ] YXceed.Wpf.Toolkit.NET5, Version=4.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      &localclr-namespace:UITier.Views  5      .��5      � $
��Left=�6   $��10q�6&     � RowDefinitions  5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6    � ColumnDefinitions 5   
   �� 5      $	��230 �6   �� 5      .+�5   
   �� $��0��6   $��	Add Event��6!      
FormHeader#��[  62   �� 5   
   $��1��6   $��1��6!   $��4��61     FormErrorMessage#��[ 6`   ��6B   �� *ErrorMessage+6`   �� 5   
   $��1��6   $
��0,10q�6    $��General Data��6.   .��5      �� .+�5      � $��5,5,0,0q�6    5      �� 5       $
��auto �6+   �� 5!      �� 5"      $
��auto �6+   .+�5$      �� $��0��6    $	��ID:��60   �� 5%      $��1��6   $	��5,0q�6.   ��6;   �� *
IdNumber+ �� UpdateSourceTrigger$ PropertyChanged=��� 5&      $��2��6   $
��Rand��6-   ��6<   �� *RandIdCommand+5'      � 5)      $��5,5,0,0q�6    5*      �� 5+      $
��auto �6+   �� 5,      .+�5.      �� $��0��6    $��0,0,5,0q�60   $��Name:��6A   �� 5/      $��1��6   ��6/   �� *	EventName+$ PropertyChanged=�50      � 52      $��5,5,0,0q�6    53      �� 54      $
��auto �6+   �� 55      .+�57      �� $��0��6    $��0,0,5,0q�60   $��	Duration:��6A   "  Xceed.Wpf.Toolkit.TimePicker  58      $��1��6&   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.DateTime, System.Runtime, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a]], System.Runtime, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a]]   ShowButtonSpinner$ False��6V      TimeInterval$ 00:30h�6p      Value 66   �� *EventDuration+6p   59      5:      5;      �� 5=   
   $��2��6   $
��0,10q�6    $��Ticket Price��6.   .��5>      �� .+�5?      � $��5,5,0,0q�6    5@      �� 5A      $
��auto �6+   �� 5B      .+�5D      �� $��0��6    $��0,0,5,0q�60   $��Base:��6A   �� 5E      $��1��6   ��6.   �� *TicketPrice+$ PropertyChanged=��� 5F      $��1��6   $	��₪��6,   $��Right=�68   5G      � 5I      $��5,5,0,0q�6    5J      �� 5K      $
��auto �6+   �� 5L      .+�5N      �� $��0��6    $��0,0,5,0q�60   $��Method:��6A   �� 5O      $��1��6   ��60   �� *PricingMethodList+ �� SelectedItem 6Z   �� *SelectedMethod+5P      5Q      5R      �� 5U   
   $��3��6   $
��0,10q�6    $��Merchandise��6.   .��5V      �� .+�5W      �� $��Brochure��6   $��5q�6.    d� 	IsChecked 69   �� *HasBrochure+�� 5X      $��Poster��6   $��5q�6,    67   �� *	HasPoster+�� 5Y      $��Shirt��6   $��5q�6+    66   �� *
HasShirt+5Z      5[      �� 5^   
   $��4��6   $��	0,25,0,10q�6"    �� Orientation$ 
Horizontal=�65   $��Center=�6N   .+�5_      �� $	��Add��6   ��6#   �� *
AddCommand+�� 5`      $��10,0,0,0q�6   $��Clear��6'   ��67   �� *ClearCommand+5a      5c      5d      