   M S B A M L   `   `   `  ����D  @MVVMDemo, Version=22.2.3.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MVVMDemo.ServicesMVVMDemo.Services  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5
      -    'MVVMDemo.Services.MessageBoxServiceView   5      h dDevExpress.Xpf.TypedStyles.v22.2, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v22.2, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aR NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089<x,http://schemas.microsoft.com/winfx/2006/xaml      5      3localclr-namespace:MVVMDemo.Services      5      ] YDevExpress.Mvvm.v22.2, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aCdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm    5      Adxe5http://schemas.devexpress.com/winfx/2008/xaml/editors   5      =dx2http://schemas.devexpress.com/winfx/2008/xaml/core   5      j	 fDevExpress.Xpf.LayoutControl.v22.2, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aHdxlc;http://schemas.devexpress.com/winfx/2008/xaml/layoutcontrol 	  5      e
 aDevExpress.Xpf.Controls.v22.2, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aEdxwui7http://schemas.devexpress.com/winfx/2008/xaml/windowsui 
  5	      2sys&clr-namespace:System;assembly=mscorlib 
  5
        � DataContext  5      1  +DevExpress.Mvvm.UI.ViewModelSourceExtension     Type$&  local:MessageBoxServiceViewModelL� � 	Resources 5      1   +MVVMDemo.Services.TimeSpanToStringConverter   TimeSpanToStringConverterD   >MVVMDemo.Services.MessageBoxButtonToMessageBoxResultsConverter 0 ,MessageBoxButtonToMessageBoxResultsConverter��%:   &	        &	        5   
   6*     5   
   6=   5      2  ,DevExpress.Mvvm.UI.Interactivity.Interaction   	Behaviors 5      -  'DevExpress.Xpf.Core.DXMessageBoxService  5   
   -   5          Name$ DXMessageBoxService��5   !      AllowTextSelection 6>   �� *AllowTextSelection+   TimerTimeout 5   !   �� *TimerTimeout+   TimerFormat 6G   �� *TimerFormat+5 
 /DevExpress.Xpf.WindowsUI.WinUIMessageBoxService  5   
   -   5          Name$ WinUIMessageBoxService��5   '   .��5      0 	 *DevExpress.Xpf.LayoutControl.LayoutControl  . 	 (DevExpress.Xpf.LayoutControl.LayoutGroup	   Orientation$	 Vertical=�6   $	��Top=�60   .+�5   
   -	 	 'DevExpress.Xpf.LayoutControl.LayoutItem	  
 	  Label$
 Text:��6    	  Content. 5      %
  DevExpress.Xpf.Editors.TextEdit
  $	��Top=�61   )  #DevExpress.Xpf.Editors.TextEditBase   Text 6   �� *Text+61   5      	  5   
   $
 Caption:��6   . 5      
  $	��Top=�64    6   �� *	Caption+64   5      	  5   
   $
 Buttons:��6   . 5       )  #DevExpress.Xpf.Editors.ComboBoxEdit  '  !DevExpress.Xpf.Editors.ButtonEdit   IsTextEditable$ FalseX�6�   %  DevExpress.Xpf.Editors.BaseEdit   	EditValue 6   �� *	Buttons+ �� UpdateSourceTrigger$ PropertyChanged=�+  %DevExpress.Xpf.Editors.LookUpEditBase   ItemsSource 6b   ,  &DevExpress.Xpf.Editors.EnumItemsSource     EnumType$ dxmvvm:MessageButtonL�6�   5!      	  5"   
   $
 Default button:��6   . 5#        $ FalseX�6�    6   �� *DefaultButton+$ PropertyChanged=� 6h   �� *	Buttons+ �� 	Converter# [ 6�   5$      	  5%   
   $
 Icon:��6   . 5&        $ FalseX�6X    6   �� *Icon+ 6:   �� *Icons+6X   5'      	  5(   
   $
 Allow text selection:��6   . 5)      &   DevExpress.Xpf.Editors.CheckEdit     	IsChecked 6   �� *AllowTextSelection+5*      	  5+   
   $
 Timer:��6   . 5,        $��5��6   $��1��6,   $ FalseX�6�   *  $DevExpress.Xpf.Editors.PopupBaseEdit   ShowSizeGrip$ FalseY�6�      SelectedItem 6<   �� *TimerTimeout+# [     Items. 5-      ��No timer��6'   ��5.      5��6    ��5/      6��6    ��50      7��6    ��51      8��6    ��52      9��6    ��53      10��6!   54      55      	  56   
   $
 Timer format:��6   . 57           DisplayMember$ Example��6      ValueMember$ Format��67   $ FalseX�6�   $ FalseY�6�    6L   �� *TimerFormat+ 6n   �� *PredefinedFormats+6�   58      	  59   
   $
 Result:��6   . 5:      �� ��6   �� *Result+5;      �� 5=   
   $��DXMessageBoxService��68   $	��Top=�6_   ��6   �� *ShowMessageCommand+.��6w   Show Standard Message Box6�   �� 5>   
   $��WinUIMessageBoxService��68   $	��Top=�6b   ��6   �� *ShowMessageCommand+.��6z   " Show WindowsUI Style Message Box6�   5?      5@      