   M S B A M L   `   `   `  ����A  =UITier, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:UITier.ViewsUITier.Views  5          UITier.Views.TopPanelView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:UITier.Views     5      .��5	      �   � RowDefinitions  5
   
   �� 5      �� 5      .+�5   
   �� $��0��6      WindowsBorder#��[  6   y�6E   �� *
IsLogged+ ' #InverseBooleanToVisibilityConverter �� 	Converter# [ ._�5      �  � ColumnDefinitions 5      �� 5      $
��Auto �6'   �� 5      $
��Auto �6'   �� 5      $��* �6'   .+�5      �� $��Log In��6   $��0��6*     	LogButton#��[ 6\   ��6:   �� *LogInCommand+6\   � 5      $��1��6   $
��10,0q�6'    5      �� 5      $
��auto �6+   �� 5      $	��100 �6+     5      �� 5      �� 5      .+�5"      �� $��0��6    $��0��60   $��5q�6=   $��User ID:��6H   �� 5#      $��1��6   $��0��6.   $��5q�6;   ��6F   �� *
IdNumber+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=��� 5$      $��0��6    $��1��60   $��5q�6=   $��	Password:��6H   �� 5%      $��1��6   $��1��6.   $��5q�6;   ��6F   �� *
Password+$ PropertyChanged=�$ TwoWay=�5&      5'      5(      �� 5*   
   $��1��6   #��[  6   y�6E   �� *
IsLogged+   BooleanToVisibilityConverter# [ ._�5+      �  5,      �� 5-      $
��Auto �6'   �� 5.      $
��Auto �6'   �� 5/      $��* �6'   �� 50      $
��Auto �6'   .+�53      �� $��Log Out��6   $��0��6+   #��[ 6]   ��6;   �� *LogOutCommand+6]   � 55      $��1��6   $��
0,10,10,10q�6'    56      �� 57      �� 58        5:      �� 5;      �� 5<      .+�5?      �� $��0��6    $��2��60   $��0��6D   $
��20,0q�6Q   $��Center=�6_   $
��User��6|   $
��Bold"�6�   �� 5@      $��0��6    $��2��60   $��1��6D   $
��20,0q�6Q   $��Center=�6_   ��5A      `� 5B       �� StringFormat$ {0} {1}��6+   .1�5C   "   ��  �� Path$# TheCurrentUser.User.FirstName�6*   �� 5D   "   $" TheCurrentUser.User.LastName�6*   5E      5G      5H      � 5K      $��3��6   $��
0,10,10,10q�6'    5L      �� 5M      �� 5N        5P      �� 5Q      �� 5R      .+�5U      �� $��0��6    $��0��60   $	��5,0q�6=   $��Center=�6J   $��State��6g   $
��Bold"�6t   �� 5V      $��0��6    $��1��60   $	��5,0q�6=   $��Center=�6J   ��6g   �� *TheCurrentUser.State+�� 5W      $��1��6    $��0��60   $	��5,0q�6=   $��Center=�6J   $��Timer��6g   $
��Bold"�6t   �� 5X      $��1��6    $��1��60   $	��5,0q�6=   $��Center=�6J   ��6g   �� *TheCurrentUser.StateCountDown+ ! TimeSpanWithoutHoursConverter# [ 5Y      5Z      5[      5\      5]      