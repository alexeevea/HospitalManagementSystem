   M S B A M L   `   `   `  ����  HospitalManagementSystem;    5HospitalManagementSystem.Pages.ViewPatientHistoryPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        N� Title$  View Patient History��5      .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5	      $��* �6   �� 5
      $��* �6   .+�5   
   �� $%��История пациента��6   $��20(�6,   $��0,0,0,15q�6:   �� 5   
   $��1��6    �� Orientation$ 
Horizontal=�6"   $��0,0,0,15q�6;   .+�5      �� $��Пациент:��6   $��14(�6(   $��Center=�66   $	��100��6Q   �� 5      -   5        $��PatientComboBox��5      $	��200��6.   $��30��6:   $��14(�6F    �� DisplayMemberPath$ FullName��6T    �� SelectedValuePath$ ID��6q   6�   5      �� 5   
   -   5        $��MedicalRecordsListView��5      $��2��61   $��0,0,0,15q�6>    �� View 5      �� 5      .P�5      �� $��ID��6%    �� Width$ 50��61    �� DisplayMemberBinding 6<   �� *ID+�� 5      $��
Приём��6%   $	 100��64    6@   �� *AppointmentID+�� 5      $��Описание��6%   $	 300��67    6C   �� *Description+5      5      �� 5   
   -   5        $��NotesListView��5      $��3��6(    5      �� 5       .P�5!      �� $��ID��6%   $ 50��61    6<   �� *ID+�� 5"      $��Заметка��6%   $	 400��66    6B   �� *
NoteText+5#      5%      5&      5'      