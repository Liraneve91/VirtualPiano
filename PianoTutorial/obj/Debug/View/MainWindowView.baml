   M S B A M L   `   `   `  ????H  DPianoTutorial, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:PianoTutorial.ViewPianoTutorial.View  5   	   '    !PianoTutorial.View.MainWindowView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:PianoTutorial.View     5   	     ? WindowStyle$
  None=?5   	    ? WindowState$ 	Maximized=?5       ? Topmost .  ? Left$ 0??5      
 ? Top$ 0??6    ? Title$ MainWindowView??5   	   $
??1080??6    $
??1920??6.   ??5      ?? 5   
   .??5      ?  ? Offset$	 0.0Q?6    ? Color$ 	#FF000000??6(   ? 5      $	 1.0Q?6   $ 	#FF000000??6(   5      .??5      ? $
??1080??6   $
??1920??6   .+?5   
   )   #PianoTutorial.View.PianoControlView  )   #PianoTutorial.View.PianoControlView   Loaded$ PianoControlView_Loaded??6{   $??Bottom=?6!   $
??1920??6<   $??Right=?6I   $??0,0,-207,-28q?6e   z?5      ?? 5      	 ?? ScaleX$		 0.8Q?6!   
 ?? ScaleY$	
 0.7Q?6.   6=   5      '   !PianoTutorial.View.TopToolbarView  5   
   $	??Top=?6   $
??Auto??67   $	??100??6D   6S   &    PianoTutorial.View.ChordListView  5   
   $	??Top=?6   $
??Auto??66   $
??Auto??6C   $
??Left=?6Q   $??	0,105,0,0q?6l   6?   %   PianoTutorial.View.SongListView  5   
   $	??Top=?6   $
??Auto??65   $
??Auto??6B   $
??Left=?6P   $??120,105,0,0q?6k   6?   *   $PianoTutorial.View.SettingWindowView  5!   
   $	??Top=?6"   $	??200??6:   $	??250??6F   $??Right=?6S   $??0,105,0,100q?6o   6?   5$      5&      