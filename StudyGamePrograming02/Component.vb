Public Class Component
    Sub New(ByRef owner As Actor, ByVal updateOrder As Integer)
        'updateOrderが小さいコンポーネントほど早く更新される
        mOwner = owner
        mUpdateOrder = updateOrder
        mOwner.AddComponent(Me)
    End Sub

    Public Sub Update(ByVal deltaTime As Double)
        ' 各コンポーネント更新（オーバーライド可能）
    End Sub

    Public Sub ProcessInput(ByVal keyState As System.Windows.Forms.KeyEventArgs)
        ' 各コンポーネント入力処理（オーバーライド可能）
    End Sub

    Public mOwner As Actor      '所有アクター
    Public mUpdateOrder As Integer      'コンポーネントの更新順序

End Class
