   M S B A M L   `   `   `  ����A  =UITier, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:UITier.ViewsUITier.Views  5      "    UITier.Views.UserDetailsView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:UITier.Views     5      .��5	      � $
��Left=�6   $��10q�6&     � RowDefinitions  5
   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6    � ColumnDefinitions 5   
   �� 5      $	��230 �6   .+�5   
   �� $��0��6   $��User Details��6!      
FormHeader#��[  65   �� 5   
   $��1��6   $
��0,10q�6    $��General Data��6.   .��5      �� .+�5      � $��5,5,0,0q�6    5      �� 5      $
��auto �6+   �� 5      .+�5"      �� $��0��6    $��0,0,5,0q�60   $��User ID:��6A   �� 5#      $��1��6   ${�False��6J   ��6.   �� *
TheUser.Id+6J   5$      � 5&      $��5,5,0,0q�6    5'      �� 5(      $
��auto �6+   �� 5)      .+�5+      �� $��0��6    $��0,0,5,0q�60   $��First Name:��6A   �� 5,      $��1��6   ${�False��6R   ��6/   �� *TheUser.FirstName+6R   5-      � 5/      $��5,5,0,0q�6    50      �� 51      $
��auto �6+   �� 52      .+�54      �� $��0��6    $��0,0,5,0q�60   $��
Last Name:��6A   �� 55      $��1��6   ${�False��6Q   ��6/   �� *TheUser.LastName+6Q   56      57      58      �� 5;   
   $��2��6   $
��0,10q�6    $��Contact info��6.   .��5<      �� .+�5=      � $��5,5,0,0q�6    5>      �� 5?      $
��auto �6+   �� 5@      .+�5B      �� $��0��6    $��0,0,5,0q�60   $��City:��6A   �� 5C      $��1��6   ${�False��6M   ��6/   �� *TheUser.City+6M   5D      � 5F      $��5,5,0,0q�6    5G      �� 5H      $
��auto �6+   �� 5I      $��3* �6+   �� 5J      $
��auto �6+   �� 5K      $��* �6+   .+�5M      �� $��0��6    $��0,0,5,0q�60   $��Street:��6A   �� 5N      $��1��6   ${�False��6S   ��6/   �� *TheUser.StreetName+6S   �� 5O      $��2��6    $��10,0,4,0q�60   $��#��6B   �� 5P      $��3��6   ${�False��6U   ��6/   �� *TheUser.StreetNumber+6U   5Q      � 5S      $��5,5,0,0q�6    5T      �� 5U      $
��auto �6+   �� 5V      .+�5X      �� $��0��6    $��0,0,5,0q�60   $��Phone:��6A   �� 5Y      $��1��6   ${�False��6T   ��6/   �� *TheUser.PhoneNumber+6T   5Z      5[      5\      �� 5_   
   $��3��6   $
��0,10q�6    $��Password��6.   .��5`      �� .+�5a      � $��4��6   $��5,5,0,0q�6$    5b      �� 5c      �� 5d      $
��auto �6+   .+�5f      �� $��0��6   $��0,0,5,0q�6.   ��6@   �� *
Password+ �� UpdateSourceTrigger$ PropertyChanged=�{�6   �� *PasswordEnabled+�� 5g      $��1��6   $��Change��6-   ��6>   �� *ChangePasswordCommand+5h      5i      5j      �� 5m   
   $��4��6   $
��0,10q�6    $��Permissions��6.   .��5n      ��  �� Orientation$ 
Horizontal=�6   .+�5o      �� .+�5p      �� $��Users��6   $��5q�6/   ${�False��6k    d� 	IsChecked 6:   �� *TheUser.Permissions[Users]+6k   �� 5q      $��Halls��6   $��5q�6/   ${�False��6k    6:   �� *TheUser.Permissions[Halls]+6k   �� 5r      $��Events��6   $��5q�60   ${�False��6m    6;   �� *TheUser.Permissions[Events]+6m   5s      �� 5t      .+�5u      �� $��Occurrences��6   $��5q�65   ${�False��6w    6@   �� *" TheUser.Permissions[Occurrences]+6w   �� 5v      $��Orders��6   $��5q�60   ${�False��6m    6;   �� *TheUser.Permissions[Orders]+6m   �� 5w      $��Revenues��6   $��5q�62   ${�False��6q    6=   �� *TheUser.Permissions[Revenues]+6q   5x      5y      5z      5{      5|      