   M S B A M L   `   `   `  ����A  =UITier, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:UITier.ViewsUITier.Views  5      !    UITier.Views.NavigationMenu   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:UITier.Views     5      .��5	      ��    WindowsBorder#��[  6   ._�5
   
   �� $��Hidden=�6   $��15q�6<   .��5      ��   �� Orientation$  Vertical=�6   $��Center=�60   .+�5      �� $
��Home��6   $��	12,5,12,3q�6(     NavigationButton#��[ 6;   $
��Home��6e   �� 5      $��User Details��6   $��	12,5,12,3q�60   #��[ 6C   $��User Details��6m   �� 5      $��Revenues��6   $��	12,5,12,3q�6,   #��[ 6?   $��Revenues��6i   y�5      �� *+)TheCurrentUser.User.Permissions[Revenues]+   BooleanToVisibilityConverter �� 	Converter# [ 9� 5      $��Users��6     NavigationExpander#��[ 6R    9� 
IsExpanded 6*   �� *OpenExpanders[0]+y�5      �� *(&TheCurrentUser.User.Permissions[Users]+# [ .��5      �� .+�5      �� $	��Add��6!   #��[ 6/   $��User Add��6Y   �� 5      $
��List��6!   #��[ 60   $��	User List��6Z   �� 5      $
��Edit��6!   #��[ 60   $��	User Edit��6Z   5      5      9� 5      $��Halls��6   #��[ 6R    6*   �� *OpenExpanders[1]+y�5      �� *(&TheCurrentUser.User.Permissions[Halls]+# [ .��5      �� .+�5      �� $	��Add��6!   #��[ 6/   $��Hall Add��6Y   �� 5       $
��List��6!   #��[ 60   $��	Hall List��6Z   �� 5!      $
��Edit��6!   #��[ 60   $��	Hall Edit��6Z   �� 5"      $��Set Approval��6!   #��[ 68   $��Hall Approval��6�   ��6b   �� *OpenWindowCommand+6�   5#      5$      9� 5&      $��Events��6   #��[ 6S    6+   �� *OpenExpanders[2]+y�5'      �� *)'TheCurrentUser.User.Permissions[Events]+# [ .��5(      �� .+�5)      �� $	��Add��6!   #��[ 6/   $��	Event Add��6Y   �� 5*      $
��List��6!   #��[ 60   $��
Event List��6Z   �� 5+      $
��Edit��6!   #��[ 60   $��
Event Edit��6Z   �� 5,      $��
Set Demand��6!   #��[ 66   $��Event Demand��6�   ��6`   �� *OpenWindowCommand+6�   5-      5.      9� 50      $��Occurrences��6   #��[ 6X    60   �� *OpenExpanders[3]+y�51      �� *.,TheCurrentUser.User.Permissions[Occurrences]+# [ .��52      �� .+�53      �� $	��Add��6!   #��[ 6/   $��Occurrence Add��6Y   �� 54      $
��List��6!   #��[ 60   $��Occurrence List��6Z   �� 55      $
��Edit��6!   #��[ 60   $��Occurrence Edit��6Z   56      57      9� 59      $��Orders��6   #��[ 6S    6+   �� *OpenExpanders[4]+y�5:      �� *)'TheCurrentUser.User.Permissions[Orders]+# [ .��5;      �� .+�5<      �� $	��Add��6!   #��[ 6/   $��	Order Add��6Y   �� 5=      $
��List��6!   #��[ 60   $��
Order List��6Z   �� 5>      $
��Edit��6!   #��[ 60   $��
Order Edit��6Z   5?      5@      5B      5C      5D      5F      