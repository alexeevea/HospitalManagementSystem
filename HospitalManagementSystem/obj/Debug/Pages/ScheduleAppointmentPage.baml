   M S B A M L   `   `   `  ����  HospitalManagementSystem<    6HospitalManagementSystem.Pages.ScheduleAppointmentPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        N� Title$  Schedule Appointment��5      .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $"��Запись на приём��6   $��20(�6+   $��0,0,0,15q�69   �� 5   
   $��1��6    �� Orientation$ 
Horizontal=�6"   $��0,0,0,15q�6;   .+�5      �� $��Пациент:��6   $��14(�6(   $��Center=�66   $	��100��6Q   �� 5      -   5        $��PatientComboBox��5      $	��200��6.   $��30��6:   $��14(�6F    �� DisplayMemberPath$ FullName��6T    �� SelectedValuePath$ ID��6q   5      �� 5   
   $��2��6   $ 
Horizontal=�6"   $��0,0,0,15q�6;   .+�5      �� $��	Врач:��6   $��14(�6%   $��Center=�63   $	��100��6N   �� 5      -   5        $��DoctorComboBox��5      $	��200��6-   $��30��69   $��14(�6E   $ FullName��6S   $ ID��6p   5      �� 5   
   $��3��6   $ 
Horizontal=�6"   $��0,0,0,15q�6;   .+�5      �� $��Дата и время:��6   $��14(�6-   $��Center=�6;   $	��100��6V   (  "System.Windows.Controls.DatePicker  5      -   5        $��DateDatePicker��5      $	��120��6/   $��30��6;   $��14(�6G   �� 5      -   5        $��TimeTextBox��5      $��80��6)   $��30��64   $��14(�6@   $��10,0,0,0q�6N   $��HH:mm��6`   5      �� 5    
   -   5        $��4��5       $��Сохранить��6   $	��100��6P   $��35��6\   $
��Left=�6h   5!      5"      