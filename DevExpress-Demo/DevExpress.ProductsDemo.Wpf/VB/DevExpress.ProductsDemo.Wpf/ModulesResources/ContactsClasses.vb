Imports DevExpress.DevAV
Imports DevExpress.Mvvm
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Media
Imports System.Windows.Media.Imaging

Namespace ProductsDemo.Modules

    Public Class Contact
        Inherits DevExpress.Mvvm.BindableBase
        Implements System.ICloneable

        Private nameField As ProductsDemo.Modules.Name

        Public Property Name As Name
            Get
                Return Me.nameField
            End Get

            Set(ByVal value As Name)
                If Me.nameField IsNot value Then
                    Me.nameField = value
                    Me.RaisePropertyChanged("Name")
                End If
            End Set
        End Property

        Public ReadOnly Property FirstName As String
            Get
                Return If(Me.Name IsNot Nothing, Me.Name.FirstName, Nothing)
            End Get
        End Property

        Public ReadOnly Property LastName As String
            Get
                Return If(Me.Name IsNot Nothing, Me.Name.LastName, Nothing)
            End Get
        End Property

        Private photoField As System.Windows.Media.ImageSource

        Public Property Photo As ImageSource
            Get
                Return Me.photoField
            End Get

            Set(ByVal value As ImageSource)
                If Me.photoField IsNot value Then
                    Me.photoField = value
                    Me.RaisePropertyChanged("Photo")
                End If
            End Set
        End Property

        Private addressField As ProductsDemo.Modules.Address

        Public Property Address As Address
            Get
                Return Me.addressField
            End Get

            Set(ByVal value As Address)
                If Me.addressField IsNot value Then
                    Me.addressField = value
                    Me.RaisePropertyChanged("Address")
                End If
            End Set
        End Property

        Private emailField As String

        Public Property Email As String
            Get
                Return Me.emailField
            End Get

            Set(ByVal value As String)
                If Not Equals(Me.emailField, value) Then
                    Me.emailField = value
                    Me.RaisePropertyChanged("Email")
                End If
            End Set
        End Property

        Private phoneField As String

        Public Property Phone As String
            Get
                Return Me.phoneField
            End Get

            Set(ByVal value As String)
                If Not Equals(Me.phoneField, value) Then
                    Me.phoneField = value
                    Me.RaisePropertyChanged("Phone")
                End If
            End Set
        End Property

        Private birthDateField As System.DateTime

        Public Property BirthDate As DateTime
            Get
                Return Me.birthDateField
            End Get

            Set(ByVal value As DateTime)
                If Me.birthDateField <> value Then
                    Me.birthDateField = value
                    Me.RaisePropertyChanged("BirthDate")
                End If
            End Set
        End Property

        Private prefixField As DevExpress.DevAV.PersonPrefix

        Public Property Prefix As PersonPrefix
            Get
                Return Me.prefixField
            End Get

            Set(ByVal value As PersonPrefix)
                If Not Object.Equals(Me.prefixField, value) Then
                    Me.prefixField = value
                    Me.RaisePropertyChanged("Prefix")
                End If
            End Set
        End Property

        Private notesField As String

        Public Property Notes As String
            Get
                Return Me.notesField
            End Get

            Set(ByVal value As String)
                If Not Equals(Me.notesField, value) Then
                    Me.notesField = value
                    Me.RaisePropertyChanged("Notes")
                End If
            End Set
        End Property

        Private activityField As System.Collections.Generic.List(Of Integer)

        Public Property Activity As List(Of Integer)
            Get
                Return Me.activityField
            End Get

            Set(ByVal value As List(Of Integer))
                If Me.activityField IsNot value Then
                    Me.activityField = value
                    Me.RaisePropertiesChanged("Activity")
                End If
            End Set
        End Property

        Public Sub New()
            Me.Name = New ProductsDemo.Modules.Name()
            Me.Address = New ProductsDemo.Modules.Address()
        End Sub

        Public Sub Assign(ByVal contact As ProductsDemo.Modules.Contact)
            Me.Name.Assign(contact.Name)
            Me.Photo = contact.Photo
            Me.Address.Assign(contact.Address)
            Me.Email = contact.Email
            Me.Phone = contact.Phone
            Me.BirthDate = contact.BirthDate
            Me.Prefix = contact.Prefix
            Me.Notes = contact.Notes
            Me.Activity = contact.Activity
            Me.RaisePropertiesChanged(String.Empty)
        End Sub

        Public Function Clone() As Object Implements Global.System.ICloneable.Clone
            Return New ProductsDemo.Modules.Contact() With {.Address = CType(Me.Address.Clone(), ProductsDemo.Modules.Address), .BirthDate = Me.BirthDate, .Email = Me.Email, .Prefix = Me.Prefix, .Name = CType(Me.Name.Clone(), ProductsDemo.Modules.Name), .Photo = Me.Photo, .Phone = Me.Phone, .Notes = Me.Notes, .Activity = Me.Activity}
        End Function
    End Class

    Public Class Name
        Inherits DevExpress.Mvvm.BindableBase
        Implements System.ICloneable

        Private firstNameField As String

        Public Property FirstName As String
            Get
                Return Me.firstNameField
            End Get

            Set(ByVal value As String)
                If Not Equals(Me.firstNameField, value) Then
                    Me.firstNameField = value
                    Me.RaisePropertyChanged("FirstName")
                    Me.UpdateFullName()
                End If
            End Set
        End Property

        Private middleNameField As String

        Public Property MiddleName As String
            Get
                Return Me.middleNameField
            End Get

            Set(ByVal value As String)
                If Not Equals(Me.middleNameField, value) Then
                    Me.middleNameField = value
                    Me.RaisePropertyChanged("MiddleName")
                    Me.UpdateFullName()
                End If
            End Set
        End Property

        Private lastNameField As String

        Public Property LastName As String
            Get
                Return Me.lastNameField
            End Get

            Set(ByVal value As String)
                If Not Equals(Me.lastNameField, value) Then
                    Me.lastNameField = value
                    Me.RaisePropertyChanged("LastName")
                    Me.UpdateFullName()
                End If
            End Set
        End Property

        Private fullNameField As String

        Public Property FullName As String
            Get
                Return Me.fullNameField
            End Get

            Set(ByVal value As String)
                If Not Equals(Me.fullNameField, value) Then
                    Me.fullNameField = value
                    Me.RaisePropertyChanged("FullName")
                End If
            End Set
        End Property

        Private titleField As ProductsDemo.Modules.ContactTitle

        Public Property Title As ContactTitle
            Get
                Return Me.titleField
            End Get

            Set(ByVal value As ContactTitle)
                If Me.titleField <> value Then
                    Me.titleField = value
                    Me.RaisePropertyChanged("Title")
                    Me.UpdateFullName()
                End If
            End Set
        End Property

        Private Sub UpdateFullName()
            Me.FullName = String.Format("{0}{1}{2}{3}", If(Me.Title = ProductsDemo.Modules.ContactTitle.None, System.[String].Empty, Me.GetFormatString(Me.Title.ToString())), Me.GetFormatString(Me.FirstName), Me.GetFormatString(Me.MiddleName), Me.GetFormatString(Me.LastName))
        End Sub

        Private Function GetFormatString(ByVal name As String) As String
            If String.IsNullOrEmpty(name) Then Return String.Empty
            Return String.Format("{0} ", name)
        End Function

        Public Sub Assign(ByVal name As ProductsDemo.Modules.Name)
            Me.FirstName = name.FirstName
            Me.MiddleName = name.MiddleName
            Me.LastName = name.LastName
            Me.Title = name.Title
        End Sub

        Public Overrides Function ToString() As String
            Return Me.FullName
        End Function

        Public Function Clone() As Object Implements Global.System.ICloneable.Clone
            Return New ProductsDemo.Modules.Name() With {.FirstName = Me.FirstName, .MiddleName = Me.MiddleName, .LastName = Me.LastName, .FullName = Me.FullName, .Title = Me.Title}
        End Function
    End Class

    Public Class Address
        Inherits DevExpress.Mvvm.BindableBase
        Implements System.ICloneable

        Public Sub New()
        End Sub

        Friend Sub New(ByVal addressString As String)
            Try
                Dim lines As String() = addressString.Split(","c)
                Me.AddressLine = lines(CInt((0))).Trim()
                Me.City = lines(CInt((1))).Trim()
                Me.State = lines(CInt((2))).Trim().Substring(0, 2)
                Dim temp As String = lines(CInt((2))).Trim()
                Me.Zip = temp.Substring(3, temp.Length - 3)
            Catch
            End Try
        End Sub

        Private addressLineField As String

        Public Property AddressLine As String
            Get
                Return Me.addressLineField
            End Get

            Set(ByVal value As String)
                If Not Equals(Me.addressLineField, value) Then
                    Me.addressLineField = value
                    Me.RaisePropertyChanged("AddressLine")
                End If
            End Set
        End Property

        Private stateField As String

        Public Property State As String
            Get
                Return Me.stateField
            End Get

            Set(ByVal value As String)
                If Not Equals(Me.stateField, value) Then
                    Me.stateField = value
                    Me.RaisePropertyChanged("State")
                End If
            End Set
        End Property

        Private cityField As String

        Public Property City As String
            Get
                Return Me.cityField
            End Get

            Set(ByVal value As String)
                If Not Equals(Me.cityField, value) Then
                    Me.cityField = value
                    Me.RaisePropertyChanged("City")
                End If
            End Set
        End Property

        Private zipField As String

        Public Property Zip As String
            Get
                Return Me.zipField
            End Get

            Set(ByVal value As String)
                If Not Equals(Me.zipField, value) Then
                    Me.zipField = value
                    Me.RaisePropertyChanged("Zip")
                End If
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return Me.GetAddressFullString(System.Environment.NewLine)
        End Function

        Public Function GetAddressFullString(ByVal separator As String) As String
            Return String.Format("{0}{1}{2}{3}{4}", Me.AddressLine, separator, Me.GetFormatString(Me.City), Me.GetFormatString(Me.State, False), Me.GetFormatString(Me.Zip, False))
        End Function

        Private Function GetFormatString(ByVal name As String, ByVal Optional addComma As Boolean = True) As String
            If String.IsNullOrEmpty(name) Then Return String.Empty
            Dim format As String = If(addComma, "{0}, ", "{0} ")
            Return String.Format(format, name)
        End Function

        Public Sub Assign(ByVal address As ProductsDemo.Modules.Address)
            Me.AddressLine = address.AddressLine
            Me.State = address.State
            Me.City = address.City
            Me.Zip = address.Zip
        End Sub

        Public Function Clone() As Object Implements Global.System.ICloneable.Clone
            Return New ProductsDemo.Modules.Address() With {.AddressLine = Me.AddressLine, .City = Me.City, .State = Me.State, .Zip = Me.Zip}
        End Function
    End Class

    Public Enum ContactTitle
        None
        Dr
        Miss
        Mr
        Mrs
        Ms
        Prof
    End Enum
End Namespace
