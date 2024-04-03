﻿Public Class AnimSpriteComponent
    Inherits SpriteComponent
    Sub New(ByRef owner As Actor, ByVal drawOrder As Integer)
        MyBase.New(owner, drawOrder)
		mCurrFrame = 0.0
		mAnimFPS = 12
		mAnimNumBeg = 0
		mAnimNumLast = 0
	End Sub
	' フレームごとにアニメーションを更新する(componentからオーバーライド)
	Public Overrides Sub Update(deltaTime As Single)
		If mAnimTextures.Count > 0 Then
			' フレームレートとデルタタイムに基づいてカレントフレームを更新する
			mCurrFrame += mAnimFPS * deltaTime
			If mCurrFrame >= mAnimNumLast - (mAnimNumBeg) Then
				mIsAnimating = False
			Else
				mIsAnimating = True
			End If

			' ループさせないアニメーションは止める
			If mLoopFlag = False Then
				If mCurrFrame >= mAnimNumLast - (mAnimNumBeg - 1) Then
					mCurrFrame = mAnimNumLast - (mAnimNumBeg)
					mIsAnimating = False
				End If
			Else
				' 必要に応じてカレントフレームを巻き戻す
				While mCurrFrame >= mAnimNumLast - (mAnimNumBeg - 1)
					mCurrFrame -= (mAnimNumLast - (mAnimNumBeg - 1))
				End While
			End If
			' 現時点でのテクスチャを設定する
			SetTexture(mAnimTextures(Fix(mCurrFrame) + (mAnimNumBeg - 1)))
		End If
	End Sub
	' アニメーションに使うテクスチャを設定する
	Public Sub SetAnimTextures(ByRef textures As List(Of Image), loop_flag As Boolean)
		' すべての範囲をアニメーションとする
		Dim beg As Integer = 1
		Dim last As Integer = textures.Count
		SetAnimNum(beg, last, loop_flag)
		mAnimTextures = textures
		' アクティブなテクスチャを最初のフレームに設定する
		If mAnimTextures.Count > 0 Then
			SetTexture(mAnimTextures(0))
		End If
	End Sub

	' アニメーションに使うテクスチャの範囲を設定する
	Public Sub SetAnimNum(ByVal beg As Integer, ByVal last As Integer, ByVal loop_flag As Integer)
		mCurrFrame = 0.0
		mAnimNumBeg = beg
		mAnimNumLast = last
		' ループするか否かの判定
		mLoopFlag = loop_flag
	End Sub

	' アニメーションをループさせるか
	Dim mLoopFlag As Boolean

	' アニメーション中かどうか
	Dim mIsAnimating As Boolean

	' アニメーションでのすべてのテクスチャ
	Dim mAnimTextures As New List(Of Image)

	' 現在表示中のアニメーションテクスチャのための番号
	Dim mAnimNumBeg As Integer
	Dim mAnimNumLast As Integer
	' 現在表示中のフレーム
	Dim mCurrFrame As Single
	' アニメーションのフレームレート
	Dim mAnimFPS As Single

End Class
