Imports Windows.Win32.UI.Input

Public Class SpriteComponent
    Inherits Component
    Sub New(ByRef owner As Actor, ByVal drawOrder As Integer)
        MyBase.New(owner, drawOrder)
        'mTexture = Nothing
        mDrawOrder = drawOrder
        mTexWidth = 0
        mTexHeight = 0
        mOwner.mGame.AddSprite(Me)
    End Sub

    Public Sub Draw(ByRef mRenderer)
        If mOwner.mState <> Actor.State.EPaused Then
            Dim w = CInt(mTexWidth * mOwner.mScale)
            Dim h = CInt(mTexHeight * mOwner.mScale)
            Dim x = CInt(mOwner.mPosition.X - w / 2)
            Dim y = CInt(mOwner.mPosition.Y - h / 2)
            Dim img As New Bitmap(w, h)
            img = mTexture

            Dim d = mOwner.mRotation
            '新しい座標位置を計算する
            Dim x1 As Single = x + w * CType(Math.Cos(-d), Single)
            Dim y1 As Single = y + w * CType(Math.Sin(-d), Single)
            Dim x2 As Single = x - h * CType(Math.Sin(-d), Single)
            Dim y2 As Single = y + h * CType(Math.Cos(-d), Single)
            'PointF配列を作成
            Dim destinationPoints() As PointF = {New PointF(x, y),
                    New PointF(x1, y1),
                    New PointF(x2, y2)}

            mRenderer.DrawImage(img, destinationPoints)
        End If

    End Sub
    Public Sub SetTexture(tex As Image)
        mTexture = tex
        mTexWidth = tex.Width
        mTexHeight = tex.Height
    End Sub
    Public mTexture As Image
    Public mDrawOrder As Integer
    Public mTexWidth As Integer
    Public mTexHeight As Integer

End Class
