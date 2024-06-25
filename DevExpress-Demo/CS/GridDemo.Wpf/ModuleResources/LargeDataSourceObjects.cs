using DevExpress.Mvvm.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace GridDemo {
    public class LargeDataSourceObjectMedium : LargeDataSourceObjectBase {
        public LargeDataSourceObjectMedium(int id)
            : base(id) {
        }

        #region properties
        [Display(Name = "From (1)", Order = 1)]
        public string From1 { get { return fromValues.GetValue(1); } set { fromValues.SetValue(1, value); } }
        [Display(Name = "To (2)", Order = 2)]
        public string To2 { get { return toValues.GetValue(2); } set { toValues.SetValue(2, value); } }
        [Display(Name = "Sent (3)", Order = 3)]
        public DateTime Sent3 { get { return sentValues.GetValue(3); } set { sentValues.SetValue(3, value); } }
        [Display(Name = "Has Attachment (4)", Order = 4)]
        public bool HasAttachment4 { get { return hasAttachmentValues.GetValue(4); } set { hasAttachmentValues.SetValue(4, value); } }
        [Display(Name = "Size (5)", Order = 5)]
        public int Size5 { get { return sizeValues.GetValue(5); } set { sizeValues.SetValue(5, value); } }
        [Display(Name = "Priority (6)", Order = 6), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority6 { get { return priorityValues.GetValue(6); } set { priorityValues.SetValue(6, value); } }
        [Display(Name = "Subject (7)", Order = 7), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject7 { get { return subjectValues.GetValue(7); } set { subjectValues.SetValue(7, value); } }

        [Display(Name = "From (8)", Order = 8)]
        public string From8 { get { return fromValues.GetValue(8); } set { fromValues.SetValue(8, value); } }
        [Display(Name = "To (9)", Order = 9)]
        public string To9 { get { return toValues.GetValue(9); } set { toValues.SetValue(9, value); } }
        [Display(Name = "Sent (10)", Order = 10)]
        public DateTime Sent10 { get { return sentValues.GetValue(10); } set { sentValues.SetValue(10, value); } }
        [Display(Name = "Has Attachment (11)", Order = 11)]
        public bool HasAttachment11 { get { return hasAttachmentValues.GetValue(11); } set { hasAttachmentValues.SetValue(11, value); } }
        [Display(Name = "Size (12)", Order = 12)]
        public int Size12 { get { return sizeValues.GetValue(12); } set { sizeValues.SetValue(12, value); } }
        [Display(Name = "Priority (13)", Order = 13), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority13 { get { return priorityValues.GetValue(13); } set { priorityValues.SetValue(13, value); } }
        [Display(Name = "Subject (14)", Order = 14), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject14 { get { return subjectValues.GetValue(14); } set { subjectValues.SetValue(14, value); } }

        [Display(Name = "From (15)", Order = 15)]
        public string From15 { get { return fromValues.GetValue(15); } set { fromValues.SetValue(15, value); } }
        [Display(Name = "To (16)", Order = 16)]
        public string To16 { get { return toValues.GetValue(16); } set { toValues.SetValue(16, value); } }
        [Display(Name = "Sent (17)", Order = 17)]
        public DateTime Sent17 { get { return sentValues.GetValue(17); } set { sentValues.SetValue(17, value); } }
        [Display(Name = "Has Attachment (18)", Order = 18)]
        public bool HasAttachment18 { get { return hasAttachmentValues.GetValue(18); } set { hasAttachmentValues.SetValue(18, value); } }
        [Display(Name = "Size (19)", Order = 19)]
        public int Size19 { get { return sizeValues.GetValue(19); } set { sizeValues.SetValue(19, value); } }
        [Display(Name = "Priority (20)", Order = 20), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority20 { get { return priorityValues.GetValue(20); } set { priorityValues.SetValue(20, value); } }
        [Display(Name = "Subject (21)", Order = 21), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject21 { get { return subjectValues.GetValue(21); } set { subjectValues.SetValue(21, value); } }

        [Display(Name = "From (22)", Order = 22)]
        public string From22 { get { return fromValues.GetValue(22); } set { fromValues.SetValue(22, value); } }
        [Display(Name = "To (23)", Order = 23)]
        public string To23 { get { return toValues.GetValue(23); } set { toValues.SetValue(23, value); } }
        [Display(Name = "Sent (24)", Order = 24)]
        public DateTime Sent24 { get { return sentValues.GetValue(24); } set { sentValues.SetValue(24, value); } }
        [Display(Name = "Has Attachment (25)", Order = 25)]
        public bool HasAttachment25 { get { return hasAttachmentValues.GetValue(25); } set { hasAttachmentValues.SetValue(25, value); } }
        [Display(Name = "Size (26)", Order = 26)]
        public int Size26 { get { return sizeValues.GetValue(26); } set { sizeValues.SetValue(26, value); } }
        [Display(Name = "Priority (27)", Order = 27), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority27 { get { return priorityValues.GetValue(27); } set { priorityValues.SetValue(27, value); } }
        [Display(Name = "Subject (28)", Order = 28), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject28 { get { return subjectValues.GetValue(28); } set { subjectValues.SetValue(28, value); } }

        [Display(Name = "From (29)", Order = 29)]
        public string From29 { get { return fromValues.GetValue(29); } set { fromValues.SetValue(29, value); } }
        [Display(Name = "To (30)", Order = 30)]
        public string To30 { get { return toValues.GetValue(30); } set { toValues.SetValue(30, value); } }
        [Display(Name = "Sent (31)", Order = 31)]
        public DateTime Sent31 { get { return sentValues.GetValue(31); } set { sentValues.SetValue(31, value); } }
        [Display(Name = "Has Attachment (32)", Order = 32)]
        public bool HasAttachment32 { get { return hasAttachmentValues.GetValue(32); } set { hasAttachmentValues.SetValue(32, value); } }
        [Display(Name = "Size (33)", Order = 33)]
        public int Size33 { get { return sizeValues.GetValue(33); } set { sizeValues.SetValue(33, value); } }
        [Display(Name = "Priority (34)", Order = 34), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority34 { get { return priorityValues.GetValue(34); } set { priorityValues.SetValue(34, value); } }
        [Display(Name = "Subject (35)", Order = 35), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject35 { get { return subjectValues.GetValue(35); } set { subjectValues.SetValue(35, value); } }

        [Display(Name = "From (36)", Order = 36)]
        public string From36 { get { return fromValues.GetValue(36); } set { fromValues.SetValue(36, value); } }
        [Display(Name = "To (37)", Order = 37)]
        public string To37 { get { return toValues.GetValue(37); } set { toValues.SetValue(37, value); } }
        [Display(Name = "Sent (38)", Order = 38)]
        public DateTime Sent38 { get { return sentValues.GetValue(38); } set { sentValues.SetValue(38, value); } }
        [Display(Name = "Has Attachment (39)", Order = 39)]
        public bool HasAttachment39 { get { return hasAttachmentValues.GetValue(39); } set { hasAttachmentValues.SetValue(39, value); } }
        [Display(Name = "Size (40)", Order = 40)]
        public int Size40 { get { return sizeValues.GetValue(40); } set { sizeValues.SetValue(40, value); } }
        [Display(Name = "Priority (41)", Order = 41), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority41 { get { return priorityValues.GetValue(41); } set { priorityValues.SetValue(41, value); } }
        [Display(Name = "Subject (42)", Order = 42), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject42 { get { return subjectValues.GetValue(42); } set { subjectValues.SetValue(42, value); } }

        [Display(Name = "From (43)", Order = 43)]
        public string From43 { get { return fromValues.GetValue(43); } set { fromValues.SetValue(43, value); } }
        [Display(Name = "To (44)", Order = 44)]
        public string To44 { get { return toValues.GetValue(44); } set { toValues.SetValue(44, value); } }
        [Display(Name = "Sent (45)", Order = 45)]
        public DateTime Sent45 { get { return sentValues.GetValue(45); } set { sentValues.SetValue(45, value); } }
        [Display(Name = "Has Attachment (46)", Order = 46)]
        public bool HasAttachment46 { get { return hasAttachmentValues.GetValue(46); } set { hasAttachmentValues.SetValue(46, value); } }
        [Display(Name = "Size (47)", Order = 47)]
        public int Size47 { get { return sizeValues.GetValue(47); } set { sizeValues.SetValue(47, value); } }
        [Display(Name = "Priority (48)", Order = 48), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority48 { get { return priorityValues.GetValue(48); } set { priorityValues.SetValue(48, value); } }
        [Display(Name = "Subject (49)", Order = 49), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject49 { get { return subjectValues.GetValue(49); } set { subjectValues.SetValue(49, value); } }

        [Display(Name = "From (50)", Order = 50)]
        public string From50 { get { return fromValues.GetValue(50); } set { fromValues.SetValue(50, value); } }
        [Display(Name = "To (51)", Order = 51)]
        public string To51 { get { return toValues.GetValue(51); } set { toValues.SetValue(51, value); } }
        [Display(Name = "Sent (52)", Order = 52)]
        public DateTime Sent52 { get { return sentValues.GetValue(52); } set { sentValues.SetValue(52, value); } }
        [Display(Name = "Has Attachment (53)", Order = 53)]
        public bool HasAttachment53 { get { return hasAttachmentValues.GetValue(53); } set { hasAttachmentValues.SetValue(53, value); } }
        [Display(Name = "Size (54)", Order = 54)]
        public int Size54 { get { return sizeValues.GetValue(54); } set { sizeValues.SetValue(54, value); } }
        [Display(Name = "Priority (55)", Order = 55), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority55 { get { return priorityValues.GetValue(55); } set { priorityValues.SetValue(55, value); } }
        [Display(Name = "Subject (56)", Order = 56), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject56 { get { return subjectValues.GetValue(56); } set { subjectValues.SetValue(56, value); } }

        [Display(Name = "From (57)", Order = 57)]
        public string From57 { get { return fromValues.GetValue(57); } set { fromValues.SetValue(57, value); } }
        [Display(Name = "To (58)", Order = 58)]
        public string To58 { get { return toValues.GetValue(58); } set { toValues.SetValue(58, value); } }
        [Display(Name = "Sent (59)", Order = 59)]
        public DateTime Sent59 { get { return sentValues.GetValue(59); } set { sentValues.SetValue(59, value); } }
        [Display(Name = "Has Attachment (60)", Order = 60)]
        public bool HasAttachment60 { get { return hasAttachmentValues.GetValue(60); } set { hasAttachmentValues.SetValue(60, value); } }
        [Display(Name = "Size (61)", Order = 61)]
        public int Size61 { get { return sizeValues.GetValue(61); } set { sizeValues.SetValue(61, value); } }
        [Display(Name = "Priority (62)", Order = 62), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority62 { get { return priorityValues.GetValue(62); } set { priorityValues.SetValue(62, value); } }
        [Display(Name = "Subject (63)", Order = 63), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject63 { get { return subjectValues.GetValue(63); } set { subjectValues.SetValue(63, value); } }

        [Display(Name = "From (64)", Order = 64)]
        public string From64 { get { return fromValues.GetValue(64); } set { fromValues.SetValue(64, value); } }
        [Display(Name = "To (65)", Order = 65)]
        public string To65 { get { return toValues.GetValue(65); } set { toValues.SetValue(65, value); } }
        [Display(Name = "Sent (66)", Order = 66)]
        public DateTime Sent66 { get { return sentValues.GetValue(66); } set { sentValues.SetValue(66, value); } }
        [Display(Name = "Has Attachment (67)", Order = 67)]
        public bool HasAttachment67 { get { return hasAttachmentValues.GetValue(67); } set { hasAttachmentValues.SetValue(67, value); } }
        [Display(Name = "Size (68)", Order = 68)]
        public int Size68 { get { return sizeValues.GetValue(68); } set { sizeValues.SetValue(68, value); } }
        [Display(Name = "Priority (69)", Order = 69), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority69 { get { return priorityValues.GetValue(69); } set { priorityValues.SetValue(69, value); } }
        [Display(Name = "Subject (70)", Order = 70), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject70 { get { return subjectValues.GetValue(70); } set { subjectValues.SetValue(70, value); } }

        [Display(Name = "From (71)", Order = 71)]
        public string From71 { get { return fromValues.GetValue(71); } set { fromValues.SetValue(71, value); } }
        [Display(Name = "To (72)", Order = 72)]
        public string To72 { get { return toValues.GetValue(72); } set { toValues.SetValue(72, value); } }
        [Display(Name = "Sent (73)", Order = 73)]
        public DateTime Sent73 { get { return sentValues.GetValue(73); } set { sentValues.SetValue(73, value); } }
        [Display(Name = "Has Attachment (74)", Order = 74)]
        public bool HasAttachment74 { get { return hasAttachmentValues.GetValue(74); } set { hasAttachmentValues.SetValue(74, value); } }
        [Display(Name = "Size (75)", Order = 75)]
        public int Size75 { get { return sizeValues.GetValue(75); } set { sizeValues.SetValue(75, value); } }
        [Display(Name = "Priority (76)", Order = 76), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority76 { get { return priorityValues.GetValue(76); } set { priorityValues.SetValue(76, value); } }
        [Display(Name = "Subject (77)", Order = 77), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject77 { get { return subjectValues.GetValue(77); } set { subjectValues.SetValue(77, value); } }

        [Display(Name = "From (78)", Order = 78)]
        public string From78 { get { return fromValues.GetValue(78); } set { fromValues.SetValue(78, value); } }
        [Display(Name = "To (79)", Order = 79)]
        public string To79 { get { return toValues.GetValue(79); } set { toValues.SetValue(79, value); } }
        [Display(Name = "Sent (80)", Order = 80)]
        public DateTime Sent80 { get { return sentValues.GetValue(80); } set { sentValues.SetValue(80, value); } }
        [Display(Name = "Has Attachment (81)", Order = 81)]
        public bool HasAttachment81 { get { return hasAttachmentValues.GetValue(81); } set { hasAttachmentValues.SetValue(81, value); } }
        [Display(Name = "Size (82)", Order = 82)]
        public int Size82 { get { return sizeValues.GetValue(82); } set { sizeValues.SetValue(82, value); } }
        [Display(Name = "Priority (83)", Order = 83), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority83 { get { return priorityValues.GetValue(83); } set { priorityValues.SetValue(83, value); } }
        [Display(Name = "Subject (84)", Order = 84), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject84 { get { return subjectValues.GetValue(84); } set { subjectValues.SetValue(84, value); } }

        [Display(Name = "From (85)", Order = 85)]
        public string From85 { get { return fromValues.GetValue(85); } set { fromValues.SetValue(85, value); } }
        [Display(Name = "To (86)", Order = 86)]
        public string To86 { get { return toValues.GetValue(86); } set { toValues.SetValue(86, value); } }
        [Display(Name = "Sent (87)", Order = 87)]
        public DateTime Sent87 { get { return sentValues.GetValue(87); } set { sentValues.SetValue(87, value); } }
        [Display(Name = "Has Attachment (88)", Order = 88)]
        public bool HasAttachment88 { get { return hasAttachmentValues.GetValue(88); } set { hasAttachmentValues.SetValue(88, value); } }
        [Display(Name = "Size (89)", Order = 89)]
        public int Size89 { get { return sizeValues.GetValue(89); } set { sizeValues.SetValue(89, value); } }
        [Display(Name = "Priority (90)", Order = 90), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority90 { get { return priorityValues.GetValue(90); } set { priorityValues.SetValue(90, value); } }
        [Display(Name = "Subject (91)", Order = 91), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject91 { get { return subjectValues.GetValue(91); } set { subjectValues.SetValue(91, value); } }

        [Display(Name = "From (92)", Order = 92)]
        public string From92 { get { return fromValues.GetValue(92); } set { fromValues.SetValue(92, value); } }
        [Display(Name = "To (93)", Order = 93)]
        public string To93 { get { return toValues.GetValue(93); } set { toValues.SetValue(93, value); } }
        [Display(Name = "Sent (94)", Order = 94)]
        public DateTime Sent94 { get { return sentValues.GetValue(94); } set { sentValues.SetValue(94, value); } }
        [Display(Name = "Has Attachment (95)", Order = 95)]
        public bool HasAttachment95 { get { return hasAttachmentValues.GetValue(95); } set { hasAttachmentValues.SetValue(95, value); } }
        [Display(Name = "Size (96)", Order = 96)]
        public int Size96 { get { return sizeValues.GetValue(96); } set { sizeValues.SetValue(96, value); } }
        [Display(Name = "Priority (97)", Order = 97), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority97 { get { return priorityValues.GetValue(97); } set { priorityValues.SetValue(97, value); } }
        [Display(Name = "Subject (98)", Order = 98), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject98 { get { return subjectValues.GetValue(98); } set { subjectValues.SetValue(98, value); } }

        [Display(Name = "From (99)", Order = 99)]
        public string From99 { get { return fromValues.GetValue(99); } set { fromValues.SetValue(99, value); } }
        [Display(Name = "To (100)", Order = 100)]
        public string To100 { get { return toValues.GetValue(100); } set { toValues.SetValue(100, value); } }
        [Display(Name = "Sent (101)", Order = 101)]
        public DateTime Sent101 { get { return sentValues.GetValue(101); } set { sentValues.SetValue(101, value); } }
        [Display(Name = "Has Attachment (102)", Order = 102)]
        public bool HasAttachment102 { get { return hasAttachmentValues.GetValue(102); } set { hasAttachmentValues.SetValue(102, value); } }
        [Display(Name = "Size (103)", Order = 103)]
        public int Size103 { get { return sizeValues.GetValue(103); } set { sizeValues.SetValue(103, value); } }
        [Display(Name = "Priority (104)", Order = 104), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority104 { get { return priorityValues.GetValue(104); } set { priorityValues.SetValue(104, value); } }
        [Display(Name = "Subject (105)", Order = 105), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject105 { get { return subjectValues.GetValue(105); } set { subjectValues.SetValue(105, value); } }

        [Display(Name = "From (106)", Order = 106)]
        public string From106 { get { return fromValues.GetValue(106); } set { fromValues.SetValue(106, value); } }
        [Display(Name = "To (107)", Order = 107)]
        public string To107 { get { return toValues.GetValue(107); } set { toValues.SetValue(107, value); } }
        [Display(Name = "Sent (108)", Order = 108)]
        public DateTime Sent108 { get { return sentValues.GetValue(108); } set { sentValues.SetValue(108, value); } }
        [Display(Name = "Has Attachment (109)", Order = 109)]
        public bool HasAttachment109 { get { return hasAttachmentValues.GetValue(109); } set { hasAttachmentValues.SetValue(109, value); } }
        [Display(Name = "Size (110)", Order = 110)]
        public int Size110 { get { return sizeValues.GetValue(110); } set { sizeValues.SetValue(110, value); } }
        [Display(Name = "Priority (111)", Order = 111), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority111 { get { return priorityValues.GetValue(111); } set { priorityValues.SetValue(111, value); } }
        [Display(Name = "Subject (112)", Order = 112), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject112 { get { return subjectValues.GetValue(112); } set { subjectValues.SetValue(112, value); } }

        [Display(Name = "From (113)", Order = 113)]
        public string From113 { get { return fromValues.GetValue(113); } set { fromValues.SetValue(113, value); } }
        [Display(Name = "To (114)", Order = 114)]
        public string To114 { get { return toValues.GetValue(114); } set { toValues.SetValue(114, value); } }
        [Display(Name = "Sent (115)", Order = 115)]
        public DateTime Sent115 { get { return sentValues.GetValue(115); } set { sentValues.SetValue(115, value); } }
        [Display(Name = "Has Attachment (116)", Order = 116)]
        public bool HasAttachment116 { get { return hasAttachmentValues.GetValue(116); } set { hasAttachmentValues.SetValue(116, value); } }
        [Display(Name = "Size (117)", Order = 117)]
        public int Size117 { get { return sizeValues.GetValue(117); } set { sizeValues.SetValue(117, value); } }
        [Display(Name = "Priority (118)", Order = 118), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority118 { get { return priorityValues.GetValue(118); } set { priorityValues.SetValue(118, value); } }
        [Display(Name = "Subject (119)", Order = 119), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject119 { get { return subjectValues.GetValue(119); } set { subjectValues.SetValue(119, value); } }

        [Display(Name = "From (120)", Order = 120)]
        public string From120 { get { return fromValues.GetValue(120); } set { fromValues.SetValue(120, value); } }
        [Display(Name = "To (121)", Order = 121)]
        public string To121 { get { return toValues.GetValue(121); } set { toValues.SetValue(121, value); } }
        [Display(Name = "Sent (122)", Order = 122)]
        public DateTime Sent122 { get { return sentValues.GetValue(122); } set { sentValues.SetValue(122, value); } }
        [Display(Name = "Has Attachment (123)", Order = 123)]
        public bool HasAttachment123 { get { return hasAttachmentValues.GetValue(123); } set { hasAttachmentValues.SetValue(123, value); } }
        [Display(Name = "Size (124)", Order = 124)]
        public int Size124 { get { return sizeValues.GetValue(124); } set { sizeValues.SetValue(124, value); } }
        [Display(Name = "Priority (125)", Order = 125), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority125 { get { return priorityValues.GetValue(125); } set { priorityValues.SetValue(125, value); } }
        [Display(Name = "Subject (126)", Order = 126), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject126 { get { return subjectValues.GetValue(126); } set { subjectValues.SetValue(126, value); } }

        [Display(Name = "From (127)", Order = 127)]
        public string From127 { get { return fromValues.GetValue(127); } set { fromValues.SetValue(127, value); } }
        [Display(Name = "To (128)", Order = 128)]
        public string To128 { get { return toValues.GetValue(128); } set { toValues.SetValue(128, value); } }
        [Display(Name = "Sent (129)", Order = 129)]
        public DateTime Sent129 { get { return sentValues.GetValue(129); } set { sentValues.SetValue(129, value); } }
        [Display(Name = "Has Attachment (130)", Order = 130)]
        public bool HasAttachment130 { get { return hasAttachmentValues.GetValue(130); } set { hasAttachmentValues.SetValue(130, value); } }
        [Display(Name = "Size (131)", Order = 131)]
        public int Size131 { get { return sizeValues.GetValue(131); } set { sizeValues.SetValue(131, value); } }
        [Display(Name = "Priority (132)", Order = 132), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority132 { get { return priorityValues.GetValue(132); } set { priorityValues.SetValue(132, value); } }
        [Display(Name = "Subject (133)", Order = 133), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject133 { get { return subjectValues.GetValue(133); } set { subjectValues.SetValue(133, value); } }

        [Display(Name = "From (134)", Order = 134)]
        public string From134 { get { return fromValues.GetValue(134); } set { fromValues.SetValue(134, value); } }
        [Display(Name = "To (135)", Order = 135)]
        public string To135 { get { return toValues.GetValue(135); } set { toValues.SetValue(135, value); } }
        [Display(Name = "Sent (136)", Order = 136)]
        public DateTime Sent136 { get { return sentValues.GetValue(136); } set { sentValues.SetValue(136, value); } }
        [Display(Name = "Has Attachment (137)", Order = 137)]
        public bool HasAttachment137 { get { return hasAttachmentValues.GetValue(137); } set { hasAttachmentValues.SetValue(137, value); } }
        [Display(Name = "Size (138)", Order = 138)]
        public int Size138 { get { return sizeValues.GetValue(138); } set { sizeValues.SetValue(138, value); } }
        [Display(Name = "Priority (139)", Order = 139), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority139 { get { return priorityValues.GetValue(139); } set { priorityValues.SetValue(139, value); } }
        [Display(Name = "Subject (140)", Order = 140), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject140 { get { return subjectValues.GetValue(140); } set { subjectValues.SetValue(140, value); } }

        [Display(Name = "From (141)", Order = 141)]
        public string From141 { get { return fromValues.GetValue(141); } set { fromValues.SetValue(141, value); } }
        [Display(Name = "To (142)", Order = 142)]
        public string To142 { get { return toValues.GetValue(142); } set { toValues.SetValue(142, value); } }
        [Display(Name = "Sent (143)", Order = 143)]
        public DateTime Sent143 { get { return sentValues.GetValue(143); } set { sentValues.SetValue(143, value); } }
        [Display(Name = "Has Attachment (144)", Order = 144)]
        public bool HasAttachment144 { get { return hasAttachmentValues.GetValue(144); } set { hasAttachmentValues.SetValue(144, value); } }
        [Display(Name = "Size (145)", Order = 145)]
        public int Size145 { get { return sizeValues.GetValue(145); } set { sizeValues.SetValue(145, value); } }
        [Display(Name = "Priority (146)", Order = 146), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority146 { get { return priorityValues.GetValue(146); } set { priorityValues.SetValue(146, value); } }
        [Display(Name = "Subject (147)", Order = 147), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject147 { get { return subjectValues.GetValue(147); } set { subjectValues.SetValue(147, value); } }

        [Display(Name = "From (148)", Order = 148)]
        public string From148 { get { return fromValues.GetValue(148); } set { fromValues.SetValue(148, value); } }
        [Display(Name = "To (149)", Order = 149)]
        public string To149 { get { return toValues.GetValue(149); } set { toValues.SetValue(149, value); } }
        [Display(Name = "Sent (150)", Order = 150)]
        public DateTime Sent150 { get { return sentValues.GetValue(150); } set { sentValues.SetValue(150, value); } }
        [Display(Name = "Has Attachment (151)", Order = 151)]
        public bool HasAttachment151 { get { return hasAttachmentValues.GetValue(151); } set { hasAttachmentValues.SetValue(151, value); } }
        [Display(Name = "Size (152)", Order = 152)]
        public int Size152 { get { return sizeValues.GetValue(152); } set { sizeValues.SetValue(152, value); } }
        [Display(Name = "Priority (153)", Order = 153), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority153 { get { return priorityValues.GetValue(153); } set { priorityValues.SetValue(153, value); } }
        [Display(Name = "Subject (154)", Order = 154), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject154 { get { return subjectValues.GetValue(154); } set { subjectValues.SetValue(154, value); } }

        [Display(Name = "From (155)", Order = 155)]
        public string From155 { get { return fromValues.GetValue(155); } set { fromValues.SetValue(155, value); } }
        [Display(Name = "To (156)", Order = 156)]
        public string To156 { get { return toValues.GetValue(156); } set { toValues.SetValue(156, value); } }
        [Display(Name = "Sent (157)", Order = 157)]
        public DateTime Sent157 { get { return sentValues.GetValue(157); } set { sentValues.SetValue(157, value); } }
        [Display(Name = "Has Attachment (158)", Order = 158)]
        public bool HasAttachment158 { get { return hasAttachmentValues.GetValue(158); } set { hasAttachmentValues.SetValue(158, value); } }
        [Display(Name = "Size (159)", Order = 159)]
        public int Size159 { get { return sizeValues.GetValue(159); } set { sizeValues.SetValue(159, value); } }
        [Display(Name = "Priority (160)", Order = 160), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority160 { get { return priorityValues.GetValue(160); } set { priorityValues.SetValue(160, value); } }
        [Display(Name = "Subject (161)", Order = 161), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject161 { get { return subjectValues.GetValue(161); } set { subjectValues.SetValue(161, value); } }

        [Display(Name = "From (162)", Order = 162)]
        public string From162 { get { return fromValues.GetValue(162); } set { fromValues.SetValue(162, value); } }
        [Display(Name = "To (163)", Order = 163)]
        public string To163 { get { return toValues.GetValue(163); } set { toValues.SetValue(163, value); } }
        [Display(Name = "Sent (164)", Order = 164)]
        public DateTime Sent164 { get { return sentValues.GetValue(164); } set { sentValues.SetValue(164, value); } }
        [Display(Name = "Has Attachment (165)", Order = 165)]
        public bool HasAttachment165 { get { return hasAttachmentValues.GetValue(165); } set { hasAttachmentValues.SetValue(165, value); } }
        [Display(Name = "Size (166)", Order = 166)]
        public int Size166 { get { return sizeValues.GetValue(166); } set { sizeValues.SetValue(166, value); } }
        [Display(Name = "Priority (167)", Order = 167), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority167 { get { return priorityValues.GetValue(167); } set { priorityValues.SetValue(167, value); } }
        [Display(Name = "Subject (168)", Order = 168), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject168 { get { return subjectValues.GetValue(168); } set { subjectValues.SetValue(168, value); } }

        [Display(Name = "From (169)", Order = 169)]
        public string From169 { get { return fromValues.GetValue(169); } set { fromValues.SetValue(169, value); } }
        [Display(Name = "To (170)", Order = 170)]
        public string To170 { get { return toValues.GetValue(170); } set { toValues.SetValue(170, value); } }
        [Display(Name = "Sent (171)", Order = 171)]
        public DateTime Sent171 { get { return sentValues.GetValue(171); } set { sentValues.SetValue(171, value); } }
        [Display(Name = "Has Attachment (172)", Order = 172)]
        public bool HasAttachment172 { get { return hasAttachmentValues.GetValue(172); } set { hasAttachmentValues.SetValue(172, value); } }
        [Display(Name = "Size (173)", Order = 173)]
        public int Size173 { get { return sizeValues.GetValue(173); } set { sizeValues.SetValue(173, value); } }
        [Display(Name = "Priority (174)", Order = 174), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority174 { get { return priorityValues.GetValue(174); } set { priorityValues.SetValue(174, value); } }
        [Display(Name = "Subject (175)", Order = 175), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject175 { get { return subjectValues.GetValue(175); } set { subjectValues.SetValue(175, value); } }

        [Display(Name = "From (176)", Order = 176)]
        public string From176 { get { return fromValues.GetValue(176); } set { fromValues.SetValue(176, value); } }
        [Display(Name = "To (177)", Order = 177)]
        public string To177 { get { return toValues.GetValue(177); } set { toValues.SetValue(177, value); } }
        [Display(Name = "Sent (178)", Order = 178)]
        public DateTime Sent178 { get { return sentValues.GetValue(178); } set { sentValues.SetValue(178, value); } }
        [Display(Name = "Has Attachment (179)", Order = 179)]
        public bool HasAttachment179 { get { return hasAttachmentValues.GetValue(179); } set { hasAttachmentValues.SetValue(179, value); } }
        [Display(Name = "Size (180)", Order = 180)]
        public int Size180 { get { return sizeValues.GetValue(180); } set { sizeValues.SetValue(180, value); } }
        [Display(Name = "Priority (181)", Order = 181), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority181 { get { return priorityValues.GetValue(181); } set { priorityValues.SetValue(181, value); } }
        [Display(Name = "Subject (182)", Order = 182), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject182 { get { return subjectValues.GetValue(182); } set { subjectValues.SetValue(182, value); } }

        [Display(Name = "From (183)", Order = 183)]
        public string From183 { get { return fromValues.GetValue(183); } set { fromValues.SetValue(183, value); } }
        [Display(Name = "To (184)", Order = 184)]
        public string To184 { get { return toValues.GetValue(184); } set { toValues.SetValue(184, value); } }
        [Display(Name = "Sent (185)", Order = 185)]
        public DateTime Sent185 { get { return sentValues.GetValue(185); } set { sentValues.SetValue(185, value); } }
        [Display(Name = "Has Attachment (186)", Order = 186)]
        public bool HasAttachment186 { get { return hasAttachmentValues.GetValue(186); } set { hasAttachmentValues.SetValue(186, value); } }
        [Display(Name = "Size (187)", Order = 187)]
        public int Size187 { get { return sizeValues.GetValue(187); } set { sizeValues.SetValue(187, value); } }
        [Display(Name = "Priority (188)", Order = 188), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority188 { get { return priorityValues.GetValue(188); } set { priorityValues.SetValue(188, value); } }
        [Display(Name = "Subject (189)", Order = 189), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject189 { get { return subjectValues.GetValue(189); } set { subjectValues.SetValue(189, value); } }

        [Display(Name = "From (190)", Order = 190)]
        public string From190 { get { return fromValues.GetValue(190); } set { fromValues.SetValue(190, value); } }
        [Display(Name = "To (191)", Order = 191)]
        public string To191 { get { return toValues.GetValue(191); } set { toValues.SetValue(191, value); } }
        [Display(Name = "Sent (192)", Order = 192)]
        public DateTime Sent192 { get { return sentValues.GetValue(192); } set { sentValues.SetValue(192, value); } }
        [Display(Name = "Has Attachment (193)", Order = 193)]
        public bool HasAttachment193 { get { return hasAttachmentValues.GetValue(193); } set { hasAttachmentValues.SetValue(193, value); } }
        [Display(Name = "Size (194)", Order = 194)]
        public int Size194 { get { return sizeValues.GetValue(194); } set { sizeValues.SetValue(194, value); } }
        [Display(Name = "Priority (195)", Order = 195), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority195 { get { return priorityValues.GetValue(195); } set { priorityValues.SetValue(195, value); } }
        [Display(Name = "Subject (196)", Order = 196), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject196 { get { return subjectValues.GetValue(196); } set { subjectValues.SetValue(196, value); } }

        [Display(Name = "From (197)", Order = 197)]
        public string From197 { get { return fromValues.GetValue(197); } set { fromValues.SetValue(197, value); } }
        [Display(Name = "To (198)", Order = 198)]
        public string To198 { get { return toValues.GetValue(198); } set { toValues.SetValue(198, value); } }
        [Display(Name = "Sent (199)", Order = 199)]
        public DateTime Sent199 { get { return sentValues.GetValue(199); } set { sentValues.SetValue(199, value); } }
        [Display(Name = "Has Attachment (200)", Order = 200)]
        public bool HasAttachment200 { get { return hasAttachmentValues.GetValue(200); } set { hasAttachmentValues.SetValue(200, value); } }
        [Display(Name = "Size (201)", Order = 201)]
        public int Size201 { get { return sizeValues.GetValue(201); } set { sizeValues.SetValue(201, value); } }
        [Display(Name = "Priority (202)", Order = 202), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority202 { get { return priorityValues.GetValue(202); } set { priorityValues.SetValue(202, value); } }
        [Display(Name = "Subject (203)", Order = 203), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject203 { get { return subjectValues.GetValue(203); } set { subjectValues.SetValue(203, value); } }

        [Display(Name = "From (204)", Order = 204)]
        public string From204 { get { return fromValues.GetValue(204); } set { fromValues.SetValue(204, value); } }
        [Display(Name = "To (205)", Order = 205)]
        public string To205 { get { return toValues.GetValue(205); } set { toValues.SetValue(205, value); } }
        [Display(Name = "Sent (206)", Order = 206)]
        public DateTime Sent206 { get { return sentValues.GetValue(206); } set { sentValues.SetValue(206, value); } }
        [Display(Name = "Has Attachment (207)", Order = 207)]
        public bool HasAttachment207 { get { return hasAttachmentValues.GetValue(207); } set { hasAttachmentValues.SetValue(207, value); } }
        [Display(Name = "Size (208)", Order = 208)]
        public int Size208 { get { return sizeValues.GetValue(208); } set { sizeValues.SetValue(208, value); } }
        [Display(Name = "Priority (209)", Order = 209), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority209 { get { return priorityValues.GetValue(209); } set { priorityValues.SetValue(209, value); } }
        [Display(Name = "Subject (210)", Order = 210), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject210 { get { return subjectValues.GetValue(210); } set { subjectValues.SetValue(210, value); } }

        [Display(Name = "From (211)", Order = 211)]
        public string From211 { get { return fromValues.GetValue(211); } set { fromValues.SetValue(211, value); } }
        [Display(Name = "To (212)", Order = 212)]
        public string To212 { get { return toValues.GetValue(212); } set { toValues.SetValue(212, value); } }
        [Display(Name = "Sent (213)", Order = 213)]
        public DateTime Sent213 { get { return sentValues.GetValue(213); } set { sentValues.SetValue(213, value); } }
        [Display(Name = "Has Attachment (214)", Order = 214)]
        public bool HasAttachment214 { get { return hasAttachmentValues.GetValue(214); } set { hasAttachmentValues.SetValue(214, value); } }
        [Display(Name = "Size (215)", Order = 215)]
        public int Size215 { get { return sizeValues.GetValue(215); } set { sizeValues.SetValue(215, value); } }
        [Display(Name = "Priority (216)", Order = 216), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority216 { get { return priorityValues.GetValue(216); } set { priorityValues.SetValue(216, value); } }
        [Display(Name = "Subject (217)", Order = 217), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject217 { get { return subjectValues.GetValue(217); } set { subjectValues.SetValue(217, value); } }

        [Display(Name = "From (218)", Order = 218)]
        public string From218 { get { return fromValues.GetValue(218); } set { fromValues.SetValue(218, value); } }
        [Display(Name = "To (219)", Order = 219)]
        public string To219 { get { return toValues.GetValue(219); } set { toValues.SetValue(219, value); } }
        [Display(Name = "Sent (220)", Order = 220)]
        public DateTime Sent220 { get { return sentValues.GetValue(220); } set { sentValues.SetValue(220, value); } }
        [Display(Name = "Has Attachment (221)", Order = 221)]
        public bool HasAttachment221 { get { return hasAttachmentValues.GetValue(221); } set { hasAttachmentValues.SetValue(221, value); } }
        [Display(Name = "Size (222)", Order = 222)]
        public int Size222 { get { return sizeValues.GetValue(222); } set { sizeValues.SetValue(222, value); } }
        [Display(Name = "Priority (223)", Order = 223), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority223 { get { return priorityValues.GetValue(223); } set { priorityValues.SetValue(223, value); } }
        [Display(Name = "Subject (224)", Order = 224), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject224 { get { return subjectValues.GetValue(224); } set { subjectValues.SetValue(224, value); } }

        [Display(Name = "From (225)", Order = 225)]
        public string From225 { get { return fromValues.GetValue(225); } set { fromValues.SetValue(225, value); } }
        [Display(Name = "To (226)", Order = 226)]
        public string To226 { get { return toValues.GetValue(226); } set { toValues.SetValue(226, value); } }
        [Display(Name = "Sent (227)", Order = 227)]
        public DateTime Sent227 { get { return sentValues.GetValue(227); } set { sentValues.SetValue(227, value); } }
        [Display(Name = "Has Attachment (228)", Order = 228)]
        public bool HasAttachment228 { get { return hasAttachmentValues.GetValue(228); } set { hasAttachmentValues.SetValue(228, value); } }
        [Display(Name = "Size (229)", Order = 229)]
        public int Size229 { get { return sizeValues.GetValue(229); } set { sizeValues.SetValue(229, value); } }
        [Display(Name = "Priority (230)", Order = 230), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority230 { get { return priorityValues.GetValue(230); } set { priorityValues.SetValue(230, value); } }
        [Display(Name = "Subject (231)", Order = 231), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject231 { get { return subjectValues.GetValue(231); } set { subjectValues.SetValue(231, value); } }

        [Display(Name = "From (232)", Order = 232)]
        public string From232 { get { return fromValues.GetValue(232); } set { fromValues.SetValue(232, value); } }
        [Display(Name = "To (233)", Order = 233)]
        public string To233 { get { return toValues.GetValue(233); } set { toValues.SetValue(233, value); } }
        [Display(Name = "Sent (234)", Order = 234)]
        public DateTime Sent234 { get { return sentValues.GetValue(234); } set { sentValues.SetValue(234, value); } }
        [Display(Name = "Has Attachment (235)", Order = 235)]
        public bool HasAttachment235 { get { return hasAttachmentValues.GetValue(235); } set { hasAttachmentValues.SetValue(235, value); } }
        [Display(Name = "Size (236)", Order = 236)]
        public int Size236 { get { return sizeValues.GetValue(236); } set { sizeValues.SetValue(236, value); } }
        [Display(Name = "Priority (237)", Order = 237), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority237 { get { return priorityValues.GetValue(237); } set { priorityValues.SetValue(237, value); } }
        [Display(Name = "Subject (238)", Order = 238), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject238 { get { return subjectValues.GetValue(238); } set { subjectValues.SetValue(238, value); } }

        [Display(Name = "From (239)", Order = 239)]
        public string From239 { get { return fromValues.GetValue(239); } set { fromValues.SetValue(239, value); } }
        [Display(Name = "To (240)", Order = 240)]
        public string To240 { get { return toValues.GetValue(240); } set { toValues.SetValue(240, value); } }
        [Display(Name = "Sent (241)", Order = 241)]
        public DateTime Sent241 { get { return sentValues.GetValue(241); } set { sentValues.SetValue(241, value); } }
        [Display(Name = "Has Attachment (242)", Order = 242)]
        public bool HasAttachment242 { get { return hasAttachmentValues.GetValue(242); } set { hasAttachmentValues.SetValue(242, value); } }
        [Display(Name = "Size (243)", Order = 243)]
        public int Size243 { get { return sizeValues.GetValue(243); } set { sizeValues.SetValue(243, value); } }
        [Display(Name = "Priority (244)", Order = 244), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority244 { get { return priorityValues.GetValue(244); } set { priorityValues.SetValue(244, value); } }
        [Display(Name = "Subject (245)", Order = 245), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject245 { get { return subjectValues.GetValue(245); } set { subjectValues.SetValue(245, value); } }

        [Display(Name = "From (246)", Order = 246)]
        public string From246 { get { return fromValues.GetValue(246); } set { fromValues.SetValue(246, value); } }
        [Display(Name = "To (247)", Order = 247)]
        public string To247 { get { return toValues.GetValue(247); } set { toValues.SetValue(247, value); } }
        [Display(Name = "Sent (248)", Order = 248)]
        public DateTime Sent248 { get { return sentValues.GetValue(248); } set { sentValues.SetValue(248, value); } }
        [Display(Name = "Has Attachment (249)", Order = 249)]
        public bool HasAttachment249 { get { return hasAttachmentValues.GetValue(249); } set { hasAttachmentValues.SetValue(249, value); } }
        #endregion
    }
    public class LargeDataSourceObjectLarge : LargeDataSourceObjectMedium {
        public LargeDataSourceObjectLarge(int id)
            : base(id) {
        }

        #region properties
        [Display(Name = "Size (250)", Order = 250)]
        public int Size250 { get { return sizeValues.GetValue(250); } set { sizeValues.SetValue(250, value); } }
        [Display(Name = "Priority (251)", Order = 251), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority251 { get { return priorityValues.GetValue(251); } set { priorityValues.SetValue(251, value); } }
        [Display(Name = "Subject (252)", Order = 252), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject252 { get { return subjectValues.GetValue(252); } set { subjectValues.SetValue(252, value); } }

        [Display(Name = "From (253)", Order = 253)]
        public string From253 { get { return fromValues.GetValue(253); } set { fromValues.SetValue(253, value); } }
        [Display(Name = "To (254)", Order = 254)]
        public string To254 { get { return toValues.GetValue(254); } set { toValues.SetValue(254, value); } }
        [Display(Name = "Sent (255)", Order = 255)]
        public DateTime Sent255 { get { return sentValues.GetValue(255); } set { sentValues.SetValue(255, value); } }
        [Display(Name = "Has Attachment (256)", Order = 256)]
        public bool HasAttachment256 { get { return hasAttachmentValues.GetValue(256); } set { hasAttachmentValues.SetValue(256, value); } }
        [Display(Name = "Size (257)", Order = 257)]
        public int Size257 { get { return sizeValues.GetValue(257); } set { sizeValues.SetValue(257, value); } }
        [Display(Name = "Priority (258)", Order = 258), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority258 { get { return priorityValues.GetValue(258); } set { priorityValues.SetValue(258, value); } }
        [Display(Name = "Subject (259)", Order = 259), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject259 { get { return subjectValues.GetValue(259); } set { subjectValues.SetValue(259, value); } }

        [Display(Name = "From (260)", Order = 260)]
        public string From260 { get { return fromValues.GetValue(260); } set { fromValues.SetValue(260, value); } }
        [Display(Name = "To (261)", Order = 261)]
        public string To261 { get { return toValues.GetValue(261); } set { toValues.SetValue(261, value); } }
        [Display(Name = "Sent (262)", Order = 262)]
        public DateTime Sent262 { get { return sentValues.GetValue(262); } set { sentValues.SetValue(262, value); } }
        [Display(Name = "Has Attachment (263)", Order = 263)]
        public bool HasAttachment263 { get { return hasAttachmentValues.GetValue(263); } set { hasAttachmentValues.SetValue(263, value); } }
        [Display(Name = "Size (264)", Order = 264)]
        public int Size264 { get { return sizeValues.GetValue(264); } set { sizeValues.SetValue(264, value); } }
        [Display(Name = "Priority (265)", Order = 265), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority265 { get { return priorityValues.GetValue(265); } set { priorityValues.SetValue(265, value); } }
        [Display(Name = "Subject (266)", Order = 266), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject266 { get { return subjectValues.GetValue(266); } set { subjectValues.SetValue(266, value); } }

        [Display(Name = "From (267)", Order = 267)]
        public string From267 { get { return fromValues.GetValue(267); } set { fromValues.SetValue(267, value); } }
        [Display(Name = "To (268)", Order = 268)]
        public string To268 { get { return toValues.GetValue(268); } set { toValues.SetValue(268, value); } }
        [Display(Name = "Sent (269)", Order = 269)]
        public DateTime Sent269 { get { return sentValues.GetValue(269); } set { sentValues.SetValue(269, value); } }
        [Display(Name = "Has Attachment (270)", Order = 270)]
        public bool HasAttachment270 { get { return hasAttachmentValues.GetValue(270); } set { hasAttachmentValues.SetValue(270, value); } }
        [Display(Name = "Size (271)", Order = 271)]
        public int Size271 { get { return sizeValues.GetValue(271); } set { sizeValues.SetValue(271, value); } }
        [Display(Name = "Priority (272)", Order = 272), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority272 { get { return priorityValues.GetValue(272); } set { priorityValues.SetValue(272, value); } }
        [Display(Name = "Subject (273)", Order = 273), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject273 { get { return subjectValues.GetValue(273); } set { subjectValues.SetValue(273, value); } }

        [Display(Name = "From (274)", Order = 274)]
        public string From274 { get { return fromValues.GetValue(274); } set { fromValues.SetValue(274, value); } }
        [Display(Name = "To (275)", Order = 275)]
        public string To275 { get { return toValues.GetValue(275); } set { toValues.SetValue(275, value); } }
        [Display(Name = "Sent (276)", Order = 276)]
        public DateTime Sent276 { get { return sentValues.GetValue(276); } set { sentValues.SetValue(276, value); } }
        [Display(Name = "Has Attachment (277)", Order = 277)]
        public bool HasAttachment277 { get { return hasAttachmentValues.GetValue(277); } set { hasAttachmentValues.SetValue(277, value); } }
        [Display(Name = "Size (278)", Order = 278)]
        public int Size278 { get { return sizeValues.GetValue(278); } set { sizeValues.SetValue(278, value); } }
        [Display(Name = "Priority (279)", Order = 279), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority279 { get { return priorityValues.GetValue(279); } set { priorityValues.SetValue(279, value); } }
        [Display(Name = "Subject (280)", Order = 280), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject280 { get { return subjectValues.GetValue(280); } set { subjectValues.SetValue(280, value); } }

        [Display(Name = "From (281)", Order = 281)]
        public string From281 { get { return fromValues.GetValue(281); } set { fromValues.SetValue(281, value); } }
        [Display(Name = "To (282)", Order = 282)]
        public string To282 { get { return toValues.GetValue(282); } set { toValues.SetValue(282, value); } }
        [Display(Name = "Sent (283)", Order = 283)]
        public DateTime Sent283 { get { return sentValues.GetValue(283); } set { sentValues.SetValue(283, value); } }
        [Display(Name = "Has Attachment (284)", Order = 284)]
        public bool HasAttachment284 { get { return hasAttachmentValues.GetValue(284); } set { hasAttachmentValues.SetValue(284, value); } }
        [Display(Name = "Size (285)", Order = 285)]
        public int Size285 { get { return sizeValues.GetValue(285); } set { sizeValues.SetValue(285, value); } }
        [Display(Name = "Priority (286)", Order = 286), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority286 { get { return priorityValues.GetValue(286); } set { priorityValues.SetValue(286, value); } }
        [Display(Name = "Subject (287)", Order = 287), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject287 { get { return subjectValues.GetValue(287); } set { subjectValues.SetValue(287, value); } }

        [Display(Name = "From (288)", Order = 288)]
        public string From288 { get { return fromValues.GetValue(288); } set { fromValues.SetValue(288, value); } }
        [Display(Name = "To (289)", Order = 289)]
        public string To289 { get { return toValues.GetValue(289); } set { toValues.SetValue(289, value); } }
        [Display(Name = "Sent (290)", Order = 290)]
        public DateTime Sent290 { get { return sentValues.GetValue(290); } set { sentValues.SetValue(290, value); } }
        [Display(Name = "Has Attachment (291)", Order = 291)]
        public bool HasAttachment291 { get { return hasAttachmentValues.GetValue(291); } set { hasAttachmentValues.SetValue(291, value); } }
        [Display(Name = "Size (292)", Order = 292)]
        public int Size292 { get { return sizeValues.GetValue(292); } set { sizeValues.SetValue(292, value); } }
        [Display(Name = "Priority (293)", Order = 293), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority293 { get { return priorityValues.GetValue(293); } set { priorityValues.SetValue(293, value); } }
        [Display(Name = "Subject (294)", Order = 294), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject294 { get { return subjectValues.GetValue(294); } set { subjectValues.SetValue(294, value); } }

        [Display(Name = "From (295)", Order = 295)]
        public string From295 { get { return fromValues.GetValue(295); } set { fromValues.SetValue(295, value); } }
        [Display(Name = "To (296)", Order = 296)]
        public string To296 { get { return toValues.GetValue(296); } set { toValues.SetValue(296, value); } }
        [Display(Name = "Sent (297)", Order = 297)]
        public DateTime Sent297 { get { return sentValues.GetValue(297); } set { sentValues.SetValue(297, value); } }
        [Display(Name = "Has Attachment (298)", Order = 298)]
        public bool HasAttachment298 { get { return hasAttachmentValues.GetValue(298); } set { hasAttachmentValues.SetValue(298, value); } }
        [Display(Name = "Size (299)", Order = 299)]
        public int Size299 { get { return sizeValues.GetValue(299); } set { sizeValues.SetValue(299, value); } }
        [Display(Name = "Priority (300)", Order = 300), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority300 { get { return priorityValues.GetValue(300); } set { priorityValues.SetValue(300, value); } }
        [Display(Name = "Subject (301)", Order = 301), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject301 { get { return subjectValues.GetValue(301); } set { subjectValues.SetValue(301, value); } }

        [Display(Name = "From (302)", Order = 302)]
        public string From302 { get { return fromValues.GetValue(302); } set { fromValues.SetValue(302, value); } }
        [Display(Name = "To (303)", Order = 303)]
        public string To303 { get { return toValues.GetValue(303); } set { toValues.SetValue(303, value); } }
        [Display(Name = "Sent (304)", Order = 304)]
        public DateTime Sent304 { get { return sentValues.GetValue(304); } set { sentValues.SetValue(304, value); } }
        [Display(Name = "Has Attachment (305)", Order = 305)]
        public bool HasAttachment305 { get { return hasAttachmentValues.GetValue(305); } set { hasAttachmentValues.SetValue(305, value); } }
        [Display(Name = "Size (306)", Order = 306)]
        public int Size306 { get { return sizeValues.GetValue(306); } set { sizeValues.SetValue(306, value); } }
        [Display(Name = "Priority (307)", Order = 307), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority307 { get { return priorityValues.GetValue(307); } set { priorityValues.SetValue(307, value); } }
        [Display(Name = "Subject (308)", Order = 308), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject308 { get { return subjectValues.GetValue(308); } set { subjectValues.SetValue(308, value); } }

        [Display(Name = "From (309)", Order = 309)]
        public string From309 { get { return fromValues.GetValue(309); } set { fromValues.SetValue(309, value); } }
        [Display(Name = "To (310)", Order = 310)]
        public string To310 { get { return toValues.GetValue(310); } set { toValues.SetValue(310, value); } }
        [Display(Name = "Sent (311)", Order = 311)]
        public DateTime Sent311 { get { return sentValues.GetValue(311); } set { sentValues.SetValue(311, value); } }
        [Display(Name = "Has Attachment (312)", Order = 312)]
        public bool HasAttachment312 { get { return hasAttachmentValues.GetValue(312); } set { hasAttachmentValues.SetValue(312, value); } }
        [Display(Name = "Size (313)", Order = 313)]
        public int Size313 { get { return sizeValues.GetValue(313); } set { sizeValues.SetValue(313, value); } }
        [Display(Name = "Priority (314)", Order = 314), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority314 { get { return priorityValues.GetValue(314); } set { priorityValues.SetValue(314, value); } }
        [Display(Name = "Subject (315)", Order = 315), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject315 { get { return subjectValues.GetValue(315); } set { subjectValues.SetValue(315, value); } }

        [Display(Name = "From (316)", Order = 316)]
        public string From316 { get { return fromValues.GetValue(316); } set { fromValues.SetValue(316, value); } }
        [Display(Name = "To (317)", Order = 317)]
        public string To317 { get { return toValues.GetValue(317); } set { toValues.SetValue(317, value); } }
        [Display(Name = "Sent (318)", Order = 318)]
        public DateTime Sent318 { get { return sentValues.GetValue(318); } set { sentValues.SetValue(318, value); } }
        [Display(Name = "Has Attachment (319)", Order = 319)]
        public bool HasAttachment319 { get { return hasAttachmentValues.GetValue(319); } set { hasAttachmentValues.SetValue(319, value); } }
        [Display(Name = "Size (320)", Order = 320)]
        public int Size320 { get { return sizeValues.GetValue(320); } set { sizeValues.SetValue(320, value); } }
        [Display(Name = "Priority (321)", Order = 321), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority321 { get { return priorityValues.GetValue(321); } set { priorityValues.SetValue(321, value); } }
        [Display(Name = "Subject (322)", Order = 322), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject322 { get { return subjectValues.GetValue(322); } set { subjectValues.SetValue(322, value); } }

        [Display(Name = "From (323)", Order = 323)]
        public string From323 { get { return fromValues.GetValue(323); } set { fromValues.SetValue(323, value); } }
        [Display(Name = "To (324)", Order = 324)]
        public string To324 { get { return toValues.GetValue(324); } set { toValues.SetValue(324, value); } }
        [Display(Name = "Sent (325)", Order = 325)]
        public DateTime Sent325 { get { return sentValues.GetValue(325); } set { sentValues.SetValue(325, value); } }
        [Display(Name = "Has Attachment (326)", Order = 326)]
        public bool HasAttachment326 { get { return hasAttachmentValues.GetValue(326); } set { hasAttachmentValues.SetValue(326, value); } }
        [Display(Name = "Size (327)", Order = 327)]
        public int Size327 { get { return sizeValues.GetValue(327); } set { sizeValues.SetValue(327, value); } }
        [Display(Name = "Priority (328)", Order = 328), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority328 { get { return priorityValues.GetValue(328); } set { priorityValues.SetValue(328, value); } }
        [Display(Name = "Subject (329)", Order = 329), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject329 { get { return subjectValues.GetValue(329); } set { subjectValues.SetValue(329, value); } }

        [Display(Name = "From (330)", Order = 330)]
        public string From330 { get { return fromValues.GetValue(330); } set { fromValues.SetValue(330, value); } }
        [Display(Name = "To (331)", Order = 331)]
        public string To331 { get { return toValues.GetValue(331); } set { toValues.SetValue(331, value); } }
        [Display(Name = "Sent (332)", Order = 332)]
        public DateTime Sent332 { get { return sentValues.GetValue(332); } set { sentValues.SetValue(332, value); } }
        [Display(Name = "Has Attachment (333)", Order = 333)]
        public bool HasAttachment333 { get { return hasAttachmentValues.GetValue(333); } set { hasAttachmentValues.SetValue(333, value); } }
        [Display(Name = "Size (334)", Order = 334)]
        public int Size334 { get { return sizeValues.GetValue(334); } set { sizeValues.SetValue(334, value); } }
        [Display(Name = "Priority (335)", Order = 335), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority335 { get { return priorityValues.GetValue(335); } set { priorityValues.SetValue(335, value); } }
        [Display(Name = "Subject (336)", Order = 336), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject336 { get { return subjectValues.GetValue(336); } set { subjectValues.SetValue(336, value); } }

        [Display(Name = "From (337)", Order = 337)]
        public string From337 { get { return fromValues.GetValue(337); } set { fromValues.SetValue(337, value); } }
        [Display(Name = "To (338)", Order = 338)]
        public string To338 { get { return toValues.GetValue(338); } set { toValues.SetValue(338, value); } }
        [Display(Name = "Sent (339)", Order = 339)]
        public DateTime Sent339 { get { return sentValues.GetValue(339); } set { sentValues.SetValue(339, value); } }
        [Display(Name = "Has Attachment (340)", Order = 340)]
        public bool HasAttachment340 { get { return hasAttachmentValues.GetValue(340); } set { hasAttachmentValues.SetValue(340, value); } }
        [Display(Name = "Size (341)", Order = 341)]
        public int Size341 { get { return sizeValues.GetValue(341); } set { sizeValues.SetValue(341, value); } }
        [Display(Name = "Priority (342)", Order = 342), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority342 { get { return priorityValues.GetValue(342); } set { priorityValues.SetValue(342, value); } }
        [Display(Name = "Subject (343)", Order = 343), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject343 { get { return subjectValues.GetValue(343); } set { subjectValues.SetValue(343, value); } }

        [Display(Name = "From (344)", Order = 344)]
        public string From344 { get { return fromValues.GetValue(344); } set { fromValues.SetValue(344, value); } }
        [Display(Name = "To (345)", Order = 345)]
        public string To345 { get { return toValues.GetValue(345); } set { toValues.SetValue(345, value); } }
        [Display(Name = "Sent (346)", Order = 346)]
        public DateTime Sent346 { get { return sentValues.GetValue(346); } set { sentValues.SetValue(346, value); } }
        [Display(Name = "Has Attachment (347)", Order = 347)]
        public bool HasAttachment347 { get { return hasAttachmentValues.GetValue(347); } set { hasAttachmentValues.SetValue(347, value); } }
        [Display(Name = "Size (348)", Order = 348)]
        public int Size348 { get { return sizeValues.GetValue(348); } set { sizeValues.SetValue(348, value); } }
        [Display(Name = "Priority (349)", Order = 349), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority349 { get { return priorityValues.GetValue(349); } set { priorityValues.SetValue(349, value); } }
        [Display(Name = "Subject (350)", Order = 350), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject350 { get { return subjectValues.GetValue(350); } set { subjectValues.SetValue(350, value); } }

        [Display(Name = "From (351)", Order = 351)]
        public string From351 { get { return fromValues.GetValue(351); } set { fromValues.SetValue(351, value); } }
        [Display(Name = "To (352)", Order = 352)]
        public string To352 { get { return toValues.GetValue(352); } set { toValues.SetValue(352, value); } }
        [Display(Name = "Sent (353)", Order = 353)]
        public DateTime Sent353 { get { return sentValues.GetValue(353); } set { sentValues.SetValue(353, value); } }
        [Display(Name = "Has Attachment (354)", Order = 354)]
        public bool HasAttachment354 { get { return hasAttachmentValues.GetValue(354); } set { hasAttachmentValues.SetValue(354, value); } }
        [Display(Name = "Size (355)", Order = 355)]
        public int Size355 { get { return sizeValues.GetValue(355); } set { sizeValues.SetValue(355, value); } }
        [Display(Name = "Priority (356)", Order = 356), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority356 { get { return priorityValues.GetValue(356); } set { priorityValues.SetValue(356, value); } }
        [Display(Name = "Subject (357)", Order = 357), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject357 { get { return subjectValues.GetValue(357); } set { subjectValues.SetValue(357, value); } }

        [Display(Name = "From (358)", Order = 358)]
        public string From358 { get { return fromValues.GetValue(358); } set { fromValues.SetValue(358, value); } }
        [Display(Name = "To (359)", Order = 359)]
        public string To359 { get { return toValues.GetValue(359); } set { toValues.SetValue(359, value); } }
        [Display(Name = "Sent (360)", Order = 360)]
        public DateTime Sent360 { get { return sentValues.GetValue(360); } set { sentValues.SetValue(360, value); } }
        [Display(Name = "Has Attachment (361)", Order = 361)]
        public bool HasAttachment361 { get { return hasAttachmentValues.GetValue(361); } set { hasAttachmentValues.SetValue(361, value); } }
        [Display(Name = "Size (362)", Order = 362)]
        public int Size362 { get { return sizeValues.GetValue(362); } set { sizeValues.SetValue(362, value); } }
        [Display(Name = "Priority (363)", Order = 363), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority363 { get { return priorityValues.GetValue(363); } set { priorityValues.SetValue(363, value); } }
        [Display(Name = "Subject (364)", Order = 364), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject364 { get { return subjectValues.GetValue(364); } set { subjectValues.SetValue(364, value); } }

        [Display(Name = "From (365)", Order = 365)]
        public string From365 { get { return fromValues.GetValue(365); } set { fromValues.SetValue(365, value); } }
        [Display(Name = "To (366)", Order = 366)]
        public string To366 { get { return toValues.GetValue(366); } set { toValues.SetValue(366, value); } }
        [Display(Name = "Sent (367)", Order = 367)]
        public DateTime Sent367 { get { return sentValues.GetValue(367); } set { sentValues.SetValue(367, value); } }
        [Display(Name = "Has Attachment (368)", Order = 368)]
        public bool HasAttachment368 { get { return hasAttachmentValues.GetValue(368); } set { hasAttachmentValues.SetValue(368, value); } }
        [Display(Name = "Size (369)", Order = 369)]
        public int Size369 { get { return sizeValues.GetValue(369); } set { sizeValues.SetValue(369, value); } }
        [Display(Name = "Priority (370)", Order = 370), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority370 { get { return priorityValues.GetValue(370); } set { priorityValues.SetValue(370, value); } }
        [Display(Name = "Subject (371)", Order = 371), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject371 { get { return subjectValues.GetValue(371); } set { subjectValues.SetValue(371, value); } }

        [Display(Name = "From (372)", Order = 372)]
        public string From372 { get { return fromValues.GetValue(372); } set { fromValues.SetValue(372, value); } }
        [Display(Name = "To (373)", Order = 373)]
        public string To373 { get { return toValues.GetValue(373); } set { toValues.SetValue(373, value); } }
        [Display(Name = "Sent (374)", Order = 374)]
        public DateTime Sent374 { get { return sentValues.GetValue(374); } set { sentValues.SetValue(374, value); } }
        [Display(Name = "Has Attachment (375)", Order = 375)]
        public bool HasAttachment375 { get { return hasAttachmentValues.GetValue(375); } set { hasAttachmentValues.SetValue(375, value); } }
        [Display(Name = "Size (376)", Order = 376)]
        public int Size376 { get { return sizeValues.GetValue(376); } set { sizeValues.SetValue(376, value); } }
        [Display(Name = "Priority (377)", Order = 377), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority377 { get { return priorityValues.GetValue(377); } set { priorityValues.SetValue(377, value); } }
        [Display(Name = "Subject (378)", Order = 378), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject378 { get { return subjectValues.GetValue(378); } set { subjectValues.SetValue(378, value); } }

        [Display(Name = "From (379)", Order = 379)]
        public string From379 { get { return fromValues.GetValue(379); } set { fromValues.SetValue(379, value); } }
        [Display(Name = "To (380)", Order = 380)]
        public string To380 { get { return toValues.GetValue(380); } set { toValues.SetValue(380, value); } }
        [Display(Name = "Sent (381)", Order = 381)]
        public DateTime Sent381 { get { return sentValues.GetValue(381); } set { sentValues.SetValue(381, value); } }
        [Display(Name = "Has Attachment (382)", Order = 382)]
        public bool HasAttachment382 { get { return hasAttachmentValues.GetValue(382); } set { hasAttachmentValues.SetValue(382, value); } }
        [Display(Name = "Size (383)", Order = 383)]
        public int Size383 { get { return sizeValues.GetValue(383); } set { sizeValues.SetValue(383, value); } }
        [Display(Name = "Priority (384)", Order = 384), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority384 { get { return priorityValues.GetValue(384); } set { priorityValues.SetValue(384, value); } }
        [Display(Name = "Subject (385)", Order = 385), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject385 { get { return subjectValues.GetValue(385); } set { subjectValues.SetValue(385, value); } }

        [Display(Name = "From (386)", Order = 386)]
        public string From386 { get { return fromValues.GetValue(386); } set { fromValues.SetValue(386, value); } }
        [Display(Name = "To (387)", Order = 387)]
        public string To387 { get { return toValues.GetValue(387); } set { toValues.SetValue(387, value); } }
        [Display(Name = "Sent (388)", Order = 388)]
        public DateTime Sent388 { get { return sentValues.GetValue(388); } set { sentValues.SetValue(388, value); } }
        [Display(Name = "Has Attachment (389)", Order = 389)]
        public bool HasAttachment389 { get { return hasAttachmentValues.GetValue(389); } set { hasAttachmentValues.SetValue(389, value); } }
        [Display(Name = "Size (390)", Order = 390)]
        public int Size390 { get { return sizeValues.GetValue(390); } set { sizeValues.SetValue(390, value); } }
        [Display(Name = "Priority (391)", Order = 391), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority391 { get { return priorityValues.GetValue(391); } set { priorityValues.SetValue(391, value); } }
        [Display(Name = "Subject (392)", Order = 392), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject392 { get { return subjectValues.GetValue(392); } set { subjectValues.SetValue(392, value); } }

        [Display(Name = "From (393)", Order = 393)]
        public string From393 { get { return fromValues.GetValue(393); } set { fromValues.SetValue(393, value); } }
        [Display(Name = "To (394)", Order = 394)]
        public string To394 { get { return toValues.GetValue(394); } set { toValues.SetValue(394, value); } }
        [Display(Name = "Sent (395)", Order = 395)]
        public DateTime Sent395 { get { return sentValues.GetValue(395); } set { sentValues.SetValue(395, value); } }
        [Display(Name = "Has Attachment (396)", Order = 396)]
        public bool HasAttachment396 { get { return hasAttachmentValues.GetValue(396); } set { hasAttachmentValues.SetValue(396, value); } }
        [Display(Name = "Size (397)", Order = 397)]
        public int Size397 { get { return sizeValues.GetValue(397); } set { sizeValues.SetValue(397, value); } }
        [Display(Name = "Priority (398)", Order = 398), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority398 { get { return priorityValues.GetValue(398); } set { priorityValues.SetValue(398, value); } }
        [Display(Name = "Subject (399)", Order = 399), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject399 { get { return subjectValues.GetValue(399); } set { subjectValues.SetValue(399, value); } }

        [Display(Name = "From (400)", Order = 400)]
        public string From400 { get { return fromValues.GetValue(400); } set { fromValues.SetValue(400, value); } }
        [Display(Name = "To (401)", Order = 401)]
        public string To401 { get { return toValues.GetValue(401); } set { toValues.SetValue(401, value); } }
        [Display(Name = "Sent (402)", Order = 402)]
        public DateTime Sent402 { get { return sentValues.GetValue(402); } set { sentValues.SetValue(402, value); } }
        [Display(Name = "Has Attachment (403)", Order = 403)]
        public bool HasAttachment403 { get { return hasAttachmentValues.GetValue(403); } set { hasAttachmentValues.SetValue(403, value); } }
        [Display(Name = "Size (404)", Order = 404)]
        public int Size404 { get { return sizeValues.GetValue(404); } set { sizeValues.SetValue(404, value); } }
        [Display(Name = "Priority (405)", Order = 405), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority405 { get { return priorityValues.GetValue(405); } set { priorityValues.SetValue(405, value); } }
        [Display(Name = "Subject (406)", Order = 406), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject406 { get { return subjectValues.GetValue(406); } set { subjectValues.SetValue(406, value); } }

        [Display(Name = "From (407)", Order = 407)]
        public string From407 { get { return fromValues.GetValue(407); } set { fromValues.SetValue(407, value); } }
        [Display(Name = "To (408)", Order = 408)]
        public string To408 { get { return toValues.GetValue(408); } set { toValues.SetValue(408, value); } }
        [Display(Name = "Sent (409)", Order = 409)]
        public DateTime Sent409 { get { return sentValues.GetValue(409); } set { sentValues.SetValue(409, value); } }
        [Display(Name = "Has Attachment (410)", Order = 410)]
        public bool HasAttachment410 { get { return hasAttachmentValues.GetValue(410); } set { hasAttachmentValues.SetValue(410, value); } }
        [Display(Name = "Size (411)", Order = 411)]
        public int Size411 { get { return sizeValues.GetValue(411); } set { sizeValues.SetValue(411, value); } }
        [Display(Name = "Priority (412)", Order = 412), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority412 { get { return priorityValues.GetValue(412); } set { priorityValues.SetValue(412, value); } }
        [Display(Name = "Subject (413)", Order = 413), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject413 { get { return subjectValues.GetValue(413); } set { subjectValues.SetValue(413, value); } }

        [Display(Name = "From (414)", Order = 414)]
        public string From414 { get { return fromValues.GetValue(414); } set { fromValues.SetValue(414, value); } }
        [Display(Name = "To (415)", Order = 415)]
        public string To415 { get { return toValues.GetValue(415); } set { toValues.SetValue(415, value); } }
        [Display(Name = "Sent (416)", Order = 416)]
        public DateTime Sent416 { get { return sentValues.GetValue(416); } set { sentValues.SetValue(416, value); } }
        [Display(Name = "Has Attachment (417)", Order = 417)]
        public bool HasAttachment417 { get { return hasAttachmentValues.GetValue(417); } set { hasAttachmentValues.SetValue(417, value); } }
        [Display(Name = "Size (418)", Order = 418)]
        public int Size418 { get { return sizeValues.GetValue(418); } set { sizeValues.SetValue(418, value); } }
        [Display(Name = "Priority (419)", Order = 419), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority419 { get { return priorityValues.GetValue(419); } set { priorityValues.SetValue(419, value); } }
        [Display(Name = "Subject (420)", Order = 420), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject420 { get { return subjectValues.GetValue(420); } set { subjectValues.SetValue(420, value); } }

        [Display(Name = "From (421)", Order = 421)]
        public string From421 { get { return fromValues.GetValue(421); } set { fromValues.SetValue(421, value); } }
        [Display(Name = "To (422)", Order = 422)]
        public string To422 { get { return toValues.GetValue(422); } set { toValues.SetValue(422, value); } }
        [Display(Name = "Sent (423)", Order = 423)]
        public DateTime Sent423 { get { return sentValues.GetValue(423); } set { sentValues.SetValue(423, value); } }
        [Display(Name = "Has Attachment (424)", Order = 424)]
        public bool HasAttachment424 { get { return hasAttachmentValues.GetValue(424); } set { hasAttachmentValues.SetValue(424, value); } }
        [Display(Name = "Size (425)", Order = 425)]
        public int Size425 { get { return sizeValues.GetValue(425); } set { sizeValues.SetValue(425, value); } }
        [Display(Name = "Priority (426)", Order = 426), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority426 { get { return priorityValues.GetValue(426); } set { priorityValues.SetValue(426, value); } }
        [Display(Name = "Subject (427)", Order = 427), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject427 { get { return subjectValues.GetValue(427); } set { subjectValues.SetValue(427, value); } }

        [Display(Name = "From (428)", Order = 428)]
        public string From428 { get { return fromValues.GetValue(428); } set { fromValues.SetValue(428, value); } }
        [Display(Name = "To (429)", Order = 429)]
        public string To429 { get { return toValues.GetValue(429); } set { toValues.SetValue(429, value); } }
        [Display(Name = "Sent (430)", Order = 430)]
        public DateTime Sent430 { get { return sentValues.GetValue(430); } set { sentValues.SetValue(430, value); } }
        [Display(Name = "Has Attachment (431)", Order = 431)]
        public bool HasAttachment431 { get { return hasAttachmentValues.GetValue(431); } set { hasAttachmentValues.SetValue(431, value); } }
        [Display(Name = "Size (432)", Order = 432)]
        public int Size432 { get { return sizeValues.GetValue(432); } set { sizeValues.SetValue(432, value); } }
        [Display(Name = "Priority (433)", Order = 433), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority433 { get { return priorityValues.GetValue(433); } set { priorityValues.SetValue(433, value); } }
        [Display(Name = "Subject (434)", Order = 434), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject434 { get { return subjectValues.GetValue(434); } set { subjectValues.SetValue(434, value); } }

        [Display(Name = "From (435)", Order = 435)]
        public string From435 { get { return fromValues.GetValue(435); } set { fromValues.SetValue(435, value); } }
        [Display(Name = "To (436)", Order = 436)]
        public string To436 { get { return toValues.GetValue(436); } set { toValues.SetValue(436, value); } }
        [Display(Name = "Sent (437)", Order = 437)]
        public DateTime Sent437 { get { return sentValues.GetValue(437); } set { sentValues.SetValue(437, value); } }
        [Display(Name = "Has Attachment (438)", Order = 438)]
        public bool HasAttachment438 { get { return hasAttachmentValues.GetValue(438); } set { hasAttachmentValues.SetValue(438, value); } }
        [Display(Name = "Size (439)", Order = 439)]
        public int Size439 { get { return sizeValues.GetValue(439); } set { sizeValues.SetValue(439, value); } }
        [Display(Name = "Priority (440)", Order = 440), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority440 { get { return priorityValues.GetValue(440); } set { priorityValues.SetValue(440, value); } }
        [Display(Name = "Subject (441)", Order = 441), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject441 { get { return subjectValues.GetValue(441); } set { subjectValues.SetValue(441, value); } }

        [Display(Name = "From (442)", Order = 442)]
        public string From442 { get { return fromValues.GetValue(442); } set { fromValues.SetValue(442, value); } }
        [Display(Name = "To (443)", Order = 443)]
        public string To443 { get { return toValues.GetValue(443); } set { toValues.SetValue(443, value); } }
        [Display(Name = "Sent (444)", Order = 444)]
        public DateTime Sent444 { get { return sentValues.GetValue(444); } set { sentValues.SetValue(444, value); } }
        [Display(Name = "Has Attachment (445)", Order = 445)]
        public bool HasAttachment445 { get { return hasAttachmentValues.GetValue(445); } set { hasAttachmentValues.SetValue(445, value); } }
        [Display(Name = "Size (446)", Order = 446)]
        public int Size446 { get { return sizeValues.GetValue(446); } set { sizeValues.SetValue(446, value); } }
        [Display(Name = "Priority (447)", Order = 447), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority447 { get { return priorityValues.GetValue(447); } set { priorityValues.SetValue(447, value); } }
        [Display(Name = "Subject (448)", Order = 448), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject448 { get { return subjectValues.GetValue(448); } set { subjectValues.SetValue(448, value); } }

        [Display(Name = "From (449)", Order = 449)]
        public string From449 { get { return fromValues.GetValue(449); } set { fromValues.SetValue(449, value); } }
        [Display(Name = "To (450)", Order = 450)]
        public string To450 { get { return toValues.GetValue(450); } set { toValues.SetValue(450, value); } }
        [Display(Name = "Sent (451)", Order = 451)]
        public DateTime Sent451 { get { return sentValues.GetValue(451); } set { sentValues.SetValue(451, value); } }
        [Display(Name = "Has Attachment (452)", Order = 452)]
        public bool HasAttachment452 { get { return hasAttachmentValues.GetValue(452); } set { hasAttachmentValues.SetValue(452, value); } }
        [Display(Name = "Size (453)", Order = 453)]
        public int Size453 { get { return sizeValues.GetValue(453); } set { sizeValues.SetValue(453, value); } }
        [Display(Name = "Priority (454)", Order = 454), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority454 { get { return priorityValues.GetValue(454); } set { priorityValues.SetValue(454, value); } }
        [Display(Name = "Subject (455)", Order = 455), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject455 { get { return subjectValues.GetValue(455); } set { subjectValues.SetValue(455, value); } }

        [Display(Name = "From (456)", Order = 456)]
        public string From456 { get { return fromValues.GetValue(456); } set { fromValues.SetValue(456, value); } }
        [Display(Name = "To (457)", Order = 457)]
        public string To457 { get { return toValues.GetValue(457); } set { toValues.SetValue(457, value); } }
        [Display(Name = "Sent (458)", Order = 458)]
        public DateTime Sent458 { get { return sentValues.GetValue(458); } set { sentValues.SetValue(458, value); } }
        [Display(Name = "Has Attachment (459)", Order = 459)]
        public bool HasAttachment459 { get { return hasAttachmentValues.GetValue(459); } set { hasAttachmentValues.SetValue(459, value); } }
        [Display(Name = "Size (460)", Order = 460)]
        public int Size460 { get { return sizeValues.GetValue(460); } set { sizeValues.SetValue(460, value); } }
        [Display(Name = "Priority (461)", Order = 461), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority461 { get { return priorityValues.GetValue(461); } set { priorityValues.SetValue(461, value); } }
        [Display(Name = "Subject (462)", Order = 462), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject462 { get { return subjectValues.GetValue(462); } set { subjectValues.SetValue(462, value); } }

        [Display(Name = "From (463)", Order = 463)]
        public string From463 { get { return fromValues.GetValue(463); } set { fromValues.SetValue(463, value); } }
        [Display(Name = "To (464)", Order = 464)]
        public string To464 { get { return toValues.GetValue(464); } set { toValues.SetValue(464, value); } }
        [Display(Name = "Sent (465)", Order = 465)]
        public DateTime Sent465 { get { return sentValues.GetValue(465); } set { sentValues.SetValue(465, value); } }
        [Display(Name = "Has Attachment (466)", Order = 466)]
        public bool HasAttachment466 { get { return hasAttachmentValues.GetValue(466); } set { hasAttachmentValues.SetValue(466, value); } }
        [Display(Name = "Size (467)", Order = 467)]
        public int Size467 { get { return sizeValues.GetValue(467); } set { sizeValues.SetValue(467, value); } }
        [Display(Name = "Priority (468)", Order = 468), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority468 { get { return priorityValues.GetValue(468); } set { priorityValues.SetValue(468, value); } }
        [Display(Name = "Subject (469)", Order = 469), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject469 { get { return subjectValues.GetValue(469); } set { subjectValues.SetValue(469, value); } }

        [Display(Name = "From (470)", Order = 470)]
        public string From470 { get { return fromValues.GetValue(470); } set { fromValues.SetValue(470, value); } }
        [Display(Name = "To (471)", Order = 471)]
        public string To471 { get { return toValues.GetValue(471); } set { toValues.SetValue(471, value); } }
        [Display(Name = "Sent (472)", Order = 472)]
        public DateTime Sent472 { get { return sentValues.GetValue(472); } set { sentValues.SetValue(472, value); } }
        [Display(Name = "Has Attachment (473)", Order = 473)]
        public bool HasAttachment473 { get { return hasAttachmentValues.GetValue(473); } set { hasAttachmentValues.SetValue(473, value); } }
        [Display(Name = "Size (474)", Order = 474)]
        public int Size474 { get { return sizeValues.GetValue(474); } set { sizeValues.SetValue(474, value); } }
        [Display(Name = "Priority (475)", Order = 475), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority475 { get { return priorityValues.GetValue(475); } set { priorityValues.SetValue(475, value); } }
        [Display(Name = "Subject (476)", Order = 476), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject476 { get { return subjectValues.GetValue(476); } set { subjectValues.SetValue(476, value); } }

        [Display(Name = "From (477)", Order = 477)]
        public string From477 { get { return fromValues.GetValue(477); } set { fromValues.SetValue(477, value); } }
        [Display(Name = "To (478)", Order = 478)]
        public string To478 { get { return toValues.GetValue(478); } set { toValues.SetValue(478, value); } }
        [Display(Name = "Sent (479)", Order = 479)]
        public DateTime Sent479 { get { return sentValues.GetValue(479); } set { sentValues.SetValue(479, value); } }
        [Display(Name = "Has Attachment (480)", Order = 480)]
        public bool HasAttachment480 { get { return hasAttachmentValues.GetValue(480); } set { hasAttachmentValues.SetValue(480, value); } }
        [Display(Name = "Size (481)", Order = 481)]
        public int Size481 { get { return sizeValues.GetValue(481); } set { sizeValues.SetValue(481, value); } }
        [Display(Name = "Priority (482)", Order = 482), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority482 { get { return priorityValues.GetValue(482); } set { priorityValues.SetValue(482, value); } }
        [Display(Name = "Subject (483)", Order = 483), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject483 { get { return subjectValues.GetValue(483); } set { subjectValues.SetValue(483, value); } }

        [Display(Name = "From (484)", Order = 484)]
        public string From484 { get { return fromValues.GetValue(484); } set { fromValues.SetValue(484, value); } }
        [Display(Name = "To (485)", Order = 485)]
        public string To485 { get { return toValues.GetValue(485); } set { toValues.SetValue(485, value); } }
        [Display(Name = "Sent (486)", Order = 486)]
        public DateTime Sent486 { get { return sentValues.GetValue(486); } set { sentValues.SetValue(486, value); } }
        [Display(Name = "Has Attachment (487)", Order = 487)]
        public bool HasAttachment487 { get { return hasAttachmentValues.GetValue(487); } set { hasAttachmentValues.SetValue(487, value); } }
        [Display(Name = "Size (488)", Order = 488)]
        public int Size488 { get { return sizeValues.GetValue(488); } set { sizeValues.SetValue(488, value); } }
        [Display(Name = "Priority (489)", Order = 489), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority489 { get { return priorityValues.GetValue(489); } set { priorityValues.SetValue(489, value); } }
        [Display(Name = "Subject (490)", Order = 490), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject490 { get { return subjectValues.GetValue(490); } set { subjectValues.SetValue(490, value); } }

        [Display(Name = "From (491)", Order = 491)]
        public string From491 { get { return fromValues.GetValue(491); } set { fromValues.SetValue(491, value); } }
        [Display(Name = "To (492)", Order = 492)]
        public string To492 { get { return toValues.GetValue(492); } set { toValues.SetValue(492, value); } }
        [Display(Name = "Sent (493)", Order = 493)]
        public DateTime Sent493 { get { return sentValues.GetValue(493); } set { sentValues.SetValue(493, value); } }
        [Display(Name = "Has Attachment (494)", Order = 494)]
        public bool HasAttachment494 { get { return hasAttachmentValues.GetValue(494); } set { hasAttachmentValues.SetValue(494, value); } }
        [Display(Name = "Size (495)", Order = 495)]
        public int Size495 { get { return sizeValues.GetValue(495); } set { sizeValues.SetValue(495, value); } }
        [Display(Name = "Priority (496)", Order = 496), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority496 { get { return priorityValues.GetValue(496); } set { priorityValues.SetValue(496, value); } }
        [Display(Name = "Subject (497)", Order = 497), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject497 { get { return subjectValues.GetValue(497); } set { subjectValues.SetValue(497, value); } }

        [Display(Name = "From (498)", Order = 498)]
        public string From498 { get { return fromValues.GetValue(498); } set { fromValues.SetValue(498, value); } }
        [Display(Name = "To (499)", Order = 499)]
        public string To499 { get { return toValues.GetValue(499); } set { toValues.SetValue(499, value); } }
        #endregion
    }
    public class LargeDataSourceObjectImmense : LargeDataSourceObjectLarge {
        public LargeDataSourceObjectImmense(int id)
            : base(id) {
        }

        #region properties
        [Display(Name = "Sent (500)", Order = 500)]
        public DateTime Sent500 { get { return sentValues.GetValue(500); } set { sentValues.SetValue(500, value); } }
        [Display(Name = "Has Attachment (501)", Order = 501)]
        public bool HasAttachment501 { get { return hasAttachmentValues.GetValue(501); } set { hasAttachmentValues.SetValue(501, value); } }
        [Display(Name = "Size (502)", Order = 502)]
        public int Size502 { get { return sizeValues.GetValue(502); } set { sizeValues.SetValue(502, value); } }
        [Display(Name = "Priority (503)", Order = 503), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority503 { get { return priorityValues.GetValue(503); } set { priorityValues.SetValue(503, value); } }
        [Display(Name = "Subject (504)", Order = 504), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject504 { get { return subjectValues.GetValue(504); } set { subjectValues.SetValue(504, value); } }

        [Display(Name = "From (505)", Order = 505)]
        public string From505 { get { return fromValues.GetValue(505); } set { fromValues.SetValue(505, value); } }
        [Display(Name = "To (506)", Order = 506)]
        public string To506 { get { return toValues.GetValue(506); } set { toValues.SetValue(506, value); } }
        [Display(Name = "Sent (507)", Order = 507)]
        public DateTime Sent507 { get { return sentValues.GetValue(507); } set { sentValues.SetValue(507, value); } }
        [Display(Name = "Has Attachment (508)", Order = 508)]
        public bool HasAttachment508 { get { return hasAttachmentValues.GetValue(508); } set { hasAttachmentValues.SetValue(508, value); } }
        [Display(Name = "Size (509)", Order = 509)]
        public int Size509 { get { return sizeValues.GetValue(509); } set { sizeValues.SetValue(509, value); } }
        [Display(Name = "Priority (510)", Order = 510), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority510 { get { return priorityValues.GetValue(510); } set { priorityValues.SetValue(510, value); } }
        [Display(Name = "Subject (511)", Order = 511), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject511 { get { return subjectValues.GetValue(511); } set { subjectValues.SetValue(511, value); } }

        [Display(Name = "From (512)", Order = 512)]
        public string From512 { get { return fromValues.GetValue(512); } set { fromValues.SetValue(512, value); } }
        [Display(Name = "To (513)", Order = 513)]
        public string To513 { get { return toValues.GetValue(513); } set { toValues.SetValue(513, value); } }
        [Display(Name = "Sent (514)", Order = 514)]
        public DateTime Sent514 { get { return sentValues.GetValue(514); } set { sentValues.SetValue(514, value); } }
        [Display(Name = "Has Attachment (515)", Order = 515)]
        public bool HasAttachment515 { get { return hasAttachmentValues.GetValue(515); } set { hasAttachmentValues.SetValue(515, value); } }
        [Display(Name = "Size (516)", Order = 516)]
        public int Size516 { get { return sizeValues.GetValue(516); } set { sizeValues.SetValue(516, value); } }
        [Display(Name = "Priority (517)", Order = 517), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority517 { get { return priorityValues.GetValue(517); } set { priorityValues.SetValue(517, value); } }
        [Display(Name = "Subject (518)", Order = 518), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject518 { get { return subjectValues.GetValue(518); } set { subjectValues.SetValue(518, value); } }

        [Display(Name = "From (519)", Order = 519)]
        public string From519 { get { return fromValues.GetValue(519); } set { fromValues.SetValue(519, value); } }
        [Display(Name = "To (520)", Order = 520)]
        public string To520 { get { return toValues.GetValue(520); } set { toValues.SetValue(520, value); } }
        [Display(Name = "Sent (521)", Order = 521)]
        public DateTime Sent521 { get { return sentValues.GetValue(521); } set { sentValues.SetValue(521, value); } }
        [Display(Name = "Has Attachment (522)", Order = 522)]
        public bool HasAttachment522 { get { return hasAttachmentValues.GetValue(522); } set { hasAttachmentValues.SetValue(522, value); } }
        [Display(Name = "Size (523)", Order = 523)]
        public int Size523 { get { return sizeValues.GetValue(523); } set { sizeValues.SetValue(523, value); } }
        [Display(Name = "Priority (524)", Order = 524), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority524 { get { return priorityValues.GetValue(524); } set { priorityValues.SetValue(524, value); } }
        [Display(Name = "Subject (525)", Order = 525), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject525 { get { return subjectValues.GetValue(525); } set { subjectValues.SetValue(525, value); } }

        [Display(Name = "From (526)", Order = 526)]
        public string From526 { get { return fromValues.GetValue(526); } set { fromValues.SetValue(526, value); } }
        [Display(Name = "To (527)", Order = 527)]
        public string To527 { get { return toValues.GetValue(527); } set { toValues.SetValue(527, value); } }
        [Display(Name = "Sent (528)", Order = 528)]
        public DateTime Sent528 { get { return sentValues.GetValue(528); } set { sentValues.SetValue(528, value); } }
        [Display(Name = "Has Attachment (529)", Order = 529)]
        public bool HasAttachment529 { get { return hasAttachmentValues.GetValue(529); } set { hasAttachmentValues.SetValue(529, value); } }
        [Display(Name = "Size (530)", Order = 530)]
        public int Size530 { get { return sizeValues.GetValue(530); } set { sizeValues.SetValue(530, value); } }
        [Display(Name = "Priority (531)", Order = 531), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority531 { get { return priorityValues.GetValue(531); } set { priorityValues.SetValue(531, value); } }
        [Display(Name = "Subject (532)", Order = 532), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject532 { get { return subjectValues.GetValue(532); } set { subjectValues.SetValue(532, value); } }

        [Display(Name = "From (533)", Order = 533)]
        public string From533 { get { return fromValues.GetValue(533); } set { fromValues.SetValue(533, value); } }
        [Display(Name = "To (534)", Order = 534)]
        public string To534 { get { return toValues.GetValue(534); } set { toValues.SetValue(534, value); } }
        [Display(Name = "Sent (535)", Order = 535)]
        public DateTime Sent535 { get { return sentValues.GetValue(535); } set { sentValues.SetValue(535, value); } }
        [Display(Name = "Has Attachment (536)", Order = 536)]
        public bool HasAttachment536 { get { return hasAttachmentValues.GetValue(536); } set { hasAttachmentValues.SetValue(536, value); } }
        [Display(Name = "Size (537)", Order = 537)]
        public int Size537 { get { return sizeValues.GetValue(537); } set { sizeValues.SetValue(537, value); } }
        [Display(Name = "Priority (538)", Order = 538), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority538 { get { return priorityValues.GetValue(538); } set { priorityValues.SetValue(538, value); } }
        [Display(Name = "Subject (539)", Order = 539), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject539 { get { return subjectValues.GetValue(539); } set { subjectValues.SetValue(539, value); } }

        [Display(Name = "From (540)", Order = 540)]
        public string From540 { get { return fromValues.GetValue(540); } set { fromValues.SetValue(540, value); } }
        [Display(Name = "To (541)", Order = 541)]
        public string To541 { get { return toValues.GetValue(541); } set { toValues.SetValue(541, value); } }
        [Display(Name = "Sent (542)", Order = 542)]
        public DateTime Sent542 { get { return sentValues.GetValue(542); } set { sentValues.SetValue(542, value); } }
        [Display(Name = "Has Attachment (543)", Order = 543)]
        public bool HasAttachment543 { get { return hasAttachmentValues.GetValue(543); } set { hasAttachmentValues.SetValue(543, value); } }
        [Display(Name = "Size (544)", Order = 544)]
        public int Size544 { get { return sizeValues.GetValue(544); } set { sizeValues.SetValue(544, value); } }
        [Display(Name = "Priority (545)", Order = 545), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority545 { get { return priorityValues.GetValue(545); } set { priorityValues.SetValue(545, value); } }
        [Display(Name = "Subject (546)", Order = 546), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject546 { get { return subjectValues.GetValue(546); } set { subjectValues.SetValue(546, value); } }

        [Display(Name = "From (547)", Order = 547)]
        public string From547 { get { return fromValues.GetValue(547); } set { fromValues.SetValue(547, value); } }
        [Display(Name = "To (548)", Order = 548)]
        public string To548 { get { return toValues.GetValue(548); } set { toValues.SetValue(548, value); } }
        [Display(Name = "Sent (549)", Order = 549)]
        public DateTime Sent549 { get { return sentValues.GetValue(549); } set { sentValues.SetValue(549, value); } }
        [Display(Name = "Has Attachment (550)", Order = 550)]
        public bool HasAttachment550 { get { return hasAttachmentValues.GetValue(550); } set { hasAttachmentValues.SetValue(550, value); } }
        [Display(Name = "Size (551)", Order = 551)]
        public int Size551 { get { return sizeValues.GetValue(551); } set { sizeValues.SetValue(551, value); } }
        [Display(Name = "Priority (552)", Order = 552), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority552 { get { return priorityValues.GetValue(552); } set { priorityValues.SetValue(552, value); } }
        [Display(Name = "Subject (553)", Order = 553), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject553 { get { return subjectValues.GetValue(553); } set { subjectValues.SetValue(553, value); } }

        [Display(Name = "From (554)", Order = 554)]
        public string From554 { get { return fromValues.GetValue(554); } set { fromValues.SetValue(554, value); } }
        [Display(Name = "To (555)", Order = 555)]
        public string To555 { get { return toValues.GetValue(555); } set { toValues.SetValue(555, value); } }
        [Display(Name = "Sent (556)", Order = 556)]
        public DateTime Sent556 { get { return sentValues.GetValue(556); } set { sentValues.SetValue(556, value); } }
        [Display(Name = "Has Attachment (557)", Order = 557)]
        public bool HasAttachment557 { get { return hasAttachmentValues.GetValue(557); } set { hasAttachmentValues.SetValue(557, value); } }
        [Display(Name = "Size (558)", Order = 558)]
        public int Size558 { get { return sizeValues.GetValue(558); } set { sizeValues.SetValue(558, value); } }
        [Display(Name = "Priority (559)", Order = 559), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority559 { get { return priorityValues.GetValue(559); } set { priorityValues.SetValue(559, value); } }
        [Display(Name = "Subject (560)", Order = 560), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject560 { get { return subjectValues.GetValue(560); } set { subjectValues.SetValue(560, value); } }

        [Display(Name = "From (561)", Order = 561)]
        public string From561 { get { return fromValues.GetValue(561); } set { fromValues.SetValue(561, value); } }
        [Display(Name = "To (562)", Order = 562)]
        public string To562 { get { return toValues.GetValue(562); } set { toValues.SetValue(562, value); } }
        [Display(Name = "Sent (563)", Order = 563)]
        public DateTime Sent563 { get { return sentValues.GetValue(563); } set { sentValues.SetValue(563, value); } }
        [Display(Name = "Has Attachment (564)", Order = 564)]
        public bool HasAttachment564 { get { return hasAttachmentValues.GetValue(564); } set { hasAttachmentValues.SetValue(564, value); } }
        [Display(Name = "Size (565)", Order = 565)]
        public int Size565 { get { return sizeValues.GetValue(565); } set { sizeValues.SetValue(565, value); } }
        [Display(Name = "Priority (566)", Order = 566), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority566 { get { return priorityValues.GetValue(566); } set { priorityValues.SetValue(566, value); } }
        [Display(Name = "Subject (567)", Order = 567), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject567 { get { return subjectValues.GetValue(567); } set { subjectValues.SetValue(567, value); } }

        [Display(Name = "From (568)", Order = 568)]
        public string From568 { get { return fromValues.GetValue(568); } set { fromValues.SetValue(568, value); } }
        [Display(Name = "To (569)", Order = 569)]
        public string To569 { get { return toValues.GetValue(569); } set { toValues.SetValue(569, value); } }
        [Display(Name = "Sent (570)", Order = 570)]
        public DateTime Sent570 { get { return sentValues.GetValue(570); } set { sentValues.SetValue(570, value); } }
        [Display(Name = "Has Attachment (571)", Order = 571)]
        public bool HasAttachment571 { get { return hasAttachmentValues.GetValue(571); } set { hasAttachmentValues.SetValue(571, value); } }
        [Display(Name = "Size (572)", Order = 572)]
        public int Size572 { get { return sizeValues.GetValue(572); } set { sizeValues.SetValue(572, value); } }
        [Display(Name = "Priority (573)", Order = 573), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority573 { get { return priorityValues.GetValue(573); } set { priorityValues.SetValue(573, value); } }
        [Display(Name = "Subject (574)", Order = 574), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject574 { get { return subjectValues.GetValue(574); } set { subjectValues.SetValue(574, value); } }

        [Display(Name = "From (575)", Order = 575)]
        public string From575 { get { return fromValues.GetValue(575); } set { fromValues.SetValue(575, value); } }
        [Display(Name = "To (576)", Order = 576)]
        public string To576 { get { return toValues.GetValue(576); } set { toValues.SetValue(576, value); } }
        [Display(Name = "Sent (577)", Order = 577)]
        public DateTime Sent577 { get { return sentValues.GetValue(577); } set { sentValues.SetValue(577, value); } }
        [Display(Name = "Has Attachment (578)", Order = 578)]
        public bool HasAttachment578 { get { return hasAttachmentValues.GetValue(578); } set { hasAttachmentValues.SetValue(578, value); } }
        [Display(Name = "Size (579)", Order = 579)]
        public int Size579 { get { return sizeValues.GetValue(579); } set { sizeValues.SetValue(579, value); } }
        [Display(Name = "Priority (580)", Order = 580), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority580 { get { return priorityValues.GetValue(580); } set { priorityValues.SetValue(580, value); } }
        [Display(Name = "Subject (581)", Order = 581), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject581 { get { return subjectValues.GetValue(581); } set { subjectValues.SetValue(581, value); } }

        [Display(Name = "From (582)", Order = 582)]
        public string From582 { get { return fromValues.GetValue(582); } set { fromValues.SetValue(582, value); } }
        [Display(Name = "To (583)", Order = 583)]
        public string To583 { get { return toValues.GetValue(583); } set { toValues.SetValue(583, value); } }
        [Display(Name = "Sent (584)", Order = 584)]
        public DateTime Sent584 { get { return sentValues.GetValue(584); } set { sentValues.SetValue(584, value); } }
        [Display(Name = "Has Attachment (585)", Order = 585)]
        public bool HasAttachment585 { get { return hasAttachmentValues.GetValue(585); } set { hasAttachmentValues.SetValue(585, value); } }
        [Display(Name = "Size (586)", Order = 586)]
        public int Size586 { get { return sizeValues.GetValue(586); } set { sizeValues.SetValue(586, value); } }
        [Display(Name = "Priority (587)", Order = 587), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority587 { get { return priorityValues.GetValue(587); } set { priorityValues.SetValue(587, value); } }
        [Display(Name = "Subject (588)", Order = 588), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject588 { get { return subjectValues.GetValue(588); } set { subjectValues.SetValue(588, value); } }

        [Display(Name = "From (589)", Order = 589)]
        public string From589 { get { return fromValues.GetValue(589); } set { fromValues.SetValue(589, value); } }
        [Display(Name = "To (590)", Order = 590)]
        public string To590 { get { return toValues.GetValue(590); } set { toValues.SetValue(590, value); } }
        [Display(Name = "Sent (591)", Order = 591)]
        public DateTime Sent591 { get { return sentValues.GetValue(591); } set { sentValues.SetValue(591, value); } }
        [Display(Name = "Has Attachment (592)", Order = 592)]
        public bool HasAttachment592 { get { return hasAttachmentValues.GetValue(592); } set { hasAttachmentValues.SetValue(592, value); } }
        [Display(Name = "Size (593)", Order = 593)]
        public int Size593 { get { return sizeValues.GetValue(593); } set { sizeValues.SetValue(593, value); } }
        [Display(Name = "Priority (594)", Order = 594), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority594 { get { return priorityValues.GetValue(594); } set { priorityValues.SetValue(594, value); } }
        [Display(Name = "Subject (595)", Order = 595), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject595 { get { return subjectValues.GetValue(595); } set { subjectValues.SetValue(595, value); } }

        [Display(Name = "From (596)", Order = 596)]
        public string From596 { get { return fromValues.GetValue(596); } set { fromValues.SetValue(596, value); } }
        [Display(Name = "To (597)", Order = 597)]
        public string To597 { get { return toValues.GetValue(597); } set { toValues.SetValue(597, value); } }
        [Display(Name = "Sent (598)", Order = 598)]
        public DateTime Sent598 { get { return sentValues.GetValue(598); } set { sentValues.SetValue(598, value); } }
        [Display(Name = "Has Attachment (599)", Order = 599)]
        public bool HasAttachment599 { get { return hasAttachmentValues.GetValue(599); } set { hasAttachmentValues.SetValue(599, value); } }
        [Display(Name = "Size (600)", Order = 600)]
        public int Size600 { get { return sizeValues.GetValue(600); } set { sizeValues.SetValue(600, value); } }
        [Display(Name = "Priority (601)", Order = 601), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority601 { get { return priorityValues.GetValue(601); } set { priorityValues.SetValue(601, value); } }
        [Display(Name = "Subject (602)", Order = 602), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject602 { get { return subjectValues.GetValue(602); } set { subjectValues.SetValue(602, value); } }

        [Display(Name = "From (603)", Order = 603)]
        public string From603 { get { return fromValues.GetValue(603); } set { fromValues.SetValue(603, value); } }
        [Display(Name = "To (604)", Order = 604)]
        public string To604 { get { return toValues.GetValue(604); } set { toValues.SetValue(604, value); } }
        [Display(Name = "Sent (605)", Order = 605)]
        public DateTime Sent605 { get { return sentValues.GetValue(605); } set { sentValues.SetValue(605, value); } }
        [Display(Name = "Has Attachment (606)", Order = 606)]
        public bool HasAttachment606 { get { return hasAttachmentValues.GetValue(606); } set { hasAttachmentValues.SetValue(606, value); } }
        [Display(Name = "Size (607)", Order = 607)]
        public int Size607 { get { return sizeValues.GetValue(607); } set { sizeValues.SetValue(607, value); } }
        [Display(Name = "Priority (608)", Order = 608), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority608 { get { return priorityValues.GetValue(608); } set { priorityValues.SetValue(608, value); } }
        [Display(Name = "Subject (609)", Order = 609), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject609 { get { return subjectValues.GetValue(609); } set { subjectValues.SetValue(609, value); } }

        [Display(Name = "From (610)", Order = 610)]
        public string From610 { get { return fromValues.GetValue(610); } set { fromValues.SetValue(610, value); } }
        [Display(Name = "To (611)", Order = 611)]
        public string To611 { get { return toValues.GetValue(611); } set { toValues.SetValue(611, value); } }
        [Display(Name = "Sent (612)", Order = 612)]
        public DateTime Sent612 { get { return sentValues.GetValue(612); } set { sentValues.SetValue(612, value); } }
        [Display(Name = "Has Attachment (613)", Order = 613)]
        public bool HasAttachment613 { get { return hasAttachmentValues.GetValue(613); } set { hasAttachmentValues.SetValue(613, value); } }
        [Display(Name = "Size (614)", Order = 614)]
        public int Size614 { get { return sizeValues.GetValue(614); } set { sizeValues.SetValue(614, value); } }
        [Display(Name = "Priority (615)", Order = 615), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority615 { get { return priorityValues.GetValue(615); } set { priorityValues.SetValue(615, value); } }
        [Display(Name = "Subject (616)", Order = 616), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject616 { get { return subjectValues.GetValue(616); } set { subjectValues.SetValue(616, value); } }

        [Display(Name = "From (617)", Order = 617)]
        public string From617 { get { return fromValues.GetValue(617); } set { fromValues.SetValue(617, value); } }
        [Display(Name = "To (618)", Order = 618)]
        public string To618 { get { return toValues.GetValue(618); } set { toValues.SetValue(618, value); } }
        [Display(Name = "Sent (619)", Order = 619)]
        public DateTime Sent619 { get { return sentValues.GetValue(619); } set { sentValues.SetValue(619, value); } }
        [Display(Name = "Has Attachment (620)", Order = 620)]
        public bool HasAttachment620 { get { return hasAttachmentValues.GetValue(620); } set { hasAttachmentValues.SetValue(620, value); } }
        [Display(Name = "Size (621)", Order = 621)]
        public int Size621 { get { return sizeValues.GetValue(621); } set { sizeValues.SetValue(621, value); } }
        [Display(Name = "Priority (622)", Order = 622), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority622 { get { return priorityValues.GetValue(622); } set { priorityValues.SetValue(622, value); } }
        [Display(Name = "Subject (623)", Order = 623), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject623 { get { return subjectValues.GetValue(623); } set { subjectValues.SetValue(623, value); } }

        [Display(Name = "From (624)", Order = 624)]
        public string From624 { get { return fromValues.GetValue(624); } set { fromValues.SetValue(624, value); } }
        [Display(Name = "To (625)", Order = 625)]
        public string To625 { get { return toValues.GetValue(625); } set { toValues.SetValue(625, value); } }
        [Display(Name = "Sent (626)", Order = 626)]
        public DateTime Sent626 { get { return sentValues.GetValue(626); } set { sentValues.SetValue(626, value); } }
        [Display(Name = "Has Attachment (627)", Order = 627)]
        public bool HasAttachment627 { get { return hasAttachmentValues.GetValue(627); } set { hasAttachmentValues.SetValue(627, value); } }
        [Display(Name = "Size (628)", Order = 628)]
        public int Size628 { get { return sizeValues.GetValue(628); } set { sizeValues.SetValue(628, value); } }
        [Display(Name = "Priority (629)", Order = 629), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority629 { get { return priorityValues.GetValue(629); } set { priorityValues.SetValue(629, value); } }
        [Display(Name = "Subject (630)", Order = 630), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject630 { get { return subjectValues.GetValue(630); } set { subjectValues.SetValue(630, value); } }

        [Display(Name = "From (631)", Order = 631)]
        public string From631 { get { return fromValues.GetValue(631); } set { fromValues.SetValue(631, value); } }
        [Display(Name = "To (632)", Order = 632)]
        public string To632 { get { return toValues.GetValue(632); } set { toValues.SetValue(632, value); } }
        [Display(Name = "Sent (633)", Order = 633)]
        public DateTime Sent633 { get { return sentValues.GetValue(633); } set { sentValues.SetValue(633, value); } }
        [Display(Name = "Has Attachment (634)", Order = 634)]
        public bool HasAttachment634 { get { return hasAttachmentValues.GetValue(634); } set { hasAttachmentValues.SetValue(634, value); } }
        [Display(Name = "Size (635)", Order = 635)]
        public int Size635 { get { return sizeValues.GetValue(635); } set { sizeValues.SetValue(635, value); } }
        [Display(Name = "Priority (636)", Order = 636), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority636 { get { return priorityValues.GetValue(636); } set { priorityValues.SetValue(636, value); } }
        [Display(Name = "Subject (637)", Order = 637), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject637 { get { return subjectValues.GetValue(637); } set { subjectValues.SetValue(637, value); } }

        [Display(Name = "From (638)", Order = 638)]
        public string From638 { get { return fromValues.GetValue(638); } set { fromValues.SetValue(638, value); } }
        [Display(Name = "To (639)", Order = 639)]
        public string To639 { get { return toValues.GetValue(639); } set { toValues.SetValue(639, value); } }
        [Display(Name = "Sent (640)", Order = 640)]
        public DateTime Sent640 { get { return sentValues.GetValue(640); } set { sentValues.SetValue(640, value); } }
        [Display(Name = "Has Attachment (641)", Order = 641)]
        public bool HasAttachment641 { get { return hasAttachmentValues.GetValue(641); } set { hasAttachmentValues.SetValue(641, value); } }
        [Display(Name = "Size (642)", Order = 642)]
        public int Size642 { get { return sizeValues.GetValue(642); } set { sizeValues.SetValue(642, value); } }
        [Display(Name = "Priority (643)", Order = 643), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority643 { get { return priorityValues.GetValue(643); } set { priorityValues.SetValue(643, value); } }
        [Display(Name = "Subject (644)", Order = 644), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject644 { get { return subjectValues.GetValue(644); } set { subjectValues.SetValue(644, value); } }

        [Display(Name = "From (645)", Order = 645)]
        public string From645 { get { return fromValues.GetValue(645); } set { fromValues.SetValue(645, value); } }
        [Display(Name = "To (646)", Order = 646)]
        public string To646 { get { return toValues.GetValue(646); } set { toValues.SetValue(646, value); } }
        [Display(Name = "Sent (647)", Order = 647)]
        public DateTime Sent647 { get { return sentValues.GetValue(647); } set { sentValues.SetValue(647, value); } }
        [Display(Name = "Has Attachment (648)", Order = 648)]
        public bool HasAttachment648 { get { return hasAttachmentValues.GetValue(648); } set { hasAttachmentValues.SetValue(648, value); } }
        [Display(Name = "Size (649)", Order = 649)]
        public int Size649 { get { return sizeValues.GetValue(649); } set { sizeValues.SetValue(649, value); } }
        [Display(Name = "Priority (650)", Order = 650), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority650 { get { return priorityValues.GetValue(650); } set { priorityValues.SetValue(650, value); } }
        [Display(Name = "Subject (651)", Order = 651), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject651 { get { return subjectValues.GetValue(651); } set { subjectValues.SetValue(651, value); } }

        [Display(Name = "From (652)", Order = 652)]
        public string From652 { get { return fromValues.GetValue(652); } set { fromValues.SetValue(652, value); } }
        [Display(Name = "To (653)", Order = 653)]
        public string To653 { get { return toValues.GetValue(653); } set { toValues.SetValue(653, value); } }
        [Display(Name = "Sent (654)", Order = 654)]
        public DateTime Sent654 { get { return sentValues.GetValue(654); } set { sentValues.SetValue(654, value); } }
        [Display(Name = "Has Attachment (655)", Order = 655)]
        public bool HasAttachment655 { get { return hasAttachmentValues.GetValue(655); } set { hasAttachmentValues.SetValue(655, value); } }
        [Display(Name = "Size (656)", Order = 656)]
        public int Size656 { get { return sizeValues.GetValue(656); } set { sizeValues.SetValue(656, value); } }
        [Display(Name = "Priority (657)", Order = 657), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority657 { get { return priorityValues.GetValue(657); } set { priorityValues.SetValue(657, value); } }
        [Display(Name = "Subject (658)", Order = 658), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject658 { get { return subjectValues.GetValue(658); } set { subjectValues.SetValue(658, value); } }

        [Display(Name = "From (659)", Order = 659)]
        public string From659 { get { return fromValues.GetValue(659); } set { fromValues.SetValue(659, value); } }
        [Display(Name = "To (660)", Order = 660)]
        public string To660 { get { return toValues.GetValue(660); } set { toValues.SetValue(660, value); } }
        [Display(Name = "Sent (661)", Order = 661)]
        public DateTime Sent661 { get { return sentValues.GetValue(661); } set { sentValues.SetValue(661, value); } }
        [Display(Name = "Has Attachment (662)", Order = 662)]
        public bool HasAttachment662 { get { return hasAttachmentValues.GetValue(662); } set { hasAttachmentValues.SetValue(662, value); } }
        [Display(Name = "Size (663)", Order = 663)]
        public int Size663 { get { return sizeValues.GetValue(663); } set { sizeValues.SetValue(663, value); } }
        [Display(Name = "Priority (664)", Order = 664), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority664 { get { return priorityValues.GetValue(664); } set { priorityValues.SetValue(664, value); } }
        [Display(Name = "Subject (665)", Order = 665), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject665 { get { return subjectValues.GetValue(665); } set { subjectValues.SetValue(665, value); } }

        [Display(Name = "From (666)", Order = 666)]
        public string From666 { get { return fromValues.GetValue(666); } set { fromValues.SetValue(666, value); } }
        [Display(Name = "To (667)", Order = 667)]
        public string To667 { get { return toValues.GetValue(667); } set { toValues.SetValue(667, value); } }
        [Display(Name = "Sent (668)", Order = 668)]
        public DateTime Sent668 { get { return sentValues.GetValue(668); } set { sentValues.SetValue(668, value); } }
        [Display(Name = "Has Attachment (669)", Order = 669)]
        public bool HasAttachment669 { get { return hasAttachmentValues.GetValue(669); } set { hasAttachmentValues.SetValue(669, value); } }
        [Display(Name = "Size (670)", Order = 670)]
        public int Size670 { get { return sizeValues.GetValue(670); } set { sizeValues.SetValue(670, value); } }
        [Display(Name = "Priority (671)", Order = 671), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority671 { get { return priorityValues.GetValue(671); } set { priorityValues.SetValue(671, value); } }
        [Display(Name = "Subject (672)", Order = 672), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject672 { get { return subjectValues.GetValue(672); } set { subjectValues.SetValue(672, value); } }

        [Display(Name = "From (673)", Order = 673)]
        public string From673 { get { return fromValues.GetValue(673); } set { fromValues.SetValue(673, value); } }
        [Display(Name = "To (674)", Order = 674)]
        public string To674 { get { return toValues.GetValue(674); } set { toValues.SetValue(674, value); } }
        [Display(Name = "Sent (675)", Order = 675)]
        public DateTime Sent675 { get { return sentValues.GetValue(675); } set { sentValues.SetValue(675, value); } }
        [Display(Name = "Has Attachment (676)", Order = 676)]
        public bool HasAttachment676 { get { return hasAttachmentValues.GetValue(676); } set { hasAttachmentValues.SetValue(676, value); } }
        [Display(Name = "Size (677)", Order = 677)]
        public int Size677 { get { return sizeValues.GetValue(677); } set { sizeValues.SetValue(677, value); } }
        [Display(Name = "Priority (678)", Order = 678), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority678 { get { return priorityValues.GetValue(678); } set { priorityValues.SetValue(678, value); } }
        [Display(Name = "Subject (679)", Order = 679), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject679 { get { return subjectValues.GetValue(679); } set { subjectValues.SetValue(679, value); } }

        [Display(Name = "From (680)", Order = 680)]
        public string From680 { get { return fromValues.GetValue(680); } set { fromValues.SetValue(680, value); } }
        [Display(Name = "To (681)", Order = 681)]
        public string To681 { get { return toValues.GetValue(681); } set { toValues.SetValue(681, value); } }
        [Display(Name = "Sent (682)", Order = 682)]
        public DateTime Sent682 { get { return sentValues.GetValue(682); } set { sentValues.SetValue(682, value); } }
        [Display(Name = "Has Attachment (683)", Order = 683)]
        public bool HasAttachment683 { get { return hasAttachmentValues.GetValue(683); } set { hasAttachmentValues.SetValue(683, value); } }
        [Display(Name = "Size (684)", Order = 684)]
        public int Size684 { get { return sizeValues.GetValue(684); } set { sizeValues.SetValue(684, value); } }
        [Display(Name = "Priority (685)", Order = 685), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority685 { get { return priorityValues.GetValue(685); } set { priorityValues.SetValue(685, value); } }
        [Display(Name = "Subject (686)", Order = 686), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject686 { get { return subjectValues.GetValue(686); } set { subjectValues.SetValue(686, value); } }

        [Display(Name = "From (687)", Order = 687)]
        public string From687 { get { return fromValues.GetValue(687); } set { fromValues.SetValue(687, value); } }
        [Display(Name = "To (688)", Order = 688)]
        public string To688 { get { return toValues.GetValue(688); } set { toValues.SetValue(688, value); } }
        [Display(Name = "Sent (689)", Order = 689)]
        public DateTime Sent689 { get { return sentValues.GetValue(689); } set { sentValues.SetValue(689, value); } }
        [Display(Name = "Has Attachment (690)", Order = 690)]
        public bool HasAttachment690 { get { return hasAttachmentValues.GetValue(690); } set { hasAttachmentValues.SetValue(690, value); } }
        [Display(Name = "Size (691)", Order = 691)]
        public int Size691 { get { return sizeValues.GetValue(691); } set { sizeValues.SetValue(691, value); } }
        [Display(Name = "Priority (692)", Order = 692), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority692 { get { return priorityValues.GetValue(692); } set { priorityValues.SetValue(692, value); } }
        [Display(Name = "Subject (693)", Order = 693), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject693 { get { return subjectValues.GetValue(693); } set { subjectValues.SetValue(693, value); } }

        [Display(Name = "From (694)", Order = 694)]
        public string From694 { get { return fromValues.GetValue(694); } set { fromValues.SetValue(694, value); } }
        [Display(Name = "To (695)", Order = 695)]
        public string To695 { get { return toValues.GetValue(695); } set { toValues.SetValue(695, value); } }
        [Display(Name = "Sent (696)", Order = 696)]
        public DateTime Sent696 { get { return sentValues.GetValue(696); } set { sentValues.SetValue(696, value); } }
        [Display(Name = "Has Attachment (697)", Order = 697)]
        public bool HasAttachment697 { get { return hasAttachmentValues.GetValue(697); } set { hasAttachmentValues.SetValue(697, value); } }
        [Display(Name = "Size (698)", Order = 698)]
        public int Size698 { get { return sizeValues.GetValue(698); } set { sizeValues.SetValue(698, value); } }
        [Display(Name = "Priority (699)", Order = 699), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority699 { get { return priorityValues.GetValue(699); } set { priorityValues.SetValue(699, value); } }
        [Display(Name = "Subject (700)", Order = 700), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject700 { get { return subjectValues.GetValue(700); } set { subjectValues.SetValue(700, value); } }

        [Display(Name = "From (701)", Order = 701)]
        public string From701 { get { return fromValues.GetValue(701); } set { fromValues.SetValue(701, value); } }
        [Display(Name = "To (702)", Order = 702)]
        public string To702 { get { return toValues.GetValue(702); } set { toValues.SetValue(702, value); } }
        [Display(Name = "Sent (703)", Order = 703)]
        public DateTime Sent703 { get { return sentValues.GetValue(703); } set { sentValues.SetValue(703, value); } }
        [Display(Name = "Has Attachment (704)", Order = 704)]
        public bool HasAttachment704 { get { return hasAttachmentValues.GetValue(704); } set { hasAttachmentValues.SetValue(704, value); } }
        [Display(Name = "Size (705)", Order = 705)]
        public int Size705 { get { return sizeValues.GetValue(705); } set { sizeValues.SetValue(705, value); } }
        [Display(Name = "Priority (706)", Order = 706), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority706 { get { return priorityValues.GetValue(706); } set { priorityValues.SetValue(706, value); } }
        [Display(Name = "Subject (707)", Order = 707), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject707 { get { return subjectValues.GetValue(707); } set { subjectValues.SetValue(707, value); } }

        [Display(Name = "From (708)", Order = 708)]
        public string From708 { get { return fromValues.GetValue(708); } set { fromValues.SetValue(708, value); } }
        [Display(Name = "To (709)", Order = 709)]
        public string To709 { get { return toValues.GetValue(709); } set { toValues.SetValue(709, value); } }
        [Display(Name = "Sent (710)", Order = 710)]
        public DateTime Sent710 { get { return sentValues.GetValue(710); } set { sentValues.SetValue(710, value); } }
        [Display(Name = "Has Attachment (711)", Order = 711)]
        public bool HasAttachment711 { get { return hasAttachmentValues.GetValue(711); } set { hasAttachmentValues.SetValue(711, value); } }
        [Display(Name = "Size (712)", Order = 712)]
        public int Size712 { get { return sizeValues.GetValue(712); } set { sizeValues.SetValue(712, value); } }
        [Display(Name = "Priority (713)", Order = 713), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority713 { get { return priorityValues.GetValue(713); } set { priorityValues.SetValue(713, value); } }
        [Display(Name = "Subject (714)", Order = 714), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject714 { get { return subjectValues.GetValue(714); } set { subjectValues.SetValue(714, value); } }

        [Display(Name = "From (715)", Order = 715)]
        public string From715 { get { return fromValues.GetValue(715); } set { fromValues.SetValue(715, value); } }
        [Display(Name = "To (716)", Order = 716)]
        public string To716 { get { return toValues.GetValue(716); } set { toValues.SetValue(716, value); } }
        [Display(Name = "Sent (717)", Order = 717)]
        public DateTime Sent717 { get { return sentValues.GetValue(717); } set { sentValues.SetValue(717, value); } }
        [Display(Name = "Has Attachment (718)", Order = 718)]
        public bool HasAttachment718 { get { return hasAttachmentValues.GetValue(718); } set { hasAttachmentValues.SetValue(718, value); } }
        [Display(Name = "Size (719)", Order = 719)]
        public int Size719 { get { return sizeValues.GetValue(719); } set { sizeValues.SetValue(719, value); } }
        [Display(Name = "Priority (720)", Order = 720), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority720 { get { return priorityValues.GetValue(720); } set { priorityValues.SetValue(720, value); } }
        [Display(Name = "Subject (721)", Order = 721), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject721 { get { return subjectValues.GetValue(721); } set { subjectValues.SetValue(721, value); } }

        [Display(Name = "From (722)", Order = 722)]
        public string From722 { get { return fromValues.GetValue(722); } set { fromValues.SetValue(722, value); } }
        [Display(Name = "To (723)", Order = 723)]
        public string To723 { get { return toValues.GetValue(723); } set { toValues.SetValue(723, value); } }
        [Display(Name = "Sent (724)", Order = 724)]
        public DateTime Sent724 { get { return sentValues.GetValue(724); } set { sentValues.SetValue(724, value); } }
        [Display(Name = "Has Attachment (725)", Order = 725)]
        public bool HasAttachment725 { get { return hasAttachmentValues.GetValue(725); } set { hasAttachmentValues.SetValue(725, value); } }
        [Display(Name = "Size (726)", Order = 726)]
        public int Size726 { get { return sizeValues.GetValue(726); } set { sizeValues.SetValue(726, value); } }
        [Display(Name = "Priority (727)", Order = 727), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority727 { get { return priorityValues.GetValue(727); } set { priorityValues.SetValue(727, value); } }
        [Display(Name = "Subject (728)", Order = 728), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject728 { get { return subjectValues.GetValue(728); } set { subjectValues.SetValue(728, value); } }

        [Display(Name = "From (729)", Order = 729)]
        public string From729 { get { return fromValues.GetValue(729); } set { fromValues.SetValue(729, value); } }
        [Display(Name = "To (730)", Order = 730)]
        public string To730 { get { return toValues.GetValue(730); } set { toValues.SetValue(730, value); } }
        [Display(Name = "Sent (731)", Order = 731)]
        public DateTime Sent731 { get { return sentValues.GetValue(731); } set { sentValues.SetValue(731, value); } }
        [Display(Name = "Has Attachment (732)", Order = 732)]
        public bool HasAttachment732 { get { return hasAttachmentValues.GetValue(732); } set { hasAttachmentValues.SetValue(732, value); } }
        [Display(Name = "Size (733)", Order = 733)]
        public int Size733 { get { return sizeValues.GetValue(733); } set { sizeValues.SetValue(733, value); } }
        [Display(Name = "Priority (734)", Order = 734), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority734 { get { return priorityValues.GetValue(734); } set { priorityValues.SetValue(734, value); } }
        [Display(Name = "Subject (735)", Order = 735), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject735 { get { return subjectValues.GetValue(735); } set { subjectValues.SetValue(735, value); } }

        [Display(Name = "From (736)", Order = 736)]
        public string From736 { get { return fromValues.GetValue(736); } set { fromValues.SetValue(736, value); } }
        [Display(Name = "To (737)", Order = 737)]
        public string To737 { get { return toValues.GetValue(737); } set { toValues.SetValue(737, value); } }
        [Display(Name = "Sent (738)", Order = 738)]
        public DateTime Sent738 { get { return sentValues.GetValue(738); } set { sentValues.SetValue(738, value); } }
        [Display(Name = "Has Attachment (739)", Order = 739)]
        public bool HasAttachment739 { get { return hasAttachmentValues.GetValue(739); } set { hasAttachmentValues.SetValue(739, value); } }
        [Display(Name = "Size (740)", Order = 740)]
        public int Size740 { get { return sizeValues.GetValue(740); } set { sizeValues.SetValue(740, value); } }
        [Display(Name = "Priority (741)", Order = 741), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority741 { get { return priorityValues.GetValue(741); } set { priorityValues.SetValue(741, value); } }
        [Display(Name = "Subject (742)", Order = 742), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject742 { get { return subjectValues.GetValue(742); } set { subjectValues.SetValue(742, value); } }

        [Display(Name = "From (743)", Order = 743)]
        public string From743 { get { return fromValues.GetValue(743); } set { fromValues.SetValue(743, value); } }
        [Display(Name = "To (744)", Order = 744)]
        public string To744 { get { return toValues.GetValue(744); } set { toValues.SetValue(744, value); } }
        [Display(Name = "Sent (745)", Order = 745)]
        public DateTime Sent745 { get { return sentValues.GetValue(745); } set { sentValues.SetValue(745, value); } }
        [Display(Name = "Has Attachment (746)", Order = 746)]
        public bool HasAttachment746 { get { return hasAttachmentValues.GetValue(746); } set { hasAttachmentValues.SetValue(746, value); } }
        [Display(Name = "Size (747)", Order = 747)]
        public int Size747 { get { return sizeValues.GetValue(747); } set { sizeValues.SetValue(747, value); } }
        [Display(Name = "Priority (748)", Order = 748), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority748 { get { return priorityValues.GetValue(748); } set { priorityValues.SetValue(748, value); } }
        [Display(Name = "Subject (749)", Order = 749), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject749 { get { return subjectValues.GetValue(749); } set { subjectValues.SetValue(749, value); } }

        [Display(Name = "From (750)", Order = 750)]
        public string From750 { get { return fromValues.GetValue(750); } set { fromValues.SetValue(750, value); } }
        [Display(Name = "To (751)", Order = 751)]
        public string To751 { get { return toValues.GetValue(751); } set { toValues.SetValue(751, value); } }
        [Display(Name = "Sent (752)", Order = 752)]
        public DateTime Sent752 { get { return sentValues.GetValue(752); } set { sentValues.SetValue(752, value); } }
        [Display(Name = "Has Attachment (753)", Order = 753)]
        public bool HasAttachment753 { get { return hasAttachmentValues.GetValue(753); } set { hasAttachmentValues.SetValue(753, value); } }
        [Display(Name = "Size (754)", Order = 754)]
        public int Size754 { get { return sizeValues.GetValue(754); } set { sizeValues.SetValue(754, value); } }
        [Display(Name = "Priority (755)", Order = 755), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority755 { get { return priorityValues.GetValue(755); } set { priorityValues.SetValue(755, value); } }
        [Display(Name = "Subject (756)", Order = 756), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject756 { get { return subjectValues.GetValue(756); } set { subjectValues.SetValue(756, value); } }

        [Display(Name = "From (757)", Order = 757)]
        public string From757 { get { return fromValues.GetValue(757); } set { fromValues.SetValue(757, value); } }
        [Display(Name = "To (758)", Order = 758)]
        public string To758 { get { return toValues.GetValue(758); } set { toValues.SetValue(758, value); } }
        [Display(Name = "Sent (759)", Order = 759)]
        public DateTime Sent759 { get { return sentValues.GetValue(759); } set { sentValues.SetValue(759, value); } }
        [Display(Name = "Has Attachment (760)", Order = 760)]
        public bool HasAttachment760 { get { return hasAttachmentValues.GetValue(760); } set { hasAttachmentValues.SetValue(760, value); } }
        [Display(Name = "Size (761)", Order = 761)]
        public int Size761 { get { return sizeValues.GetValue(761); } set { sizeValues.SetValue(761, value); } }
        [Display(Name = "Priority (762)", Order = 762), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority762 { get { return priorityValues.GetValue(762); } set { priorityValues.SetValue(762, value); } }
        [Display(Name = "Subject (763)", Order = 763), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject763 { get { return subjectValues.GetValue(763); } set { subjectValues.SetValue(763, value); } }

        [Display(Name = "From (764)", Order = 764)]
        public string From764 { get { return fromValues.GetValue(764); } set { fromValues.SetValue(764, value); } }
        [Display(Name = "To (765)", Order = 765)]
        public string To765 { get { return toValues.GetValue(765); } set { toValues.SetValue(765, value); } }
        [Display(Name = "Sent (766)", Order = 766)]
        public DateTime Sent766 { get { return sentValues.GetValue(766); } set { sentValues.SetValue(766, value); } }
        [Display(Name = "Has Attachment (767)", Order = 767)]
        public bool HasAttachment767 { get { return hasAttachmentValues.GetValue(767); } set { hasAttachmentValues.SetValue(767, value); } }
        [Display(Name = "Size (768)", Order = 768)]
        public int Size768 { get { return sizeValues.GetValue(768); } set { sizeValues.SetValue(768, value); } }
        [Display(Name = "Priority (769)", Order = 769), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority769 { get { return priorityValues.GetValue(769); } set { priorityValues.SetValue(769, value); } }
        [Display(Name = "Subject (770)", Order = 770), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject770 { get { return subjectValues.GetValue(770); } set { subjectValues.SetValue(770, value); } }

        [Display(Name = "From (771)", Order = 771)]
        public string From771 { get { return fromValues.GetValue(771); } set { fromValues.SetValue(771, value); } }
        [Display(Name = "To (772)", Order = 772)]
        public string To772 { get { return toValues.GetValue(772); } set { toValues.SetValue(772, value); } }
        [Display(Name = "Sent (773)", Order = 773)]
        public DateTime Sent773 { get { return sentValues.GetValue(773); } set { sentValues.SetValue(773, value); } }
        [Display(Name = "Has Attachment (774)", Order = 774)]
        public bool HasAttachment774 { get { return hasAttachmentValues.GetValue(774); } set { hasAttachmentValues.SetValue(774, value); } }
        [Display(Name = "Size (775)", Order = 775)]
        public int Size775 { get { return sizeValues.GetValue(775); } set { sizeValues.SetValue(775, value); } }
        [Display(Name = "Priority (776)", Order = 776), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority776 { get { return priorityValues.GetValue(776); } set { priorityValues.SetValue(776, value); } }
        [Display(Name = "Subject (777)", Order = 777), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject777 { get { return subjectValues.GetValue(777); } set { subjectValues.SetValue(777, value); } }

        [Display(Name = "From (778)", Order = 778)]
        public string From778 { get { return fromValues.GetValue(778); } set { fromValues.SetValue(778, value); } }
        [Display(Name = "To (779)", Order = 779)]
        public string To779 { get { return toValues.GetValue(779); } set { toValues.SetValue(779, value); } }
        [Display(Name = "Sent (780)", Order = 780)]
        public DateTime Sent780 { get { return sentValues.GetValue(780); } set { sentValues.SetValue(780, value); } }
        [Display(Name = "Has Attachment (781)", Order = 781)]
        public bool HasAttachment781 { get { return hasAttachmentValues.GetValue(781); } set { hasAttachmentValues.SetValue(781, value); } }
        [Display(Name = "Size (782)", Order = 782)]
        public int Size782 { get { return sizeValues.GetValue(782); } set { sizeValues.SetValue(782, value); } }
        [Display(Name = "Priority (783)", Order = 783), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority783 { get { return priorityValues.GetValue(783); } set { priorityValues.SetValue(783, value); } }
        [Display(Name = "Subject (784)", Order = 784), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject784 { get { return subjectValues.GetValue(784); } set { subjectValues.SetValue(784, value); } }

        [Display(Name = "From (785)", Order = 785)]
        public string From785 { get { return fromValues.GetValue(785); } set { fromValues.SetValue(785, value); } }
        [Display(Name = "To (786)", Order = 786)]
        public string To786 { get { return toValues.GetValue(786); } set { toValues.SetValue(786, value); } }
        [Display(Name = "Sent (787)", Order = 787)]
        public DateTime Sent787 { get { return sentValues.GetValue(787); } set { sentValues.SetValue(787, value); } }
        [Display(Name = "Has Attachment (788)", Order = 788)]
        public bool HasAttachment788 { get { return hasAttachmentValues.GetValue(788); } set { hasAttachmentValues.SetValue(788, value); } }
        [Display(Name = "Size (789)", Order = 789)]
        public int Size789 { get { return sizeValues.GetValue(789); } set { sizeValues.SetValue(789, value); } }
        [Display(Name = "Priority (790)", Order = 790), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority790 { get { return priorityValues.GetValue(790); } set { priorityValues.SetValue(790, value); } }
        [Display(Name = "Subject (791)", Order = 791), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject791 { get { return subjectValues.GetValue(791); } set { subjectValues.SetValue(791, value); } }

        [Display(Name = "From (792)", Order = 792)]
        public string From792 { get { return fromValues.GetValue(792); } set { fromValues.SetValue(792, value); } }
        [Display(Name = "To (793)", Order = 793)]
        public string To793 { get { return toValues.GetValue(793); } set { toValues.SetValue(793, value); } }
        [Display(Name = "Sent (794)", Order = 794)]
        public DateTime Sent794 { get { return sentValues.GetValue(794); } set { sentValues.SetValue(794, value); } }
        [Display(Name = "Has Attachment (795)", Order = 795)]
        public bool HasAttachment795 { get { return hasAttachmentValues.GetValue(795); } set { hasAttachmentValues.SetValue(795, value); } }
        [Display(Name = "Size (796)", Order = 796)]
        public int Size796 { get { return sizeValues.GetValue(796); } set { sizeValues.SetValue(796, value); } }
        [Display(Name = "Priority (797)", Order = 797), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority797 { get { return priorityValues.GetValue(797); } set { priorityValues.SetValue(797, value); } }
        [Display(Name = "Subject (798)", Order = 798), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject798 { get { return subjectValues.GetValue(798); } set { subjectValues.SetValue(798, value); } }

        [Display(Name = "From (799)", Order = 799)]
        public string From799 { get { return fromValues.GetValue(799); } set { fromValues.SetValue(799, value); } }
        [Display(Name = "To (800)", Order = 800)]
        public string To800 { get { return toValues.GetValue(800); } set { toValues.SetValue(800, value); } }
        [Display(Name = "Sent (801)", Order = 801)]
        public DateTime Sent801 { get { return sentValues.GetValue(801); } set { sentValues.SetValue(801, value); } }
        [Display(Name = "Has Attachment (802)", Order = 802)]
        public bool HasAttachment802 { get { return hasAttachmentValues.GetValue(802); } set { hasAttachmentValues.SetValue(802, value); } }
        [Display(Name = "Size (803)", Order = 803)]
        public int Size803 { get { return sizeValues.GetValue(803); } set { sizeValues.SetValue(803, value); } }
        [Display(Name = "Priority (804)", Order = 804), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority804 { get { return priorityValues.GetValue(804); } set { priorityValues.SetValue(804, value); } }
        [Display(Name = "Subject (805)", Order = 805), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject805 { get { return subjectValues.GetValue(805); } set { subjectValues.SetValue(805, value); } }

        [Display(Name = "From (806)", Order = 806)]
        public string From806 { get { return fromValues.GetValue(806); } set { fromValues.SetValue(806, value); } }
        [Display(Name = "To (807)", Order = 807)]
        public string To807 { get { return toValues.GetValue(807); } set { toValues.SetValue(807, value); } }
        [Display(Name = "Sent (808)", Order = 808)]
        public DateTime Sent808 { get { return sentValues.GetValue(808); } set { sentValues.SetValue(808, value); } }
        [Display(Name = "Has Attachment (809)", Order = 809)]
        public bool HasAttachment809 { get { return hasAttachmentValues.GetValue(809); } set { hasAttachmentValues.SetValue(809, value); } }
        [Display(Name = "Size (810)", Order = 810)]
        public int Size810 { get { return sizeValues.GetValue(810); } set { sizeValues.SetValue(810, value); } }
        [Display(Name = "Priority (811)", Order = 811), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority811 { get { return priorityValues.GetValue(811); } set { priorityValues.SetValue(811, value); } }
        [Display(Name = "Subject (812)", Order = 812), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject812 { get { return subjectValues.GetValue(812); } set { subjectValues.SetValue(812, value); } }

        [Display(Name = "From (813)", Order = 813)]
        public string From813 { get { return fromValues.GetValue(813); } set { fromValues.SetValue(813, value); } }
        [Display(Name = "To (814)", Order = 814)]
        public string To814 { get { return toValues.GetValue(814); } set { toValues.SetValue(814, value); } }
        [Display(Name = "Sent (815)", Order = 815)]
        public DateTime Sent815 { get { return sentValues.GetValue(815); } set { sentValues.SetValue(815, value); } }
        [Display(Name = "Has Attachment (816)", Order = 816)]
        public bool HasAttachment816 { get { return hasAttachmentValues.GetValue(816); } set { hasAttachmentValues.SetValue(816, value); } }
        [Display(Name = "Size (817)", Order = 817)]
        public int Size817 { get { return sizeValues.GetValue(817); } set { sizeValues.SetValue(817, value); } }
        [Display(Name = "Priority (818)", Order = 818), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority818 { get { return priorityValues.GetValue(818); } set { priorityValues.SetValue(818, value); } }
        [Display(Name = "Subject (819)", Order = 819), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject819 { get { return subjectValues.GetValue(819); } set { subjectValues.SetValue(819, value); } }

        [Display(Name = "From (820)", Order = 820)]
        public string From820 { get { return fromValues.GetValue(820); } set { fromValues.SetValue(820, value); } }
        [Display(Name = "To (821)", Order = 821)]
        public string To821 { get { return toValues.GetValue(821); } set { toValues.SetValue(821, value); } }
        [Display(Name = "Sent (822)", Order = 822)]
        public DateTime Sent822 { get { return sentValues.GetValue(822); } set { sentValues.SetValue(822, value); } }
        [Display(Name = "Has Attachment (823)", Order = 823)]
        public bool HasAttachment823 { get { return hasAttachmentValues.GetValue(823); } set { hasAttachmentValues.SetValue(823, value); } }
        [Display(Name = "Size (824)", Order = 824)]
        public int Size824 { get { return sizeValues.GetValue(824); } set { sizeValues.SetValue(824, value); } }
        [Display(Name = "Priority (825)", Order = 825), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority825 { get { return priorityValues.GetValue(825); } set { priorityValues.SetValue(825, value); } }
        [Display(Name = "Subject (826)", Order = 826), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject826 { get { return subjectValues.GetValue(826); } set { subjectValues.SetValue(826, value); } }

        [Display(Name = "From (827)", Order = 827)]
        public string From827 { get { return fromValues.GetValue(827); } set { fromValues.SetValue(827, value); } }
        [Display(Name = "To (828)", Order = 828)]
        public string To828 { get { return toValues.GetValue(828); } set { toValues.SetValue(828, value); } }
        [Display(Name = "Sent (829)", Order = 829)]
        public DateTime Sent829 { get { return sentValues.GetValue(829); } set { sentValues.SetValue(829, value); } }
        [Display(Name = "Has Attachment (830)", Order = 830)]
        public bool HasAttachment830 { get { return hasAttachmentValues.GetValue(830); } set { hasAttachmentValues.SetValue(830, value); } }
        [Display(Name = "Size (831)", Order = 831)]
        public int Size831 { get { return sizeValues.GetValue(831); } set { sizeValues.SetValue(831, value); } }
        [Display(Name = "Priority (832)", Order = 832), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority832 { get { return priorityValues.GetValue(832); } set { priorityValues.SetValue(832, value); } }
        [Display(Name = "Subject (833)", Order = 833), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject833 { get { return subjectValues.GetValue(833); } set { subjectValues.SetValue(833, value); } }

        [Display(Name = "From (834)", Order = 834)]
        public string From834 { get { return fromValues.GetValue(834); } set { fromValues.SetValue(834, value); } }
        [Display(Name = "To (835)", Order = 835)]
        public string To835 { get { return toValues.GetValue(835); } set { toValues.SetValue(835, value); } }
        [Display(Name = "Sent (836)", Order = 836)]
        public DateTime Sent836 { get { return sentValues.GetValue(836); } set { sentValues.SetValue(836, value); } }
        [Display(Name = "Has Attachment (837)", Order = 837)]
        public bool HasAttachment837 { get { return hasAttachmentValues.GetValue(837); } set { hasAttachmentValues.SetValue(837, value); } }
        [Display(Name = "Size (838)", Order = 838)]
        public int Size838 { get { return sizeValues.GetValue(838); } set { sizeValues.SetValue(838, value); } }
        [Display(Name = "Priority (839)", Order = 839), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority839 { get { return priorityValues.GetValue(839); } set { priorityValues.SetValue(839, value); } }
        [Display(Name = "Subject (840)", Order = 840), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject840 { get { return subjectValues.GetValue(840); } set { subjectValues.SetValue(840, value); } }

        [Display(Name = "From (841)", Order = 841)]
        public string From841 { get { return fromValues.GetValue(841); } set { fromValues.SetValue(841, value); } }
        [Display(Name = "To (842)", Order = 842)]
        public string To842 { get { return toValues.GetValue(842); } set { toValues.SetValue(842, value); } }
        [Display(Name = "Sent (843)", Order = 843)]
        public DateTime Sent843 { get { return sentValues.GetValue(843); } set { sentValues.SetValue(843, value); } }
        [Display(Name = "Has Attachment (844)", Order = 844)]
        public bool HasAttachment844 { get { return hasAttachmentValues.GetValue(844); } set { hasAttachmentValues.SetValue(844, value); } }
        [Display(Name = "Size (845)", Order = 845)]
        public int Size845 { get { return sizeValues.GetValue(845); } set { sizeValues.SetValue(845, value); } }
        [Display(Name = "Priority (846)", Order = 846), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority846 { get { return priorityValues.GetValue(846); } set { priorityValues.SetValue(846, value); } }
        [Display(Name = "Subject (847)", Order = 847), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject847 { get { return subjectValues.GetValue(847); } set { subjectValues.SetValue(847, value); } }

        [Display(Name = "From (848)", Order = 848)]
        public string From848 { get { return fromValues.GetValue(848); } set { fromValues.SetValue(848, value); } }
        [Display(Name = "To (849)", Order = 849)]
        public string To849 { get { return toValues.GetValue(849); } set { toValues.SetValue(849, value); } }
        [Display(Name = "Sent (850)", Order = 850)]
        public DateTime Sent850 { get { return sentValues.GetValue(850); } set { sentValues.SetValue(850, value); } }
        [Display(Name = "Has Attachment (851)", Order = 851)]
        public bool HasAttachment851 { get { return hasAttachmentValues.GetValue(851); } set { hasAttachmentValues.SetValue(851, value); } }
        [Display(Name = "Size (852)", Order = 852)]
        public int Size852 { get { return sizeValues.GetValue(852); } set { sizeValues.SetValue(852, value); } }
        [Display(Name = "Priority (853)", Order = 853), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority853 { get { return priorityValues.GetValue(853); } set { priorityValues.SetValue(853, value); } }
        [Display(Name = "Subject (854)", Order = 854), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject854 { get { return subjectValues.GetValue(854); } set { subjectValues.SetValue(854, value); } }

        [Display(Name = "From (855)", Order = 855)]
        public string From855 { get { return fromValues.GetValue(855); } set { fromValues.SetValue(855, value); } }
        [Display(Name = "To (856)", Order = 856)]
        public string To856 { get { return toValues.GetValue(856); } set { toValues.SetValue(856, value); } }
        [Display(Name = "Sent (857)", Order = 857)]
        public DateTime Sent857 { get { return sentValues.GetValue(857); } set { sentValues.SetValue(857, value); } }
        [Display(Name = "Has Attachment (858)", Order = 858)]
        public bool HasAttachment858 { get { return hasAttachmentValues.GetValue(858); } set { hasAttachmentValues.SetValue(858, value); } }
        [Display(Name = "Size (859)", Order = 859)]
        public int Size859 { get { return sizeValues.GetValue(859); } set { sizeValues.SetValue(859, value); } }
        [Display(Name = "Priority (860)", Order = 860), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority860 { get { return priorityValues.GetValue(860); } set { priorityValues.SetValue(860, value); } }
        [Display(Name = "Subject (861)", Order = 861), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject861 { get { return subjectValues.GetValue(861); } set { subjectValues.SetValue(861, value); } }

        [Display(Name = "From (862)", Order = 862)]
        public string From862 { get { return fromValues.GetValue(862); } set { fromValues.SetValue(862, value); } }
        [Display(Name = "To (863)", Order = 863)]
        public string To863 { get { return toValues.GetValue(863); } set { toValues.SetValue(863, value); } }
        [Display(Name = "Sent (864)", Order = 864)]
        public DateTime Sent864 { get { return sentValues.GetValue(864); } set { sentValues.SetValue(864, value); } }
        [Display(Name = "Has Attachment (865)", Order = 865)]
        public bool HasAttachment865 { get { return hasAttachmentValues.GetValue(865); } set { hasAttachmentValues.SetValue(865, value); } }
        [Display(Name = "Size (866)", Order = 866)]
        public int Size866 { get { return sizeValues.GetValue(866); } set { sizeValues.SetValue(866, value); } }
        [Display(Name = "Priority (867)", Order = 867), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority867 { get { return priorityValues.GetValue(867); } set { priorityValues.SetValue(867, value); } }
        [Display(Name = "Subject (868)", Order = 868), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject868 { get { return subjectValues.GetValue(868); } set { subjectValues.SetValue(868, value); } }

        [Display(Name = "From (869)", Order = 869)]
        public string From869 { get { return fromValues.GetValue(869); } set { fromValues.SetValue(869, value); } }
        [Display(Name = "To (870)", Order = 870)]
        public string To870 { get { return toValues.GetValue(870); } set { toValues.SetValue(870, value); } }
        [Display(Name = "Sent (871)", Order = 871)]
        public DateTime Sent871 { get { return sentValues.GetValue(871); } set { sentValues.SetValue(871, value); } }
        [Display(Name = "Has Attachment (872)", Order = 872)]
        public bool HasAttachment872 { get { return hasAttachmentValues.GetValue(872); } set { hasAttachmentValues.SetValue(872, value); } }
        [Display(Name = "Size (873)", Order = 873)]
        public int Size873 { get { return sizeValues.GetValue(873); } set { sizeValues.SetValue(873, value); } }
        [Display(Name = "Priority (874)", Order = 874), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority874 { get { return priorityValues.GetValue(874); } set { priorityValues.SetValue(874, value); } }
        [Display(Name = "Subject (875)", Order = 875), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject875 { get { return subjectValues.GetValue(875); } set { subjectValues.SetValue(875, value); } }

        [Display(Name = "From (876)", Order = 876)]
        public string From876 { get { return fromValues.GetValue(876); } set { fromValues.SetValue(876, value); } }
        [Display(Name = "To (877)", Order = 877)]
        public string To877 { get { return toValues.GetValue(877); } set { toValues.SetValue(877, value); } }
        [Display(Name = "Sent (878)", Order = 878)]
        public DateTime Sent878 { get { return sentValues.GetValue(878); } set { sentValues.SetValue(878, value); } }
        [Display(Name = "Has Attachment (879)", Order = 879)]
        public bool HasAttachment879 { get { return hasAttachmentValues.GetValue(879); } set { hasAttachmentValues.SetValue(879, value); } }
        [Display(Name = "Size (880)", Order = 880)]
        public int Size880 { get { return sizeValues.GetValue(880); } set { sizeValues.SetValue(880, value); } }
        [Display(Name = "Priority (881)", Order = 881), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority881 { get { return priorityValues.GetValue(881); } set { priorityValues.SetValue(881, value); } }
        [Display(Name = "Subject (882)", Order = 882), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject882 { get { return subjectValues.GetValue(882); } set { subjectValues.SetValue(882, value); } }

        [Display(Name = "From (883)", Order = 883)]
        public string From883 { get { return fromValues.GetValue(883); } set { fromValues.SetValue(883, value); } }
        [Display(Name = "To (884)", Order = 884)]
        public string To884 { get { return toValues.GetValue(884); } set { toValues.SetValue(884, value); } }
        [Display(Name = "Sent (885)", Order = 885)]
        public DateTime Sent885 { get { return sentValues.GetValue(885); } set { sentValues.SetValue(885, value); } }
        [Display(Name = "Has Attachment (886)", Order = 886)]
        public bool HasAttachment886 { get { return hasAttachmentValues.GetValue(886); } set { hasAttachmentValues.SetValue(886, value); } }
        [Display(Name = "Size (887)", Order = 887)]
        public int Size887 { get { return sizeValues.GetValue(887); } set { sizeValues.SetValue(887, value); } }
        [Display(Name = "Priority (888)", Order = 888), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority888 { get { return priorityValues.GetValue(888); } set { priorityValues.SetValue(888, value); } }
        [Display(Name = "Subject (889)", Order = 889), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject889 { get { return subjectValues.GetValue(889); } set { subjectValues.SetValue(889, value); } }

        [Display(Name = "From (890)", Order = 890)]
        public string From890 { get { return fromValues.GetValue(890); } set { fromValues.SetValue(890, value); } }
        [Display(Name = "To (891)", Order = 891)]
        public string To891 { get { return toValues.GetValue(891); } set { toValues.SetValue(891, value); } }
        [Display(Name = "Sent (892)", Order = 892)]
        public DateTime Sent892 { get { return sentValues.GetValue(892); } set { sentValues.SetValue(892, value); } }
        [Display(Name = "Has Attachment (893)", Order = 893)]
        public bool HasAttachment893 { get { return hasAttachmentValues.GetValue(893); } set { hasAttachmentValues.SetValue(893, value); } }
        [Display(Name = "Size (894)", Order = 894)]
        public int Size894 { get { return sizeValues.GetValue(894); } set { sizeValues.SetValue(894, value); } }
        [Display(Name = "Priority (895)", Order = 895), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority895 { get { return priorityValues.GetValue(895); } set { priorityValues.SetValue(895, value); } }
        [Display(Name = "Subject (896)", Order = 896), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject896 { get { return subjectValues.GetValue(896); } set { subjectValues.SetValue(896, value); } }

        [Display(Name = "From (897)", Order = 897)]
        public string From897 { get { return fromValues.GetValue(897); } set { fromValues.SetValue(897, value); } }
        [Display(Name = "To (898)", Order = 898)]
        public string To898 { get { return toValues.GetValue(898); } set { toValues.SetValue(898, value); } }
        [Display(Name = "Sent (899)", Order = 899)]
        public DateTime Sent899 { get { return sentValues.GetValue(899); } set { sentValues.SetValue(899, value); } }
        [Display(Name = "Has Attachment (900)", Order = 900)]
        public bool HasAttachment900 { get { return hasAttachmentValues.GetValue(900); } set { hasAttachmentValues.SetValue(900, value); } }
        [Display(Name = "Size (901)", Order = 901)]
        public int Size901 { get { return sizeValues.GetValue(901); } set { sizeValues.SetValue(901, value); } }
        [Display(Name = "Priority (902)", Order = 902), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority902 { get { return priorityValues.GetValue(902); } set { priorityValues.SetValue(902, value); } }
        [Display(Name = "Subject (903)", Order = 903), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject903 { get { return subjectValues.GetValue(903); } set { subjectValues.SetValue(903, value); } }

        [Display(Name = "From (904)", Order = 904)]
        public string From904 { get { return fromValues.GetValue(904); } set { fromValues.SetValue(904, value); } }
        [Display(Name = "To (905)", Order = 905)]
        public string To905 { get { return toValues.GetValue(905); } set { toValues.SetValue(905, value); } }
        [Display(Name = "Sent (906)", Order = 906)]
        public DateTime Sent906 { get { return sentValues.GetValue(906); } set { sentValues.SetValue(906, value); } }
        [Display(Name = "Has Attachment (907)", Order = 907)]
        public bool HasAttachment907 { get { return hasAttachmentValues.GetValue(907); } set { hasAttachmentValues.SetValue(907, value); } }
        [Display(Name = "Size (908)", Order = 908)]
        public int Size908 { get { return sizeValues.GetValue(908); } set { sizeValues.SetValue(908, value); } }
        [Display(Name = "Priority (909)", Order = 909), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority909 { get { return priorityValues.GetValue(909); } set { priorityValues.SetValue(909, value); } }
        [Display(Name = "Subject (910)", Order = 910), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject910 { get { return subjectValues.GetValue(910); } set { subjectValues.SetValue(910, value); } }

        [Display(Name = "From (911)", Order = 911)]
        public string From911 { get { return fromValues.GetValue(911); } set { fromValues.SetValue(911, value); } }
        [Display(Name = "To (912)", Order = 912)]
        public string To912 { get { return toValues.GetValue(912); } set { toValues.SetValue(912, value); } }
        [Display(Name = "Sent (913)", Order = 913)]
        public DateTime Sent913 { get { return sentValues.GetValue(913); } set { sentValues.SetValue(913, value); } }
        [Display(Name = "Has Attachment (914)", Order = 914)]
        public bool HasAttachment914 { get { return hasAttachmentValues.GetValue(914); } set { hasAttachmentValues.SetValue(914, value); } }
        [Display(Name = "Size (915)", Order = 915)]
        public int Size915 { get { return sizeValues.GetValue(915); } set { sizeValues.SetValue(915, value); } }
        [Display(Name = "Priority (916)", Order = 916), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority916 { get { return priorityValues.GetValue(916); } set { priorityValues.SetValue(916, value); } }
        [Display(Name = "Subject (917)", Order = 917), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject917 { get { return subjectValues.GetValue(917); } set { subjectValues.SetValue(917, value); } }

        [Display(Name = "From (918)", Order = 918)]
        public string From918 { get { return fromValues.GetValue(918); } set { fromValues.SetValue(918, value); } }
        [Display(Name = "To (919)", Order = 919)]
        public string To919 { get { return toValues.GetValue(919); } set { toValues.SetValue(919, value); } }
        [Display(Name = "Sent (920)", Order = 920)]
        public DateTime Sent920 { get { return sentValues.GetValue(920); } set { sentValues.SetValue(920, value); } }
        [Display(Name = "Has Attachment (921)", Order = 921)]
        public bool HasAttachment921 { get { return hasAttachmentValues.GetValue(921); } set { hasAttachmentValues.SetValue(921, value); } }
        [Display(Name = "Size (922)", Order = 922)]
        public int Size922 { get { return sizeValues.GetValue(922); } set { sizeValues.SetValue(922, value); } }
        [Display(Name = "Priority (923)", Order = 923), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority923 { get { return priorityValues.GetValue(923); } set { priorityValues.SetValue(923, value); } }
        [Display(Name = "Subject (924)", Order = 924), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject924 { get { return subjectValues.GetValue(924); } set { subjectValues.SetValue(924, value); } }

        [Display(Name = "From (925)", Order = 925)]
        public string From925 { get { return fromValues.GetValue(925); } set { fromValues.SetValue(925, value); } }
        [Display(Name = "To (926)", Order = 926)]
        public string To926 { get { return toValues.GetValue(926); } set { toValues.SetValue(926, value); } }
        [Display(Name = "Sent (927)", Order = 927)]
        public DateTime Sent927 { get { return sentValues.GetValue(927); } set { sentValues.SetValue(927, value); } }
        [Display(Name = "Has Attachment (928)", Order = 928)]
        public bool HasAttachment928 { get { return hasAttachmentValues.GetValue(928); } set { hasAttachmentValues.SetValue(928, value); } }
        [Display(Name = "Size (929)", Order = 929)]
        public int Size929 { get { return sizeValues.GetValue(929); } set { sizeValues.SetValue(929, value); } }
        [Display(Name = "Priority (930)", Order = 930), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority930 { get { return priorityValues.GetValue(930); } set { priorityValues.SetValue(930, value); } }
        [Display(Name = "Subject (931)", Order = 931), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject931 { get { return subjectValues.GetValue(931); } set { subjectValues.SetValue(931, value); } }

        [Display(Name = "From (932)", Order = 932)]
        public string From932 { get { return fromValues.GetValue(932); } set { fromValues.SetValue(932, value); } }
        [Display(Name = "To (933)", Order = 933)]
        public string To933 { get { return toValues.GetValue(933); } set { toValues.SetValue(933, value); } }
        [Display(Name = "Sent (934)", Order = 934)]
        public DateTime Sent934 { get { return sentValues.GetValue(934); } set { sentValues.SetValue(934, value); } }
        [Display(Name = "Has Attachment (935)", Order = 935)]
        public bool HasAttachment935 { get { return hasAttachmentValues.GetValue(935); } set { hasAttachmentValues.SetValue(935, value); } }
        [Display(Name = "Size (936)", Order = 936)]
        public int Size936 { get { return sizeValues.GetValue(936); } set { sizeValues.SetValue(936, value); } }
        [Display(Name = "Priority (937)", Order = 937), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority937 { get { return priorityValues.GetValue(937); } set { priorityValues.SetValue(937, value); } }
        [Display(Name = "Subject (938)", Order = 938), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject938 { get { return subjectValues.GetValue(938); } set { subjectValues.SetValue(938, value); } }

        [Display(Name = "From (939)", Order = 939)]
        public string From939 { get { return fromValues.GetValue(939); } set { fromValues.SetValue(939, value); } }
        [Display(Name = "To (940)", Order = 940)]
        public string To940 { get { return toValues.GetValue(940); } set { toValues.SetValue(940, value); } }
        [Display(Name = "Sent (941)", Order = 941)]
        public DateTime Sent941 { get { return sentValues.GetValue(941); } set { sentValues.SetValue(941, value); } }
        [Display(Name = "Has Attachment (942)", Order = 942)]
        public bool HasAttachment942 { get { return hasAttachmentValues.GetValue(942); } set { hasAttachmentValues.SetValue(942, value); } }
        [Display(Name = "Size (943)", Order = 943)]
        public int Size943 { get { return sizeValues.GetValue(943); } set { sizeValues.SetValue(943, value); } }
        [Display(Name = "Priority (944)", Order = 944), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority944 { get { return priorityValues.GetValue(944); } set { priorityValues.SetValue(944, value); } }
        [Display(Name = "Subject (945)", Order = 945), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject945 { get { return subjectValues.GetValue(945); } set { subjectValues.SetValue(945, value); } }

        [Display(Name = "From (946)", Order = 946)]
        public string From946 { get { return fromValues.GetValue(946); } set { fromValues.SetValue(946, value); } }
        [Display(Name = "To (947)", Order = 947)]
        public string To947 { get { return toValues.GetValue(947); } set { toValues.SetValue(947, value); } }
        [Display(Name = "Sent (948)", Order = 948)]
        public DateTime Sent948 { get { return sentValues.GetValue(948); } set { sentValues.SetValue(948, value); } }
        [Display(Name = "Has Attachment (949)", Order = 949)]
        public bool HasAttachment949 { get { return hasAttachmentValues.GetValue(949); } set { hasAttachmentValues.SetValue(949, value); } }
        [Display(Name = "Size (950)", Order = 950)]
        public int Size950 { get { return sizeValues.GetValue(950); } set { sizeValues.SetValue(950, value); } }
        [Display(Name = "Priority (951)", Order = 951), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority951 { get { return priorityValues.GetValue(951); } set { priorityValues.SetValue(951, value); } }
        [Display(Name = "Subject (952)", Order = 952), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject952 { get { return subjectValues.GetValue(952); } set { subjectValues.SetValue(952, value); } }

        [Display(Name = "From (953)", Order = 953)]
        public string From953 { get { return fromValues.GetValue(953); } set { fromValues.SetValue(953, value); } }
        [Display(Name = "To (954)", Order = 954)]
        public string To954 { get { return toValues.GetValue(954); } set { toValues.SetValue(954, value); } }
        [Display(Name = "Sent (955)", Order = 955)]
        public DateTime Sent955 { get { return sentValues.GetValue(955); } set { sentValues.SetValue(955, value); } }
        [Display(Name = "Has Attachment (956)", Order = 956)]
        public bool HasAttachment956 { get { return hasAttachmentValues.GetValue(956); } set { hasAttachmentValues.SetValue(956, value); } }
        [Display(Name = "Size (957)", Order = 957)]
        public int Size957 { get { return sizeValues.GetValue(957); } set { sizeValues.SetValue(957, value); } }
        [Display(Name = "Priority (958)", Order = 958), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority958 { get { return priorityValues.GetValue(958); } set { priorityValues.SetValue(958, value); } }
        [Display(Name = "Subject (959)", Order = 959), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject959 { get { return subjectValues.GetValue(959); } set { subjectValues.SetValue(959, value); } }

        [Display(Name = "From (960)", Order = 960)]
        public string From960 { get { return fromValues.GetValue(960); } set { fromValues.SetValue(960, value); } }
        [Display(Name = "To (961)", Order = 961)]
        public string To961 { get { return toValues.GetValue(961); } set { toValues.SetValue(961, value); } }
        [Display(Name = "Sent (962)", Order = 962)]
        public DateTime Sent962 { get { return sentValues.GetValue(962); } set { sentValues.SetValue(962, value); } }
        [Display(Name = "Has Attachment (963)", Order = 963)]
        public bool HasAttachment963 { get { return hasAttachmentValues.GetValue(963); } set { hasAttachmentValues.SetValue(963, value); } }
        [Display(Name = "Size (964)", Order = 964)]
        public int Size964 { get { return sizeValues.GetValue(964); } set { sizeValues.SetValue(964, value); } }
        [Display(Name = "Priority (965)", Order = 965), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority965 { get { return priorityValues.GetValue(965); } set { priorityValues.SetValue(965, value); } }
        [Display(Name = "Subject (966)", Order = 966), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject966 { get { return subjectValues.GetValue(966); } set { subjectValues.SetValue(966, value); } }

        [Display(Name = "From (967)", Order = 967)]
        public string From967 { get { return fromValues.GetValue(967); } set { fromValues.SetValue(967, value); } }
        [Display(Name = "To (968)", Order = 968)]
        public string To968 { get { return toValues.GetValue(968); } set { toValues.SetValue(968, value); } }
        [Display(Name = "Sent (969)", Order = 969)]
        public DateTime Sent969 { get { return sentValues.GetValue(969); } set { sentValues.SetValue(969, value); } }
        [Display(Name = "Has Attachment (970)", Order = 970)]
        public bool HasAttachment970 { get { return hasAttachmentValues.GetValue(970); } set { hasAttachmentValues.SetValue(970, value); } }
        [Display(Name = "Size (971)", Order = 971)]
        public int Size971 { get { return sizeValues.GetValue(971); } set { sizeValues.SetValue(971, value); } }
        [Display(Name = "Priority (972)", Order = 972), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority972 { get { return priorityValues.GetValue(972); } set { priorityValues.SetValue(972, value); } }
        [Display(Name = "Subject (973)", Order = 973), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject973 { get { return subjectValues.GetValue(973); } set { subjectValues.SetValue(973, value); } }

        [Display(Name = "From (974)", Order = 974)]
        public string From974 { get { return fromValues.GetValue(974); } set { fromValues.SetValue(974, value); } }
        [Display(Name = "To (975)", Order = 975)]
        public string To975 { get { return toValues.GetValue(975); } set { toValues.SetValue(975, value); } }
        [Display(Name = "Sent (976)", Order = 976)]
        public DateTime Sent976 { get { return sentValues.GetValue(976); } set { sentValues.SetValue(976, value); } }
        [Display(Name = "Has Attachment (977)", Order = 977)]
        public bool HasAttachment977 { get { return hasAttachmentValues.GetValue(977); } set { hasAttachmentValues.SetValue(977, value); } }
        [Display(Name = "Size (978)", Order = 978)]
        public int Size978 { get { return sizeValues.GetValue(978); } set { sizeValues.SetValue(978, value); } }
        [Display(Name = "Priority (979)", Order = 979), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority979 { get { return priorityValues.GetValue(979); } set { priorityValues.SetValue(979, value); } }
        [Display(Name = "Subject (980)", Order = 980), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject980 { get { return subjectValues.GetValue(980); } set { subjectValues.SetValue(980, value); } }

        [Display(Name = "From (981)", Order = 981)]
        public string From981 { get { return fromValues.GetValue(981); } set { fromValues.SetValue(981, value); } }
        [Display(Name = "To (982)", Order = 982)]
        public string To982 { get { return toValues.GetValue(982); } set { toValues.SetValue(982, value); } }
        [Display(Name = "Sent (983)", Order = 983)]
        public DateTime Sent983 { get { return sentValues.GetValue(983); } set { sentValues.SetValue(983, value); } }
        [Display(Name = "Has Attachment (984)", Order = 984)]
        public bool HasAttachment984 { get { return hasAttachmentValues.GetValue(984); } set { hasAttachmentValues.SetValue(984, value); } }
        [Display(Name = "Size (985)", Order = 985)]
        public int Size985 { get { return sizeValues.GetValue(985); } set { sizeValues.SetValue(985, value); } }
        [Display(Name = "Priority (986)", Order = 986), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority986 { get { return priorityValues.GetValue(986); } set { priorityValues.SetValue(986, value); } }
        [Display(Name = "Subject (987)", Order = 987), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject987 { get { return subjectValues.GetValue(987); } set { subjectValues.SetValue(987, value); } }

        [Display(Name = "From (988)", Order = 988)]
        public string From988 { get { return fromValues.GetValue(988); } set { fromValues.SetValue(988, value); } }
        [Display(Name = "To (989)", Order = 989)]
        public string To989 { get { return toValues.GetValue(989); } set { toValues.SetValue(989, value); } }
        [Display(Name = "Sent (990)", Order = 990)]
        public DateTime Sent990 { get { return sentValues.GetValue(990); } set { sentValues.SetValue(990, value); } }
        [Display(Name = "Has Attachment (991)", Order = 991)]
        public bool HasAttachment991 { get { return hasAttachmentValues.GetValue(991); } set { hasAttachmentValues.SetValue(991, value); } }
        [Display(Name = "Size (992)", Order = 992)]
        public int Size992 { get { return sizeValues.GetValue(992); } set { sizeValues.SetValue(992, value); } }
        [Display(Name = "Priority (993)", Order = 993), GridEditor(TemplateKey = "priorityColumn")]
        public Priority Priority993 { get { return priorityValues.GetValue(993); } set { priorityValues.SetValue(993, value); } }
        [Display(Name = "Subject (994)", Order = 994), GridEditor(TemplateKey = "subjectEditor")]
        public string Subject994 { get { return subjectValues.GetValue(994); } set { subjectValues.SetValue(994, value); } }

        [Display(Name = "From (995)", Order = 995)]
        public string From995 { get { return fromValues.GetValue(995); } set { fromValues.SetValue(995, value); } }
        [Display(Name = "To (996)", Order = 996)]
        public string To996 { get { return toValues.GetValue(996); } set { toValues.SetValue(996, value); } }
        [Display(Name = "Sent (997)", Order = 997)]
        public DateTime Sent997 { get { return sentValues.GetValue(997); } set { sentValues.SetValue(997, value); } }
        [Display(Name = "Has Attachment (998)", Order = 998)]
        public bool HasAttachment998 { get { return hasAttachmentValues.GetValue(998); } set { hasAttachmentValues.SetValue(998, value); } }
        [Display(Name = "Size (999)", Order = 999)]
        public int Size999 { get { return sizeValues.GetValue(999); } set { sizeValues.SetValue(999, value); } }
        #endregion
    }
}
