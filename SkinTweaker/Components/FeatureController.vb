﻿' Copyright (c) 2012 ITM
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 

Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Services.Search

Namespace Components


    Public Class FeatureController
        'Implements IPortable
        'Implements ISearchable
        'Implements IUpgradeable

        '/// -----------------------------------------------------------------------------
        '/// <summary>
        '/// ExportModule implements the IPortable ExportModule Interface
        '/// </summary>
        '/// <param name="ModuleID">The Id of the module to be exported</param>
        '/// -----------------------------------------------------------------------------
        'Public Function ExportModule(ByVal ModuleID As Integer) As String Implements IPortable.ExportModule
        'Throw New NotImplementedException()
        'End Function

        '/// -----------------------------------------------------------------------------
        '/// <summary>
        '/// ImportModule implements the IPortable ImportModule Interface
        '/// </summary>
        '/// <param name="ModuleID">The Id of the module to be imported</param>
        '/// <param name="Content">The content to be imported</param>
        '/// <param name="Version">The version of the module to be imported</param>
        '/// <param name="UserId">The Id of the user performing the import</param>
        '/// -----------------------------------------------------------------------------
        'Public Sub ImportModule(ByVal ModuleID As Integer, ByVal Content As String, ByVal Version As String, ByVal UserID As Integer) Implements IPortable.ImportModule
        '   Throw New NotImplementedException()
        'End Sub

        '/// -----------------------------------------------------------------------------
        '/// <summary>
        '/// UpgradeModule implements the IUpgradeable Interface
        '/// </summary>
        '/// <param name="Version">The current version of the module</param>
        '/// -----------------------------------------------------------------------------
        'Public Function UpgradeModule(ByVal Version As String) As String Implements DotNetNuke.Entities.Modules.IUpgradeable.UpgradeModule
        '    Throw New NotImplementedException()
        'End Function

        '/// -----------------------------------------------------------------------------
        '/// <summary>
        '/// GetSearchItems implements the ISearchable Interface
        '/// </summary>
        '/// <param name="ModInfo">The ModuleInfo for the module to be Indexed</param>
        '/// -----------------------------------------------------------------------------
        'Public Function GetSearchItems(ByVal ModInfo As ModuleInfo) As SearchItemInfoCollection Implements ISearchable.GetSearchItems
        '   Throw New NotImplementedException()
        'End Function
    End Class

End Namespace
