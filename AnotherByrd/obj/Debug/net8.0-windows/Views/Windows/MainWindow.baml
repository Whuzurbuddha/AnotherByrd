   M S B A M L   `   `   `  ����F  BAnotherByrd, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:AnotherByrd.Views.WindowsAnotherByrd.Views.Windows  5      *    $AnotherByrd.Views.Windows.MainWindow   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      A =Wpf.Ui, Version=3.0.0.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml  5      5windows'clr-namespace:AnotherByrd.Views.Windows  5      $
��1100��5
      $	��650��5         ApplicationBackgroundBrush  Wpf.Ui.Markup.Design    
Background#  �   5        TextFillColorPrimaryBrush   
Foreground# �  5      "  Wpf.Ui.Controls.FluentWindow!   ExtendsContentIntoTitleBar$
 True��5      #���  5         WindowBackdropType$
 Mica=�5         WindowCornerPreference$ Round=�5       � WindowStartupLocation$ CenterScreen=�5       � Title 5	      �� *ViewModel.ApplicationTitle+ �� Mode$ OneWay=�.��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   $  Wpf.Ui.Controls.NavigationView  -   5        	  Name$	 RootNavigation��5      $��1��5      $��	42,0,42,0q�5       
   FrameMargin$
 0q�5"         IsBackButtonVisible$ Visible=�5#         IsPaneToggleVisible$
 True��5$         PaneDisplayMode$ 
LeftFluent=�5&         FooterMenuItemsSource 5!      �� *ViewModel.FooterMenuItems+$ OneWay=�   MenuItemsSource 5%      �� *ViewModel.MenuItems+$ OneWay=�   ContentOverlay 5'      � 5(      .+�5)      '  !Wpf.Ui.Controls.SnackbarPresenter  -   5          Name$ SnackbarPresenter��5)   +   5*      5,      �� 5.   
   -   5         ��Name$ RootContentDialog��5/      $��0��50      $��2��51        Wpf.Ui.Controls.TitleBar  53   
   -   5          Name$ TitleBar��54         Title$ Another Byrd��55      $��0��56      %   CloseWindowByDoubleClickOnIcon$
 True��57         Icon 58        Wpf.Ui.Controls.ImageIcon  59         Source$7 1pack://application:,,,/Assets/wpfui-icon-1024.png��6   5;      5<      5=      