   M S B A M L   `   `   `  ???? H  DPianoTutorial, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:PianoTutorial.ViewPianoTutorial.View  '    !PianoTutorial.View.MainWindowView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     2local clr-namespace:PianoTutorial.View       ? WindowStyle$
  None=? ? WindowState$ 	Maximized=? ? Title$ MainWindowView??$
??1080??$
??1920?????? .???  ? Offset$	 0.0Q? ? Color$ 	#FF000000??? $	 1.0Q?$ 	#FF000000??.??? .+?'   !PianoTutorial.View.TopToolbarView  $	??Top=?$
??1920??$	??100??)   #PianoTutorial.View.PianoControlView  $??Bottom=?$??Center=?$??250,0,0,-100q?z???  ?? ScaleX$	 0.9Q? ?? ScaleY$	 0.8Q?"   PianoTutorial.View.NotesView  $	??Top=?$
??1900??$	??300??$??Right=?$??	0,120,5,0q?&    PianoTutorial.View.ChordListView  $	??Top=?$
??Auto??$
??Auto??$
??Left=?$??	0,105,0,0q?%   PianoTutorial.View.SongListView  $	??Top=?$
??Auto??$
??Auto??$
??Left=?$??120,105,0,0q?*   $PianoTutorial.View.SettingWindowView  $	??Top=?$	??200??$	??250??$??Right=?$??0,105,0,100q?