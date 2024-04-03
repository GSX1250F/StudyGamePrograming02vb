Imports System.Numerics

Public Class Ship
    Inherits Actor
    Sub New(ByRef game As Game)
        MyBase.New(game)
        mPosition.X = 1024 / 2
        mPosition.Y = 768 / 2
        mRotation = 30 / 180 * Math.PI
        mScale = 1.5

        'アニメーションのスプライトコンポーネント作成、テクスチャ設定
        Dim asc As New AnimSpriteComponent(Me, 10)
        Dim anims As New List(Of Image)(New Image() {
            game.GetTexture("../../../Assets/Ship01.png"),
            game.GetTexture("../../../Assets/Ship02.png"),
            game.GetTexture("../../../Assets/Ship03.png"),
            game.GetTexture("../../../Assets/Ship04.png")})
        asc.SetAnimTextures(anims, True)


    End Sub


End Class
