   M S B A M L   `   `   `  ����  HospitalManagementSystem=    7HospitalManagementSystem.Pages.RecordMedicalHistoryPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        N� Title$  Record Medical History��5      .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   .+�5   
   �� $0��*Запись истории болезни��6   $��20(�62   $��0,0,0,15q�6@   �� 5   
   $��1��6    �� Orientation$ 
Horizontal=�6"   $��0,0,0,15q�6;   .+�5      �� $��Приём:��6   $��14(�6&   $��Center=�64   $	��100��6O   �� 5      -   5        $��AppointmentComboBox��5      $	��200��62   $��30��6>   $��14(�6J    �� DisplayMemberPath$ DateTime��6X    �� SelectedValuePath$ ID��6u   5      �� 5   
   $��2��6   $��0,0,0,15q�6"   .+�5      �� $��Описание:��6   $��14(�6)   $��0,0,0,5q�67   �� 5      -   5        $��DescriptionTextBox��5      $	��100��60    �� AcceptsReturn . $��14(�6R   5      �� 5   
   -   5        $��3��5      $��Сохранить��6   $	��100��6R   $��35��6^   $
��Left=�6j   5      5      