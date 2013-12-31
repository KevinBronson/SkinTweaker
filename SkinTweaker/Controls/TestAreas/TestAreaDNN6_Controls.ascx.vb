Imports System.IO

Public Class TestAreaDNN6_Controls
    'Inherits Framework.UserControlBase
    'Inherits SkinTweakerModuleBase
    'Inherits UserControlBase
    Inherits System.Web.UI.UserControl

    Private Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Dim strDllPath As String = Request.MapPath(Request.ApplicationPath & "\bin\DotNetNuke.Web.dll")
        If File.Exists(strDllPath) Then
            'DNN Version 6.x or higher is being used, so load the date picker
            Dim myDllAssembly As System.Reflection.Assembly = System.Reflection.Assembly.LoadFile(strDllPath)
            Dim objDatePicker As System.Web.UI.Control = DirectCast(myDllAssembly.CreateInstance("DotNetNuke.Web.UI.WebControls.DnnDatePicker"), System.Web.UI.Control)
            objDatePicker.ID = "StartDatePicker"
            phDatePicker.Controls.Add(objDatePicker)
            lblStartDatePicker.AssociatedControlID = "StartDatePicker"
        End If
        HyperLinkCancel.NavigateUrl = HttpContext.Current.Request.Url.ToString & "#SimpleFormExample"
    End Sub

    Protected Friend Function GetScopeClass() As String
        Try
            If chkApplyScope.Checked Then
                Return SkinTweaker.Common.Variables.CSS_ScopeName
            Else
                Return String.Empty
            End If

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
            Return String.Empty
        End Try
    End Function

End Class