   M S B A M L   `   `   `  ����B  >Clicker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ClickerClicker  5   	       Clicker.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Clicker     5   	   -   5          �Name$  
mainWindow��5	   &    � 
ResizeMode$ NoResize=�5   	    � Title$ Clicker��5	   	   $	��223��6    � Topmost . $	��193��6g   $	��122��6v   $	��300��6�   $	��400��6�   $��194.746��6�   .��5
      � $��0,0,0,1q�6   .+�5   
   �� -   5         ��Name$ buttonStart��5      $��Start��6&   $
��Left=�66   $��8,22,0,0q�6Q   $	��Top=�6c   $��75��6{   6�   �� 5   
   -   5         ��Name$ textBoxDelay��5      $��	75,73,0,0q�6(    �� TextWrapping$
 Wrap=�6;   $��20��6O   $	��Top=�6[   $
��Left=�6�   $��40��6�   $��52��6�   $��23��6�   �� 5   
   $
��Left=�6   $��	65,51,0,0q�6/   $
��Wrap=�6B   $	��Top=�6V   $��60��6n   $��22��6y    �� TextAlignment$ Center=�6�   $��
Delay (ms)��6�   �� 5   
   $
��Left=�6   $��8,55,0,0q�6/   $
��Wrap=�6A   $	��Top=�6U   $��20��6m   $��11��6y   $ Right=�6�   �� 6�   $�X��6�   �� 6�   $�:��6�   6�   �� 5   
   $
��Left=�6   $��
-15,73,0,0q�6/   $
��Wrap=�6C   $	��Top=�6W   $��20��6o   $��36��6{   �� 5   
   $
��Left=�6   $��8,73,0,0q�6/   $
��Wrap=�6A   $��Y:��6U   $	��Top=�6_   $��20��6w   $��11��6�   $ Right=�6�   �� 5   
   -   5        $ textBoxX��5      $��	21,53,0,0q�6$   $
 Wrap=�67   $��20��6K   $	��Top=�6W   $
��Left=�6�   $��40��6�   $��52��6�   $��20��6�   �� 5   
   -   5        $ textBoxY��5      $��	21,73,0,0q�6$   $
 Wrap=�67   $��20��6K   $	��Top=�6W   $
��Left=�6�   $��40��6�   $��52��6�   $��20��6�   �� 5   
   $r��lPress:
• Ctrl + Alt + Q - to set mouse 
coordinates
• Esc - to stop Clicker
Repeat = 0 - unlimited combo��6   $��3,93,0,0q�6�   $��92��6�   $	��Top=�6�   $	��185��6�   $
��Left=�6�   �� 5   
   -   5        $ buttonStartCombo��5      $��Combo��6+   $
��Left=�6;   $��	95,22,0,0q�6V   $	��Top=�6i   $��75��6�   6�   �� 5   
   -   5        $ textBoxRepeat��5      $��
130,73,0,0q�6)   $
 Wrap=�6=   $��20��6Q   $	��Top=�6]   $
��Left=�6�   $��40��6�   $��52��6�   $��23��6�   �� 5   
   $
��Left=�6   $��
130,51,0,0q�6/   $
��Wrap=�6C   $	��Top=�6W   $��40��6o   $��22��6z   $ Center=�6�   $��Repeat��6�   o� 5   
   $��Center=�6   $��20��6,   $	��Top=�68   $	��189��6P   ���	#FFF0F0F0.@�5      m� -   5         m�Name$ MenuItemLoadConfig��5      $��Load Config��63   6H   5      5      5      