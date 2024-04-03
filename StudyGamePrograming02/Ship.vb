Imports System.Numerics

Public Class Ship
    Inherits Actor
    Sub New(ByRef game As Game)
        MyBase.New(game)
        mPosition.X = 100
        mPosition.Y = 150
        'スプライトコンポーネント作成、テクスチャ設定
        Dim sc As New SpriteComponent(Me, 10)
        sc.SetTexture(game.GetTexture("../../../Assets/Ship01.png"))


    End Sub


End Class
