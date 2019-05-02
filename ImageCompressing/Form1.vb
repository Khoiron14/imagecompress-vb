Imports System.Drawing.Imaging
Imports System.IO

Public Class Form1
    Dim filename As String

    Private Sub compressImg(intPercent As Integer) '100 percent to intPercent
        Dim intX, intY As Integer
        intX = Int(PictureBox1.Image.Width / 100 * intPercent)
        intY = Int(PictureBox1.Image.Height / 100 * intPercent)
        Dim bm As Bitmap = New Bitmap(intX, intY)
        Dim g As Graphics = Graphics.FromImage(bm)

        g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        g.DrawImage(PictureBox1.Image, 0, 0, intX, intY)

        Dim ms As New MemoryStream()
        bm.Save(ms, ImageFormat.Jpeg)
        Dim imageB = DirectCast(ms.GetBuffer, Byte())
        ms.Write(imageB, 0, imageB.Length)

        PictureBox2.Image = Image.FromStream(ms)
        Label4.Text = getSizeCompress()
    End Sub

    Private Function getSizeCompress()
        Using ms As New MemoryStream
            PictureBox2.Image.Save(ms, ImageFormat.Jpeg)
            Dim imgSize = ms.Length / 1024

            Return imgSize.ToString("F2") + " Kb"
        End Using
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            filename = OpenFileDialog1.FileName
            PictureBox1.Image = Image.FromFile(filename)

            Dim imageinfo As New FileInfo(filename)
            Dim imagesize = imageinfo.Length / 1024

            Label3.Text = imagesize.ToString("F2") + " Kb"
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If PictureBox1.Image IsNot Nothing Then
            compressImg(TrackBar1.Value)
        Else
            MsgBox("Select image first!", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If PictureBox2.Image IsNot Nothing Then
            SaveFileDialog1.FileName = Path.GetFileName(filename)
            If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                PictureBox2.Image.Save(SaveFileDialog1.FileName)
                MsgBox("Saved!", MsgBoxStyle.Information)
            End If
        Else
            MsgBox("Compress image first!", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        FrmInfo.ShowDialog()
    End Sub
End Class
