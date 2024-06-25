using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using GridDemo.ModuleResources;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace GridDemo {
    public class CardViewExplorerFileSource {
        static readonly Func<string, TypeElement, SizeIcon, int, CardViewExplorerFileSource> factory 
            = ViewModelSource.Factory((string fullPath, TypeElement type, SizeIcon sizeType, int size) => new CardViewExplorerFileSource(fullPath, type, sizeType, size));
        public static CardViewExplorerFileSource Create(string fullPath, TypeElement type, SizeIcon sizeType, int size) {
            return factory(fullPath, type, sizeType, size);
        }

        static byte[] folder;
        static Dictionary<string, byte[]> cache = new Dictionary<string, byte[]>();

        [DependsOnProperties("FileName")]
        public char FileNameFirst {
            get { return char.ToUpper(FileName[0]); }
        }
        public virtual string FileName { get; set; }
        public virtual byte[] Icon { get; set; }
        public virtual int Size { get; set; }
        public string FullPath { get; private set; }
        public TypeElement Type { get; private set; }    

        public enum TypeElement {
            Folder,
            File,
            Drive
        }       

        protected CardViewExplorerFileSource(string fullPath, TypeElement type, SizeIcon sizeType, int size) {
            Type = type;
            FullPath = fullPath;
            SetIcon(sizeType, size);
        }

        public void Resize(SizeIcon sizeType, int sizeInt) {
            SetIcon(sizeType, sizeInt);
        }

        void SetIcon(SizeIcon sizeType, int sizeInt) {
            Size = sizeInt;
            Size size = new Size(sizeInt, sizeInt);
            switch(Type) {
                case TypeElement.Folder:
                    FileName = Path.GetFileName(FullPath);
                    if(folder == null) 
                        folder = CardViewExplorerIconManager.GetLargeIconByte(FullPath, false);
                    Icon = folder;
                    break;
                case TypeElement.File:
                    FileName = Path.GetFileName(FullPath);
                    string ext = System.IO.Path.GetExtension(FullPath);
                    if(ext == ".exe" || ext == ".lnk") {
                        Icon = CardViewExplorerIconManager.GetLargeIconByte(FullPath, true, sizeType);
                    }
                    else if(!cache.ContainsKey(ext)) {
                            byte[] bi = CardViewExplorerIconManager.GetLargeIconByte(FullPath, true, sizeType);
                            cache.Add(ext, bi);
                            Icon = bi;
                          } else
                            Icon = cache[ext];
                    break;
                case TypeElement.Drive:
                    FileName = FullPath;
                    Icon = CardViewExplorerIconManager.GetLargeIconByte(FullPath, false);                  
                    break;
                default:
                    break;
            }
        }

        public static void ClearCache() {
            cache.Clear();
            folder = null;
        }
    }
}
