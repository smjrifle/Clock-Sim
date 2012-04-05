' SMJRIFLE
' VB CLOCK SIM
' Ladilazine
' Sidra
Imports Microsoft.Win32
Public Class Form2
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Timer1.Enabled = True
        Label1.Text = 1
        Label3.Text = 0
        Label4.Text = 0
        Label2.Text = 0
        Button3.Show()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        Label1.Text = Label1.Text + 1
        System.Threading.Thread.Sleep(1000)
        If Label1.Text = 60 Then
            Label1.Text = 0
            Label3.Text = Label3.Text + 1
        End If
        If Label3.Text = 60 Then
            Label3.Text = 0
            Label4.Text = Label4.Text + 1
        End If
        If Label4.Text = 24 Then
            Label4.Text = 0
            Label2.Text = Label2.Text + 1
        End If
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Label1.Text = 0
        Label2.Text = 0
        Label3.Text = 0
        Label4.Text = 0

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Timer1.Enabled = False
        Button3.Hide()
        Label12.Text = Label2.Text & " days   " & Label4.Text & " hrs   " & Label3.Text & " mins   " & Label1.Text & " secs"
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label1.Text = 0
        Label2.Text = 0
        Label3.Text = 0
        Label4.Text = 0
        Label18.Text = TimeOfDay
        CheckBox2.Text = "Don't Show Minimized" & vbNewLine & "to System Tray Dialog"
        GroupBox1.BackColor = Color.Black
        GroupBox2.BackColor = Color.Black
        GroupBox3.BackColor = Color.Black
        TabPage1.BackColor = Color.Black
        TabPage2.BackColor = Color.Black
        TabPage3.BackColor = Color.Black
        TabPage4.BackColor = Color.Black
        TabPage5.BackColor = Color.Black
        Dim tone As RegistryKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\YesPiracy\Clock Sim", RegistryKeyPermissionCheck.Default)
        Dim a As String
        a = tone.GetValue("Alarm tone")
        
        If a = "OpenFileDialog1" Then
            AxWindowsMediaPlayer1.URL = ""
        ElseIf a <> "" Then
            Label25.Text = "Tone Selected"
            AxWindowsMediaPlayer1.URL = a
            AxWindowsMediaPlayer1.Ctlcontrols.stop()
        End If
        Dim time As RegistryKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\YesPiracy\Clock Sim", RegistryKeyPermissionCheck.Default)
        Dim b As String = time.GetValue("Alarm hr")
        Dim g As String = time.GetValue("Alarm min")
        Dim h As String = time.GetValue("Alarm sec")
        Dim moment As RegistryKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\YesPiracy\Clock Sim", RegistryKeyPermissionCheck.Default)
        moment.GetValue("Alarm Moment")

        If b <> "" Then
            ComboBox2.Text = b
            ComboBox3.Text = g
            ComboBox4.Text = h
        End If
        Dim c As String = moment.GetValue("Alarm Moment")
        If c <> "" Then
            If c = "AM" Then
                ComboBox1.SelectedItem = "AM"
            Else
                ComboBox1.SelectedItem = "PM"
            End If
        End If
        Dim msgbox As RegistryKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\YesPiracy\Clock Sim", RegistryKeyPermissionCheck.Default)
        Dim d As String
        d = msgbox.GetValue("Msgbox")
        If d <> 0 Or d = "" Then
            CheckBox2.Checked = False
        Else

            CheckBox2.Checked = True
        End If
        Dim startup As RegistryKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", RegistryKeyPermissionCheck.Default)
        Dim f As String
        f = startup.GetValue("YP Clock")
        If f = "" Then
            CheckBox1.Checked = False
        Else
            CheckBox1.Checked = True
        End If

        Timer3.Enabled = True

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click

        If ComboBox2.Text = "" And ComboBox3.Text = "" And ComboBox4.Text = "" Then
            MsgBox("No time Set. Please set Time.", MsgBoxStyle.Critical, "YP Clock")
        ElseIf Ofd.FileName = "" Then
            MsgBox("No Tone Selected. Please select an alarm tone!", MsgBoxStyle.Critical, "YP Clock")

        Else
            Dim button As DialogResult
            button = MsgBox("Do you want to set Alarm", MsgBoxStyle.YesNo, "YP Clock")

            Dim tone As RegistryKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\YesPiracy\Clock Sim", RegistryKeyPermissionCheck.Default)
            tone.SetValue("Alarm tone", Ofd.FileName)
            Dim time As RegistryKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\YesPiracy\Clock Sim", RegistryKeyPermissionCheck.Default)
            time.SetValue("Alarm hr", ComboBox2.Text)
            time.SetValue("Alarm min", ComboBox3.Text)
            time.SetValue("Alarm sec", ComboBox4.Text)
            Dim moment As RegistryKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\YesPiracy\Clock Sim", RegistryKeyPermissionCheck.Default)
            moment.SetValue("Alarm Moment", ComboBox1.Text)

            If button = Windows.Forms.DialogResult.Yes Then

                Timer2.Enabled = True
                MsgBox("Alarm Set", MsgBoxStyle.Information, "YP Clock")
            Else
                MsgBox("Alarm not Set", MsgBoxStyle.Information, "YP Clock")
            End If
        End If


    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click

        Ofd.Title = "YP Clock - Browse Music"
        Ofd.Filter = "Music (*.mp3)|*.mp3;"
        Ofd.FileName = ""
        Ofd.ShowDialog()
        If Ofd.FileName <> "" Then
            AxWindowsMediaPlayer1.URL = Ofd.FileName
            AxWindowsMediaPlayer1.Ctlcontrols.stop()
            Label25.Text = "Tone Selected"
        End If
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick

        Label16.Text = DateTime.Now.ToLongTimeString
        Dim alarm As String = ComboBox2.Text & ":" & ComboBox3.Text & ":" & ComboBox4.Text & " " & ComboBox1.Text
        If Label16.Text = alarm Then
            Me.Show()
            Me.WindowState = FormWindowState.Normal
            AxWindowsMediaPlayer1.Ctlcontrols.play()
            Button6.Show()


        End If
    End Sub

    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox2.Enter
        Timer3.Enabled = True
    End Sub

    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        Label13.Text = TimeOfDay
        Label18.Text = TimeOfDay
        Label16.Text = DateTime.Now.ToLongTimeString
        Dim alarm As String = ComboBox2.Text & ":" & ComboBox3.Text & ":" & ComboBox4.Text & " " & ComboBox1.Text
        If Label16.Text = alarm Then
            Me.Show()
            Me.WindowState = FormWindowState.Normal
            AxWindowsMediaPlayer1.Ctlcontrols.play()
            Button6.Show()
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        AxWindowsMediaPlayer1.Ctlcontrols.stop()
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            Dim a As RegistryKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", RegistryKeyPermissionCheck.Default)
            a.SetValue("YP Clock", CurDir() & "\Clock Sim.exe")
        Else
            Dim a As RegistryKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", RegistryKeyPermissionCheck.Default)
            a.DeleteValue("YP Clock")
        End If

    End Sub

    Private Sub Form2_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Try
            If Me.WindowState = FormWindowState.Minimized Then
                If CheckBox2.Checked = False Then
                    MsgBox("Minimized to System Tray", MsgBoxStyle.Information, "YP Clock")
                End If
                Me.WindowState = FormWindowState.Minimized
                NotifyIcon1.Visible = True
                Me.Hide()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ShowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowToolStripMenuItem.Click
        Me.Show()
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub YesPiracyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YesPiracyToolStripMenuItem.Click
        Shell("www.yespiracy.com/forum")
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox1.Checked = True Then
            Dim a As RegistryKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\YesPiracy\Clock Sim", RegistryKeyPermissionCheck.Default)
            a.SetValue("Msgbox", 1)
        Else
            Dim a As RegistryKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\YesPiracy\Clock Sim", RegistryKeyPermissionCheck.Default)
            a.SetValue("Msgbox", 0)
        End If
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Me.Show()
        Me.WindowState = FormWindowState.Normal
    End Sub
End Class
