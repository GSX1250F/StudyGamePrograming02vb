Imports System.Numerics
Imports System.Reflection.Metadata.Ecma335
Imports System.Runtime.CompilerServices

Public Class Game
    'コンストラクタ
    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        'mWindow = nullptr
        'mRenderer = nullptr
        mIsRunning = True
        mUpdatingActors = False
        mWindowW = 1024
        mWindowH = 768
        Dim success = Initialize()

    End Sub

    Public Function Initialize() As Boolean
        'フォームを表示させ、ストップウォッチを開始
        Me.StartPosition = FormStartPosition.Manual
        Me.Location = New Point(50, 50)

        canvas = New Bitmap(mWindowW, mWindowH)      'PictureBoxと同じ大きさの画像を作る
        graph = Graphics.FromImage(canvas)           '画像のGraphicsクラスを生成

        LoadData()

        mTicksCount.Start()

        mTicksCountPre = mTicksCount.ElapsedMilliseconds

        Return True

    End Function
    Private Sub Game_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Initialize()
    End Sub
    'VBではLoop処理中のイベント処理はやりにくい。
    'イベントハンドラでProcessInputを実行
    'TimerでUpdateGame・GenerateOutputを実行
    'Public Sub RunLoop()
    '   While mIsRunning       
    '       ProcessInput()
    '       UpdateGame()    
    '       GenerateOutput()
    '   End While
    'End Sub


    Public Sub AddActor(actor As Actor)

    End Sub
    Public Sub RemoveActor(actor As Actor)

    End Sub
    Public Sub AddSprite(spirite As SpriteComponent)

    End Sub
    Public Sub RemoveSprite(spirite As SpriteComponent)

    End Sub

    Public Function GetTexture(filename As String) As SDL_Texture

    End Function

    Public Sub SetRunning(isrunning As Boolean)
        mIsRunning = isrunning
    End Sub

    'ゲームウィンドウの大きさ
    Public mWindowW As Integer
    Public mWindowH As Integer
    'TicksCountの一時保持用。
    Public mTicksCountPre As Long

    'Game-specific
    Public Sub AddAsteroid(ast As Asteroid)

    End Sub
    Public Sub RemoveAsteroid(ast As Asteroid)

    End Sub
    Public Function GetAsteroids() As List(Of Asteroid)()
        Return mAsteroids
    End Function

    Private Sub ProcessInput(sender As Object, keyState As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        'ESCキーでゲーム終了
        If keyState.KeyCode = Keys.Escape Then
            mIsRunning = False
        End If

        mUpdatingActors = True
        For actor As Actor = mActors
                actor.ProcessInput(keyState)
        Next
        mUpdatingActors = False
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        UpdateGame()
        GenerateOutput()
        mTicksCountPre = mTicksCount.ElapsedMilliseconds
    End Sub
    Private Sub UpdateGame()
        'デルタタイムの計算
        Dim deltaTime As Long = (mTicksCount.ElapsedMilliseconds - mTicksCountPre) / 1000
        'If deltatime > 0.05 Then deltatime = 0.05       'deltatime=0.05 ～　20fps
        'すべてのアクターを更新
        mUpdatingActors = True
        For actor As Actor = 0 To mActors.Size()
            actor.Update(deltaTime)
        Next
        mUpdatingActors = False
        '待ちアクターをmActorsに移動
        For pending As Actor = 0 To mPendingActors.Size()
            mActors.Add(pending)
        Next
        mPendingActors.Clear()
        '死んだアクターを一時配列に追加
        Dim deadActors As List(Of Actor)
        For actor As Actor = 0 To mActors.Size()
            If actor.GetState() = actor.EDead Then
                deadActors.Add(actor)
            End If
        Next
        '死んだアクターを削除
        For actor As Actor = 0 To deadActors.Size()
            Dispose(actor)
        Next


    End Sub
    Private Sub GenerateOutput()
        '画面のクリア
        graph.Clear(Color.Black)

        'すべてのスプライトコンポーネントを描画
        For sprite = SpriteComponent = 0 To mSprites.Size()
            'どっちやろ。。。？
            sprite.Draw(graph)
            sprite.Draw(canvas)
        Next

        'バッファの交換・・・不要　PictureBoxはダブルバッファがデフォルトでオン。canvas→pictureboxでよい。
        'PictureBoxに表示する
        PictureBox.Image = canvas
    End Sub
    Private Sub LoadData()
        'プレイヤーの宇宙船を作成
        Dim mShip As New Ship

        '小惑星を複数生成
        Dim numAsteroids As Integer = 20
        For i As Integer = 0 To numAsteroids - 1
            Dim mAsteroid As New Asteroid
        Next

    End Sub
    Private Sub UnloadData()

    End Sub
    'テクスチャの配列
    Private mTextures As Dictionary(Of String, SDL_Texture)
    'すべてのアクター
    Private mActors As List(Of Actor)
    'すべての待ちアクター
    Private mPendingActors As List(Of Actor)
    'すべての描画されるスプライトコンポーネント
    Private mSprites As List(Of SpriteComponent)

    'Private mWindow As SDL_Window   C++のmWindowに相当するのはForm,PictureBox
    'Private mRenderer As SDL_Renderer    C++のRendererに相当するのはCanvas,Graphics
    Private canvas As Bitmap      'PictureBoxに表示するためのBitmapオブジェクト作成
    Private graph As Graphics      'ImageオブジェクトのGraphicsオブジェクトを作成する
    'Private mTicksCount As Unit32       Stopwatchに相当？
    Private mTicksCount As New System.Diagnostics.Stopwatch()
    Private mIsRunning As Boolean
    Private mUpdatingActors As Boolean
    'Game-specific
    Private mShip As Ship
    Private mAsteroids As List(Of Asteroid)




    Private Sub Shutdown()
        mTicksCount.Stop()
        Me.Close()
    End Sub

End Class
