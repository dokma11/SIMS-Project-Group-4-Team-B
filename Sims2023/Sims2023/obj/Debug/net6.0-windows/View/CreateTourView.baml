   M S B A M L   `   `   `  ����C  ?Sims2023, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Sims2023.ViewSims2023.View  5   	   /clr-namespace:Sims2023.ModelSims2023.Model  63   "    Sims2023.View.CreateTourView   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Sims2023.View     5   	   .modelclr-namespace:Sims2023.Model     63     � Title$  CreateTourView��5   	   $	��450��6    $	��800��6-    � WindowStartupLocation$ CenterScreen=�69   .��5	      �  � RowDefinitions 5
   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      �� 5      $	��600 �6   �� 5      .+�5   
   �� $��0��6   $��0��6   .��6-   Naziv64   �� 5   
   $��1��6   $��0��6   .��6-   Lokacija Grad6<   �� 5   
   $��1��6   $��300,43,190,43q�6    $��3��67   .��6H   Drzava6P   �� 5   
   $��2��6   $��0��6   .��6-   Opis63   �� 5   
   $��3��6   $��0��6   .��6-   Jezik64   �� 5    
   $��2��6   $��1��6   $��238,43,222,43q�6-   $��3��6D   .��6U   Maks broj gostiju6h   �� 5!   
   $��4��6   $��1��6   $��92,10,292,33q�6-   $��2��6C   .��6T    Dodajte ih po redosledu posete6t   �� 5"   
   $��5��6   $��0��6   .��6-   Kljucne tacke6<   �� 5#   
   $��6��6   $��0��6   .��6-   Datum i vreme pocetka6D   �� 5$   
   $��7��6   $��0��6   .��6-   
Trajanje67   �� 5%   
   $��8��6   $��0��6   .��6-   Slike64   �� 5&   
   -   5        $��nameTextBox��5&      $��0��6%   $��1��62   $	��400��6B   ��6N   ��  �� Path$ 	Tour.Name� �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnDataErrors$
 True��6�   �� 5'   
   -   5        $��cityTextBox��5'      $��1��6%   $	��150��65   $��100,43,350,43q�6�   $��3��6�   ��6A   �� $ Location.City�$ PropertyChanged=�$
 True��6�   �� 5(   
   -   5        $��countryTextBox��5(      $��1��6(   $	��150��68   $��350,43,100,43q�6�   $��3��6�   ��6D   �� $ Location.Country�$ PropertyChanged=�$
 True��6�   �� 5)   
   -   5        $��descriptionTextBox��5)      $��2��6,   $��1��69   $	��400��6I   ��6U   �� $ Tour.Description�$ PropertyChanged=�$
 True��6�   �� 5*   
   -   5        $��languageTextBox��5*      $��2��6*   $��1��67   $	��100��6G   $��100,43,400,43q�6�   $��3��6   �� Text 6S   �� $ Tour.GuideLanguage�$ PropertyChanged=�$
 True��.@�5+      �� .��6   	English6$   �� 5,      .��6   	Serbian6$   �� 5-      .��6   German6#   �� 5.      .��6   French6#   �� 5/      .��6   	Spanish6$   �� 50      .��6   	Italian6$   �� 51      .��6   	Chinese6$   �� 52      .��6   
Japanese6%   53      �� 54   
   -   5        $��maxGuestNumberTextBox��54      $��2��6/   $��1��6<   $	��150��6L   $��350,43,100,43q�6�   $��3��6�   ��6X   �� $ Tour.MaxGuestNumber�$ PropertyChanged=�$
 True��6�   �� 56   
   -   5        $��keyPointTextBox��56      $��4��6)   $��1��66   $��100,43,355,0q�6F   $��2��6\   6m   �� 57   
   -   5        $��keyPointsOutput��57      $��4��6)   $��1��66   $��	355,7,0,7q�6F   $��2��6Y   ��6j   �� *Tour.KeyPointsString+�� 58   
   -	   5        $��addKeyPointsButton��58      $��4��6@   $��1��6M   $��250,43,250,0q�6]   $��2��6s   .��6�   +6�   �� 59   
   -
   5        $��dateTimeTextBox��59   �   $��5��6   $��1��6   $��100,43,267,43q�6�   $��3��6�   ��6/   �� $ 
Tour.Start�$ PropertyChanged=�$
 True��6�   �� 5:   
   -   5        $��addMoreDatesButton��5:      $��5��6@   $��1��6M   $��425,43,50,43q�6]   $��3��6s   .��6�   +6�   �� 5;   
   -   5        $��lengthTextBox��5;      $��7��6'   $��1��64   $��100,0,350,0q�6�   ��6D   �� $ Tour.Length�$ PropertyChanged=�$
 True��6�   �� 5<   
   -   5        $��picturesTextBox��5<      $��8��6)   $��1��66   $	��400��6F   ��6R   �� $ Tour.PicturesString�$ PropertyChanged=�$
 True��6�   �� 5=   
   -   5        $��submitButton��5=      $��9��6@   $��1��6M   $	��100��6]   .��6i   Submit6q   �� 5>   
   -   5        $��9��5>   ,   $��1��69   $��70��6I   $��400, 0, 0, 0q�6T   .��6j   Cancel6r   5?      5@      