   M S B A M L   `   `   `  ����A  =UITier, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:UITier.ViewsUITier.Views  5           UITier.Views.ErrorPageView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:UITier.Views     5      .��5      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   �� $��10q�6   $��
Error Page��6    $��36(�62   $
��Bold"�6@   
���  ��6R   �� 5   
   $��1��6   $��10q�6!   $
��Bold"�6-   $��Exception Type:��6?   �� 5   
   $��2��6   $��10q�6!   $
��Bold"�6-   $��Error Message:��6?   �� 5   
   $��3��6   $��10q�6!   $
��Bold"�6-   $��Stack Trace:��6?   �� 5   
   $��1��6   $��1��6!   $��10q�61   ��6=   �� *ExceptionType+�� 5    
   $��2��6   $��1��6!   $��10q�61   ��6=   �� *ErrorMessage+�� 5!   
   $��3��6   $��1��6!   $��10q�61   $
��Wrap=�6Y   ��6=   �� *
StackTrace+6Y   5"      5#      