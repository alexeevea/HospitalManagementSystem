   M S B A M L   `   `   `  ����  HospitalManagementSystem7    1HospitalManagementSystem.Pages.AddMedicalNotePage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        N� Title$  Add Medical Note��5      .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   .+�5   
   �� $<��6Добавить медицинскую заметку��6   $��20(�68   $��0,0,0,15q�6F   �� 5   
   $��1��6    �� Orientation$ 
Horizontal=�6"   $��0,0,0,15q�6;   .+�5      �� $��Пациент:��6   $��14(�6(   $��Center=�66   $	��100��6Q   �� 5      -   5        $��PatientComboBox��5      $	��200��6.   $��30��6:   $��14(�6F    �� DisplayMemberPath$ FullName��6T    �� SelectedValuePath$ ID��6q   5      �� 5   
   $��2��6   $��0,0,0,15q�6"   .+�5      �� $ ��Текст заметки:��6   $��14(�6.   $��0,0,0,5q�6<   �� 5      -   5        $��NoteTextBox��5      $	��100��6)    �� AcceptsReturn . $��14(�6K   5      �� 5   
   -   5        $��3��5      $��Сохранить��6   $	��100��6I   $��35��6U   $
��Left=�6a   5      5      