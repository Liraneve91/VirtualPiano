   M S B A M L   `   `   `  ????H  DPianoTutorial, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:PianoTutorial.ViewPianoTutorial.View  5   	   A%clr-namespace:PianoTutorial.ViewModelPianoTutorial.ViewModel  5   	   *    $PianoTutorial.View.SettingWindowView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:PianoTutorial.View     5   	   ;	viewModel%clr-namespace:PianoTutorial.ViewModel     5   	   $	??250??5	   	   $	??200??6     ? 	Resources  5          BooleanToVisibilityConverter  BottomPanelColorBrush ?? Color  RadioButtonColorBrush??%x   &	        &	      &	 6     ?? 5   
   6'   ?? 5   
   $ 	#FF303335??68   ?? 5   
   $ 
WhiteSmoke??68   5      .??5      ? ???#252525y?6    ?? *	IsVisible+ ?? 	Converter# [   ? RowDefinitions 5   
   ?? 5      $??80 ?6   ?? 5      $??80 ?6   ?? 5      $??80 ?6   .+?5   
   ?? ???#252525
???????$??0??6>   $??70??6K   $??Settings??6W   $??30(?6j     RoundedButton#???  6x   ?? 5   
   ???#252525
???????$??1??6>   $??70??6K   $??About??6W   $??30(?6g   #???  6u   ?? 5   
   ???#252525
???????$??2??6>   $??70??6K   $
??Exit??6W   $??30(?6f   #???  6?   ??6t   ?? *Close+6?   5      5      