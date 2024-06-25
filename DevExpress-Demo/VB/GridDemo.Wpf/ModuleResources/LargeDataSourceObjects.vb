Imports DevExpress.Mvvm.DataAnnotations
Imports System.ComponentModel.DataAnnotations

Namespace GridDemo

    Public Class LargeDataSourceObjectMedium
        Inherits LargeDataSourceObjectBase

        Public Sub New(ByVal id As Integer)
            MyBase.New(id)
        End Sub

#Region "properties"
        <Display(Name:="From (1)", Order:=1)>
        Public Property From1 As String
            Get
                Return fromValues.GetValue(1)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(1, value)
            End Set
        End Property

        <Display(Name:="To (2)", Order:=2)>
        Public Property To2 As String
            Get
                Return toValues.GetValue(2)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(2, value)
            End Set
        End Property

        <Display(Name:="Sent (3)", Order:=3)>
        Public Property Sent3 As Date
            Get
                Return sentValues.GetValue(3)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(3, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (4)", Order:=4)>
        Public Property HasAttachment4 As Boolean
            Get
                Return hasAttachmentValues.GetValue(4)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(4, value)
            End Set
        End Property

        <Display(Name:="Size (5)", Order:=5)>
        Public Property Size5 As Integer
            Get
                Return sizeValues.GetValue(5)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(5, value)
            End Set
        End Property

        <Display(Name:="Priority (6)", Order:=6), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority6 As Priority
            Get
                Return priorityValues.GetValue(6)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(6, value)
            End Set
        End Property

        <Display(Name:="Subject (7)", Order:=7), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject7 As String
            Get
                Return subjectValues.GetValue(7)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(7, value)
            End Set
        End Property

        <Display(Name:="From (8)", Order:=8)>
        Public Property From8 As String
            Get
                Return fromValues.GetValue(8)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(8, value)
            End Set
        End Property

        <Display(Name:="To (9)", Order:=9)>
        Public Property To9 As String
            Get
                Return toValues.GetValue(9)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(9, value)
            End Set
        End Property

        <Display(Name:="Sent (10)", Order:=10)>
        Public Property Sent10 As Date
            Get
                Return sentValues.GetValue(10)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(10, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (11)", Order:=11)>
        Public Property HasAttachment11 As Boolean
            Get
                Return hasAttachmentValues.GetValue(11)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(11, value)
            End Set
        End Property

        <Display(Name:="Size (12)", Order:=12)>
        Public Property Size12 As Integer
            Get
                Return sizeValues.GetValue(12)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(12, value)
            End Set
        End Property

        <Display(Name:="Priority (13)", Order:=13), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority13 As Priority
            Get
                Return priorityValues.GetValue(13)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(13, value)
            End Set
        End Property

        <Display(Name:="Subject (14)", Order:=14), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject14 As String
            Get
                Return subjectValues.GetValue(14)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(14, value)
            End Set
        End Property

        <Display(Name:="From (15)", Order:=15)>
        Public Property From15 As String
            Get
                Return fromValues.GetValue(15)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(15, value)
            End Set
        End Property

        <Display(Name:="To (16)", Order:=16)>
        Public Property To16 As String
            Get
                Return toValues.GetValue(16)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(16, value)
            End Set
        End Property

        <Display(Name:="Sent (17)", Order:=17)>
        Public Property Sent17 As Date
            Get
                Return sentValues.GetValue(17)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(17, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (18)", Order:=18)>
        Public Property HasAttachment18 As Boolean
            Get
                Return hasAttachmentValues.GetValue(18)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(18, value)
            End Set
        End Property

        <Display(Name:="Size (19)", Order:=19)>
        Public Property Size19 As Integer
            Get
                Return sizeValues.GetValue(19)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(19, value)
            End Set
        End Property

        <Display(Name:="Priority (20)", Order:=20), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority20 As Priority
            Get
                Return priorityValues.GetValue(20)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(20, value)
            End Set
        End Property

        <Display(Name:="Subject (21)", Order:=21), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject21 As String
            Get
                Return subjectValues.GetValue(21)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(21, value)
            End Set
        End Property

        <Display(Name:="From (22)", Order:=22)>
        Public Property From22 As String
            Get
                Return fromValues.GetValue(22)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(22, value)
            End Set
        End Property

        <Display(Name:="To (23)", Order:=23)>
        Public Property To23 As String
            Get
                Return toValues.GetValue(23)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(23, value)
            End Set
        End Property

        <Display(Name:="Sent (24)", Order:=24)>
        Public Property Sent24 As Date
            Get
                Return sentValues.GetValue(24)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(24, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (25)", Order:=25)>
        Public Property HasAttachment25 As Boolean
            Get
                Return hasAttachmentValues.GetValue(25)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(25, value)
            End Set
        End Property

        <Display(Name:="Size (26)", Order:=26)>
        Public Property Size26 As Integer
            Get
                Return sizeValues.GetValue(26)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(26, value)
            End Set
        End Property

        <Display(Name:="Priority (27)", Order:=27), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority27 As Priority
            Get
                Return priorityValues.GetValue(27)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(27, value)
            End Set
        End Property

        <Display(Name:="Subject (28)", Order:=28), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject28 As String
            Get
                Return subjectValues.GetValue(28)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(28, value)
            End Set
        End Property

        <Display(Name:="From (29)", Order:=29)>
        Public Property From29 As String
            Get
                Return fromValues.GetValue(29)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(29, value)
            End Set
        End Property

        <Display(Name:="To (30)", Order:=30)>
        Public Property To30 As String
            Get
                Return toValues.GetValue(30)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(30, value)
            End Set
        End Property

        <Display(Name:="Sent (31)", Order:=31)>
        Public Property Sent31 As Date
            Get
                Return sentValues.GetValue(31)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(31, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (32)", Order:=32)>
        Public Property HasAttachment32 As Boolean
            Get
                Return hasAttachmentValues.GetValue(32)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(32, value)
            End Set
        End Property

        <Display(Name:="Size (33)", Order:=33)>
        Public Property Size33 As Integer
            Get
                Return sizeValues.GetValue(33)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(33, value)
            End Set
        End Property

        <Display(Name:="Priority (34)", Order:=34), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority34 As Priority
            Get
                Return priorityValues.GetValue(34)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(34, value)
            End Set
        End Property

        <Display(Name:="Subject (35)", Order:=35), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject35 As String
            Get
                Return subjectValues.GetValue(35)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(35, value)
            End Set
        End Property

        <Display(Name:="From (36)", Order:=36)>
        Public Property From36 As String
            Get
                Return fromValues.GetValue(36)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(36, value)
            End Set
        End Property

        <Display(Name:="To (37)", Order:=37)>
        Public Property To37 As String
            Get
                Return toValues.GetValue(37)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(37, value)
            End Set
        End Property

        <Display(Name:="Sent (38)", Order:=38)>
        Public Property Sent38 As Date
            Get
                Return sentValues.GetValue(38)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(38, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (39)", Order:=39)>
        Public Property HasAttachment39 As Boolean
            Get
                Return hasAttachmentValues.GetValue(39)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(39, value)
            End Set
        End Property

        <Display(Name:="Size (40)", Order:=40)>
        Public Property Size40 As Integer
            Get
                Return sizeValues.GetValue(40)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(40, value)
            End Set
        End Property

        <Display(Name:="Priority (41)", Order:=41), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority41 As Priority
            Get
                Return priorityValues.GetValue(41)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(41, value)
            End Set
        End Property

        <Display(Name:="Subject (42)", Order:=42), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject42 As String
            Get
                Return subjectValues.GetValue(42)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(42, value)
            End Set
        End Property

        <Display(Name:="From (43)", Order:=43)>
        Public Property From43 As String
            Get
                Return fromValues.GetValue(43)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(43, value)
            End Set
        End Property

        <Display(Name:="To (44)", Order:=44)>
        Public Property To44 As String
            Get
                Return toValues.GetValue(44)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(44, value)
            End Set
        End Property

        <Display(Name:="Sent (45)", Order:=45)>
        Public Property Sent45 As Date
            Get
                Return sentValues.GetValue(45)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(45, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (46)", Order:=46)>
        Public Property HasAttachment46 As Boolean
            Get
                Return hasAttachmentValues.GetValue(46)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(46, value)
            End Set
        End Property

        <Display(Name:="Size (47)", Order:=47)>
        Public Property Size47 As Integer
            Get
                Return sizeValues.GetValue(47)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(47, value)
            End Set
        End Property

        <Display(Name:="Priority (48)", Order:=48), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority48 As Priority
            Get
                Return priorityValues.GetValue(48)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(48, value)
            End Set
        End Property

        <Display(Name:="Subject (49)", Order:=49), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject49 As String
            Get
                Return subjectValues.GetValue(49)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(49, value)
            End Set
        End Property

        <Display(Name:="From (50)", Order:=50)>
        Public Property From50 As String
            Get
                Return fromValues.GetValue(50)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(50, value)
            End Set
        End Property

        <Display(Name:="To (51)", Order:=51)>
        Public Property To51 As String
            Get
                Return toValues.GetValue(51)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(51, value)
            End Set
        End Property

        <Display(Name:="Sent (52)", Order:=52)>
        Public Property Sent52 As Date
            Get
                Return sentValues.GetValue(52)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(52, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (53)", Order:=53)>
        Public Property HasAttachment53 As Boolean
            Get
                Return hasAttachmentValues.GetValue(53)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(53, value)
            End Set
        End Property

        <Display(Name:="Size (54)", Order:=54)>
        Public Property Size54 As Integer
            Get
                Return sizeValues.GetValue(54)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(54, value)
            End Set
        End Property

        <Display(Name:="Priority (55)", Order:=55), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority55 As Priority
            Get
                Return priorityValues.GetValue(55)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(55, value)
            End Set
        End Property

        <Display(Name:="Subject (56)", Order:=56), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject56 As String
            Get
                Return subjectValues.GetValue(56)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(56, value)
            End Set
        End Property

        <Display(Name:="From (57)", Order:=57)>
        Public Property From57 As String
            Get
                Return fromValues.GetValue(57)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(57, value)
            End Set
        End Property

        <Display(Name:="To (58)", Order:=58)>
        Public Property To58 As String
            Get
                Return toValues.GetValue(58)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(58, value)
            End Set
        End Property

        <Display(Name:="Sent (59)", Order:=59)>
        Public Property Sent59 As Date
            Get
                Return sentValues.GetValue(59)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(59, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (60)", Order:=60)>
        Public Property HasAttachment60 As Boolean
            Get
                Return hasAttachmentValues.GetValue(60)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(60, value)
            End Set
        End Property

        <Display(Name:="Size (61)", Order:=61)>
        Public Property Size61 As Integer
            Get
                Return sizeValues.GetValue(61)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(61, value)
            End Set
        End Property

        <Display(Name:="Priority (62)", Order:=62), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority62 As Priority
            Get
                Return priorityValues.GetValue(62)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(62, value)
            End Set
        End Property

        <Display(Name:="Subject (63)", Order:=63), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject63 As String
            Get
                Return subjectValues.GetValue(63)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(63, value)
            End Set
        End Property

        <Display(Name:="From (64)", Order:=64)>
        Public Property From64 As String
            Get
                Return fromValues.GetValue(64)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(64, value)
            End Set
        End Property

        <Display(Name:="To (65)", Order:=65)>
        Public Property To65 As String
            Get
                Return toValues.GetValue(65)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(65, value)
            End Set
        End Property

        <Display(Name:="Sent (66)", Order:=66)>
        Public Property Sent66 As Date
            Get
                Return sentValues.GetValue(66)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(66, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (67)", Order:=67)>
        Public Property HasAttachment67 As Boolean
            Get
                Return hasAttachmentValues.GetValue(67)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(67, value)
            End Set
        End Property

        <Display(Name:="Size (68)", Order:=68)>
        Public Property Size68 As Integer
            Get
                Return sizeValues.GetValue(68)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(68, value)
            End Set
        End Property

        <Display(Name:="Priority (69)", Order:=69), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority69 As Priority
            Get
                Return priorityValues.GetValue(69)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(69, value)
            End Set
        End Property

        <Display(Name:="Subject (70)", Order:=70), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject70 As String
            Get
                Return subjectValues.GetValue(70)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(70, value)
            End Set
        End Property

        <Display(Name:="From (71)", Order:=71)>
        Public Property From71 As String
            Get
                Return fromValues.GetValue(71)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(71, value)
            End Set
        End Property

        <Display(Name:="To (72)", Order:=72)>
        Public Property To72 As String
            Get
                Return toValues.GetValue(72)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(72, value)
            End Set
        End Property

        <Display(Name:="Sent (73)", Order:=73)>
        Public Property Sent73 As Date
            Get
                Return sentValues.GetValue(73)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(73, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (74)", Order:=74)>
        Public Property HasAttachment74 As Boolean
            Get
                Return hasAttachmentValues.GetValue(74)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(74, value)
            End Set
        End Property

        <Display(Name:="Size (75)", Order:=75)>
        Public Property Size75 As Integer
            Get
                Return sizeValues.GetValue(75)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(75, value)
            End Set
        End Property

        <Display(Name:="Priority (76)", Order:=76), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority76 As Priority
            Get
                Return priorityValues.GetValue(76)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(76, value)
            End Set
        End Property

        <Display(Name:="Subject (77)", Order:=77), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject77 As String
            Get
                Return subjectValues.GetValue(77)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(77, value)
            End Set
        End Property

        <Display(Name:="From (78)", Order:=78)>
        Public Property From78 As String
            Get
                Return fromValues.GetValue(78)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(78, value)
            End Set
        End Property

        <Display(Name:="To (79)", Order:=79)>
        Public Property To79 As String
            Get
                Return toValues.GetValue(79)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(79, value)
            End Set
        End Property

        <Display(Name:="Sent (80)", Order:=80)>
        Public Property Sent80 As Date
            Get
                Return sentValues.GetValue(80)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(80, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (81)", Order:=81)>
        Public Property HasAttachment81 As Boolean
            Get
                Return hasAttachmentValues.GetValue(81)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(81, value)
            End Set
        End Property

        <Display(Name:="Size (82)", Order:=82)>
        Public Property Size82 As Integer
            Get
                Return sizeValues.GetValue(82)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(82, value)
            End Set
        End Property

        <Display(Name:="Priority (83)", Order:=83), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority83 As Priority
            Get
                Return priorityValues.GetValue(83)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(83, value)
            End Set
        End Property

        <Display(Name:="Subject (84)", Order:=84), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject84 As String
            Get
                Return subjectValues.GetValue(84)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(84, value)
            End Set
        End Property

        <Display(Name:="From (85)", Order:=85)>
        Public Property From85 As String
            Get
                Return fromValues.GetValue(85)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(85, value)
            End Set
        End Property

        <Display(Name:="To (86)", Order:=86)>
        Public Property To86 As String
            Get
                Return toValues.GetValue(86)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(86, value)
            End Set
        End Property

        <Display(Name:="Sent (87)", Order:=87)>
        Public Property Sent87 As Date
            Get
                Return sentValues.GetValue(87)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(87, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (88)", Order:=88)>
        Public Property HasAttachment88 As Boolean
            Get
                Return hasAttachmentValues.GetValue(88)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(88, value)
            End Set
        End Property

        <Display(Name:="Size (89)", Order:=89)>
        Public Property Size89 As Integer
            Get
                Return sizeValues.GetValue(89)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(89, value)
            End Set
        End Property

        <Display(Name:="Priority (90)", Order:=90), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority90 As Priority
            Get
                Return priorityValues.GetValue(90)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(90, value)
            End Set
        End Property

        <Display(Name:="Subject (91)", Order:=91), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject91 As String
            Get
                Return subjectValues.GetValue(91)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(91, value)
            End Set
        End Property

        <Display(Name:="From (92)", Order:=92)>
        Public Property From92 As String
            Get
                Return fromValues.GetValue(92)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(92, value)
            End Set
        End Property

        <Display(Name:="To (93)", Order:=93)>
        Public Property To93 As String
            Get
                Return toValues.GetValue(93)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(93, value)
            End Set
        End Property

        <Display(Name:="Sent (94)", Order:=94)>
        Public Property Sent94 As Date
            Get
                Return sentValues.GetValue(94)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(94, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (95)", Order:=95)>
        Public Property HasAttachment95 As Boolean
            Get
                Return hasAttachmentValues.GetValue(95)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(95, value)
            End Set
        End Property

        <Display(Name:="Size (96)", Order:=96)>
        Public Property Size96 As Integer
            Get
                Return sizeValues.GetValue(96)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(96, value)
            End Set
        End Property

        <Display(Name:="Priority (97)", Order:=97), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority97 As Priority
            Get
                Return priorityValues.GetValue(97)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(97, value)
            End Set
        End Property

        <Display(Name:="Subject (98)", Order:=98), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject98 As String
            Get
                Return subjectValues.GetValue(98)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(98, value)
            End Set
        End Property

        <Display(Name:="From (99)", Order:=99)>
        Public Property From99 As String
            Get
                Return fromValues.GetValue(99)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(99, value)
            End Set
        End Property

        <Display(Name:="To (100)", Order:=100)>
        Public Property To100 As String
            Get
                Return toValues.GetValue(100)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(100, value)
            End Set
        End Property

        <Display(Name:="Sent (101)", Order:=101)>
        Public Property Sent101 As Date
            Get
                Return sentValues.GetValue(101)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(101, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (102)", Order:=102)>
        Public Property HasAttachment102 As Boolean
            Get
                Return hasAttachmentValues.GetValue(102)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(102, value)
            End Set
        End Property

        <Display(Name:="Size (103)", Order:=103)>
        Public Property Size103 As Integer
            Get
                Return sizeValues.GetValue(103)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(103, value)
            End Set
        End Property

        <Display(Name:="Priority (104)", Order:=104), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority104 As Priority
            Get
                Return priorityValues.GetValue(104)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(104, value)
            End Set
        End Property

        <Display(Name:="Subject (105)", Order:=105), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject105 As String
            Get
                Return subjectValues.GetValue(105)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(105, value)
            End Set
        End Property

        <Display(Name:="From (106)", Order:=106)>
        Public Property From106 As String
            Get
                Return fromValues.GetValue(106)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(106, value)
            End Set
        End Property

        <Display(Name:="To (107)", Order:=107)>
        Public Property To107 As String
            Get
                Return toValues.GetValue(107)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(107, value)
            End Set
        End Property

        <Display(Name:="Sent (108)", Order:=108)>
        Public Property Sent108 As Date
            Get
                Return sentValues.GetValue(108)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(108, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (109)", Order:=109)>
        Public Property HasAttachment109 As Boolean
            Get
                Return hasAttachmentValues.GetValue(109)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(109, value)
            End Set
        End Property

        <Display(Name:="Size (110)", Order:=110)>
        Public Property Size110 As Integer
            Get
                Return sizeValues.GetValue(110)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(110, value)
            End Set
        End Property

        <Display(Name:="Priority (111)", Order:=111), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority111 As Priority
            Get
                Return priorityValues.GetValue(111)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(111, value)
            End Set
        End Property

        <Display(Name:="Subject (112)", Order:=112), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject112 As String
            Get
                Return subjectValues.GetValue(112)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(112, value)
            End Set
        End Property

        <Display(Name:="From (113)", Order:=113)>
        Public Property From113 As String
            Get
                Return fromValues.GetValue(113)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(113, value)
            End Set
        End Property

        <Display(Name:="To (114)", Order:=114)>
        Public Property To114 As String
            Get
                Return toValues.GetValue(114)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(114, value)
            End Set
        End Property

        <Display(Name:="Sent (115)", Order:=115)>
        Public Property Sent115 As Date
            Get
                Return sentValues.GetValue(115)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(115, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (116)", Order:=116)>
        Public Property HasAttachment116 As Boolean
            Get
                Return hasAttachmentValues.GetValue(116)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(116, value)
            End Set
        End Property

        <Display(Name:="Size (117)", Order:=117)>
        Public Property Size117 As Integer
            Get
                Return sizeValues.GetValue(117)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(117, value)
            End Set
        End Property

        <Display(Name:="Priority (118)", Order:=118), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority118 As Priority
            Get
                Return priorityValues.GetValue(118)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(118, value)
            End Set
        End Property

        <Display(Name:="Subject (119)", Order:=119), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject119 As String
            Get
                Return subjectValues.GetValue(119)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(119, value)
            End Set
        End Property

        <Display(Name:="From (120)", Order:=120)>
        Public Property From120 As String
            Get
                Return fromValues.GetValue(120)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(120, value)
            End Set
        End Property

        <Display(Name:="To (121)", Order:=121)>
        Public Property To121 As String
            Get
                Return toValues.GetValue(121)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(121, value)
            End Set
        End Property

        <Display(Name:="Sent (122)", Order:=122)>
        Public Property Sent122 As Date
            Get
                Return sentValues.GetValue(122)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(122, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (123)", Order:=123)>
        Public Property HasAttachment123 As Boolean
            Get
                Return hasAttachmentValues.GetValue(123)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(123, value)
            End Set
        End Property

        <Display(Name:="Size (124)", Order:=124)>
        Public Property Size124 As Integer
            Get
                Return sizeValues.GetValue(124)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(124, value)
            End Set
        End Property

        <Display(Name:="Priority (125)", Order:=125), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority125 As Priority
            Get
                Return priorityValues.GetValue(125)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(125, value)
            End Set
        End Property

        <Display(Name:="Subject (126)", Order:=126), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject126 As String
            Get
                Return subjectValues.GetValue(126)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(126, value)
            End Set
        End Property

        <Display(Name:="From (127)", Order:=127)>
        Public Property From127 As String
            Get
                Return fromValues.GetValue(127)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(127, value)
            End Set
        End Property

        <Display(Name:="To (128)", Order:=128)>
        Public Property To128 As String
            Get
                Return toValues.GetValue(128)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(128, value)
            End Set
        End Property

        <Display(Name:="Sent (129)", Order:=129)>
        Public Property Sent129 As Date
            Get
                Return sentValues.GetValue(129)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(129, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (130)", Order:=130)>
        Public Property HasAttachment130 As Boolean
            Get
                Return hasAttachmentValues.GetValue(130)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(130, value)
            End Set
        End Property

        <Display(Name:="Size (131)", Order:=131)>
        Public Property Size131 As Integer
            Get
                Return sizeValues.GetValue(131)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(131, value)
            End Set
        End Property

        <Display(Name:="Priority (132)", Order:=132), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority132 As Priority
            Get
                Return priorityValues.GetValue(132)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(132, value)
            End Set
        End Property

        <Display(Name:="Subject (133)", Order:=133), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject133 As String
            Get
                Return subjectValues.GetValue(133)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(133, value)
            End Set
        End Property

        <Display(Name:="From (134)", Order:=134)>
        Public Property From134 As String
            Get
                Return fromValues.GetValue(134)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(134, value)
            End Set
        End Property

        <Display(Name:="To (135)", Order:=135)>
        Public Property To135 As String
            Get
                Return toValues.GetValue(135)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(135, value)
            End Set
        End Property

        <Display(Name:="Sent (136)", Order:=136)>
        Public Property Sent136 As Date
            Get
                Return sentValues.GetValue(136)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(136, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (137)", Order:=137)>
        Public Property HasAttachment137 As Boolean
            Get
                Return hasAttachmentValues.GetValue(137)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(137, value)
            End Set
        End Property

        <Display(Name:="Size (138)", Order:=138)>
        Public Property Size138 As Integer
            Get
                Return sizeValues.GetValue(138)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(138, value)
            End Set
        End Property

        <Display(Name:="Priority (139)", Order:=139), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority139 As Priority
            Get
                Return priorityValues.GetValue(139)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(139, value)
            End Set
        End Property

        <Display(Name:="Subject (140)", Order:=140), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject140 As String
            Get
                Return subjectValues.GetValue(140)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(140, value)
            End Set
        End Property

        <Display(Name:="From (141)", Order:=141)>
        Public Property From141 As String
            Get
                Return fromValues.GetValue(141)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(141, value)
            End Set
        End Property

        <Display(Name:="To (142)", Order:=142)>
        Public Property To142 As String
            Get
                Return toValues.GetValue(142)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(142, value)
            End Set
        End Property

        <Display(Name:="Sent (143)", Order:=143)>
        Public Property Sent143 As Date
            Get
                Return sentValues.GetValue(143)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(143, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (144)", Order:=144)>
        Public Property HasAttachment144 As Boolean
            Get
                Return hasAttachmentValues.GetValue(144)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(144, value)
            End Set
        End Property

        <Display(Name:="Size (145)", Order:=145)>
        Public Property Size145 As Integer
            Get
                Return sizeValues.GetValue(145)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(145, value)
            End Set
        End Property

        <Display(Name:="Priority (146)", Order:=146), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority146 As Priority
            Get
                Return priorityValues.GetValue(146)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(146, value)
            End Set
        End Property

        <Display(Name:="Subject (147)", Order:=147), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject147 As String
            Get
                Return subjectValues.GetValue(147)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(147, value)
            End Set
        End Property

        <Display(Name:="From (148)", Order:=148)>
        Public Property From148 As String
            Get
                Return fromValues.GetValue(148)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(148, value)
            End Set
        End Property

        <Display(Name:="To (149)", Order:=149)>
        Public Property To149 As String
            Get
                Return toValues.GetValue(149)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(149, value)
            End Set
        End Property

        <Display(Name:="Sent (150)", Order:=150)>
        Public Property Sent150 As Date
            Get
                Return sentValues.GetValue(150)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(150, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (151)", Order:=151)>
        Public Property HasAttachment151 As Boolean
            Get
                Return hasAttachmentValues.GetValue(151)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(151, value)
            End Set
        End Property

        <Display(Name:="Size (152)", Order:=152)>
        Public Property Size152 As Integer
            Get
                Return sizeValues.GetValue(152)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(152, value)
            End Set
        End Property

        <Display(Name:="Priority (153)", Order:=153), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority153 As Priority
            Get
                Return priorityValues.GetValue(153)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(153, value)
            End Set
        End Property

        <Display(Name:="Subject (154)", Order:=154), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject154 As String
            Get
                Return subjectValues.GetValue(154)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(154, value)
            End Set
        End Property

        <Display(Name:="From (155)", Order:=155)>
        Public Property From155 As String
            Get
                Return fromValues.GetValue(155)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(155, value)
            End Set
        End Property

        <Display(Name:="To (156)", Order:=156)>
        Public Property To156 As String
            Get
                Return toValues.GetValue(156)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(156, value)
            End Set
        End Property

        <Display(Name:="Sent (157)", Order:=157)>
        Public Property Sent157 As Date
            Get
                Return sentValues.GetValue(157)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(157, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (158)", Order:=158)>
        Public Property HasAttachment158 As Boolean
            Get
                Return hasAttachmentValues.GetValue(158)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(158, value)
            End Set
        End Property

        <Display(Name:="Size (159)", Order:=159)>
        Public Property Size159 As Integer
            Get
                Return sizeValues.GetValue(159)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(159, value)
            End Set
        End Property

        <Display(Name:="Priority (160)", Order:=160), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority160 As Priority
            Get
                Return priorityValues.GetValue(160)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(160, value)
            End Set
        End Property

        <Display(Name:="Subject (161)", Order:=161), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject161 As String
            Get
                Return subjectValues.GetValue(161)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(161, value)
            End Set
        End Property

        <Display(Name:="From (162)", Order:=162)>
        Public Property From162 As String
            Get
                Return fromValues.GetValue(162)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(162, value)
            End Set
        End Property

        <Display(Name:="To (163)", Order:=163)>
        Public Property To163 As String
            Get
                Return toValues.GetValue(163)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(163, value)
            End Set
        End Property

        <Display(Name:="Sent (164)", Order:=164)>
        Public Property Sent164 As Date
            Get
                Return sentValues.GetValue(164)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(164, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (165)", Order:=165)>
        Public Property HasAttachment165 As Boolean
            Get
                Return hasAttachmentValues.GetValue(165)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(165, value)
            End Set
        End Property

        <Display(Name:="Size (166)", Order:=166)>
        Public Property Size166 As Integer
            Get
                Return sizeValues.GetValue(166)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(166, value)
            End Set
        End Property

        <Display(Name:="Priority (167)", Order:=167), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority167 As Priority
            Get
                Return priorityValues.GetValue(167)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(167, value)
            End Set
        End Property

        <Display(Name:="Subject (168)", Order:=168), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject168 As String
            Get
                Return subjectValues.GetValue(168)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(168, value)
            End Set
        End Property

        <Display(Name:="From (169)", Order:=169)>
        Public Property From169 As String
            Get
                Return fromValues.GetValue(169)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(169, value)
            End Set
        End Property

        <Display(Name:="To (170)", Order:=170)>
        Public Property To170 As String
            Get
                Return toValues.GetValue(170)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(170, value)
            End Set
        End Property

        <Display(Name:="Sent (171)", Order:=171)>
        Public Property Sent171 As Date
            Get
                Return sentValues.GetValue(171)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(171, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (172)", Order:=172)>
        Public Property HasAttachment172 As Boolean
            Get
                Return hasAttachmentValues.GetValue(172)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(172, value)
            End Set
        End Property

        <Display(Name:="Size (173)", Order:=173)>
        Public Property Size173 As Integer
            Get
                Return sizeValues.GetValue(173)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(173, value)
            End Set
        End Property

        <Display(Name:="Priority (174)", Order:=174), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority174 As Priority
            Get
                Return priorityValues.GetValue(174)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(174, value)
            End Set
        End Property

        <Display(Name:="Subject (175)", Order:=175), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject175 As String
            Get
                Return subjectValues.GetValue(175)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(175, value)
            End Set
        End Property

        <Display(Name:="From (176)", Order:=176)>
        Public Property From176 As String
            Get
                Return fromValues.GetValue(176)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(176, value)
            End Set
        End Property

        <Display(Name:="To (177)", Order:=177)>
        Public Property To177 As String
            Get
                Return toValues.GetValue(177)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(177, value)
            End Set
        End Property

        <Display(Name:="Sent (178)", Order:=178)>
        Public Property Sent178 As Date
            Get
                Return sentValues.GetValue(178)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(178, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (179)", Order:=179)>
        Public Property HasAttachment179 As Boolean
            Get
                Return hasAttachmentValues.GetValue(179)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(179, value)
            End Set
        End Property

        <Display(Name:="Size (180)", Order:=180)>
        Public Property Size180 As Integer
            Get
                Return sizeValues.GetValue(180)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(180, value)
            End Set
        End Property

        <Display(Name:="Priority (181)", Order:=181), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority181 As Priority
            Get
                Return priorityValues.GetValue(181)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(181, value)
            End Set
        End Property

        <Display(Name:="Subject (182)", Order:=182), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject182 As String
            Get
                Return subjectValues.GetValue(182)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(182, value)
            End Set
        End Property

        <Display(Name:="From (183)", Order:=183)>
        Public Property From183 As String
            Get
                Return fromValues.GetValue(183)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(183, value)
            End Set
        End Property

        <Display(Name:="To (184)", Order:=184)>
        Public Property To184 As String
            Get
                Return toValues.GetValue(184)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(184, value)
            End Set
        End Property

        <Display(Name:="Sent (185)", Order:=185)>
        Public Property Sent185 As Date
            Get
                Return sentValues.GetValue(185)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(185, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (186)", Order:=186)>
        Public Property HasAttachment186 As Boolean
            Get
                Return hasAttachmentValues.GetValue(186)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(186, value)
            End Set
        End Property

        <Display(Name:="Size (187)", Order:=187)>
        Public Property Size187 As Integer
            Get
                Return sizeValues.GetValue(187)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(187, value)
            End Set
        End Property

        <Display(Name:="Priority (188)", Order:=188), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority188 As Priority
            Get
                Return priorityValues.GetValue(188)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(188, value)
            End Set
        End Property

        <Display(Name:="Subject (189)", Order:=189), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject189 As String
            Get
                Return subjectValues.GetValue(189)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(189, value)
            End Set
        End Property

        <Display(Name:="From (190)", Order:=190)>
        Public Property From190 As String
            Get
                Return fromValues.GetValue(190)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(190, value)
            End Set
        End Property

        <Display(Name:="To (191)", Order:=191)>
        Public Property To191 As String
            Get
                Return toValues.GetValue(191)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(191, value)
            End Set
        End Property

        <Display(Name:="Sent (192)", Order:=192)>
        Public Property Sent192 As Date
            Get
                Return sentValues.GetValue(192)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(192, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (193)", Order:=193)>
        Public Property HasAttachment193 As Boolean
            Get
                Return hasAttachmentValues.GetValue(193)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(193, value)
            End Set
        End Property

        <Display(Name:="Size (194)", Order:=194)>
        Public Property Size194 As Integer
            Get
                Return sizeValues.GetValue(194)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(194, value)
            End Set
        End Property

        <Display(Name:="Priority (195)", Order:=195), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority195 As Priority
            Get
                Return priorityValues.GetValue(195)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(195, value)
            End Set
        End Property

        <Display(Name:="Subject (196)", Order:=196), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject196 As String
            Get
                Return subjectValues.GetValue(196)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(196, value)
            End Set
        End Property

        <Display(Name:="From (197)", Order:=197)>
        Public Property From197 As String
            Get
                Return fromValues.GetValue(197)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(197, value)
            End Set
        End Property

        <Display(Name:="To (198)", Order:=198)>
        Public Property To198 As String
            Get
                Return toValues.GetValue(198)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(198, value)
            End Set
        End Property

        <Display(Name:="Sent (199)", Order:=199)>
        Public Property Sent199 As Date
            Get
                Return sentValues.GetValue(199)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(199, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (200)", Order:=200)>
        Public Property HasAttachment200 As Boolean
            Get
                Return hasAttachmentValues.GetValue(200)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(200, value)
            End Set
        End Property

        <Display(Name:="Size (201)", Order:=201)>
        Public Property Size201 As Integer
            Get
                Return sizeValues.GetValue(201)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(201, value)
            End Set
        End Property

        <Display(Name:="Priority (202)", Order:=202), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority202 As Priority
            Get
                Return priorityValues.GetValue(202)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(202, value)
            End Set
        End Property

        <Display(Name:="Subject (203)", Order:=203), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject203 As String
            Get
                Return subjectValues.GetValue(203)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(203, value)
            End Set
        End Property

        <Display(Name:="From (204)", Order:=204)>
        Public Property From204 As String
            Get
                Return fromValues.GetValue(204)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(204, value)
            End Set
        End Property

        <Display(Name:="To (205)", Order:=205)>
        Public Property To205 As String
            Get
                Return toValues.GetValue(205)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(205, value)
            End Set
        End Property

        <Display(Name:="Sent (206)", Order:=206)>
        Public Property Sent206 As Date
            Get
                Return sentValues.GetValue(206)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(206, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (207)", Order:=207)>
        Public Property HasAttachment207 As Boolean
            Get
                Return hasAttachmentValues.GetValue(207)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(207, value)
            End Set
        End Property

        <Display(Name:="Size (208)", Order:=208)>
        Public Property Size208 As Integer
            Get
                Return sizeValues.GetValue(208)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(208, value)
            End Set
        End Property

        <Display(Name:="Priority (209)", Order:=209), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority209 As Priority
            Get
                Return priorityValues.GetValue(209)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(209, value)
            End Set
        End Property

        <Display(Name:="Subject (210)", Order:=210), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject210 As String
            Get
                Return subjectValues.GetValue(210)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(210, value)
            End Set
        End Property

        <Display(Name:="From (211)", Order:=211)>
        Public Property From211 As String
            Get
                Return fromValues.GetValue(211)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(211, value)
            End Set
        End Property

        <Display(Name:="To (212)", Order:=212)>
        Public Property To212 As String
            Get
                Return toValues.GetValue(212)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(212, value)
            End Set
        End Property

        <Display(Name:="Sent (213)", Order:=213)>
        Public Property Sent213 As Date
            Get
                Return sentValues.GetValue(213)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(213, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (214)", Order:=214)>
        Public Property HasAttachment214 As Boolean
            Get
                Return hasAttachmentValues.GetValue(214)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(214, value)
            End Set
        End Property

        <Display(Name:="Size (215)", Order:=215)>
        Public Property Size215 As Integer
            Get
                Return sizeValues.GetValue(215)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(215, value)
            End Set
        End Property

        <Display(Name:="Priority (216)", Order:=216), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority216 As Priority
            Get
                Return priorityValues.GetValue(216)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(216, value)
            End Set
        End Property

        <Display(Name:="Subject (217)", Order:=217), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject217 As String
            Get
                Return subjectValues.GetValue(217)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(217, value)
            End Set
        End Property

        <Display(Name:="From (218)", Order:=218)>
        Public Property From218 As String
            Get
                Return fromValues.GetValue(218)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(218, value)
            End Set
        End Property

        <Display(Name:="To (219)", Order:=219)>
        Public Property To219 As String
            Get
                Return toValues.GetValue(219)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(219, value)
            End Set
        End Property

        <Display(Name:="Sent (220)", Order:=220)>
        Public Property Sent220 As Date
            Get
                Return sentValues.GetValue(220)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(220, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (221)", Order:=221)>
        Public Property HasAttachment221 As Boolean
            Get
                Return hasAttachmentValues.GetValue(221)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(221, value)
            End Set
        End Property

        <Display(Name:="Size (222)", Order:=222)>
        Public Property Size222 As Integer
            Get
                Return sizeValues.GetValue(222)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(222, value)
            End Set
        End Property

        <Display(Name:="Priority (223)", Order:=223), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority223 As Priority
            Get
                Return priorityValues.GetValue(223)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(223, value)
            End Set
        End Property

        <Display(Name:="Subject (224)", Order:=224), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject224 As String
            Get
                Return subjectValues.GetValue(224)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(224, value)
            End Set
        End Property

        <Display(Name:="From (225)", Order:=225)>
        Public Property From225 As String
            Get
                Return fromValues.GetValue(225)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(225, value)
            End Set
        End Property

        <Display(Name:="To (226)", Order:=226)>
        Public Property To226 As String
            Get
                Return toValues.GetValue(226)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(226, value)
            End Set
        End Property

        <Display(Name:="Sent (227)", Order:=227)>
        Public Property Sent227 As Date
            Get
                Return sentValues.GetValue(227)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(227, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (228)", Order:=228)>
        Public Property HasAttachment228 As Boolean
            Get
                Return hasAttachmentValues.GetValue(228)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(228, value)
            End Set
        End Property

        <Display(Name:="Size (229)", Order:=229)>
        Public Property Size229 As Integer
            Get
                Return sizeValues.GetValue(229)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(229, value)
            End Set
        End Property

        <Display(Name:="Priority (230)", Order:=230), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority230 As Priority
            Get
                Return priorityValues.GetValue(230)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(230, value)
            End Set
        End Property

        <Display(Name:="Subject (231)", Order:=231), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject231 As String
            Get
                Return subjectValues.GetValue(231)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(231, value)
            End Set
        End Property

        <Display(Name:="From (232)", Order:=232)>
        Public Property From232 As String
            Get
                Return fromValues.GetValue(232)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(232, value)
            End Set
        End Property

        <Display(Name:="To (233)", Order:=233)>
        Public Property To233 As String
            Get
                Return toValues.GetValue(233)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(233, value)
            End Set
        End Property

        <Display(Name:="Sent (234)", Order:=234)>
        Public Property Sent234 As Date
            Get
                Return sentValues.GetValue(234)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(234, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (235)", Order:=235)>
        Public Property HasAttachment235 As Boolean
            Get
                Return hasAttachmentValues.GetValue(235)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(235, value)
            End Set
        End Property

        <Display(Name:="Size (236)", Order:=236)>
        Public Property Size236 As Integer
            Get
                Return sizeValues.GetValue(236)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(236, value)
            End Set
        End Property

        <Display(Name:="Priority (237)", Order:=237), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority237 As Priority
            Get
                Return priorityValues.GetValue(237)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(237, value)
            End Set
        End Property

        <Display(Name:="Subject (238)", Order:=238), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject238 As String
            Get
                Return subjectValues.GetValue(238)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(238, value)
            End Set
        End Property

        <Display(Name:="From (239)", Order:=239)>
        Public Property From239 As String
            Get
                Return fromValues.GetValue(239)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(239, value)
            End Set
        End Property

        <Display(Name:="To (240)", Order:=240)>
        Public Property To240 As String
            Get
                Return toValues.GetValue(240)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(240, value)
            End Set
        End Property

        <Display(Name:="Sent (241)", Order:=241)>
        Public Property Sent241 As Date
            Get
                Return sentValues.GetValue(241)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(241, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (242)", Order:=242)>
        Public Property HasAttachment242 As Boolean
            Get
                Return hasAttachmentValues.GetValue(242)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(242, value)
            End Set
        End Property

        <Display(Name:="Size (243)", Order:=243)>
        Public Property Size243 As Integer
            Get
                Return sizeValues.GetValue(243)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(243, value)
            End Set
        End Property

        <Display(Name:="Priority (244)", Order:=244), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority244 As Priority
            Get
                Return priorityValues.GetValue(244)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(244, value)
            End Set
        End Property

        <Display(Name:="Subject (245)", Order:=245), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject245 As String
            Get
                Return subjectValues.GetValue(245)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(245, value)
            End Set
        End Property

        <Display(Name:="From (246)", Order:=246)>
        Public Property From246 As String
            Get
                Return fromValues.GetValue(246)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(246, value)
            End Set
        End Property

        <Display(Name:="To (247)", Order:=247)>
        Public Property To247 As String
            Get
                Return toValues.GetValue(247)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(247, value)
            End Set
        End Property

        <Display(Name:="Sent (248)", Order:=248)>
        Public Property Sent248 As Date
            Get
                Return sentValues.GetValue(248)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(248, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (249)", Order:=249)>
        Public Property HasAttachment249 As Boolean
            Get
                Return hasAttachmentValues.GetValue(249)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(249, value)
            End Set
        End Property
#End Region
    End Class

    Public Class LargeDataSourceObjectLarge
        Inherits LargeDataSourceObjectMedium

        Public Sub New(ByVal id As Integer)
            MyBase.New(id)
        End Sub

#Region "properties"
        <Display(Name:="Size (250)", Order:=250)>
        Public Property Size250 As Integer
            Get
                Return sizeValues.GetValue(250)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(250, value)
            End Set
        End Property

        <Display(Name:="Priority (251)", Order:=251), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority251 As Priority
            Get
                Return priorityValues.GetValue(251)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(251, value)
            End Set
        End Property

        <Display(Name:="Subject (252)", Order:=252), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject252 As String
            Get
                Return subjectValues.GetValue(252)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(252, value)
            End Set
        End Property

        <Display(Name:="From (253)", Order:=253)>
        Public Property From253 As String
            Get
                Return fromValues.GetValue(253)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(253, value)
            End Set
        End Property

        <Display(Name:="To (254)", Order:=254)>
        Public Property To254 As String
            Get
                Return toValues.GetValue(254)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(254, value)
            End Set
        End Property

        <Display(Name:="Sent (255)", Order:=255)>
        Public Property Sent255 As Date
            Get
                Return sentValues.GetValue(255)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(255, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (256)", Order:=256)>
        Public Property HasAttachment256 As Boolean
            Get
                Return hasAttachmentValues.GetValue(256)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(256, value)
            End Set
        End Property

        <Display(Name:="Size (257)", Order:=257)>
        Public Property Size257 As Integer
            Get
                Return sizeValues.GetValue(257)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(257, value)
            End Set
        End Property

        <Display(Name:="Priority (258)", Order:=258), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority258 As Priority
            Get
                Return priorityValues.GetValue(258)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(258, value)
            End Set
        End Property

        <Display(Name:="Subject (259)", Order:=259), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject259 As String
            Get
                Return subjectValues.GetValue(259)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(259, value)
            End Set
        End Property

        <Display(Name:="From (260)", Order:=260)>
        Public Property From260 As String
            Get
                Return fromValues.GetValue(260)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(260, value)
            End Set
        End Property

        <Display(Name:="To (261)", Order:=261)>
        Public Property To261 As String
            Get
                Return toValues.GetValue(261)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(261, value)
            End Set
        End Property

        <Display(Name:="Sent (262)", Order:=262)>
        Public Property Sent262 As Date
            Get
                Return sentValues.GetValue(262)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(262, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (263)", Order:=263)>
        Public Property HasAttachment263 As Boolean
            Get
                Return hasAttachmentValues.GetValue(263)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(263, value)
            End Set
        End Property

        <Display(Name:="Size (264)", Order:=264)>
        Public Property Size264 As Integer
            Get
                Return sizeValues.GetValue(264)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(264, value)
            End Set
        End Property

        <Display(Name:="Priority (265)", Order:=265), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority265 As Priority
            Get
                Return priorityValues.GetValue(265)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(265, value)
            End Set
        End Property

        <Display(Name:="Subject (266)", Order:=266), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject266 As String
            Get
                Return subjectValues.GetValue(266)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(266, value)
            End Set
        End Property

        <Display(Name:="From (267)", Order:=267)>
        Public Property From267 As String
            Get
                Return fromValues.GetValue(267)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(267, value)
            End Set
        End Property

        <Display(Name:="To (268)", Order:=268)>
        Public Property To268 As String
            Get
                Return toValues.GetValue(268)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(268, value)
            End Set
        End Property

        <Display(Name:="Sent (269)", Order:=269)>
        Public Property Sent269 As Date
            Get
                Return sentValues.GetValue(269)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(269, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (270)", Order:=270)>
        Public Property HasAttachment270 As Boolean
            Get
                Return hasAttachmentValues.GetValue(270)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(270, value)
            End Set
        End Property

        <Display(Name:="Size (271)", Order:=271)>
        Public Property Size271 As Integer
            Get
                Return sizeValues.GetValue(271)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(271, value)
            End Set
        End Property

        <Display(Name:="Priority (272)", Order:=272), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority272 As Priority
            Get
                Return priorityValues.GetValue(272)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(272, value)
            End Set
        End Property

        <Display(Name:="Subject (273)", Order:=273), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject273 As String
            Get
                Return subjectValues.GetValue(273)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(273, value)
            End Set
        End Property

        <Display(Name:="From (274)", Order:=274)>
        Public Property From274 As String
            Get
                Return fromValues.GetValue(274)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(274, value)
            End Set
        End Property

        <Display(Name:="To (275)", Order:=275)>
        Public Property To275 As String
            Get
                Return toValues.GetValue(275)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(275, value)
            End Set
        End Property

        <Display(Name:="Sent (276)", Order:=276)>
        Public Property Sent276 As Date
            Get
                Return sentValues.GetValue(276)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(276, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (277)", Order:=277)>
        Public Property HasAttachment277 As Boolean
            Get
                Return hasAttachmentValues.GetValue(277)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(277, value)
            End Set
        End Property

        <Display(Name:="Size (278)", Order:=278)>
        Public Property Size278 As Integer
            Get
                Return sizeValues.GetValue(278)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(278, value)
            End Set
        End Property

        <Display(Name:="Priority (279)", Order:=279), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority279 As Priority
            Get
                Return priorityValues.GetValue(279)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(279, value)
            End Set
        End Property

        <Display(Name:="Subject (280)", Order:=280), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject280 As String
            Get
                Return subjectValues.GetValue(280)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(280, value)
            End Set
        End Property

        <Display(Name:="From (281)", Order:=281)>
        Public Property From281 As String
            Get
                Return fromValues.GetValue(281)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(281, value)
            End Set
        End Property

        <Display(Name:="To (282)", Order:=282)>
        Public Property To282 As String
            Get
                Return toValues.GetValue(282)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(282, value)
            End Set
        End Property

        <Display(Name:="Sent (283)", Order:=283)>
        Public Property Sent283 As Date
            Get
                Return sentValues.GetValue(283)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(283, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (284)", Order:=284)>
        Public Property HasAttachment284 As Boolean
            Get
                Return hasAttachmentValues.GetValue(284)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(284, value)
            End Set
        End Property

        <Display(Name:="Size (285)", Order:=285)>
        Public Property Size285 As Integer
            Get
                Return sizeValues.GetValue(285)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(285, value)
            End Set
        End Property

        <Display(Name:="Priority (286)", Order:=286), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority286 As Priority
            Get
                Return priorityValues.GetValue(286)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(286, value)
            End Set
        End Property

        <Display(Name:="Subject (287)", Order:=287), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject287 As String
            Get
                Return subjectValues.GetValue(287)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(287, value)
            End Set
        End Property

        <Display(Name:="From (288)", Order:=288)>
        Public Property From288 As String
            Get
                Return fromValues.GetValue(288)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(288, value)
            End Set
        End Property

        <Display(Name:="To (289)", Order:=289)>
        Public Property To289 As String
            Get
                Return toValues.GetValue(289)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(289, value)
            End Set
        End Property

        <Display(Name:="Sent (290)", Order:=290)>
        Public Property Sent290 As Date
            Get
                Return sentValues.GetValue(290)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(290, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (291)", Order:=291)>
        Public Property HasAttachment291 As Boolean
            Get
                Return hasAttachmentValues.GetValue(291)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(291, value)
            End Set
        End Property

        <Display(Name:="Size (292)", Order:=292)>
        Public Property Size292 As Integer
            Get
                Return sizeValues.GetValue(292)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(292, value)
            End Set
        End Property

        <Display(Name:="Priority (293)", Order:=293), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority293 As Priority
            Get
                Return priorityValues.GetValue(293)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(293, value)
            End Set
        End Property

        <Display(Name:="Subject (294)", Order:=294), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject294 As String
            Get
                Return subjectValues.GetValue(294)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(294, value)
            End Set
        End Property

        <Display(Name:="From (295)", Order:=295)>
        Public Property From295 As String
            Get
                Return fromValues.GetValue(295)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(295, value)
            End Set
        End Property

        <Display(Name:="To (296)", Order:=296)>
        Public Property To296 As String
            Get
                Return toValues.GetValue(296)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(296, value)
            End Set
        End Property

        <Display(Name:="Sent (297)", Order:=297)>
        Public Property Sent297 As Date
            Get
                Return sentValues.GetValue(297)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(297, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (298)", Order:=298)>
        Public Property HasAttachment298 As Boolean
            Get
                Return hasAttachmentValues.GetValue(298)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(298, value)
            End Set
        End Property

        <Display(Name:="Size (299)", Order:=299)>
        Public Property Size299 As Integer
            Get
                Return sizeValues.GetValue(299)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(299, value)
            End Set
        End Property

        <Display(Name:="Priority (300)", Order:=300), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority300 As Priority
            Get
                Return priorityValues.GetValue(300)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(300, value)
            End Set
        End Property

        <Display(Name:="Subject (301)", Order:=301), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject301 As String
            Get
                Return subjectValues.GetValue(301)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(301, value)
            End Set
        End Property

        <Display(Name:="From (302)", Order:=302)>
        Public Property From302 As String
            Get
                Return fromValues.GetValue(302)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(302, value)
            End Set
        End Property

        <Display(Name:="To (303)", Order:=303)>
        Public Property To303 As String
            Get
                Return toValues.GetValue(303)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(303, value)
            End Set
        End Property

        <Display(Name:="Sent (304)", Order:=304)>
        Public Property Sent304 As Date
            Get
                Return sentValues.GetValue(304)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(304, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (305)", Order:=305)>
        Public Property HasAttachment305 As Boolean
            Get
                Return hasAttachmentValues.GetValue(305)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(305, value)
            End Set
        End Property

        <Display(Name:="Size (306)", Order:=306)>
        Public Property Size306 As Integer
            Get
                Return sizeValues.GetValue(306)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(306, value)
            End Set
        End Property

        <Display(Name:="Priority (307)", Order:=307), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority307 As Priority
            Get
                Return priorityValues.GetValue(307)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(307, value)
            End Set
        End Property

        <Display(Name:="Subject (308)", Order:=308), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject308 As String
            Get
                Return subjectValues.GetValue(308)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(308, value)
            End Set
        End Property

        <Display(Name:="From (309)", Order:=309)>
        Public Property From309 As String
            Get
                Return fromValues.GetValue(309)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(309, value)
            End Set
        End Property

        <Display(Name:="To (310)", Order:=310)>
        Public Property To310 As String
            Get
                Return toValues.GetValue(310)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(310, value)
            End Set
        End Property

        <Display(Name:="Sent (311)", Order:=311)>
        Public Property Sent311 As Date
            Get
                Return sentValues.GetValue(311)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(311, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (312)", Order:=312)>
        Public Property HasAttachment312 As Boolean
            Get
                Return hasAttachmentValues.GetValue(312)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(312, value)
            End Set
        End Property

        <Display(Name:="Size (313)", Order:=313)>
        Public Property Size313 As Integer
            Get
                Return sizeValues.GetValue(313)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(313, value)
            End Set
        End Property

        <Display(Name:="Priority (314)", Order:=314), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority314 As Priority
            Get
                Return priorityValues.GetValue(314)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(314, value)
            End Set
        End Property

        <Display(Name:="Subject (315)", Order:=315), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject315 As String
            Get
                Return subjectValues.GetValue(315)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(315, value)
            End Set
        End Property

        <Display(Name:="From (316)", Order:=316)>
        Public Property From316 As String
            Get
                Return fromValues.GetValue(316)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(316, value)
            End Set
        End Property

        <Display(Name:="To (317)", Order:=317)>
        Public Property To317 As String
            Get
                Return toValues.GetValue(317)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(317, value)
            End Set
        End Property

        <Display(Name:="Sent (318)", Order:=318)>
        Public Property Sent318 As Date
            Get
                Return sentValues.GetValue(318)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(318, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (319)", Order:=319)>
        Public Property HasAttachment319 As Boolean
            Get
                Return hasAttachmentValues.GetValue(319)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(319, value)
            End Set
        End Property

        <Display(Name:="Size (320)", Order:=320)>
        Public Property Size320 As Integer
            Get
                Return sizeValues.GetValue(320)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(320, value)
            End Set
        End Property

        <Display(Name:="Priority (321)", Order:=321), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority321 As Priority
            Get
                Return priorityValues.GetValue(321)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(321, value)
            End Set
        End Property

        <Display(Name:="Subject (322)", Order:=322), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject322 As String
            Get
                Return subjectValues.GetValue(322)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(322, value)
            End Set
        End Property

        <Display(Name:="From (323)", Order:=323)>
        Public Property From323 As String
            Get
                Return fromValues.GetValue(323)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(323, value)
            End Set
        End Property

        <Display(Name:="To (324)", Order:=324)>
        Public Property To324 As String
            Get
                Return toValues.GetValue(324)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(324, value)
            End Set
        End Property

        <Display(Name:="Sent (325)", Order:=325)>
        Public Property Sent325 As Date
            Get
                Return sentValues.GetValue(325)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(325, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (326)", Order:=326)>
        Public Property HasAttachment326 As Boolean
            Get
                Return hasAttachmentValues.GetValue(326)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(326, value)
            End Set
        End Property

        <Display(Name:="Size (327)", Order:=327)>
        Public Property Size327 As Integer
            Get
                Return sizeValues.GetValue(327)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(327, value)
            End Set
        End Property

        <Display(Name:="Priority (328)", Order:=328), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority328 As Priority
            Get
                Return priorityValues.GetValue(328)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(328, value)
            End Set
        End Property

        <Display(Name:="Subject (329)", Order:=329), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject329 As String
            Get
                Return subjectValues.GetValue(329)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(329, value)
            End Set
        End Property

        <Display(Name:="From (330)", Order:=330)>
        Public Property From330 As String
            Get
                Return fromValues.GetValue(330)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(330, value)
            End Set
        End Property

        <Display(Name:="To (331)", Order:=331)>
        Public Property To331 As String
            Get
                Return toValues.GetValue(331)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(331, value)
            End Set
        End Property

        <Display(Name:="Sent (332)", Order:=332)>
        Public Property Sent332 As Date
            Get
                Return sentValues.GetValue(332)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(332, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (333)", Order:=333)>
        Public Property HasAttachment333 As Boolean
            Get
                Return hasAttachmentValues.GetValue(333)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(333, value)
            End Set
        End Property

        <Display(Name:="Size (334)", Order:=334)>
        Public Property Size334 As Integer
            Get
                Return sizeValues.GetValue(334)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(334, value)
            End Set
        End Property

        <Display(Name:="Priority (335)", Order:=335), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority335 As Priority
            Get
                Return priorityValues.GetValue(335)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(335, value)
            End Set
        End Property

        <Display(Name:="Subject (336)", Order:=336), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject336 As String
            Get
                Return subjectValues.GetValue(336)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(336, value)
            End Set
        End Property

        <Display(Name:="From (337)", Order:=337)>
        Public Property From337 As String
            Get
                Return fromValues.GetValue(337)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(337, value)
            End Set
        End Property

        <Display(Name:="To (338)", Order:=338)>
        Public Property To338 As String
            Get
                Return toValues.GetValue(338)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(338, value)
            End Set
        End Property

        <Display(Name:="Sent (339)", Order:=339)>
        Public Property Sent339 As Date
            Get
                Return sentValues.GetValue(339)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(339, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (340)", Order:=340)>
        Public Property HasAttachment340 As Boolean
            Get
                Return hasAttachmentValues.GetValue(340)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(340, value)
            End Set
        End Property

        <Display(Name:="Size (341)", Order:=341)>
        Public Property Size341 As Integer
            Get
                Return sizeValues.GetValue(341)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(341, value)
            End Set
        End Property

        <Display(Name:="Priority (342)", Order:=342), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority342 As Priority
            Get
                Return priorityValues.GetValue(342)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(342, value)
            End Set
        End Property

        <Display(Name:="Subject (343)", Order:=343), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject343 As String
            Get
                Return subjectValues.GetValue(343)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(343, value)
            End Set
        End Property

        <Display(Name:="From (344)", Order:=344)>
        Public Property From344 As String
            Get
                Return fromValues.GetValue(344)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(344, value)
            End Set
        End Property

        <Display(Name:="To (345)", Order:=345)>
        Public Property To345 As String
            Get
                Return toValues.GetValue(345)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(345, value)
            End Set
        End Property

        <Display(Name:="Sent (346)", Order:=346)>
        Public Property Sent346 As Date
            Get
                Return sentValues.GetValue(346)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(346, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (347)", Order:=347)>
        Public Property HasAttachment347 As Boolean
            Get
                Return hasAttachmentValues.GetValue(347)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(347, value)
            End Set
        End Property

        <Display(Name:="Size (348)", Order:=348)>
        Public Property Size348 As Integer
            Get
                Return sizeValues.GetValue(348)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(348, value)
            End Set
        End Property

        <Display(Name:="Priority (349)", Order:=349), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority349 As Priority
            Get
                Return priorityValues.GetValue(349)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(349, value)
            End Set
        End Property

        <Display(Name:="Subject (350)", Order:=350), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject350 As String
            Get
                Return subjectValues.GetValue(350)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(350, value)
            End Set
        End Property

        <Display(Name:="From (351)", Order:=351)>
        Public Property From351 As String
            Get
                Return fromValues.GetValue(351)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(351, value)
            End Set
        End Property

        <Display(Name:="To (352)", Order:=352)>
        Public Property To352 As String
            Get
                Return toValues.GetValue(352)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(352, value)
            End Set
        End Property

        <Display(Name:="Sent (353)", Order:=353)>
        Public Property Sent353 As Date
            Get
                Return sentValues.GetValue(353)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(353, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (354)", Order:=354)>
        Public Property HasAttachment354 As Boolean
            Get
                Return hasAttachmentValues.GetValue(354)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(354, value)
            End Set
        End Property

        <Display(Name:="Size (355)", Order:=355)>
        Public Property Size355 As Integer
            Get
                Return sizeValues.GetValue(355)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(355, value)
            End Set
        End Property

        <Display(Name:="Priority (356)", Order:=356), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority356 As Priority
            Get
                Return priorityValues.GetValue(356)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(356, value)
            End Set
        End Property

        <Display(Name:="Subject (357)", Order:=357), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject357 As String
            Get
                Return subjectValues.GetValue(357)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(357, value)
            End Set
        End Property

        <Display(Name:="From (358)", Order:=358)>
        Public Property From358 As String
            Get
                Return fromValues.GetValue(358)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(358, value)
            End Set
        End Property

        <Display(Name:="To (359)", Order:=359)>
        Public Property To359 As String
            Get
                Return toValues.GetValue(359)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(359, value)
            End Set
        End Property

        <Display(Name:="Sent (360)", Order:=360)>
        Public Property Sent360 As Date
            Get
                Return sentValues.GetValue(360)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(360, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (361)", Order:=361)>
        Public Property HasAttachment361 As Boolean
            Get
                Return hasAttachmentValues.GetValue(361)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(361, value)
            End Set
        End Property

        <Display(Name:="Size (362)", Order:=362)>
        Public Property Size362 As Integer
            Get
                Return sizeValues.GetValue(362)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(362, value)
            End Set
        End Property

        <Display(Name:="Priority (363)", Order:=363), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority363 As Priority
            Get
                Return priorityValues.GetValue(363)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(363, value)
            End Set
        End Property

        <Display(Name:="Subject (364)", Order:=364), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject364 As String
            Get
                Return subjectValues.GetValue(364)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(364, value)
            End Set
        End Property

        <Display(Name:="From (365)", Order:=365)>
        Public Property From365 As String
            Get
                Return fromValues.GetValue(365)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(365, value)
            End Set
        End Property

        <Display(Name:="To (366)", Order:=366)>
        Public Property To366 As String
            Get
                Return toValues.GetValue(366)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(366, value)
            End Set
        End Property

        <Display(Name:="Sent (367)", Order:=367)>
        Public Property Sent367 As Date
            Get
                Return sentValues.GetValue(367)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(367, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (368)", Order:=368)>
        Public Property HasAttachment368 As Boolean
            Get
                Return hasAttachmentValues.GetValue(368)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(368, value)
            End Set
        End Property

        <Display(Name:="Size (369)", Order:=369)>
        Public Property Size369 As Integer
            Get
                Return sizeValues.GetValue(369)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(369, value)
            End Set
        End Property

        <Display(Name:="Priority (370)", Order:=370), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority370 As Priority
            Get
                Return priorityValues.GetValue(370)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(370, value)
            End Set
        End Property

        <Display(Name:="Subject (371)", Order:=371), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject371 As String
            Get
                Return subjectValues.GetValue(371)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(371, value)
            End Set
        End Property

        <Display(Name:="From (372)", Order:=372)>
        Public Property From372 As String
            Get
                Return fromValues.GetValue(372)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(372, value)
            End Set
        End Property

        <Display(Name:="To (373)", Order:=373)>
        Public Property To373 As String
            Get
                Return toValues.GetValue(373)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(373, value)
            End Set
        End Property

        <Display(Name:="Sent (374)", Order:=374)>
        Public Property Sent374 As Date
            Get
                Return sentValues.GetValue(374)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(374, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (375)", Order:=375)>
        Public Property HasAttachment375 As Boolean
            Get
                Return hasAttachmentValues.GetValue(375)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(375, value)
            End Set
        End Property

        <Display(Name:="Size (376)", Order:=376)>
        Public Property Size376 As Integer
            Get
                Return sizeValues.GetValue(376)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(376, value)
            End Set
        End Property

        <Display(Name:="Priority (377)", Order:=377), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority377 As Priority
            Get
                Return priorityValues.GetValue(377)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(377, value)
            End Set
        End Property

        <Display(Name:="Subject (378)", Order:=378), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject378 As String
            Get
                Return subjectValues.GetValue(378)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(378, value)
            End Set
        End Property

        <Display(Name:="From (379)", Order:=379)>
        Public Property From379 As String
            Get
                Return fromValues.GetValue(379)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(379, value)
            End Set
        End Property

        <Display(Name:="To (380)", Order:=380)>
        Public Property To380 As String
            Get
                Return toValues.GetValue(380)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(380, value)
            End Set
        End Property

        <Display(Name:="Sent (381)", Order:=381)>
        Public Property Sent381 As Date
            Get
                Return sentValues.GetValue(381)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(381, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (382)", Order:=382)>
        Public Property HasAttachment382 As Boolean
            Get
                Return hasAttachmentValues.GetValue(382)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(382, value)
            End Set
        End Property

        <Display(Name:="Size (383)", Order:=383)>
        Public Property Size383 As Integer
            Get
                Return sizeValues.GetValue(383)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(383, value)
            End Set
        End Property

        <Display(Name:="Priority (384)", Order:=384), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority384 As Priority
            Get
                Return priorityValues.GetValue(384)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(384, value)
            End Set
        End Property

        <Display(Name:="Subject (385)", Order:=385), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject385 As String
            Get
                Return subjectValues.GetValue(385)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(385, value)
            End Set
        End Property

        <Display(Name:="From (386)", Order:=386)>
        Public Property From386 As String
            Get
                Return fromValues.GetValue(386)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(386, value)
            End Set
        End Property

        <Display(Name:="To (387)", Order:=387)>
        Public Property To387 As String
            Get
                Return toValues.GetValue(387)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(387, value)
            End Set
        End Property

        <Display(Name:="Sent (388)", Order:=388)>
        Public Property Sent388 As Date
            Get
                Return sentValues.GetValue(388)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(388, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (389)", Order:=389)>
        Public Property HasAttachment389 As Boolean
            Get
                Return hasAttachmentValues.GetValue(389)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(389, value)
            End Set
        End Property

        <Display(Name:="Size (390)", Order:=390)>
        Public Property Size390 As Integer
            Get
                Return sizeValues.GetValue(390)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(390, value)
            End Set
        End Property

        <Display(Name:="Priority (391)", Order:=391), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority391 As Priority
            Get
                Return priorityValues.GetValue(391)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(391, value)
            End Set
        End Property

        <Display(Name:="Subject (392)", Order:=392), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject392 As String
            Get
                Return subjectValues.GetValue(392)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(392, value)
            End Set
        End Property

        <Display(Name:="From (393)", Order:=393)>
        Public Property From393 As String
            Get
                Return fromValues.GetValue(393)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(393, value)
            End Set
        End Property

        <Display(Name:="To (394)", Order:=394)>
        Public Property To394 As String
            Get
                Return toValues.GetValue(394)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(394, value)
            End Set
        End Property

        <Display(Name:="Sent (395)", Order:=395)>
        Public Property Sent395 As Date
            Get
                Return sentValues.GetValue(395)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(395, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (396)", Order:=396)>
        Public Property HasAttachment396 As Boolean
            Get
                Return hasAttachmentValues.GetValue(396)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(396, value)
            End Set
        End Property

        <Display(Name:="Size (397)", Order:=397)>
        Public Property Size397 As Integer
            Get
                Return sizeValues.GetValue(397)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(397, value)
            End Set
        End Property

        <Display(Name:="Priority (398)", Order:=398), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority398 As Priority
            Get
                Return priorityValues.GetValue(398)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(398, value)
            End Set
        End Property

        <Display(Name:="Subject (399)", Order:=399), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject399 As String
            Get
                Return subjectValues.GetValue(399)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(399, value)
            End Set
        End Property

        <Display(Name:="From (400)", Order:=400)>
        Public Property From400 As String
            Get
                Return fromValues.GetValue(400)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(400, value)
            End Set
        End Property

        <Display(Name:="To (401)", Order:=401)>
        Public Property To401 As String
            Get
                Return toValues.GetValue(401)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(401, value)
            End Set
        End Property

        <Display(Name:="Sent (402)", Order:=402)>
        Public Property Sent402 As Date
            Get
                Return sentValues.GetValue(402)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(402, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (403)", Order:=403)>
        Public Property HasAttachment403 As Boolean
            Get
                Return hasAttachmentValues.GetValue(403)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(403, value)
            End Set
        End Property

        <Display(Name:="Size (404)", Order:=404)>
        Public Property Size404 As Integer
            Get
                Return sizeValues.GetValue(404)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(404, value)
            End Set
        End Property

        <Display(Name:="Priority (405)", Order:=405), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority405 As Priority
            Get
                Return priorityValues.GetValue(405)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(405, value)
            End Set
        End Property

        <Display(Name:="Subject (406)", Order:=406), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject406 As String
            Get
                Return subjectValues.GetValue(406)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(406, value)
            End Set
        End Property

        <Display(Name:="From (407)", Order:=407)>
        Public Property From407 As String
            Get
                Return fromValues.GetValue(407)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(407, value)
            End Set
        End Property

        <Display(Name:="To (408)", Order:=408)>
        Public Property To408 As String
            Get
                Return toValues.GetValue(408)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(408, value)
            End Set
        End Property

        <Display(Name:="Sent (409)", Order:=409)>
        Public Property Sent409 As Date
            Get
                Return sentValues.GetValue(409)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(409, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (410)", Order:=410)>
        Public Property HasAttachment410 As Boolean
            Get
                Return hasAttachmentValues.GetValue(410)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(410, value)
            End Set
        End Property

        <Display(Name:="Size (411)", Order:=411)>
        Public Property Size411 As Integer
            Get
                Return sizeValues.GetValue(411)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(411, value)
            End Set
        End Property

        <Display(Name:="Priority (412)", Order:=412), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority412 As Priority
            Get
                Return priorityValues.GetValue(412)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(412, value)
            End Set
        End Property

        <Display(Name:="Subject (413)", Order:=413), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject413 As String
            Get
                Return subjectValues.GetValue(413)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(413, value)
            End Set
        End Property

        <Display(Name:="From (414)", Order:=414)>
        Public Property From414 As String
            Get
                Return fromValues.GetValue(414)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(414, value)
            End Set
        End Property

        <Display(Name:="To (415)", Order:=415)>
        Public Property To415 As String
            Get
                Return toValues.GetValue(415)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(415, value)
            End Set
        End Property

        <Display(Name:="Sent (416)", Order:=416)>
        Public Property Sent416 As Date
            Get
                Return sentValues.GetValue(416)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(416, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (417)", Order:=417)>
        Public Property HasAttachment417 As Boolean
            Get
                Return hasAttachmentValues.GetValue(417)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(417, value)
            End Set
        End Property

        <Display(Name:="Size (418)", Order:=418)>
        Public Property Size418 As Integer
            Get
                Return sizeValues.GetValue(418)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(418, value)
            End Set
        End Property

        <Display(Name:="Priority (419)", Order:=419), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority419 As Priority
            Get
                Return priorityValues.GetValue(419)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(419, value)
            End Set
        End Property

        <Display(Name:="Subject (420)", Order:=420), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject420 As String
            Get
                Return subjectValues.GetValue(420)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(420, value)
            End Set
        End Property

        <Display(Name:="From (421)", Order:=421)>
        Public Property From421 As String
            Get
                Return fromValues.GetValue(421)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(421, value)
            End Set
        End Property

        <Display(Name:="To (422)", Order:=422)>
        Public Property To422 As String
            Get
                Return toValues.GetValue(422)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(422, value)
            End Set
        End Property

        <Display(Name:="Sent (423)", Order:=423)>
        Public Property Sent423 As Date
            Get
                Return sentValues.GetValue(423)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(423, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (424)", Order:=424)>
        Public Property HasAttachment424 As Boolean
            Get
                Return hasAttachmentValues.GetValue(424)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(424, value)
            End Set
        End Property

        <Display(Name:="Size (425)", Order:=425)>
        Public Property Size425 As Integer
            Get
                Return sizeValues.GetValue(425)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(425, value)
            End Set
        End Property

        <Display(Name:="Priority (426)", Order:=426), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority426 As Priority
            Get
                Return priorityValues.GetValue(426)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(426, value)
            End Set
        End Property

        <Display(Name:="Subject (427)", Order:=427), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject427 As String
            Get
                Return subjectValues.GetValue(427)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(427, value)
            End Set
        End Property

        <Display(Name:="From (428)", Order:=428)>
        Public Property From428 As String
            Get
                Return fromValues.GetValue(428)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(428, value)
            End Set
        End Property

        <Display(Name:="To (429)", Order:=429)>
        Public Property To429 As String
            Get
                Return toValues.GetValue(429)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(429, value)
            End Set
        End Property

        <Display(Name:="Sent (430)", Order:=430)>
        Public Property Sent430 As Date
            Get
                Return sentValues.GetValue(430)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(430, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (431)", Order:=431)>
        Public Property HasAttachment431 As Boolean
            Get
                Return hasAttachmentValues.GetValue(431)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(431, value)
            End Set
        End Property

        <Display(Name:="Size (432)", Order:=432)>
        Public Property Size432 As Integer
            Get
                Return sizeValues.GetValue(432)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(432, value)
            End Set
        End Property

        <Display(Name:="Priority (433)", Order:=433), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority433 As Priority
            Get
                Return priorityValues.GetValue(433)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(433, value)
            End Set
        End Property

        <Display(Name:="Subject (434)", Order:=434), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject434 As String
            Get
                Return subjectValues.GetValue(434)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(434, value)
            End Set
        End Property

        <Display(Name:="From (435)", Order:=435)>
        Public Property From435 As String
            Get
                Return fromValues.GetValue(435)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(435, value)
            End Set
        End Property

        <Display(Name:="To (436)", Order:=436)>
        Public Property To436 As String
            Get
                Return toValues.GetValue(436)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(436, value)
            End Set
        End Property

        <Display(Name:="Sent (437)", Order:=437)>
        Public Property Sent437 As Date
            Get
                Return sentValues.GetValue(437)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(437, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (438)", Order:=438)>
        Public Property HasAttachment438 As Boolean
            Get
                Return hasAttachmentValues.GetValue(438)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(438, value)
            End Set
        End Property

        <Display(Name:="Size (439)", Order:=439)>
        Public Property Size439 As Integer
            Get
                Return sizeValues.GetValue(439)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(439, value)
            End Set
        End Property

        <Display(Name:="Priority (440)", Order:=440), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority440 As Priority
            Get
                Return priorityValues.GetValue(440)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(440, value)
            End Set
        End Property

        <Display(Name:="Subject (441)", Order:=441), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject441 As String
            Get
                Return subjectValues.GetValue(441)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(441, value)
            End Set
        End Property

        <Display(Name:="From (442)", Order:=442)>
        Public Property From442 As String
            Get
                Return fromValues.GetValue(442)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(442, value)
            End Set
        End Property

        <Display(Name:="To (443)", Order:=443)>
        Public Property To443 As String
            Get
                Return toValues.GetValue(443)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(443, value)
            End Set
        End Property

        <Display(Name:="Sent (444)", Order:=444)>
        Public Property Sent444 As Date
            Get
                Return sentValues.GetValue(444)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(444, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (445)", Order:=445)>
        Public Property HasAttachment445 As Boolean
            Get
                Return hasAttachmentValues.GetValue(445)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(445, value)
            End Set
        End Property

        <Display(Name:="Size (446)", Order:=446)>
        Public Property Size446 As Integer
            Get
                Return sizeValues.GetValue(446)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(446, value)
            End Set
        End Property

        <Display(Name:="Priority (447)", Order:=447), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority447 As Priority
            Get
                Return priorityValues.GetValue(447)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(447, value)
            End Set
        End Property

        <Display(Name:="Subject (448)", Order:=448), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject448 As String
            Get
                Return subjectValues.GetValue(448)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(448, value)
            End Set
        End Property

        <Display(Name:="From (449)", Order:=449)>
        Public Property From449 As String
            Get
                Return fromValues.GetValue(449)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(449, value)
            End Set
        End Property

        <Display(Name:="To (450)", Order:=450)>
        Public Property To450 As String
            Get
                Return toValues.GetValue(450)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(450, value)
            End Set
        End Property

        <Display(Name:="Sent (451)", Order:=451)>
        Public Property Sent451 As Date
            Get
                Return sentValues.GetValue(451)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(451, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (452)", Order:=452)>
        Public Property HasAttachment452 As Boolean
            Get
                Return hasAttachmentValues.GetValue(452)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(452, value)
            End Set
        End Property

        <Display(Name:="Size (453)", Order:=453)>
        Public Property Size453 As Integer
            Get
                Return sizeValues.GetValue(453)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(453, value)
            End Set
        End Property

        <Display(Name:="Priority (454)", Order:=454), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority454 As Priority
            Get
                Return priorityValues.GetValue(454)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(454, value)
            End Set
        End Property

        <Display(Name:="Subject (455)", Order:=455), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject455 As String
            Get
                Return subjectValues.GetValue(455)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(455, value)
            End Set
        End Property

        <Display(Name:="From (456)", Order:=456)>
        Public Property From456 As String
            Get
                Return fromValues.GetValue(456)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(456, value)
            End Set
        End Property

        <Display(Name:="To (457)", Order:=457)>
        Public Property To457 As String
            Get
                Return toValues.GetValue(457)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(457, value)
            End Set
        End Property

        <Display(Name:="Sent (458)", Order:=458)>
        Public Property Sent458 As Date
            Get
                Return sentValues.GetValue(458)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(458, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (459)", Order:=459)>
        Public Property HasAttachment459 As Boolean
            Get
                Return hasAttachmentValues.GetValue(459)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(459, value)
            End Set
        End Property

        <Display(Name:="Size (460)", Order:=460)>
        Public Property Size460 As Integer
            Get
                Return sizeValues.GetValue(460)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(460, value)
            End Set
        End Property

        <Display(Name:="Priority (461)", Order:=461), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority461 As Priority
            Get
                Return priorityValues.GetValue(461)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(461, value)
            End Set
        End Property

        <Display(Name:="Subject (462)", Order:=462), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject462 As String
            Get
                Return subjectValues.GetValue(462)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(462, value)
            End Set
        End Property

        <Display(Name:="From (463)", Order:=463)>
        Public Property From463 As String
            Get
                Return fromValues.GetValue(463)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(463, value)
            End Set
        End Property

        <Display(Name:="To (464)", Order:=464)>
        Public Property To464 As String
            Get
                Return toValues.GetValue(464)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(464, value)
            End Set
        End Property

        <Display(Name:="Sent (465)", Order:=465)>
        Public Property Sent465 As Date
            Get
                Return sentValues.GetValue(465)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(465, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (466)", Order:=466)>
        Public Property HasAttachment466 As Boolean
            Get
                Return hasAttachmentValues.GetValue(466)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(466, value)
            End Set
        End Property

        <Display(Name:="Size (467)", Order:=467)>
        Public Property Size467 As Integer
            Get
                Return sizeValues.GetValue(467)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(467, value)
            End Set
        End Property

        <Display(Name:="Priority (468)", Order:=468), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority468 As Priority
            Get
                Return priorityValues.GetValue(468)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(468, value)
            End Set
        End Property

        <Display(Name:="Subject (469)", Order:=469), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject469 As String
            Get
                Return subjectValues.GetValue(469)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(469, value)
            End Set
        End Property

        <Display(Name:="From (470)", Order:=470)>
        Public Property From470 As String
            Get
                Return fromValues.GetValue(470)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(470, value)
            End Set
        End Property

        <Display(Name:="To (471)", Order:=471)>
        Public Property To471 As String
            Get
                Return toValues.GetValue(471)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(471, value)
            End Set
        End Property

        <Display(Name:="Sent (472)", Order:=472)>
        Public Property Sent472 As Date
            Get
                Return sentValues.GetValue(472)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(472, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (473)", Order:=473)>
        Public Property HasAttachment473 As Boolean
            Get
                Return hasAttachmentValues.GetValue(473)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(473, value)
            End Set
        End Property

        <Display(Name:="Size (474)", Order:=474)>
        Public Property Size474 As Integer
            Get
                Return sizeValues.GetValue(474)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(474, value)
            End Set
        End Property

        <Display(Name:="Priority (475)", Order:=475), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority475 As Priority
            Get
                Return priorityValues.GetValue(475)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(475, value)
            End Set
        End Property

        <Display(Name:="Subject (476)", Order:=476), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject476 As String
            Get
                Return subjectValues.GetValue(476)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(476, value)
            End Set
        End Property

        <Display(Name:="From (477)", Order:=477)>
        Public Property From477 As String
            Get
                Return fromValues.GetValue(477)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(477, value)
            End Set
        End Property

        <Display(Name:="To (478)", Order:=478)>
        Public Property To478 As String
            Get
                Return toValues.GetValue(478)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(478, value)
            End Set
        End Property

        <Display(Name:="Sent (479)", Order:=479)>
        Public Property Sent479 As Date
            Get
                Return sentValues.GetValue(479)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(479, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (480)", Order:=480)>
        Public Property HasAttachment480 As Boolean
            Get
                Return hasAttachmentValues.GetValue(480)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(480, value)
            End Set
        End Property

        <Display(Name:="Size (481)", Order:=481)>
        Public Property Size481 As Integer
            Get
                Return sizeValues.GetValue(481)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(481, value)
            End Set
        End Property

        <Display(Name:="Priority (482)", Order:=482), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority482 As Priority
            Get
                Return priorityValues.GetValue(482)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(482, value)
            End Set
        End Property

        <Display(Name:="Subject (483)", Order:=483), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject483 As String
            Get
                Return subjectValues.GetValue(483)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(483, value)
            End Set
        End Property

        <Display(Name:="From (484)", Order:=484)>
        Public Property From484 As String
            Get
                Return fromValues.GetValue(484)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(484, value)
            End Set
        End Property

        <Display(Name:="To (485)", Order:=485)>
        Public Property To485 As String
            Get
                Return toValues.GetValue(485)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(485, value)
            End Set
        End Property

        <Display(Name:="Sent (486)", Order:=486)>
        Public Property Sent486 As Date
            Get
                Return sentValues.GetValue(486)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(486, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (487)", Order:=487)>
        Public Property HasAttachment487 As Boolean
            Get
                Return hasAttachmentValues.GetValue(487)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(487, value)
            End Set
        End Property

        <Display(Name:="Size (488)", Order:=488)>
        Public Property Size488 As Integer
            Get
                Return sizeValues.GetValue(488)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(488, value)
            End Set
        End Property

        <Display(Name:="Priority (489)", Order:=489), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority489 As Priority
            Get
                Return priorityValues.GetValue(489)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(489, value)
            End Set
        End Property

        <Display(Name:="Subject (490)", Order:=490), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject490 As String
            Get
                Return subjectValues.GetValue(490)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(490, value)
            End Set
        End Property

        <Display(Name:="From (491)", Order:=491)>
        Public Property From491 As String
            Get
                Return fromValues.GetValue(491)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(491, value)
            End Set
        End Property

        <Display(Name:="To (492)", Order:=492)>
        Public Property To492 As String
            Get
                Return toValues.GetValue(492)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(492, value)
            End Set
        End Property

        <Display(Name:="Sent (493)", Order:=493)>
        Public Property Sent493 As Date
            Get
                Return sentValues.GetValue(493)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(493, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (494)", Order:=494)>
        Public Property HasAttachment494 As Boolean
            Get
                Return hasAttachmentValues.GetValue(494)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(494, value)
            End Set
        End Property

        <Display(Name:="Size (495)", Order:=495)>
        Public Property Size495 As Integer
            Get
                Return sizeValues.GetValue(495)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(495, value)
            End Set
        End Property

        <Display(Name:="Priority (496)", Order:=496), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority496 As Priority
            Get
                Return priorityValues.GetValue(496)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(496, value)
            End Set
        End Property

        <Display(Name:="Subject (497)", Order:=497), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject497 As String
            Get
                Return subjectValues.GetValue(497)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(497, value)
            End Set
        End Property

        <Display(Name:="From (498)", Order:=498)>
        Public Property From498 As String
            Get
                Return fromValues.GetValue(498)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(498, value)
            End Set
        End Property

        <Display(Name:="To (499)", Order:=499)>
        Public Property To499 As String
            Get
                Return toValues.GetValue(499)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(499, value)
            End Set
        End Property
#End Region
    End Class

    Public Class LargeDataSourceObjectImmense
        Inherits LargeDataSourceObjectLarge

        Public Sub New(ByVal id As Integer)
            MyBase.New(id)
        End Sub

#Region "properties"
        <Display(Name:="Sent (500)", Order:=500)>
        Public Property Sent500 As Date
            Get
                Return sentValues.GetValue(500)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(500, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (501)", Order:=501)>
        Public Property HasAttachment501 As Boolean
            Get
                Return hasAttachmentValues.GetValue(501)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(501, value)
            End Set
        End Property

        <Display(Name:="Size (502)", Order:=502)>
        Public Property Size502 As Integer
            Get
                Return sizeValues.GetValue(502)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(502, value)
            End Set
        End Property

        <Display(Name:="Priority (503)", Order:=503), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority503 As Priority
            Get
                Return priorityValues.GetValue(503)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(503, value)
            End Set
        End Property

        <Display(Name:="Subject (504)", Order:=504), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject504 As String
            Get
                Return subjectValues.GetValue(504)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(504, value)
            End Set
        End Property

        <Display(Name:="From (505)", Order:=505)>
        Public Property From505 As String
            Get
                Return fromValues.GetValue(505)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(505, value)
            End Set
        End Property

        <Display(Name:="To (506)", Order:=506)>
        Public Property To506 As String
            Get
                Return toValues.GetValue(506)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(506, value)
            End Set
        End Property

        <Display(Name:="Sent (507)", Order:=507)>
        Public Property Sent507 As Date
            Get
                Return sentValues.GetValue(507)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(507, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (508)", Order:=508)>
        Public Property HasAttachment508 As Boolean
            Get
                Return hasAttachmentValues.GetValue(508)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(508, value)
            End Set
        End Property

        <Display(Name:="Size (509)", Order:=509)>
        Public Property Size509 As Integer
            Get
                Return sizeValues.GetValue(509)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(509, value)
            End Set
        End Property

        <Display(Name:="Priority (510)", Order:=510), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority510 As Priority
            Get
                Return priorityValues.GetValue(510)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(510, value)
            End Set
        End Property

        <Display(Name:="Subject (511)", Order:=511), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject511 As String
            Get
                Return subjectValues.GetValue(511)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(511, value)
            End Set
        End Property

        <Display(Name:="From (512)", Order:=512)>
        Public Property From512 As String
            Get
                Return fromValues.GetValue(512)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(512, value)
            End Set
        End Property

        <Display(Name:="To (513)", Order:=513)>
        Public Property To513 As String
            Get
                Return toValues.GetValue(513)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(513, value)
            End Set
        End Property

        <Display(Name:="Sent (514)", Order:=514)>
        Public Property Sent514 As Date
            Get
                Return sentValues.GetValue(514)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(514, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (515)", Order:=515)>
        Public Property HasAttachment515 As Boolean
            Get
                Return hasAttachmentValues.GetValue(515)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(515, value)
            End Set
        End Property

        <Display(Name:="Size (516)", Order:=516)>
        Public Property Size516 As Integer
            Get
                Return sizeValues.GetValue(516)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(516, value)
            End Set
        End Property

        <Display(Name:="Priority (517)", Order:=517), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority517 As Priority
            Get
                Return priorityValues.GetValue(517)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(517, value)
            End Set
        End Property

        <Display(Name:="Subject (518)", Order:=518), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject518 As String
            Get
                Return subjectValues.GetValue(518)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(518, value)
            End Set
        End Property

        <Display(Name:="From (519)", Order:=519)>
        Public Property From519 As String
            Get
                Return fromValues.GetValue(519)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(519, value)
            End Set
        End Property

        <Display(Name:="To (520)", Order:=520)>
        Public Property To520 As String
            Get
                Return toValues.GetValue(520)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(520, value)
            End Set
        End Property

        <Display(Name:="Sent (521)", Order:=521)>
        Public Property Sent521 As Date
            Get
                Return sentValues.GetValue(521)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(521, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (522)", Order:=522)>
        Public Property HasAttachment522 As Boolean
            Get
                Return hasAttachmentValues.GetValue(522)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(522, value)
            End Set
        End Property

        <Display(Name:="Size (523)", Order:=523)>
        Public Property Size523 As Integer
            Get
                Return sizeValues.GetValue(523)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(523, value)
            End Set
        End Property

        <Display(Name:="Priority (524)", Order:=524), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority524 As Priority
            Get
                Return priorityValues.GetValue(524)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(524, value)
            End Set
        End Property

        <Display(Name:="Subject (525)", Order:=525), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject525 As String
            Get
                Return subjectValues.GetValue(525)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(525, value)
            End Set
        End Property

        <Display(Name:="From (526)", Order:=526)>
        Public Property From526 As String
            Get
                Return fromValues.GetValue(526)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(526, value)
            End Set
        End Property

        <Display(Name:="To (527)", Order:=527)>
        Public Property To527 As String
            Get
                Return toValues.GetValue(527)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(527, value)
            End Set
        End Property

        <Display(Name:="Sent (528)", Order:=528)>
        Public Property Sent528 As Date
            Get
                Return sentValues.GetValue(528)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(528, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (529)", Order:=529)>
        Public Property HasAttachment529 As Boolean
            Get
                Return hasAttachmentValues.GetValue(529)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(529, value)
            End Set
        End Property

        <Display(Name:="Size (530)", Order:=530)>
        Public Property Size530 As Integer
            Get
                Return sizeValues.GetValue(530)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(530, value)
            End Set
        End Property

        <Display(Name:="Priority (531)", Order:=531), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority531 As Priority
            Get
                Return priorityValues.GetValue(531)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(531, value)
            End Set
        End Property

        <Display(Name:="Subject (532)", Order:=532), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject532 As String
            Get
                Return subjectValues.GetValue(532)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(532, value)
            End Set
        End Property

        <Display(Name:="From (533)", Order:=533)>
        Public Property From533 As String
            Get
                Return fromValues.GetValue(533)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(533, value)
            End Set
        End Property

        <Display(Name:="To (534)", Order:=534)>
        Public Property To534 As String
            Get
                Return toValues.GetValue(534)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(534, value)
            End Set
        End Property

        <Display(Name:="Sent (535)", Order:=535)>
        Public Property Sent535 As Date
            Get
                Return sentValues.GetValue(535)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(535, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (536)", Order:=536)>
        Public Property HasAttachment536 As Boolean
            Get
                Return hasAttachmentValues.GetValue(536)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(536, value)
            End Set
        End Property

        <Display(Name:="Size (537)", Order:=537)>
        Public Property Size537 As Integer
            Get
                Return sizeValues.GetValue(537)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(537, value)
            End Set
        End Property

        <Display(Name:="Priority (538)", Order:=538), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority538 As Priority
            Get
                Return priorityValues.GetValue(538)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(538, value)
            End Set
        End Property

        <Display(Name:="Subject (539)", Order:=539), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject539 As String
            Get
                Return subjectValues.GetValue(539)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(539, value)
            End Set
        End Property

        <Display(Name:="From (540)", Order:=540)>
        Public Property From540 As String
            Get
                Return fromValues.GetValue(540)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(540, value)
            End Set
        End Property

        <Display(Name:="To (541)", Order:=541)>
        Public Property To541 As String
            Get
                Return toValues.GetValue(541)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(541, value)
            End Set
        End Property

        <Display(Name:="Sent (542)", Order:=542)>
        Public Property Sent542 As Date
            Get
                Return sentValues.GetValue(542)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(542, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (543)", Order:=543)>
        Public Property HasAttachment543 As Boolean
            Get
                Return hasAttachmentValues.GetValue(543)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(543, value)
            End Set
        End Property

        <Display(Name:="Size (544)", Order:=544)>
        Public Property Size544 As Integer
            Get
                Return sizeValues.GetValue(544)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(544, value)
            End Set
        End Property

        <Display(Name:="Priority (545)", Order:=545), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority545 As Priority
            Get
                Return priorityValues.GetValue(545)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(545, value)
            End Set
        End Property

        <Display(Name:="Subject (546)", Order:=546), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject546 As String
            Get
                Return subjectValues.GetValue(546)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(546, value)
            End Set
        End Property

        <Display(Name:="From (547)", Order:=547)>
        Public Property From547 As String
            Get
                Return fromValues.GetValue(547)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(547, value)
            End Set
        End Property

        <Display(Name:="To (548)", Order:=548)>
        Public Property To548 As String
            Get
                Return toValues.GetValue(548)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(548, value)
            End Set
        End Property

        <Display(Name:="Sent (549)", Order:=549)>
        Public Property Sent549 As Date
            Get
                Return sentValues.GetValue(549)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(549, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (550)", Order:=550)>
        Public Property HasAttachment550 As Boolean
            Get
                Return hasAttachmentValues.GetValue(550)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(550, value)
            End Set
        End Property

        <Display(Name:="Size (551)", Order:=551)>
        Public Property Size551 As Integer
            Get
                Return sizeValues.GetValue(551)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(551, value)
            End Set
        End Property

        <Display(Name:="Priority (552)", Order:=552), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority552 As Priority
            Get
                Return priorityValues.GetValue(552)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(552, value)
            End Set
        End Property

        <Display(Name:="Subject (553)", Order:=553), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject553 As String
            Get
                Return subjectValues.GetValue(553)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(553, value)
            End Set
        End Property

        <Display(Name:="From (554)", Order:=554)>
        Public Property From554 As String
            Get
                Return fromValues.GetValue(554)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(554, value)
            End Set
        End Property

        <Display(Name:="To (555)", Order:=555)>
        Public Property To555 As String
            Get
                Return toValues.GetValue(555)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(555, value)
            End Set
        End Property

        <Display(Name:="Sent (556)", Order:=556)>
        Public Property Sent556 As Date
            Get
                Return sentValues.GetValue(556)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(556, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (557)", Order:=557)>
        Public Property HasAttachment557 As Boolean
            Get
                Return hasAttachmentValues.GetValue(557)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(557, value)
            End Set
        End Property

        <Display(Name:="Size (558)", Order:=558)>
        Public Property Size558 As Integer
            Get
                Return sizeValues.GetValue(558)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(558, value)
            End Set
        End Property

        <Display(Name:="Priority (559)", Order:=559), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority559 As Priority
            Get
                Return priorityValues.GetValue(559)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(559, value)
            End Set
        End Property

        <Display(Name:="Subject (560)", Order:=560), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject560 As String
            Get
                Return subjectValues.GetValue(560)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(560, value)
            End Set
        End Property

        <Display(Name:="From (561)", Order:=561)>
        Public Property From561 As String
            Get
                Return fromValues.GetValue(561)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(561, value)
            End Set
        End Property

        <Display(Name:="To (562)", Order:=562)>
        Public Property To562 As String
            Get
                Return toValues.GetValue(562)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(562, value)
            End Set
        End Property

        <Display(Name:="Sent (563)", Order:=563)>
        Public Property Sent563 As Date
            Get
                Return sentValues.GetValue(563)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(563, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (564)", Order:=564)>
        Public Property HasAttachment564 As Boolean
            Get
                Return hasAttachmentValues.GetValue(564)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(564, value)
            End Set
        End Property

        <Display(Name:="Size (565)", Order:=565)>
        Public Property Size565 As Integer
            Get
                Return sizeValues.GetValue(565)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(565, value)
            End Set
        End Property

        <Display(Name:="Priority (566)", Order:=566), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority566 As Priority
            Get
                Return priorityValues.GetValue(566)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(566, value)
            End Set
        End Property

        <Display(Name:="Subject (567)", Order:=567), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject567 As String
            Get
                Return subjectValues.GetValue(567)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(567, value)
            End Set
        End Property

        <Display(Name:="From (568)", Order:=568)>
        Public Property From568 As String
            Get
                Return fromValues.GetValue(568)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(568, value)
            End Set
        End Property

        <Display(Name:="To (569)", Order:=569)>
        Public Property To569 As String
            Get
                Return toValues.GetValue(569)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(569, value)
            End Set
        End Property

        <Display(Name:="Sent (570)", Order:=570)>
        Public Property Sent570 As Date
            Get
                Return sentValues.GetValue(570)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(570, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (571)", Order:=571)>
        Public Property HasAttachment571 As Boolean
            Get
                Return hasAttachmentValues.GetValue(571)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(571, value)
            End Set
        End Property

        <Display(Name:="Size (572)", Order:=572)>
        Public Property Size572 As Integer
            Get
                Return sizeValues.GetValue(572)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(572, value)
            End Set
        End Property

        <Display(Name:="Priority (573)", Order:=573), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority573 As Priority
            Get
                Return priorityValues.GetValue(573)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(573, value)
            End Set
        End Property

        <Display(Name:="Subject (574)", Order:=574), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject574 As String
            Get
                Return subjectValues.GetValue(574)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(574, value)
            End Set
        End Property

        <Display(Name:="From (575)", Order:=575)>
        Public Property From575 As String
            Get
                Return fromValues.GetValue(575)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(575, value)
            End Set
        End Property

        <Display(Name:="To (576)", Order:=576)>
        Public Property To576 As String
            Get
                Return toValues.GetValue(576)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(576, value)
            End Set
        End Property

        <Display(Name:="Sent (577)", Order:=577)>
        Public Property Sent577 As Date
            Get
                Return sentValues.GetValue(577)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(577, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (578)", Order:=578)>
        Public Property HasAttachment578 As Boolean
            Get
                Return hasAttachmentValues.GetValue(578)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(578, value)
            End Set
        End Property

        <Display(Name:="Size (579)", Order:=579)>
        Public Property Size579 As Integer
            Get
                Return sizeValues.GetValue(579)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(579, value)
            End Set
        End Property

        <Display(Name:="Priority (580)", Order:=580), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority580 As Priority
            Get
                Return priorityValues.GetValue(580)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(580, value)
            End Set
        End Property

        <Display(Name:="Subject (581)", Order:=581), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject581 As String
            Get
                Return subjectValues.GetValue(581)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(581, value)
            End Set
        End Property

        <Display(Name:="From (582)", Order:=582)>
        Public Property From582 As String
            Get
                Return fromValues.GetValue(582)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(582, value)
            End Set
        End Property

        <Display(Name:="To (583)", Order:=583)>
        Public Property To583 As String
            Get
                Return toValues.GetValue(583)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(583, value)
            End Set
        End Property

        <Display(Name:="Sent (584)", Order:=584)>
        Public Property Sent584 As Date
            Get
                Return sentValues.GetValue(584)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(584, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (585)", Order:=585)>
        Public Property HasAttachment585 As Boolean
            Get
                Return hasAttachmentValues.GetValue(585)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(585, value)
            End Set
        End Property

        <Display(Name:="Size (586)", Order:=586)>
        Public Property Size586 As Integer
            Get
                Return sizeValues.GetValue(586)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(586, value)
            End Set
        End Property

        <Display(Name:="Priority (587)", Order:=587), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority587 As Priority
            Get
                Return priorityValues.GetValue(587)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(587, value)
            End Set
        End Property

        <Display(Name:="Subject (588)", Order:=588), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject588 As String
            Get
                Return subjectValues.GetValue(588)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(588, value)
            End Set
        End Property

        <Display(Name:="From (589)", Order:=589)>
        Public Property From589 As String
            Get
                Return fromValues.GetValue(589)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(589, value)
            End Set
        End Property

        <Display(Name:="To (590)", Order:=590)>
        Public Property To590 As String
            Get
                Return toValues.GetValue(590)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(590, value)
            End Set
        End Property

        <Display(Name:="Sent (591)", Order:=591)>
        Public Property Sent591 As Date
            Get
                Return sentValues.GetValue(591)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(591, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (592)", Order:=592)>
        Public Property HasAttachment592 As Boolean
            Get
                Return hasAttachmentValues.GetValue(592)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(592, value)
            End Set
        End Property

        <Display(Name:="Size (593)", Order:=593)>
        Public Property Size593 As Integer
            Get
                Return sizeValues.GetValue(593)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(593, value)
            End Set
        End Property

        <Display(Name:="Priority (594)", Order:=594), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority594 As Priority
            Get
                Return priorityValues.GetValue(594)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(594, value)
            End Set
        End Property

        <Display(Name:="Subject (595)", Order:=595), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject595 As String
            Get
                Return subjectValues.GetValue(595)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(595, value)
            End Set
        End Property

        <Display(Name:="From (596)", Order:=596)>
        Public Property From596 As String
            Get
                Return fromValues.GetValue(596)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(596, value)
            End Set
        End Property

        <Display(Name:="To (597)", Order:=597)>
        Public Property To597 As String
            Get
                Return toValues.GetValue(597)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(597, value)
            End Set
        End Property

        <Display(Name:="Sent (598)", Order:=598)>
        Public Property Sent598 As Date
            Get
                Return sentValues.GetValue(598)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(598, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (599)", Order:=599)>
        Public Property HasAttachment599 As Boolean
            Get
                Return hasAttachmentValues.GetValue(599)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(599, value)
            End Set
        End Property

        <Display(Name:="Size (600)", Order:=600)>
        Public Property Size600 As Integer
            Get
                Return sizeValues.GetValue(600)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(600, value)
            End Set
        End Property

        <Display(Name:="Priority (601)", Order:=601), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority601 As Priority
            Get
                Return priorityValues.GetValue(601)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(601, value)
            End Set
        End Property

        <Display(Name:="Subject (602)", Order:=602), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject602 As String
            Get
                Return subjectValues.GetValue(602)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(602, value)
            End Set
        End Property

        <Display(Name:="From (603)", Order:=603)>
        Public Property From603 As String
            Get
                Return fromValues.GetValue(603)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(603, value)
            End Set
        End Property

        <Display(Name:="To (604)", Order:=604)>
        Public Property To604 As String
            Get
                Return toValues.GetValue(604)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(604, value)
            End Set
        End Property

        <Display(Name:="Sent (605)", Order:=605)>
        Public Property Sent605 As Date
            Get
                Return sentValues.GetValue(605)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(605, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (606)", Order:=606)>
        Public Property HasAttachment606 As Boolean
            Get
                Return hasAttachmentValues.GetValue(606)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(606, value)
            End Set
        End Property

        <Display(Name:="Size (607)", Order:=607)>
        Public Property Size607 As Integer
            Get
                Return sizeValues.GetValue(607)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(607, value)
            End Set
        End Property

        <Display(Name:="Priority (608)", Order:=608), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority608 As Priority
            Get
                Return priorityValues.GetValue(608)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(608, value)
            End Set
        End Property

        <Display(Name:="Subject (609)", Order:=609), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject609 As String
            Get
                Return subjectValues.GetValue(609)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(609, value)
            End Set
        End Property

        <Display(Name:="From (610)", Order:=610)>
        Public Property From610 As String
            Get
                Return fromValues.GetValue(610)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(610, value)
            End Set
        End Property

        <Display(Name:="To (611)", Order:=611)>
        Public Property To611 As String
            Get
                Return toValues.GetValue(611)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(611, value)
            End Set
        End Property

        <Display(Name:="Sent (612)", Order:=612)>
        Public Property Sent612 As Date
            Get
                Return sentValues.GetValue(612)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(612, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (613)", Order:=613)>
        Public Property HasAttachment613 As Boolean
            Get
                Return hasAttachmentValues.GetValue(613)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(613, value)
            End Set
        End Property

        <Display(Name:="Size (614)", Order:=614)>
        Public Property Size614 As Integer
            Get
                Return sizeValues.GetValue(614)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(614, value)
            End Set
        End Property

        <Display(Name:="Priority (615)", Order:=615), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority615 As Priority
            Get
                Return priorityValues.GetValue(615)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(615, value)
            End Set
        End Property

        <Display(Name:="Subject (616)", Order:=616), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject616 As String
            Get
                Return subjectValues.GetValue(616)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(616, value)
            End Set
        End Property

        <Display(Name:="From (617)", Order:=617)>
        Public Property From617 As String
            Get
                Return fromValues.GetValue(617)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(617, value)
            End Set
        End Property

        <Display(Name:="To (618)", Order:=618)>
        Public Property To618 As String
            Get
                Return toValues.GetValue(618)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(618, value)
            End Set
        End Property

        <Display(Name:="Sent (619)", Order:=619)>
        Public Property Sent619 As Date
            Get
                Return sentValues.GetValue(619)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(619, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (620)", Order:=620)>
        Public Property HasAttachment620 As Boolean
            Get
                Return hasAttachmentValues.GetValue(620)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(620, value)
            End Set
        End Property

        <Display(Name:="Size (621)", Order:=621)>
        Public Property Size621 As Integer
            Get
                Return sizeValues.GetValue(621)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(621, value)
            End Set
        End Property

        <Display(Name:="Priority (622)", Order:=622), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority622 As Priority
            Get
                Return priorityValues.GetValue(622)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(622, value)
            End Set
        End Property

        <Display(Name:="Subject (623)", Order:=623), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject623 As String
            Get
                Return subjectValues.GetValue(623)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(623, value)
            End Set
        End Property

        <Display(Name:="From (624)", Order:=624)>
        Public Property From624 As String
            Get
                Return fromValues.GetValue(624)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(624, value)
            End Set
        End Property

        <Display(Name:="To (625)", Order:=625)>
        Public Property To625 As String
            Get
                Return toValues.GetValue(625)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(625, value)
            End Set
        End Property

        <Display(Name:="Sent (626)", Order:=626)>
        Public Property Sent626 As Date
            Get
                Return sentValues.GetValue(626)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(626, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (627)", Order:=627)>
        Public Property HasAttachment627 As Boolean
            Get
                Return hasAttachmentValues.GetValue(627)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(627, value)
            End Set
        End Property

        <Display(Name:="Size (628)", Order:=628)>
        Public Property Size628 As Integer
            Get
                Return sizeValues.GetValue(628)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(628, value)
            End Set
        End Property

        <Display(Name:="Priority (629)", Order:=629), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority629 As Priority
            Get
                Return priorityValues.GetValue(629)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(629, value)
            End Set
        End Property

        <Display(Name:="Subject (630)", Order:=630), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject630 As String
            Get
                Return subjectValues.GetValue(630)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(630, value)
            End Set
        End Property

        <Display(Name:="From (631)", Order:=631)>
        Public Property From631 As String
            Get
                Return fromValues.GetValue(631)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(631, value)
            End Set
        End Property

        <Display(Name:="To (632)", Order:=632)>
        Public Property To632 As String
            Get
                Return toValues.GetValue(632)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(632, value)
            End Set
        End Property

        <Display(Name:="Sent (633)", Order:=633)>
        Public Property Sent633 As Date
            Get
                Return sentValues.GetValue(633)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(633, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (634)", Order:=634)>
        Public Property HasAttachment634 As Boolean
            Get
                Return hasAttachmentValues.GetValue(634)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(634, value)
            End Set
        End Property

        <Display(Name:="Size (635)", Order:=635)>
        Public Property Size635 As Integer
            Get
                Return sizeValues.GetValue(635)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(635, value)
            End Set
        End Property

        <Display(Name:="Priority (636)", Order:=636), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority636 As Priority
            Get
                Return priorityValues.GetValue(636)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(636, value)
            End Set
        End Property

        <Display(Name:="Subject (637)", Order:=637), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject637 As String
            Get
                Return subjectValues.GetValue(637)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(637, value)
            End Set
        End Property

        <Display(Name:="From (638)", Order:=638)>
        Public Property From638 As String
            Get
                Return fromValues.GetValue(638)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(638, value)
            End Set
        End Property

        <Display(Name:="To (639)", Order:=639)>
        Public Property To639 As String
            Get
                Return toValues.GetValue(639)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(639, value)
            End Set
        End Property

        <Display(Name:="Sent (640)", Order:=640)>
        Public Property Sent640 As Date
            Get
                Return sentValues.GetValue(640)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(640, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (641)", Order:=641)>
        Public Property HasAttachment641 As Boolean
            Get
                Return hasAttachmentValues.GetValue(641)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(641, value)
            End Set
        End Property

        <Display(Name:="Size (642)", Order:=642)>
        Public Property Size642 As Integer
            Get
                Return sizeValues.GetValue(642)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(642, value)
            End Set
        End Property

        <Display(Name:="Priority (643)", Order:=643), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority643 As Priority
            Get
                Return priorityValues.GetValue(643)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(643, value)
            End Set
        End Property

        <Display(Name:="Subject (644)", Order:=644), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject644 As String
            Get
                Return subjectValues.GetValue(644)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(644, value)
            End Set
        End Property

        <Display(Name:="From (645)", Order:=645)>
        Public Property From645 As String
            Get
                Return fromValues.GetValue(645)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(645, value)
            End Set
        End Property

        <Display(Name:="To (646)", Order:=646)>
        Public Property To646 As String
            Get
                Return toValues.GetValue(646)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(646, value)
            End Set
        End Property

        <Display(Name:="Sent (647)", Order:=647)>
        Public Property Sent647 As Date
            Get
                Return sentValues.GetValue(647)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(647, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (648)", Order:=648)>
        Public Property HasAttachment648 As Boolean
            Get
                Return hasAttachmentValues.GetValue(648)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(648, value)
            End Set
        End Property

        <Display(Name:="Size (649)", Order:=649)>
        Public Property Size649 As Integer
            Get
                Return sizeValues.GetValue(649)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(649, value)
            End Set
        End Property

        <Display(Name:="Priority (650)", Order:=650), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority650 As Priority
            Get
                Return priorityValues.GetValue(650)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(650, value)
            End Set
        End Property

        <Display(Name:="Subject (651)", Order:=651), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject651 As String
            Get
                Return subjectValues.GetValue(651)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(651, value)
            End Set
        End Property

        <Display(Name:="From (652)", Order:=652)>
        Public Property From652 As String
            Get
                Return fromValues.GetValue(652)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(652, value)
            End Set
        End Property

        <Display(Name:="To (653)", Order:=653)>
        Public Property To653 As String
            Get
                Return toValues.GetValue(653)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(653, value)
            End Set
        End Property

        <Display(Name:="Sent (654)", Order:=654)>
        Public Property Sent654 As Date
            Get
                Return sentValues.GetValue(654)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(654, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (655)", Order:=655)>
        Public Property HasAttachment655 As Boolean
            Get
                Return hasAttachmentValues.GetValue(655)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(655, value)
            End Set
        End Property

        <Display(Name:="Size (656)", Order:=656)>
        Public Property Size656 As Integer
            Get
                Return sizeValues.GetValue(656)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(656, value)
            End Set
        End Property

        <Display(Name:="Priority (657)", Order:=657), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority657 As Priority
            Get
                Return priorityValues.GetValue(657)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(657, value)
            End Set
        End Property

        <Display(Name:="Subject (658)", Order:=658), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject658 As String
            Get
                Return subjectValues.GetValue(658)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(658, value)
            End Set
        End Property

        <Display(Name:="From (659)", Order:=659)>
        Public Property From659 As String
            Get
                Return fromValues.GetValue(659)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(659, value)
            End Set
        End Property

        <Display(Name:="To (660)", Order:=660)>
        Public Property To660 As String
            Get
                Return toValues.GetValue(660)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(660, value)
            End Set
        End Property

        <Display(Name:="Sent (661)", Order:=661)>
        Public Property Sent661 As Date
            Get
                Return sentValues.GetValue(661)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(661, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (662)", Order:=662)>
        Public Property HasAttachment662 As Boolean
            Get
                Return hasAttachmentValues.GetValue(662)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(662, value)
            End Set
        End Property

        <Display(Name:="Size (663)", Order:=663)>
        Public Property Size663 As Integer
            Get
                Return sizeValues.GetValue(663)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(663, value)
            End Set
        End Property

        <Display(Name:="Priority (664)", Order:=664), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority664 As Priority
            Get
                Return priorityValues.GetValue(664)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(664, value)
            End Set
        End Property

        <Display(Name:="Subject (665)", Order:=665), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject665 As String
            Get
                Return subjectValues.GetValue(665)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(665, value)
            End Set
        End Property

        <Display(Name:="From (666)", Order:=666)>
        Public Property From666 As String
            Get
                Return fromValues.GetValue(666)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(666, value)
            End Set
        End Property

        <Display(Name:="To (667)", Order:=667)>
        Public Property To667 As String
            Get
                Return toValues.GetValue(667)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(667, value)
            End Set
        End Property

        <Display(Name:="Sent (668)", Order:=668)>
        Public Property Sent668 As Date
            Get
                Return sentValues.GetValue(668)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(668, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (669)", Order:=669)>
        Public Property HasAttachment669 As Boolean
            Get
                Return hasAttachmentValues.GetValue(669)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(669, value)
            End Set
        End Property

        <Display(Name:="Size (670)", Order:=670)>
        Public Property Size670 As Integer
            Get
                Return sizeValues.GetValue(670)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(670, value)
            End Set
        End Property

        <Display(Name:="Priority (671)", Order:=671), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority671 As Priority
            Get
                Return priorityValues.GetValue(671)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(671, value)
            End Set
        End Property

        <Display(Name:="Subject (672)", Order:=672), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject672 As String
            Get
                Return subjectValues.GetValue(672)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(672, value)
            End Set
        End Property

        <Display(Name:="From (673)", Order:=673)>
        Public Property From673 As String
            Get
                Return fromValues.GetValue(673)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(673, value)
            End Set
        End Property

        <Display(Name:="To (674)", Order:=674)>
        Public Property To674 As String
            Get
                Return toValues.GetValue(674)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(674, value)
            End Set
        End Property

        <Display(Name:="Sent (675)", Order:=675)>
        Public Property Sent675 As Date
            Get
                Return sentValues.GetValue(675)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(675, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (676)", Order:=676)>
        Public Property HasAttachment676 As Boolean
            Get
                Return hasAttachmentValues.GetValue(676)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(676, value)
            End Set
        End Property

        <Display(Name:="Size (677)", Order:=677)>
        Public Property Size677 As Integer
            Get
                Return sizeValues.GetValue(677)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(677, value)
            End Set
        End Property

        <Display(Name:="Priority (678)", Order:=678), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority678 As Priority
            Get
                Return priorityValues.GetValue(678)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(678, value)
            End Set
        End Property

        <Display(Name:="Subject (679)", Order:=679), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject679 As String
            Get
                Return subjectValues.GetValue(679)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(679, value)
            End Set
        End Property

        <Display(Name:="From (680)", Order:=680)>
        Public Property From680 As String
            Get
                Return fromValues.GetValue(680)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(680, value)
            End Set
        End Property

        <Display(Name:="To (681)", Order:=681)>
        Public Property To681 As String
            Get
                Return toValues.GetValue(681)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(681, value)
            End Set
        End Property

        <Display(Name:="Sent (682)", Order:=682)>
        Public Property Sent682 As Date
            Get
                Return sentValues.GetValue(682)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(682, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (683)", Order:=683)>
        Public Property HasAttachment683 As Boolean
            Get
                Return hasAttachmentValues.GetValue(683)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(683, value)
            End Set
        End Property

        <Display(Name:="Size (684)", Order:=684)>
        Public Property Size684 As Integer
            Get
                Return sizeValues.GetValue(684)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(684, value)
            End Set
        End Property

        <Display(Name:="Priority (685)", Order:=685), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority685 As Priority
            Get
                Return priorityValues.GetValue(685)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(685, value)
            End Set
        End Property

        <Display(Name:="Subject (686)", Order:=686), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject686 As String
            Get
                Return subjectValues.GetValue(686)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(686, value)
            End Set
        End Property

        <Display(Name:="From (687)", Order:=687)>
        Public Property From687 As String
            Get
                Return fromValues.GetValue(687)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(687, value)
            End Set
        End Property

        <Display(Name:="To (688)", Order:=688)>
        Public Property To688 As String
            Get
                Return toValues.GetValue(688)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(688, value)
            End Set
        End Property

        <Display(Name:="Sent (689)", Order:=689)>
        Public Property Sent689 As Date
            Get
                Return sentValues.GetValue(689)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(689, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (690)", Order:=690)>
        Public Property HasAttachment690 As Boolean
            Get
                Return hasAttachmentValues.GetValue(690)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(690, value)
            End Set
        End Property

        <Display(Name:="Size (691)", Order:=691)>
        Public Property Size691 As Integer
            Get
                Return sizeValues.GetValue(691)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(691, value)
            End Set
        End Property

        <Display(Name:="Priority (692)", Order:=692), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority692 As Priority
            Get
                Return priorityValues.GetValue(692)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(692, value)
            End Set
        End Property

        <Display(Name:="Subject (693)", Order:=693), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject693 As String
            Get
                Return subjectValues.GetValue(693)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(693, value)
            End Set
        End Property

        <Display(Name:="From (694)", Order:=694)>
        Public Property From694 As String
            Get
                Return fromValues.GetValue(694)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(694, value)
            End Set
        End Property

        <Display(Name:="To (695)", Order:=695)>
        Public Property To695 As String
            Get
                Return toValues.GetValue(695)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(695, value)
            End Set
        End Property

        <Display(Name:="Sent (696)", Order:=696)>
        Public Property Sent696 As Date
            Get
                Return sentValues.GetValue(696)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(696, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (697)", Order:=697)>
        Public Property HasAttachment697 As Boolean
            Get
                Return hasAttachmentValues.GetValue(697)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(697, value)
            End Set
        End Property

        <Display(Name:="Size (698)", Order:=698)>
        Public Property Size698 As Integer
            Get
                Return sizeValues.GetValue(698)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(698, value)
            End Set
        End Property

        <Display(Name:="Priority (699)", Order:=699), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority699 As Priority
            Get
                Return priorityValues.GetValue(699)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(699, value)
            End Set
        End Property

        <Display(Name:="Subject (700)", Order:=700), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject700 As String
            Get
                Return subjectValues.GetValue(700)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(700, value)
            End Set
        End Property

        <Display(Name:="From (701)", Order:=701)>
        Public Property From701 As String
            Get
                Return fromValues.GetValue(701)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(701, value)
            End Set
        End Property

        <Display(Name:="To (702)", Order:=702)>
        Public Property To702 As String
            Get
                Return toValues.GetValue(702)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(702, value)
            End Set
        End Property

        <Display(Name:="Sent (703)", Order:=703)>
        Public Property Sent703 As Date
            Get
                Return sentValues.GetValue(703)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(703, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (704)", Order:=704)>
        Public Property HasAttachment704 As Boolean
            Get
                Return hasAttachmentValues.GetValue(704)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(704, value)
            End Set
        End Property

        <Display(Name:="Size (705)", Order:=705)>
        Public Property Size705 As Integer
            Get
                Return sizeValues.GetValue(705)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(705, value)
            End Set
        End Property

        <Display(Name:="Priority (706)", Order:=706), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority706 As Priority
            Get
                Return priorityValues.GetValue(706)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(706, value)
            End Set
        End Property

        <Display(Name:="Subject (707)", Order:=707), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject707 As String
            Get
                Return subjectValues.GetValue(707)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(707, value)
            End Set
        End Property

        <Display(Name:="From (708)", Order:=708)>
        Public Property From708 As String
            Get
                Return fromValues.GetValue(708)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(708, value)
            End Set
        End Property

        <Display(Name:="To (709)", Order:=709)>
        Public Property To709 As String
            Get
                Return toValues.GetValue(709)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(709, value)
            End Set
        End Property

        <Display(Name:="Sent (710)", Order:=710)>
        Public Property Sent710 As Date
            Get
                Return sentValues.GetValue(710)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(710, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (711)", Order:=711)>
        Public Property HasAttachment711 As Boolean
            Get
                Return hasAttachmentValues.GetValue(711)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(711, value)
            End Set
        End Property

        <Display(Name:="Size (712)", Order:=712)>
        Public Property Size712 As Integer
            Get
                Return sizeValues.GetValue(712)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(712, value)
            End Set
        End Property

        <Display(Name:="Priority (713)", Order:=713), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority713 As Priority
            Get
                Return priorityValues.GetValue(713)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(713, value)
            End Set
        End Property

        <Display(Name:="Subject (714)", Order:=714), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject714 As String
            Get
                Return subjectValues.GetValue(714)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(714, value)
            End Set
        End Property

        <Display(Name:="From (715)", Order:=715)>
        Public Property From715 As String
            Get
                Return fromValues.GetValue(715)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(715, value)
            End Set
        End Property

        <Display(Name:="To (716)", Order:=716)>
        Public Property To716 As String
            Get
                Return toValues.GetValue(716)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(716, value)
            End Set
        End Property

        <Display(Name:="Sent (717)", Order:=717)>
        Public Property Sent717 As Date
            Get
                Return sentValues.GetValue(717)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(717, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (718)", Order:=718)>
        Public Property HasAttachment718 As Boolean
            Get
                Return hasAttachmentValues.GetValue(718)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(718, value)
            End Set
        End Property

        <Display(Name:="Size (719)", Order:=719)>
        Public Property Size719 As Integer
            Get
                Return sizeValues.GetValue(719)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(719, value)
            End Set
        End Property

        <Display(Name:="Priority (720)", Order:=720), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority720 As Priority
            Get
                Return priorityValues.GetValue(720)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(720, value)
            End Set
        End Property

        <Display(Name:="Subject (721)", Order:=721), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject721 As String
            Get
                Return subjectValues.GetValue(721)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(721, value)
            End Set
        End Property

        <Display(Name:="From (722)", Order:=722)>
        Public Property From722 As String
            Get
                Return fromValues.GetValue(722)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(722, value)
            End Set
        End Property

        <Display(Name:="To (723)", Order:=723)>
        Public Property To723 As String
            Get
                Return toValues.GetValue(723)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(723, value)
            End Set
        End Property

        <Display(Name:="Sent (724)", Order:=724)>
        Public Property Sent724 As Date
            Get
                Return sentValues.GetValue(724)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(724, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (725)", Order:=725)>
        Public Property HasAttachment725 As Boolean
            Get
                Return hasAttachmentValues.GetValue(725)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(725, value)
            End Set
        End Property

        <Display(Name:="Size (726)", Order:=726)>
        Public Property Size726 As Integer
            Get
                Return sizeValues.GetValue(726)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(726, value)
            End Set
        End Property

        <Display(Name:="Priority (727)", Order:=727), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority727 As Priority
            Get
                Return priorityValues.GetValue(727)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(727, value)
            End Set
        End Property

        <Display(Name:="Subject (728)", Order:=728), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject728 As String
            Get
                Return subjectValues.GetValue(728)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(728, value)
            End Set
        End Property

        <Display(Name:="From (729)", Order:=729)>
        Public Property From729 As String
            Get
                Return fromValues.GetValue(729)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(729, value)
            End Set
        End Property

        <Display(Name:="To (730)", Order:=730)>
        Public Property To730 As String
            Get
                Return toValues.GetValue(730)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(730, value)
            End Set
        End Property

        <Display(Name:="Sent (731)", Order:=731)>
        Public Property Sent731 As Date
            Get
                Return sentValues.GetValue(731)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(731, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (732)", Order:=732)>
        Public Property HasAttachment732 As Boolean
            Get
                Return hasAttachmentValues.GetValue(732)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(732, value)
            End Set
        End Property

        <Display(Name:="Size (733)", Order:=733)>
        Public Property Size733 As Integer
            Get
                Return sizeValues.GetValue(733)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(733, value)
            End Set
        End Property

        <Display(Name:="Priority (734)", Order:=734), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority734 As Priority
            Get
                Return priorityValues.GetValue(734)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(734, value)
            End Set
        End Property

        <Display(Name:="Subject (735)", Order:=735), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject735 As String
            Get
                Return subjectValues.GetValue(735)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(735, value)
            End Set
        End Property

        <Display(Name:="From (736)", Order:=736)>
        Public Property From736 As String
            Get
                Return fromValues.GetValue(736)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(736, value)
            End Set
        End Property

        <Display(Name:="To (737)", Order:=737)>
        Public Property To737 As String
            Get
                Return toValues.GetValue(737)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(737, value)
            End Set
        End Property

        <Display(Name:="Sent (738)", Order:=738)>
        Public Property Sent738 As Date
            Get
                Return sentValues.GetValue(738)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(738, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (739)", Order:=739)>
        Public Property HasAttachment739 As Boolean
            Get
                Return hasAttachmentValues.GetValue(739)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(739, value)
            End Set
        End Property

        <Display(Name:="Size (740)", Order:=740)>
        Public Property Size740 As Integer
            Get
                Return sizeValues.GetValue(740)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(740, value)
            End Set
        End Property

        <Display(Name:="Priority (741)", Order:=741), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority741 As Priority
            Get
                Return priorityValues.GetValue(741)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(741, value)
            End Set
        End Property

        <Display(Name:="Subject (742)", Order:=742), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject742 As String
            Get
                Return subjectValues.GetValue(742)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(742, value)
            End Set
        End Property

        <Display(Name:="From (743)", Order:=743)>
        Public Property From743 As String
            Get
                Return fromValues.GetValue(743)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(743, value)
            End Set
        End Property

        <Display(Name:="To (744)", Order:=744)>
        Public Property To744 As String
            Get
                Return toValues.GetValue(744)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(744, value)
            End Set
        End Property

        <Display(Name:="Sent (745)", Order:=745)>
        Public Property Sent745 As Date
            Get
                Return sentValues.GetValue(745)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(745, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (746)", Order:=746)>
        Public Property HasAttachment746 As Boolean
            Get
                Return hasAttachmentValues.GetValue(746)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(746, value)
            End Set
        End Property

        <Display(Name:="Size (747)", Order:=747)>
        Public Property Size747 As Integer
            Get
                Return sizeValues.GetValue(747)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(747, value)
            End Set
        End Property

        <Display(Name:="Priority (748)", Order:=748), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority748 As Priority
            Get
                Return priorityValues.GetValue(748)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(748, value)
            End Set
        End Property

        <Display(Name:="Subject (749)", Order:=749), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject749 As String
            Get
                Return subjectValues.GetValue(749)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(749, value)
            End Set
        End Property

        <Display(Name:="From (750)", Order:=750)>
        Public Property From750 As String
            Get
                Return fromValues.GetValue(750)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(750, value)
            End Set
        End Property

        <Display(Name:="To (751)", Order:=751)>
        Public Property To751 As String
            Get
                Return toValues.GetValue(751)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(751, value)
            End Set
        End Property

        <Display(Name:="Sent (752)", Order:=752)>
        Public Property Sent752 As Date
            Get
                Return sentValues.GetValue(752)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(752, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (753)", Order:=753)>
        Public Property HasAttachment753 As Boolean
            Get
                Return hasAttachmentValues.GetValue(753)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(753, value)
            End Set
        End Property

        <Display(Name:="Size (754)", Order:=754)>
        Public Property Size754 As Integer
            Get
                Return sizeValues.GetValue(754)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(754, value)
            End Set
        End Property

        <Display(Name:="Priority (755)", Order:=755), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority755 As Priority
            Get
                Return priorityValues.GetValue(755)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(755, value)
            End Set
        End Property

        <Display(Name:="Subject (756)", Order:=756), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject756 As String
            Get
                Return subjectValues.GetValue(756)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(756, value)
            End Set
        End Property

        <Display(Name:="From (757)", Order:=757)>
        Public Property From757 As String
            Get
                Return fromValues.GetValue(757)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(757, value)
            End Set
        End Property

        <Display(Name:="To (758)", Order:=758)>
        Public Property To758 As String
            Get
                Return toValues.GetValue(758)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(758, value)
            End Set
        End Property

        <Display(Name:="Sent (759)", Order:=759)>
        Public Property Sent759 As Date
            Get
                Return sentValues.GetValue(759)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(759, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (760)", Order:=760)>
        Public Property HasAttachment760 As Boolean
            Get
                Return hasAttachmentValues.GetValue(760)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(760, value)
            End Set
        End Property

        <Display(Name:="Size (761)", Order:=761)>
        Public Property Size761 As Integer
            Get
                Return sizeValues.GetValue(761)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(761, value)
            End Set
        End Property

        <Display(Name:="Priority (762)", Order:=762), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority762 As Priority
            Get
                Return priorityValues.GetValue(762)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(762, value)
            End Set
        End Property

        <Display(Name:="Subject (763)", Order:=763), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject763 As String
            Get
                Return subjectValues.GetValue(763)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(763, value)
            End Set
        End Property

        <Display(Name:="From (764)", Order:=764)>
        Public Property From764 As String
            Get
                Return fromValues.GetValue(764)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(764, value)
            End Set
        End Property

        <Display(Name:="To (765)", Order:=765)>
        Public Property To765 As String
            Get
                Return toValues.GetValue(765)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(765, value)
            End Set
        End Property

        <Display(Name:="Sent (766)", Order:=766)>
        Public Property Sent766 As Date
            Get
                Return sentValues.GetValue(766)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(766, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (767)", Order:=767)>
        Public Property HasAttachment767 As Boolean
            Get
                Return hasAttachmentValues.GetValue(767)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(767, value)
            End Set
        End Property

        <Display(Name:="Size (768)", Order:=768)>
        Public Property Size768 As Integer
            Get
                Return sizeValues.GetValue(768)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(768, value)
            End Set
        End Property

        <Display(Name:="Priority (769)", Order:=769), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority769 As Priority
            Get
                Return priorityValues.GetValue(769)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(769, value)
            End Set
        End Property

        <Display(Name:="Subject (770)", Order:=770), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject770 As String
            Get
                Return subjectValues.GetValue(770)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(770, value)
            End Set
        End Property

        <Display(Name:="From (771)", Order:=771)>
        Public Property From771 As String
            Get
                Return fromValues.GetValue(771)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(771, value)
            End Set
        End Property

        <Display(Name:="To (772)", Order:=772)>
        Public Property To772 As String
            Get
                Return toValues.GetValue(772)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(772, value)
            End Set
        End Property

        <Display(Name:="Sent (773)", Order:=773)>
        Public Property Sent773 As Date
            Get
                Return sentValues.GetValue(773)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(773, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (774)", Order:=774)>
        Public Property HasAttachment774 As Boolean
            Get
                Return hasAttachmentValues.GetValue(774)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(774, value)
            End Set
        End Property

        <Display(Name:="Size (775)", Order:=775)>
        Public Property Size775 As Integer
            Get
                Return sizeValues.GetValue(775)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(775, value)
            End Set
        End Property

        <Display(Name:="Priority (776)", Order:=776), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority776 As Priority
            Get
                Return priorityValues.GetValue(776)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(776, value)
            End Set
        End Property

        <Display(Name:="Subject (777)", Order:=777), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject777 As String
            Get
                Return subjectValues.GetValue(777)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(777, value)
            End Set
        End Property

        <Display(Name:="From (778)", Order:=778)>
        Public Property From778 As String
            Get
                Return fromValues.GetValue(778)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(778, value)
            End Set
        End Property

        <Display(Name:="To (779)", Order:=779)>
        Public Property To779 As String
            Get
                Return toValues.GetValue(779)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(779, value)
            End Set
        End Property

        <Display(Name:="Sent (780)", Order:=780)>
        Public Property Sent780 As Date
            Get
                Return sentValues.GetValue(780)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(780, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (781)", Order:=781)>
        Public Property HasAttachment781 As Boolean
            Get
                Return hasAttachmentValues.GetValue(781)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(781, value)
            End Set
        End Property

        <Display(Name:="Size (782)", Order:=782)>
        Public Property Size782 As Integer
            Get
                Return sizeValues.GetValue(782)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(782, value)
            End Set
        End Property

        <Display(Name:="Priority (783)", Order:=783), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority783 As Priority
            Get
                Return priorityValues.GetValue(783)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(783, value)
            End Set
        End Property

        <Display(Name:="Subject (784)", Order:=784), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject784 As String
            Get
                Return subjectValues.GetValue(784)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(784, value)
            End Set
        End Property

        <Display(Name:="From (785)", Order:=785)>
        Public Property From785 As String
            Get
                Return fromValues.GetValue(785)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(785, value)
            End Set
        End Property

        <Display(Name:="To (786)", Order:=786)>
        Public Property To786 As String
            Get
                Return toValues.GetValue(786)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(786, value)
            End Set
        End Property

        <Display(Name:="Sent (787)", Order:=787)>
        Public Property Sent787 As Date
            Get
                Return sentValues.GetValue(787)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(787, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (788)", Order:=788)>
        Public Property HasAttachment788 As Boolean
            Get
                Return hasAttachmentValues.GetValue(788)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(788, value)
            End Set
        End Property

        <Display(Name:="Size (789)", Order:=789)>
        Public Property Size789 As Integer
            Get
                Return sizeValues.GetValue(789)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(789, value)
            End Set
        End Property

        <Display(Name:="Priority (790)", Order:=790), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority790 As Priority
            Get
                Return priorityValues.GetValue(790)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(790, value)
            End Set
        End Property

        <Display(Name:="Subject (791)", Order:=791), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject791 As String
            Get
                Return subjectValues.GetValue(791)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(791, value)
            End Set
        End Property

        <Display(Name:="From (792)", Order:=792)>
        Public Property From792 As String
            Get
                Return fromValues.GetValue(792)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(792, value)
            End Set
        End Property

        <Display(Name:="To (793)", Order:=793)>
        Public Property To793 As String
            Get
                Return toValues.GetValue(793)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(793, value)
            End Set
        End Property

        <Display(Name:="Sent (794)", Order:=794)>
        Public Property Sent794 As Date
            Get
                Return sentValues.GetValue(794)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(794, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (795)", Order:=795)>
        Public Property HasAttachment795 As Boolean
            Get
                Return hasAttachmentValues.GetValue(795)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(795, value)
            End Set
        End Property

        <Display(Name:="Size (796)", Order:=796)>
        Public Property Size796 As Integer
            Get
                Return sizeValues.GetValue(796)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(796, value)
            End Set
        End Property

        <Display(Name:="Priority (797)", Order:=797), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority797 As Priority
            Get
                Return priorityValues.GetValue(797)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(797, value)
            End Set
        End Property

        <Display(Name:="Subject (798)", Order:=798), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject798 As String
            Get
                Return subjectValues.GetValue(798)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(798, value)
            End Set
        End Property

        <Display(Name:="From (799)", Order:=799)>
        Public Property From799 As String
            Get
                Return fromValues.GetValue(799)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(799, value)
            End Set
        End Property

        <Display(Name:="To (800)", Order:=800)>
        Public Property To800 As String
            Get
                Return toValues.GetValue(800)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(800, value)
            End Set
        End Property

        <Display(Name:="Sent (801)", Order:=801)>
        Public Property Sent801 As Date
            Get
                Return sentValues.GetValue(801)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(801, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (802)", Order:=802)>
        Public Property HasAttachment802 As Boolean
            Get
                Return hasAttachmentValues.GetValue(802)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(802, value)
            End Set
        End Property

        <Display(Name:="Size (803)", Order:=803)>
        Public Property Size803 As Integer
            Get
                Return sizeValues.GetValue(803)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(803, value)
            End Set
        End Property

        <Display(Name:="Priority (804)", Order:=804), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority804 As Priority
            Get
                Return priorityValues.GetValue(804)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(804, value)
            End Set
        End Property

        <Display(Name:="Subject (805)", Order:=805), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject805 As String
            Get
                Return subjectValues.GetValue(805)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(805, value)
            End Set
        End Property

        <Display(Name:="From (806)", Order:=806)>
        Public Property From806 As String
            Get
                Return fromValues.GetValue(806)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(806, value)
            End Set
        End Property

        <Display(Name:="To (807)", Order:=807)>
        Public Property To807 As String
            Get
                Return toValues.GetValue(807)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(807, value)
            End Set
        End Property

        <Display(Name:="Sent (808)", Order:=808)>
        Public Property Sent808 As Date
            Get
                Return sentValues.GetValue(808)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(808, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (809)", Order:=809)>
        Public Property HasAttachment809 As Boolean
            Get
                Return hasAttachmentValues.GetValue(809)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(809, value)
            End Set
        End Property

        <Display(Name:="Size (810)", Order:=810)>
        Public Property Size810 As Integer
            Get
                Return sizeValues.GetValue(810)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(810, value)
            End Set
        End Property

        <Display(Name:="Priority (811)", Order:=811), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority811 As Priority
            Get
                Return priorityValues.GetValue(811)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(811, value)
            End Set
        End Property

        <Display(Name:="Subject (812)", Order:=812), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject812 As String
            Get
                Return subjectValues.GetValue(812)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(812, value)
            End Set
        End Property

        <Display(Name:="From (813)", Order:=813)>
        Public Property From813 As String
            Get
                Return fromValues.GetValue(813)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(813, value)
            End Set
        End Property

        <Display(Name:="To (814)", Order:=814)>
        Public Property To814 As String
            Get
                Return toValues.GetValue(814)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(814, value)
            End Set
        End Property

        <Display(Name:="Sent (815)", Order:=815)>
        Public Property Sent815 As Date
            Get
                Return sentValues.GetValue(815)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(815, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (816)", Order:=816)>
        Public Property HasAttachment816 As Boolean
            Get
                Return hasAttachmentValues.GetValue(816)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(816, value)
            End Set
        End Property

        <Display(Name:="Size (817)", Order:=817)>
        Public Property Size817 As Integer
            Get
                Return sizeValues.GetValue(817)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(817, value)
            End Set
        End Property

        <Display(Name:="Priority (818)", Order:=818), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority818 As Priority
            Get
                Return priorityValues.GetValue(818)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(818, value)
            End Set
        End Property

        <Display(Name:="Subject (819)", Order:=819), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject819 As String
            Get
                Return subjectValues.GetValue(819)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(819, value)
            End Set
        End Property

        <Display(Name:="From (820)", Order:=820)>
        Public Property From820 As String
            Get
                Return fromValues.GetValue(820)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(820, value)
            End Set
        End Property

        <Display(Name:="To (821)", Order:=821)>
        Public Property To821 As String
            Get
                Return toValues.GetValue(821)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(821, value)
            End Set
        End Property

        <Display(Name:="Sent (822)", Order:=822)>
        Public Property Sent822 As Date
            Get
                Return sentValues.GetValue(822)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(822, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (823)", Order:=823)>
        Public Property HasAttachment823 As Boolean
            Get
                Return hasAttachmentValues.GetValue(823)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(823, value)
            End Set
        End Property

        <Display(Name:="Size (824)", Order:=824)>
        Public Property Size824 As Integer
            Get
                Return sizeValues.GetValue(824)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(824, value)
            End Set
        End Property

        <Display(Name:="Priority (825)", Order:=825), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority825 As Priority
            Get
                Return priorityValues.GetValue(825)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(825, value)
            End Set
        End Property

        <Display(Name:="Subject (826)", Order:=826), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject826 As String
            Get
                Return subjectValues.GetValue(826)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(826, value)
            End Set
        End Property

        <Display(Name:="From (827)", Order:=827)>
        Public Property From827 As String
            Get
                Return fromValues.GetValue(827)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(827, value)
            End Set
        End Property

        <Display(Name:="To (828)", Order:=828)>
        Public Property To828 As String
            Get
                Return toValues.GetValue(828)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(828, value)
            End Set
        End Property

        <Display(Name:="Sent (829)", Order:=829)>
        Public Property Sent829 As Date
            Get
                Return sentValues.GetValue(829)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(829, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (830)", Order:=830)>
        Public Property HasAttachment830 As Boolean
            Get
                Return hasAttachmentValues.GetValue(830)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(830, value)
            End Set
        End Property

        <Display(Name:="Size (831)", Order:=831)>
        Public Property Size831 As Integer
            Get
                Return sizeValues.GetValue(831)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(831, value)
            End Set
        End Property

        <Display(Name:="Priority (832)", Order:=832), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority832 As Priority
            Get
                Return priorityValues.GetValue(832)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(832, value)
            End Set
        End Property

        <Display(Name:="Subject (833)", Order:=833), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject833 As String
            Get
                Return subjectValues.GetValue(833)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(833, value)
            End Set
        End Property

        <Display(Name:="From (834)", Order:=834)>
        Public Property From834 As String
            Get
                Return fromValues.GetValue(834)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(834, value)
            End Set
        End Property

        <Display(Name:="To (835)", Order:=835)>
        Public Property To835 As String
            Get
                Return toValues.GetValue(835)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(835, value)
            End Set
        End Property

        <Display(Name:="Sent (836)", Order:=836)>
        Public Property Sent836 As Date
            Get
                Return sentValues.GetValue(836)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(836, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (837)", Order:=837)>
        Public Property HasAttachment837 As Boolean
            Get
                Return hasAttachmentValues.GetValue(837)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(837, value)
            End Set
        End Property

        <Display(Name:="Size (838)", Order:=838)>
        Public Property Size838 As Integer
            Get
                Return sizeValues.GetValue(838)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(838, value)
            End Set
        End Property

        <Display(Name:="Priority (839)", Order:=839), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority839 As Priority
            Get
                Return priorityValues.GetValue(839)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(839, value)
            End Set
        End Property

        <Display(Name:="Subject (840)", Order:=840), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject840 As String
            Get
                Return subjectValues.GetValue(840)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(840, value)
            End Set
        End Property

        <Display(Name:="From (841)", Order:=841)>
        Public Property From841 As String
            Get
                Return fromValues.GetValue(841)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(841, value)
            End Set
        End Property

        <Display(Name:="To (842)", Order:=842)>
        Public Property To842 As String
            Get
                Return toValues.GetValue(842)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(842, value)
            End Set
        End Property

        <Display(Name:="Sent (843)", Order:=843)>
        Public Property Sent843 As Date
            Get
                Return sentValues.GetValue(843)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(843, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (844)", Order:=844)>
        Public Property HasAttachment844 As Boolean
            Get
                Return hasAttachmentValues.GetValue(844)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(844, value)
            End Set
        End Property

        <Display(Name:="Size (845)", Order:=845)>
        Public Property Size845 As Integer
            Get
                Return sizeValues.GetValue(845)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(845, value)
            End Set
        End Property

        <Display(Name:="Priority (846)", Order:=846), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority846 As Priority
            Get
                Return priorityValues.GetValue(846)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(846, value)
            End Set
        End Property

        <Display(Name:="Subject (847)", Order:=847), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject847 As String
            Get
                Return subjectValues.GetValue(847)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(847, value)
            End Set
        End Property

        <Display(Name:="From (848)", Order:=848)>
        Public Property From848 As String
            Get
                Return fromValues.GetValue(848)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(848, value)
            End Set
        End Property

        <Display(Name:="To (849)", Order:=849)>
        Public Property To849 As String
            Get
                Return toValues.GetValue(849)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(849, value)
            End Set
        End Property

        <Display(Name:="Sent (850)", Order:=850)>
        Public Property Sent850 As Date
            Get
                Return sentValues.GetValue(850)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(850, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (851)", Order:=851)>
        Public Property HasAttachment851 As Boolean
            Get
                Return hasAttachmentValues.GetValue(851)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(851, value)
            End Set
        End Property

        <Display(Name:="Size (852)", Order:=852)>
        Public Property Size852 As Integer
            Get
                Return sizeValues.GetValue(852)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(852, value)
            End Set
        End Property

        <Display(Name:="Priority (853)", Order:=853), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority853 As Priority
            Get
                Return priorityValues.GetValue(853)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(853, value)
            End Set
        End Property

        <Display(Name:="Subject (854)", Order:=854), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject854 As String
            Get
                Return subjectValues.GetValue(854)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(854, value)
            End Set
        End Property

        <Display(Name:="From (855)", Order:=855)>
        Public Property From855 As String
            Get
                Return fromValues.GetValue(855)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(855, value)
            End Set
        End Property

        <Display(Name:="To (856)", Order:=856)>
        Public Property To856 As String
            Get
                Return toValues.GetValue(856)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(856, value)
            End Set
        End Property

        <Display(Name:="Sent (857)", Order:=857)>
        Public Property Sent857 As Date
            Get
                Return sentValues.GetValue(857)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(857, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (858)", Order:=858)>
        Public Property HasAttachment858 As Boolean
            Get
                Return hasAttachmentValues.GetValue(858)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(858, value)
            End Set
        End Property

        <Display(Name:="Size (859)", Order:=859)>
        Public Property Size859 As Integer
            Get
                Return sizeValues.GetValue(859)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(859, value)
            End Set
        End Property

        <Display(Name:="Priority (860)", Order:=860), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority860 As Priority
            Get
                Return priorityValues.GetValue(860)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(860, value)
            End Set
        End Property

        <Display(Name:="Subject (861)", Order:=861), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject861 As String
            Get
                Return subjectValues.GetValue(861)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(861, value)
            End Set
        End Property

        <Display(Name:="From (862)", Order:=862)>
        Public Property From862 As String
            Get
                Return fromValues.GetValue(862)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(862, value)
            End Set
        End Property

        <Display(Name:="To (863)", Order:=863)>
        Public Property To863 As String
            Get
                Return toValues.GetValue(863)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(863, value)
            End Set
        End Property

        <Display(Name:="Sent (864)", Order:=864)>
        Public Property Sent864 As Date
            Get
                Return sentValues.GetValue(864)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(864, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (865)", Order:=865)>
        Public Property HasAttachment865 As Boolean
            Get
                Return hasAttachmentValues.GetValue(865)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(865, value)
            End Set
        End Property

        <Display(Name:="Size (866)", Order:=866)>
        Public Property Size866 As Integer
            Get
                Return sizeValues.GetValue(866)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(866, value)
            End Set
        End Property

        <Display(Name:="Priority (867)", Order:=867), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority867 As Priority
            Get
                Return priorityValues.GetValue(867)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(867, value)
            End Set
        End Property

        <Display(Name:="Subject (868)", Order:=868), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject868 As String
            Get
                Return subjectValues.GetValue(868)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(868, value)
            End Set
        End Property

        <Display(Name:="From (869)", Order:=869)>
        Public Property From869 As String
            Get
                Return fromValues.GetValue(869)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(869, value)
            End Set
        End Property

        <Display(Name:="To (870)", Order:=870)>
        Public Property To870 As String
            Get
                Return toValues.GetValue(870)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(870, value)
            End Set
        End Property

        <Display(Name:="Sent (871)", Order:=871)>
        Public Property Sent871 As Date
            Get
                Return sentValues.GetValue(871)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(871, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (872)", Order:=872)>
        Public Property HasAttachment872 As Boolean
            Get
                Return hasAttachmentValues.GetValue(872)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(872, value)
            End Set
        End Property

        <Display(Name:="Size (873)", Order:=873)>
        Public Property Size873 As Integer
            Get
                Return sizeValues.GetValue(873)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(873, value)
            End Set
        End Property

        <Display(Name:="Priority (874)", Order:=874), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority874 As Priority
            Get
                Return priorityValues.GetValue(874)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(874, value)
            End Set
        End Property

        <Display(Name:="Subject (875)", Order:=875), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject875 As String
            Get
                Return subjectValues.GetValue(875)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(875, value)
            End Set
        End Property

        <Display(Name:="From (876)", Order:=876)>
        Public Property From876 As String
            Get
                Return fromValues.GetValue(876)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(876, value)
            End Set
        End Property

        <Display(Name:="To (877)", Order:=877)>
        Public Property To877 As String
            Get
                Return toValues.GetValue(877)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(877, value)
            End Set
        End Property

        <Display(Name:="Sent (878)", Order:=878)>
        Public Property Sent878 As Date
            Get
                Return sentValues.GetValue(878)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(878, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (879)", Order:=879)>
        Public Property HasAttachment879 As Boolean
            Get
                Return hasAttachmentValues.GetValue(879)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(879, value)
            End Set
        End Property

        <Display(Name:="Size (880)", Order:=880)>
        Public Property Size880 As Integer
            Get
                Return sizeValues.GetValue(880)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(880, value)
            End Set
        End Property

        <Display(Name:="Priority (881)", Order:=881), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority881 As Priority
            Get
                Return priorityValues.GetValue(881)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(881, value)
            End Set
        End Property

        <Display(Name:="Subject (882)", Order:=882), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject882 As String
            Get
                Return subjectValues.GetValue(882)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(882, value)
            End Set
        End Property

        <Display(Name:="From (883)", Order:=883)>
        Public Property From883 As String
            Get
                Return fromValues.GetValue(883)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(883, value)
            End Set
        End Property

        <Display(Name:="To (884)", Order:=884)>
        Public Property To884 As String
            Get
                Return toValues.GetValue(884)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(884, value)
            End Set
        End Property

        <Display(Name:="Sent (885)", Order:=885)>
        Public Property Sent885 As Date
            Get
                Return sentValues.GetValue(885)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(885, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (886)", Order:=886)>
        Public Property HasAttachment886 As Boolean
            Get
                Return hasAttachmentValues.GetValue(886)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(886, value)
            End Set
        End Property

        <Display(Name:="Size (887)", Order:=887)>
        Public Property Size887 As Integer
            Get
                Return sizeValues.GetValue(887)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(887, value)
            End Set
        End Property

        <Display(Name:="Priority (888)", Order:=888), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority888 As Priority
            Get
                Return priorityValues.GetValue(888)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(888, value)
            End Set
        End Property

        <Display(Name:="Subject (889)", Order:=889), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject889 As String
            Get
                Return subjectValues.GetValue(889)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(889, value)
            End Set
        End Property

        <Display(Name:="From (890)", Order:=890)>
        Public Property From890 As String
            Get
                Return fromValues.GetValue(890)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(890, value)
            End Set
        End Property

        <Display(Name:="To (891)", Order:=891)>
        Public Property To891 As String
            Get
                Return toValues.GetValue(891)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(891, value)
            End Set
        End Property

        <Display(Name:="Sent (892)", Order:=892)>
        Public Property Sent892 As Date
            Get
                Return sentValues.GetValue(892)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(892, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (893)", Order:=893)>
        Public Property HasAttachment893 As Boolean
            Get
                Return hasAttachmentValues.GetValue(893)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(893, value)
            End Set
        End Property

        <Display(Name:="Size (894)", Order:=894)>
        Public Property Size894 As Integer
            Get
                Return sizeValues.GetValue(894)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(894, value)
            End Set
        End Property

        <Display(Name:="Priority (895)", Order:=895), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority895 As Priority
            Get
                Return priorityValues.GetValue(895)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(895, value)
            End Set
        End Property

        <Display(Name:="Subject (896)", Order:=896), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject896 As String
            Get
                Return subjectValues.GetValue(896)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(896, value)
            End Set
        End Property

        <Display(Name:="From (897)", Order:=897)>
        Public Property From897 As String
            Get
                Return fromValues.GetValue(897)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(897, value)
            End Set
        End Property

        <Display(Name:="To (898)", Order:=898)>
        Public Property To898 As String
            Get
                Return toValues.GetValue(898)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(898, value)
            End Set
        End Property

        <Display(Name:="Sent (899)", Order:=899)>
        Public Property Sent899 As Date
            Get
                Return sentValues.GetValue(899)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(899, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (900)", Order:=900)>
        Public Property HasAttachment900 As Boolean
            Get
                Return hasAttachmentValues.GetValue(900)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(900, value)
            End Set
        End Property

        <Display(Name:="Size (901)", Order:=901)>
        Public Property Size901 As Integer
            Get
                Return sizeValues.GetValue(901)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(901, value)
            End Set
        End Property

        <Display(Name:="Priority (902)", Order:=902), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority902 As Priority
            Get
                Return priorityValues.GetValue(902)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(902, value)
            End Set
        End Property

        <Display(Name:="Subject (903)", Order:=903), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject903 As String
            Get
                Return subjectValues.GetValue(903)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(903, value)
            End Set
        End Property

        <Display(Name:="From (904)", Order:=904)>
        Public Property From904 As String
            Get
                Return fromValues.GetValue(904)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(904, value)
            End Set
        End Property

        <Display(Name:="To (905)", Order:=905)>
        Public Property To905 As String
            Get
                Return toValues.GetValue(905)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(905, value)
            End Set
        End Property

        <Display(Name:="Sent (906)", Order:=906)>
        Public Property Sent906 As Date
            Get
                Return sentValues.GetValue(906)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(906, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (907)", Order:=907)>
        Public Property HasAttachment907 As Boolean
            Get
                Return hasAttachmentValues.GetValue(907)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(907, value)
            End Set
        End Property

        <Display(Name:="Size (908)", Order:=908)>
        Public Property Size908 As Integer
            Get
                Return sizeValues.GetValue(908)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(908, value)
            End Set
        End Property

        <Display(Name:="Priority (909)", Order:=909), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority909 As Priority
            Get
                Return priorityValues.GetValue(909)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(909, value)
            End Set
        End Property

        <Display(Name:="Subject (910)", Order:=910), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject910 As String
            Get
                Return subjectValues.GetValue(910)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(910, value)
            End Set
        End Property

        <Display(Name:="From (911)", Order:=911)>
        Public Property From911 As String
            Get
                Return fromValues.GetValue(911)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(911, value)
            End Set
        End Property

        <Display(Name:="To (912)", Order:=912)>
        Public Property To912 As String
            Get
                Return toValues.GetValue(912)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(912, value)
            End Set
        End Property

        <Display(Name:="Sent (913)", Order:=913)>
        Public Property Sent913 As Date
            Get
                Return sentValues.GetValue(913)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(913, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (914)", Order:=914)>
        Public Property HasAttachment914 As Boolean
            Get
                Return hasAttachmentValues.GetValue(914)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(914, value)
            End Set
        End Property

        <Display(Name:="Size (915)", Order:=915)>
        Public Property Size915 As Integer
            Get
                Return sizeValues.GetValue(915)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(915, value)
            End Set
        End Property

        <Display(Name:="Priority (916)", Order:=916), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority916 As Priority
            Get
                Return priorityValues.GetValue(916)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(916, value)
            End Set
        End Property

        <Display(Name:="Subject (917)", Order:=917), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject917 As String
            Get
                Return subjectValues.GetValue(917)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(917, value)
            End Set
        End Property

        <Display(Name:="From (918)", Order:=918)>
        Public Property From918 As String
            Get
                Return fromValues.GetValue(918)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(918, value)
            End Set
        End Property

        <Display(Name:="To (919)", Order:=919)>
        Public Property To919 As String
            Get
                Return toValues.GetValue(919)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(919, value)
            End Set
        End Property

        <Display(Name:="Sent (920)", Order:=920)>
        Public Property Sent920 As Date
            Get
                Return sentValues.GetValue(920)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(920, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (921)", Order:=921)>
        Public Property HasAttachment921 As Boolean
            Get
                Return hasAttachmentValues.GetValue(921)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(921, value)
            End Set
        End Property

        <Display(Name:="Size (922)", Order:=922)>
        Public Property Size922 As Integer
            Get
                Return sizeValues.GetValue(922)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(922, value)
            End Set
        End Property

        <Display(Name:="Priority (923)", Order:=923), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority923 As Priority
            Get
                Return priorityValues.GetValue(923)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(923, value)
            End Set
        End Property

        <Display(Name:="Subject (924)", Order:=924), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject924 As String
            Get
                Return subjectValues.GetValue(924)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(924, value)
            End Set
        End Property

        <Display(Name:="From (925)", Order:=925)>
        Public Property From925 As String
            Get
                Return fromValues.GetValue(925)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(925, value)
            End Set
        End Property

        <Display(Name:="To (926)", Order:=926)>
        Public Property To926 As String
            Get
                Return toValues.GetValue(926)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(926, value)
            End Set
        End Property

        <Display(Name:="Sent (927)", Order:=927)>
        Public Property Sent927 As Date
            Get
                Return sentValues.GetValue(927)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(927, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (928)", Order:=928)>
        Public Property HasAttachment928 As Boolean
            Get
                Return hasAttachmentValues.GetValue(928)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(928, value)
            End Set
        End Property

        <Display(Name:="Size (929)", Order:=929)>
        Public Property Size929 As Integer
            Get
                Return sizeValues.GetValue(929)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(929, value)
            End Set
        End Property

        <Display(Name:="Priority (930)", Order:=930), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority930 As Priority
            Get
                Return priorityValues.GetValue(930)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(930, value)
            End Set
        End Property

        <Display(Name:="Subject (931)", Order:=931), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject931 As String
            Get
                Return subjectValues.GetValue(931)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(931, value)
            End Set
        End Property

        <Display(Name:="From (932)", Order:=932)>
        Public Property From932 As String
            Get
                Return fromValues.GetValue(932)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(932, value)
            End Set
        End Property

        <Display(Name:="To (933)", Order:=933)>
        Public Property To933 As String
            Get
                Return toValues.GetValue(933)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(933, value)
            End Set
        End Property

        <Display(Name:="Sent (934)", Order:=934)>
        Public Property Sent934 As Date
            Get
                Return sentValues.GetValue(934)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(934, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (935)", Order:=935)>
        Public Property HasAttachment935 As Boolean
            Get
                Return hasAttachmentValues.GetValue(935)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(935, value)
            End Set
        End Property

        <Display(Name:="Size (936)", Order:=936)>
        Public Property Size936 As Integer
            Get
                Return sizeValues.GetValue(936)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(936, value)
            End Set
        End Property

        <Display(Name:="Priority (937)", Order:=937), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority937 As Priority
            Get
                Return priorityValues.GetValue(937)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(937, value)
            End Set
        End Property

        <Display(Name:="Subject (938)", Order:=938), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject938 As String
            Get
                Return subjectValues.GetValue(938)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(938, value)
            End Set
        End Property

        <Display(Name:="From (939)", Order:=939)>
        Public Property From939 As String
            Get
                Return fromValues.GetValue(939)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(939, value)
            End Set
        End Property

        <Display(Name:="To (940)", Order:=940)>
        Public Property To940 As String
            Get
                Return toValues.GetValue(940)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(940, value)
            End Set
        End Property

        <Display(Name:="Sent (941)", Order:=941)>
        Public Property Sent941 As Date
            Get
                Return sentValues.GetValue(941)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(941, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (942)", Order:=942)>
        Public Property HasAttachment942 As Boolean
            Get
                Return hasAttachmentValues.GetValue(942)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(942, value)
            End Set
        End Property

        <Display(Name:="Size (943)", Order:=943)>
        Public Property Size943 As Integer
            Get
                Return sizeValues.GetValue(943)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(943, value)
            End Set
        End Property

        <Display(Name:="Priority (944)", Order:=944), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority944 As Priority
            Get
                Return priorityValues.GetValue(944)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(944, value)
            End Set
        End Property

        <Display(Name:="Subject (945)", Order:=945), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject945 As String
            Get
                Return subjectValues.GetValue(945)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(945, value)
            End Set
        End Property

        <Display(Name:="From (946)", Order:=946)>
        Public Property From946 As String
            Get
                Return fromValues.GetValue(946)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(946, value)
            End Set
        End Property

        <Display(Name:="To (947)", Order:=947)>
        Public Property To947 As String
            Get
                Return toValues.GetValue(947)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(947, value)
            End Set
        End Property

        <Display(Name:="Sent (948)", Order:=948)>
        Public Property Sent948 As Date
            Get
                Return sentValues.GetValue(948)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(948, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (949)", Order:=949)>
        Public Property HasAttachment949 As Boolean
            Get
                Return hasAttachmentValues.GetValue(949)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(949, value)
            End Set
        End Property

        <Display(Name:="Size (950)", Order:=950)>
        Public Property Size950 As Integer
            Get
                Return sizeValues.GetValue(950)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(950, value)
            End Set
        End Property

        <Display(Name:="Priority (951)", Order:=951), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority951 As Priority
            Get
                Return priorityValues.GetValue(951)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(951, value)
            End Set
        End Property

        <Display(Name:="Subject (952)", Order:=952), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject952 As String
            Get
                Return subjectValues.GetValue(952)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(952, value)
            End Set
        End Property

        <Display(Name:="From (953)", Order:=953)>
        Public Property From953 As String
            Get
                Return fromValues.GetValue(953)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(953, value)
            End Set
        End Property

        <Display(Name:="To (954)", Order:=954)>
        Public Property To954 As String
            Get
                Return toValues.GetValue(954)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(954, value)
            End Set
        End Property

        <Display(Name:="Sent (955)", Order:=955)>
        Public Property Sent955 As Date
            Get
                Return sentValues.GetValue(955)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(955, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (956)", Order:=956)>
        Public Property HasAttachment956 As Boolean
            Get
                Return hasAttachmentValues.GetValue(956)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(956, value)
            End Set
        End Property

        <Display(Name:="Size (957)", Order:=957)>
        Public Property Size957 As Integer
            Get
                Return sizeValues.GetValue(957)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(957, value)
            End Set
        End Property

        <Display(Name:="Priority (958)", Order:=958), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority958 As Priority
            Get
                Return priorityValues.GetValue(958)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(958, value)
            End Set
        End Property

        <Display(Name:="Subject (959)", Order:=959), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject959 As String
            Get
                Return subjectValues.GetValue(959)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(959, value)
            End Set
        End Property

        <Display(Name:="From (960)", Order:=960)>
        Public Property From960 As String
            Get
                Return fromValues.GetValue(960)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(960, value)
            End Set
        End Property

        <Display(Name:="To (961)", Order:=961)>
        Public Property To961 As String
            Get
                Return toValues.GetValue(961)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(961, value)
            End Set
        End Property

        <Display(Name:="Sent (962)", Order:=962)>
        Public Property Sent962 As Date
            Get
                Return sentValues.GetValue(962)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(962, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (963)", Order:=963)>
        Public Property HasAttachment963 As Boolean
            Get
                Return hasAttachmentValues.GetValue(963)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(963, value)
            End Set
        End Property

        <Display(Name:="Size (964)", Order:=964)>
        Public Property Size964 As Integer
            Get
                Return sizeValues.GetValue(964)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(964, value)
            End Set
        End Property

        <Display(Name:="Priority (965)", Order:=965), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority965 As Priority
            Get
                Return priorityValues.GetValue(965)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(965, value)
            End Set
        End Property

        <Display(Name:="Subject (966)", Order:=966), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject966 As String
            Get
                Return subjectValues.GetValue(966)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(966, value)
            End Set
        End Property

        <Display(Name:="From (967)", Order:=967)>
        Public Property From967 As String
            Get
                Return fromValues.GetValue(967)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(967, value)
            End Set
        End Property

        <Display(Name:="To (968)", Order:=968)>
        Public Property To968 As String
            Get
                Return toValues.GetValue(968)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(968, value)
            End Set
        End Property

        <Display(Name:="Sent (969)", Order:=969)>
        Public Property Sent969 As Date
            Get
                Return sentValues.GetValue(969)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(969, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (970)", Order:=970)>
        Public Property HasAttachment970 As Boolean
            Get
                Return hasAttachmentValues.GetValue(970)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(970, value)
            End Set
        End Property

        <Display(Name:="Size (971)", Order:=971)>
        Public Property Size971 As Integer
            Get
                Return sizeValues.GetValue(971)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(971, value)
            End Set
        End Property

        <Display(Name:="Priority (972)", Order:=972), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority972 As Priority
            Get
                Return priorityValues.GetValue(972)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(972, value)
            End Set
        End Property

        <Display(Name:="Subject (973)", Order:=973), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject973 As String
            Get
                Return subjectValues.GetValue(973)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(973, value)
            End Set
        End Property

        <Display(Name:="From (974)", Order:=974)>
        Public Property From974 As String
            Get
                Return fromValues.GetValue(974)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(974, value)
            End Set
        End Property

        <Display(Name:="To (975)", Order:=975)>
        Public Property To975 As String
            Get
                Return toValues.GetValue(975)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(975, value)
            End Set
        End Property

        <Display(Name:="Sent (976)", Order:=976)>
        Public Property Sent976 As Date
            Get
                Return sentValues.GetValue(976)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(976, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (977)", Order:=977)>
        Public Property HasAttachment977 As Boolean
            Get
                Return hasAttachmentValues.GetValue(977)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(977, value)
            End Set
        End Property

        <Display(Name:="Size (978)", Order:=978)>
        Public Property Size978 As Integer
            Get
                Return sizeValues.GetValue(978)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(978, value)
            End Set
        End Property

        <Display(Name:="Priority (979)", Order:=979), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority979 As Priority
            Get
                Return priorityValues.GetValue(979)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(979, value)
            End Set
        End Property

        <Display(Name:="Subject (980)", Order:=980), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject980 As String
            Get
                Return subjectValues.GetValue(980)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(980, value)
            End Set
        End Property

        <Display(Name:="From (981)", Order:=981)>
        Public Property From981 As String
            Get
                Return fromValues.GetValue(981)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(981, value)
            End Set
        End Property

        <Display(Name:="To (982)", Order:=982)>
        Public Property To982 As String
            Get
                Return toValues.GetValue(982)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(982, value)
            End Set
        End Property

        <Display(Name:="Sent (983)", Order:=983)>
        Public Property Sent983 As Date
            Get
                Return sentValues.GetValue(983)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(983, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (984)", Order:=984)>
        Public Property HasAttachment984 As Boolean
            Get
                Return hasAttachmentValues.GetValue(984)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(984, value)
            End Set
        End Property

        <Display(Name:="Size (985)", Order:=985)>
        Public Property Size985 As Integer
            Get
                Return sizeValues.GetValue(985)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(985, value)
            End Set
        End Property

        <Display(Name:="Priority (986)", Order:=986), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority986 As Priority
            Get
                Return priorityValues.GetValue(986)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(986, value)
            End Set
        End Property

        <Display(Name:="Subject (987)", Order:=987), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject987 As String
            Get
                Return subjectValues.GetValue(987)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(987, value)
            End Set
        End Property

        <Display(Name:="From (988)", Order:=988)>
        Public Property From988 As String
            Get
                Return fromValues.GetValue(988)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(988, value)
            End Set
        End Property

        <Display(Name:="To (989)", Order:=989)>
        Public Property To989 As String
            Get
                Return toValues.GetValue(989)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(989, value)
            End Set
        End Property

        <Display(Name:="Sent (990)", Order:=990)>
        Public Property Sent990 As Date
            Get
                Return sentValues.GetValue(990)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(990, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (991)", Order:=991)>
        Public Property HasAttachment991 As Boolean
            Get
                Return hasAttachmentValues.GetValue(991)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(991, value)
            End Set
        End Property

        <Display(Name:="Size (992)", Order:=992)>
        Public Property Size992 As Integer
            Get
                Return sizeValues.GetValue(992)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(992, value)
            End Set
        End Property

        <Display(Name:="Priority (993)", Order:=993), GridEditor(TemplateKey:="priorityColumn")>
        Public Property Priority993 As Priority
            Get
                Return priorityValues.GetValue(993)
            End Get

            Set(ByVal value As Priority)
                priorityValues.SetValue(993, value)
            End Set
        End Property

        <Display(Name:="Subject (994)", Order:=994), GridEditor(TemplateKey:="subjectEditor")>
        Public Property Subject994 As String
            Get
                Return subjectValues.GetValue(994)
            End Get

            Set(ByVal value As String)
                subjectValues.SetValue(994, value)
            End Set
        End Property

        <Display(Name:="From (995)", Order:=995)>
        Public Property From995 As String
            Get
                Return fromValues.GetValue(995)
            End Get

            Set(ByVal value As String)
                fromValues.SetValue(995, value)
            End Set
        End Property

        <Display(Name:="To (996)", Order:=996)>
        Public Property To996 As String
            Get
                Return toValues.GetValue(996)
            End Get

            Set(ByVal value As String)
                toValues.SetValue(996, value)
            End Set
        End Property

        <Display(Name:="Sent (997)", Order:=997)>
        Public Property Sent997 As Date
            Get
                Return sentValues.GetValue(997)
            End Get

            Set(ByVal value As Date)
                sentValues.SetValue(997, value)
            End Set
        End Property

        <Display(Name:="Has Attachment (998)", Order:=998)>
        Public Property HasAttachment998 As Boolean
            Get
                Return hasAttachmentValues.GetValue(998)
            End Get

            Set(ByVal value As Boolean)
                hasAttachmentValues.SetValue(998, value)
            End Set
        End Property

        <Display(Name:="Size (999)", Order:=999)>
        Public Property Size999 As Integer
            Get
                Return sizeValues.GetValue(999)
            End Get

            Set(ByVal value As Integer)
                sizeValues.SetValue(999, value)
            End Set
        End Property
#End Region
    End Class
End Namespace
