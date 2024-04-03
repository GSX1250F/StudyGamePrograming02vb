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

    Public Sub Draw(ByRef graph)
        If mOwner.mState <> Actor.State.EPaused Then
            Dim w = CInt(mTexWidth * mOwner.mScale)
            Dim h = CInt(mTexHeight * mOwner.mScale)
            Dim x = CInt(mOwner.mPosition.X - w / 2)
            Dim y = CInt(mOwner.mPosition.Y - h / 2)
            Dim bmp As New Bitmap(w, h)
            bmp = mTexture
            'mOwner.mRotationに対する回転処理が必要。。。。。

            graph.DrawImage(mTexture, x, y)
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
