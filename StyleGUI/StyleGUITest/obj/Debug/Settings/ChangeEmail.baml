   M S B A M L   `   `   `  ����  StyleGUITest    StyleGUITest.ChangeEmail   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5        $��ChangeEmailWindow��5        � Title$  ChangeEmail��5   	   $	��300��6   $	��300��6*    � 
ResizeMode$ NoResize=�5   	    � WindowStyle$
 None=�6    � WindowState$ Normal=�62    � WindowStartupLocation$ CenterScreen=�6G      SettingsWindow#���   5   	    � 	Resources 5	       �� Source�� 5
   
   $ /Styles/Styles.xaml?�6   %    6<   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      $��* �6   6)   .+�5   
   ]� -   5        $��0��5       J� IsHitTestVisible . .+�5      �� -   5        $��Close��5   ,   $��Right=�6   $��Center=�69   $��35��6[   $��20(�6f   $��X��5        CloseButton#��[ 6!   5      �� 5      
������  . 	 �� Stretch$
	 Fill=�6C   6T   5      � 5   
   $��1��6   $��3q�6    5      �� 5      $
��Auto �6    60   �� 5      $
��Auto �6    60   �� 5      $
��Auto �6    60   �� 5      $
��Auto �6    60   �� 5       $
��Auto �6    60   �� 5!      $
��Auto �6    60   �� 5"      $��* �6    6-   �� 5#      $
��Auto �6    60   .+�5&      �� $��0��6   {�.  .��65   Write your old email:6M   �� 5'      $��1��6   $��0,2,0,2q�6"     LoginTextBox#��[ 63   6[   �� 5)      $��2��6   {�.  .��66   Write your new email:6M   �� 5*      $��3��6   $��0,2,0,2q�6#   #��[ 64   6\   �� 5,      $��4��6   {�.  .��66   Write your password:6L   G� 5-      $��5��6   $��0,2,0,2q�6'   6:   ]� 5/      $��7��6   .+�50      �� -   5        $��OkButton��50   0   $��Right=�6   $��3,0,0,0q�6@   .��6Q   Ok6U   �� 51      -   5        $��CancelButton��51   0   $��Right=�6   .��6E   Cancel6M   �� 53      $
��Left=�6   
������ $
	 Fill=�6E   6V   55      57      58      59      