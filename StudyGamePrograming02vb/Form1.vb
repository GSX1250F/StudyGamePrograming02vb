Public Class Form1
    Private mIsRunning As Boolean
    Private deltatime As Double
    Private fps As Integer
    Private mTickCount As Double
    Private sw As New System.Diagnostics.Stopwatch()
    Private canvas As Bitmap      'PictureBoxに表示するためのBitmapオブジェクト作成
    Private graph As Graphics      'ImageオブジェクトのGraphicsオブジェクトを作成する
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'フォームを表示させ、ストップウォッチを開始
        Me.StartPosition = FormStartPosition.Manual
        Me.Location = New Point(50, 50)
        sw.Start()

        canvas = New Bitmap(1024, 768)      'フォームと同じ大きさの画像を作る
        graph = Graphics.FromImage(canvas)      '画像のGraphicsクラスを生成

        Initialize()
    End Sub

    Private Sub Initialize()
        mIsRunning = True
        fps = 100
        Timer1.Interval = CInt(1000 / fps)
        Timer1.Enabled = True
        mTickCount = 0

    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        deltatime = (sw.ElapsedMilliseconds - mTickCount) / 1000
        If deltatime > 0.05 Then deltatime = 0.05       'deltatime=0.05 ～　20fps

        If mIsRunning = True Then
            'ProcessInput()     ※formアプリではループ処理中のキー入力を受け付ける処理が難しい。
            UpdateGame()
            GenerateOutput()

        Else
            Shutdown()
        End If
        mTickCount = sw.ElapsedMilliseconds
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            mIsRunning = False
        End If
    End Sub
    Private Sub UpdateGame()

    End Sub
    Private Sub GenerateOutput()
        '画面を消去
        graph.Clear(Color.Black)

        'PictureBox1に表示する
        PictureBox1.Image = canvas
    End Sub
    Private Sub Shutdown()
        sw.Stop()
        Me.Close()
    End Sub
End Class
