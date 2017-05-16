Imports System
Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Timers
Imports Microsoft.VisualBasic

Public Class Form1

    'TODO: Napravit loop kroz proxy.prx (plain-text) file, i timer sa max idleom za changeanje proxya i UA-a. (Napravit droplist za popis UA, myb korisno za neke money siteove etc..)

    Dim isWebsiteLoaded = False
    Dim strFileName As String

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label4.Text = "No proxy set"
        Label4.ForeColor = Color.Crimson
    End Sub

    Public Structure Struct_INTERNET_PROXY_INFO
        Public dwAccessType As Integer
        Public proxy As IntPtr
        Public proxyBypass As IntPtr
    End Structure
    <Runtime.InteropServices.DllImport("wininet.dll", SetLastError:=True)> _
    Private Shared Function InternetSetOption(ByVal hInternet As IntPtr, ByVal dwOption As Integer, ByVal lpBuffer As IntPtr, ByVal lpdwBufferLength As Integer) As Boolean
    End Function

    Private Sub RefreshPRSettings(ByVal strProxy As String)
        Const INTERNET_OPTION_PROXY As Integer = 38
        Const INTERNET_OPEN_TYPE_PROXY As Integer = 3
        Dim struct_IPI As Struct_INTERNET_PROXY_INFO
        struct_IPI.dwAccessType = INTERNET_OPEN_TYPE_PROXY
        struct_IPI.proxy = System.Runtime.InteropServices.Marshal.StringToHGlobalAnsi(strProxy)
        struct_IPI.proxyBypass = System.Runtime.InteropServices.Marshal.StringToHGlobalAnsi("local")
        Dim intptrStruct As IntPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(System.Runtime.InteropServices.Marshal.SizeOf(struct_IPI))
        System.Runtime.InteropServices.Marshal.StructureToPtr(struct_IPI, intptrStruct, True)
        Dim iReturn As Boolean = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_PROXY, intptrStruct, System.Runtime.InteropServices.Marshal.SizeOf(struct_IPI))
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox2.Text = "" Then
            MsgBox("Please enter valid proxy path", MsgBoxStyle.Critical, "RefBOT - Proxy path not correct")
        End If
        Label4.Text = TextBox2.Text
        Label4.ForeColor = Color.Green
        ReadProxyFile()

        'Debug shit - output na potrcko.esy.es/vb/visit_info.txt '
        'RefreshPRSettings(TextBox2.Text)
        'ChangeUserAgent("Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/28.0.1468.0 Safari/537.36")
        'ChangeUserAgent("FUCK YOU HEHE XD")
        'WebBrowser1.Navigate(TextBox1.Text)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim fd As OpenFileDialog = New OpenFileDialog()
        fd.Title = "Open File Dialog"
        fd.InitialDirectory = "C:\"
        fd.Filter = "Proxy file (*.prx)|*.prx"
        fd.FilterIndex = 2
        fd.RestoreDirectory = True

        If fd.ShowDialog() = DialogResult.OK Then
            strFileName = fd.FileName
            TextBox2.Text = fd.FileName
        End If
    End Sub

    Private Sub ReadProxyFile()

        Dim reader As StreamReader = My.Computer.FileSystem.OpenTextFileReader(strFileName)
        Dim a As String

        Do
            a = reader.ReadLine
            MsgBox(a)
        Loop Until a Is Nothing

        reader.Close()


        'Dim reader = File.OpenText(fd.fileName)
        'Dim line As String = Nothing
        'Dim lines As Integer = 0

        'While (reader.Peek() <> -1)
        '    line = reader.
        'End While

    End Sub

End Class