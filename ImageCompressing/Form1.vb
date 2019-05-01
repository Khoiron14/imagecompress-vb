Imports System.Drawing.Imaging
Imports System.IO

Public Class Form1
    Private Sub compressImg(intPercent As Integer) '100 percent to intPercent
        Dim intX, intY As Integer
        intX = Int(PictureBox1.Image.Width / 100 * intPercent)
        intY = Int(PictureBox1.Image.Height / 100 * intPercent)
        Dim bm As Bitmap = New Bitmap(intX, intY)
        Dim g As Graphics = Graphics.FromImage(bm)

        g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic

        g.DrawImage(PictureBox1.Image, 0, 0, intX, intY)
        PictureBox2.Image = bm
    End Sub

    Private Function getSize(pb As PictureBox)
        Dim ms = New MemoryStream()
        pb.Image.Save(ms, ImageFormat.Jpeg)
        Dim imgSize = ms.Length / 1024

        Return imgSize.ToString("F2") + " Kb"
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If OpenFileDialog1.ShowDialog Then
            PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName)
            Label3.Text = getSize(PictureBox1)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If PictureBox1.Image IsNot Nothing Then
            compressImg(35)
            Label4.Text = getSize(PictureBox2)
        End If
    End Sub
End Class
